using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySQLSample.Models
{
    [Table("user")]
    public class User
    {
	[Key]
	public int    id       {get;set;}
	public string? username {get;set;}
	public byte[]? password {get;set;}
    public byte[]? key {get;set;}
	public string? tel      {get;set;}
	public string? email    {get;set;}
	public string? admin    {get;set;}
    }
    
    public class UserDto
    {
	//[Key]
	public string? username {get;set;}
	public string? password {get;set;}
	public string? tel      {get;set;}
	public string? email    {get;set;}
	public string? admin    {get;set;}
    }
    public class LoginDto
    {
        public string? username {get;set;}
        public string? password {get;set;}
    }

    public class  AlterDto
    {
        public int id {get;set;}
        public string? tel {get;set;}
        public string? email {get;set;}
    }
}
