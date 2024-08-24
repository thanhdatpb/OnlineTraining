using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
	public class UserDao
	{
		OnlineTrainingEntities db = null;
		public UserDao()
		{
			db = new OnlineTrainingEntities();
		}

		//Hàm thêm dữ liệu
		public long Insert(User entity)
		{
			db.Users.Add(entity);
			db.SaveChanges();
			return entity.ID;
		}

		//Hàm cập nhật, sửa dữ liệu
		public bool Update(User entity)
		{
			try
			{
				var user = db.Users.Find(entity.ID);
				if (!string.IsNullOrEmpty(entity.Password))
				{
					user.Password = entity.Password;
				}
				user.Name = entity.Name;
				user.Address = entity.Address;
				user.Email = entity.Email;
				user.Phone = entity.Phone;
				user.ModifiedDate = entity.ModifiedDate;
				user.ModifiedBy = entity.ModifiedBy;
				user.Status = entity.Status;
				db.SaveChanges();

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		//Hàm xóa dữ liệu
		public bool Delete(int id)
		{
			try
			{
				var user = db.Users.Find(id);
				db.Users.Remove(user);
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public User GetByUserName(string userName)
		{
			return db.Users.SingleOrDefault(x => x.UserName == userName);
		}

		//Hàm cho chức năng show danh sách người dùng(bao gồm phân trang)
		//Chức năng dành cho admin, kiểm tra có bao nhiêu tk
		public IEnumerable<User> ListAllPaging(string searchString, int page, int pagesize)
		{
			IQueryable<User> model = db.Users;
			model = model.Where(x => x.UserName == "admin");

			if (!string.IsNullOrEmpty(searchString))
			{
				model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
			}
			return model.OrderByDescending(x => x.CreatDate).ToPagedList(page, pagesize);
		}

		public User ViewDetail(int id)
		{
			return db.Users.Find(id);
		}

		public int Login(string userName, string password, bool isLoginAdmin = false)
		{
			var result = db.Users.SingleOrDefault(x => x.UserName == userName);
			if (result == null)
			{
				return 0;
			}
			else
			{
				if (isLoginAdmin == true)
				{
					if (result.Status == false)
					{
						return -1;
					}
					else
					{
						if (result.Password == password)
						{
							return 1;
						}
						else
						{
							return -2;
						}
					}
				}
				else
				{
					if (result.Status == false)
					{
						return -1;
					}
					else
					{
						if (result.Password == password)
						{
							return 1;
						}
						else
						{
							return -2;
						}
					}
				}
			}
		}
	}
}
