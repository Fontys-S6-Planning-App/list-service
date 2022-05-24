using list_service.DBContexts;
using list_service.Models;
using list_service.Repositories.Interfaces;

namespace list_service.Repositories;

public class ToDoListRepository: IToDoListRepository
{
    private readonly ToDoListContext _context;

    public ToDoListRepository(ToDoListContext context)
    {
        _context = context;
    }

    public List<ToDoList> GetByBoard(int boardId)
    {
        return _context.ToDoLists.Where(x => x.BoardId == boardId).ToList();
    }

    public void Add(ToDoList toDoList)
    {
        _context.ToDoLists.Add(toDoList);
        _context.SaveChanges();
    }

    public void Update(ToDoList toDoList)
    {
        _context.ToDoLists.Update(toDoList);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var toDoList = _context.ToDoLists.Find(id);
        if(toDoList == null)
        {
            throw new Exception("ToDoList not found");
        }
        _context.ToDoLists.Remove(toDoList);
        _context.SaveChanges();
    }
}