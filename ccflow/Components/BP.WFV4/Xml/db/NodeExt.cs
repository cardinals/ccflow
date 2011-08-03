using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.XML;


namespace BP.WF.XML
{
	public class NodeExtAttr
	{
		/// <summary>
		/// �ڵ�
		/// </summary>
		public const string NodeEnName="NodeEnName";
		/// <summary>
		/// �Ƿ��������Ͱ�ť
		/// </summary>
		public const string EnableSendBtn="EnableSendBtn";
		/// <summary>
		/// ��ѡ��ʱ��
		/// </summary>
		public const string OnSelectedMsg="OnSelectedMsg";
		/// <summary>
		/// ���½�ʱ��
		/// </summary>
		public const string OnNew="OnNew";
		/// <summary>
		/// �ڱ���
		/// </summary>
		public const string OnSaveMsg="OnSaveMsg";
		/// <summary>
		/// ����
		/// </summary>
		public const string RptMsg="RptMsg";
		/// <summary>
		/// ��ײ���Ϣ
		/// </summary>
		public const string WorkEndInfo="WorkEndInfo";
        public const string EnableReturnBtn = "EnableReturnBtn";
	}
	public class NodeExt:XmlEn
	{
		#region ����
		/// <summary>
		/// �ڵ�
		/// </summary>
		public string NodeEnName
		{
			get
			{
				return  this.GetValStringByKey(NodeExtAttr.NodeEnName) ;
			}
		}
        public string EnableReturnBtnStr
        {
            get
            {
                string msg = this.GetValStringByKey(NodeExtAttr.EnableReturnBtn);
                return msg;
            }
        }
        public string EnableSendBtnStr
        {
            get
            {
                string msg = this.GetValStringByKey(NodeExtAttr.EnableSendBtn);
                return msg;
            }
        }
		/// <summary>
		/// �Ƿ��������Ͱ�ť
		/// </summary>
		public bool EnableSendBtn
		{
            get
            {
                string msg = this.GetValStringByKey(NodeExtAttr.EnableSendBtn);
                if (msg == "0")
                    return false;
                else
                    return true;
            }
		}
		/// <summary>
		/// ��ѡ��ʱ��
		/// </summary>
		public string OnSelectedMsg
		{
			get
			{
				return  this.GetValStringByKey(NodeExtAttr.OnSelectedMsg) ;
			}
		}
		/// <summary>
		/// ���½�ʱ��
		/// </summary>
		public string OnNew
		{
			get
			{
				return  this.GetValStringByKey(NodeExtAttr.OnNew) ;
			}
		}
		/// <summary>
		/// �ڱ���
		/// </summary>
		public string OnSaveMsg
		{
			get
			{
				return this.GetValStringByKey(NodeExtAttr.OnSaveMsg);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
        public string RptMsg
        {
            get
            {
                return this.GetValStringByKey(NodeExtAttr.RptMsg);
            }
        }
		/// <summary>
		/// ��ײ���Ϣ
		/// </summary>
		public string WorkEndInfo
		{
			get
			{
				return this.GetValStringByKey(NodeExtAttr.WorkEndInfo);
			}
		}
		#endregion

		#region ����
		/// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
		public NodeExt()
		{
		}
		/// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
		/// <param name="enName">�������˵㣬�������ǽڵ�ID��</param></param>
		public NodeExt(string enName)
		{
			this.RetrieveByPK(NodeExtAttr.NodeEnName,enName);
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new NodeExts();
			}
		}
		#endregion

		#region  ��������
		 
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class NodeExts:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
		public NodeExts(){}
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new NodeExt();
			}
		}
		public override string File
		{
			get
			{
				return  SystemConfig.PathOfXML+"\\Node\\";
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
				return "Node";
			}
		}
		public override Entities RefEns
		{
			get
			{
				return null; //new BP.ZF1.AdminTools();
			}
		}
		#endregion
		 
	}
}
