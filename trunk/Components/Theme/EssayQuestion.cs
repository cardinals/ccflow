using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �ʴ���attr
	/// </summary>
	public class EssayQuestionAttr:EntityNoNameAttr
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string FK_ThemeSort="FK_ThemeSort";
		/// <summary>
		/// ��
		/// </summary>
		public const string Answer="Answer";

        /// <summary>
        /// �Ƿ�����������
        /// </summary>
        public const string IsTextInput = "IsTextInput";
        /// <summary>
        /// 
        /// </summary>
        public const string AutoCheckType = "AutoCheckType";
	}
	/// <summary>
	/// �ʴ���
	/// </summary>
	public class EssayQuestion :ChoseBase
	{
        public bool IsTextInput
        {
            get
            {
                return this.GetValBooleanByKey(EssayQuestionAttr.IsTextInput);
            }
            set
            {
                this.SetValByKey(EssayQuestionAttr.IsTextInput, value);
            }
        }

        public string NameHtml
        {
            get
            {
                return "<b><font color=blue>" + this.GetValHtmlStringByKey("Name") + "</font></b>";
            }
        }
        public string NameHtml_Ext
        {
            get
            {
                string s = this.GetValHtmlStringByKey("Name");
                s = s.TrimEnd();
                s = s.Replace("<BR>", "<BR><BR>&nbsp;&nbsp;");
                s = s.Replace(" ", "&nbsp;");
                return s;
            }
        }
       

		#region attr
		/// <summary>
		/// ��
		/// </summary>
		public string Answer
		{
			get
			{
				return this.GetValStringByKey(EssayQuestionAttr.Answer);
			}
			set
			{
				this.SetValByKey(EssayQuestionAttr.Answer,value);
			}
		}
		public string AnswerExt
		{
			get
			{
				return ChoseBase.GenerStr(this.Answer);
				
			}
		}
		public string FK_ThemeSort
		{
			get
			{
				return this.GetValStringByKey(EssayQuestionAttr.FK_ThemeSort);
			}
			set
			{
				this.SetValByKey(EssayQuestionAttr.FK_ThemeSort,value);
			}
		}
		public string AnswerHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(EssayQuestionAttr.Answer);
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
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("GTS_EssayQuestion");
                map.EnDesc = "�ʴ���";
                map.CodeStruct = "5";
                map.EnType = EnType.Admin;
                map.AddTBStringPK(EssayQuestionAttr.No, null, "���", true, true, 0, 50, 20);
                map.AddDDLEntities(EssayQuestionAttr.FK_ThemeSort, "0001", "�ʴ�������", new ThemeSorts(), true);

                map.AddTBStringDoc(EssayQuestionAttr.Name, null, "����", true, false);
                map.AddTBStringDoc(EssayQuestionAttr.Answer, null, "��", true, false);

                map.AddBoolean(EssayQuestionAttr.IsTextInput, false, "�Ƿ�������������", true, true);
                map.AddDDLSysEnum(EssayQuestionAttr.AutoCheckType, "AutoCheckType", 0, "�������", true, false, "AutoCheckType", "@0=������@1=������");

                //map.AddTBStringDoc("Note", null, "Note", true, false);
                map.AddSearchAttr(EssayQuestionAttr.FK_ThemeSort);
                //   map.AddSearchAttr(EssayQuestionAttr.FK_ThemeSort);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

		#region ���췽��
		/// <summary>
		/// �ʴ���
		/// </summary> 
		public EssayQuestion()
		{
		}
		/// <summary>
		/// �ʴ���
		/// </summary>
		/// <param name="_No">���ջ��ر��</param> 
		public EssayQuestion(string _No ):base(_No)
		{
		}
		#endregion 

		#region �߼�����

        /// <summary>
        /// ���÷�
        /// </summary>
        /// <param name="answer">��</param>
        /// <param name="cent">�÷�</param>
        /// <returns></returns>
        public int CheckIt(string answer, decimal cent)
        {
            if (answer.Trim() == "")
                return 0;

            string text = this.Name;
            text = text.Replace(" ", "");
            text = text.Replace("\t\n", "");
            text = text.Replace("\t", "");
            text = text.Replace("\n", "");
            text = text.Replace("\r", "");


            text = text.Replace("��", "@");
            text = text.Replace("��", "@");

            text = text.Replace(",", "@");
            text = text.Replace(".", "@");

            string[] strs = text.Split('@');
            int i = 0;
            int leng = 0;
            foreach (string str in strs)
            {
                if (str == null || str=="")
                    continue;

                if (str.Trim().Length == 0)
                    continue;

                if (answer.Contains(str))
                    i++;

                leng++;
            }

            decimal fz = decimal.Parse(i.ToString());
            decimal fm = decimal.Parse(leng.ToString());
            decimal mycent = decimal.Parse(cent.ToString());


            decimal mycentNUm = decimal.Round(fz / fm * mycent, 0);

            string mystr = mycentNUm.ToString("0.0");
            return int.Parse(mystr.Substring(0, mystr.IndexOf('.')));
        }

		protected override void afterDelete()
		{
			this.DeleteHisRefEns();
			base.afterDelete ();
		}
		#endregion

	 
	}
	/// <summary>
	/// ��˰���ջ���
	/// </summary>
	public class EssayQuestions :EntitiesNoName
	{
		public int Search(string sort, string fk_emp, int sxcd)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(FillBlankAttr.FK_ThemeSort,sort);
			qo.addAnd();
			if (sxcd==4)
			{
				qo.AddWhereNotInSQL(FillBlankAttr.No, "SELECT FK_Theme FROM GTS_Study WHERE FK_Emp='"+fk_emp+"' AND FK_ThemeType='"+ThemeType.EssayQuestion+"'"  );
			}
			else
			{
				qo.AddWhereInSQL(FillBlankAttr.No, "SELECT FK_Theme FROM GTS_Study WHERE FK_Emp='"+fk_emp+"' AND FK_ThemeType='"+ThemeType.EssayQuestion+"' AND SXCD="+sxcd );
			}
			return qo.DoQuery();
		}

		public EssayQuestions(string sort)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(FillBlankAttr.FK_ThemeSort,sort);
			qo.DoQuery();
		}
		/// <summary>
		/// EssayQuestions
		/// </summary>
		public EssayQuestions(){}
		/// <summary>
		/// EssayQuestion
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new EssayQuestion();
			}
		}
	}
}
