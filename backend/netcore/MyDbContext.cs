using Microsoft.EntityFrameworkCore;
using MySQLSample.Models;

namespace MySQLSample
{
    public class MyWork : DbContext
    {
        public DbSet<User>? User { get; set; }
        public DbSet<Book>? Book {get;set;}
        public DbSet<Booktype>? Booktype {get;set;}
        public MyWork(DbContextOptions<MyWork> options) : base(options)
        {

        }
    }


}
