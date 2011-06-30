using System;
using System.Collections;
using BP.DA;
using BP.Sys;
using BP.En;
using BP.WF.Port;
//using BP.ZHZS.Base;

namespace BP.WF
{
	/// <summary>
	/// Frm����
	/// </summary>
    public class FrmAttr : EntityNoNameAttr
    {
        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// Idx
        /// </summary>
        public const string Idx = "Idx";
    }
	/// <summary>
	/// Frm
	/// </summary>
	public class Frm :EntityNoName
	{
		#region ��������
		/// <summary>
		///�ڵ�
		/// </summary>
		public int  FK_Node
		{
			get
			{
				return this.GetValIntByKey(FrmAttr.FK_Node);
			}
			set
			{
				this.SetValByKey(FrmAttr.FK_Node,value);
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// Frm
		/// </summary>
		public Frm(){}
		/// <summary>
		/// ��д���෽��
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_Frm");
                map.EnDesc = "Frm";
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(FrmAttr.No, null, null, true, true, 1, 10, 3);
                map.AddTBString(FrmAttr.Name, null, null, true, false, 0, 50, 10);
                map.AddTBInt(FrmAttr.FK_Node, 0, null, true, false);
                map.AddTBInt(FrmAttr.Idx, 0, null, true, false);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion
	}
	/// <summary>
	/// Frm
	/// </summary>
    public class Frms : EntitiesMyPK
    {
        /// <summary>
        /// Frm
        /// </summary>
        public Frms() { }
        /// <summary>
        /// Frm
        /// </summary>
        /// <param name="fk_node"></param>
        public Frms(int fk_node)
        {
            this.Retrieve(FrmAttr.FK_Node, fk_node, FrmAttr.Idx);
        }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Frm();
            }
        }
    }
}
