namespace Toyin_group_api.Core.Entities;

    public class Todo : BaseEntity
    {
    public string TodoName { get; set; }
    public bool IsCompleted { get; set; } = false;
}

