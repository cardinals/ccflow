using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �Ķ����attr
	/// </summary>
	public class RCAttr:EntityNoNameAttr
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string FK_ThemeSort="FK_ThemeSort";
		/// <summary>
		/// ThemeNum
		/// </summary>
		public const string ThemeNum="ThemeNum";

	}
	/// <summary>
	/// �Ķ����
	/// </summary>
	public class RC :ChoseBase
	{
		public string NameHtml
		{
			get
			{
				return "<b><font color=blue>"+this.GetValHtmlStringByKey("Name")+"</font></b>";
			}
		}

		#region ����
		public RCDtls HisRCDtls
		{
			get
			{
				return new RCDtls(this.No);
			}
		}
		/// <summary>
		/// FK_ThemeSort
		/// </summary>
		public string FK_ThemeSort
		{
			get
			{
				return this.GetValStringByKey(RCAttr.FK_ThemeSort);
			}
			set
			{
				this.SetValByKey(RCAttr.FK_ThemeSort,value);
			}
		}
		/// <summary>
		/// ThemeNum
		/// </summary>
		public int ThemeNum
		{
			get
			{
				return this.GetValIntByKey(RCAttr.ThemeNum);
			}
			set
			{
				this.SetValByKey(RCAttr.ThemeNum,value);
			}
		}
		#endregion

	 
		#region ʵ�ֻ����ķ���

		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.OpenForSysAdmin();
				return uc;
			}
		}

		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("GTS_RC");
				map.EnDesc="�Ķ����";
				map.CodeStruct="5";

				map.EnType= EnType.Admin;
				map.AddTBStringPK(RCAttr.No,null,"���",true,true,0,50,20);
				map.AddTBString(RCAttr.Name,null,"��������",true,false,0,5000,600);
				map.AddDDLEntities(RCAttr.FK_ThemeSort,"0001","��Ŀ����",new ThemeSorts(),true);
				map.AddTBInt(RCAttr.ThemeNum,0,"С����",true,true);

				map.AddSearchAttr( RCAttr.FK_ThemeSort);
				map.AddDtl(new RCDtls(),RCDtlAttr.FK_RC);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// �Ķ����
		/// </summary> 
		public RC()
		{
		}
		/// <summary>
		/// �Ķ����
		/// </summary>
		/// <param name="_No">���ջ��ر��</param> 
		public RC(string _No ):base(_No)
		{
		}
		#endregion

		#region �߼�����
		protected override void afterDelete()
		{
			this.DeleteHisRefEns();
			base.afterDelete ();
		}
		protected override void afterInsert()
		{
			RCDtl ctA = new RCDtl();
			ctA.FK_RC = this.No;
			ctA.Insert();

			RCDtl ctB = new RCDtl();
			ctB.FK_RC = this.No;
			ctB.Insert();

			RCDtl ctC = new RCDtl();
			ctC.FK_RC = this.No;
			ctC.Insert();

			RCDtl ctD = new RCDtl();
			ctD.FK_RC = this.No;
			ctD.Insert();
			base.afterInsert ();
		}

		protected override bool beforeUpdateInsertAction()
		{
			this.ThemeNum = DBAccess.RunSQLReturnValInt("select count(*) from GTS_RCDtl where fk_RC='"+this.No+"'");
			return base.beforeUpdateInsertAction ();
		}

		#endregion

	 
	}
	/// <summary>
	/// ��˰���ջ���
	/// </summary>
	public class RCs :EntitiesNoName
	{
		public int Search(string sort, string fk_emp, int sxcd)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(FillBlankAttr.FK_ThemeSort,sort);
			qo.addAnd();
			if (sxcd==4)
				qo.AddWhereNotInSQL(FillBlankAttr.No, "SELECT FK_Theme FROM GTS_Study WHERE FK_Emp='"+fk_emp+"' AND FK_ThemeType='"+ThemeType.RC+"'"  );
			else
				qo.AddWhereInSQL(FillBlankAttr.No, "SELECT FK_Theme FROM GTS_Study WHERE FK_Emp='"+fk_emp+"' AND FK_ThemeType='"+ThemeType.RC+"' AND SXCD="+sxcd );
			return qo.DoQuery();
		}
		public RCs(string sort)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(FillBlankAttr.FK_ThemeSort,sort);
			qo.DoQuery();
		}
		/// <summary>
		/// RCs
		/// </summary>
		public RCs(){}
		/// <summary>
		/// RC
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new RC();
			}
		}
	}
}
