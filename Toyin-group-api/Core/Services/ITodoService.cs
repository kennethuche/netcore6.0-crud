using Toyin_group_api.Core.Models.Payload;
using Toyin_group_api.Core.Models.Resources;

namespace Toyin_group_api.Core.Services;

    public interface ITodoService
    {

    Task<ObjectResource<TodoResource>> CreateTodoAsync(CreateTodoPayload payload);
    Task<StatusResource> UpdateTodoAsync(Guid id, UpdateTodoPayload payload);
    Task<ListResource<TodoResource>> GetAllTodosAsync();
    Task<ObjectResource<string>> DeleteTodoAsync(Guid id);
    Task<ObjectResource<TodoResource>> GetTodoAsync(Guid id);
   
}

