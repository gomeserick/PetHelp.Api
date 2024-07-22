using Microsoft.OData.ModelBuilder;
using PetHelp.Dtos.Base;
using System.Linq.Expressions;

namespace PetHelp.Extensions
{
    public static class EntitySetConfigurationExtensions
    {
        public static void IgnorePrivateData<T>(this EntitySetConfiguration<T> builder) where T : BaseDto => 
            typeof(T).GetProperties()
                .Where(e => e.PropertyType.IsSubclassOf(typeof(PrivateDataDto)))
                .ToList()
                .ForEach(e => builder.EntityType
                    .Ignore(e => typeof(PrivateDataDto)
                        .GetProperty(nameof(PrivateDataDto.User))
                        .GetValue(e)));
        
        public static EntitySetConfiguration<TStructuralType> Ignoring<TStructuralType, TProperty>(this EntitySetConfiguration<TStructuralType> builder, Expression<Func<TStructuralType, TProperty>> propertyExpression) where TStructuralType : class
                                                                                                                                                                                       where TProperty : notnull
        {
            builder.EntityType.Ignore(propertyExpression);
            return builder;
        }
    }
}
