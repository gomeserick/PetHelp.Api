using PetHelp.Dtos.Base;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;
using System.Reflection;

namespace PetHelp.Services.Reflection
{
    public class ODataReflectionService : IODataReflectionService
    {
        private readonly INotificatorService _notificator;
        private readonly DatabaseContext _dbContext;
        public ODataReflectionService(DatabaseContext dbContext, INotificatorService notificator)
        {
            _notificator = notificator ?? throw new ArgumentNullException(nameof(notificator));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IQueryable<BaseDto>? GetDbSet(string propertyName)
        {
            var typeName = propertyName;
            var prop = typeof(DatabaseContext).GetProperties().FirstOrDefault(e => e.PropertyType.GetGenericArguments().FirstOrDefault()?.Name == propertyName)!;

            if (prop == null)
            {
                _notificator.Notify(typeName, $"There is no DbSet of Type {typeName} in the Database Context");
                return null;
            }

            var acessor = prop.GetAccessors().FirstOrDefault(e => e.ReturnType != typeof(void));

            if (acessor == null)
            {
                _notificator.Notify(typeName, $"The DbSet for {typeName} is private or has no get accessor");
                return null;
            }

            var result = (IQueryable<BaseDto>?)acessor.Invoke(_dbContext, Array.Empty<object>());

            if (result == null)
            {
                _notificator.Notify(typeName, $"An error has ocorred while trying to invoke accessor from DbSet of {typeName}");
                return null;
            }

            return result;
        }

        public IQueryable<BaseDto>? GetDbSet(Type type)
        {
            return (IQueryable<BaseDto>?)_dbContext.GetType().GetProperties().First(e => e.PropertyType.GetGenericArguments() == type.GetGenericArguments()).GetAccessors().First(e => e.ReturnType != typeof(void)).Invoke(_dbContext, Array.Empty<object>());
        }

        public int? GetForeignKey<TDto>(PropertyInfo relationship) where TDto : BaseDto
        {
            var typeName = nameof(TDto);
            var propTypeName = relationship.PropertyType.Name;
            var prop = typeof(TDto).GetProperties().FirstOrDefault(e => e.Name == relationship.Name + "Id");

            if (prop == null)
            {
                _notificator.Notify(typeName, $"There is no foreign key for {propTypeName} in the class {typeName}");
                return null;
            }

            var acessor = prop.GetAccessors().FirstOrDefault(e => e.ReturnType != typeof(void));

            if (acessor == null)
            {
                _notificator.Notify(typeName, $"The property for the foreign key of {propTypeName} in the class {typeName} is private or has no get accessor");
                return null;
            }

            var result = (int?)acessor.Invoke(_dbContext, Array.Empty<object>());

            if (result == null)
            {
                _notificator.Notify(typeName, $"An error has ocorred while trying to invoke accessor of {typeName}");
                return null;
            }

            return result;
        }

        public bool ForeignKeyExists(Type type, PropertyInfo relationship)
        {
            var prop = type.GetProperties().FirstOrDefault(e => e.Name == relationship.Name + "Id");

            if (prop == null)
            {
                return false;
            }

            var acessor = prop.GetAccessors().FirstOrDefault(e => e.ReturnType != typeof(void));

            if (acessor == null)
            {
                return false;
            }

            var result = (int?)acessor.Invoke(_dbContext, Array.Empty<object>());

            if (result == null)
            {
                return false;
            }

            return true;
        }
    }
}
