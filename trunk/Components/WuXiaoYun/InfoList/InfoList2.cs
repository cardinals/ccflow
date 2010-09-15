using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
	 	/// <summary>
    /// ��Ϣ����
	/// </summary>
    public class InfoList2Attr : InfoListBaseAttr
    {
    }
	/// <summary>
    /// ��Ϣ����
	/// </summary>
    public class InfoList2 : InfoListBase
    {
        /// <summary>
        /// ��ȡTable
        /// </summary>
        public override string PTable
        {
            get
            {
                return "GE_InfoList2";
            }
        }
        /// <summary>
        /// ��һƪ����
        /// </summary>
        public InfoList2 PreviousInfo
        {
            get
            {
                InfoList2 en = new InfoList2();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere("GE_InfoList2.No < '" + this.No + "'");
                qo.addAnd();
                qo.AddWhereNotIn(InfoList2Attr.InfoListSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(InfoList2Attr.No);
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
        public InfoList2 NextInfo
        {
            get
            {
                InfoList2 en = new InfoList2();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere("GE_InfoList2.No > '" + this.No + "'");
                qo.addAnd();
                qo.AddWhereNotIn(InfoList2Attr.InfoListSta, "0");
                qo.Top = 1;
                qo.addOrderBy(InfoList2Attr.No);
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
        public InfoList2(string no)
            : base(no)
        {

        }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public InfoList2()
        {
        }
        #endregion
    }
	/// <summary>
    /// ��Ϣ����s
	/// </summary>
    public class InfoList2s : InfoListBases
    {
        /// <summary>
        /// ��Ϣ����s
        /// </summary>
        public InfoList2s()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new InfoList2();
            }
        }
    }
}
