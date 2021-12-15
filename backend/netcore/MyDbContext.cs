using Microsoft.EntityFrameworkCore;
using MySQLSample.Models;

namespace MySQLSample
{
    public class MyWork : DbContext
    {
        public DbSet<User>? User { get; set; }
        public DbSet<Book>? Book {get;set;}
        public DbSet<Booktype>? Booktype {get;set;}
        public DbSet<Bookitr>? Bookitr {get;set;}
        public DbSet<Booknum>? Booknum {get;set;}
        public DbSet<Cart>? Cart {get;set;}
        public DbSet<Order>? Order {get;set;}
        public DbSet<Ordernum>? Ordernum {get;set;}
        public MyWork(DbContextOptions<MyWork> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Booknum>(entity =>
            {
                entity.HasNoKey();//忽略数据库映射
            });
            modelBuilder.Entity<Ordernum>(entity =>
            {
                entity.HasNoKey();//忽略数据库映射
            });
        }
    }


}
