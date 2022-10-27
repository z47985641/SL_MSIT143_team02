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
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = (from d in (new MingSuContext()).Activities
                        select new Activity
                        {
                            ActivityId = d.ActivityId,
                            ActivityName = d.ActivityName
                        }).Take(3).ToList();
            return View(data);
        }
    }
}
