using Model;
using Model.Dao;
using OnlineTraining.Common;
using System;
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
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult AddUserAjax(string username, string name, string password, string address, string email, string phone)
        {
            try
            {
                var dao = new UserDao();
                User user = new User();     
                var encryptedMd5Pas = Encryptor.MD5Hash(password);

                user.UserName = username;
                user.Name = name;
                user.Address = address;
                user.Email = email;
                user.Phone = phone;
                user.Status = true;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    return Json(new { status = true });
                }
                else
                {
                    return Json(new { status = false });
                }
            }
            catch
            {
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public JsonResult UpdateUserAjax(string userid, string name, string address, string email, string phone)
        {
            try
            {
                var dao = new UserDao();
                User user = new User();
                user = dao.ViewDetail(Convert.ToInt16(userid));

                user.Name = name;
                user.Address = address;
                user.Email = email;
                user.Phone = phone;

                bool editresult = dao.Update(user);
                if (editresult == true)
                {
                    return Json(new { status = true });
                }
                else
                {
                    return Json(new { status = false });
                }
            }
            catch
            {
                return Json(new { status = false });
            }
        }
    }
}