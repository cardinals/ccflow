using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.En;


namespace BP.Rpt.Doc
{
	public class RefLinkAttr : EntityNoNameAttr
	{
		/// <summary>
		/// ������
		/// </summary>
		public const  string ClassName="ClassName";	
		/// <summary>
		/// ����
		/// </summary>
		public const  string Url="Url";
		/// <summary>
		/// Ŀ��
		/// </summary>
		public const  string Target="Target";
		public const  string Note="Note";
	}

	/// <summary>
	/// ��ع�������
	/// </summary>
	public class RefLink:EntityNoName 
	{		
		#region ����

		/// <summary>
		/// Ҫ���ӵ��ĵط���
		/// </summary>
		public string Url
		{
			get
			{
				return this.GetValStringByKey(RefLinkAttr.Url);
			}
			set
			{
				SetValByKey(RefLinkAttr.Url,value);
			}
		}
		/// <summary>
		/// ��ϵ�HTMl�ַ�����
		/// </summary>
		/// <param name="className"></param>
		/// <param name="val"></param>
		/// <returns></returns>
		public string HtmlName(string className, string val)
		{	 
			return "<a href='"+System.Web.HttpContext.Current.Request.ApplicationPath+this.Url+"?"+className+"="+val+"' target='"+this.Target+"' >"+this.Name+"</a>";
		}
		/// <summary>
		/// ���ӵ�Ŀ��
		/// </summary>
		public string Target
		{
			get
			{
				return this.GetValStringByKey(RefLinkAttr.Target);
			}
			set
			{
				SetValByKey(RefLinkAttr.Target,value);
			}
		}
		/// <summary>
		/// ��ע��������ʾ���û�����
		/// </summary>
		public string Note
		{
			get
			{
				return this.GetValStringByKey(RefLinkAttr.Note);
			}
			set
			{
				SetValByKey(RefLinkAttr.Note,value);
			}
		}
	    /// <summary>
	    /// ������
	    /// </summary>		 
		public string ClassName
		{
			get
			{
				return  this.GetValStringByKey(RefLinkAttr.ClassName)   ; 
			}
		}
		#endregion 

		public RefLink(){}
	
		
		public RefLink(string _No ) : base(_No){}	
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("Sys_RptRefLink");
				map.EnDesc="Port_RefLink";
				map.AddAttr(RefLinkAttr.No, "", DataType.AppString,true,"���");
				map.AddAttr(RefLinkAttr.Name,null,DataType.AppString,false,"����");				
				map.AddAttr(RefLinkAttr.ClassName,null,DataType.AppString ,true,"������");
				map.AddAttr(RefLinkAttr.Url,null,DataType.AppString,false,"url");
				map.AddAttr(RefLinkAttr.Target,null,DataType.AppString,false,"Target");
				map.AddAttr(RefLinkAttr.Note,null,DataType.AppString,false,"��ע");				
				this._enMap=map;
				return this._enMap; 
			}
		}
	
	}
	/// <summary>
	/// ��ع������� ����
	/// </summary>
	public class RefLinks :EntitiesNoName 
	{
		/// <summary>
		/// ����
		/// </summary>
		public string Title
		{
			get
			{
				return "��ع���ʵ��";
			}
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new RefLink();
			}
		}	
		public RefLinks(){}
		/// <summary>
		/// ����className ������������֮�������ʵ�弯�ϡ�
		/// </summary>
		/// <param name="className"></param>
		public RefLinks(string className)
		{
			QueryObject qo = new QueryObject(this) ; 
			qo.AddWhere(RefLinkAttr.ClassName,className) ; 
			qo.DoQuery();			
		}
	}
}
