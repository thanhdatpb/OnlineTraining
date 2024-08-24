using System;

namespace OnlineTraining.Common
{
	[Serializable]
	public class UserLogin
	{
		public long UserID { get; set; }
		public string UserName { get; set; }
		public string GroupID { get; set; }
	}
}