using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectOne.Data;
using Microsoft.EntityFrameworkCore;

namespace ProjectOne.Models
{
	public class Product
	{
		private int _ProductID;// --- key
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductID
		{
			get
			{
				return _ProductID;
			}
			set
			{
				_ProductID = value;
			}
		}
		private string _PName;// --- Product name
		[Required]
		public string PName
		{
			get
			{
				return _PName;
			}
			set
			{
				_PName = value;
			}
		}
		private float _PCost;// --- unit cost
		[Required]
		public float PCost
		{
			get
			{
				return _PCost;
			}
			set
			{
				_PCost = value;
			}
		}
		public Product()
		{
		}

		public bool isValidProduct(int ID)
		{
			using (var db = new ProjectOneContext())
			{
				var prods = db.Products
					.FromSqlInterpolated($"SELECT * FROM Products WHERE ProductID = {ID}")
					.ToList();
				if (prods.Count != 0)
					return true;
				else
					return false;
			}
		}
	}
}
