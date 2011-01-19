using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;


namespace BP.Web.Port.Xml
{
    public class MenuLeftAttr
    {
        /// <summary>
        /// ���
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// HelpFile
        /// </summary>
        public const string Url = "Url";
    }
	/// <summary>
	/// 
	/// </summary>
	public class MenuLeft:XmlEnNoName
	{
		#region ����
        public string Url
        {
            get
            {
                return this.GetValStringByKey(MenuLeftAttr.Url);
            }
        }
		#endregion

		#region ����
		public MenuLeft()
		{
		}
		/// <summary>
		/// ���
		/// </summary>
		/// <param name="no"></param>
		public MenuLeft(string no)
		{

		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new MenuLefts();
			}
		}
		#endregion

		#region  ��������
		 
		#endregion
	}
	/// <summary>
	/// 
	/// </summary>
	public class MenuLefts:XmlEns
	{
		#region ����
		/// <summary>
		/// �����ʵ�����Ԫ��
		/// </summary>
        public MenuLefts() { }
		#endregion

		#region ��д�������Ի򷽷���
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override XmlEn GetNewEntity
		{
			get
			{
				return new MenuLeft();
			}
		}
		public override string File
		{
			get
			{
				return  SystemConfig.PathOfXML+"\\Menu.xml";
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
				return null; 
			}
		}
		#endregion
		 
	}
}
