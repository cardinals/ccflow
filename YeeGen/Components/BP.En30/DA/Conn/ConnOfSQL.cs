using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient ;
using System.Data;
using System.Data.Common;

namespace BP.DA
{
    public class ConnOfSQL : ConnBase
    {
        public new SqlConnection Conn = null;
        public ConnOfSQL()
        {
        }
    }
    public class ConnOfSQLs : System.Collections.CollectionBase
    {
        public ConnOfSQLs()
        {
        }
        public void Add(ConnOfSQL conn)
        {
            this.InnerList.Add(conn);
        }
        /// <summary>
        ///  ��ʼ��
        /// </summary>
        public void Init()
        {
            for (int i = 0; i <= 3; i++)
            {
                ConnOfSQL conn = new ConnOfSQL();
                conn.IDX = i;
                this.Add(conn);
            }
        }
        public bool isLock = false;
        public ConnOfSQL GetOne()
        {
            //if (this.Count == 0)
            //    this.Init();
            while (isLock)
            {
                ;
            }

            isLock = true;
            foreach (ConnOfSQL conn in this)
            {
                if (conn.IsUsing == false)
                {
                    if (conn.Conn == null)
                        conn.Conn = new SqlConnection(SystemConfig.AppCenterDSN);
                    conn.Times++;
                    conn.IsUsing = true;
                    isLock = false;
                    return conn;
                }
            }

            // ���û���µ����ӡ�
            ConnOfSQL nconn = new ConnOfSQL();
            nconn.IDX = this.Count;
            nconn.Conn = new SqlConnection(SystemConfig.AppCenterDSN);
            nconn.IsUsing = true;
            this.InnerList.Add(nconn);
            isLock = false;
            return nconn;
            //throw new Exception("û�п��õ������ˣ��뱨�������Ա��");
        }
      
        /// <summary>
        /// PutPool
        /// </summary>
        /// <param name="conn"></param>
        public void PutPool(ConnBase conn)
        {
            conn.IsUsing = false;
            this.InnerList[conn.IDX] = conn;
        }
    }
}
