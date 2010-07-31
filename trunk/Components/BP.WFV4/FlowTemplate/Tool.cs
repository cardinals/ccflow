using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;


namespace BP.CRM
{
	public class Tool:XmlEnNoName
	{
		#region ����
        public string No
        {
            get
            {
                return this.GetValStringByKey("No");
            }
        }
 
		/// <summary>
		/// ͼƬ
		/// </summary>
		public string Img
		{
			get
			{
				return  this.GetValStringByKey("Img") ;
			}
		}
        public string Title
        {
            get
            {
                return this.GetValStringByKey("Title");
            }
        }
        public string Url
        {
            get
            {
                return this.GetValStringByKey("Url");
            }
        }
		#endregion

		#region ����
		/// <summary>
		/// �ڵ���չ��Ϣ
		/// </summary>
		public Tool()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new Tools();
			}
		}
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class Tools:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
        public Tools() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new Tool();
			}
		}
		public override string File
		{
			get
			{
                return SystemConfig.PathOfWebApp + "\\FlowTemplate\\Style\\Tools.xml";
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
