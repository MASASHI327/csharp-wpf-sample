using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
   
    public class TodolistRepository
    {
        private readonly DbContext _context;

        public TodolistRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoEntity>> GetTodoListAsync()
        {
            return await _context.TodoListNoUserId.ToListAsync();
        }

        public async Task<TodoEntity?> GetTodoByTodoIdAsync(int todoId)
        {
            return await _context.TodoListNoUserId.FirstOrDefaultAsync(t => t.TODO_ID == todoId);
        }
        public async Task<bool> AddTodoAsync(TodoEntity newTodo)
        {
            bool result = false;
            _context.TodoListNoUserId.Add(newTodo);
            int savedResult = await _context.SaveChangesAsync();
            if (savedResult > 0)
            {
                result = true;
            }
            return result;
        }
        public async Task<bool> DeleteTodoAsync(int todoId)
        {
            bool result = false;
            TodoEntity? deleteTodo = await _context.TodoListNoUserId.FirstOrDefaultAsync(t => t.TODO_ID == todoId);
            if (deleteTodo != null)
            {
                _context.TodoListNoUserId.Remove(deleteTodo);
                int deletedResult = await _context.SaveChangesAsync();
                if (deletedResult > 0)
                {
                    result = true;
                }
            }
            else
            {
                throw new Exception("データが取得できない");
            }
            return result;
        }

        public async Task<bool> UpdateTodoAsync(TodoEntity updateTodo)
        {
            bool result = false;
            TodoEntity? preUpdateTodo = _context.TodoListNoUserId.FirstOrDefault
                                  　　　　 (t => t.TODO_ID == updateTodo.TODO_ID);

            if (preUpdateTodo != null)
            {
                // TODOリストの内容を更新
                preUpdateTodo.TITLE = updateTodo.TITLE;
                preUpdateTodo.CONTENT = updateTodo.CONTENT;
                preUpdateTodo.UPDATED_DATE = DateTime.Now;
            }
            else
            {
                throw new Exception("データが取得できない");
            }
            int savedResult = await _context.SaveChangesAsync();
            if (savedResult > 0)
            {
                result = true;
            }
            return result;
        }
    }
}
