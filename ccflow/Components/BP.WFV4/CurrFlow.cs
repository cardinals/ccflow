
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;
using BP.Port;
using BP.En;

namespace BP.WF
{
	/// <summary>
	/// ��ǰ���� ����
	/// </summary>
    public class CurrFlowAttr
    {
        #region ��������
        /// <summary>
        /// �����ڵ�
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// ��ǰ�ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ��������ǲ��Ƿ���
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// Title
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// ��ҵ����
        /// </summary>
        public const string FK_Taxpayer = "FK_Taxpayer";
        /// <summary>
        /// ��˰������
        /// </summary>
        public const string TaxpayerName = "TaxpayerName";
        /// <summary>
        /// ����ڵ�
        /// </summary>
        public const string FK_NodeOfJion = "FK_NodeOfJion";
        /// <summary>
        /// ������
        /// </summary>
        public const string FK_EmpOfJion = "FK_EmpOfJion";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string DTOfAccept = "DTOfAccept";
        /// <summary>
        /// �����˵� ����.
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        public const string FK_SJ = "FK_SJ";
        public const string RDT = "RDT";
        #endregion
    }
	/// <summary>
	/// ��ǰ����
	/// </summary>
	public class CurrFlow :Entity
	{
		public string FK_Flow
		{
			get
			{
				return this.GetValStringByKey(CurrFlowAttr.FK_Flow);
			}
		}
		public int WorkID
		{
			get
			{
				return this.GetValIntByKey(CurrFlowAttr.WorkID);
			}
		}

		#region ���캯��
		/// <summary>
		/// CurrFlow
		/// </summary>
		public CurrFlow()
		{
		}
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null)
					return this._enMap;
				Map map = new Map("V_WF_CurrFlow");
				map.EnDesc="��ǰ����";
				map.EnType=EnType.View;

				map.AddTBIntPK(CurrFlowAttr.WorkID,0,"����ID",false,false);
				map.AddTBString(CurrFlowAttr.Title,"","����",true,false,0,10,10);
				map.AddTBString(CurrFlowAttr.FK_Taxpayer,"","˰�������",true,false,0,50,10);
				map.AddTBString(CurrFlowAttr.TaxpayerName,"","��˰������",true,false,0,200,10);
				map.AddDDLEntities( CurrFlowAttr.Rec, Web.WebUser.No,"������", new Emps(),false);
				map.AddTBDate(CurrFlowAttr.RDT,"��������",false,false);
				map.AddDDLEntities( CurrFlowAttr.FK_Flow,null, "����", new Flows(),false);
				map.AddDDLEntities( CurrFlowAttr.FK_Node,null, "ͣ���ڵ�", new NodeExts(),false);

				map.AddDDLEntitiesPK( CurrFlowAttr.FK_NodeOfJion,null, "�����ڵ�", new NodeExts(),false);
				map.AddDDLEntitiesPK( CurrFlowAttr.FK_EmpOfJion,null, "������", new Emps(),false);
				map.AddTBDate(CurrFlowAttr.DTOfAccept,"��������",true,false);

				map.AddDDLEntities( CurrFlowAttr.FK_Dept,null, "����", new Depts(),false);
				map.AddTBInt("MyNum",1, "����",false,false);

				map.AddSearchAttr(CurrFlowAttr.FK_Dept);
				map.AddSearchAttr(CurrFlowAttr.FK_Flow);
				map.AddSearchAttr(CurrFlowAttr.FK_EmpOfJion);
				map.IsShowSearchKey=false;



				//�Ҽ���ӹ������湦��
				RefMethod rm = new RefMethod();
                rm.Title = this.ToE("WorkRpt", "��������"); ; // "��������";
				rm.ClassMethodName=this.ToString()+".DoShowWorkRpt()";
				rm.Icon="/Images/Btn/Rpt.gif";
				rm.Width=0;
				rm.Height=0;
				rm.HisAttrs=null;
				rm.Target=null;
				map.AddRefMethod(rm);


				//�Ҽ����ִ�й�������
				rm = new RefMethod();
				rm.Title="ִ�й���";
				rm.ClassMethodName=this.ToString()+".DoWork()";
				rm.Icon="/Images/Btn/Do.gif";
				rm.Width=0;
				rm.Height=0;
				rm.HisAttrs=null;
				rm.Target=null;
				map.AddRefMethod(rm);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		/// <summary>
		/// �Ҽ���ӹ�����������
		/// </summary>
		/// <returns></returns>
		public string DoShowWorkRpt()
		{
			PubClass.WinOpen("../../WFQH/WF/WFRpt.aspx?FK_Flow="+this.FK_Flow+"&WorkID="+this.WorkID );
			return null;
		}
		
		/// <summary>
		/// �Ҽ����ִ�й�������
		/// </summary>
		/// <returns></returns>
		public string DoWork()
		{
            PubClass.WinOpen("../../Face/MyFlow.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID);
			return null;
		}
	}
	/// <summary>
	/// ��ǰ����
	/// </summary>
	public class CurrFlows: Entities
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CurrFlow();
			}
		}
		/// <summary>
		/// CurrFlow
		/// </summary>
		public CurrFlows(){} 		 
		#endregion
	}
	
}
