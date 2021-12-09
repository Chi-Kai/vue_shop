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
    public class UserController: ControllerBase
    {
	     private readonly MyWork _context;
	     public  UserController(MyWork context)
	     {
		   _context = context;
	     }

	     [HttpPost]
	     public async Task<ActionResult<User>> Register(UserDto user)
	     {
				  if (await UserExists(user.username)) return BadRequest("UserName Is Already Taken");
          var hmac = new HMACSHA512();
					System.Console.WriteLine(hmac.Key);
					var item = new User {
					        username = user.username,
									password = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.password)),
		              key = hmac.Key, // 得到的值大于100 数据库要注意
									tel = user.tel,
									email = user.email,
									admin = user.admin
					};
					_context.User.Add(item);
		      await _context.SaveChangesAsync();
		      return item; 
	     }
	     [HttpPost]
	     public async Task <ActionResult<User>> Login(LoginDto loginDto)
        {
          var user = await _context.User
              .SingleOrDefaultAsync(x => x.username == loginDto.username);

          if (user == null) return Unauthorized("Invalid UserName");

          var hmac = new HMACSHA512(user.key);

          var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

          for(int i=0;i<computedHash.Length;i++)
            {
                if (computedHash[i] != user.password[i]) return Unauthorized("Invalid Password");
            }

          return Ok(user);
        }
        
			[HttpGet]
			 public async Task <ActionResult<User>> List (string s,string p)
			 { 
				 int size = int.Parse(s);
				 int page = int.Parse(p);
         page  = (page-1) * size; 
				 //System.Console.WriteLine(page);
				 //System.Console.WriteLine(size);
				 
				 var list = _context.User.FromSqlRaw("select * from user limit {0},{1}",page,size).ToList();
			   return Ok(list);
			 }
       
			 [HttpPost]
			 public async Task <ActionResult<User>> Alter (AlterDto user)
			 {
				 /*
				 System.Console.WriteLine(user.id);
				 System.Console.WriteLine(user.email);
				 System.Console.WriteLine(user.tel);
				 */
				 var entity = _context.User.FirstOrDefault(item => item.id == user.id);
				 if (entity != null) {
					   entity.tel = user.tel;
						 entity.email = user.email;
						 _context.SaveChanges();
						 return  Ok("alter succeed");
				 }
         // _context.User.FromSqlRaw($"update `user` set tel='{0}',email='{1}' where id= {3}",user.tel,user.email,user.id).ToList(); 失败，必须返回结果
			   return BadRequest("cant find");
			 }
       
			 [HttpPost]
			 public async Task <ActionResult<User>> Delete (AlterDto user)
			 {
				 var entity = _context.User.FirstOrDefault(item => item.id == user.id);
				 if (entity != null) {
				  _context.Remove(entity);
				  _context.SaveChanges();
         //_context.User.FromSqlRaw("delete from user  where id={0}",id);
         return Ok("delete succeed");
				  }
				 return BadRequest("cant find");
			 }
       
			 [HttpPost]
			 public async Task <ActionResult<User>> Alteradmin (AlterDto user)
			 {
			 
				 var entity = _context.User.FirstOrDefault(item => item.id == user.id);
				 if (entity != null) {
					   if (entity.admin == "true") entity.admin = "false";
						 else entity.admin = "true";
						 _context.SaveChanges();
						 return  Ok("alter succeed");
				 }
         // _context.User.FromSqlRaw($"update `user` set tel='{0}',email='{1}' where id= {3}",user.tel,user.email,user.id).ToList(); 失败，必须返回结果
			   return BadRequest("cant find");
			 }
	     
			 [HttpGet]
			 public async Task <ActionResult<User>> Find (string username)
			 {
				 var user = _context.User.FromSqlRaw("select * from user where username = {0}",username).ToList(); //这是直接替换，不用加单引号，插入加单引号是识别为字符串
				 return Ok(user);
			 }
			 private async Task<bool> UserExists(string username)
       {
          return await _context.User.AnyAsync(u => u.username == username);
       }

	
  }

}
