using System.Windows.Input;
using GalaSoft.MvvmLight.Command;


namespace TodoList.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly TodoListService _service;
        public ICommand? LoadCommand { get; }

        private ViewModelBase? _currentView;
        public ViewModelBase? CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }
        public MainViewModel(TodoListService service)
        {
            _service = service;
            CurrentView = new TodoListViewModel(_service);
        }

        //一覧画面表示
        public ICommand ShowListCommand => new RelayCommand(() =>
        {
            if (CurrentView is TodoListViewModel)
            {
                return;
            }
            CurrentView = new TodoListViewModel(_service);
        });

        //詳細場面表示
        public ICommand ShowDetailCommand => new RelayCommand<TodoDto>(selectedItem =>
        {
            CurrentView = new DetailViewModel(selectedItem);
        });

        //作成画面表示
        public ICommand ShowCreateCommand => new RelayCommand(() =>
        {
            CurrentView = new CreateViewModel(_service, () =>
            {
                CurrentView = new TodoListViewModel(_service);
            });
        });

        //更新画面表示
        public ICommand ShowEditCommand => new RelayCommand<TodoDto>(selectedTodo =>
        {
            CurrentView = new EditViewModel(_service, selectedTodo, () =>
            {
                CurrentView = new TodoListViewModel(_service);
            });
        });
    }
}
