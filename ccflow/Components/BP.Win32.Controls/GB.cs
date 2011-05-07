using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BP.Win32.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.GroupBox))]
	public class GB : System.Windows.Forms.GroupBox
	{
		
		#region ����
		public GB()
		{
			 
		}
		#endregion



		#region  find ctl
        public ComboBox GetComboBoxByKey(string key)
        {
            return (ComboBox)this.GetCtlByKey(key);

        }
		public DDL GetDDLByKey(string key)
		{ 
			return (DDL)this.GetCtlByKey(key);
			
		}

		public TB GetTBByKey(string key)
		{ 
			return (TB)this.GetCtlByKey(key);
		}
		public CB GetCBByKey(string key)
		{ 
			return (CB)this.GetCtlByKey(key);
		}
		public DateTimePicker GetDateByKey(string key)
		{ 
			return (DateTimePicker)this.GetCtlByKey(key);
		}

		public Control GetCtlByKey(string key)
		{
			foreach(Control ctl in this.Controls)
			{
				if (ctl.Name==key)
				{
					return  ctl;
				}
			}
			throw new Exception("û���ҵ�name="+key+"�Ŀؼ�.");
		}
		/// <summary>
		/// �Ƿ����ָ��key �� �ؼ���
		/// </summary>
		/// <param name="key">ָ����key</param>
		/// <returns>true/false</returns>
		public bool IsContains(string key)
		{
			foreach(Control ctl in this.Controls)
			{
				if (ctl.Name==key)
				{
					return  true;
				}
			}
			return false;
		}
		#endregion

	}
}
