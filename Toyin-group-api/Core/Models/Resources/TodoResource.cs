namespace Toyin_group_api.Core.Models.Resources;

  

    public record TodoResource(
    Guid Id,
    string TodoName,
    bool IsCompleted,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

