﻿using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class Order
    {
        public Order()
        {
            ActivityReferences = new HashSet<ActivityReference>();
            AddReferences = new HashSet<AddReference>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public string OrderRemark { get; set; }
        public int MemberId { get; set; }
        public int OrderstatusId { get; set; }
        public int? RoomId { get; set; }
        public int? ActivityId { get; set; }

        public virtual Member Member { get; set; }
        public virtual OrderStatus Orderstatus { get; set; }
        public virtual ICollection<ActivityReference> ActivityReferences { get; set; }
        public virtual ICollection<AddReference> AddReferences { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public List<OrderItem> OrderItem { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int RoomId { get; set; }
        public int Amount { get; set; }
        public int SubTotal { get; set; }
    }
    public class CartItem : OrderItem
    {
        public CartItem() { }
        public CartItem(OrderItem order)
        {
            this.OrderId = order.OrderId;
            this.RoomId = order.RoomId;
            this.Amount = order.Amount;
            this.SubTotal = order.SubTotal;
        }

        public Room room { get; set; }
    }
}







