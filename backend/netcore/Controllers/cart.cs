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
    public class CartController: ControllerBase
		{
				private readonly MyWork _context;
				public CartController(MyWork context)
				{
            _context = context;
				}

				[HttpGet]
				public async Task <ActionResult<Cart>> Find (int id)
		    { 
				 var list = _context.Cart.FromSqlRaw("select * from cart where id={0}",id).ToList();
				 return Ok(list);
				}
        [HttpGet]
				public async Task <ActionResult<Cart>> Add (int id)
        {
           var n = new Cart {
              id = id,
							cart = "[]"
						}; 
						_context.Cart.Add(n);
						_context.SaveChanges();
						return Ok("add cart");
				}
				public async Task <ActionResult<Cart>> Alter (Cart itm)
			 {
				 var entity = _context.Cart.FirstOrDefault(item => item.id == itm.id);
				 
				 if (entity != null) {
						 entity.cart = itm.cart;
						 _context.SaveChanges();
						 return  Ok("alter succeed");
				 }
			   return BadRequest("cant find");
			 }
		}
}