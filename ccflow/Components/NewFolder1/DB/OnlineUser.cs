using System;
using System.Collections;
using System.Web.Hosting;

namespace BP.Web
{
	/// <summary>
	/// �����û�
	/// </summary>
	public class OnlineUserManager
	{
		public static OnlineUsers GetOnlineUsers()
		{			 
			OnlineUsers users= (OnlineUsers)System.Web.HttpContext.Current.Application["OnlineUsers"];
			if (users==null)
			{
				users= new OnlineUsers();
				System.Web.HttpContext.Current.Application["OnlineUsers"]=users;
			}
			return users;
		} 
	  
		  
	}
	/// <summary>
	/// OnlineUser ��ժҪ˵����
	/// </summary>
	public class OnlineUser
	{
		#region ����
		 
		private string  _EmpNo="";
		private string  _Name="";
		private string  _DeptName="";
		private string  _IP="";
		private string  _LoginDateTime=DateTime.Now.ToString("yyyy-MM-dd hh:mm");
		//private string  _AccessDateTime=_LoginDateTime ;

		
		/// <summary>
		/// ������Ա���
		/// </summary>
		public string No
		{
			get
			{
				return this._EmpNo;
			}
			set
			{
				_EmpNo=value;
			}
		}	 
		public string IP
		{
			get
			{
				return this._IP;
			}
			set
			{
				_IP=value;
			}
		}	 
		/// <summary>
		/// ����
		/// </summary>
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				_Name=value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string DeptName
		{
			get
			{
				return this._DeptName;
			}
			set
			{
				_DeptName=value;
			}
		}
		/// <summary>
		/// ��½����
		/// </summary>
		public string LoginDateTime
		{
			get
			{
				return this._LoginDateTime;
			}
			set
			{
				_LoginDateTime=value;
			}
		}
//		/// <summary>
//		/// ��������
//		/// </summary>
//		public string AccessDateTime
//		{
//			get
//			{
//				return this._AccessDateTime;
//			}
//			set
//			{
//				_AccessDateTime=value;
//			}
//		}
		#endregion

		/// <summary>
		/// �����û�
		/// </summary>
		public OnlineUser()
		{

		}		 
	}
	/// <summary>
	/// ��Ϣ����
	/// </summary>
	public class OnlineUsers : ArrayList
	{
		#region ������Ϣ
		/// <summary>
		/// ������Ϣ
		/// </summary>
		/// <param name="workId"></param>
		/// <param name="nodeId"></param>
		/// <param name="toEmpId"></param>
		/// <param name="info"></param>
		public void AddOnlineUser(  string empNo, string empName, string deptName, string ip)
		{
			OnlineUser user = new OnlineUser();
			user.No=empNo;
			user.Name=empName;
			user.IP=ip;
			user.DeptName=deptName;

			this.AddOnlineUser(user);
		}
		/// <summary>
		/// ������Ϣ
		/// </summary>
		/// <param name="OnlineUser">��Ϣ</param>
		public void AddOnlineUser(OnlineUser user)
		{			 
			if (this.IsExites(user.No)==false)
				this.Add(user);
		}
		public bool IsExites(string  empNo)
		{
			foreach(OnlineUser ou in this)
			{
				if (ou.No==empNo)
					return true;
			}
			return false;
		}
		#endregion 

		#region ������Ϣ���ϵĲ���		
		#endregion

		public OnlineUsers()
		{
		}
		/// <summary>
		/// ����λ��ȡ������
		/// </summary>
		public new OnlineUser this[int index]
		{
			get 
			{
				return (OnlineUser)this[index];
			}
		}
	}
}
