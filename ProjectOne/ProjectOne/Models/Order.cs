﻿using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;

namespace ProjectOne.Models
{
    public class Order
    {
		private int _ID;// --- order key
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID
		{
			get
			{
				return _ID;
			}
			set
			{
				_ID = value;
			}
		}
		private int _StoreID;// --- store record
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
		private int _CustomerID;// --- customer record
		[Required]
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
		private DateTime _SellTime;// --- time order was made
		[Required]
		public DateTime SellTime
		{
			get
			{
				return _SellTime;
			}
			set
			{
				_SellTime = value;
			}
		}
		private string _SoldItems;// --- string rep of what they bought
		[Required]
		public string SoldItems
		{
			get
			{
				return _SoldItems;
			}
			set
			{
				_SoldItems = value;
			}
		}
		public Order()
		{
		}
	}
}