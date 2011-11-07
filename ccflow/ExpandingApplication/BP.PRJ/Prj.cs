using System;
using System.Data;
using BP.DA;
using BP.Port; 
using BP.En;

namespace BP.PRJ
{
    /// <summary>
    /// ��Ŀ״̬
    /// </summary>
    public enum PrjState
    {
        /// <summary>
        /// �½�
        /// </summary>
        Init,
        /// <summary>
        /// ����
        /// </summary>
        Runing,
        /// <summary>
        /// ɾ��
        /// </summary>
        Delete
    }
	/// <summary>
	/// ��Ŀ�����б�
	/// </summary>
    public class PrjAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ��λ
        /// </summary>
        public const string DW = "DW";
        /// <summary>
        /// ��ַ
        /// </summary>
        public const string Addr = "Addr";
        /// <summary>
        /// ���
        /// </summary>
        public const string ZJ = "ZJ";
        /// <summary>
        /// ��������
        /// </summary>
        public const string KFFY = "KFFY";
        /// <summary>
        /// �걨����
        /// </summary>
        public const string SBRQ = "SBRQ";
        /// <summary>
        /// �Ƿ��ذ�
        /// </summary>
        public const string IsTB = "IsTB";
        /// <summary>
        /// ��Ŀ״̬
        /// </summary>
        public const string PrjState = "PrjState";
    }
	/// <summary>
	/// ��Ŀ
	/// </summary>
    public class Prj : EntityNoName
    {
        #region ��������
        /// <summary>
        /// ��Ŀ״̬
        /// </summary>
        public PrjState HisPrjState
        {
            get
            {
                return (PrjState)this.GetValIntByKey(PrjAttr.PrjState);
            }
            set
            {
                this.SetValByKey(PrjAttr.PrjState, (int)value);
            }
        }
        /// <summary>
        /// ��ַ
        /// </summary>
        public string Addr
        {
            get
            {
                return this.GetValStrByKey(PrjAttr.Addr);
            }
            set
            {
                this.SetValByKey(PrjAttr.Addr, value);
            }
        }
        /// <summary>
        /// ��λ
        /// </summary>
        public string DW
        {
            get
            {
                return this.GetValStrByKey(PrjAttr.DW);
            }
            set
            {
                this.SetValByKey(PrjAttr.DW, value);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public float ZJ
        {
            get
            {
                return this.GetValFloatByKey(PrjAttr.ZJ);
            }
            set
            {
                this.SetValByKey(PrjAttr.ZJ, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public float KFFY
        {
            get
            {
                return this.GetValFloatByKey(PrjAttr.KFFY);
            }
            set
            {
                this.SetValByKey(PrjAttr.KFFY, value);
            }
        }
        /// <summary>
        /// �걨����
        /// </summary>
        public string SBRQ
        {
            get
            {
                return this.GetValStrByKey(PrjAttr.SBRQ);
            }
            set
            {
                this.SetValByKey(PrjAttr.SBRQ, value);
            }
        }
        /// <summary>
        /// �Ƿ��ذ�
        /// </summary>
        public bool IsTB
        {
            get
            {
                return this.GetValBooleanByKey(PrjAttr.IsTB);
            }
            set
            {
                this.SetValByKey(PrjAttr.IsTB, value);
            }
        }
        /// <summary>
        /// ��Ŀ״̬
        /// </summary>
        public int PrjState
        {
            get
            {
                return this.GetValIntByKey(PrjAttr.PrjState);
            }
            set
            {
                this.SetValByKey(PrjAttr.PrjState, value);
            }
        }
        /// <summary>
        /// �Ƿ��ذ��ǩ
        /// </summary>
        public string PrjStateText
        {
            get
            {
                return this.GetValRefTextByKey(PrjAttr.PrjState);
            }
        }
        #endregion

        #region ���캯��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsDelete = true;
                uac.IsInsert = true;
                uac.IsUpdate = true;
                uac.IsView = true;
                return uac;
            }
        }
        /// <summary>
        /// ��Ŀ
        /// </summary>
        public Prj() { }
        /// <summary>
        /// strubg
        /// </summary>
        public Prj(string no)
        {
            this.No = no;
            this.Retrieve();
        }
        public Prj(int no)
        {
            this.No = no.ToString();
            this.Retrieve();
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

                Map map = new Map("Prj_Prj");
                map.EnDesc = "��Ŀ";

                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "4";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(PrjAttr.No, null, "���", true, true, 4, 4, 4);
                map.AddTBString(PrjAttr.Name, null, "����", true, false, 0, 60, 500);
               
                map.AddDDLSysEnum(PrjAttr.PrjState, 0, "��Ŀ״̬", true, true, PrjAttr.PrjState,
                    "@0=�½�@1=������@2=����");

                map.AddTBString(PrjAttr.DW, null, "��λ", true, false, 0, 60, 500);
                map.AddTBString(PrjAttr.Addr, null, "��ַ", true, false, 0, 60, 500);

                map.AttrsOfOneVSM.Add(new EmpPrjs(), new Emps(), EmpPrjAttr.FK_Prj, EmpPrjAttr.FK_Emp,
                    DeptAttr.Name, DeptAttr.No, "��Ա");

                  

                map.AddSearchAttr(PrjAttr.PrjState);

                RefMethod rm = new RefMethod();
                rm.Title = "��Ա��λ";
                rm.ClassMethodName = this.ToString() + ".DoEmpPrjStations";
                rm.IsCanBatch = true;
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "������";
                rm.ClassMethodName = this.ToString() + ".DoDocTree";
                rm.IsCanBatch = true;
                map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = "ִ�з���";
                //rm.ClassMethodName = this.ToString() + ".DoFK";
                //rm.HisAttrs.AddTBDecimal("JE", 100, "������", true, false);
                //map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion


        public string DoEmpPrjStations()
        {
            PubClass.WinOpen("../Comm/PanelEns.aspx?EnsName=BP.PRJ.EmpPrjExts&FK_Prj=" + this.No, 800, 500);
            return null;
        }
      
        public string DoDocTree()
        {
            PubClass.WinOpen("../ExpandingApplication/PRJ/DocTree.aspx?No=" + this.No, 500, 500);
            return null;
        }
        protected override bool beforeInsert()
        {
            this.No = this.GenerNewNo;
            this.SBRQ = DataType.CurrentData;
            //this.PrjState = 1;
            //if (this.DW.Length < 1)
            //    throw new Exception("@dsdafasdfadsfsd");
            return base.beforeInsert();
        }
    }
	/// <summary>
	/// ��Ŀs
	/// </summary>
	public class Prjs : EntitiesNoName
	{	
		#region ���췽��
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new Prj();
			}
		}
		/// <summary>
		/// ��Ŀs 
		/// </summary>
		public Prjs(){}
		#endregion
	}
	
}
