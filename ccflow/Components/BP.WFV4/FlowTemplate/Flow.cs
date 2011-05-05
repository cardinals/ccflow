using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.Port;
using BP.En;
using BP.Web;

namespace BP.CRM
{
    /// <summary>
    /// ��������
    /// </summary>
    public class FlowAttr : EntityNoNameAttr
    {
        public const string FK_FlowSort = "FK_FlowSort";
        public const string Note = "Note";
        public const string Author = "Author";
        public const string ReadTimes = "ReadTimes";
        public const string AutoDoc = "AutoDoc";
    }
    /// <summary>
    /// ����
    /// ��¼�����̵���Ϣ��
    /// ���̵ı�ţ����ƣ�����ʱ�䣮
    /// </summary>
    public class Flow : EntityNoName
    {
        #region ���췽��
        /// <summary>
        /// ����
        /// </summary>
        public Flow()
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="_No">���</param>
        public Flow(string _No)
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

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = this.ToE("Flow", "����");
                map.CodeStruct = "3";

                map.AddTBStringPK(FlowAttr.No, null, null, true, true, 1, 10, 3);
                map.AddTBString(FlowAttr.Name, null, null, true, false, 0, 50, 10);
                map.AddDDLEntities(FlowAttr.FK_FlowSort, "01", this.ToE("FlowSort", "�������") , new FlowSorts(), false);
                map.AddTBString(FlowAttr.Author, null, "�ṩ��", true, false, 0, 200, 10);
                map.AddTBString(FlowAttr.Note, null, null, true, false, 0, 100, 10);
                map.AddTBString(FlowAttr.AutoDoc, null, "ϵͳ����", true, false, 0, 100, 10);
                map.AddTBInt(FlowAttr.ReadTimes, 1, "���ô���", false, false);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ���̼���
    /// </summary>
    public class Flows : EntitiesNoName
    {

        #region ���췽��
        /// <summary>
        /// ��������
        /// </summary>
        public Flows() { }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="fk_sort"></param>
        public Flows(string fk_sort)
        {
            this.Retrieve(FlowAttr.FK_FlowSort, fk_sort);
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
                return new Flow();
            }
        }
        #endregion
    }
}

