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
        
				[HttpPost]
				public async Task <ActionResult<Book>> add (Book item) {
          _context.Book.Add(item);
		      await _context.SaveChangesAsync();
		      return Ok(item.bookid);

				}

        [HttpPost]
				public async Task <ActionResult<Book>> Alter (Book book)
			 {
				 var entity = _context.Book.FirstOrDefault(item => item.bookid == book.bookid);
				 if (entity != null) {
             entity.name = book.name;
						 entity.isdn = book.isdn;
						 entity.picture = book.picture;
						 entity.press   = book.press;
						 entity.price   = book.price;
						 entity.sum = book.sum;
						 entity.type = book.sum;
						 _context.SaveChanges();
						 return  Ok("alter succeed");
				 }
			   return BadRequest("cant find");
			 }
       
			 [HttpGet]
			 public async Task <ActionResult<Book>> Delete (int id)
			 {
				 var entity = _context.Book.FirstOrDefault(item => item.bookid == id);
				 if (entity != null) {
				  _context.Remove(entity);
				  _context.SaveChanges();
         return Ok("delete succeed");
				  }
				 return BadRequest("cant find");
			 }
       
			 // 类型的CRUD
			 [HttpGet]
				public async Task <ActionResult<Booktype>> alltype () {
             var list = _context.Booktype.FromSqlRaw("select * from booktype").ToList();
			       return Ok(list);
				}
        [HttpPost]
				public async Task <ActionResult<Booktype>> addtype (Booktype item) {
          _context.Booktype.Add(item);
		      await _context.SaveChangesAsync();
		      return item; 
				}
				
        [HttpPost]
				public async Task <ActionResult<Booktype>> Altertype (Booktype book)
			 {
				 var entity = _context.Booktype.FirstOrDefault(item => item.typeid == book.typeid);
				 if (entity != null) {
             entity.type = book.type;
						 _context.SaveChanges();
						 return  Ok("alter succeed");
				 }
			   return BadRequest("cant find");
			 }

       [HttpGet]
			 public async Task <ActionResult<Booktype>> Deletetype (int id)
			 {
				 var entity = _context.Booktype.FirstOrDefault(item => item.typeid == id);
				 if (entity != null) {
				  _context.Remove(entity);
				  _context.SaveChanges();
         return Ok("delete succeed");
				  }
				 return BadRequest("cant find");
			 }

			 // 简介的CRUD

        [HttpPost]
				public async Task <ActionResult<Bookitr>> additr (Bookitr item) {
          _context.Bookitr.Add(item);
		      await _context.SaveChangesAsync();
		      return item; 
				}
				
        [HttpPost]
				public async Task <ActionResult<Bookitr>> Alteritr (Bookitr book)
			 {
				 var entity = _context.Bookitr.FirstOrDefault(item => item.bookid == book.bookid);
				 if (entity != null) {
             entity.introduce = book.introduce;
						 _context.SaveChanges();
						 return  Ok("alter succeed");
				 }
			   return BadRequest("cant find");
			 }
       [HttpGet]
			 public async Task <ActionResult<Bookitr>> Deleteitr (int id)
			 {
				 var entity = _context.Bookitr.FirstOrDefault(item => item.bookid == id);
				 if (entity != null) {
				  _context.Remove(entity);
				  _context.SaveChanges();
         return Ok("delete succeed");
				  }
				 return BadRequest("cant find");
			 }
		}
}