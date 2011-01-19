using System;
using System.Reflection;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

namespace Tax666.WebControls
{
    /// <summary>
    /// ѡ�����
    /// </summary>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class TaboptionItemCollection : ControlCollection
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="owner"></param>
        public TaboptionItemCollection(Control owner)
            : base(owner)
        {
        }
        /// <summary>
        /// ���ӿؼ�
        /// </summary>
        /// <param name="v"></param>
        public override void Add(Control v)
        {
            if (!(v is TabOptionItem))
            {
                throw new ArgumentException("ViewCollection_must_contain_view");
            }
            base.Add(v);
        }
        /// <summary>
        /// ���ӿؼ�
        /// </summary>
        /// <param name="index"></param>
        /// <param name="v"></param>
        public override void AddAt(int index, Control v)
        {
            if (!(v is TabOptionItem))
            {
                throw new ArgumentException("ViewCollection_must_contain_view");
            }
            base.AddAt(index, v);
        }
        /// <summary>
        /// ��ȡTabOptionItem
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public new TabOptionItem this[int i]
        {
            get
            {
                return (TabOptionItem)base[i];
            }
        }
    }
}
