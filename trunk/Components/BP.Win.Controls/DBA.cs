/*
��飺�����ȡ���ݵ���
����ʱ�䣺2004-4
�����ߣ� CCB
*/
using System;
using System.Diagnostics;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient ; 
using System.Data.OleDb;
using System.Data.Odbc ; 
using CWAI.Config;
using System.ComponentModel;

namespace CWAI.DBA
{
	/// <summary>
	/// ���ݿ�����
	/// </summary>
	public enum DBType
	{
		/// <summary>
		/// sql 2000
		/// </summary>
		SQL2000,
		/// <summary>
		/// oracle 9i 
		/// </summary>
		Oracle9i
	}
	/// <summary>
	/// ���ݿ���ʡ�
	/// ����ฺ�����ˡ�ʵ����Ϣ
	/// </summary>
	public class DBAccess
	{
		//���캯��
		static DBAccess()
		{
			CurrentSys_Serial = new Hashtable();
			KeyLockState = new Hashtable();
		}
		public static readonly Hashtable CurrentSys_Serial;
		private static int readCount = -1;
		private static readonly Hashtable KeyLockState ;

		#region �������к��뷽��
		/// <summary>
		/// ���к�
		/// </summary>
		/// <returns>���ݱ�ʶ���������к�</returns>
		public static int GenerSequenceNumber(string type)
		{
			if( readCount == -1 )//ϵͳ��һ������ʱ
			{
				DataTable tb = DBAccess.RunSQLReturnTable("select CfgKey,intVal from Sys_Serial");
				foreach( DataRow row in tb.Rows)
				{
					CurrentSys_Serial.Add( row[0].ToString().Trim(),(int)row[1] );
					KeyLockState.Add( row[0].ToString().Trim() ,false);
				}
				readCount++;
			}
			if( !CurrentSys_Serial.ContainsKey( type ) )
			{
				throw new Exception( "��������û�ҵ�["+type+"]��" );
			}

			while(true)
			{
				while( ! (bool)KeyLockState[type] )
				{
					KeyLockState[type] = true;
					int cur = (int)CurrentSys_Serial[type];
					if( readCount++%10 ==0)
					{
						readCount = 1; 
						int n = (int)CurrentSys_Serial[type] + 10;
						string upd = "update Sys_Serial set intVal="+ n +" where CfgKey='"+type+"'";
						DBAccess.RunSQL( upd );
					}
					cur++;
					CurrentSys_Serial[type] = cur;
					KeyLockState[type] = false;
					return cur;
				}
			}
		}
		/// <summary>
		/// ����һ��OID
		/// </summary>
		/// <returns></returns>
		public static int GenerOID()
		{
			return GenerSequenceNumber( "OID" );
		}
		#endregion

//		#region ���Ȩ��
//		/// <summary>
//		/// ���session . ��Ҫ���ж��ǲ������û���½��Ϣ��
//		/// </summary>
//		public static void DoCheckSession()
//		{
//			if( HttpContext.Current!=null && SystemConfig.IsDebug==false)
//			{			
//				HttpContext.Current.Session["url"]=HttpContext.Current.Request.RawUrl;
//				string str="���ĵ�¼ʱ��̫���������µ�¼��";
//				HttpContext.Current.Session["info"]=str;
//				System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+SystemConfig.PageOfLostSession);
//				//System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+"/Portal/ErrPage.aspx");
//			}
//		}
//		#endregion


		#region ȡ�����Ӷ��� ��CS��BS�������ԡ��ؼ����ԡ�
		
		public static object GetAppCenterDBConn
		{
			get
			{
//				if( SystemConfig.IsBSsystem )
//				{
//					Component conn = HttpContext.Current.Application["AppCenterDSN"] as Component;
//					if ( conn==null )
//					{
//						string connstr = SystemConfig.AppSettings[ "AppCenterDSN" ];
//						switch( AppCenterDBType )
//						{
//							case DBType.SQL2000:
//								SqlConnection sql2000 = new SqlConnection( connstr );
//								HttpContext.Current.Application[ "AppCenterDSN" ] = sql2000;
//								return sql2000 ;
//
//							case DBType.Oracle9i :
//								OracleConnection oracle = new OracleConnection( connstr );
//								HttpContext.Current.Application[ "AppCenterDSN" ] = oracle;
//								return oracle;
//
//							default :
//								throw new Exception( "����δ֪�����ݿ��������ͣ�" );
//						}
//					}
//					return conn;
//				}
//				else
				{
					Component conn = SystemConfig.CS_DBConnctionDic["AppCenterDSN"] as Component ;
					if ( conn==null )
					{
						string connstr = SystemConfig.AppSettings[ "AppCenterDSN" ];
						switch( AppCenterDBType )
						{
							case DBType.SQL2000:
								SqlConnection sql2000 = new SqlConnection( connstr );
								SystemConfig.CS_DBConnctionDic["AppCenterDSN"]=sql2000;
								return sql2000 ;

							case DBType.Oracle9i :
								OracleConnection oracle = new OracleConnection( connstr );
								SystemConfig.CS_DBConnctionDic["AppCenterDSN"]=oracle;
								return oracle;

							default :
								throw new Exception( "����δ֪�����ݿ��������ͣ�" );
						}
					}
					return conn;
				}
			}
		}
		
		#endregion ȡ�����Ӷ��� ��CS��BS��������

		public static DBType AppCenterDBType
		{
			get
			{
				return SystemConfig.AppCenterDBType;
			}
		}		
		#region ���� SQL  

		#region ��ָ����Connection��ִ�� SQL ��䣬������Ӱ�������
		public static int RunSQL(string sql ,SqlConnection conn)
		{
			return RunSQL(sql ,conn ,CommandType.Text);
		}
		public static int RunSQL(string sql ,SqlConnection conn ,CommandType sqlType ,params object[] pars)
		{
			Debug.WriteLine( sql );
			try
			{
				SqlCommand cmd = new SqlCommand( sql ,conn);
				cmd.CommandType = sqlType;
				foreach(object par in pars)
				{
					cmd.Parameters.Add( "par",par);
				}
				if (conn.State != System.Data.ConnectionState.Open)
				{
					conn.Open();
				}
				return cmd.ExecuteNonQuery();				 
			}
			catch(System.Exception ex)
			{
				throw new Exception(ex.Message + sql );
			}
		}
		
		
		public static int RunSQL(string sql ,OracleConnection conn)
		{
			return RunSQL(sql ,conn ,CommandType.Text);	 
		}
		public static int RunSQL(string sql ,OracleConnection conn ,CommandType sqlType ,params object[] pars)
		{
			Debug.WriteLine( sql );
			try
			{
				OracleCommand cmd = new OracleCommand( sql ,conn);
				cmd.CommandType = sqlType;
				foreach(object par in pars)
				{
					cmd.Parameters.Add( "par",par);
				}
				if (conn.State != System.Data.ConnectionState.Open)
				{
					conn.Open();
				}
				return cmd.ExecuteNonQuery();				 
			}
			catch(System.Exception ex)
			{
				throw new Exception(ex.Message + sql );
			}
		}
		#endregion 

		#region �ڵ�ǰ��Connectionִ�� SQL ��䣬������Ӱ�������
		

		public static int RunSQL(string sql ,CommandType sqlType ,params object[] pars)
		{
			object oconn = GetAppCenterDBConn;
			if(oconn is SqlConnection)
				return RunSQL( sql ,(SqlConnection)oconn, sqlType ,pars);
			else if(oconn is OracleConnection)
				return RunSQL( sql ,(OracleConnection)oconn, sqlType ,pars);
			else
				throw new Exception("��ȡ���ݿ�����[GetAppCenterDBConn]ʧ�ܣ�");
		}
		public static int RunSQL(string sql)
		{
			return RunSQL( sql ,sql );
		}
		public static int RunSQL(string msSQL, string oracleSQL)
		{
			try
			{
				switch( AppCenterDBType )
				{
					case DBType.SQL2000:
						SqlConnection sql2000 = GetAppCenterDBConn as SqlConnection;
						return RunSQL( msSQL , sql2000 );

					case DBType.Oracle9i :
						OracleConnection oracle = GetAppCenterDBConn as OracleConnection;
						return RunSQL( oracleSQL , oracle );
					default :
						throw new Exception( "����δ֪�����ݿ��������ͣ�" );
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message+msSQL);
			}
		}
		 
		#endregion 

		#endregion  

		#region ����SQL ���� DataTable 

		#region ��ָ���� Connection ��ִ��
		public static  DataTable RunSQLReturnTable(string msSQL ,SqlConnection sqlconn ,CommandType sqlType ,params object[] pars)
		{
			Debug.WriteLine( msSQL );
			try
			{
				SqlDataAdapter msAda = new SqlDataAdapter( msSQL , sqlconn );
				msAda.SelectCommand.CommandType = sqlType;
				foreach(object par in pars)
				{
					msAda.SelectCommand.Parameters.Add( "par",par);
				}
				DataTable mstb = new DataTable( "mstb" );
				msAda.Fill( mstb );

				return mstb;
			}
			catch(System.Exception ex)
			{
				throw new Exception(ex.Message+msSQL);
			}
		}
		public static  DataTable RunSQLReturnTable(string msSQL ,SqlConnection sqlconn )
		{
			return RunSQLReturnTable( msSQL ,sqlconn ,CommandType.Text );
		}


		public static  DataTable RunSQLReturnTable(string oraSQL ,OracleConnection oraconn ,CommandType sqlType ,params object[] pars)
		{
			Debug.WriteLine( oraSQL );
			try
			{
				OracleDataAdapter oraAda = new OracleDataAdapter( oraSQL , oraconn );
				oraAda.SelectCommand.CommandType = sqlType;
				foreach(object par in pars)
				{
					oraAda.SelectCommand.Parameters.Add( "par",par);
				}
				DataTable oratb = new DataTable( "oratb" );
				oraAda.Fill( oratb );
				return oratb;
			}
			catch(System.Exception ex)
			{
				throw new Exception(ex.Message+oraSQL);
			}
		}
		public static  DataTable RunSQLReturnTable(string oraSQL ,OracleConnection oraconn)
		{
			return RunSQLReturnTable( oraSQL ,oraconn ,CommandType.Text );			 
		}
		#endregion 

		#region �ڵ�ǰConnection��ִ��
		public static  DataTable RunSQLReturnTable(string sql )
		{
			return RunSQLReturnTable( sql ,sql );
		}
		public static  DataTable RunSQLReturnTable(string msSQL, string oracleSQL )
		{
			switch( AppCenterDBType )
			{
				case DBType.SQL2000:
					SqlConnection sql2000 = GetAppCenterDBConn as SqlConnection;
					return RunSQLReturnTable( msSQL , sql2000 );

				case DBType.Oracle9i :
					OracleConnection oracle = GetAppCenterDBConn as OracleConnection;
					return RunSQLReturnTable( oracleSQL , oracle );

				default :
					throw new Exception( "����δ֪�����ݿ��������ͣ�" );
			}
		}
		
		#endregion �ڵ�ǰConnection��ִ��

		#endregion
 
		#region ��ѯ����ֵ�ķ���.
		#region ָ������

		public static object RunSQLReturnVal(string sql ,SqlConnection conn )
		{
			return RunSQLReturnVal( sql ,conn ,CommandType.Text );
		}
		public static object RunSQLReturnVal(string sql ,SqlConnection conn ,CommandType sqlType ,params object[] pars)
		{
			Debug.WriteLine( sql );
			SqlConnection tmp = new SqlConnection(conn.ConnectionString);
			SqlCommand cmd = new SqlCommand( sql ,tmp);
			object val = null;
			try
			{
				cmd.CommandType = sqlType;
				foreach(object par in pars)
				{
					cmd.Parameters.Add( "par",par);
				}
				tmp.Open();
				val= cmd.ExecuteScalar();
			}
			catch(System.Exception ex)
			{
				tmp.Close();
				throw new Exception(ex.Message + sql );
			}
			tmp.Close();
			return val;
		}
		
		public static object RunSQLReturnVal(string sql ,OracleConnection conn )
		{
			return RunSQLReturnVal( sql ,conn ,CommandType.Text );
		}
		public static object RunSQLReturnVal(string sql ,OracleConnection conn ,CommandType sqlType ,params object[] pars)
		{
			Debug.WriteLine( sql );
			OracleConnection tmp =new OracleConnection(conn.ConnectionString);
			OracleCommand cmd = new OracleCommand( sql ,tmp);
			object val = null;
			try
			{
				cmd.CommandType = sqlType;
				foreach(object par in pars)
				{
					cmd.Parameters.Add( "par",par);
				}
				tmp.Open();
				val= cmd.ExecuteScalar();				 
			}
			catch(System.Exception ex)
			{
				tmp.Close();
				throw new Exception(ex.Message + sql );
			}
			tmp.Close();
			return val;
		}

		#endregion 

		#region �ڵ�ǰ��Connectionִ�� SQL ��䣬������������
		public static object RunSQLReturnVal(string sql)
		{
			return RunSQLReturnVal( sql ,sql );
		}
		public static object RunSQLReturnVal(string msSQL, string oracleSQL)
		{
			switch( AppCenterDBType )
			{
				case DBType.SQL2000:
					SqlConnection sql2000 = GetAppCenterDBConn as SqlConnection;
					return RunSQLReturnVal( msSQL , sql2000 );

				case DBType.Oracle9i :
					OracleConnection oracle = GetAppCenterDBConn as OracleConnection;
					return RunSQLReturnVal( oracleSQL , oracle );

				default :
					throw new Exception( "����δ֪�����ݿ��������ͣ�" );
			}
		}
		#endregion 

		#endregion


		#region ����ǲ��Ǵ���		 
		/// <summary>
		/// ����ǲ��Ǵ���
		/// </summary>
		/// <param name="selectSQL"></param>
		/// <returns>����ǲ��Ǵ���</returns>
		public static bool IsExits(string selectSQL)
		{
			if ( RunSQLReturnVal(selectSQL) == null)
				return false;
			return true;
		}
		/// <summary>
		/// ����ǲ��Ǵ���
		/// </summary>
		/// <param name="msSQL">msSQL</param>
		/// <param name="oracleSQL">oracleSQL</param>
		/// <returns>����ǲ��Ǵ���</returns>
		public static bool IsExits(string msSQL,string oracleSQL)
		{
			if ( RunSQLReturnVal(msSQL ,oracleSQL) == null)
				return false;
			return true;
		}
		#endregion 
	}

	#region ODBC
	public class DBAccessOfODBC
	{
		/// <summary>
		/// ����ǲ��Ǵ���
		/// </summary>
		public static bool IsExits(string selectSQL)
		{
			if ( RunSQLReturnVal(selectSQL) == null)
				return false;
			return true;
		}


		#region ȡ�����Ӷ��� ��CS��BS�������ԡ��ؼ����ԡ�
		public static OdbcConnection GetSingleConn
		{
			get
			{
//				if( SystemConfig.IsBSsystem )
//				{
//					OdbcConnection conn = HttpContext.Current.Application["DBAccessOfODBC"] as OdbcConnection;
//					if ( conn==null )
//					{
//						conn = new OdbcConnection( SystemConfig.AppSettings[ "DBAccessOfODBC" ] );
//						HttpContext.Current.Application[ "DBAccessOfODBC" ] = conn;
//					}
//					return conn;
//				}
//				else
				{
					OdbcConnection conn = SystemConfig.CS_DBConnctionDic["DBAccessOfODBC"] as OdbcConnection;
					if ( conn==null )
					{
						conn = new OdbcConnection( SystemConfig.AppSettings[ "DBAccessOfODBC" ] );
						SystemConfig.CS_DBConnctionDic[ "DBAccessOfODBC" ] = conn;
					}
					return conn;
				}
			}
		}
		#endregion ȡ�����Ӷ��� ��CS��BS��������


		#region ���� RunSQLReturnTable

		#region ʹ�ñ��ص�����
		public static DataTable RunSQLReturnTable(string sql )
		{
			return RunSQLReturnTable( sql , GetSingleConn ,CommandType.Text );
		}
		public static DataTable RunSQLReturnTable(string sql ,CommandType sqlType ,params object[] pars)
		{
			return RunSQLReturnTable( sql , GetSingleConn ,sqlType ,pars);
		}
	
		#endregion 

		#region ʹ��ָ��������
		public static DataTable RunSQLReturnTable(string sql , OdbcConnection conn )
		{
			return RunSQLReturnTable( sql ,conn ,CommandType.Text );
		}
		public static DataTable RunSQLReturnTable(string sql , OdbcConnection conn ,CommandType sqlType ,params object[] pars)
		{
			try
			{
				OdbcDataAdapter ada = new OdbcDataAdapter( sql ,conn);
				ada.SelectCommand.CommandType = sqlType;
				foreach(object par in pars)
				{
					ada.SelectCommand.Parameters.Add( "par",par);
				}
				if (conn.State != ConnectionState.Open)
					conn.Open();
				DataTable dt = new DataTable("tb");
				ada.Fill( dt );
				ada.Dispose();
				return dt;
			}
			catch(System.Exception ex)
			{
				throw new Exception(ex.Message + sql);
			}
		}
		#endregion 

		#endregion

		#region ���� RunSQL

		#region ʹ�ñ��ص�����
		public static int RunSQL(string sql )
		{
			return RunSQL( sql , GetSingleConn ,CommandType.Text);
		}
		public static int RunSQL(string sql ,CommandType sqlType ,params object[] pars)
		{
			return RunSQL( sql , GetSingleConn ,sqlType , pars);
		}
		#endregion ʹ�ñ��ص�����

		#region ʹ��ָ��������
		public static int RunSQL(string sql ,OdbcConnection conn )
		{
			return RunSQL( sql , conn ,CommandType.Text );
		}
		public static int RunSQL(string sql ,OdbcConnection conn ,CommandType sqlType ,params object[] pars)
		{
			Debug.WriteLine( sql );
			try
			{
				OdbcCommand cmd = new OdbcCommand( sql ,conn);
				cmd.CommandType = sqlType;
				foreach(object par in pars)
				{
					cmd.Parameters.Add( "par",par);
				}
				if (conn.State != System.Data.ConnectionState.Open)
				{
					conn.Open();
				}
				return cmd.ExecuteNonQuery();				 
			}
			catch(System.Exception ex)
			{
				throw new Exception(ex.Message + sql );
			}
		}

		#endregion ʹ��ָ��������

		#endregion 

		#region ִ��SQL ��������������
		public static object RunSQLReturnVal(string sql )
		{
			return RunSQLReturnVal( sql ,GetSingleConn ,CommandType.Text );
		}
		public static object RunSQLReturnVal(string sql ,CommandType sqlType ,params object[] pars)
		{
			return RunSQLReturnVal( sql ,GetSingleConn , sqlType ,pars );
		}

		public static object RunSQLReturnVal(string sql ,OdbcConnection conn )
		{
			return RunSQLReturnVal( sql ,conn ,CommandType.Text );
		}
		public static object RunSQLReturnVal(string sql ,OdbcConnection conn ,CommandType sqlType ,params object[] pars)
		{
			Debug.WriteLine( sql );
			OdbcConnection tmp = new OdbcConnection(conn.ConnectionString);
			OdbcCommand cmd = new OdbcCommand( sql ,tmp);
			object val = null;
			try
			{
				cmd.CommandType = sqlType;
				foreach(object par in pars)
				{
					cmd.Parameters.Add( "par",par);
				}
				tmp.Open();
				val= cmd.ExecuteScalar();				 
			}
			catch(System.Exception ex)
			{
				tmp.Close();
				throw new Exception(ex.Message + sql );
			}
			tmp.Close();
			return val;
		}
		#endregion ִ��SQL ��������������

	}
	#endregion

	/// <summary>
	/// ����OLE ������
	/// </summary>
	public class DBAccessOfOLE
	{
		/// <summary>
		/// ����ǲ��Ǵ���
		/// </summary>
		/// <param name="selectSQL"></param>
		/// <returns>����ǲ��Ǵ���</returns>
		public static bool IsExits(string selectSQL)
		{
			if ( RunSQLReturnVal(selectSQL) == null)
				return false;
			return true;
		}


		#region ȡ�����Ӷ��� ��CS��BS�������ԡ��ؼ����ԡ�
		public static OleDbConnection GetSingleConn
		{
			get
			{
//				if( SystemConfig.IsBSsystem )
//				{
//					OleDbConnection conn = HttpContext.Current.Application["DBAccessOfOLE"] as OleDbConnection;
//					if ( conn==null )
//					{
//						conn = new OleDbConnection( SystemConfig.AppSettings[ "DBAccessOfOLE" ] );
//						HttpContext.Current.Application[ "DBAccessOfOLE" ] = conn;
//					}
//					return conn;
//				}
//				else
				{
					OleDbConnection conn = SystemConfig.CS_DBConnctionDic["DBAccessOfOLE"] as OleDbConnection;
					if ( conn==null )
					{
						conn = new OleDbConnection( SystemConfig.AppSettings[ "DBAccessOfOLE" ] );
						SystemConfig.CS_DBConnctionDic[ "DBAccessOfOLE" ] = conn;
					}
					return conn;
				}
			}
		}
		#endregion ȡ�����Ӷ��� ��CS��BS��������


		#region ���� RunSQLReturnTable

		#region ʹ�ñ��ص�����
		public static DataTable RunSQLReturnTable(string sql )
		{
			return RunSQLReturnTable( sql , GetSingleConn ,CommandType.Text );
		}
		public static DataTable RunSQLReturnTable(string sql ,CommandType sqlType ,params object[] pars)
		{
			return RunSQLReturnTable( sql , GetSingleConn ,sqlType ,pars);
		}
	
		#endregion 

		#region ʹ��ָ��������
		public static DataTable RunSQLReturnTable(string sql , OleDbConnection conn )
		{
			return RunSQLReturnTable( sql ,conn ,CommandType.Text );
		}
		public static DataTable RunSQLReturnTable(string sql , OleDbConnection conn ,CommandType sqlType ,params object[] pars)
		{
			try
			{
				OleDbDataAdapter ada = new OleDbDataAdapter( sql ,conn);
				ada.SelectCommand.CommandType = sqlType;
				foreach(object par in pars)
				{
					ada.SelectCommand.Parameters.Add( "par",par);
				}
				if (conn.State != ConnectionState.Open)
					conn.Open();
				DataTable dt = new DataTable("tb");
				ada.Fill( dt );
				ada.Dispose();
				return dt;
			}
			catch(System.Exception ex)
			{
				throw new Exception(ex.Message + sql);
			}
		}
		#endregion 

		#endregion

		#region ���� RunSQL

		#region ʹ�ñ��ص�����
		public static int RunSQL(string sql )
		{
			return RunSQL( sql , GetSingleConn ,CommandType.Text);
		}
		public static int RunSQL(string sql ,CommandType sqlType ,params object[] pars)
		{
			return RunSQL( sql , GetSingleConn ,sqlType , pars);
		}
		#endregion ʹ�ñ��ص�����

		#region ʹ��ָ��������
		public static int RunSQL(string sql ,OleDbConnection conn )
		{
			return RunSQL( sql , conn ,CommandType.Text );
		}
		public static int RunSQL(string sql ,OleDbConnection conn ,CommandType sqlType ,params object[] pars)
		{
			Debug.WriteLine( sql );
			try
			{
				OleDbCommand cmd = new OleDbCommand( sql ,conn);
				cmd.CommandType = sqlType;
				foreach(object par in pars)
				{
					cmd.Parameters.Add( "par",par);
				}
				if (conn.State != System.Data.ConnectionState.Open)
				{
					conn.Open();
				}
				return cmd.ExecuteNonQuery();				 
			}
			catch(System.Exception ex)
			{
				throw new Exception(ex.Message + sql );
			}
		}

		#endregion ʹ��ָ��������

		#endregion 

		#region ִ��SQL ��������������
		public static object RunSQLReturnVal(string sql )
		{
			return RunSQLReturnVal( sql ,GetSingleConn ,CommandType.Text );
		}
		public static object RunSQLReturnVal(string sql ,CommandType sqlType ,params object[] pars)
		{
			return RunSQLReturnVal( sql ,GetSingleConn , sqlType ,pars );
		}

		public static object RunSQLReturnVal(string sql ,OleDbConnection conn )
		{
			return RunSQLReturnVal( sql ,conn ,CommandType.Text );
		}
		public static object RunSQLReturnVal(string sql ,OleDbConnection conn ,CommandType sqlType ,params object[] pars)
		{
			Debug.WriteLine( sql );
			OleDbConnection tmp = new OleDbConnection(conn.ConnectionString);
			OleDbCommand cmd = new OleDbCommand( sql ,tmp);
			object val = null;
			try
			{
				cmd.CommandType = sqlType;
				foreach(object par in pars)
				{
					cmd.Parameters.Add( "par",par);
				}
				tmp.Open();
				val= cmd.ExecuteScalar();
			}
			catch(System.Exception ex)
			{
				tmp.Close();
				throw new Exception(ex.Message + sql );
			}
			tmp.Close();
			return val;
		}
		#endregion ִ��SQL ��������������
	}
	/// <summary>
	/// ����DBAccessOfSQLServer2000 ������
	/// </summary>
	public class DBAccessOfMSSQL2000
	{
		/// <summary>
		/// ����ǲ��Ǵ���
		/// </summary>
		/// <param name="selectSQL"></param>
		/// <returns>����ǲ��Ǵ���</returns>
		public static bool IsExits(string selectSQL)
		{
			if ( RunSQLReturnVal(selectSQL) == null)
				return false;
			return true;
		}


		#region ȡ�����Ӷ��� ��CS��BS�������ԡ��ؼ����ԡ�
		public static SqlConnection GetSingleConn
		{
			get
			{
//				if( SystemConfig.IsBSsystem )
//				{
//					SqlConnection conn = HttpContext.Current.Application["DBAccessOfMSSQL2000"] as SqlConnection;
//					if (conn==null)
//					{
//						conn = new SqlConnection( SystemConfig.AppSettings[ "DBAccessOfMSSQL2000" ] );
//						HttpContext.Current.Application[ "DBAccessOfMSSQL2000" ] = conn;
//					}
//					return conn;
//				}
//				else
				{
					SqlConnection conn = SystemConfig.CS_DBConnctionDic["DBAccessOfMSSQL2000"] as SqlConnection;
					if (conn==null)
					{
						conn = new SqlConnection( SystemConfig.AppSettings[ "DBAccessOfMSSQL2000" ] );
						SystemConfig.CS_DBConnctionDic[ "DBAccessOfMSSQL2000" ] = conn;
					}
					return conn;
				}
			}
		}
		#endregion ȡ�����Ӷ��� ��CS��BS��������

		
		#region ���� RunSQLReturnTable
		public static DataTable RunSQLReturnTable(string sql )
		{
			return DBAccess.RunSQLReturnTable( sql , GetSingleConn ,CommandType.Text );
		}
		public static DataTable RunSQLReturnTable(string sql ,CommandType sqlType ,params object[] pars)
		{
			return DBAccess.RunSQLReturnTable( sql , GetSingleConn ,sqlType ,pars);
		}
		#endregion

		#region ���� RunSQL
		public static int RunSQL(string sql )
		{
			return DBAccess.RunSQL( sql , GetSingleConn ,CommandType.Text );
		}
		public static int RunSQL(string sql ,CommandType sqlType ,params object[] pars)
		{
			return  DBAccess.RunSQL( sql , GetSingleConn ,sqlType , pars);
		}
		#endregion 

		#region ִ��SQL ��������������
		public static object RunSQLReturnVal(string sql )
		{
			return DBAccess.RunSQLReturnVal( sql ,GetSingleConn ,CommandType.Text );
		}
		public static object RunSQLReturnVal(string sql ,CommandType sqlType ,params object[] pars)
		{
			return DBAccess.RunSQLReturnVal( sql ,GetSingleConn , sqlType ,pars );
		}
		#endregion ִ��SQL ��������������

	}
	/// <summary>
	/// Oracle9i �ķ���
	/// </summary>
	public class DBAccessOfOracle9i
	{
		/// <summary>
		/// ����ǲ��Ǵ���
		/// </summary>
		/// <param name="selectSQL"></param>
		/// <returns>����ǲ��Ǵ���</returns>
		public static bool IsExits(string selectSQL)
		{
			if ( RunSQLReturnVal(selectSQL) == null)
				return false;
			return true;
		}

		#region ȡ�����Ӷ��� ��CS��BS�������ԡ��ؼ����ԡ�
		public static OracleConnection GetSingleConn
		{
			get
			{
//				if( SystemConfig.IsBSsystem )
//				{
//					OracleConnection conn = HttpContext.Current.Application["DBAccessOfOracle9i"] as OracleConnection;
//					if (conn==null)
//					{
//						conn = new OracleConnection( SystemConfig.AppSettings[ "DBAccessOfOracle9i" ] );
//						HttpContext.Current.Application[ "DBAccessOfOracle9i" ] = conn;
//					}
//					return conn;
//				}
//				else
				{
					OracleConnection conn = SystemConfig.CS_DBConnctionDic["DBAccessOfOracle9i"] as OracleConnection;
					if (conn==null)
					{
						conn = new OracleConnection( SystemConfig.AppSettings[ "DBAccessOfOracle9i" ] );
						SystemConfig.CS_DBConnctionDic[ "DBAccessOfOracle9i" ] = conn;
					}
					return conn;
				}
			}
		}
		#endregion ȡ�����Ӷ��� ��CS��BS��������

		
		#region ���� RunSQLReturnTable
		public static DataTable RunSQLReturnTable(string sql )
		{
			return DBAccess.RunSQLReturnTable( sql , GetSingleConn );
		}
		public static DataTable RunSQLReturnTable(string sql ,CommandType sqlType ,params object[] pars)
		{
			return DBAccess.RunSQLReturnTable( sql , GetSingleConn ,sqlType ,pars);
		}
		#endregion

		#region ���� RunSQL 

		public static int RunSQL(string sql )
		{
			return DBAccess.RunSQL( sql , GetSingleConn ,CommandType.Text);
		}
		public static int RunSQL(string sql ,CommandType sqlType ,params object[] pars)
		{
			return DBAccess.RunSQL( sql , GetSingleConn ,sqlType , pars);
		}
		#endregion 
				
		#region ִ��SQL ��������������
		public static object RunSQLReturnVal(string sql )
		{
			return DBAccess.RunSQLReturnVal( sql ,GetSingleConn ,CommandType.Text );
		}
		public static object RunSQLReturnVal(string sql ,CommandType sqlType ,params object[] pars)
		{
			return DBAccess.RunSQLReturnVal( sql ,GetSingleConn , sqlType ,pars );
		}

		#endregion ִ��SQL ��������������
	 
	}
	
	
}
