using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// �Ծ�ֲ�attr
	/// </summary>
    public class PaperRandomFBAttr : EntityNoNameAttr
    {
        /// <summary>
        /// FK_Type
        /// </summary>
        public const string FK_Type = "FK_Type";
        /// <summary>
        /// FK_Sort
        /// </summary>
        public const string FK_Sort = "FK_Sort";
        /// <summary>
        /// HisNum
        /// </summary>
        public const string HisNum = "HisNum";
    }
	/// <summary>
	/// �Ծ�ֲ�
	/// </summary>
	public class PaperRandomFB :EntityNoName
	{
		#region ʵ�ֻ����ķ���
		/// <summary>
		/// uac
		/// </summary>
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
				
				Map map = new Map("GTS_PaperRandomFB");
				map.EnDesc="����Ծ�ֲ����";
				map.CodeStruct="4";
				map.EnType= EnType.Admin;

                map.AddTBStringPK(PaperRandomFBAttr.No, null, "No", true, true, 4, 4, 4);
                map.AddTBString(PaperRandomFBAttr.Name, null, "Name", true, true, 4, 4, 4);

			 
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// �Ծ�ֲ�
		/// </summary> 
		public PaperRandomFB()
		{
		}
		/// <summary>
		/// �Ծ�ֲ�
		/// </summary>
		/// <param name="_No">���ջ��ر��</param> 
		public PaperRandomFB(string _No ):base(_No)
		{
		}
		#endregion 

		#region �߼�����
		#endregion

	}
	/// <summary>
	///  �Ծ�ֲ�
	/// </summary>
	public class PaperRandomFBs :EntitiesNoName
	{
		/// <summary>
		/// PaperRandomFBs
		/// </summary>
		public PaperRandomFBs(){}
		/// <summary>
		/// PaperRandomFB
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperRandomFB();
			}
		}
		 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <returns></returns>
		public int RetrievePaperRandomFB(string fk_emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhereInSQL(PaperFixAttr.No,  "SELECT FK_Paper FROM GTS_PaperVSEmp WHERE FK_Emp='"+fk_emp+"'");
			return qo.DoQuery();
		}

		 
	}
}
