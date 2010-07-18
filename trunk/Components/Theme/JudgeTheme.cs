using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �ж���attr
	/// </summary>
	public class JudgeThemeAttr:EntityNoNameAttr
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string FK_ThemeSort="FK_ThemeSort";
		/// <summary>
		/// �Ƿ���ȷ
		/// </summary>
		public const string IsOk="IsOk";

	}
	/// <summary>
	/// �ж���
	/// </summary>
	public class JudgeTheme :ChoseBase
	{
		public string NameHtml
		{
			get
			{
				return "<b><font color=blue>"+this.Name+"</font></b>";
			}
		}
        public string FK_ThemeSort
        {
            get
            {
                return this.GetValStringByKey(JudgeThemeAttr.FK_ThemeSort);
            }
            set
            {
                this.SetValByKey(JudgeThemeAttr.FK_ThemeSort, value);
            }
        }

		#region attr
		/// <summary>
		/// �Ƿ���ȷ
		/// </summary>
		public bool IsOk
		{
			get
			{
				return this.GetValBooleanByKey(JudgeThemeAttr.IsOk);
			}
			set
			{
				this.SetValByKey(JudgeThemeAttr.IsOk,value);
			}
		}
		public string isOkOfStr
		{
			get
			{
				if (this.IsOk)
					return "��ȷ";
				else
					return "����";
			}
		}

		public int IsOkOfInt
		{
			get
			{
				return this.GetValIntByKey(JudgeThemeAttr.IsOk);
			}
			set
			{
				this.SetValByKey(JudgeThemeAttr.IsOk,value);
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
				
				Map map = new Map("GTS_JudgeTheme");
				map.EnDesc="�ж���";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;
				map.AddTBStringPK(JudgeThemeAttr.No,null,"���",true,true,0,50,20);
				map.AddTBString(JudgeThemeAttr.Name,null,"����",true,false,0,600,600);
				map.AddDDLEntities(JudgeThemeAttr.FK_ThemeSort,"0001","�ж�������",new ThemeSorts(),true);
				map.AddBoolean(JudgeThemeAttr.IsOk,true,"��ȷ",true,false);
 
				map.AddSearchAttr( JudgeThemeAttr.FK_ThemeSort);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region ���췽��
		protected override void afterDelete()
		{
			this.DeleteHisRefEns();
			base.afterDelete ();
		}
		/// <summary>
		/// �ж���
		/// </summary> 
		public JudgeTheme()
		{
		}
		/// <summary>
		/// �ж���
		/// </summary>
		/// <param name="_No"> No</param> 
		public JudgeTheme(string _No ):base(_No)
		{
		}
		#endregion 

		#region �߼�����
		#endregion

	 
	}
	/// <summary>
	/// �ж���
	/// </summary>
	public class JudgeThemes :EntitiesNoName
	{
		public int Search(string sort, string fk_emp, int sxcd)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(FillBlankAttr.FK_ThemeSort,sort);
			qo.addAnd();
			if (sxcd==4)
				qo.AddWhereNotInSQL(FillBlankAttr.No, "SELECT FK_Theme FROM GTS_Study WHERE FK_Emp='"+fk_emp+"' AND FK_ThemeType='"+ThemeType.JudgeTheme+"'"  );
			else
				qo.AddWhereInSQL(FillBlankAttr.No, "SELECT FK_Theme FROM GTS_Study WHERE FK_Emp='"+fk_emp+"' AND FK_ThemeType='"+ThemeType.JudgeTheme+"' AND SXCD="+sxcd );
			return qo.DoQuery();
		}

		/// <summary>
		/// JudgeThemes
		/// </summary>
		public JudgeThemes(){}

		public JudgeThemes(string sort)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(FillBlankAttr.FK_ThemeSort,sort);
			qo.DoQuery();
		}

		/// <summary>
		/// JudgeTheme
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new JudgeTheme();
			}
		}
	}
}
