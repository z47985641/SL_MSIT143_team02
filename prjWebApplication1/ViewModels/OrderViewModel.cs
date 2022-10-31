using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PJ_MSIT143_team02.Models;
using static PJ_MSIT143_team02.Models.Order;

namespace PJ_MSIT143_team02.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<CartItem> CartItems { get; set; }

    }
}
