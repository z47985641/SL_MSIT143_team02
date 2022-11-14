using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using System.Threading.Tasks;

using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace PJ_MSIT143_team02.ViewComponents
{
    public class CommentCreateViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            var data = new Comment {
                           RoomId = (int)id,
                       };
            return View(data);
        }
    }
}
