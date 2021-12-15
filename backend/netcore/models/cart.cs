using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySQLSample.Models 
{
	[Table("cart")]

	public class Cart
	{
			[Key]
			public int id {get;set;}
			public string? cart {get;set;}
	}
	[Table("orders")]
	public class Order 
	{
		public int orderid {get;set;}
		public int id   {get;set;}
		public string? data {get;set;}
		public string? addr{get;set;}
		public int sum {get;set;}
		public string? content {get;set;}
		public string? name {get;set;}
		public string? tel {get;set;}
		public string? code {get;set;}
		
	}
	public class Ordernum 
	{
		public string? date {get;set;}
		public int num {get;set;}
	}
}