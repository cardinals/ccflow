using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.WF
{
	 
	/// <summary>
	/// ��������
	/// </summary>
	public class BookXHXHAttr:BookBaseAttr
	{
		
	}
	/// <summary>
	/// ����
	/// </summary> 
	public class BookXH :BookBase
	{
		 
		//private static string PTable="WF_BookXH";

		#region ���췽��
		/// <summary>
		/// ��Ŀ
		/// </summary>
		public BookXH(){}
		#endregion 

		#region Map
		/// <summary>
		/// EnMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map( Book.PTable );
				map.DepositaryOfMap=Depositary.None;

				map.EnDesc="����";                
				map.IsDelete=true;
				map.IsInsert=true;
				map.IsUpdate=true;
				map.IsView=true;
				map.AddTBMID();
				map.AddTBIntPK(BookAttr.WorkID,0,"����ID",true,true);
				map.AddBoolean(BookAttr.IsReturn,false,"�黹��",true,true);
				map.AddTBString(BookAttr.FlowTitle,null,"���̱���",true,true,0,100,5);

				map.AddTBDateTime(BookAttr.RecordDateTime,"����ʱ��",true,true);
				map.AddDDLEntities(BookAttr.Recorder,1, DataType.AppInt, "�黹��",new Emps(),EmpAttr.OID,EmpAttr.Name,false);
				map.AddTBString(BookAttr.ReturnerNote,null,"��ע",true,true,0,100,5);
				map.AddTBDateTime(BookAttr.ReturnTime,"�黹ʱ��",false,true);
				map.AddTBString(BookAttr.ReturnerNote,null,"��ע",true,true,0,100,5);
				map.AddTBIntPK(BookAttr.FK_NodeRefFunc,0,"����",false,true);
				map.AddTBString(BookAttr.ReturnerDept,null,"����",true,true,0,100,5);
				map.AddTBString(BookAttr.ReturnerZSJG,null,"���ջ���",true,true,0,100,5);
				
				map.AddSearchAttr(BookAttr.IsReturn);
				map.AttrsOfSearch.AddFromTo("����",BookAttr.RecordDateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataFormat), DA.DataType.CurrentData,7);
 
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// ��Ŀ
	/// </summary>
	public class BookXHs :BookBases
	{
		 

		#region ���췽������
		 
		 
		
		#endregion 

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Book();
			}
		}
		#endregion
	}
}
