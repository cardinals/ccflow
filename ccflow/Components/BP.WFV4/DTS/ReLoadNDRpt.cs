using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
namespace BP.WF
{
    /// <summary>
    /// �޸���������ֶγ��� ��ժҪ˵��
    /// </summary>
    public class ReLoadNDRpt : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public ReLoadNDRpt()
        {
            this.Title = "���������װ�����̱���";
            this.Help = "�ڽڵ�������ش�仯�������޸����ݣ�ִ�д˹��ܲ���Ӱ�����ݵ��ǻ�����ʱ��Ƚϳ���";
            
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
            string keys="~!@#$%^&*()_+{}|:<>?`=[];,./�����������������������������������������������࣭���ۣݣ���������";
            char[] cc = keys.ToCharArray();
            foreach (char c in cc)
            {
                DBAccess.RunSQL("update sys_mapattr set keyofen=REPLACE(keyofen,'" + c + "' , '_')");
            }

            BP.Sys.MapAttrs attrs = new Sys.MapAttrs();
            attrs.RetrieveAll();
            foreach (BP.Sys.MapAttr item in attrs)
            {
                try
                {
                    int i = int.Parse(item.KeyOfEn.Substring(0,1));
                    item.KeyOfEn = "_A" + item.KeyOfEn;
                }
                catch
                {
                    continue;
                }
                item.DirectUpdate();
            }
            BP.DA.DBAccess.RunSQL("UPDATE  sys_mapattr SET MyPK=FK_MapData+'_'+KeyOfEn where MyPK!=FK_MapData+'_'+KeyOfEn");

            string msg = "";
            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();
            foreach (Flow fl in fls)
            {
              msg+=  fl.DoReloadRptData();
            }
            return "��ʾ��"+fls.Count+"�����̲�������죬��Ϣ���£�@"+msg;
        }
    }
}
