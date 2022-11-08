using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.ViewComponents
{
    public class ActivityViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string? city,int RoomID)
        {
            MingSuContext ms = new MingSuContext();
            //var rcity = (from c in ms.Rooms
            //            select new Room { 
            //            Cities = c.Cities}).ToString();
            city = (from c in ms.Rooms
                    where c.RoomId.Equals(16)
                        select new Room { 
                        Cities = c.Cities}).ToString();
            string ca = ms.Rooms.Where(p => p.RoomId == RoomID).Select(c => c.Cities).First();
            string all = "全國適用";
            var data = (from d in ms.Activities
                        where ((d.ActivityLocation.Contains(ca) || d.ActivityLocation.Contains(all) )&& d.ActivityStatus.Equals("開放報名"))
                        select new Activity
                        {
                            ActivityId = d.ActivityId,
                            ActivityName = d.ActivityName
                        })
                        //.Where(d => d.ActivityLocation.Equals(rcity.ToString()))
                        .Take(4).ToList();

            return View(data);
        }
    }
}
