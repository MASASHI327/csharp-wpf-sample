using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// プロパティの値を更新し、変更通知を送る
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="field">バッキングフィールド（_プロパティ名）</param>
        /// <param name="value">新しい値</param>
        /// <param name="propertyName">プロパティ名（自動取得される）</param>
        /// <returns>値が更新されたかどうか</returns>
        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value)) return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// 変更通知イベントを発生させる
        /// </summary>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
