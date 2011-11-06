using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.WF
{
	/// <summary>
	/// ��������
	/// </summary>
    public class WinFlowAttr : EntityNoNameAttr
    {
        /// <summary>
        /// �������
        /// </summary>
        public const string FK_WinFlowSort = "FK_WinFlowSort";
        /// <summary>
        /// ���������ڡ�
        /// </summary>
        public const string CreateDate = "CreateDate";
        /// <summary>
        /// ������
        /// </summary>
        public const string Creater = "Creater";
    }
	/// <summary>
	/// ����
	/// ��¼�����̵���Ϣ��
	/// ���̵ı�ţ����ƣ�����ʱ�䣮
	/// </summary>
	public class WinFlow :EntityNoName
	{
		#region ��������
		/// <summary>
		/// �������
		/// </summary>
		public string FK_WinFlowSort
		{
			get
			{
				return this.GetValStringByKey(WinFlowAttr.FK_WinFlowSort);
			}
			set
			{
				this.SetValByKey(WinFlowAttr.FK_WinFlowSort,value);
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public int Creater
		{
			get
			{
				return this.GetValIntByKey(WinFlowAttr.Creater);
			}
			set
			{
				this.SetValByKey(WinFlowAttr.Creater,value);
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string CreateDate
		{
			get
			{
				return this.GetValAppDateByKey(WinFlowAttr.CreateDate);
			}
			set
			{
				this.SetDateValByKey(WinFlowAttr.CreateDate,value);
			}
		}		
		#endregion

		#region ��չ����
		/// <summary>
		/// �ǲ���һ���Զ����еĹ�������.
		/// </summary>
		public bool IsPCWinFlow
		{
			get
			{
				foreach(Node nd in this.HisNodes)
				{
					if (nd.IsPCNode)
						return true;
				}
				return false;
			}
		}
		/// <summary>
		/// �ڵ�
		/// </summary>
		private Nodes  _HisNodes=null;
		/// <summary>
		/// ���Ľڵ㼯��.
		/// </summary>
		public Nodes HisNodes
		{
			get
			{
				if (this._HisNodes==null)					 
					_HisNodes =new Nodes(this.No);				 
				return _HisNodes;
			}
		}
		/// <summary>
		/// ���� Start �ڵ�
		/// </summary>
		public Node HisStartNode
		{
			get
			{
				foreach(Node nd in this.HisNodes)
				{
					if (nd.IsStartNode)
						return nd;
				}
				throw new Exception("û���ҵ����Ŀ�ʼ�ڵ�,�������̶������.");
			}
		}
		/// <summary>
		/// ���� end �ڵ�
		/// �����кܶ�Ľ����ڵ�.
		/// </summary>
		public Nodes HisEndNodes
		{
			get
			{
				Nodes ens = new Nodes();
				foreach(Node nd in this.HisNodes)
				{
					if (nd.IsEndNode)
						ens.AddEntity(nd);
				}
				if (ens.Count==0)
					throw new Exception("û���ҵ�����End�ڵ�,�������̶������.");
				else
					return ens;
			}
		} 
		 
	
		#endregion
		

		#region ���췽��
		/// <summary>
		/// ����
		/// </summary>
		public WinFlow()
		{
			//this.No =this.GenerNewNo;
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="_No">���</param>
		public WinFlow(string _No ): base(_No){}
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				 
				Map map = new Map("WF_Flow");			 
				map.EnDesc="������Ϣ";
				map.AddTBStringPK(WinFlowAttr.No,null,"�������",false,true,3,3,3);
				map.AddTBString(WinFlowAttr.Name,null,"��������",true,false,0,20,10);
			//	map.AddDDLEntitiesNoName(WinFlowAttr.FK_WinFlowSort,"001","�������",new WinFlowSorts(),true);			 
			//	map.AddDDLEntities(WinFlowAttr.Creater,WebUser.OID,DataType.AppInt,"������",new Emps(),"OID","Name",true);
			//	map.AddTBDate(WinFlowAttr.CreateDate , DateTime.Now.ToString("yyyy-MM-dd hh-mm"),"����ʱ��",true,true);

			//	map.AddSearchAttrsByKey(WinFlowAttr.FK_WinFlowSort);
			//	map.AddSearchAttrsByKey(WinFlowAttr.Creater);
				this._enMap=map;

               
			 

				return this._enMap;
			}
		}
		#endregion 

		#region  ��������
		/// <summary>
		/// ִ�б���ǰ�ļ��
		/// </summary>
		/// <returns>����Ĺ��������ǲ��Ǻ���</returns>
		public bool DoCheck()
		{
			// 1 �ж��ǲ����п�ʼ�ڵ�?
			// 2 �ж�����
			return true;
		}
		#endregion
	}

	/// <summary>
	/// ���̼���
	/// </summary>
	public class WinFlows : EntitiesNoName
	{
		#region ���췽��
		/// <summary>
		/// ���񼯺�
		/// </summary>
		public WinFlows(){}
		/// <summary>
		/// ������������������� ���񼯺�.
		/// </summary>
		/// <param name="FK_WinFlowSort"></param>
		public WinFlows(string FK_WinFlowSort)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WinFlowAttr.FK_WinFlowSort,FK_WinFlowSort);
			qo.DoQuery();
			return;
		}		
		#endregion

		#region �õ�ʵ��
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WinFlow();
			}
		}
		#endregion
	}	 
}

