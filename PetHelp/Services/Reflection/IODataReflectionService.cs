using PetHelp.Dtos.Base;
using System.Reflection;

namespace PetHelp.Services.Reflection
{
    public interface IODataReflectionService
    {
        bool ForeignKeyExists(Type type, PropertyInfo relationship);
        IQueryable<BaseDto>? GetDbSet(string propertyName);
        int? GetForeignKey<T>(PropertyInfo relationship) where T : BaseDto;
        IQueryable<BaseDto>? GetDbSet(Type type);
    }
}