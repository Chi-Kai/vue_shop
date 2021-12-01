using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySQLSample.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore; //一些查询方法的扩展

namespace MySQLSample.Controllers
{
	  [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookController: ControllerBase
		{
				private readonly MyWork _context;
				public BookController(MyWork context)
				{
            _context = context;
				}

				[HttpGet]
				public async Task <ActionResult<Book>> List (string s,string p)
		    {
         int size = int.Parse(s);
				 int page = int.Parse(p);
         page  = (page-1) * size; 
				 
				 var list = _context.Book.FromSqlRaw("select * from books limit {0},{1}",page,size).ToList();
			   return Ok(list);
				}

				[HttpGet]
				public async Task <ActionResult<Booktype>> alltype () {
             var list = _context.Booktype.FromSqlRaw("select * from booktype").ToList();
			       return Ok(list);
				}

				public async Task <ActionResult<Book>> add (Book item) {
          _context.Book.Add(item);
		      await _context.SaveChangesAsync();
		      return item; 
				}

				public async Task <ActionResult<Book>> Alter (Book book)
			 {
				 var entity = _context.Book.FirstOrDefault(item => item.bookid == book.bookid);
				 if (entity != null) {
             entity = book;
						 _context.SaveChanges();
						 return  Ok("alter succeed");
				 }
			   return BadRequest("cant find");
			 }
       
			 [HttpPost]
			 public async Task <ActionResult<Book>> Delete (Book book)
			 {
				 var entity = _context.Book.FirstOrDefault(item => item.bookid == book.bookid);
				 if (entity != null) {
				  _context.Remove(entity);
				  _context.SaveChanges();
         return Ok("delete succeed");
				  }
				 return BadRequest("cant find");
			 }
		}
}