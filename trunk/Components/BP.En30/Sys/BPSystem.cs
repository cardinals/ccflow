using System;
using BP.En;
using BP.DA;

namespace BP.Sys
{
	
	public class BPSystemAttr : EntityNoNameAttr
	{	 
		/// <summary>
		/// ���
		/// </summary>
		public const string  IsOk="IsOk";
		/// <summary>
		/// ����
		/// </summary>
		public const string  URL="URL";
		/// <summary>
		/// �汾
		/// </summary>
		public const string  Ver="Ver";
		/// <summary>
		/// ��ע
		/// </summary>
		public const string  Note="Note";
		/// <summary>
		/// ��������
		/// </summary>
		public const string  IssueDate="IssueDate";
	}
	public class BPSystem : EntityNoName
	{
		/// <summary>
		/// �Ƿ�ʹ��
		/// </summary>
		public bool IsOk
		{
			get
			{
				return  this.GetValBooleanByKey(BPSystemAttr.IsOk);
			}
		}
		public string URL
		{
			get
			{
				return  this.GetValStringByKey(BPSystemAttr.URL);
			}
		}	
		public string IssueDate
		{
			get
			{
				return  this.GetValStringByKey(BPSystemAttr.IssueDate);
			}
		}
		public string Ver
		{
			get
			{
				return  this.GetValStringByKey(BPSystemAttr.Ver);
			}
		}
		public string Note
		{
			get
			{
				return  this.GetValStringByKey(BPSystemAttr.Note);
			}
		}	
		/// <summary>
		/// 
		/// </summary>
		public BPSystem()
		{
		}
		public BPSystem(string no) :base(no){}
		/// <summary>
		/// ��д����ķ���
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("Sys_Tem");
				map.EnDesc="ϵͳ";
				map.AddTBStringPK(BPSystemAttr.No,null,"���",true,false,1,10,10);
				map.AddTBString(BPSystemAttr.Name,null,"ϵͳ����",true,false,1,50,10);
				map.AddTBString(BPSystemAttr.URL,null,"����",true,false,1,100,20);
				map.AddTBInt(BPSystemAttr.IsOk,0,"�Ƿ�ʹ��",true,false);
				map.AddTBString(BPSystemAttr.Ver,"1.0","�汾",true,false,1,10,10);
				map.AddTBDate(BPSystemAttr.IssueDate,"��������",true,false);
				//map.AddTBString(BPSystemAttr.Note,"��ע","��ע",true,false,0,500,20);
				this._enMap=map;
				return this._enMap;
			}
		}
	}
	public class BPSystems : EntitiesNoName
	{
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new BPSystem();
			}
		}		
	}
	 
	
	
}
