using System;
using System.Collections;
using BP.DA;
using BP.Port;
using BP.En;
using BP.Web;

namespace BP.WF.Ext
{
    /// <summary>
    /// ����
    /// </summary>
    public class FlowSheet : EntityNoName
    {
        #region ���췽��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (Web.WebUser.No == "admin")
                    uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public FlowSheet()
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="_No">���</param>
        public FlowSheet(string _No)
        {
            this.No = _No;
            if (SystemConfig.IsDebug)
            {
                int i = this.RetrieveFromDBSources();
                if (i == 0)
                    throw new Exception("���̱�Ų�����");
            }
            else
            {
                this.Retrieve();
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


                Map map = new Map("WF_Flow");

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = this.ToE("Flow", "����");
                map.CodeStruct = "3";

                map.AddTBStringPK(FlowAttr.No, null, null, true, true, 1, 10, 3);
                map.AddTBString(FlowAttr.Name, null, null, true, false, 0, 50, 10);
                map.AddDDLEntities(FlowAttr.FK_FlowSort, "01", this.ToE("FlowSort", "�������"), new FlowSorts(), true);


                map.AddTBInt(FlowAttr.DateLit, 30, "�涨����", false, false);

                map.AddBoolean(FlowAttr.IsOK, true, "�Ƿ�����", true, true);

                map.AddDDLSysEnum(BP.WF.FlowAttr.CCType, (int)CCType.None, "��������", true, true, BP.WF.FlowAttr.CCType,
             "@0=������@1=����Ա@2=����λ@3=���ڵ�@4=������@5=���ղ������λ");

                map.AddDDLSysEnum(BP.WF.FlowAttr.CCWay, (int)CCWay.ByMsg, "���ͷ�ʽ", true, true, BP.WF.FlowAttr.CCWay,
         "@0=���ñ�ϵͳ��ʱ��Ϣ@1=ͨ��Email(��web.config������)@2=�����ֻ��ӿ�@3=�������ݿ⺯��");

                // map.AddBoolean(FlowAttr.IsCCAll, false, "�Ƿ��Ͳ�����", true, true);


                map.AddDDLSysEnum(FlowAttr.FlowRunWay, (int)FlowRunWay.HandWork, "���з�ʽ", true, true, FlowAttr.FlowRunWay,
                    "@0=�ֹ�����@1=��������@2=��������@3=��������@4=��������@5=��Сʱ����");
                map.AddTBString(FlowAttr.FlowRunObj, null, "��������", true, false, 0, 100, 10);


                map.AddTBString(BP.WF.FlowAttr.Note, null, this.ToE("Note", "��ע"), true, false, 0, 100, 10);
                Attr attr = map.GetAttrByKey(BP.WF.FlowAttr.Note);
                attr.UIIsLine = true;


                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("CCNode", "���ͽڵ�"); // "���ͽڵ�";
                rm.ToolTip = "�����ͷ�ʽ����Ϊ���ͽڵ�ʱ�������ò���Ч��";
                rm.Icon = "/Images/Btn/Confirm.gif";
                rm.ClassMethodName = this.ToString() + ".DoCCNode";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = this.ToE("DesignCheckRpt", "��Ƽ�鱨��"); // "��Ƽ�鱨��";
                rm.ToolTip = "���������Ƶ����⡣";
                rm.Icon = "/Images/Btn/Confirm.gif";
                rm.ClassMethodName = this.ToString() + ".DoCheck";
                map.AddRefMethod(rm);


                rm = new RefMethod();
                rm.Title = this.ToE("ViewDef", "��ͼ����"); //"��ͼ����";
                rm.Icon = "/Images/Btn/View.gif";
                rm.ClassMethodName = this.ToString() + ".DoDRpt";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("RptRun", "��������"); // "��������";
                rm.ClassMethodName = this.ToString() + ".DoOpenRpt()";
                //rm.Icon = "/Images/Btn/Table.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("FlowSheetDataOut", "����ת������");  //"����ת������";
                //  rm.Icon = "/Images/Btn/Table.gif";
                rm.ToolTip = "���������ʱ�䣬��������ת���浽����ϵͳ��Ӧ�á�";

                rm.ClassMethodName = this.ToString() + ".DoExp";
                map.AddRefMethod(rm);

                map.AttrsOfOneVSM.Add(new FlowStations(), new Stations(), FlowStationAttr.FK_Flow,
                    FlowStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "���͸�λ");

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region  ��������
        public string DoCCNode()
        {
            PubClass.WinOpen("./../WF/Admin/CCNode.aspx?FK_Flow="+this.No,400,500);
            return null;
        }
        /// <summary>
        /// ִ�м��
        /// </summary>
        /// <returns></returns>
        public string DoCheck()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoCheck();
        }
        /// <summary>
        /// �������ת��
        /// </summary>
        /// <returns></returns>
        public string DoExp()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoExp();
        }
        /// <summary>
        /// ���屨��
        /// </summary>
        /// <returns></returns>
        public string DoDRpt()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoDRpt();
        }
        /// <summary>
        /// ���б���
        /// </summary>
        /// <returns></returns>
        public string DoOpenRpt()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            return fl.DoOpenRpt();
        }
        /// <summary>
        /// ����֮������飬ҲҪ���»��档
        /// </summary>
        protected override void afterUpdate()
        {
            Flow fl = new Flow();
            fl.No = this.No;
            fl.RetrieveFromDBSources();
            fl.Update();
            base.afterUpdate();
        }
        #endregion
    }
    /// <summary>
    /// ���̼���
    /// </summary>
    public class FlowSheets : EntitiesNoName
    {
        #region ��ѯ
        /// <summary>
        /// ��ѯ����ȫ�����������ڼ��ڵ�����
        /// </summary>
        /// <param name="FlowSort">�������</param>
        /// <param name="IsCountInLifeCycle">�ǲ��Ǽ����������ڼ��� true ��ѯ����ȫ���� </param>
        public void Retrieve(string FlowSort)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(BP.WF.FlowAttr.FK_FlowSort, FlowSort);
            qo.addOrderBy(BP.WF.FlowAttr.No);
            qo.DoQuery();
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��������
        /// </summary>
        public FlowSheets() { }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="fk_sort"></param>
        public FlowSheets(string fk_sort)
        {
            this.Retrieve(BP.WF.FlowAttr.FK_FlowSort, fk_sort);
        }
        #endregion

        #region �õ�ʵ��
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FlowSheet();
            }
        }
        #endregion
    }
}

