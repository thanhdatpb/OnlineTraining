using OnlineTraining.Common;
using System.Web.Mvc;
using System.Web.Routing;


namespace OnlineTraining.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        /*
         * Kiểm tra xem người dùng đã đăng nhập chưa, có phải là admin không. 
         * Nếu chưa hoặc không phải admin, 
         * người dùng sẽ bị chuyển đến trang đăng nhập của khu vực giao diện quản trị. 
         */
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sesion = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (sesion == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            else
            {
                if (sesion.UserName.ToLower() != "admin")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
                }
            }
            base.OnActionExecuting(filterContext);
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            if (type == "warning")
            {
                TempData["AlertMessage"] = "alert-warning";
            }
            if (type == "error")
            {
                TempData["error"] = "alert-danger";
            }
        }
    }
}