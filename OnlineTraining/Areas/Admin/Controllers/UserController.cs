using Model.Dao;
using System.Web.Mvc;

namespace OnlineTraining.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchSring, int page = 1, int pageSize = 200)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchSring, page, pageSize);
            ViewBag.SearchSring = searchSring;  
            return View(model);
        }
    }
}