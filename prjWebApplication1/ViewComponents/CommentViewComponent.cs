using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using System.Threading.Tasks;

using System.Linq;

namespace PJ_MSIT143_team02.ViewComponents
{
    public class CommentViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            var data = (from c in (new MingSuContext()).Comments
                        where c.RoomId.Equals(id) && c.CommentStatus == "yes"
                        select new Comment {
                            RoomId = c.RoomId,
                            CommentPoint = c.CommentPoint, 
                            CommentDetail = c.CommentDetail,
                            MemberAccount = c.MemberAccount,
                        }).ToList();
            return View(data);
        }
    }
}
