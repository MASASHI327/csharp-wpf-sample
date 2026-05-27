using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList
{
    public class TodoListService
    {
        private readonly TodolistRepository _repository;

        public TodoListService(TodolistRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TodoDto>> GetTodoListAsync()
        {
            List<TodoEntity> todoEntity = await _repository.GetTodoListAsync();
            List<TodoDto> todoListDto = todoEntity.Select(todoEntity => new TodoDto
            {
                TODO_ID = todoEntity.TODO_ID,
                TITLE = todoEntity.TITLE ?? "",
                CONTENT = todoEntity.CONTENT ?? ""
            }).ToList();
            return todoListDto;
        }

        public async Task<TodoDto> GetTodoByTodoIdAsync(int todoId)
        {
            TodoEntity? todoEntity = await _repository.GetTodoByTodoIdAsync(todoId);
            var todoDto = new TodoDto
            {
                TODO_ID = todoEntity?.TODO_ID ?? 0,
                TITLE = todoEntity?.TITLE ?? "",
                CONTENT = todoEntity?.CONTENT ?? ""
            };
            return todoDto;

        }

        public async Task CreateTodoAsync(TodoDto todoDto)
        {
            bool result = false;
            TodoEntity newTodo = new TodoEntity();
            newTodo.TITLE = todoDto.TITLE;
            newTodo.CONTENT = todoDto.CONTENT;
            newTodo.CREATED_DATE = DateTime.Now;
            result = await _repository.AddTodoAsync(newTodo);
            if (result == false)
            {
                throw new Exception("レコードが保存できない");
            }
        }
        public async Task<bool> DeleteTodoAsync(int todolistId)
        {
            return await _repository.DeleteTodoAsync(todolistId);
        }

        public async Task UpdateTodoAsync(TodoDto todoDto)
        {
            bool result = false;

            TodoEntity updateTodo = new TodoEntity();
            updateTodo.TODO_ID = todoDto.TODO_ID;
            updateTodo.TITLE = todoDto.TITLE;
            updateTodo.CONTENT = todoDto.CONTENT;
            updateTodo.CREATED_DATE = DateTime.Now;
            result = await _repository.UpdateTodoAsync(updateTodo);

            if (result == false)
            {
                throw new Exception("レコードが保存できない");
            }
        }
    }
}
