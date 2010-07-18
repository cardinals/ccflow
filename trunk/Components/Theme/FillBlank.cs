using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �����attr
	/// </summary>
	public class FillBlankAttr:EntityNoNameAttr
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string FK_ThemeSort="FK_ThemeSort";
		/// <summary>
		/// �ո�����
		/// </summary>
		public const string BlankNum="BlankNum";
	}
	/// <summary>
	/// �����
	/// </summary>
	public class FillBlank :ChoseBase
	{
		#region attr
		public new string NameExt
		{
			get
			{
				return ChoseBase.GenerStr(this.Name)+"\n\n\n";
				
			}
		}
		/// <summary>
		/// �ո�����
		/// </summary>
		public int BlankNum
		{
			get
			{
				return this.GetValIntByKey(FillBlankAttr.BlankNum);
			}
			set
			{
				this.SetValByKey(FillBlankAttr.BlankNum,value);
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
				
				Map map = new Map("GTS_FillBlank");
				map.EnDesc="�����";
				map.CodeStruct="5";
				//map.EnType= EnType.Admin;

				map.AddTBStringPK(FillBlankAttr.No,null,"���",true,true,0,50,20);
				map.AddTBString(FillBlankAttr.Name,null,"����",true,false,0,5000,600);
				map.AddDDLEntities(FillBlankAttr.FK_ThemeSort,"0001","���������",new ThemeSorts(),true);
				map.AddTBInt(FillBlankAttr.BlankNum,0,"�ո���",true,true);
 
				map.AddSearchAttr( FillBlankAttr.FK_ThemeSort);
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
		/// �����
		/// </summary> 
		public FillBlank()
		{
		}
		/// <summary>
		/// �����
		/// </summary>
		/// <param name="_No">���ջ��ر��</param> 
		public FillBlank(string _No ):base(_No)
		{
		}
		#endregion 

		#region �߼�����
		public string[] HisKeys
		{
			get
			{
				string[] keys= new string[this.BlankNum];
				string name=this.Name ; 
				 

				int i=0;
				while( name.IndexOf("(")!=-1)
				{
					name = name.Substring( name.IndexOf("(") +1  );
					string answer = name.Substring( 0 ,  name.IndexOf(")")   ); // �𰸡�
					name=name.Substring( answer.Length);
					keys[i]=answer;
					i++;
				}
				
				return keys;
			}
		}
		protected override bool beforeUpdate()
		{
		 
 
			string name=this.Name ; 
			name=name.Replace("��","(");
			name=name.Replace("��",")");
			name=name.Replace(" ","");
			this.Name=name;

			if (name.IndexOf("(")==-1)
			{
				throw new Exception("��Ŀ��ʽ������û�а�����ȷ�ĸ�ʽ����(abc)�𰸲��֣�����Ӧ�����д��");
			}

			int i=0;
			while( name.IndexOf("(")!=-1)
			{
				/*  */
				name = name.Substring( name.IndexOf("(") +1  );

				string answer = name.Substring( 0 ,  name.IndexOf(")")   ); // �𰸡�

				name=name.Substring( answer.Length);

				i++;

			}
			this.BlankNum = i;

			//string[] docs = name.Split("(") ;

			



			return base.beforeUpdate ();
		}

 

		#endregion

	 
	}
	/// <summary>
	/// ��˰���ջ���
	/// </summary>
	public class FillBlanks :EntitiesNoName
	{
		public FillBlanks(string sort)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(FillBlankAttr.FK_ThemeSort,sort);
			qo.DoQuery();
		}
		public int Search(string sort, string fk_emp, int sxcd)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(FillBlankAttr.FK_ThemeSort,sort);
			qo.addAnd();
			if (sxcd==4)
			{
				qo.AddWhereNotInSQL(FillBlankAttr.No, "SELECT FK_Theme FROM GTS_Study WHERE FK_Emp='"+fk_emp+"' AND FK_ThemeType='"+ThemeType.FillBlank+"'"  );
			}
			else
			{
				qo.AddWhereInSQL(FillBlankAttr.No, "SELECT FK_Theme FROM GTS_Study WHERE FK_Emp='"+fk_emp+"' AND FK_ThemeType='"+ThemeType.FillBlank+"' AND SXCD="+sxcd );
			}
			return qo.DoQuery();
		}

		/// <summary>
		/// FillBlanks
		/// </summary>
		public FillBlanks(){}
		/// <summary>
		/// FillBlank
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new FillBlank();
			}
		}
	}
}
