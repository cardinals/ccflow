using System;
using System.Collections.Generic;
using System.Text;

namespace BP.En20
{
    public class Function
    {
public static string getUserIP() 
{ 
return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); 
} 
/// <summary> 
/// ȥ���ַ������һ��','�� 
/// </summary> 
/// <param name="chr">:Ҫ��������ַ���</param> 
/// <returns>�����Ѵ�����ַ���</returns> 
public static string Lost(string chr) 
{ 
if (chr == null || chr == string.Empty) 
{ 
return ""; 
} 
else 
{ 
chr = chr.Remove(chr.LastIndexOf(",")); 
return chr; 
} 
} 
/// <summary> 
/// ȥ���ַ�����һ��'/'�� 
/// </summary> 
/// <param name="chr">Ҫ��������ַ���</param> 
/// <returns>�����Ѵ�����ַ���</returns> 
public static string lostfirst(string chr) 
{ 
string flg = ""; 
if (chr != string.Empty || chr != null) 
{ 
if (chr.Substring(0, 1) == "/") 
flg = chr.Replace(chr.Substring(0, 1), ""); 
else 
flg = chr; 
} 
return flg; 
} 
/// <summary> 
/// �滻html�е������ַ� 
/// </summary> 
/// <param name="theString">��Ҫ�����滻���ı���</param> 
/// <returns>�滻����ı���</returns> 
public static string HtmlEncode(string theString) 
{ 
theString = theString.Replace(">", ">"); 
theString = theString.Replace("<", "<"); 
theString = theString.Replace(" ", " "); 
theString = theString.Replace(" ", " "); 
theString = theString.Replace("\"", """) ;
theString = theString.Replace("\'", "'"); 
theString = theString.Replace("\n", "<br/> "); 
return theString; 
} 
/// <summary> 
/// �ָ�html�е������ַ� 
/// </summary> 
/// <param name="theString">��Ҫ�ָ����ı���</param> 
/// <returns>�ָ��õ��ı���</returns> 
public static string HtmlDiscode(string theString) 
{ 
theString = theString.Replace(">", ">"); 
theString = theString.Replace("<", "<"); 
theString = theString.Replace(" ", " "); 
theString = theString.Replace(" ", " "); 
theString = theString.Replace(""", "\""); 
theString = theString.Replace("'", "\'"); 
theString = theString.Replace("<br/> ", "\n"); 
return theString; 
} 

/// <summary> 
/// ����������� 
/// </summary> 
/// <param name="length">���ɳ���</param> 
/// <returns></returns> 
public static string Number(int Length) 
{ 
return Number(Length, false); 
} 
/// <summary> 
/// ����������� 
/// </summary> 
/// <param name="Length">���ɳ���</param> 
/// <param name="Sleep">�Ƿ�Ҫ������ǰ����ǰ�߳���ֹ�Ա����ظ�</param> 
/// <returns></returns> 
public static string Number(int Length, bool Sleep) 
{ 
if (Sleep) 
System.Threading.Thread.Sleep(3); 
string result = ""; 
System.Random random = new Random(); 
for (int i = 0; i < Length; i++) 
{ 
result += random.Next(10).ToString(); 
} 
return result; 
}

    }
    
}
