using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Specialized;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Drawing.Design;

namespace Tax666.WebControls
{
    /// <summary>
    /// ѡ�
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Designer(typeof(MyContainerControlDesigner))]
    [ParseChildren(false)]
    [PersistChildren(true)]
    public class TabOptionItem : Control
    {
        #region "Private Variables"
        private string _Tab_Name;
        private bool _Tab_Visible = true;
        #endregion

        #region "Public Variables"
        /// <summary>
        /// ѡ�����
        /// </summary>
        public string Tab_Name
        {
            get
            {
                return _Tab_Name;
            }
            set
            {
                _Tab_Name = value;
            }
        }
        /// <summary>
        /// ѡ��Ƿ���ʾ
        /// </summary>
        public bool Tab_Visible
        {
            get
            {
                return _Tab_Visible;
            }
            set
            {
                _Tab_Visible = value;
            }
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        public TabOptionItem()
            : this(String.Empty, true)
        {
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="_Tab_Name"></param>
        /// <param name="_Tab_Visible"></param>
        public TabOptionItem(string _Tab_Name, bool _Tab_Visible)
        {
            this._Tab_Name = _Tab_Name;
            this._Tab_Visible = _Tab_Visible;
        }
        #endregion
    }
}
