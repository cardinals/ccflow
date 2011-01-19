using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;
using BP.YG;

namespace BP.SF
{
	/// <summary>
	/// �ϴ������ļ�
	/// </summary>
	public class ShareFileAttr: EntityOIDAttr
	{
		#region ��������
		/// <summary>
		/// �ļ�����
		/// </summary>
		public const  string NameOfShort="NameOfShort";
		/// <summary>
		/// �߼��ļ���
		/// </summary>
		public const  string NameOfFull="NameOfFull";
		/// <summary>
		/// �ļ�·��
		/// </summary>
		public const string FilePath="FilePath";
		/// <summary>
		/// ��չ
		/// </summary>
		public const string Ext="Ext";
		/// <summary>
		/// �ļ����
		/// </summary>
		public const string Doc="Doc";
		 
		/// <summary>
		/// ���ش���
		/// </summary>
		public const string DownTimes="DownTimes";
		/// <summary>
		/// �ϴ�����
		/// </summary>
		public const string FK_Member="FK_Member";
		/// <summary>
		/// 
		/// </summary>
		public const string FK_Sort="FK_Sort";
		/// <summary>
		/// ������ֵ(Ĭ��0)
		/// </summary>
		public const string PayCent="PayCent";
		public const string FSize="FSize";
		public const string RDT="RDT";
		public const string Income="Income";
		#endregion
	}
	/// <summary>
	/// �ϴ������ļ�
	/// </summary>
	public class ShareFile :EntityOID
	{	
		#region ��������
		/// <summary>
		/// �ļ���
		/// </summary>
		public string NameOfShortHtml
		{

			get
			{
				return this.ExtHtml+this.GetValStringByKey(ShareFileAttr.NameOfShort);
			}
			set
			{
				this.SetValByKey(ShareFileAttr.NameOfShort,value);
			}
		}
		public string NameOfS
		{
			get
			{
				return this.GetValStringByKey(ShareFileAttr.NameOfShort);
			}
			set
			{
				this.SetValByKey(ShareFileAttr.NameOfShort,value);
			}
		}
        public string Ext
        {
            get
            {
                return this.GetValStringByKey(ShareFileAttr.Ext);
            }
            set
            {
                this.SetValByKey(ShareFileAttr.Ext, value);
            }
        }
        public string ExtHtml
        {
            get
            {
                return "<img src='/Images/System/FileType/" + this.Ext + ".gif' border=0 />";
            }
        }
        public string NameOfIcon
        {
            get
            {
                string s = this.GetValStringByKey(ShareFileAttr.NameOfShort);
                if (s.Length > 30)
                    s = s.Substring(0, 25) + "...";
                return this.ExtHtml + s; //this.GetValStringByKey(ShareFileAttr.NameOfShort);
            }
        }
		public string NameOfShort
		{
			get
			{
				string s =this.GetValStringByKey(ShareFileAttr.NameOfShort);
				if (s.Length >15)
					s=s.Substring(0,13)+"...";
				return this.ExtHtml+s; //this.GetValStringByKey(ShareFileAttr.NameOfShort);
			}
			set
			{
				this.SetValByKey(ShareFileAttr.NameOfShort,value);
			}
		}
		public string NameOfFull
		{
			get
			{
				return this.GetValStringByKey(ShareFileAttr.NameOfFull);
			}
			set
			{
				this.SetValByKey(ShareFileAttr.NameOfFull,value);
			}
		}
		
        public string FK_Sort
        {
            get
            {
                return this.GetValStrByKey(ShareFileAttr.FK_Sort);
            }
            set
            {
                this.SetValByKey(ShareFileAttr.FK_Sort,value);
            }
        }
        public string FK_SortText
        {
            get
            {
                return this.GetValRefTextByKey(ShareFileAttr.FK_Sort);
            }
        }
		public string FK_Member
		{
			get
			{
				return this.GetValStringByKey(ShareFileAttr.FK_Member);
			}
			set
			{
				this.SetValByKey(ShareFileAttr.FK_Member,value);
			}
		}
		public string FK_MemberH
		{
			get
			{
				return Glo.GenerMemberStr( this.GetValStringByKey(ShareFileAttr.FK_Member));
			}
			 
		}
		public string RDT
		{
			get
			{
				return this.GetValStringByKey(ShareFileAttr.RDT).Substring(5,6);
			}
			set
			{
				this.SetValByKey(ShareFileAttr.RDT,value);
			}
		}
		public string Doc
		{
			get
			{
				return this.GetValStringByKey(ShareFileAttr.Doc);
			}
			set
			{
				this.SetValByKey(ShareFileAttr.Doc,value);
			}
		}
		public int DownTimes
		{
			get
			{
				return this.GetValIntByKey(ShareFileAttr.DownTimes);
			}
			set
			{
				this.SetValByKey(ShareFileAttr.DownTimes,value);
			}
		}
		public int Income
		{
			get
			{
				return this.GetValIntByKey(ShareFileAttr.Income);
			}
			set
			{
				this.SetValByKey(ShareFileAttr.Income,value);
			}
		}
		public string IncomeStr
		{
			get
			{
				return this.Income+"<img src='../Img/Cent.gif' border=0 />" ;
			}
		}
		public string IncomeStrBig
		{
			get
			{
				return this.Income +"<img src='../Img/CentOFbig.gif' border=0 />";
			}
		}
		public string PayCentStr
		{
			get
			{
				return "<font color=red><B>"+this.PayCent+"</b></font><img src='../img/cent.gif' border=0 />";
			}
		}
		public int PayCent
		{
			get
			{
				return this.GetValIntByKey(ShareFileAttr.PayCent);
			}
			set
			{
				this.SetValByKey(ShareFileAttr.PayCent,value);
			}
		}
		public int FSize
		{
			get
			{
				return this.GetValIntByKey(ShareFileAttr.FSize)/1000;
			}
			set
			{
				this.SetValByKey(ShareFileAttr.FSize,value);
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
                uac.IsInsert = false;
				return uac;
			}
		}
		/// <summary>
		/// �ϴ������ļ�
		/// </summary>		
		public ShareFile(){}

		public ShareFile(int oid):base(oid)
		{
		}
        protected override string SerialKey
        {
            get
            {
                return "SF";
            }
        }
		/// <summary>
		/// ShareFileMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map();

				#region �������� 
				map.EnDBUrl =new DBUrl(DBUrlType.AppCenterDSN) ; 
				map.PhysicsTable="SF_File";  
				map.AdjunctType = AdjunctType.AllType ;  
				map.DepositaryOfMap=Depositary.Application; 
				map.DepositaryOfEntity=Depositary.None; 
				map.IsAllowRepeatNo=false;
				map.IsCheckNoLength=false;
				map.EnDesc="�ϴ������ļ�";
				map.EnType=EnType.App;
				#endregion

				#region ���� 
				map.AddTBIntPKOID();

                map.AddTBString(ShareFileAttr.NameOfShort, null, "�ļ�����", true, false, 0, 500, 200);
                map.AddTBString(ShareFileAttr.NameOfFull, null, "�����ļ�����", false, false, 0, 500, 200);
                map.AddTBString(ShareFileAttr.Ext, null, "��׺", false, false, 0, 50, 50);
				map.AddTBString(ShareFileAttr.FK_Member,null,"�ϴ���",true,false,0,50,200);
				map.AddDDLEntities(ShareFileAttr.FK_Sort,null,"�ļ�����", new SFSorts(), false);
                map.AddTBString(ShareFileAttr.Doc, null, "���", false, false, 0, 500, 500);


				map.AddTBDate(ShareFileAttr.RDT,"����",true,false);
				map.AddTBInt(ShareFileAttr.FSize,0,"�ļ���С",true,false);
				map.AddTBInt(ShareFileAttr.DownTimes,0,"���ش���",true,false);
				map.AddTBInt(ShareFileAttr.PayCent,1,"������ֵ",true,false);
				map.AddTBInt(ShareFileAttr.Income,1,"����",true,false);
				#endregion

				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			this.Income = this.DownTimes*this.PayCent+5;
			this.FK_Member=Glo.MemberNo;
			return base.beforeUpdateInsertAction ();
		}
		/// <summary>
		/// ɾ������ļ�
		/// </summary>
        public void DeleteIt()
        {
            // �����ķ֡�
            Glo.TradeAdmin(this.FK_Member, CBuessType.SF_QBFK, this.OID, "�ļ�:" + this.NameOfShort + "������Աɾ��");

            // ɾ���û��� Favorites
            Favorites f = new Favorites();
            f.Delete(FavoriteAttr.FK_Type, Favorite.TypeOfShareFile,
                FavoriteAttr.RefObj, this.OID.ToString());

        }
        protected override void afterDelete()
        {
            this.DeleteIt();
            base.afterDelete();
        } 
		#endregion

		#region ִ��
        public void DoDownLoad()
        {
            this.DownTimes = this.DownTimes + 1;
            this.Update(ShareFileAttr.DownTimes, this.DownTimes);
            if (this.PayCent == 0)
            {
                this.DoOpen();
            }
            else
            {
                Member c = Glo.Member;
                if (c.Cent < this.PayCent)
                    throw new Exception("<img src='/img/sorry.gif' align=left />���Ļ����Ѿ�������������ļ���<BR><BR>�������õ�����Ļ��֣�����Ҫ�������Լ����ļ����߰������˽�����⡣<BR><BR>������鿴<a href='AboutCent.htm' >���ֹ���</a>��");

                Glo.Trade(CBuessType.SF_Down, this.OID, "����:" + this.NameOfS, - this.PayCent );
                this.DoOpen();
            }
        }
		private void DoOpen()
		{
			System.IO.File.Copy(this.NameOfFull, Glo.PathTemp  +"www.caishui800.cn  "+this.OID+"."+this.Ext,true);
			System.Web.HttpContext.Current.Response.Redirect("/Temp/www.caishui800.cn  "+this.OID+"."+this.Ext);
		}
		#endregion
	}
	/// <summary>
	/// �ϴ������ļ�
	/// </summary>
    public class ShareFiles : EntitiesOID
    {
        #region
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ShareFile();
            }
        }
        #endregion

        #region ���췽��

        public ShareFiles() { }

        public ShareFiles(string fk_custmor)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(ShareFileAttr.FK_Member, fk_custmor);
            qo.addOrderByDesc("OID");
            qo.DoQuery();
        }
        
        #endregion
    }
	
}
