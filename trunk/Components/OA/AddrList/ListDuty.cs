

using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ����
    /// </summary>
    public class ListDutyAttr : EntityNoNameAttr
    {
        public const string IsShowDtl = "IsShowDtl";
    }
	/// <summary>
	/// ��ϵ��ְ��
	/// </summary>
    public class ListDuty : EntityNoName
    {
        #region ���캯��
        /// <summary>
        /// ��ϵ��ְ��
        /// </summary>
        public ListDuty()
        {
        }
        public ListDuty(string no)
        {
            this.No = no;
            try
            {
                this.Retrieve();
            }
            catch
            {
            }
        }
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("GE_ListDuty");
                map.EnDesc = "��ϵ��ְ��";

                map.IsAutoGenerNo = false;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                //  map.IsAutoGenerNo = true;

                map.AddTBStringPK(PictSortAttr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(ListDutyAttr.Name, null, "����", true, false, 0, 50, 300);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ��ϵ��ְ�� 
	/// </summary>
	public class ListDutys: EntitiesNoName
	{
		/// <summary>
		/// ��ȡ��ϵ��ְ��
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ListDuty();
			}
		}

		#region ���캯��		
		/// <summary>
		/// ��ϵ��ְ��
		/// </summary>
		public ListDutys()
		{
		}		 
		#endregion
		 
		
	}
}
 

