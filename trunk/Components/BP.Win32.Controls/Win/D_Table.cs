using System;
using System.Data;

namespace BP.Win.Controls
{
	/// <summary>
	/// D_Table ��ժҪ˵����
	/// </summary>
	public class D_Table:DataTable
	{
		public D_Table()
		{
		}
		public D_Table(string tbname):base(tbname)
		{
		}
		public DataView GetCheckDate(string cols)
		{
			return null;
		}
	}
}
