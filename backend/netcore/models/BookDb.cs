using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySQLSample.Models 
{
	[Table("books")]
	public class  Book
	{
			[Key]
			public int bookid {get;set;}
			public string? name {get;set;}
      public string? autor    {get;set;}
			public string?  type   {get;set;}
			public string? press    {get;set;}
			public string? isdn {get;set;}
			public string? price {get;set;}
			public string? sum   {get;set;}
			public string? picture {get;set;}

	}
	[Table("booktype")]

	public class Booktype
	{
			[Key]
			public int typeid {get;set;}
			public string? type {get;set;}
	}

	[Table("bookintroduce")]

	public class Bookitr
	{
			[Key]
			public int bookid {get;set;}
			public string? introduce {get;set;}
	}	

	public class Booknum
	{
		public int num {get;set;}
	} 
}