
namespace WpfApp1.ViewModel
{
    public class DetailViewModel : ViewModelBase
    {
        public TodoDto todoDto { get; set; }
        public DetailViewModel(TodoDto selectedTodo)
        {
            this.todoDto = selectedTodo;
        }
    }
}
