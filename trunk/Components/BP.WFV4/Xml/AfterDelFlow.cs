using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.XML;


namespace BP.WF.XML
{
	public class AfterDelFlowAttr
	{
		/// <summary>
		/// ���
		/// </summary>
		public const string FK_Flow="FK_Flow";
		/// <summary>
		/// ����
		/// </summary>
		public const string SQL="SQL";
	}
	public class AfterDelFlow:XmlEn
	{
		#region ����
        /// <summary>
        /// flow
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(AfterDelFlowAttr.FK_Flow);
            }
        }
        /// <summary>
        /// sql
        /// </summary>
		public string SQL
		{
			get
			{
				return this.GetValStringByKey(AfterDelFlowAttr.SQL);
			}
		}
		#endregion

		#region ����
		public AfterDelFlow()
		{
		}
		/// <summary>
		/// ���
		/// </summary>
		/// <param name="no"></param>
        public AfterDelFlow(string no)
        {
        }
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new AfterDelFlows();
			}
		}
		#endregion

		#region  ��������
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class AfterDelFlows:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
		public AfterDelFlows(){}

		public AfterDelFlows(string flow)
		{
			this.RetrieveBy(AfterDelFlowAttr.FK_Flow, flow);
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
				return new AfterDelFlow();
			}
		}
		public override string File
		{
			get
			{
				return  SystemConfig.PathOfXML+"\\AfterDelFlow.xml";
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
