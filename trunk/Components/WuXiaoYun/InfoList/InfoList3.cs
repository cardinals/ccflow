using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
	 	/// <summary>
    /// ��Ϣ����
	/// </summary>
    public class InfoList3Attr : InfoListBaseAttr
    {
    }
	/// <summary>
    /// ��Ϣ����
	/// </summary>
    public class InfoList3 : InfoListBase
    {
        /// <summary>
        /// ��ȡTable
        /// </summary>
        public override string PTable
        {
            get
            {
                return "GE_InfoList3";
            }
        }
        /// <summary>
        /// ��һƪ����
        /// </summary>
        public InfoList3 PreviousInfo
        {
            get
            {
                InfoList3 en = new InfoList3();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere("GE_InfoList3.No < '" + this.No + "'");
                qo.addAnd();
                qo.AddWhereNotIn(InfoList3Attr.InfoListSta, "0");
                qo.Top = 1;
                qo.addOrderByDesc(InfoList3Attr.No);
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
        public InfoList3 NextInfo
        {
            get
            {
                InfoList3 en = new InfoList3();
                QueryObject qo = new QueryObject(en);
                qo.AddWhere("GE_InfoList3.No > '" + this.No + "'");
                qo.addAnd();
                qo.AddWhereNotIn(InfoList3Attr.InfoListSta, "0");
                qo.Top = 1;
                qo.addOrderBy(InfoList3Attr.No);
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
        public InfoList3(string no)
            : base(no)
        {

        }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public InfoList3()
        {
        }
        #endregion
    }
	/// <summary>
    /// ��Ϣ����s
	/// </summary>
    public class InfoList3s : InfoListBases
    {
        /// <summary>
        /// ��Ϣ����s
        /// </summary>
        public InfoList3s()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new InfoList3();
            }
        }
    }
}
