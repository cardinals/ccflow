using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using Tax666.Common;

namespace Tax666.WebControls
{
    /// <summary>
    /// �˵���ť
    /// </summary>
    [TypeConverter(typeof(TypeConverter))]
    public class HeadMenuButtonItem
    {
        #region ˽�б���
        private string _ButtonName;
        private string _ButtonUrl;
        private PopedomType _ButtonPopedom;
        private UrlType _ButtonUrlType = UrlType.Href;
        private string _ButtonIcon;
        private bool _ButtonVisible = true;
        #endregion

        #region ���캯��������
        /// <summary>
        /// ���캯��
        /// </summary>
        public HeadMenuButtonItem()
            : this(String.Empty, String.Empty, PopedomType.List, UrlType.Href, string.Empty, true)
        {
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="_ButtonName">��ť����</param>
        /// <param name="_ButtonUrl">��ť����</param>
        /// <param name="_ButtonPopedom">��ť����Ȩ��</param>
        /// <param name="_ButtonUrlType">��ť��������</param>
        /// <param name="_ButtonIcon">��ťIcon</param>
        /// <param name="_ButtonVisible">�Ƿ���ʾ</param>
        public HeadMenuButtonItem(string _ButtonName, string _ButtonUrl, PopedomType _ButtonPopedom,
            UrlType _ButtonUrlType, string _ButtonIcon, bool _ButtonVisible
            )
        {
            this._ButtonIcon = _ButtonIcon;
            this._ButtonName = _ButtonName;
            this._ButtonPopedom = _ButtonPopedom;
            this._ButtonUrl = _ButtonUrl;
            this._ButtonUrlType = _ButtonUrlType;
            this._ButtonVisible = _ButtonVisible;
        }

        /// <summary>
        /// ��ť����
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(""),
        Description("��ť����"),
        NotifyParentProperty(true),
        ]
        public string ButtonName
        {
            get
            {
                return _ButtonName;
            }
            set
            {
                _ButtonName = value;
            }
        }
        /// <summary>
        /// ��ť����
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(""),
        Description("��ť����"),
        NotifyParentProperty(true),
        ]
        public string ButtonUrl
        {
            get
            {
                return _ButtonUrl;
            }
            set
            {
                _ButtonUrl = value;
            }
        }
        /// <summary>
        /// ��ť����Ȩ��
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(""),
        Description("��ť����Ȩ��"),
        NotifyParentProperty(true),
        ]
        public PopedomType ButtonPopedom
        {
            get
            {
                return _ButtonPopedom;
            }
            set
            {
                _ButtonPopedom = value;
            }
        }
        /// <summary>
        /// ��ť�������� Ĭ��Ϊurl
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(""),
        Description("��ť�������� Ĭ��Ϊurl"),
        NotifyParentProperty(true),
        ]
        public UrlType ButtonUrlType
        {
            get
            {
                return _ButtonUrlType;
            }
            set
            {
                _ButtonUrlType = value;
            }
        }
        /// <summary>
        /// ��ťIcon Ĭ��Ϊ��
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(""),
        Description("��ťIcon Ĭ��Ϊ��"),
        NotifyParentProperty(true),
        ]
        public string ButtonIcon
        {
            get
            {
                return _ButtonIcon;
            }
            set
            {
                _ButtonIcon = value;
            }
        }
        /// <summary> 
        /// ��ť�Ƿ���ʾ Ĭ��Ϊtrue
        /// </summary>

        [
        Category("Behavior"),
        DefaultValue(""),
        Description("��ť�Ƿ���ʾ Ĭ��Ϊtrue"),
        NotifyParentProperty(true),
        ]
        public bool ButtonVisible
        {
            get
            {
                return _ButtonVisible;
            }
            set
            {
                _ButtonVisible = value;
            }
        }
        #endregion
    }
}
