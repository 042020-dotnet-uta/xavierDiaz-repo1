using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
	}
}