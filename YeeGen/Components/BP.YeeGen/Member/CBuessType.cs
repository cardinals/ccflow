using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.YG
{
	public class BBuessAttr:EntityNoNameAttr
	{
		/// <summary>
		/// ������
		/// </summary>
		public const string Cent="Cent";
		/// <summary>
		/// ����Ա
		/// </summary>
		public const string Note="Note";
	}
	/// <summary>
	/// ҵ������
	/// </summary>
	public class CBuessType :EntityNoName
	{
		#region  �ճ�����
		/// <summary>
		/// �����ļ�
		/// </summary>
		public const string FDB_Down="FDB_Down";
		/// <summary>
		/// �ٱ��ļ�
		/// </summary>
		public const string FDB_QBJL="FDB_QBJL";
		#endregion �ճ�����

		#region  �ճ�����
		/// <summary>
		/// �ṩ����
		/// </summary>
		public const string CZ_Advices="CZ_Advices";
		/// <summary>
		/// ����ϵͳ����
		/// </summary>
		public const string CZ_Debug="CZ_Debug";
		/// <summary>
		/// ��¼ϵͳ
		/// </summary>
		public const string CZ_Login="CZ_Login";
		/// <summary>
		/// ע���Ա
		/// </summary>
		public const string CZ_Reg="CZ_Reg";
		#endregion �ճ�����

		#region FAQ
		/// <summary>
		/// �ش���˵�����
		/// </summary>
		public const string FAQ_Answer="FAQ_Answer";
		/// <summary>
		/// �ش�������
		/// </summary>
		public const string FAQ_AnswerFK="FAQ_AnswerFK";
		/// <summary>
		/// �𰸱����˲���
		/// </summary>
		public const string FAQ_AnswerOK="FAQ_AnswerOK";
		/// <summary>
		/// ��������
		/// </summary>
		public const string FAQ_Ask="FAQ_Ask";
		/// <summary>
		/// ���⳷������
		/// </summary>
		public const string FAQ_AskFK="FAQ_AskFK";
		#endregion

		#region �ļ�����
		/// <summary>
		/// ���ؼӷ�
		/// </summary>
		public const string SF_Down="SF_Down";
		/// <summary>
		/// �ļ����ٱ�
		/// </summary>
		public const string SF_QBFK="SF_QBFK";
		/// <summary>
		/// �ٱ�����
		/// </summary>
		public const string SF_QBJL="SF_QBJL";
		/// <summary>
		/// �ļ�����Ŀ
		/// </summary>
		public const string SF_SL="SF_SL";
		/// <summary>
		/// �ϴ��ļ�
		/// </summary>
		public const string SF_Upload="SF_Upload";
        /// <summary>
        /// ����ԭ���ļ�
        /// </summary>
        public const string YC_Read = "YC_Read";
		#endregion 

		#region Post
		/// <summary>
		/// ����Υ������
		/// </summary>
		public const string Post_Del="Post_Del";
		/// <summary>
		/// ���ӱ��ö�
		/// </summary>
		public const string Post_Up="Post_Up";
		#endregion


		#region ���·���
		/// <summary>
		/// ��������
		/// </summary>
		public const string WZ_FB="WZ_FB";
		/// <summary>
		/// Υ�淢������
		/// </summary>
		public const string WZ_FBFK="WZ_FBFK";
		/// <summary>
		/// ����¼��
		/// </summary>
		public const string WZ_FBOK="WZ_FBOK";
		#endregion

		#region cent .
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(BBuessAttr.Cent);
			}
			set
			{
				this.SetValByKey(BBuessAttr.Cent,value);
			}
		}
		public string Note
		{
			get
			{
				return this.GetValStringByKey(BBuessAttr.Note);
			}
			set
			{
				this.SetValByKey(BBuessAttr.Note,value);
			}
		}
		#endregion


		#region ʵ�ֻ����ķ���
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
		/// FLinkMap
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
                map.PhysicsTable = "YG_CBuessType";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "ҵ������";
                map.EnType = EnType.App;
                map.AddTBString("Sort", null, "���", true, false, 1, 200, 10);
                map.AddTBStringPK(BBuessAttr.No, null, "���", true, false, 1, 10, 10);
                map.AddTBString(BBuessAttr.Name, null, "����", true, false, 1, 200, 10);
                map.AddTBInt(BBuessAttr.Cent, 0, "�ۼӷ�", true, false);
                map.AddTBString(BBuessAttr.Note, null, "˵��", true, false, 1, 200, 10);

                #endregion

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// ҵ������
		/// </summary>
		public CBuessType(){}
		/// <summary>
		/// ҵ������
		/// </summary>
		/// <param name="_No">���</param>
		public CBuessType(string _No ): base(_No){}
		#endregion 
	}
	/// <summary>
	/// ҵ������
	/// </summary>
	public class CBuessTypes :Entities
	{
		#region ����
		/// <summary>
		/// ҵ������s
		/// </summary>
		public CBuessTypes(){}
		/// <summary>
		/// ҵ������
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CBuessType();
			}
		}
		#endregion
	}
}
