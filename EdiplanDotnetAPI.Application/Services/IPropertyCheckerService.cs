namespace EdiplanDotnetAPI.Application.Services;

public interface IPropertyCheckerService
{
    bool TypeHasProperties<T>(string? fields);
}