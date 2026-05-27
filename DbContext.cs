using Microsoft.EntityFrameworkCore;
using System.Configuration; // ConfigurationManagerを使うために必要

namespace TodoList
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        // ここにテーブルの定義（DbSet）を書く
        public DbSet<TodoEntity> TodoListNoUserId { get; set; }

        // ★このタイミング（メソッド）をオーバーライドして記述します
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // App.configから接続文字列を取得
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                // SQL Serverを使う設定を注入
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
 }