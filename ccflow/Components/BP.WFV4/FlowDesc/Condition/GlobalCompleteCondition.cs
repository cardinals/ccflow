
using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
//using BP.ZHZS.Base;
using BP.Sys;

namespace BP.WF
{	 
	/// <summary>
	/// �ڵ�������������
	/// </summary>
	public class GlobalCompleteConditionAttr : ConditionAttr
	{
		/// <summary>
		/// ����No
		/// </summary>
		public const string NodeID="NodeID";
	}
	/// <summary>
	/// �нڵ�� �ڵ������������
	/// </summary>
	public class GlobalCompleteCondition :Condition
	{
		#region ��������
		/// <summary>
		/// gongu
		/// </summary>
		public string NodeID
		{
			get
			{
				return this.GetValStringByKey(GlobalCompleteConditionAttr.NodeID);
			}
			set
			{
				this.SetValByKey(GlobalCompleteConditionAttr.NodeID,value);
			}   
		}		 
		#endregion 

		#region ��չ����
		/// <summary>
		/// ��������
		/// </summary>
		private Flow _HisFlow=null;
		/// <summary>
		/// ��������
		/// </summary>
		public Flow HisFlow
		{
			get
			{
				if (this._HisFlow==null)
					this._HisFlow =  new Flow(this.NodeID);

				return this._HisFlow ; 
			}
		}		 
		#endregion 

		#region ʵ�ֻ���ķ���
	    
		#endregion

		#region ���췽��
		/// <summary>
		/// ȫ�ֹ���������ɹ���������
		/// </summary>
		public GlobalCompleteCondition(){}
		/// <summary>
		/// ����
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("WF_GlobalCompleteCondition");
				map.EnDesc="ȫ�ֹ���������ɹ���������";
                map.AddTBIntPK(GlobalCompleteConditionAttr.NodeID,0, "��ǰ��", true, true);
				map.AddTBString(GlobalCompleteConditionAttr.AttrKey,null,"����",true,true,1,60,20);
                map.AddTBString(GlobalCompleteConditionAttr.FK_Operator, "=", "�������", true, true, 1, 60, 20);
				map.AddTBString(GlobalCompleteConditionAttr.OperatorValue,"","Ҫ�����ֵ",true,true,1,60,20);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

		#region ��������
		
		#endregion
	}
	/// <summary>
	/// ȫ�ֹ���������ɹ���������s
	/// һ����˵��ֻ��һ������.
	/// ����������֮���� or  ��ϵ.
	/// ����ж������,����,����һ��������ͨ��.
	/// </summary>
	public class GlobalCompleteConditions :Conditions
	{
		#region ����
		/// <summary>
		/// ȫ�ֹ���������ɹ���������
		/// </summary>
		public GlobalCompleteConditions(){}
		/// <summary>
		/// ȫ�ֹ���������ɹ�������������
		/// </summary>
		/// <param name="flowNo">flowNo</param>
		/// <param name="workFolwID">workFolwID</param>
		public GlobalCompleteConditions(string flowNo, int workFolwID)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(GlobalCompleteConditionAttr.NodeID,flowNo);
			qo.DoQuery();
			foreach(GlobalCompleteCondition en in this)
			{
                en.WorkID = workFolwID;
			}
		}
		#endregion

		#region ʵ�ֻ���ķ���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new GlobalCompleteCondition();
			}
		}
		#endregion
	}
}
