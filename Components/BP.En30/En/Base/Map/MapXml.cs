using System;
using BP.XML;

namespace BP.En.Base
{
	/// <summary>
	/// MapXml ��ժҪ˵����
	/// </summary>
	public class Base:BP.XML.XmlEn
	{
		public Base()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
	public class Bases:BP.XML.XmlEns
	{
		public Bases()
		{

		}
		public override string File
		{
			get
			{
				return null;
			}
		}
		public override Entities RefEns
		{
			get
			{
				return null;
			}
		}
		public override string TableName
		{
			get
			{
				return null;
			}
		}
	}
}
