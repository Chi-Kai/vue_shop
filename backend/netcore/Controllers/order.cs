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
    public class OrderController: ControllerBase
		{
				private readonly MyWork _context;
				public OrderController(MyWork context)
				{
            _context = context;
				}

				[HttpGet]
				public async Task <ActionResult<Order>> List (string s,string p)
		    {
         int size = int.Parse(s);
				 int page = int.Parse(p);
         page  = (page-1) * size; 
				 
				 var list = _context.Order.FromSqlRaw("select * from orders limit {0},{1}",page,size).ToList();
			   return Ok(list);
				}
        [HttpPost] 
				public async Task <ActionResult<Order>> add (Order item) 
				{
           _context.Order.Add(item);
					 _context.SaveChanges();
					 return Ok("succeed");  
				}

			 [HttpGet]
			 public async Task <ActionResult<Order>> Find (string id)
			 {
				 var user = _context.Order.FromSqlRaw("select * from orders where orderid = {0}",id).ToList(); //这是直接替换，不用加单引号，插入加单引号是识别为字符串
				 return Ok(user);
			 }
       
			 [HttpGet]
			 public async Task <ActionResult<Booknum>> Num ()
			 {
				 var user = _context.Booknum.FromSqlRaw("select count(*) as num from orders").ToList(); //这是直接替换，不用加单引号，插入加单引号是识别为字符串
				 return Ok(user);
			 }
			 [HttpGet]
			 public async Task <ActionResult<Order>> Groupnum ()
			 {
				var query = from b in _context.Set<Order>()
                     group b by b.data
                     into g
                     select new { g.Key, Count = g.Count() };
				var res = query.ToList();	
				return Ok(res);
			 }
		}
}