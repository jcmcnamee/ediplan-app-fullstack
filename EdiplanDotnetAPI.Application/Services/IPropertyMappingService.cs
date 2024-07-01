
namespace EdiplanDotnetAPI.Application.Services;

public interface IPropertyMappingService
{
    Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestionation>();
    bool ValidMappingExistsFor<TSource, TDestination>(string fields);
}