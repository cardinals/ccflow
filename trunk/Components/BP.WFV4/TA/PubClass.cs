using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.Port;
using BP.DTS;

namespace BP.TA
{
	/// <summary>
	/// PubClass ��ժҪ˵����
	/// </summary>
	public class PubClass
	{
		public PubClass()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
       
		/// <summary>
		/// ���emps ����ȷ��
		/// </summary>
		/// <param name="emps"></param>
		public static string CheckEmps(string emps)
		{
			emps=emps.Replace(",," , "," );
			emps=emps.Replace(",,"  ,  "," );

			emps=emps.Replace(" "  ,  "" );
			emps=emps.Replace(" "  ,  "" );

			if (emps.Length==0)
				throw new Exception("������Ϊ��");

            string names = "";
			DataTable dt =DBAccess.RunSQLReturnTable("select No,Name from pub_emp");
			//Emps emps = new Emps();
			string errMsg="������Ա��Ŵ���:";		 
			string[] strs= emps.Split(',');
			foreach(string str in strs)
			{
				if (str==null)
					continue;
				if (str.Length==0)
					continue;
				if (str==",")
					continue;

				bool isHave=false;
				foreach(DataRow dr in dt.Rows)
				{
					if (str==dr[0].ToString())
					{
                        names += dr[1].ToString()+",";
						isHave=true;
						break;
					}
				}

				if (isHave==false)
					errMsg+="<br>�޴˽�����"+str;
			}
			if (errMsg!="������Ա��Ŵ���:")
				throw new Exception(errMsg);

            WebUser.Tag =names ;
			return emps;
		}
		 
	}
}
