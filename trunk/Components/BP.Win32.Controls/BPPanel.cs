
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BP.Win32.Controls
{
	/// <summary>
	/// ����
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.Panel))]
	public class BPPanel : System.Windows.Forms.Panel
	{
		/// <summary>
		/// 
		/// </summary>
		public BPPanel()
		{
		}
		/// <summary>
		/// ����ؼ�
		/// </summary>
		public Control ActivateControl
		{
			get
			{
				foreach(Control con in this.Controls)
				{
					if(con.Visible)
						return con;
				}
				return this.Controls[0];
			}
		}
		/// <summary>
		/// �������Ʋ��ҿؼ�
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private Control FindControl(string name)
		{
			foreach(Control con in this.Controls)
			{
				if(con.Name == name )
				{
					return con;
				}
			}
			return null;
		}
		/// <summary>
		/// ɾ���ؼ�
		/// </summary>
		/// <param name="name"></param>
		private void RemoveControl(string name)
		{
			Control tmp = FindControl(name);
			if(tmp != null)
			{
				this.Controls.Remove(tmp);
				tmp.Dispose();
			}
		}

//		public void AddUC( UCBase uc)
//		{
//			RemoveControl ( uc.Name );
//			if(this.Controls.Count>0)
//				this.ActivateControl.Hide();
//			uc.Location = new Point(0,0);
//			uc.Dock = DockStyle.Fill;
//			this.Controls.Add( uc );
//			uc.BringToFront();
//		}
		
	}
}
