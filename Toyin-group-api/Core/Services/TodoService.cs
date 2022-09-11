using Microsoft.EntityFrameworkCore;
using Toyin_group_api.Core.Data;
using Toyin_group_api.Core.Entities;
using Toyin_group_api.Core.Models;
using Toyin_group_api.Core.Models.Payload;
using Toyin_group_api.Core.Models.Resources;

namespace Toyin_group_api.Core.Services
{
    public class TodoService : ITodoService
    {


        private readonly AppDbContext dbContext;
        private readonly ILogger<TodoService> logger;

        public TodoService(AppDbContext dbContext, ILogger<TodoService> logger)
        {
        
            this.dbContext = dbContext;
            this.logger = logger;
         
        }

        public async Task<ObjectResource<TodoResource>> CreateTodoAsync(CreateTodoPayload payload)
        {
            var todo = await CreateTodoInDb(payload.TodoName);

            return (todo is not null) ? (new ObjectResource<TodoResource>(message: "todo Created Successfully")
            {
                Code = ResourceCodes.Success,
                Data = new TodoResource(todo.Id, todo.TodoName, todo.IsCompleted, todo.CreatedAt, todo.UpdatedAt),

            }) : (new ObjectResource<TodoResource>(code : ResourceCodes.NoData ,message: "Unable To Create Todo"));
           

        }

        public async Task<ObjectResource<string>> DeleteTodoAsync(Guid id)
        {
            var todo = await dbContext.Todos.FindAsync(id);
            if (todo is null) return new ObjectResource<string>(ResourceCodes.ServiceError, "Todo Not Found");

      
           var deletedData =  await DeleteTodoInDb(todo);

            return (deletedData is not null) ? (new ObjectResource<string>(message: "todo Deleted Successfully")
            {
                Code = ResourceCodes.Success,
                

            }) : (new ObjectResource<string>(code: ResourceCodes.ServiceError,message: "Unable To Delete Todo"));

        }

        public async Task<ListResource<TodoResource>> GetAllTodosAsync()
        {
          

            var query = await (from t in dbContext.Todos
                        orderby t.CreatedAt descending
                        select new TodoResource
                        (
                          t.Id,
                           t.TodoName, t.IsCompleted,
                          t.CreatedAt,
                          t.UpdatedAt
                         
                        )).ToListAsync();
            return new ListResource<TodoResource>
            {
                Message = "Todo's Retrived Succesfully",
                Data = query,
                Total= query.Count(),
                Code = ResourceCodes.Success
            };

  
        }

        public async Task<ObjectResource<TodoResource>> GetTodoAsync(Guid id)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            return (todo is not null) ? (new ObjectResource<TodoResource>(message: "todo Reteived Successfully")
            {
                Code = ResourceCodes.Success,
                Data = new TodoResource(todo.Id, todo.TodoName, todo.IsCompleted, todo.CreatedAt, todo.UpdatedAt),

            }) : (new ObjectResource<TodoResource>(code : ResourceCodes.ServiceError,message: "Todo not Found"));
        }

        public async Task<StatusResource> UpdateTodoAsync(Guid id, UpdateTodoPayload payload)
        {
            var todo = await dbContext.Todos.FindAsync(id);
            if (todo is null) return new ObjectResource<string>(ResourceCodes.ServiceError, "Todo Not Found");

            return (await UpdateTodoInDb(todo, payload)) ? (new ObjectResource<StatusResource>(message: "Updated Successfully")
            {
                Code = ResourceCodes.Success,
               

            }) : (new ObjectResource<StatusResource>(message: "Update Failed in Db")
            {
                Code = ResourceCodes.ServiceError
            });


        }

        private async Task<Todo?> CreateTodoInDb(string TodoName)
        {
            var todo = new Todo
            {
               Id = Guid.NewGuid(),
               TodoName= TodoName,
               CreatedAt= DateTime.UtcNow,
            };

            dbContext.Todos.Add(todo);

            return (await dbContext.TrySaveChangesAsync(logger) is DatabaseResponseCodes.Success) ? todo : null;
        }

        private async Task<Todo?> DeleteTodoInDb(Todo todo)
        {
            dbContext.Todos.Remove(todo);
            return await dbContext.TrySaveChangesAsync(logger) == DatabaseResponseCodes.Success ? todo : null;
        }


        private async Task<bool> UpdateTodoInDb(Todo todo, UpdateTodoPayload payload)
        {
            todo.TodoName = payload.TodoName;

            dbContext.Entry(todo).State = EntityState.Modified;

            return await dbContext.TrySaveChangesAsync(logger) == DatabaseResponseCodes.Success ? true : false;

        }
    }
}
