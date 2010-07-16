using System;
using System.Collections;
using System.IO;
using BP.En;

namespace BP.DA
{	 
	/// <summary>
	/// ����λ��
	/// </summary>
	public enum Depositary
	{
		/// <summary>
		/// ȫ��
		/// </summary>
		Application,
		/// <summary>
		/// �Ի�
		/// </summary>
		Session,
		/// <summary>
		/// ������
		/// </summary>
		None
	}
	/// <summary>
	/// Cash ��ժҪ˵����
	/// </summary>
	public class Cash
	{
		static Cash()
		{
			if( !SystemConfig.IsBSsystem)
			{
				CS_Cash = new Hashtable();
			}
		}
		public static readonly Hashtable CS_Cash;

        #region Book_Cash ����ģ��cash.
        private static Hashtable _Book_Cash;
        public static Hashtable Book_Cash
        {
            get
            {
                if (_Book_Cash == null)
                    _Book_Cash = new Hashtable();
                return _Book_Cash;
            }
        }
        public static string GetBookStr(string cfile)
        {
            string val = Book_Cash[cfile] as string;
            if (val == null)
            {

                if (SystemConfig.IsDebug)
                {
                    BP.Rpt.RTF.RepBook.RepairBook(SystemConfig.PathOfData + "\\CyclostyleFile\\" + cfile);
                }
                try
                {
                    StreamReader read = new StreamReader(SystemConfig.PathOfData + "\\CyclostyleFile\\" + cfile, System.Text.Encoding.ASCII); // �ļ���.
                    val = read.ReadToEnd();  //��ȡ��ϡ�
                    read.Close(); // �رա�
                }
                catch (Exception ex)
                {
                    throw new Exception("@��ȡ����ģ��ʱ���ִ���cfile=" + cfile + " @Ex=" + ex.Message);
                }
                _Book_Cash[cfile] = val;

            }
            return val.Substring(0);
        }
        public static string[] GetBookParas(string cfile,string ensStrs)
        {
            string[] paras = Book_Cash[cfile + "Para"] as string[];
            // paras = null;
            if (paras == null)
            {
                paras = new string[300];
                string bookstr = Cash.GetBookStr(cfile);
                char[] chars = bookstr.ToCharArray();
                string para = "";
                int i = 0;
                foreach (char c in chars)
                {
                    if (c == '>')
                    {
                        try
                        {
                            if (ensStrs.IndexOf("." + para.Substring(0, para.IndexOf('.'))) == -1)
                                if (para.IndexOf("C.") == -1)
                                    continue;
                        }
                        catch (Exception ex)
                        {
#warning ���� 2009 - 04 -20 
                           // continue;
                            throw new Exception("@���������ȡ[" + para + "]�ڼ���ִ�����Ϣ��" + ex.Message);
                        }

                        paras.SetValue(para, i);
                        i++;
                    }

                    if (c == '<')
                        para = ""; // ��������� '<' ��ʼ��¼
                    else
                    {
                        if (c.ToString() == "")
                            continue;
                        para += c.ToString();
                    }
                }
                _Book_Cash[cfile + "Para"] = paras;
            }
            return paras;
        }
        #endregion

        #region BS_Cash
        private static Hashtable _BS_Cash;
        public static Hashtable BS_Cash
        {
            get
            {
                if (_BS_Cash == null)
                    _BS_Cash = new Hashtable();
                return _BS_Cash;
            }
        }
        #endregion

        #region SQL cash
        private static Hashtable _SQL_Cash;
        public static Hashtable SQL_Cash
        {
            get
            {
                if (_SQL_Cash == null)
                    _SQL_Cash = new Hashtable();
                return _SQL_Cash;
            }
        }
        public static BP.En.SQLCash GetSQL(string clName)
        {
            return SQL_Cash[clName] as BP.En.SQLCash;
        }
        public static void SetSQL(string clName, BP.En.SQLCash csh)
        {
            if (clName == null || csh == null)
                throw new Exception("clName.  csh ������һ��Ϊ�ա�");
            SQL_Cash[clName] = csh;
        }
        public static void InitSQL()
        {
            ArrayList al = ClassFactory.GetObjects("BP.En.Entity");
            foreach (BP.En.Entity en in al)
            {
                string sql = BP.En.SqlBuilder.Retrieve(en);
            }
        }
        #endregion

        #region EnsData cash
        private static Hashtable _EnsData_Cash;
        public static Hashtable EnsData_Cash
        {
            get
            {
                if (_EnsData_Cash == null)
                    _EnsData_Cash = new Hashtable();
                return _EnsData_Cash;
            }
        }
        public static BP.En.Entities GetEnsData(string clName)
        {
            Entities ens = EnsData_Cash[clName] as BP.En.Entities;
            if (ens == null)
                return null;

            if (ens.Count == 0)
                return null;
            //throw new Exception(clName + "����Ϊ0");
            return ens;
        }
        public static void EnsDataSet(string clName, BP.En.Entities obj)
        {
            if (obj.Count == 0)
            {
                ///obj.RetrieveAll();
                #warning ���ø���Ϊ

                //  throw new Exception(clName + "���ø���Ϊ0 �� ��ȷ���������ʵ�壬�Ƿ������ݣ�sq=select * from " + obj.GetNewEntity.EnMap.PhysicsTable);
            }

            EnsData_Cash[clName] = obj;
        }
        public static void Remove(string clName)
        {
            EnsData_Cash.Remove(clName);
        }
        #endregion

        #region EnsData cash ��չ ��ʱ��cash �ļ���
        private static Hashtable _EnsData_Cash_Ext;
        public static Hashtable EnsData_Cash_Ext
        {
            get
            {
                if (_EnsData_Cash_Ext == null)
                    _EnsData_Cash_Ext = new Hashtable();
                return _EnsData_Cash_Ext;
            }
        }
        /// <summary>
        /// Ϊ�����������Ļ��崦��
        /// </summary>
        /// <param name="clName"></param>
        /// <returns></returns>
        public static BP.En.Entities GetEnsDataExt(string clName)
        {
            // �ж��Ƿ�ʧЧ�ˡ�
            if (SystemConfig.IsTempCashFail)
            {
                EnsData_Cash_Ext.Clear();
                return null;
            }

            try
            {
                BP.En.Entities ens; 
                ens=  EnsData_Cash_Ext[clName] as BP.En.Entities ;
                return ens;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Ϊ�����������Ļ��崦��
        /// </summary>
        /// <param name="clName"></param>
        /// <param name="obj"></param>
        public static void SetEnsDataExt(string clName, BP.En.Entities obj)
        {
            if (clName == null || obj == null)
                throw new Exception("clName.  obj ������һ��Ϊ�ա�");
            EnsData_Cash_Ext[clName] = obj;
        }
        #endregion

        #region map cash
        private static Hashtable _Map_Cash;
        public static Hashtable Map_Cash
        {
            get
            {
                if (_Map_Cash == null)
                    _Map_Cash = new Hashtable();
                return _Map_Cash;
            }
        }
        public static BP.En.Map GetMap(string clName)
        {
            try
            {
                return Map_Cash[clName] as BP.En.Map;
            }
            catch
            {
                return null;
            }
        }

        public static void SetMap(string clName, BP.En.Map map)
        {
            if (clName == null || map == null)
                throw new Exception("clName.  Map ������һ��Ϊ�ա�");
            Map_Cash[clName] = map;
        }
         
        #endregion


        #region ȡ������

        /// <summary>
		/// �� Cash ����ȡ������.
		/// </summary>
        public static object GetObj(string key, Depositary where)
        {

#if DEBUG
            if (where == Depositary.None)
                throw new Exception("��û�а�[" + key + "]�ŵ�session or application ���治���ҳ�����.");
#endif

            if (SystemConfig.IsBSsystem)
            {
                if (where == Depositary.Application)
                    // return  System.Web.HttpContext.Current.Cache[key];
                    return BS_Cash[key]; //  System.Web.HttpContext.Current.Cache[key];
                else
                    return System.Web.HttpContext.Current.Session[key];
            }
            else
            {
                return CS_Cash[key];
            }
        }
		public static object GetObj(string key)
		{
            if (SystemConfig.IsBSsystem)
            {
                object obj = BS_Cash[key]; // Cash.GetObjFormApplication(key, null);
                if (obj == null)
                    obj = Cash.GetObjFormSession(key);
                return obj;
            }
            else
            {
                return CS_Cash[key];
            }
		}
		public static object GetObjFormApplication(string key, object isNullAsVal )
		{
            if (SystemConfig.IsBSsystem)
            {
                object obj = BS_Cash[key]; // System.Web.HttpContext.Current.Cache[key];
                if (obj == null)
                    return isNullAsVal;
                else
                    return obj;
            }
            else
            {
                object obj = CS_Cash[key];
                if (obj == null)
                    return isNullAsVal;
                else
                    return obj;
            }
		}
		public static object GetObjFormSession(string key)
		{
            if (SystemConfig.IsBSsystem)
            {
                try
                {
                    return System.Web.HttpContext.Current.Session[key];
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return CS_Cash[key];
            }
		}
		#endregion

		#region Remove Obj
		/// <summary>
		/// RemoveObj
		/// </summary>
		/// <param name="key"></param>
		/// <param name="where"></param>
		public static void RemoveObj(string key, Depositary where)
		{
			if ( Cash.IsExits( key,where )==false )
				return ;

			if( SystemConfig.IsBSsystem)
			{
                if (where == Depositary.Application)
                    System.Web.HttpContext.Current.Cache.Remove(key);
				else
					System.Web.HttpContext.Current.Session.Remove(key);
			}
			else
			{   
				CS_Cash.Remove( key );
			}
		}
		#endregion

		#region �������
        public static void RemoveObj(string key)
        {
            BS_Cash.Remove(key);
        }
        public static void AddObj(string key, Depositary where, object obj)
        {
            if (key == null)
                throw new Exception("����ҪΪobj=" + obj.ToString() + ",����Ϊ����ֵ��key");

            if (obj == null)
                throw new Exception("����ҪΪobj=null  ����Ϊ����ֵ��key=" + key);

#if DEBUG
            if (where == Depositary.None)
                throw new Exception("��û�а�[" + key + "]�ŵ� session or application ������������.");
#endif
            //if (Cash.IsExits(key, where))
            //    return;

            if (SystemConfig.IsBSsystem)
            {
                // System.Web.Caching.CacheDependency cd = new System.Web.Caching.CacheDependency(
                if (where == Depositary.Application)
                {
                    // System.Web.HttpContext.Current.Cache.Insert(key, obj);
                    // BS_Cash.Add(key, obj);

                    BS_Cash[key] = obj;

                    //System.Web.HttpContext.Current.Cache.Insert(key, obj, null, DateTime.Now.AddMonths(10), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                else
                {
                    System.Web.HttpContext.Current.Session[key] = obj;
                }
            }
            else
            {
                CS_Cash.Add(key, obj);
            }
        }
		#endregion

		#region �ж϶����ǲ��Ǵ���
		/// <summary>
		/// �ж϶����ǲ��Ǵ���
		/// </summary>
		public static bool IsExits(string key, Depositary where)
		{
			if( SystemConfig.IsBSsystem)
			{
                if (where == Depositary.Application)
                {
                    if (System.Web.HttpContext.Current.Cache[key] == null)
                        return false;
                    else
                        return true;
                }
                else
                {
                    if (System.Web.HttpContext.Current.Session[key] == null)
                        return false;
                    else
                        return true;
                }
			}
			else
			{
				return CS_Cash.ContainsKey(key);
			}
		}
		#endregion


	}
}
