using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Globalization;

[assembly: WebResource("Tax666.WebControls.Resource.moveAllLeft.gif", "image/gif")]
[assembly: WebResource("Tax666.WebControls.Resource.moveAllLeft2.gif", "image/gif")]
[assembly: WebResource("Tax666.WebControls.Resource.moveAllRight.gif", "image/gif")]
[assembly: WebResource("Tax666.WebControls.Resource.moveAllRight2.gif", "image/gif")]
[assembly: WebResource("Tax666.WebControls.Resource.moveLeft.gif", "image/gif")]
[assembly: WebResource("Tax666.WebControls.Resource.moveLeft2.gif", "image/gif")]
[assembly: WebResource("Tax666.WebControls.Resource.moveRight.gif", "image/gif")]
[assembly: WebResource("Tax666.WebControls.Resource.moveRight2.gif", "image/gif")]
[assembly: WebResource("Tax666.WebControls.Resource.sortDown.gif", "image/gif")]
[assembly: WebResource("Tax666.WebControls.Resource.sortDown2.gif", "image/gif")]
[assembly: WebResource("Tax666.WebControls.Resource.sortUp.gif", "image/gif")]
[assembly: WebResource("Tax666.WebControls.Resource.sortUp2.gif", "image/gif")]
[assembly: WebResource("Tax666.WebControls.Resource.MultiListBox.js", "text/javascript")]

namespace Tax666.WebControls
{
    /// <summary>
    /// Multi-ListBox�ؼ�
    /// </summary>
    [ToolboxData("<{0}:MultiListBox runat=\"server\"></{0}:MultiListBox")]
    [
    ParseChildren(true),
    PersistChildren(false),
    ]
    public class MultiListBox : CompositeControl, IPostBackDataHandler
    {
        #region Fields
        private MultiListBoxItem _firstListItem = new MultiListBoxItem();
        private MultiListBoxItem _secondListItem = new MultiListBoxItem();
        //private bool _stateLoaded = false;
        /// <summary>
        /// ��ʾ��
        /// </summary>
        protected int _rows = 4;
        //private bool _marked = false;
        private string _separator;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void RaisePostDataChangedEvent()
        {

        }

        #region AddAttributesToRender
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Width, this.Width);
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "1");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            //ע��ͻ��˲����ű�
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<script language=\"javascript\">{0}", Environment.NewLine);
            sb.AppendFormat("var multiList=new MultiListBox(document.getElementById('{1}_firstListBox'),document.getElementById('{1}_secondListBox'),'{1}_');{0}", Environment.NewLine, this.ClientID);
            sb.AppendFormat("var img_AllLeft_have=\"{1}\";{0}", Environment.NewLine, GetWebResourceUrl("moveAllLeft.gif"));
            sb.AppendFormat("var img_AllLeft_has=\"{1}\";{0}", Environment.NewLine, GetWebResourceUrl("moveAllLeft2.gif"));
            sb.AppendFormat("var img_AllRight_have=\"{1}\";{0}", Environment.NewLine, GetWebResourceUrl("moveAllRight.gif"));
            sb.AppendFormat("var img_AllRight_has=\"{1}\";{0}", Environment.NewLine, GetWebResourceUrl("moveAllRight2.gif"));
            sb.AppendFormat("var img_Left_have=\"{1}\"{0};", Environment.NewLine, GetWebResourceUrl("moveLeft.gif"));
            sb.AppendFormat("var img_Left_has=\"{1}\";{0}", Environment.NewLine, GetWebResourceUrl("moveLeft2.gif"));
            sb.AppendFormat("var img_Right_have=\"{1}\";{0}", Environment.NewLine, GetWebResourceUrl("moveRight.gif"));
            sb.AppendFormat("var img_Right_has=\"{1}\";{0}", Environment.NewLine, GetWebResourceUrl("moveRight2.gif"));
            sb.Append("</script>");

            writer.Write(sb.ToString());
        }
        #endregion

        #region RenderContents
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "45%");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            //��ʾFirstListBox�б��
            RenderOptionsContents(writer, FirstListBox, "firstListBox", "multiList.transferRight()");
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "10%");
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");

            //���

            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            //���ͼ��
            RenderMultiIcon(writer);
            //
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "45%");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            //��ʾSecondListBox�б��
            RenderOptionsContents(writer, SecondListBox, "secondListBox", "multiList.transferLeft()");
            writer.RenderEndTag();
            writer.RenderEndTag();


        }
        #endregion

        #region Propertity
        /// <summary>
        /// 
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Table;
            }
        }

        /// <summary>
        /// ��ȡ������Ϊ�б����ṩ�ı����ݵ�����Դ�ֶ�
        /// </summary>
        [WebCategory("Data"), Browsable(true), Description("��ȡ������Ϊ�б����ṩ�ı����ݵ�����Դ�ֶ�")]
        public string DataTextField
        {
            get
            {
                object objTextField = this.ViewState["DataTextField"];
                if (objTextField != null)
                {
                    return (string)objTextField;
                }
                return string.Empty;

            }
            set
            {
                this.ViewState["DataTextField"] = value;
            }
        }

        /// <summary>
        /// ��ȡ������Ϊ�б����ṩֵ������Դ�ֶ�
        /// </summary>
        [WebCategory("Data"), Browsable(true), Description("��ȡ������Ϊ�б����ṩֵ������Դ�ֶ�")]
        public string DataValueField
        {
            get
            {
                object objValueField = this.ViewState["DataValueField"];
                if (objValueField != null)
                {
                    return (string)objValueField;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["DataValueField"] = value;
            }

        }

        /// <summary>
        /// ��ȡ�����ø�ʽ���ַ��������ַ����������������ʾ�󶨵��б�ؼ������ݡ����� ListControl �̳С���
        /// </summary>
        [WebCategory("Data"), Browsable(true), Description("��ȡ�����ø�ʽ���ַ��������ַ����������������ʾ�󶨵��б�ؼ�������")]
        protected string DataTextFormatString
        {
            get
            {
                object objTextFormat = this.ViewState["DataTextFormatString"];
                if (objTextFormat != null)
                {
                    return (string)objTextFormat;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["DataTextFormatString"] = value;
            }

        }

        /// <summary>
        /// ��һ���б��ؼ�(Դ�б��)
        /// </summary>
        [Browsable(false)]
        //[NotifyParentProperty(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public MultiListBoxItem FirstListBox
        {
            get
            {
                if (_firstListItem == null)
                    _firstListItem = new MultiListBoxItem();
                return _firstListItem;
            }
        }

        /// <summary>
        /// �ڶ����б��ؼ�(Ŀ���б��)
        /// </summary>
        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public MultiListBoxItem SecondListBox
        {
            get
            {
                if (_secondListItem == null)
                    _secondListItem = new MultiListBoxItem();
                return _secondListItem;
            }
        }

        /// <summary>
        /// ��ȡ�����ÿؼ���ʾ�Ŀ��
        /// </summary>
        [WebCategory("Style"), Browsable(true), Description("��ȡ�����ÿؼ���ʾ�Ŀ��")]
        public new string Width
        {
            get
            {
                string val = Attributes["width"];
                if (val != null)
                {
                    return val;
                }
                return string.Empty;
            }
            set
            {
                if (value == null || value == string.Empty)
                    Attributes.Remove("width");
                else
                    Attributes["width"] = value;
            }
        }

        /// <summary>
        /// ��ȡ�����ÿؼ���ʾ�ĸ߶�
        /// </summary>
        [WebCategory("Style"), Browsable(true), Description("��ȡ�����ÿؼ���ʾ�ĸ߶�")]
        public string Heigth
        {
            get
            {
                string val = Attributes["height"];
                if (val == null)
                    return string.Empty;
                return val;
            }
            set
            {
                if (value == null || value == string.Empty)
                    Attributes.Remove("height");
                else
                    Attributes["height"] = value;
            }
        }

        /// <summary>
        /// ����ListBox�б���м���
        /// </summary>
        public string Separator
        {
            get { return _separator; }
            set { _separator = value; }
        }
        /// <summary>
        /// ��ȡ�������б��ؼ���ʾ����
        /// </summary>
        [WebCategory("Style"), Browsable(true), Description("��ȡ�������б��ؼ���ʾ����")]
        public int Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                _rows = value;
            }
        }

        /// <summary>
        /// �б��ѡ��ģʽ
        /// </summary>
        [WebCategory("Behavior"), Browsable(true), DefaultValue(0), Description("�б��ѡ��ģʽ")]
        public ListSelectionMode SelectionMode
        {
            get
            {
                object objMode = this.ViewState["SelectionMode"];
                if (objMode != null)
                {
                    return (ListSelectionMode)objMode;
                }
                return ListSelectionMode.Single;

            }
            set
            {
                if ((value < ListSelectionMode.Single) || (value > ListSelectionMode.Multiple))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.ViewState["SelectionMode"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected string HFItemsAdded
        {
            get { return this.ClientID + "_ADDED"; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected string HFItemsRemoved
        {
            get { return this.ClientID + "_REMOVED"; }
        }
        #endregion

        #region ViewStates
        /// <summary>
        /// 
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                Triplet triplet = (Triplet)savedState;
                base.LoadViewState(triplet.First);
                Reflector.InvokeMethod(this.FirstListBox.Items, "LoadViewState", new object[] { triplet.Second });
                Reflector.InvokeMethod(this.SecondListBox.Items, "LoadViewState", new object[] { triplet.Third });
            }
            else
            {
                base.LoadViewState(null);
            }
            //this._stateLoaded = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            if (EnableViewState == false)
                return null;
            //���ÿؼ���ͼ״̬
            object x = base.SaveViewState();
            object y = Reflector.InvokeMethod(FirstListBox.Items, "SaveViewState", null);
            object z = Reflector.InvokeMethod(SecondListBox.Items, "SaveViewState", null);
            if ((x == null) && (y == null) && (z == null))
            {
                return null;
            }
            return new Triplet(x, y, z);
        }

        #endregion



        #region OnDataBinding
        /// <summary>
        /// ������Դ
        /// </summary>
        public new void DataBind()
        {
            PerformanceData(FirstListBox);
            PerformanceData(SecondListBox);
        }

        /// <summary>
        /// ��DataSourceת����ָ����Items
        /// </summary>
        /// <param name="listItem"></param>
        protected void PerformanceData(MultiListBoxItem listItem)
        {
            object dataSource = listItem.DataSource;
            if (dataSource != null)
            {
                bool flag_display = false;
                bool flag_format = false;

                string propName = this.DataTextField;
                string dataValueField = this.DataValueField;
                string format = this.DataTextFormatString;

                ICollection data = dataSource as ICollection;
                if (data != null)
                {
                    listItem.Items.Capacity = listItem.Items.Count + data.Count;
                }
                if (propName.Length != 0 || dataValueField.Length != 0)
                    flag_display = true;
                if (this.DataTextFormatString.Length != 0)
                    flag_format = true;
                foreach (object obj in data)
                {
                    ListItem item = new ListItem();
                    //�Զ�����ʾTextField,ValueField
                    if (flag_display)
                    {
                        if (propName.Length > 0)
                        {
                            item.Text = DataBinder.GetPropertyValue(obj, propName).ToString();
                        }
                        if (dataValueField.Length > 0)
                        {
                            item.Value = DataBinder.GetPropertyValue(obj, dataValueField).ToString();
                        }
                    }
                    else
                    {
                        if (flag_format)
                        {
                            item.Text = string.Format(CultureInfo.CurrentCulture, format, new object[] { obj });
                        }
                        else
                        {
                            item.Text = obj.ToString();
                        }
                    }
                    listItem.Items.Add(item);
                }
            }
        }

        #endregion

        #region OnPreRender
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (Page != null)
                Page.RegisterRequiresPostBack(this);

            if (!Page.ClientScript.IsClientScriptBlockRegistered("MultiListBox-JavaScriptKey"))
            {
                //ע��ű�
                string jsResource = Page.ClientScript.GetWebResourceUrl(typeof(MultiListBox), "Tax666.WebControls.Resource.MultiListBox.js");
                Page.ClientScript.RegisterClientScriptInclude("MultiListBox-JavaScriptKey", jsResource);

                //ע��������
                Page.ClientScript.RegisterHiddenField(this.HFItemsAdded, "");

                Page.ClientScript.RegisterHiddenField(this.HFItemsRemoved, "");

            }

        }
        #endregion

        #region IPostBackDataHandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postDataKey"></param>
        /// <param name="postCollection"></param>
        /// <returns></returns>
        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            bool resultValueFlag = false;
            //�Ƴ�ָ��ListItem������Ҫ�����Left ListBox�б����
            string itemsRemoved = postCollection[this.ClientID + "_REMOVED"];
            string[] itemsRemovedCol = itemsRemoved.Split(',');
            if (itemsRemovedCol != null)
            {
                if (itemsRemovedCol.Length > 0 && itemsRemovedCol[0] != "")
                {
                    for (int i = 0; i < itemsRemovedCol.Length; i++)
                    {
                        string[] itemsRemoveItems = itemsRemovedCol[i].Split('|');
                        ListItem item = this.SecondListBox.Items.FindByValue(itemsRemoveItems[1]);
                        if (item != null)
                        {
                            this.SecondListBox.Items.Remove(item);
                        }
                        item = this.FirstListBox.Items.FindByValue(itemsRemoveItems[1]);
                        if (item == null)
                        {

                            this.FirstListBox.Items.Add(new ListItem(itemsRemoveItems[0], itemsRemoveItems[1]));
                        }
                        resultValueFlag = true;
                    }
                }
            }
            //�ӿͻ������ָ����ListItem
            string itemsAdded = postCollection[this.ClientID + "_ADDED"];
            string[] itemsAddedCol = itemsAdded.Split(',');
            if (itemsAddedCol != null)
            {
                if (itemsAddedCol.Length > 0 && itemsAddedCol[0] != "")
                {
                    int counter = -1;
                    for (int i = 0; i < itemsAddedCol.Length; i++)
                    {
                        string[] itemsAddItems = itemsAddedCol[i].Split('|');
                        ListItem item = this.SecondListBox.Items.FindByValue(itemsAddItems[1]);
                        if (item == null)
                        {
                            this.SecondListBox.Items.Add(new ListItem(itemsAddItems[0], itemsAddItems[1]));
                            counter += 1;
                        }
                        item = this.FirstListBox.Items.FindByValue(itemsAddItems[1]);
                        if (item != null)
                        {
                            this.FirstListBox.Items.Remove(item);
                        }
                    }
                    resultValueFlag = counter > -1 ? true : false;
                }
            }

            //�ӿͻ������Ƴ�ָ����ListItem
            return resultValueFlag;
        }

        #endregion

        #region Functionality
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="multiListItem"></param>
        /// <param name="name"></param>
        /// <param name="jsDbClick"></param>
        protected void RenderOptionsContents(HtmlTextWriter writer, MultiListBoxItem multiListItem, string name, string jsDbClick)
        {
            ListItemCollection items = multiListItem.Items;
            writer.AddAttribute("name", this.ClientID + "_" + name);
            writer.AddAttribute("id", this.ClientID + "_" + name);
            writer.AddAttribute("size", this.Rows.ToString());
            writer.AddAttribute("onDblClick", jsDbClick);
            if (SelectionMode == ListSelectionMode.Multiple)
                writer.AddAttribute(HtmlTextWriterAttribute.Multiple, "multiple");
            //���CSS����
            if (multiListItem.StyleSheet != null)
            {
                StringBuilder sb = new StringBuilder();
                string sheetValue = multiListItem.StyleSheet.Width;
                if (sheetValue != null && sheetValue != "")
                    sb.AppendFormat("width:{0};", sheetValue);
                sheetValue = multiListItem.StyleSheet.Height;
                if (sheetValue != null && sheetValue != "")
                    sb.AppendFormat("height:{0};", sheetValue);
                writer.AddAttribute("style", sb.ToString());
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Select);
            int count = items.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    ListItem item = items[i];
                    writer.AddAttribute("value", item.Value, true);
                    writer.RenderBeginTag(HtmlTextWriterTag.Option);
                    writer.Write(item.Text);
                    writer.RenderEndTag();
                }
            }
            writer.RenderEndTag();
        }

        /// <summary>
        /// ��ʾ����ͼ��
        /// </summary>
        /// <param name="writer"></param>
        protected void RenderMultiIcon(HtmlTextWriter writer)
        {
            //���Div
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            RenderImageContents(writer, "moveAllRight.gif", "multiList.transferAllLeft();", "_AllRight");//ȫ������
            RenderImageContents(writer, "moveRight.gif", "multiList.transferRight();", "_Right");
            RenderImageContents(writer, "moveLeft.gif", "multiList.transferLeft();", "_Left");
            RenderImageContents(writer, "moveAllLeft.gif", "multiList.transferAllRight();", "_AllLeft");
            writer.RenderEndTag();
        }

        private string GetWebResourceUrl(string img)
        {
            return Page.ClientScript.GetWebResourceUrl(typeof(MultiListBox), "Tax666.WebControls.Resource." + img);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="imgFileName"></param>
        /// <param name="jsClickName"></param>
        /// <param name="imgClientName"></param>
        private void RenderImageContents(HtmlTextWriter writer, string imgFileName, string jsClickName, string imgClientName)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.AddAttribute(HtmlTextWriterAttribute.Src, GetWebResourceUrl(imgFileName));
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + imgClientName);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientID + imgClientName);
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "cursor:pointer");
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, jsClickName);
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
            writer.RenderEndTag();
        }
        #endregion
    }

    /// <summary>
    /// Multi-ListItem
    /// </summary>
    public class MultiListBoxItem
    {

        #region Fields
        private ListItemCollection items;
        private object _dataSource = null;
        private string _style = string.Empty;
        private MultiListBoxStyle _styleSheet = new MultiListBoxStyle();
        #endregion


        #region Propertity
        /// <summary>
        /// ��ȡ�б�ؼ���ļ���
        /// </summary>
        [Browsable(false)]
        public ListItemCollection Items
        {
            get
            {
                if (items == null)
                {
                    items = new ListItemCollection();
                }
                return items;
            }
        }

        /// <summary>
        /// ��ȡ�������б�ؼ�������Դ
        /// </summary> 
        [Browsable(false)]
        public object DataSource
        {
            get
            {
                ValidateDataSource();
                return this._dataSource;
            }
            set
            {
                this._dataSource = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true), Description("Css��ʽ����")]
        public string Style
        {
            get { return _style; }
            set { _style = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true), Description("Css��ʽ����,�����߶ȣ���ȡ����������<Style>��ʽ���ƣ������Կ��Ժ���")]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public MultiListBoxStyle StyleSheet
        {
            get { return _styleSheet; }
            set { _styleSheet = value; }
        }
        #endregion

        /// <summary>
        /// ��֤����Դ
        /// </summary>
        protected void ValidateDataSource()
        {
            if (_dataSource != null && !(_dataSource is ICollection) && !(_dataSource is IEnumerable) && !(_dataSource is IListSource))
            {
                throw new InvalidOperationException("DataBoundControl_InvalidDataSourceType");
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Reflector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object InvokeMethod(ListItemCollection items, string methodName, object[] parameters)
        {
            Type type = items.GetType();
            FieldInfo fieldInfo = type.GetField("saveAll", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Default);
            fieldInfo.SetValue(items, (object)true);//
            MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Default | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (methodInfo != null)
            {
                return methodInfo.Invoke(items, parameters);
            }
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MultiListBoxStyle
    {
        #region Fields
        private string _width;
        private string _height;

        #endregion

        #region Propertity
        /// <summary>
        /// ��
        /// </summary>
        public string Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// ��
        /// </summary>
        public string Height
        {
            get { return _height; }
            set { _height = value; }
        }

        #endregion
    }

    [AttributeUsage(AttributeTargets.All)]
    internal sealed class WebCategoryAttribute : CategoryAttribute
    {
        /// <summary>
        /// ʹ��ָ����<paramref name="category"/>����
        /// </summary>
        /// <param name="category"></param>
        internal WebCategoryAttribute(string category)
            : base(category)
        {
        }

        public override object TypeId
        {
            get
            {
                return typeof(CategoryAttribute);
            }
        }

    }
}
