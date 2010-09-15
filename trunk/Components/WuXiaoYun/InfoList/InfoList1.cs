using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
	 	/// <summary>
    /// ��Ϣ����
	/// </summary>
    public class InfoList1Attr : InfoListBaseAttr
    {
    }
	/// <summary>
    /// ��Ϣ����
	/// </summary>
    public class InfoList1 : InfoListBase
    {
        /// <summary>
        /// ��ȡTable
        /// </summary>
        public override string PTable
        {
            get
            {
                return "GE_InfoList1";
            }
        }
        /// <summary>
        /// ��һƪ����
        /// </summary>
        public InfoList1 PreviousInfo
        {
            get
            {
                InfoList1 en = new InfoList1();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere("GE_InfoList1.No < '" + this.No + "'");
                qo.addAnd();
                qo.AddWhereNotIn(InfoList1Attr.InfoListSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(InfoList1Attr.No);
                qo.DoQuery();
                if (qo.GetCount() <= 0)
                {
                    return null;
                }
                return en;
            }
        }
        /// <summary>
        /// ��һƪ����
        /// </summary>
        public InfoList1 NextInfo
        {
            get
            {
                InfoList1 en = new InfoList1();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere("GE_InfoList1.No > '" + this.No + "'");
                qo.addAnd();
                qo.AddWhereNotIn(InfoList1Attr.InfoListSta, "0");
                qo.Top = 1;
                qo.addOrderBy(InfoList1Attr.No);
                qo.DoQuery();
                if (qo.GetCount() <= 0)
                {
                    return null;
                }
                return en;
            }
        }


        #region ���췽��
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public InfoList1(string no)
            : base(no)
        {

        }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public InfoList1()
        {
        }
        #endregion
    }
	/// <summary>
    /// ��Ϣ����s
	/// </summary>
    public class InfoList1s : InfoListBases
    {
        /// <summary>
        /// ��Ϣ����s
        /// </summary>
        public InfoList1s()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new InfoList1();
            }
        }
    }
}
