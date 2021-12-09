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
}