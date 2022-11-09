using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using System.Threading.Tasks;

using System.Linq;

namespace PJ_MSIT143_team02.ViewComponents
{
    public class ImageViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            var data = (from i in (new MingSuContext()).Images
                        join ir in (new MingSuContext()).ImageReferences on i.ImageId equals ir.ImageId
                        join r in (new MingSuContext()).Rooms on ir.RoomId equals r.RoomId
                        where r.RoomId == id
                        select new Image
                        {
                            ImageId = i.ImageId,
                            Image1 = i.Image1
                        }).ToList();
            return View(data);
        }
    }
}
