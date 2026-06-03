using Microsoft.EntityFrameworkCore;
using System.Configuration; // ConfigurationManagerを使うために必要

namespace TodoList
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        // ここにテーブルの定義（DbSet）を書く
        public DbSet<TodoEntity> TodoListNoUserId { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
 }