using System;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.Sys;
using BP.En;
namespace BP.WF
{
    /// <summary>
    /// �޸��ڵ��map ��ժҪ˵��
    /// </summary>
    public class RepariNodeFrmMap : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public RepariNodeFrmMap()
        {
            this.Title = "�޸��ڵ��";
            this.Help = "���ڵ��ϵͳ�ֶ��Ƿ񱻷Ƿ�ɾ��������Ƿ�ɾ���Զ�������������Щ�ֶΰ���:Rec,Title,OID,FID,NodeState,WFState,RDT,CDT";
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
            //this.Warning = "��ȷ��Ҫִ����";
            //HisAttrs.AddTBString("P1", null, "ԭ����", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P2", null, "������", true, false, 0, 10, 10);
            //HisAttrs.AddTBString("P3", null, "ȷ��", true, false, 0, 10, 10);
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
            Nodes nds = new Nodes();
            nds.RetrieveAllFromDBSource();

            string info = "";
            foreach (Node nd in nds)
            {
                string msg = "";
                Work wk = nd.HisWork;
                if (wk.EnMap.Attrs.Contains("Rec") == false)
                {
                    msg += "@�ֶ�Rec���Ƿ�ɾ����.";
                    BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + nd.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = WorkAttr.Rec;
                    if (nd.IsStartNode == false)
                        attr.Name = BP.Sys.Language.GetValByUserLang("Rec", "��¼��"); // "��¼��";
                    else
                        attr.Name = BP.Sys.Language.GetValByUserLang("Sponsor", "������"); //"������";

                    attr.MyDataType = BP.DA.DataType.AppString;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.MaxLen = 20;
                    attr.MinLen = 0;
                    attr.DefVal = "@WebUser.No";
                    attr.Insert();
                }

                if (wk.EnMap.Attrs.Contains("RDT") == false)
                {
                    msg += "@�ֶ�CDT���Ƿ�ɾ����.";
                    MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + nd.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = WorkAttr.RDT;
                    if (nd.IsStartNode)
                        attr.Name = BP.Sys.Language.GetValByUserLang("StartTime", "����ʱ��"); //"����ʱ��";
                    else
                        attr.Name = BP.Sys.Language.GetValByUserLang("CompleteTime", "���ʱ��"); //"���ʱ��";

                    attr.MyDataType = BP.DA.DataType.AppDateTime;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.DefVal = "@RDT";
                    attr.Tag = "1";
                    attr.Insert();
                }

                if (wk.EnMap.Attrs.Contains("CDT") == false)
                {
                    msg += "@�ֶ�CDT���Ƿ�ɾ����.";
                    BP.Sys.MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + nd.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = WorkAttr.CDT;
                    if (nd.IsStartNode)
                        attr.Name = BP.Sys.Language.GetValByUserLang("StartTime", "����ʱ��"); //"����ʱ��";
                    else
                        attr.Name = BP.Sys.Language.GetValByUserLang("CompleteTime", "���ʱ��"); //"���ʱ��";
                    attr.MyDataType = BP.DA.DataType.AppDateTime;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.DefVal = "@RDT";
                    attr.Tag = "1";
                    attr.Insert();
                }

                if (wk.EnMap.Attrs.Contains(StartWorkAttr.NodeState) == false)
                {
                    msg += "@��ʼ�ڵ��ֶ�NodeState���Ƿ�ɾ����.";
                    MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + nd.NodeID;
                    attr.KeyOfEn = WorkAttr.NodeState;
                    attr.Name = BP.Sys.Language.GetValByUserLang("NodeState", "�ڵ�״̬"); //"�ڵ�״̬";
                    attr.MyDataType = BP.DA.DataType.AppInt;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.DefVal = "0";
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.Insert();
                }

                if (wk.EnMap.Attrs.Contains(StartWorkAttr.FK_Dept) == false)
                {
                    MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + nd.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = StartWorkAttr.FK_Dept;
                    attr.Name = BP.Sys.Language.GetValByUserLang("OperDept", "����Ա����"); //"����Ա����";
                    attr.MyDataType = BP.DA.DataType.AppString;
                    attr.UIContralType = UIContralType.DDL;
                    attr.LGType = FieldTypeS.FK;
                    attr.UIBindKey = "BP.Port.Depts";
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.MinLen = 0;
                    attr.MaxLen = 20;
                    attr.Insert();
                }


                if (wk.EnMap.Attrs.Contains(StartWorkAttr.Emps) == false)
                {
                    msg += "@��ʼ�ڵ��ֶ�Emps���Ƿ�ɾ����.";
                    MapAttr attr = new BP.Sys.MapAttr();
                    attr.FK_MapData = "ND" + nd.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = WorkAttr.Emps;
                    attr.Name = StartWorkAttr.Emps;
                    attr.MyDataType = BP.DA.DataType.AppString;
                    attr.UIContralType = UIContralType.TB;
                    attr.LGType = FieldTypeS.Normal;
                    attr.UIVisible = false;
                    attr.UIIsEnable = false;
                    attr.MaxLen = 400;
                    attr.MinLen = 0;
                    attr.Insert();
                }

                if (nd.IsStartNode)
                {
                    if (wk.EnMap.Attrs.Contains("Title") == false)
                    {
                        msg += "@��ʼ�ڵ��ֶ�Title���Ƿ�ɾ����.";
                        MapAttr attr = new BP.Sys.MapAttr();
                        attr.FK_MapData = "ND" + nd.NodeID;
                        attr.HisEditType = BP.En.EditType.UnDel;
                        attr.KeyOfEn = "Title";
                        attr.Name = BP.Sys.Language.GetValByUserLang("Title", "����"); // "���̱���";
                        attr.MyDataType = BP.DA.DataType.AppString;
                        attr.UIContralType = UIContralType.TB;
                        attr.LGType = FieldTypeS.Normal;
                        attr.UIVisible = false;
                        attr.UIIsEnable = false;
                        attr.UIIsLine = true;
                        attr.UIWidth = 251;

                        attr.MinLen = 0;
                        attr.MaxLen = 200;
                        attr.IDX = -100;
                        attr.X = (float)174.83;
                        attr.Y = (float)54.4;
                        attr.Insert();
                    }

                    if (wk.EnMap.Attrs.Contains(StartWorkAttr.WFState) == false)
                    {
                        msg += "@��ʼ�ڵ��ֶ�WFState���Ƿ�ɾ����.";
                        MapAttr attr = new BP.Sys.MapAttr();
                        attr.FK_MapData = "ND" + nd.NodeID;
                        attr.HisEditType = BP.En.EditType.Readonly;
                        attr.KeyOfEn = StartWorkAttr.WFState;
                        attr.DefVal = "0";
                        attr.Name = BP.Sys.Language.GetValByUserLang("FlowState", "����״̬"); //"����״̬";
                        attr.MyDataType = BP.DA.DataType.AppInt;
                        attr.LGType = FieldTypeS.Normal;
                        attr.UIBindKey = attr.KeyOfEn;
                        attr.UIVisible = false;
                        attr.UIIsEnable = false;
                        attr.Insert();
                    }
                }

                if (nd.FocusField != "")
                {
                    if (wk.EnMap.Attrs.Contains(nd.FocusField ) == false)
                    {
                        msg += "@�����ֶ�"+nd.FocusField+" ���Ƿ�ɾ����.";
                    }
                }

                if (msg != "")
                {
                    info += "<b>������" + nd.FlowName + ",�ڵ�("+nd.NodeID+")(" + nd.Name + "), ���������:</b>" + msg + "<hr>";
                }
            }
            return info + "ִ�����...";
        }
    }
}
