namespace Toyin_group_api.Core.Models.Payload;

 
public record CreateTodoPayload(
     string TodoName
    );


public record UpdateTodoPayload(
     string TodoName
    );