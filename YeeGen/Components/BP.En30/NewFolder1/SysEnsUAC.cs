using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.Sys
{
	/// <summary>
	/// ���ʿ���
	/// </summary>
	public class SysEnUACAttr  //EntityClassNameAttr
	{	
		/// <summary>
		/// ������ԱID
		/// </summary>
		public const string FK_Emp="FK_Emp";
		/// <summary>
		/// ʵ��������
		/// </summary>
		public const string FK_Ens="FK_Ens";
		/// <summary>
		///�Ƿ�����
		/// </summary>
		public const string IsInsert="IsInsert"; 
		/// <summary>
		/// �Ƿ�ɾ��
		/// </summary>
		public const string IsDelete="IsDelete";
		/// <summary>
		/// �Ƿ����
		/// </summary>
		public const string IsUpdate="IsUpdate";
		/// <summary>
		/// �Ƿ���ͼ
		/// </summary>
		public const string IsView="IsView";
		//public const string IsPrint="IsPrint";
		/// <summary>
		/// �Ƿ񸽼�
		/// </summary>
		public const string IsAdjunct="IsAdjunct";
		//public const string IsExport="IsExport";
		//public const string IsHelp="IsHelp";

	}
	/// <summary>
	/// ���ʿ���
	/// </summary> 
	public class SysEnsUAC:Entity //EntityClassName 
	{
		#region ��������
		/// <summary>
		/// ������ԱID
		/// </summary>
		public  string  FK_Emp
		{
			get
			{
				return this.GetValStringByKey(SysEnUACAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(SysEnUACAttr.FK_Emp,value);
			}
		} 
		/// <summary>
		/// ������
		/// </summary>
		public  string  FK_Ens
		{
			get
			{
				return this.GetValStringByKey(SysEnUACAttr.FK_Ens);
			}
			set
			{
				this.SetValByKey(SysEnUACAttr.FK_Ens,value);
			}
		}
		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public  bool  IsInsert
		{
			get
			{
				return this.GetValBooleanByKey(SysEnUACAttr.IsInsert);
			}
			set
			{
				this.SetValByKey(SysEnUACAttr.IsInsert,value);
			}
		}
		/// <summary>
		/// �Ƿ�ɾ��
		/// </summary>
		public  bool  IsDelete
		{
			get
			{
				return this.GetValBooleanByKey(SysEnUACAttr.IsDelete);
			}
			set
			{
				this.SetValByKey(SysEnUACAttr.IsDelete,value);
			}
		}
		
		/// <summary>
		/// �Ƿ����
		/// </summary>
		public  bool  IsUpdate
		{
			get
			{
				return this.GetValBooleanByKey(SysEnUACAttr.IsUpdate);
			}
			set
			{
				this.SetValByKey(SysEnUACAttr.IsUpdate,value);
			}
		}
		/// <summary>
		/// �Ƿ�鿴
		/// </summary>
		public  bool  IsView
		{
			get
			{
				return this.GetValBooleanByKey(SysEnUACAttr.IsView);
			}
			set
			{
				this.SetValByKey(SysEnUACAttr.IsView,value);
			}
		} 
		/// <summary>
		/// �Ƿ񸽼�
		/// </summary>
		public  bool  IsAdjunct
		{
			get
			{
				return this.GetValBooleanByKey(SysEnUACAttr.IsAdjunct);
			}
			set
			{
				this.SetValByKey(SysEnUACAttr.IsAdjunct,value);
			}
		} 
		#endregion 

		#region ��鷽��
//		/// <summary>
//		/// CheckByEmpID
//		/// </summary>
//		/// <param name="empId"></param>
//		public  static void   CheckByEmpID(int empId)
//		{
//			SysEns ens  =  new SysEns();
//			ens.RetrieveAll();
//			foreach(SysEn en in ens)
//			{
//				SysEnsUAC ua = new SysEnsUAC(en.EnsClassName,empId);
//			}
//		}
		/// <summary>
		/// CheckAll
		/// </summary>
		public  static void  CheckAll()
		{
//			Emps  ens  =  new Emps();
//			ens.RetrieveAll();
//			foreach(Emp en in ens)
//			{
//				SysEnsUAC.CheckByEmpID(en.OID) ; 				
//			}
		}
		#endregion 

		#region ���췽��		 
		/// <summary>
		/// ���ʿ���
		/// </summary>
		public SysEnsUAC()
		{
		}		 
		/// <summary>
		/// ���ʿ���
		/// </summary>
		/// <param name="classesName">����������</param>
		/// <param name="empid">������ԱID</param>
		public SysEnsUAC(Entities ens, string empid)
		{
			Entity myen = ens.GetNewEntity;
			if (myen.EnMap.EnType==EnType.View || myen.EnMap.EnType==EnType.ThirdPart )
			{
				/*  ��ͼ  ThirdPart */
			}

			if (empid=="admin")
			{				
				return ;
			}

			if (SystemConfig.SysNo=="TP"   )
			{
//				/* ����� TP ��Ŀ����������Ϣ����Ա */
			 
				return;
			}
			if (  myen.EnMap.EnType == EnType.Dtl )
			{
				 
				return ;

			}
			
		
			if ( (myen.EnMap.EnType == EnType.PowerAble
				|| myen.EnMap.EnType != EnType.Dtl)==false )
			{
				/* �����������Ȩ�޹����ʵ�壬 һ�ɲ��ø����޸ģ��쿴�� */
				return ;
			}

			/* ������������Ȩ�޹���ġ�*/
			string classesName = ens.ToString();


			//SysEnPowerAble en = new SysEnPowerAble( classesName );
			this.FK_Ens=classesName;
			this.EmpID=empid;
			if (this.IsExits==false)
			{
				 
				this.Insert();
			}
			else
			{
				this.Retrieve();
			}
		}
		 
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null)
					return this._enMap;
				Map map = new Map("Sys_EnsUAC");
				map.EnDesc="Ȩ����Ϣ";
				map.EnType=EnType.Admin; //ʵ�����ͣ�admin ϵͳ����Ա��PowerAble Ȩ�޹����,Ҳ���û���,��Ҫ���������Ȩ�޹������������������á���
				
			 
				map.EnType=EnType.Admin; //ʵ�����͡�


				map.AddDDLEntitiesPK(SysEnUACAttr.EmpID,SysEnUACAttr.EmpID, 0 , DataType.AppInt ,"����Ա",new Emps(),"OID","Name",false);
				map.AddDDLEntitiesPK(SysEnUACAttr.FK_Ens,null, DataType.AppString,"��������",new SysEnPowerAbles(),SysEnAttr.EnsClassName,SysEnAttr.Name,false);
				map.AddBoolean(SysEnUACAttr.IsView,true,"�鿴",true,true);
				map.AddBoolean(SysEnUACAttr.IsInsert,false,"����",true,true);
				map.AddBoolean(SysEnUACAttr.IsUpdate,false,"����",true,true);
				map.AddBoolean(SysEnUACAttr.IsDelete,false,"ɾ��",true,true);
				map.AddBoolean(SysEnUACAttr.IsAdjunct,false,"����",true,true);

				//map.AddBoolean(SysEnUACAttr.IsExport,false,"���ݵ���",true,true);
				//map.AddBoolean(SysEnUACAttr.IsPrint,false,"��ӡ",true,true);
				//map.AddBoolean(SysEnUACAttr.IsHelp,false,"�Ƿ��ǰ���ҳ��",true,true);

				map.AddSearchAttr(SysEnUACAttr.EmpID);
				map.AddSearchAttr(SysEnUACAttr.FK_Ens);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// ���ʿ���
	/// </summary> 
	public class SysEnsUACs : Entities//EntitiesNoName
	{
		#region ˢ��
		/// <summary>
		/// ˢ��
		/// </summary>
		public static void RefreshUAC()
		{			
		}
		#endregion		 
		
		#region ���캯��
		/// <summary>
		/// ����ʵ����ʵĹ���
		/// </summary>
		public SysEnsUACs(){}
		/// <summary>
		/// New entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysEnsUAC();
			}
		}
		#endregion
	
	}
}
