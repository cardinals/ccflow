using System;
using BP.En;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP;
 

namespace BP.En
{
	/// <summary>
	/// ������ʵ������
	/// </summary>
	public class IEnUrlAttr:IEnAttr
	{
		/// <summary>
		/// URL
		/// </summary>
		public const string Url="Url";		
		/// <summary>
		/// Target
		/// </summary>
		public const string Target="Target";	
	}
	/// <summary>
	/// �����Ե�ʵ��
	/// </summary>
	[Serializable]
	abstract public class IEnUrl : IEn
	{
		#region ��������
		/// <summary>
		/// ���
		/// </summary>
		public string  Url
		{
			get
			{
				return this.GetValStringByKey(IEnUrlAttr.Url) ; 
			}
			set
			{
				this.SetValByKey(IEnUrlAttr.Url,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string  Target
		{
			get
			{
				return this.GetValStringByKey(IEnUrlAttr.Target) ; 
			}
			set
			{
				this.SetValByKey(IEnUrlAttr.Target,value);
			}
		}
		#endregion		
 
		#region ����
		protected IEnUrl(){}
		protected IEnUrl(int oid) : base(oid){}
		protected IEnUrl(string no, string langugae ) : base(no,langugae){}
		#endregion
			
		
	}
	/// <summary>
	/// �����Ե�ʵ�弯��
	/// </summary>
	[Serializable]
	abstract public  class IEnsUrl : IEns
	{
		#region ���캯��
		public IEnsUrl( string fk_language) :base( fk_language){}
		/// <summary>
		/// ���캯��
		/// </summary>
		public IEnsUrl(){}
		#endregion
	}

}
