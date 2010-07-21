using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
   
    /// <summary>
    /// GroupField
    /// </summary>
    public class GroupFieldAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string EnName = "EnName";
        /// <summary>
        /// Lab
        /// </summary>
        public const string Lab = "Lab";
        /// <summary>
        /// RowIdx
        /// </summary>
        public const string RowIdx = "RowIdx";
    }
    /// <summary>
    /// GroupField
    /// </summary>
    public class GroupField : EntityOID
    {
        #region ����
        public bool IsUse = false;
        public string EnName
        {
            get
            {
                return this.GetValStrByKey(GroupFieldAttr.Lab);
            }
            set
            {
                this.SetValByKey(GroupFieldAttr.EnName, value);
            }
        }
        public string Lab
        {
            get
            {
                return this.GetValStrByKey(GroupFieldAttr.Lab);
            }
            set
            {
                this.SetValByKey(GroupFieldAttr.Lab, value);
            }
        }
        public int RowIdx
        {
            get
            {
                return this.GetValIntByKey(GroupFieldAttr.RowIdx);
            }
            set
            {
                this.SetValByKey(GroupFieldAttr.RowIdx, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// GroupField
        /// </summary>
        public GroupField()
        {
        }
        public GroupField(int oid):base (oid)
        {
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_GroupField");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "GroupField";
                map.EnType = EnType.Sys;

                map.AddTBIntPKOID();
                map.AddTBString(GroupFieldAttr.Lab, null, "Lab", true, false, 0, 30, 20);
                map.AddTBString(GroupFieldAttr.EnName, null, "����", true, false, 0, 30, 20);
                map.AddTBInt(GroupFieldAttr.RowIdx, 99, "RowIdx", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// GroupFields
    /// </summary>
    public class GroupFields : EntitiesOID
    {
        #region ����
        /// <summary>
        /// GroupFields
        /// </summary>
        public GroupFields()
        {
        }
        /// <summary>
        /// GroupFields
        /// </summary>
        /// <param name="EnName">s</param>
        public GroupFields(string EnName)
        {
            this.Retrieve(GroupFieldAttr.EnName, EnName, GroupFieldAttr.RowIdx);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GroupField();
            }
        }
        #endregion
    }
}
