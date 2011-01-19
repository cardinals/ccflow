using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Design;
using Tax666.Common;

namespace Tax666.WebControls
{
    /// <summary>
    /// ͷ���˵�web�ؼ�
    /// </summary>
    [
    AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal),
    DefaultProperty("ButtonList"),
    ParseChildren(true, "ButtonList"),
    ToolboxData("<{0}:HeadMenuWebControls runat=\"server\"> </{0}:HeadMenuWebControls>"),
    Description("ͷ���˵�web�ؼ�")
    ]
    public class HeadMenuWebControls : WebControl
    {
        /// <summary>
        /// ���캯��,����������÷�����Ҫ��ʼ��
        /// </summary>
        public HeadMenuWebControls()
        {
            _ButtonList = new List<HeadMenuButtonItem>();
        }

        #region ˽�б���
        private List<HeadMenuButtonItem> _ButtonList;
        private string HeadMenuTemplateTxt =
        @"<!-- ͷ���˵� Start -->
	        <table border='0' cellpadding='0' cellspacing='0' width='100%' align='center'>
              <tr>
                <td class='menubar_title'><img border='0' src='{0}' align='absmiddle' hspace='4' vspace='4'>&nbsp;{1}</td>
                <td class='menubar_readme_text' valign='bottom'><img src='{2}' align='absMiddle' border='0' />&nbsp;{3}</td>
              </tr>
              <tr>
                <td height='27px' class='menubar_function_text'>Ŀǰ�������ܣ�{4}</td>
                <td class='menubar_menu_td' align='right'>{5}</td>
              </tr>
              <tr><td height='5px' colspan='2'></td></tr>
            </table>
        <!-- ͷ���˵� End -->
        ";
        private string _HeadIconPath = WebRequests.GetWebUrl() + "Manager/images/Icon/";
        private string _HeadTitleIcon = "default.gif";
        private string _HeadTitleTxt = "����";
        private string _HeadHelpIcon = "HelpIco.gif";
        private string _HeadHelpTxt = "������";
        private string _HeadOPTxt = "";


        private string CreateButtonHtml()
        {
            StringBuilder sb = new StringBuilder();
            if (_ButtonList != null && _ButtonList.Count > 0)
            {
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
                string OnUrlJs = "";
                string ButtonIcon = "";
                string ButtonTxt = "";
                int DispButtonNum = 0;
                for (int i = 0; i < _ButtonList.Count; i++)
                {
                    if (_ButtonList[i].ButtonVisible)
                    {
                        DispButtonNum++;
                        OnUrlJs = "";
                        ButtonIcon = "";
                        ButtonTxt = "";
                        switch (_ButtonList[i].ButtonUrlType)
                        {
                            case UrlType.Href:
                                OnUrlJs = string.Format("JavaScript:window.location.href='{0}';", _ButtonList[i].ButtonUrl);
                                break;
                            case UrlType.JavaScript:
                                OnUrlJs = string.Format("JavaScript:{0}", _ButtonList[i].ButtonUrl);
                                break;
                            case UrlType.VBScript:
                                OnUrlJs = string.Format("VBScript:{0}", _ButtonList[i].ButtonUrl);
                                break;
                        }
                        if (_ButtonList[i].ButtonIcon != string.Empty)
                        {
                            ButtonIcon = HeadIconPath + _ButtonList[i].ButtonIcon;
                        }
                        else
                        {
                            ButtonIcon = HeadIconPath + _ButtonList[i].ButtonPopedom.ToString() + ".gif";
                            switch (_ButtonList[i].ButtonPopedom)
                            {
                                case PopedomType.Valid:
                                    ButtonTxt = "��Ч/��Ч";
                                    break;
                                case PopedomType.Audit:
                                    ButtonTxt = "���";
                                    break;
                                case PopedomType.Delete:
                                    ButtonTxt = "ɾ��";
                                    break;
                                case PopedomType.Edit:
                                    ButtonTxt = "�޸�";
                                    break;
                                case PopedomType.List:
                                    ButtonTxt = "�б�";
                                    break;
                                case PopedomType.Orderby:
                                    ButtonTxt = "����";
                                    break;
                                case PopedomType.New:
                                    ButtonTxt = "���";
                                    break;
                                case PopedomType.Print:
                                    ButtonTxt = "��ӡ";
                                    break;
                                case PopedomType.Other:
                                    ButtonTxt = "";
                                    ButtonIcon = "";
                                    break;
                            }
                        }
                        sb.AppendFormat("<td class=\"menubar_button\" id=\"button_{1}\" OnClick=\"{0}\" OnMouseOut=\"javascript:MenuOnMouseOver(this);\" OnMouseOver=\"javascript:MenuOnMouseOut(this);\">", OnUrlJs, i);
                        sb.AppendFormat("<img border=\"0\" align=\"texttop\" src=\"{0}\">&nbsp;", ButtonIcon);
                        sb.AppendFormat("{0}{1}</td>", ButtonTxt, _ButtonList[i].ButtonName);
                    }
                }
                if (DispButtonNum == 0)
                    sb.Append("<td>&nbsp;</td>");
                sb.Append("</tr></table>");
            }
            if (sb.ToString() == string.Empty)
                sb.Append("&nbsp");
            return sb.ToString();
        }


        #endregion

        #region ����
        /// <summary>
        /// ��ȡ/����ͷ���˵�·��
        /// </summary>
        [Description("��ȡ/����ͷ���˵�·��"), Category("���"), DefaultValue("~/images/icon/")]
        public string HeadIconPath
        {
            get
            {
                object m = ViewState["HeadIconPath"];
                return m == null ? ResolveClientUrl(_HeadIconPath) : ResolveClientUrl(m.ToString());
            }
            set
            {
                ViewState["HeadIconPath"] = value;
            }
        }
        /// <summary>
        /// ��ȡ/���ñ���IconͼƬ��
        /// </summary>
        [Description("��ȡ/���ñ���IconͼƬ��"), Category("���"), DefaultValue("default.gif")]
        public string HeadTitleIcon
        {
            get
            {
                object m = ViewState["HeadTitleIcon"];
                return m == null ? string.Format("{0}{1}", HeadIconPath, _HeadTitleIcon) : string.Format("{0}{1}", HeadIconPath, m);
            }
            set
            {
                ViewState["HeadTitleIcon"] = value;
            }
        }
        /// <summary>
        /// ��ȡ/���ñ�������
        /// </summary>
        [Description("��ȡ/���ñ�������"), Category("���"), DefaultValue("��������")]
        public string HeadTitleTxt
        {
            get
            {
                object m = ViewState["HeadTitleTxt"];
                return m == null ? _HeadTitleTxt : m.ToString();
            }
            set
            {
                ViewState["HeadTitleTxt"] = value;
            }
        }

        /// <summary>
        /// ��ȡ/���ð���Icon����
        /// </summary>
        [Description("��ȡ/���ð���IconͼƬ��"), Category("���"), DefaultValue("HelpIco.gif")]
        public string HeadHelpIcon
        {
            get
            {
                object m = ViewState["HeadHelpIcon"];
                return m == null ? string.Format("{0}{1}", HeadIconPath, _HeadHelpIcon) : string.Format("{0}{1}", HeadIconPath, m);
            }
            set
            {
                ViewState["HeadHelpIcon"] = value;
            }
        }
        /// <summary>
        /// ��ȡ/���ð�������
        /// </summary>
        [Description("��ȡ/���ð�������"), Category("���"), DefaultValue("������")]
        public string HeadHelpTxt
        {
            get
            {
                object m = ViewState["HeadHelpTxt"];
                return m == null ? _HeadHelpTxt : m.ToString();
            }
            set
            {
                ViewState["HeadHelpTxt"] = value;
            }
        }
        /// <summary>
        /// ��ȡ/���ò���˵��
        /// </summary>
        [Description("��ȡ/���ò���˵��"), Category("���"), DefaultValue("")]
        public string HeadOPTxt
        {
            get
            {
                object m = ViewState["HeadOPTxt"];
                return m == null ? _HeadOPTxt : m.ToString();
            }
            set
            {
                ViewState["HeadOPTxt"] = value;
            }
        }
        /// <summary>
        /// ��ť����
        /// </summary>
        [
        Category("Behavior"),
        Description("��ť����"),
        Editor(typeof(CollectionEditor), typeof(UITypeEditor)),
        PersistenceMode(PersistenceMode.InnerDefaultProperty)
        ]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<HeadMenuButtonItem> ButtonList
        {
            get
            {
                //object m = ViewState["ButtonList"];
                //return m == null ? _ButtonList : (List<HeadMenuButtonItem>)m;
                return _ButtonList;
            }
            //set
            //{
            //    ViewState["ButtonList"] = value;
            //}
        }

        /// <summary>
        /// ��дRenderContents����
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(HeadMenuTemplateTxt, HeadTitleIcon, HeadTitleTxt, HeadHelpIcon, HeadHelpTxt, HeadOPTxt, CreateButtonHtml());
        }
        #endregion
    }
}