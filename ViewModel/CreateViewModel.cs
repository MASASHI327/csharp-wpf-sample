using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    public class CreateViewModel : ViewModelBase
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        private readonly Action? _onClose;

        private readonly TodoListService? _todoListService;

        public CreateViewModel(TodoListService todoListService, Action? onClose)
        {
            _todoListService = todoListService;
            _onClose = onClose;
        }

        //Todo作成
        public ICommand CreateCommand => new RelayCommand(async () =>
        {
            if(Title != null && Content != null && _todoListService != null)
            {
                var todoDto = new TodoDto
                {
                    CONTENT = Content,
                    TITLE = Title
                };
                await _todoListService.CreateTodoAsync(todoDto);
                _onClose?.Invoke();
            }
        });
    }
}
