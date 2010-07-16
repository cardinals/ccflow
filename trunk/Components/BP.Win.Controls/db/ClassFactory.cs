using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.IO;


namespace BP.Win.Controls
{
	public class ClassFactory
	{
		#region ���캯��
		static ClassFactory()
		{
			ArrayList arr = new ArrayList();
			foreach( Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
			{
				if( ass.FullName.IndexOf("BP.")!=-1 )
					arr.Add( ass );
			}
			BPAssemblies = new Assembly[arr.Count];
			for(int i=0;i<arr.Count;i++)
				BPAssemblies[i] = arr[i] as Assembly;
		}
		#endregion ���캯��

		#region ����
		/// <summary>
		/// ��ȡȡ����[dll]
		/// </summary>
		/// <returns></returns>
		public static readonly Assembly[] BPAssemblies = null;

		#endregion ����


		#region ���� 
		public static Type GetBPType(string className)
		{
			Type typ = null;
			foreach(Assembly ass in BPAssemblies)
			{
				typ = ass.GetType(className);
				if(typ != null)
					return typ;
			}
			return typ ;
		}
		
		public static ArrayList GetBPTypes(string baseClassName )
		{
			ArrayList arr = new ArrayList();
			Type baseClass =null;
			foreach(Assembly ass in BPAssemblies)
			{
				if(baseClass ==null)
					baseClass = ass.GetType( baseClassName);
				Type[] tps = ass.GetTypes();
				for(int i=0; i<tps.Length ;i++)
				{
					if( tps[i].IsAbstract 
						|| tps[i].BaseType==null
						|| !tps[i].IsClass
						|| !tps[i].IsPublic 
						)
						continue;
					Type tmp = tps[i].BaseType;
					while( tmp!=null && tmp.Namespace.IndexOf("BP")!=-1 )
					{
						if(tmp.FullName == baseClassName )
							arr.Add( tps[i] );
						tmp = tmp.BaseType;
					}
				}
			}
			if(baseClass ==null)
			{
				throw new Exception("�Ҳ�������"+baseClassName+"��");
			}
			return arr ;
		}

		public static bool IsFromType(string childTypeFullName , string parentTypeFullName)
		{
			foreach(Assembly ass in BPAssemblies)
			{
				Type childType = ass.GetType(childTypeFullName);
				while( childType!=null && childType.BaseType!=null )
				{
					if( childType.BaseType.FullName ==parentTypeFullName )
						return true;
					childType = childType.BaseType;
				}
			}
			return false;
		}
		#endregion ����


		#region ����ʵ��

		public static object GetObject(string className)
		{
			if (className=="" || className==null)
				throw new Exception("Ҫת��������Ϊ��...");
			
			Type ty = null;
			object obj=null;
			foreach(Assembly ass in BPAssemblies)
			{
				ty = ass.GetType(className);
				if(ty==null)
					continue;
				obj = ass.CreateInstance(className);
				if(obj!=null)
					return obj;
				else
					throw new Exception("��������ʵ�� "+className+" ʧ�ܣ�");
			}
			if(obj==null)
				throw new Exception("������������ "+className+" ʧ�ܣ�");
			return obj ;
		}
		public static ArrayList GetObjects(string baseClassName)
		{
			ArrayList arr = new ArrayList();
			Type baseClass =null;
			foreach(Assembly ass in BPAssemblies)
			{
				if(baseClass ==null)
					baseClass = ass.GetType( baseClassName);
				Type[] tps = new Type[0];
//				try
//				{
					tps= ass.GetTypes();
//				}
//				catch{}
				for(int i=0; i<tps.Length ;i++)
				{
					if(tps[i].IsAbstract 
						|| tps[i].BaseType==null
						|| !tps[i].IsClass
						|| !tps[i].IsPublic
						)
						continue;
					Type tmp = tps[i].BaseType;
					while( tmp!=null && tmp.Namespace.IndexOf("BP")!=-1 )
					{
						if(tmp.FullName == baseClassName )
							arr.Add( ass.CreateInstance(tps[i].FullName));
						tmp = tmp.BaseType;
					}
				}
			}
			if(baseClass ==null)
			{
				throw new Exception("�Ҳ�������"+baseClassName+"��");
			}
			return arr ;
		}
		
		#endregion ʵ��

	}
}
