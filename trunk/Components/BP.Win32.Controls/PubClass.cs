using System;
using BP.Win32.Controls ;

using BP.En ;
using System.Windows.Forms;


namespace BP.Win32
{
	/// <summary>
	/// Class2 ��ժҪ˵����
	/// </summary>
	public class PubClass
	{
		public static bool Warning(string msg)
		{
			if (MessageBox.Show(msg,"ִ��ȷ��", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2,MessageBoxOptions.DefaultDesktopOnly)==DialogResult.No)
				return false;
			return true;
		}
		public static bool Question(string msg)
		{
            if (MessageBox.Show(msg, BP.Sys.Language.GetValByUserLang("PChose", "��ѡ��"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.No)
				return false;
			return true;
		}
		public static void Information(string msg)
		{
			MessageBox.Show(msg, "��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}

		public static void Alert(Exception ex)
		{
			UIAlert ui = new UIAlert();
			ui.Show(ex.Message) ;
			ui.ShowDialog();
		}
		public static void Alert(string msg)
		{
            MessageBox.Show(msg, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //UIAlert ui = new UIAlert();
            //ui.Show(msg) ;
            //ui.ShowDialog();
		}
		/// <summary>
		/// ��һ�����������ҵ����ĺ��ӽڵ㡣
		/// </summary>
		/// <param name="ens"></param>
		/// <param name="en"></param>
		/// <returns></returns>
		//		public static GradeEntitiesNoNameBase GetHisChildEns(GradeEntitiesNoNameBase ens, GradeEntityNoNameBase en)
		//		{
		//
		//		}
	}
	
}
