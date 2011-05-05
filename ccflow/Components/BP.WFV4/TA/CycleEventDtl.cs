using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.TA
{
	/// <summary>
	/// �����¼�����
	/// </summary>
    public class CycleEventDtlAttr : EntityOIDAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// ��ʼѭ������
        /// </summary>
        public const string RefDate = "RefDate";
        public const string FK_CycleEvent = "FK_CycleEvent";
        
    }
	/// <summary>
	/// �����¼�
	/// </summary> 
    public class CycleEventDtl : EntityMyPK
    {
        #region  ����
        public int FK_CycleEvent
        {
            get
            {
                return this.GetValIntByKey(CycleEventDtlAttr.FK_CycleEvent);
            }
            set
            {
                SetValByKey(CycleEventDtlAttr.FK_CycleEvent, value);
            }
        }
        public string RefDate
        {
            get
            {
                return this.GetValStringByKey(CycleEventDtlAttr.RefDate);
            }
            set
            {
                SetValByKey(CycleEventDtlAttr.RefDate, value);
            }
        }
        #endregion

        #region ���캯��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenAll();
                return uac;
            }
        }
        /// <summary>
        /// �����¼�
        /// </summary>
        public CycleEventDtl()
        {
        }
        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="_No">No</param>
        public CycleEventDtl(string oid)
            : base(oid)
        {
        }
        /// <summary>
        /// Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("TA_CycleEventDtl");
                map.EnDesc = "�����¼�����";
                map.AddTBIntPKOID();
                map.Icon = "./Images/CycleEventDtl_s.ico";

                map.AddMyPK();

                map.AddTBInt(CycleEventDtlAttr.FK_CycleEvent, 0, "FK_CycleEvent", false, false);
                map.AddTBDate(CycleEventDtlAttr.RefDate, "��������", false, false);

                //map.AddTBString(CycleEventAttr.ToEmps, null, "������", true, false, 0, 1000, 15);
                //map.AddTBString(CycleEventAttr.ToEmpNames, null, "����������", true, false, 0, 1000, 15);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// �����¼�s
	/// </summary> 
	public class CycleEventDtls: Entities
	{
		public override Entity GetNewEntity
		{
			get
			{
				return new CycleEventDtl();
			}
		}
		public CycleEventDtls()
		{

		}
		
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="emp">��Ա</param>
		/// <param name="RefDate">��ʼ����</param>
		public CycleEventDtls(int fk_cycle)
		{
			QueryObject qo = new QueryObject(this);
            qo.AddWhere(CycleEventDtlAttr.FK_CycleEvent, fk_cycle);
            qo.addOrderBy(CycleEventDtlAttr.RefDate);
			qo.DoQuery();
		}
	}
}
 