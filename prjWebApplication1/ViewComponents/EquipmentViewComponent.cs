using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using System.Threading.Tasks;

using System.Linq;

namespace PJ_MSIT143_team02.ViewComponents
{
    public class EquipmentViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            var data = (from e in (new MingSuContext()).Equipment
                        join er in (new MingSuContext()).EquipmentReferences on e.EquipmentId equals er.EquipmentId
                        join r in (new MingSuContext()).Rooms on er.RoomId equals r.RoomId
                        where r.RoomId == id
                        select new Equipment
                        {
                            EquipmentId = e.EquipmentId,
                            EquipmentName = e.EquipmentName,
                        }).ToList();
            return View(data);
        }
    }
}
