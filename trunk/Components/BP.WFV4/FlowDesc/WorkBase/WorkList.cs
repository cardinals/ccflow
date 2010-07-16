using System;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.WF
{	 
	/// <summary>
	/// ������Ϣ�б�
	/// </summary>
	public class WorkListAttr:EntityEnsNameAttr
	{
		/// <summary>
		/// ��������
		/// </summary>
		public const string WorkDesc="WorkDesc";
	}
	/// <summary>
	/// �����б�
	/// </summary>
	public class WorkList :EntityEnsName
	{
		#region ʵ�ֻ����ķ�����	
		public string WorkDesc
		{
			get
			{
				return this.GetValStringByKey(WorkListAttr.WorkDesc);
			}
			set
			{
				this.SetValByKey(WorkListAttr.WorkDesc,value);
			}
		}

		#endregion 

		#region ���췽��
		/// <summary>
		/// �����б�
		/// </summary> 
		public WorkList(){}		 
		/// <summary>
		/// �����б�
		/// </summary>
		/// <param name="_No"></param>
		public WorkList(string _No ): base(_No){}
		/// <summary>
		/// EnMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("WF_WorkList");
				map.EnDesc="������Ϣ�б�";
				map.AddTBStringPK(WorkListAttr.EnsEnsName,null,"����������",true,true,1,100,4);
				map.AddTBString(WorkListAttr.WorkDesc,null,"����",true,false,0,50,50);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	 /// <summary>
	 /// �����б�s
	 /// </summary>
	public class WorkLists :En.EntitiesEnsName
	{
		/// <summary>
		/// �����б�s
		/// </summary>
		public WorkLists(){}
		/// <summary>
		/// �����б�s
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkList();
			}
		}
	}
}
