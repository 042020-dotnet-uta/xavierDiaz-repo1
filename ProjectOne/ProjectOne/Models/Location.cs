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
	interface VerifyID
	{
		bool IsValidLocation(int i);
	}
	//Ideally this class would have things like address, hours, manager ID
	//But for now it basically only holds and adjusts inventory
	public class Location : VerifyID
	{
		private int _InventoryID;// --- key
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int InventoryID
		{
			get
			{
				return _InventoryID;
			}
			set
			{
				_InventoryID = value;
			}
		}
		private int _StoreID;// --- key
		[Required]
		public int StoreID
		{
			get
			{
				return _StoreID;
			}
			set
			{
				_StoreID = value;
			}
		}
		private int _ItemID;// --- key
		[Required]
		public int ItemID
		{
			get
			{
				return _ItemID;
			}
			set
			{
				_ItemID = value;
			}
		}
		private int _Quantity;// --- key
		[Required]
		public int Quantity
		{
			get
			{
				return _Quantity;
			}
			set
			{
				_Quantity = value;
			}
		}
		public Location()
		{
		}

		public bool IsValidLocation(int i)
		{
			using (var db = new ProjectOneContext())
			{
				var locs = db.Locations
					.FromSqlInterpolated($"SELECT * FROM Locations WHERE StoreID = {i}")
					.ToList();
				if (locs.Count != 0)
					return true;
				else
					return false;
			}
		}
	}
}
