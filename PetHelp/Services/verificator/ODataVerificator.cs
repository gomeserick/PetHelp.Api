using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PetHelp.Dtos;
using PetHelp.Dtos.Base;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;
using PetHelp.Services.Reflection;
using System.Collections;
using System.Reflection;

namespace PetHelp.Services.verificator
{
    public class ODataVerificator<T> : IODataVerificator<T> where T : BaseDto
    {
        private readonly DatabaseContext _dbContext;
        private readonly INotificatorService _notificator;
        private readonly IODataReflectionService _reflection;
        public ODataVerificator(
            DatabaseContext dbContext,
            INotificatorService notificator,
            IODataReflectionService reflection)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _notificator = notificator ?? throw new ArgumentNullException(nameof(notificator));
            _reflection = reflection ?? throw new ArgumentNullException(nameof(reflection));
        }

        public async Task<bool> VerifyExistency(int id)
        {
            var set = _dbContext.Set<T>();

            var exists = await set.AnyAsync(e => e.Id == id);

            if (!exists)
            {
                _notificator.Notify(nameof(T), $"There is no {nameof(T)} with id {id}");
                return false;
            }

            return true;
        }

        public async Task<bool> VerifyInexistency(int id)
        {
            var set = _dbContext.Set<T>();

            var exists = await set.AnyAsync(e => e.Id == id);

            if (exists)
            {
                _notificator.Notify(nameof(T), $"There already a {nameof(T)} with id {id}");
                return false;
            }

            return true;
        }

        public async Task<bool> VerifyDependency(int id)
        {
            var properties = typeof(T).GetProperties();

            foreach (var property in properties.Where(e => e.PropertyType.Namespace == "PetHelp.Dtos" && e.PropertyType is not IEnumerable))
            {
                if (property is null)
                    continue;

                if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                    continue;

                var prop = _reflection.GetDbSet(property.Name);
                var key = _reflection.GetForeignKey<T>(property);

                if (prop == null)
                {
                    return false;
                }

                var exist = await prop.AnyAsync(e => e.Id == key);

                if (!exist)
                {
                    _notificator.Notify(nameof(T), $"There is no {property.Name} with id {key}");
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> VerifyDependents(int Id)
        {
            // Props clinica
            var tProps = typeof(T).GetProperties();
            // prop = list de animais
            foreach (var tIEnumProp in tProps.Where(e => e.PropertyType == typeof(IEnumerable) && e.PropertyType.ContainsGenericParameters))
            {
                // classe de animal
                var kType = tIEnumProp.PropertyType.GetGenericArguments().First();

                var kPropOfTType = kType.GetProperties().Where(e => e.GetType() == typeof(T) || (e.GetType().GetGenericArguments().Length == 1 && e.GetType().GetGenericArguments().First() == typeof(T)));
                var kPropOfTTypeId = kType.GetProperties().Where(e => e.GetType() == typeof(T) || (e.GetType().GetGenericArguments().Length == 1 && e.GetType().GetGenericArguments().First() == typeof(T)));

                if (!kPropOfTType.Any())
                {
                    return true;
                }


            }

            return true;
        }
    }
}
