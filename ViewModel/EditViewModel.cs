using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace WpfApp1.ViewModel
{
    public class EditViewModel : ViewModelBase
    {
        public TodoDto _todoDto { get; private set; }

        private readonly TodoListService _todoListService;

        private readonly Action? _onClose;
        public EditViewModel(TodoListService todoListService, TodoDto selectedTodo, Action? onClose)
        {
            _todoListService = todoListService;
            _todoDto = selectedTodo;
            _onClose = onClose;
        }

        //Todo更新
        public ICommand UpdateCommand => new RelayCommand(async () =>
        {
            await _todoListService.UpdateTodoAsync(_todoDto);
            _onClose?.Invoke();
        });
    }
}
