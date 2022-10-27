using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using System.Threading.Tasks;

using System.Linq;

namespace PJ_MSIT143_team02.ViewComponents
{
    public class DiscountViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = (from d in (new MingSuContext()).Discounts
                        select new Discount {
                            RoomDiscountId = d.RoomDiscountId,
                            DiscountName = d.DiscountName
                        }).Take(3).ToList();
            return View(data);
        }
    }
}
