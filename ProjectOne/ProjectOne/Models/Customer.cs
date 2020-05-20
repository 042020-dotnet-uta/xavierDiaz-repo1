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
	public class Customer
	{
		private int _CustomerID;// --- key
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CustomerID
		{
			get
			{
				return _CustomerID;
			}
			set
			{
				_CustomerID = value;
			}
		}
		private string _FName;// --- First name
		[Required]
		[Display(Name ="First Name")]
		public string FName
		{
			get
			{
				return _FName;
			}
			set
			{
				_FName = value;
			}

		}
		private string _LName;// --- Last name
		[Required]
		[Display(Name ="Last Name")]
		public string LName
		{
			get
			{
				return _LName;
			}
			set
			{
				_LName = value;
			}

		}

		private int _DefaultSto;// --- their default store to get loaded into
		[Required]
		[Display(Name ="Default Store")]
		public int DefaultSto
		{
			get
			{
				return _DefaultSto;
			}
			set
			{
				_DefaultSto = value;
			}

		}
		public Customer()
		{
			//nothing
		}
		/// <summary>
		/// returns bool is customer ID valid
		/// </summary>
		public bool IsValidCustomer(int ID)
		{
			using (var db = new ProjectOneContext())
			{
				var custs = db.Customers
					.FromSqlInterpolated($"SELECT * FROM Customers WHERE CustomerID = {ID}")
					.ToList();
				if (custs.Count != 0)
					return true;
				else
					return false;
			}
		}
	}
}