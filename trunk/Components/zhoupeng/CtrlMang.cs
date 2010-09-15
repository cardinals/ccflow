

using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ����
    /// </summary>
	public class CtrlMangAttr:EntityNoNameAttr
	{
        public const string Note = "Note";
        public const string Val = "Val";
        public const string IsPJ = "IsPJ";
        public const string IsPL = "IsPL";
        public const string IsViewH = "IsViewH";
	}
	/// <summary>
	/// �������
	/// </summary>
    public class CtrlMang : EntityNoName
    {
        #region ���캯��
        /// <summary>
        /// �������
        /// </summary>
        public CtrlMang()
        {
        }
        public CtrlMang(string no)
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
                Map map = new Map("GE_CtrlMang");

                map.EnDesc = "�������";
                map.IsAutoGenerNo = false;
                map.IsAllowRepeatName = false;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(CtrlMangAttr.No, null, "Ӧ�ñ��", true, false, 2, 59, 2);
                map.AddTBString(CtrlMangAttr.Name, null, "Ӧ������", true, false, 0, 50, 300);

                map.AddDDLSysEnum(CtrlMangAttr.IsPJ, 0, "����", true, false, "OpenOff", "@0=�ر�@1=��");
                /* ����һЩ���۵�����, �������ͷ�����������Ǽ����ۡ� ���顣������ */


                map.AddDDLSysEnum(CtrlMangAttr.IsPL, 0, "����", true, false, "OpenOff", "@0=�ر�@1=��");
                /* ����һЩ���۵�����  �����ҳ������ ��ʾip ,���ڣ� �ĸ�ʽ��=== �� */

                map.AddDDLSysEnum(CtrlMangAttr.IsViewH, 0, "���", true, false, "OpenOff", "@0=�ر�@1=��");
                map.AddTBInt(CtrlMangAttr.IsViewH, 10, "��������ʾ����(0����ʾ)", true, false);
                map.AddTBInt(CtrlMangAttr.IsViewH, 10, "ϵͳ�Ƽ���ʾ����(0����ʾ)", true, false);
                map.AddTBInt(CtrlMangAttr.IsViewH, 10, "�����Ƽ���ʾ����(0����ʾ)", true, false);

                //     map.AddTBString(CtrlMangAttr.FK_Dept, null, "����", true, false, 0, 50, 300);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ������� 
	/// </summary>
	public class CtrlMangs: EntitiesNoName
	{
		/// <summary>
		/// ��ȡ�������
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CtrlMang();
			}
		}

		#region ���캯��		
		/// <summary>
		/// �������
		/// </summary>
		public CtrlMangs()
		{
		}		 
		#endregion
		 
		
	}
}
 

