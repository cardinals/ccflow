using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.WF
{
	/// <summary>
    ///  �����
	/// </summary>
    public class FrmSort : EntityNoName
    {
        #region ���췽��
        /// <summary>
        /// �����
        /// </summary>
        public FrmSort()
        {
        }
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="_No"></param>
        public FrmSort(string _No) : base(_No) { }
        #endregion

        /// <summary>
        /// �����Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_FrmSort");
                map.EnDesc = this.ToE("FrmSort", "�����");
                map.CodeStruct = "2";

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;
                map.IsAllowRepeatNo = false;

                map.AddTBStringPK(SimpleNoNameAttr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(SimpleNoNameAttr.Name, null, "����", true, false, 2, 50, 50);
                map.AddTBInt("IDX", 0, "IDX", false, false);
                
                this._enMap = map;
                return this._enMap;
            }
        }
        protected override bool beforeDelete()
        {
            if (this.No == "00")
                throw new Exception("�����������ɾ����");
            return base.beforeDelete();
        }
    }
	/// <summary>
    /// �����
	/// </summary>
	public class FrmSorts :SimpleNoNames
	{
		/// <summary>
		/// �����s
		/// </summary>
		public FrmSorts(){}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new FrmSort();
			}
			 
		}
		/// <summary>
		/// �����s
		/// </summary>
		/// <param name="no">ss</param>
		/// <param name="name">anme</param>
		public void AddByNoName(string no , string name)
		{
			FrmSort en = new FrmSort();
			en.No = no;
			en.Name = name;
			this.AddEntity(en);
		}
        public override int RetrieveAll()
        {
            int i = base.RetrieveAll();
            if (i == 0)
            {
                FrmSort fs = new FrmSort();
                fs.Name = "Ĭ�ϱ����"; 
                fs.No = "01";
                fs.Insert();
                i = base.RetrieveAll();
            }
            return i;
        }
	}
}
