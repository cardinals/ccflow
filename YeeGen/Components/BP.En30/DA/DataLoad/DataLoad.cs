using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// ����
	/// </summary>
	public class DataLoadAttr :EntityNoNameAttr
	{
		/// <summary>
		/// �ṹ����
		/// </summary>
		public const string Ex_Stru="Ex_Stru";
		/// <summary>
		/// ����
		/// </summary>
		public const string FK_DLType="FK_DLType";
		/// <summary>
		/// ����֮��Ҫִ�еĴ洢���̡�
		/// </summary>
		public const string DBProcedureName="DBProcedureName";
		public const string XlsDefaultTbName="XlsDefaultTbName";
		public const string  ImportClear = "ImportClear";

	}
	/// <summary>
	/// DataLoad
	/// </summary>
	public class DataLoad : EntityNoName 
	{
		 
		#region ��������
		/// <summary>
		/// �ṹ
		/// </summary>
		public  string  Ex_Stru
		{
			get
			{
			  return this.GetValStringByKey(DataLoadAttr.Ex_Stru);
			}
			set
			{
				this.SetValByKey(DataLoadAttr.Ex_Stru,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public  string  FK_DLType
		{
			get
			{
				return this.GetValStringByKey(DataLoadAttr.FK_DLType);
			}
			set
			{
				this.SetValByKey(DataLoadAttr.FK_DLType,value);
			}
		}
		/// <summary>
		/// Ҫ���õĴ洢����
		/// </summary>
		public  string  DBProcedureName
		{
			get
			{
				return this.GetValStringByKey(DataLoadAttr.DBProcedureName);
			}
			set
			{
				this.SetValByKey(DataLoadAttr.DBProcedureName,value);
			}
		}
		/// <summary>
		/// �ϴ�����ʱ��Ĭ��Excel���������
		/// </summary>
		public  string  XlsDefaultTbName
		{
			get
			{
				return this.GetValStringByKey(DataLoadAttr.XlsDefaultTbName);
			}
			set
			{
				this.SetValByKey(DataLoadAttr.XlsDefaultTbName,value);
			}
		}
		/// <summary>
		/// �ϴ�����ʱ��Ĭ��Excel���������
		/// </summary>
		public  int  ImportClear
		{
			get
			{
				return this.GetValIntByKey(DataLoadAttr.ImportClear);
			}
			set
			{
				this.SetValByKey(DataLoadAttr.ImportClear,value);
			}
		}
 
		 
		#endregion 

		#region ���췽��
		/// <summary>
		/// DataLoad
		/// </summary>
		public DataLoad(){}
		public DataLoad(string no) :base (no) {}
		 
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("Sys_DataLoad");
				map.EnDesc="װ��";
				//map.PhysicsTable;
				map.AddTBStringPK(DataLoadAttr.No,null,"Ҫװ�ص��������",true,false,3,50,20);
				map.AddTBString(DataLoadAttr.Name,null,"����",true,false,3,50,20);
				map.AddTBString(DataLoadAttr.Ex_Stru,null,"�ṹ����",true,false,3,50,20);
				map.AddDDLEntities(DataLoadAttr.FK_DLType,null,"����",new DataLoadTypes(),true);
				map.AddTBString(DataLoadAttr.DBProcedureName,null,"�洢����",true,false,3,50,20);				 
				map.AddTBString(DataLoadAttr.XlsDefaultTbName,null,"Ĭ��Excel���������",true,false,0,50,30);				 
				map.AddBoolean(DataLoadAttr.ImportClear,true,"����ʱ�Ƿ������",true,true);

				this._enMap=map;
				return this._enMap; 
			}
		}		 
		#endregion 

	}
	/// <summary>
	/// ��˰�˼��� 
	/// </summary>
	public class DataLoads : EntitiesNoName
	{
		 
		/// <summary>
		/// DataLoads
		/// </summary>
		public DataLoads(){}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new DataLoad();
			}
		}
		public void RetrieveByFK_DLType( string fk_DataLoadType)
		{
			QueryObject qo = new QueryObject( this );
			qo.AddWhere( DataLoadAttr.FK_DLType , fk_DataLoadType);
			qo.DoQuery();
		}
	 
	}
}
