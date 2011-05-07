using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using BP.DA;

using BP.En;
using BP.Sys;
using BP.Win32.Controls;
using System.IO;
using System.Text;
using BP.Web ; 
using BP.Win32.Comm;
using BP.DTS;
//using Cells = SourceGrid2.Cells.Real;  
 
namespace BP.Win32
{  
	/// <summary>  
	/// FrmBase ��ժҪ˵����
	/// </summary>
    public class PageBase : System.Windows.Forms.Form
    {
        public string ToE(string no, string chVal)
        {
            return BP.Sys.Language.GetValByUserLang(no, chVal);
        }
        //D:\�г�\�Ͼ�˰ͨ�����Ʒ�ֲ� 

        public void ShowIt(System.Windows.Forms.Form parentFrm)
        {
            if (parentFrm == null)
                return;

            foreach (System.Windows.Forms.Form f in parentFrm.MdiChildren)
            {
                if (f.Tag == this.Tag)
                {
                    f.Activate();
                    return;
                }
            }
            this.MdiParent = parentFrm;
            this.Show();
        }
        public void RunTextFile(string file, string msg)
        {
        }

        public void RunExeFile(string file, string msg)
        {
            try
            {
                System.Diagnostics.Process.Start(file);
            }
            catch (Exception ex)
            {
                this.ResponseWriteRedMsg(msg + " \n�쳣��Ϣ:" + ex.Message + " filename:" + file);
            }
        }

        public void OpenWord(string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " file= " + fileName);
            }
            //System.Diagnostics.Process.Start("word.exe", fileName)  ;

            //System.Diagnostics.Process( fileName); 
        }
        protected bool Question(string msg)
        {
            return PubClass.Question(msg);
        }
        protected bool Warning(Exception ex)
        {
            return PubClass.Warning(ex.Message);
        }

        protected bool Warning(string msg)
        {
            return PubClass.Warning(msg);
        }
        private void ShowInTaskBarEx()
        {

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            // Do not allow form to be displayed in taskbar.
            this.ShowInTaskbar = false;

        }

        private DataTable _Table = null;

        protected DataTable Table
        {
            get
            {
                if (this._Table == null)
                    this._Table = new DataTable();

                return this._Table;
            }
            set
            {
                this._Table = value;

            }
        }


        #region ���÷���
        /// <summary>
        /// ���ݿ����
        /// </summary>
        protected void InvokSqlLogClear()
        {
            this.RunExeFile("D:\\WebApp\\tools\\SqlLogClear\\SqlLogClear.exe", "���ݿ����");
        }
        protected void InvokUIDTS()
        {
            FrmDTS dts = new FrmDTS();
            dts.ShowDialog();
            return;
        }
        protected void InvokUIDTS(System.Windows.Forms.Form frm)
        {
            FrmDTS dts = new FrmDTS();
            dts.MdiParent = frm;
            dts.Show();
            return;
        }
        protected void InvokUIUserEns(EnType type)
        {
            UIUserEns ui = new UIUserEns();
            ui.BindByEnType(type);
            return;
        }

        public void InvokUIEn(Entity en, bool IsReadonly)
        {
            try
            {
                BP.Win32.Comm.En ui = new BP.Win32.Comm.En();
                ui.Bind2(en);
                ui.ShowDialog();
            }
            catch (Exception ex)
            {
                en.CheckPhysicsTable();
                this.Warning(ex);
            }
            return;
        }
        public void InvokUIEn_del(Entity en, UAC uac)
        {
            try
            {
                BP.Win32.Comm.En ui = new BP.Win32.Comm.En();
                //ui.Bind2(en, uac.IsUpdate );
                ui.MdiParent = this;
                ui.Show();
            }
            catch (Exception ex)
            {
                en.CheckPhysicsTable();
                this.Warning(ex);
            }
            return;
        }
        public void InvokUIEn(Entity en, UAC uac)
        {
            try
            {
                UIEn ui = new UIEn();
                ui.SetData(en, uac.IsUpdate);
                ui.ShowDialog();

            }
            catch (Exception ex)
            {
                en.CheckPhysicsTable();
                this.Warning(ex);
            }
            return;
        }
        /// <summary>
        /// �����ַ���������ϵͳ�Զ��ж�Ȩ��
        /// </summary>
        /// <param name="ens">Ҫ�༭��Entities</param>
        public void InvokUIEns(Entities ens, System.Windows.Forms.Form frm)
        {
            UAC uac = ens.GetNewEntity.HisUAC; //  new SysEnsUAC(ens,WebUser.No) ; 
            UIEns ui = new UIEns();
            ui.BindEns(ens, uac, true);
            ui.MdiParent = frm;
            ui.Show();
            return;
        }
        public void InvokUIEns(Entities ens, BP.En.UAC uac, System.Windows.Forms.Form frm)
        {
            UIEns ui = new UIEns();
            ui.BindEns(ens, uac, true);
            ui.MdiParent = frm;
            ui.Show();
            return;
        }
        protected void InvokUIEns(Entities ens)
        {
            UAC uac = ens.GetNewEntity.HisUAC; //, WebUser.No) ; 
            UIEns ui = new UIEns();
            ui.BindEns(ens, uac, true);
            ui.Show();
            return;
        }
        /// <summary>
        /// InvokUIEns Ȩ�޿���
        /// </summary>
        /// <param name="ens"></param>
        /// <param name="uac">Ȩ�޿���</param>
        protected void InvokUIEns(Entities ens, UAC uac)
        {
            UIEns ui = new UIEns();
            ui.BindEns(ens, uac, true);
            ui.ShowDialog();
            return;
        }

        //		/// <summary>
        //		/// ����UIEns
        //		/// </summary>
        //		/// <param name="ens"></param>
        //		/// <param name="IsReadonly"></param>
        //		protected void InvokUIEns(Entities ens,bool IsReadonly)
        //		{
        //			UIEns ui = new UIEns() ;
        //			ui.BindEns(ens,IsReadonly,true);
        //			ui.ShowDialog();
        //			return;			
        //		}
        #endregion

        #region  ���������ļ�
        /// <summary>
        /// �����ļ���·��
        /// </summary>
        protected string ExportFilePath
        {
            get
            {
                return "d:\\Report\\Exported\\";
            }
        }

        /// <summary>
        /// ����ļ���ʽ�ĵ�
        /// </summary>
        /// <param name="en"></param>
        public void ExportExcelExcelTemplate(Entity en, string file)
        {
            Attrs attrs = en.EnMap.Attrs;

            //string filename = en.EnDesc+".xls";
            //string file=en.EnDesc;
            //string filepath= filePath;

            //			//�������Ŀ¼û�н���������.
            //			if (Directory.Exists(filepath) == false) 
            //				Directory.CreateDirectory(filepath);
            //
            //			//filename = filepath + filename;
            FileStream objFileStream = new FileStream(file, FileMode.Create, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
        #endregion

            #region ���ɵ����ļ�
            string strLine = "";
            foreach (Attr attr in attrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;

                strLine += attr.Desc + Convert.ToChar(9);
            }
            objStreamWriter.WriteLine(strLine);
            objStreamWriter.Close();
            objFileStream.Close();
            PubClass.Information("�ļ���ʽ�Ѿ������: \n" + file);
            //this.ResponseWriteBlueMsg("�ļ���ʽ�Ѿ������: "+filename);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="file"></param>
        public void ExportDGToExcel(DG dg, string file)
        {
            Map map = dg.HisEn.EnMap;

            #region ���嵼�����ļ�
            //			string filename = map.EnDesc +DateTime.Now.ToString("yyyyMMddhhss") + ".xls";
            //			string file=filename;
            //			//bool flag = true;
            //			string filepath=this.ExportFilePath;
            //			
            //			//�������Ŀ¼û�н���������.
            //			if (Directory.Exists(filepath) == false) 
            //				Directory.CreateDirectory(filepath);
            //
            //			filename = filepath + filename;
            FileStream objFileStream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
            #endregion

            //			int i =0 ;
            //objStreamWriter.WriteLine();
            //objStreamWriter.WriteLine(Convert.ToChar(9) + Convert.ToChar(9)+ "���ݵ���1.0" + Convert.ToChar(9) );
            //objStreamWriter.WriteLine();

            DataSet ds = new DataSet();
            try
            {
                DataTable dt = (DataTable)dg.DataSource;
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                this.Alert("������DataSetת��" + ex.Message);
            }

            //���ݱ���ʽ
            foreach (DataGridTableStyle dts in dg.TableStyles)
            {
                //objStreamWriter.WriteLine(Convert.ToChar(9) + "����"+dts.MappingName + Convert.ToChar(9) );
                /*�������*/
                foreach (Attr attr in map.Attrs)
                {

                    if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum || attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                        continue;

                    if (attr.UIVisible == false)
                    {
                        if (attr.MyFieldType != FieldType.RefText)
                            continue;
                    }
                    if (attr.MyFieldType == FieldType.RefText)
                        objStreamWriter.Write(attr.Desc.Substring(0, attr.Desc.Length - 2));
                    else
                        objStreamWriter.Write(attr.Desc);

                    objStreamWriter.Write("\t");
                }
                objStreamWriter.WriteLine();

                DataTable dt = ds.Tables[0];

                //				objStreamWriter.Write("\r\n");
                /*�������*/
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (Attr attr in map.Attrs)
                    {
                        if (attr.MyFieldType == FieldType.RefText)
                        {
                            objStreamWriter.Write(dr[attr.Key]);
                            objStreamWriter.Write('\t');
                            continue;
                        }

                        if (attr.UIVisible == false)
                            continue;
                        if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum || attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                            continue;
                        objStreamWriter.Write(dr[attr.Key]);
                        objStreamWriter.Write('\t');
                    }
                    objStreamWriter.WriteLine();
                }
            }

            #region ����Ŀ¼
            objStreamWriter.Close();
            objFileStream.Close();
            PubClass.Information("���ݵ����ɹ�,���λ��:\n" + file);
            #endregion

        }

        /// <summary>
        /// ExportToXml
        /// </summary>
        /// <param name="ens"></param>
        public void ExportToXml(BP.En.Entities ens)
        {
            string file = ens.GetNewEntity.EnMap.PhysicsTable + ".xml";
            string filepath = this.ExportFilePath;

            if (Directory.Exists(filepath) == false)
                Directory.CreateDirectory(filepath);

            string fileName = filepath + file;

            DataSet ds = ens.RetrieveAllToDataSet();
            /*WriteSchema �Թ�ϵ�ṹ��Ϊ���� XML �ܹ�
            IgnoreSchema �� XML ������ʽ��д DataSet �ĵ�ǰ���ݣ������� XML �ܹ�
             DiffGram ��ǰ�ͳ�ʼ�汾�� XML ��ʽ*/
            ds.WriteXml(fileName, System.Data.XmlWriteMode.DiffGram);
            //			this.Alert("���ݵ����ɹ�!!"+fileName);
            MessageBox.Show(fileName, "���ݵ����ɹ�!");


        }

            #endregion  �����ļ�


        #region ShowDefaultValue
        /// <summary>
        /// ��ʾ����Ĭ��ֵ
        /// </summary>
        /// <param name="en">ʵ��</param>
        /// <param name="attrKey">����</param>
        protected void ShowDefaultValue(Entity en, string attrKey)
        {
            UIWinDefaultValues window = new UIWinDefaultValues(en, attrKey);
        }
        #endregion

        #region ʵ����� Ens
        private Entity _HisEn = null;
        public Entity HisEn
        {
            get
            {
                if (this._HisEn == null)
                {
                    // throw new Exception("@û��Ϊ HisEn ����ֵ��");
                    //  _HisEn = new Emp();
                }
                return _HisEn;
            }
            set
            {
                _HisEn = value;
            }
        }
        private Entities _HisEns = null;
        public Entities HisEns
        {
            get
            {
                return _HisEns;
            }
            set
            {
                _HisEns = value;
            }
        }
        #endregion



        #region  ��ʾ��Ϣ

        protected void ResponseWriteRedMsg(Exception ex)
        {
            PubClass.Alert(ex.Message);
            //MessageBox.Show(ex.Message,"����");
        }
        protected void ResponseWriteRedMsg(string msg)
        {
            PubClass.Alert(msg);

            //MessageBox.Show(msg.Replace("@","\n"),"����");
        }
        protected void ResponseWriteBlueMsg(string msg)
        {
            MessageBox.Show(this, msg.Replace("@", "\n"), "��ʾ");
        }
        protected void ResponseWriteBlueMsg_DeleteOK()
        {
            MessageBox.Show("ɾ���ɹ�!", "��ʾ");
        }
        protected void ResponseWriteBlueMsg_DeleteOK(int delNum)
        {
            MessageBox.Show("��¼" + delNum + "ɾ���ɹ�!", "��Ϣɾ����ʾ");

        }
        protected void ResponseWriteBlueMsg_UpdataOK()
        {
            MessageBox.Show("���³ɹ�!", "��Ϣ������ʾ");

        }
        protected void ResponseWriteBlueMsg_UpdataOK(int updateNum)
        {
            MessageBox.Show("����" + updateNum + "�ɹ�!", "��Ϣ������ʾ");

        }
        protected void ResponseWriteBlueMsg_SaveOK()
        {
            MessageBox.Show("����ɹ�!", "��Ϣ������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        protected void ResponseWriteBlueMsg_InsertOK()
        {
            MessageBox.Show("����ɹ�!", "��ʾ");

        }
        protected void ResponseWriteBlueMsg_SaveOK(int saveNum)
        {
            MessageBox.Show("����" + saveNum + "�ɹ�!", "��ʾ");
        }
        protected void Alert(string msg)
        {
            PubClass.Alert(msg);

        }
        protected void Information(string msg)
        {
            PubClass.Information(msg);
        }
        protected void Alert(Exception ex)
        {
            this.Alert(ex.Message);
        }
        /// <summary>
        /// �˳�ϵͳ
        /// </summary>
        protected void Exit()
        {
            if (this.Question("��ȷ��Ҫ�˳���ϵͳ��"))
                Application.Exit();
        }




        #endregion

        #region Windows ������������ɵĴ���
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;

        public PageBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }



        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // PageBase
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Name = "PageBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmBase";
            this.Load += new System.EventHandler(this.PageBase_Load);

        }
        #endregion

        private void PageBase_Load(object sender, System.EventArgs e)
        {

        }
    }
}
