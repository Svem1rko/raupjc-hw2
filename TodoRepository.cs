using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDo
{
    /// <summary >
    /// Class that encapsulates all the logic for accessing TodoTtems .
    /// </summary >
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </summary >
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;

        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            _inMemoryTodoDatabase = initialDbState ?? new GenericList<TodoItem>();
        }

        public TodoItem Get(Guid todoId)
        {
            var temp = _inMemoryTodoDatabase.FirstOrDefault(s => s.Id == todoId);
            int pom = _inMemoryTodoDatabase.IndexOf(temp);
            if (pom >= 0) return _inMemoryTodoDatabase.GetElement(pom);
            else return null;
        }

        public TodoItem Add(TodoItem todoItem)
        {
            if (_inMemoryTodoDatabase.Contains(todoItem))
            {
                throw new DuplicateWaitObjectException($"duplicated id: {todoItem.Id}");
            }
            else
            {
                _inMemoryTodoDatabase.Add(todoItem);
                return todoItem;
            }
        }

        public bool Remove(Guid todoId)
        {
            var temp = _inMemoryTodoDatabase.Where(s => s.Id == todoId).ToArray();
            return _inMemoryTodoDatabase.Remove(temp[0]);

        }

        public TodoItem Update(TodoItem todoItem)
        {
            if (_inMemoryTodoDatabase.Contains(todoItem))
            {
                _inMemoryTodoDatabase.Where(i => i.Id == todoItem.Id).Select(i => i = todoItem);
            }
            else
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }

            return todoItem;
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            try
            {
                var result = _inMemoryTodoDatabase.Where(s => s.Id == todoId).Select(i => i.MarkAsCompleted())
                    .ToArray();
                return result[0];
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public  List<TodoItem> GetAll()
        {
            return _inMemoryTodoDatabase.OrderByDescending(s => s.DateCreated).ToList();
        }

        public List<TodoItem> GetActive()
        {
            return _inMemoryTodoDatabase.Where(s => s.IsCompleted == false).ToList();
        }

        public List<TodoItem> GetCompleted()
        {
            return _inMemoryTodoDatabase.Where(s => s.IsCompleted).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            return _inMemoryTodoDatabase.Where(filterFunction).ToList();
        }

        }

}
