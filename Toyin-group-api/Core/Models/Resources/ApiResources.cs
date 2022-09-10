namespace Toyin_group_api.Core.Models.Resources;

/// <summary>
/// Resource for status
/// </summary>
public record StatusResource(string Code = ResourceCodes.Success, string Message = "");

/// <summary>
/// Resource for object
/// </summary>
/// <typeparam name="TResource"></typeparam>

public record ObjectResource<TResource> : StatusResource
{
    public ObjectResource(string code = ResourceCodes.Success, string message = "") : base(code, message) { }
    public TResource Data { get; init; }
}

/// <summary>
/// Resource for List
/// </summary>
/// <typeparam name="TResource"></typeparam>
/// 
public record ListResource<TResource> : StatusResource
{
    public ListResource(string code = ResourceCodes.Success, string message = "") : base(code, message) { }
    public long Total { get; init; }
    public IReadOnlyCollection<TResource> Data { get; init; }
}