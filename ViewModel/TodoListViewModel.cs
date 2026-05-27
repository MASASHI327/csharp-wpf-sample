using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;


namespace TodoList.ViewModel
{
    public class TodoListViewModel : ViewModelBase
    {
        private readonly TodoListService _todoListService;
        // Viewが監視するプロパティ
        public ObservableCollection<TodoDto> TodoItems { get; }
      
        public ICollectionView TodoView { get; set; }

        //検索用のテキストボックス
        private string? _searchText;
        public string? SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                // 文字が変わるたびにフィルターを再実行する
                TodoView.Refresh();
            }
        }

        public TodoListViewModel(TodoListService todoListService)
        {
            _todoListService = todoListService;
            TodoItems = new ObservableCollection<TodoDto>();
            TodoView = CollectionViewSource.GetDefaultView(TodoItems);

            // フィルターの条件を設定
            TodoView.Filter = (obj) =>
            {
                if (string.IsNullOrEmpty(SearchText) == false)
                {
                    var seachTodoDto = obj as TodoDto;
                    if (seachTodoDto != null)
                    {
                        return seachTodoDto.TITLE.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase);
                    }
                }
                return true;
            };
        }

        //画面表示
        public async Task LoadData()
        {
            List<TodoDto> todolistDto = await _todoListService.GetTodoListAsync();

            // コレクションを更新
            TodoItems.Clear();
            foreach (var item in todolistDto)
            {
                TodoItems.Add(item);
            }
        }

        //削除ボタン押下
        public ICommand DeliteCommand => new RelayCommand<TodoDto>(async (deleteTodo) =>
        {
            if(deleteTodo != null)
            {
                MessageBoxResult result = MessageBox.Show($"{deleteTodo.TITLE} を削除してもよろしいですか？",
                                                            "削除の確認",
                                                             MessageBoxButton.YesNo,
                                                             MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    bool rezult = await _todoListService.DeleteTodoAsync(deleteTodo.TODO_ID);
                    if (rezult == true)
                    {
                        await LoadData();
                    }
                }
            }
        });
    }
}
