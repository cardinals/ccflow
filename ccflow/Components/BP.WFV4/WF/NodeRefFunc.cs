
using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.WF;
using BP.En;


namespace BP.WF
{
	 
	/// <summary> 
	/// �ڵ����ع�������
	/// </summary>
	public class NodeRefFuncAttr :EntityOIDNameAttr
	{  
		#region ��������
		/// <summary>
		/// �ڵ�ID
		/// </summary>
		public const  string NodeId="NodeId";	
		/// <summary>
		/// URL
		/// </summary>
		public const  string URL="URL";
		/// <summary>
		/// ��ع�������
		/// </summary>
		public const  string RefFuncType="RefFuncType";
		/// <summary>
		/// Width
		/// </summary>
		public const  string Width="Width";
		/// <summary>
		/// Height
		/// </summary>
		public const  string Height="Height";
		/// <summary>
		/// Ĭ�ϵ�ͼ��
		/// </summary>
		public const  string DefaultIcon="DefaultIcon";
		/// <summary>
		/// Ĭ�ϵ�����ͼ��
		/// </summary>
		public const  string DefaultHover="DefaultHover";
		/// <summary>
		/// ToolTip
		/// </summary>
		public const  string ToolTip="ToolTip";
		/// <summary>
		/// �ʹ�����
		/// </summary>
		public const  string TimeLimit="TimeLimit";
		public const  string FilePrix="FilePrix";
		#endregion
	}
	/// <summary>
	/// ��ع���
	/// </summary>
	public class NodeRefFunc : EntityOIDName
	{
		#region ��������
	    /// <summary>
	    /// ��ع�������
		/// </summary>
		public int RefFuncType
		{
			get
			{  
				return this.GetValIntByKey(NodeRefFuncAttr.RefFuncType);
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.RefFuncType,value);
			}
		}
		/// <summary>
		/// NodeId
		/// </summary>
		public int NodeId
		{
			get
			{
				return this.GetValIntByKey(NodeRefFuncAttr.NodeId);
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.NodeId,value);
			}
		}
		/// <summary>
		/// url
		/// </summary>
		public string URL
		{
			get
			{
				return this.GetValStringByKey(NodeRefFuncAttr.URL);
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.URL,value);
			}
		}
		public string FilePrix
		{
			get
			{
				return this.GetValStringByKey(NodeRefFuncAttr.FilePrix).Trim();
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.FilePrix,value);
			}
		}
		/// <summary>
		/// Height
		/// </summary>
		public int Height
		{
			get
			{
				return this.GetValIntByKey(NodeRefFuncAttr.Height);
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.Height,value);
			}
		}
		/// <summary>
		/// Width
		/// </summary>
		public int Width
		{
			get
			{
				return this.GetValIntByKey(NodeRefFuncAttr.Width);
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.Width,value);
			}
		}
		/// <summary>
		/// Ĭ������ͼ��
		/// </summary>
		public string DefaultIcon
		{
			get
			{
				if ( this.GetValStringByKey(NodeRefFuncAttr.DefaultIcon)=="")
					return "/images/AppIcon/NodeRefIcon/Default.gif";
				else
					return this.GetValStringByKey(NodeRefFuncAttr.DefaultIcon); 
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.DefaultIcon,value);
			}
		}
		/// <summary>
		/// Ĭ�ϵ�����ͼ��
		/// </summary>
		public string DefaultHover
		{
			get
			{
				if ( this.GetValStringByKey(NodeRefFuncAttr.DefaultHover)=="")
					return "/images/AppIcon/NodeRefIcon/DefaultHover.gif";
				else
					return this.GetValStringByKey(NodeRefFuncAttr.DefaultHover);

				 
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.DefaultHover,value);
			}
		}
		/// <summary>
		/// ��ʾ��Ϣ
		/// </summary>
		public string ToolTip
		{
			get
			{
				if ( this.GetValStringByKey(NodeRefFuncAttr.ToolTip)=="")
					return this.Name;

				return this.GetValStringByKey(NodeRefFuncAttr.ToolTip) ; 
			}
			set
			{
				this.SetValByKey(NodeRefFuncAttr.ToolTip,value);
			}
		}
		/// <summary>
		/// �ʹ�����
		/// </summary>
		public int TimeLimit
		{
			get
			{
				return this.GetValIntByKey(NodeRefFuncAttr.TimeLimit);
			}
		}
		#endregion

		#region ���캯��
		/// <summary>
		/// ss
		/// </summary>
		public NodeRefFunc(){}
		/// <summary>
		/// BookOID
		/// </summary>
		/// <param name="_oid"></param>
		public NodeRefFunc(int _oid) : base(_oid)
		{
		}

        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForAppAdmin();
                return uac;
            }
        }
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_NodeRefFunc");
                map.EnDesc = "�ڵ����ع���";
                map.EnType = EnType.Admin;

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPKOID();
                map.AddTBString(NodeRefFuncAttr.NodeId, "0", "�ڵ�", true, false, 0, 200, 20);
                map.AddTBString(NodeRefFuncAttr.Name, "", "��������", true, false, 0, 200, 20);
                map.AddTBString(NodeRefFuncAttr.URL, "", "�ļ�·��", true, true, 0, 200, 20);
                map.AddTBInt(NodeRefFuncAttr.TimeLimit, 3, "�����ʹ�����", true, false);
                map.AddTBString(NodeRefFuncAttr.FilePrix, null, "�ĺ�", true, false, 0, 50, 10);

                /*
                map.AddDDLSysEnum(NodeRefFuncAttr.RefFuncType,0,"����",true,true,"RefFuncType");
                map.AddTBInt(NodeRefFuncAttr.Height,150,"�߶�",false,false);
                map.AddTBInt(NodeRefFuncAttr.Width,300,"���",false,false);
                map.AddTBString(NodeRefFuncAttr.DefaultIcon,"/images/AppIcon/NodeRefIcon/Default.gif","ͼ��",false,false,0,200,20);
                map.AddTBString(NodeRefFuncAttr.DefaultHover,"/images/AppIcon/NodeRefIcon/DefaultHover.gif","����ͼ��",false,false,0,200,20);
                map.AddTBString(NodeRefFuncAttr.ToolTip,"��ع���","��ʾ��Ϣ",false,false,0,300,20);
                */
                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion 

		#region ���ػ��෽��
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdateInsertAction()
		{            	 
			return  base.beforeUpdateInsertAction();			 
		}
		/// <summary>
		/// beforeInsert
		/// </summary>
		/// <returns></returns>
		protected override bool beforeInsert()
		{
			//this.NoticeNo=EnDA.GenerOID().ToString();			
			return base.beforeInsert();			
		}
		/// <summary>
		/// ����Ҫ���Ĺ�����,��1,�����չ������������.������Ϣ.
		/// �ǲ���һ��.
		/// 1, ��˰��, ������. Ʊ�ݺ���. �ǲ�������һ����¼.
		/// ���û�о��׳��쳣.
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{
			//ȡ������num.
			NumCheck nc = new NumCheck(this.OID);
			//	nc.Num ;
			return base.beforeUpdate(); 
		}
		/// <summary>
		/// beforeDelete
		/// </summary>
		/// <returns></returns>
		protected override bool beforeDelete()
		{
			return base.beforeDelete(); 
		}
		#endregion 	
	}
	/// <summary>
	/// �ڵ����ع���
	/// </summary>
	public class NodeRefFuncs: EntitiesOID
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new NodeRefFunc();
			}
		}
		/// <summary>
		/// ��ع���
		/// </summary>
		public NodeRefFuncs(){}

        public NodeRefFuncs(Node nd)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeRefFuncAttr.NodeId, nd.NodeID);
            if (nd.IsStartNode)
            {
                qo.addOr();
                qo.AddWhere(NodeRefFuncAttr.NodeId, 0);
            }
            qo.DoQuery();
        }
		/// <summary>
		/// ��ع���
		/// </summary>
		/// <param name="flowNo">flowNo</param>
        public NodeRefFuncs(string flowNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhereInSQL(NodeRefFuncAttr.NodeId, "SELECT NodeID from wf_node where fk_flow='" + flowNo + "' ");
            qo.DoQuery();
        }
		#endregion
	}
	
}
