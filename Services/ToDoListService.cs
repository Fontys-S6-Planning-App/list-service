using list_service.Messaging;
using list_service.Models;
using list_service.Repositories.Interfaces;
using list_service.Services.Interfaces;

namespace list_service.Services;

public class ToDoListService : IToDoListService
{
    private readonly IToDoListRepository _toDoListRepository;
    private readonly Send _messageSender = new Send();
    
    public ToDoListService(IToDoListRepository toDoListRepository)
    {
        _toDoListRepository = toDoListRepository;
    }
    
    public List<ToDoList> GetByBoard(int boardId)
    {
        return _toDoListRepository.GetByBoard(boardId);
    }

    public void Add(ToDoList toDoList)
    {
        _toDoListRepository.Add(toDoList);
    }

    public void Update(ToDoList toDoList)
    {
        _toDoListRepository.Update(toDoList);
    }

    public void Delete(int id)
    {
        _toDoListRepository.Delete(id);
        _messageSender.SendMessage(id.ToString());
    }
}