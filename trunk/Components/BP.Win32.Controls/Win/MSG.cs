using System;
using System.Windows.Forms;

namespace BP.Win.Controls
{
	/// <summary>
	/// MSG ��ժҪ˵����
	/// </summary>
	public class MSG
	{
		public MSG()
		{
		}

		public static DialogResult ShowQuestion(string text ,string caption)
		{
			return MessageBox.Show(text,caption,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
		}
	}
}
