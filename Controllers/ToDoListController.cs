using list_service.Models;
using list_service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace list_service.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("lists")]
public class ToDoListController : ControllerBase
{
    private readonly IToDoListService _toDoListService;
    
    public ToDoListController(IToDoListService toDoListService)
    {
        _toDoListService = toDoListService;
    }
    
    [HttpGet("{id}")]
    public IList<ToDoList> GetByBoard(int id)
    {
        return _toDoListService.GetByBoard(id);
    }
    
    [HttpPost]
    public void Post([FromBody] ToDoList toDoList)
    {
        _toDoListService.Add(toDoList);
    }
    
    [HttpPut]
    public void Put([FromBody] ToDoList toDoList)
    {
        _toDoListService.Update(toDoList);
    }
    
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _toDoListService.Delete(id);
    }
}