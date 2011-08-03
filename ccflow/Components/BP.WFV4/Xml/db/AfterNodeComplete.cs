using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.XML;


namespace BP.WF.XML
{
	public class AfterNodeCompleteAttr
	{
		/// <summary>
		/// �ڵ�
		/// </summary>
		public const string FK_Node="FK_Node";
		/// <summary>
		/// ����
		/// </summary>
		public const string SQL="SQL";
	}
	public class AfterNodeComplete:XmlEn
	{
		#region ����
        /// <summary>
        /// flow
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(AfterNodeCompleteAttr.FK_Node);
            }
        }
        /// <summary>
        /// sql
        /// </summary>
		public string SQL
		{
			get
			{
				return this.GetValStringByKey(AfterNodeCompleteAttr.SQL);
			}
		}
		#endregion

		#region ����
		public AfterNodeComplete()
		{
		}
		/// <summary>
		/// ���
		/// </summary>
		/// <param name="no"></param>
        public AfterNodeComplete(string no)
        {
        }
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new AfterNodeCompletes();
			}
		}
		#endregion

		#region  ��������
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class AfterNodeCompletes:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
		public AfterNodeCompletes(){}

        /// <summary>
        /// AfterNodeCompletes
        /// </summary>
        /// <param name="nodeID"></param>
        public AfterNodeCompletes(int nodeID)
        {
            this.RetrieveBy(AfterNodeCompleteAttr.FK_Node, nodeID);
        }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new AfterNodeComplete();
			}
		}
        /// <summary>
        /// �ļ�
        /// </summary>
        public override string File
        {
            get
            {
                return SystemConfig.PathOfXML + "\\AfterNodeComplete.xml";
            }
        }
		/// <summary>
		/// �������
		/// </summary>
		public override string TableName
		{
			get
			{
				return "Item";
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
