using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.OA
{
	/// <summary>
	/// ��������
	/// </summary>
    public class LinkAttr : EntityNoNameAttr
    {
        /// <summary>
        /// �ظ���Ϣ
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// ��¼��
        /// </summary>
        public const string Url = "Url";
        /// <summary>
        /// �ļ���
        /// </summary>
        public const string Target = "Target";
    }
	/// <summary>
	/// ����
	/// </summary> 
	public class Link : EntityNoName
	{
		#region ��������
        public string Target
        {
            get
            {
                return this.GetValStringByKey(LinkAttr.Target);
            }
            set
            {
                SetValByKey(LinkAttr.Target, value);
            }
        }
		public string Note
		{
			get
			{
				return this.GetValStringByKey(LinkAttr.Note);
			}
			set
			{
				SetValByKey(LinkAttr.Note,value);
			}
		}
		public string Url
		{
			get
			{
                return this.GetValStringByKey(LinkAttr.Url); 
			}
			set
			{
				SetValByKey(LinkAttr.Url,value);
			}
		}
		#endregion
 
		#region ���캯��
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenAll();
				return uac;
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		public Link()
		{
		  
		}
		/// <summary>
		/// Map
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("TA_Link");
                map.EnDesc = "����";
                map.CodeStruct = "3";
                map.IsAutoGenerNo = true;
                //map.Icon="./Images/Link_s.ico";
                //map.Icon = "../TA/Images/Link_s.ico";

                map.AddTBStringPK(LinkAttr.No, null, "���", true, true, 3, 3, 3);
                map.AddTBString(LinkAttr.Name, null, "����", true, false, 0, 50, 10);
                map.AddTBString(LinkAttr.Url, null, "URL", true, false, 0, 50, 10);
                map.AddDDLSysEnum(LinkAttr.Target, 0, "Ŀ��", true, true, LinkAttr.Target, "@0=�´���@1=������@2=������");
                map.AddTBString(LinkAttr.Note, null, "˵��", true, false, 0, 50, 10);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 
	}
	/// <summary>
	/// ����s
	/// </summary> 
	public class Links: Entities
	{
		/// <summary>
		/// ��ȡentity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Link();
			}
		}
		public override int RetrieveAll()
		{
            int i = base.RetrieveAll();
            if (i == 0)
            {
                Link lk = new Link();
                lk.Name = "�۳ҹ���������";
                lk.Url = "http://ccflow.cn";
                lk.Note = "ת���۳ҹ�����������ҳ�������Ի�ȡ���̰�����";
                lk.No = lk.GenerNewNo;
                lk.Insert();

                lk = new Link();
                lk.Name = "ftp������";
                lk.Url = "../../OA/Do.aspx?DoType=GotoFtp";
                lk.Note = "ת���ڲ�����ftp�������ϡ�";
                lk.No = lk.GenerNewNo;
                lk.Insert();

                lk = new Link();
                lk.Name = "����";
                lk.Url = "http://sina.com.cn";
                lk.Note = "�������ţ��ʼ�����ѯ���ƾ���";
                lk.No = lk.GenerNewNo;
                lk.Insert();

                lk = new Link();
                lk.Name = "�Ѻ�";
                lk.Url = "http://shou.com.cn";
                lk.Note = "�Ѻ����ţ��ʼ�����ѯ���ƾ���";
                lk.No = lk.GenerNewNo;
                lk.Insert();


                lk = new Link();
                lk.Name = "�ȸ�����";
                lk.Url = "http://google.com.hk";
                lk.Note = "�ȸ����������롢gtalk��gmail��";
                lk.No = lk.GenerNewNo;
                lk.Insert();

                lk = new Link();
                lk.Name = "�ٶ�";
                lk.Url = "http://baidu.com";
                lk.Note = "�ٶ����������͡�";
                lk.No = lk.GenerNewNo;
                lk.Insert();
            }
            return i;
		}
		/// <summary>
		/// Links
		/// </summary>
		public Links()
		{
		}
		/// <summary>
		/// Links
		/// </summary>
		public Links(string Url)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(LinkAttr.Url, Url);
			qo.DoQuery();			
		}
	}
}
 