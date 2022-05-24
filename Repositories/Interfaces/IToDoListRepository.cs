using list_service.Models;

namespace list_service.Repositories.Interfaces;

public interface IToDoListRepository
{
    List<ToDoList> GetByBoard(int boardId);
    void Add(ToDoList toDoList);
    void Update(ToDoList toDoList);
    void Delete(int id);
}