

using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ����
    /// </summary>
	public class AppVarSetAttr:EntityNoNameAttr
	{
        public const string Note = "Note";
        public const string Val = "Val";
	}
	/// <summary>
	/// ϵͳ��������
	/// </summary>
    public class AppVarSet : EntityNoName
    {
        #region ���캯��
        /// <summary>
        /// ϵͳ��������
        /// </summary>
        public AppVarSet()
        {
        }
        public AppVarSet(string no)
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
                Map map = new Map("GE_AppVarSet");

                map.EnDesc = "ϵͳ��������";
                map.IsAutoGenerNo = false;
                map.IsAllowRepeatName = false; 
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(AppVarSetAttr.No, null, "Key", true, false, 2, 59, 2);
                map.AddTBString(AppVarSetAttr.Name, null, "����", true, false, 0, 50, 300);
                map.AddTBString(AppVarSetAttr.Note, null, "��ע", true, false, 0, 50, 300);
                map.AddMyFile("����");

                //     map.AddTBString(AppVarSetAttr.FK_Dept, null, "����", true, false, 0, 50, 300);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ϵͳ�������� 
	/// </summary>
	public class AppVarSets: EntitiesNoName
	{
		/// <summary>
		/// ��ȡϵͳ��������
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new AppVarSet();
			}
		}

		#region ���캯��		
		/// <summary>
		/// ϵͳ��������
		/// </summary>
		public AppVarSets()
		{
		}		 
		#endregion
		 
		
	}
}
 

