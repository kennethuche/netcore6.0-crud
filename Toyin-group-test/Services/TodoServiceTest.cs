using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Toyin_group_api.Core.Data;
using Toyin_group_api.Core.Entities;
using Toyin_group_api.Core.Models;
using Toyin_group_api.Core.Models.Payload;
using Toyin_group_api.Core.Models.Resources;
using Toyin_group_api.Core.Services;
using Xunit;

namespace Toyin_group_test.Services
{
    public class TodoServiceTest : BaseTest
    {

        private static string _todoName = "any1todo";
    
        [Fact]
        public async Task Test_Get_All_Todo_Returns_Success()
        {
            using var collection = GetCollection().BuildServiceProvider();
            var dbContext = collection.GetService<AppDbContext>();
        

            var todoService = collection.GetService<ITodoService>();

            var result = await todoService.GetAllTodosAsync();

            Assert.Equal(ResourceCodes.Success, result.Code);
            Assert.NotNull(result.Message);
            Assert.IsType<string>(result.Message);

        }

        [Fact]
        public async Task Test_Create_Todo_Return_Success()
        {
            using var collection = GetCollection().BuildServiceProvider();
     

            var todoService = collection.GetService<ITodoService>();


            var result = await todoService.CreateTodoAsync(new CreateTodoPayload(_todoName));

            Assert.Equal(ResourceCodes.Success, result.Code);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Message);
            Assert.IsType<string>(result.Message);
            Assert.IsType<string>(result.Code);
           Assert.IsType<TodoResource>(result.Data);

        }

        [Fact]
        public async Task Test_Create_Todo_Return_Failure()
        {
            using var collection = GetCollection().BuildServiceProvider();
      
            var todoService = collection.GetService<ITodoService>();


            var result = await todoService.CreateTodoAsync(new CreateTodoPayload(null));

            Assert.Equal(ResourceCodes.NoData, result.Code);
            Assert.Null(result.Data);
            Assert.NotNull(result.Message);
        

        }


        [Fact]
        public async Task Test_Get_Todo_By_Id_Returns_Success()
        {
            using var collection = GetCollection().BuildServiceProvider();
         

            var todoService = collection.GetService<ITodoService>();

            var newData = await todoService.CreateTodoAsync(new CreateTodoPayload(_todoName));


            var result = await todoService.GetTodoAsync(newData.Data.Id);

            Assert.Equal(ResourceCodes.Success, result.Code);
            Assert.NotNull(result.Message);
            Assert.IsType<string>(result.Message);
            Assert.NotNull(result.Data);

        }


        [Fact]
        public async Task Test_Get_Todo_By_Id_Returns_Service_Error()
        {
            using var collection = GetCollection().BuildServiceProvider();


            var todoService = collection.GetService<ITodoService>();

            var result = await todoService.GetTodoAsync(Guid.Empty);

            Assert.Equal(ResourceCodes.ServiceError, result.Code);
            Assert.NotNull(result.Message);
            Assert.IsType<string>(result.Message);


        }

        [Fact]
        public async Task Test_Delete_Todo_Returns_Success()
        {
            using var collection = GetCollection().BuildServiceProvider();
            var dbContext = collection.GetService<AppDbContext>();
          

            var todoService = collection.GetService<ITodoService>();
            var newData = await todoService.CreateTodoAsync(new CreateTodoPayload(_todoName));

            var result = await todoService.DeleteTodoAsync(newData.Data.Id);

            Assert.Equal(ResourceCodes.Success, result.Code);
            Assert.NotNull(result.Message);
            Assert.IsType<string>(result.Message);


        }

        [Fact]
        public async Task Test_Update_Todo_Return_Service_Error()
        {
            using var collection = GetCollection().BuildServiceProvider();
            var todoService = collection.GetService<ITodoService>();
            var result = await todoService.UpdateTodoAsync(Guid.Empty, new UpdateTodoPayload("newtodo"));

            Assert.Equal(ResourceCodes.ServiceError, result.Code);
            Assert.NotNull(result.Message);
            Assert.IsType<string>(result.Message);
            Assert.IsType<string>(result.Code);

        }

        [Fact]
        public async Task Test_Update_Todo_Return_Succes()
        {
            using var collection = GetCollection().BuildServiceProvider();
            var dbContext = collection.GetService<AppDbContext>();
         

            var todoService = collection.GetService<ITodoService>();

            var newData = await todoService.CreateTodoAsync(new CreateTodoPayload(_todoName));

            var result = await todoService.UpdateTodoAsync(newData.Data.Id,new UpdateTodoPayload("newtodo"));

            Assert.Equal(ResourceCodes.Success, result.Code);
            Assert.NotNull(result.Message);
            Assert.IsType<string>(result.Message);
            Assert.IsType<string>(result.Code);
 


        }

        [Fact]
        public async Task Test_Delete_Todo_Returns_Service_Error()
        {
            using var collection = GetCollection().BuildServiceProvider();
        

            var todoService = collection.GetService<ITodoService>();

            var result = await todoService.DeleteTodoAsync(Guid.Empty);

            Assert.Equal(ResourceCodes.ServiceError, result.Code);
            Assert.NotNull(result.Message);
            Assert.IsType<string>(result.Message);


        }





      
    }
}
