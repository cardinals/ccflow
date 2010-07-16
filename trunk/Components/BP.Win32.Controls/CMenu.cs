using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BP.Win32.Controls
{
	/// <summary>
	/// ��ݲ˵����һ������˵���
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.ContextMenu))]
	public class CMenu : System.Windows.Forms.ContextMenu
	{
		public CMenu()
		{
		}
		/// <summary>
		/// ���Ӳ˵���
		/// </summary>
		/// <param name="text"></param>
		public void AddItem(string text)
		{
			MenuItem mi = new MenuItem();
			mi.Text = text;
			this.MenuItems.Add(mi) ; 
		}
	}
	/// <summary>
	/// �˵���Ŀ
	/// </summary>
	public class BPMenuItem:System.Windows.Forms.MenuItem
	{
		/// <summary>
		/// �˵���Ŀ
		/// </summary>
		public BPMenuItem()
		{
		}
		/// <summary>
		/// ���
		/// </summary>
		private string _Tag=null;
		/// <summary>
		/// ���
		/// </summary>
		public new string Tag
		{
			get
			{
				return _Tag;
			}
			set
			{
				_Tag=value;
			}
		}
	}
}
