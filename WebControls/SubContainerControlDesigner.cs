using System;
using System.Collections;
using System.Collections.Specialized;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;

namespace Tax666.WebControls
{
    /// <summary>
    /// ѡ��༭��
    /// </summary>
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class MyContainerControlDesigner : ContainerControlDesigner
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public MyContainerControlDesigner()
        {
            base.FrameStyle.Width = Unit.Percentage(100);
        }
    }
}
