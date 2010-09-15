

using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ����
    /// </summary>
    public class InfoReqSortAttr : EntityNoNameAttr
    {
        public const string IsShowDtl = "IsShowDtl";
    }
	/// <summary>
	/// ���
	/// </summary>
    public class InfoReqSort : EntityNoName
    {
        #region ���캯��
        /// <summary>
        /// ���
        /// </summary>
        public InfoReqSort()
        {
        }
        public InfoReqSort(string no)
        {
            this.No = no;
            try
            {
                this.Retrieve();
            }
            catch
            {

            }
        }
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("GE_InfoReqSort");
                map.EnDesc = "���";
                //map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.ImgLinks' >���</a>";

                map.IsAutoGenerNo = false;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(InfoReqSortAttr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(InfoReqSortAttr.Name, null, "����", true, false, 0, 50, 300);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ��� 
	/// </summary>
    public class InfoReqSorts : EntitiesNoName
    {
        /// <summary>
        /// ��ȡ���
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new InfoReqSort();
            }
        }

        #region ���캯��
        /// <summary>
        /// ���
        /// </summary>
        public InfoReqSorts()
        {
        }
        #endregion

    }
}
 

