using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;
using BP.YG;

namespace BP.SF
{
	/// <summary>
	/// �ļ����
	/// </summary>
	public class SFSort1Attr: EntityNoNameAttr
	{
		#region ��������
        public const string ImgUrl = "ImgUrl";
        public const string Note = "Note";
        public const string FK_Sort1 = "FK_Sort1";
		#endregion
	}
	/// <summary>
    /// �ļ����
	/// </summary>
	public class SFSort1 :EntityNoName
	{	
		#region ��������
        public string Dtls
        {
            get
            {
                string dtls = "";
                SFSorts ens = new SFSorts();
                QueryObject qo = new QueryObject(ens);
                qo.AddWhere(SFSort1Attr.FK_Sort1, this.No);
                qo.DoQuery();

                foreach (SFSort en in ens)
                {
                    dtls += "<a href='ShareFile.aspx?FK_Sort=" + en.No + "' >" + en.Name + "</a>��";
                }
                return dtls;
            }
        }
        public string ImgUrl
        {
            get
            {
                return "/Img/Dots/Red.gif";
            }
        }
        public string ImgUrl_S
        {
            get
            {
                return "/img/Dots/red.gif";
            }
        }
		#endregion 

		#region ���캯��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
		/// <summary>
		/// �ļ����
		/// </summary>		
		public SFSort1(){}
		public SFSort1(string no):base(no)
		{
		}
		/// <summary>
		/// SFSort1Map
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map();

                #region ��������
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
                map.PhysicsTable = "SF_Sort1";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.EnDesc = "�ļ����";
                map.EnType = EnType.App;
                #endregion

                #region �ֶ�
                map.AddTBStringPK(SFSort1Attr.No, null, "���", true, false, 2, 2, 50);
                map.AddTBString(SFSort1Attr.Name, null, "����", true, false, 0, 50, 200);
                map.AddTBString(SFSort1Attr.Note, null, "Note", true, false, 0, 50, 200);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion
		 
	}
	/// <summary>
	/// �ļ����
	/// </summary>
    public class SFSort1s : EntitiesNoName
    {
        #region ��д
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new SFSort1();
            }
        }
        #endregion

        #region ���췽��
        public SFSort1s()
        {
        }
        #endregion
    }
	
}
