using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.WF
{
    /// <summary>
    /// ��������
    /// </summary>
    public class CHOfQLAttr : EntityNoNameAttr
    {
        public const string FK_Node = "FK_Node";
        public const string Desc = "Desc";
        public const string FK_Flow = "FK_Flow";
        public const string Cent = "Cent";
        public const string Ext = "Ext";
        public const string FK_Emp = "FK_Emp";
        public const string RDT = "RDT";
        public const string WorkID = "WorkID";
        /// <summary>
        /// MyPK
        /// </summary>
        public const string MyPK = "MyPK";
        /// <summary>
        /// ��¼��
        /// </summary>
        public const string Rec = "Rec";
        public const string FK_NY = "FK_NY";
        public const string FK_Dept = "FK_Dept";
        public const string Note1 = "Note1";
        public const string Note2 = "Note2";
    }
	/// <summary>
	/// ��������
	/// </summary>
	public class CHOfQL :Entity
	{
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.Readonly();
                return uac;
            }
        }
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(CHOfQLAttr.WorkID);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.WorkID, value);
            }
        }
        public string Note1
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.Note1);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.Note1, value);
            }
        }
        public string Note2
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.Note2);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.Note2, value);
            }
        }

        public string FK_Node
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.FK_Node, value);
            }
        }
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.FK_Flow, value);
            }
        }
        public string FK_NodeText
        {
            get
            {
                return this.GetValRefTextByKey(CHOfQLAttr.FK_Node);
            }
        }
        public string FK_EmpText
        {
            get
            {
                return this.GetValRefTextByKey(CHOfQLAttr.FK_Emp);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.RDT);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.RDT, value);
            }
        }
        public string Ext
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.Ext);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.Ext, value);
            }
        }
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.FK_Emp, value);
            }
        }
        public string Desc
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.Desc);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.Desc, value);
            }
        }
        public string FK_NY
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.FK_NY);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.FK_NY, value);
            }
        }
        public string MyPK
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.MyPK);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.MyPK, value);
            }
        }
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(CHOfQLAttr.Rec);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.Rec, value);
            }
        }
        public string FK_FlowText
        {
            get
            {
                return this.GetValRefTextByKey(CHOfQLAttr.FK_Flow);
            }
        }
        public int Cent
        {
            get
            {
                return this.GetValIntByKey(CHOfQLAttr.Cent);
            }
            set
            {
                this.SetValByKey(CHOfQLAttr.Cent, value);
            }
        }
        
		#region ʵ�ֻ����ķ�����	
        
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_CHOfQL");
                map.EnDesc = "��������";
                map.EnType = EnType.App;
                map.DepositaryOfEntity = Depositary.Application;

                map.AddMyPK();

                map.AddDDLEntities(CHOfQLAttr.FK_Flow, null, "����", new Flows(), true);
                map.AddDDLEntities(CHOfQLAttr.FK_Node, null, "�ڵ�", new NodeExts(), true);

                map.AddDDLEntities(CHOfQLAttr.Rec, null, "���˶���", new Port.Emps(), true);
                map.AddTBInt(CHOfQLAttr.Cent, 0, "��ֵ", true, false);

                map.AddDDLEntities(CHOfQLAttr.FK_Emp, null, "������", new Port.Emps(), true);
                map.AddTBDate(CHOfQLAttr.RDT, "����ʱ��", true, true);


                map.AddTBStringDoc(CHOfQLAttr.Note1, "", "����ԭ��", true, true);
                map.AddTBStringDoc(CHOfQLAttr.Note2, "", "��ע", true, true);


                map.AddDDLEntities(CHOfQLAttr.FK_NY, null, "����", new BP.Pub.NYs(), true);
                map.AddDDLEntities(CHOfQLAttr.FK_Dept, null, "����", new BP.Port.Depts(), true);
                map.AddTBInt(CHOfQLAttr.WorkID, 0, "WorkID", false, false);


                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("WorkRpt", "��������"); // "��������";
                rm.ClassMethodName = this.ToString() + ".DoRpt";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("DoCheck", "ִ�п���"); // "ִ�п���";
                rm.ClassMethodName = this.ToString() + ".DoCheck";

                Attrs attrs = new Attrs();
                attrs.AddTBString(CHOfQLAttr.Rec, "��ǰ����Ա", "�����˶���", true, true, 0, 100, 10);
                attrs.AddTBInt(CHOfQLAttr.Cent, 0, "��/�۷�(��������ʾ)", true, false );
                attrs.AddTBStringDoc(CHOfQLAttr.Note1, "", "����ԭ��", true, false);
                attrs.AddTBStringDoc(CHOfQLAttr.Note2, "", "��ע", true, false);
                rm.HisAttrs = attrs;

                map.AddRefMethod(rm);

                map.AddSearchAttr(CHOfQLAttr.FK_Dept);
                map.AddSearchAttr(CHOfQLAttr.FK_Emp);
                map.AddSearchAttr(CHOfQLAttr.FK_NY);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

        public string DoRpt()
        {
            PubClass.WinOpen("../WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FK_Node=" + this.FK_Node);
            return null;
        }
        public string DoCheck(string tmp, int cent, string note1, string note2)
        {
            if (this.FK_Emp != Web.WebUser.No)
                return "�����ǿ����ˣ�����������ִ�С�";
 

            if (cent == 0)
                return "���˷�ֵΪ0��ϵͳ�ܾ�ִ�С�";

           

            this.Cent = cent;
            this.Note1 = note1;
            this.Note2 = note2;
            this.Update();

            return "����ִ�гɹ���";
        }

		#region ���췽��
		/// <summary>
		/// ��������
		/// </summary> 
        public CHOfQL(Int64 workid, string FK_Node)
        {
            BP.WF.CHOfFlow ch = new CHOfFlow(workid);
            this.Copy(ch);

            this.WorkID = workid;
            this.FK_Node = FK_Node;

            Node nd = new Node(int.Parse(this.FK_Node));


            BP.WF.Work wk = nd.HisWork;
            wk.OID = workid;
            wk.RetrieveFromDBSources();

            this.Rec = wk.Rec;
            this.RDT = wk.RDT;
            this.FK_Emp = Web.WebUser.No;


            this.MyPK = this.FK_Node + "@" + Web.WebUser.No + "@" + this.WorkID;
            if (ch.RetrieveFromDBSources() == 0)
                this.Insert();

        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="mypk"></param>
        public CHOfQL(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }
        public CHOfQL()
        {
        }
		#endregion 
	}

    public class CHOfQLs : EntitiesNoName
	{
		/// <summary>
		/// ��������
		/// </summary>
		public CHOfQLs(){}
		/// <summary>
		/// �õ����� ��������
		/// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new CHOfQL();
            }
        }
      
	}
}
