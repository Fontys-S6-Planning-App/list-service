using list_service.Models;

namespace list_service.Services.Interfaces;

public interface IToDoListService
{
    List<ToDoList> GetByBoard(int boardId);
    void Add(ToDoList toDoList);
    void Update(ToDoList toDoList);
    void Delete(int id);
}