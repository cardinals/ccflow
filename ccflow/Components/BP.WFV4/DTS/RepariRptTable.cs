using System;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
using BP.Sys;
namespace BP.WF
{
    /// <summary>
    /// �޸����ݿ� ��ժҪ˵��
    /// </summary>
    public class RepariRptTable : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public RepariRptTable()
        {
            this.Title = "�޸�Rpt���ݱ�";
            this.Help = "���ϰ汾�ĳ�����NDxxxRpt���ж�ʧ�ֶε�����������Զ����޸������Ա�֤���Բ�ѯ�����ݡ�"; 
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
            
        }
        /// <summary>
        /// ��ǰ�Ĳ���Ա�Ƿ����ִ���������
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            Flows fls = new Flows();
            fls.RetrieveAll();
            foreach (Flow fl in fls)
            {
                string flowID = int.Parse(fl.No).ToString();



            }
            return null;
        }
    }
}
