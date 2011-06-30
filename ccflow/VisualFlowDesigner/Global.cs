using System;
using System.Collections;
using System.IO;
using System.Data;
using System.Windows.Forms;
using BP.WF.Design;
using BP.WF;
using BP.Win.WF;
using BP.Win.Controls;

namespace BP.WF
{
	/// <summary>
	/// Global ��ժҪ˵����
	/// </summary>
    public class Global
    {
        #region ��������
        public static string Host
        {
            get
            {
                string s = BP.SystemConfig.AppSettings["BPMHost"];
                if (s == null)
                    s = "127.0.0.1/Flow/";
                return s;
            }
        }
        static Global()
        {
            SystemConfig.IsBSsystem = false;
            System.DateTime dt = File.GetLastWriteTime(Application.ExecutablePath);
            VersionDate = dt.ToString("yyyy-MM-dd HH:mm");
        }
        #endregion ��������

        #region Ver
        public static readonly string VersionDate = "2004-03-16 15:26"; //  
        public static readonly string UpdateItems = "";//"\r\n�˸��²�����Ŀ¼��Image���������½ʧ�ܣ����ֶ�����Ŀ¼��Image���е��ļ���";//"\n���������Ŀ��BP.Tax.WF ->CUI�� ->YUE"; //
        private static string _updatePath = "";
        public static string UpdatePath
        {
            get
            {
                return _updatePath;
            }
        }
        public static readonly string AppConfigPath = Application.StartupPath + "\\App.config";
        private static string _RefConfigPath = "";
        public static string RefConfigPath
        {
            get
            {
                return _RefConfigPath;
            }
        }
        /// <summary>
        /// Ӧ�ó������·��
        /// </summary>
        public static string PathOfBase
        {
            get
            {
                string s = Application.StartupPath + @"\.\..\..\";
                DirectoryInfo dir = new DirectoryInfo(s);
                return dir.FullName;
            }
        }
        public static string PathOfVisualFlow
        {
            get
            {
                string s= Application.StartupPath + @"\.\..\..\..\VisualFlow\";
                DirectoryInfo dir = new DirectoryInfo(s);
                return dir.FullName;
            }
        }
        public static string PathOfVisualFlowDesigner
        {
            get
            {
                string s = Application.StartupPath + @"\.\..\..\..\VisualFlowDesigner\";
                DirectoryInfo dir = new DirectoryInfo(s);
                return dir.FullName;
            }
        }
        #endregion Ver

        #region ��½��Ϣ
        public static string User = "8888";
        public static string Right = "����Ա";
        public static string RightID = "00";
        public static MainForm MainForm;
        public static BP.Port.Emp LoadEmp
        {
            get
            {
                return BP.Win.WF.Global.LoadEmp;
            }
            set
            {
                BP.Win.WF.Global.LoadEmp = value;
            }
        }
        #endregion ��½��Ϣ

        #region ��½
        public static void DoEdit(BP.En.Entities ens)
        {
            BP.Win32.FrmIE ie = new BP.Win32.FrmIE();
            ie.ShowIE("http://" + Global.Host + "/DoPort.aspx?EnsName=" + ens.ToString() + "&DoType=Ens" + "&Lang=" + BP.Web.WebUser.SysLang);
        }
        public static void DoEdit(BP.En.Entity en)
        {
            BP.Win32.FrmIE ie = new BP.Win32.FrmIE();
            ie.ShowIE("http://" + Global.Host + "/DoPort.aspx?EnName=" + en.ToString() + "&DoType=En&PK=" + en.PKVal + "&Lang=" + BP.Web.WebUser.SysLang);
        }
        /// <summary>
        /// ִ��URL
        /// </summary>
        /// <param name="url"></param>
        public static void DoUrl(string url)
        {
            //  BP.Win32.FrmIE ie = new BP.Win32.FrmIE();

            BP.Win32.FrmIE ie = new BP.Win32.FrmIE();
            //�޸Ĳ��� by David 2011-06-29
            if (url.StartsWith("/"))
            {
                ie.ShowIE("http://" + Global.Host + url + "&Lang=" + BP.Web.WebUser.SysLang);
            }
            else
            {
                ie.ShowIE("http://" + Global.Host + "/" + url + "&Lang=" + BP.Web.WebUser.SysLang);
            }
            //ԭ������
            // ie.ShowIE("http://" + Global.Host + "/" + url + "&Lang=" + BP.Web.WebUser.SysLang);
            //�޸Ľ���
            //   ie.ShowIE("http://" + Global.Host + "/" + url + "&Lang=" + BP.Web.WebUser.SysLang);
            // ie.ShowIE("http://localhost/Front/DoPort.aspx?En=" + en.ToString() + "&DoType=En&PK=" + en.PKVal);
        }
        public static void DoUrlByType(string type, string refNo)
        {
            if (type.Contains("RunFlow"))
            {
                DoIE("http://" + Global.Host + "/DoPort.aspx?DoType=" + type + "&Lang=" + BP.Web.WebUser.SysLang);
                return;
            }

            BP.Win32.FrmIE ie = new BP.Win32.FrmIE();
            switch (type)
            {
                case "RunFlow":
                    DoIE("http://" + Global.Host + "/DoPort.aspx?DoType=" + type + "&Lang=" + BP.Web.WebUser.SysLang);
                    break;
                default:
                    ie.ShowIE("http://" + Global.Host + "/DoPort.aspx?DoType=" + type + "&RefNo=" + refNo + "&Lang=" + BP.Web.WebUser.SysLang);
                    break;
            }
        }
        public static void DoIE(string url)
        {
            try
            {
                System.Diagnostics.Process.Start("IExplore.exe", url);
            }
            catch
            {
                try
                {
                    System.Diagnostics.Process.Start(@"C:\Program Files\Internet Explorer\IExplore.exe", url);
                }
                catch
                {
                    System.Diagnostics.Process.Start(@"D:\Program Files\Internet Explorer\IExplore.exe", url);
                }
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string JieMi(string str)
        {
            if ((str.Length % 4) != 0)
            {
                throw new ArgumentException("������ȷ��BASE64���룬���顣", "str");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(str, "^[A-Z0-9/+=]*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("��������ȷ��BASE64���룬���顣", "str");
            }
            string Base64Code = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+/=";
            int page = str.Length / 4;
            System.Collections.ArrayList outMessage = new System.Collections.ArrayList(page * 3);
            char[] message = str.ToCharArray();
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[4];
                instr[0] = (byte)Base64Code.IndexOf(message[i * 4]);
                instr[1] = (byte)Base64Code.IndexOf(message[i * 4 + 1]);
                instr[2] = (byte)Base64Code.IndexOf(message[i * 4 + 2]);
                instr[3] = (byte)Base64Code.IndexOf(message[i * 4 + 3]);
                byte[] outstr = new byte[3];
                outstr[0] = (byte)((instr[0] << 2) ^ ((instr[1] & 0x30) >> 4));
                if (instr[2] != 64)
                {
                    outstr[1] = (byte)((instr[1] << 4) ^ ((instr[2] & 0x3c) >> 2));
                }
                else
                {
                    outstr[2] = 0;
                }
                if (instr[3] != 64)
                {
                    outstr[2] = (byte)((instr[2] << 6) ^ instr[3]);
                }
                else
                {
                    outstr[2] = 0;
                }
                outMessage.Add(outstr[0]);
                if (outstr[1] != 0)
                    outMessage.Add(outstr[1]);
                if (outstr[2] != 0)
                    outMessage.Add(outstr[2]);
            }
            byte[] outbyte = (byte[])outMessage.ToArray(Type.GetType("System.Byte"));
            return System.Text.Encoding.Default.GetString(outbyte);
        }
        private static int times = 1;
        /// <summary>
        /// װ������ͨ���ļ�
        /// </summary>
        /// <returns></returns>
        public static void LoadConfigByFile()
        {
            BP.WF.Glo.IntallPath = BP.WF.Global.PathOfVisualFlow;

            string path = BP.WF.Global.PathOfVisualFlow + "\\web.config"; //���������ļ���װ������
            if (System.IO.File.Exists(path) == false)
                throw new Exception("�����ļ�û���ҵ�:" + path);


            BP.DA.ClassFactory.LoadConfig(path);
            try
            {
                BP.Port.Emp em = new BP.Port.Emp("admin");
            }
            catch
            {
                BP.Port.Emp em = new BP.Port.Emp("admin");
            }

            BP.SystemConfig.IsBSsystem_Test = false;
            BP.SystemConfig.IsBSsystem = false;
            SystemConfig.IsBSsystem = false;
            BP.Win.WF.Global.FlowImagePath = BP.WF.Global.PathOfVisualFlow + "\\DataUser\\FlowDesc\\";

            BP.Web.WebUser.SysLang = BP.WF.Glo.Language;
            BP.SystemConfig.IsBSsystem_Test = false;
            BP.SystemConfig.IsBSsystem = false;
            SystemConfig.IsBSsystem = false;
        }
        #endregion

        #region Ӧ�ÿ���
        public static bool IsExitProcess(string name)
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pro in processes)
            {
                if (pro.ProcessName + ".exe" == name)
                    return true;
            }
            return false;
        }
        public static bool KillProcess(string name)
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pro in processes)
            {
                if (pro.ProcessName == name)
                {
                    pro.Kill();
                    return true;
                }
            }
            return false;
        }

        static bool Application_Start()
        {
            try
            {
                #region ɾ��word ��ת��ע�ᡣ
                Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey software = hklm.OpenSubKey("SOFTWARE", true);

                //��"SOFTWARE"�Ӽ� 
                Microsoft.Win32.RegistryKey no1 = software.OpenSubKey("Microsoft", true);
                //��"aaa"�Ӽ� 
                Microsoft.Win32.RegistryKey no2 = no1.OpenSubKey("Shared Tools", true);

                //��"aaa"�Ӽ� 
                Microsoft.Win32.RegistryKey no3 = no1.OpenSubKey("Text Converters", true);

                // Import
                Microsoft.Win32.RegistryKey no4 = no1.OpenSubKey("Import", true);
                no4.DeleteValue("MSWord6.wpc");
                #endregion ɾ��word. ��ת��ע�ᡣ
            }
            catch
            {

            }


            if (BP.WF.Global.IsExitProcess("VisualFlowAutoUpdate.exe"))
            {
                MessageBox.Show("�۳ҹ������������Ӧ�ó����Ѿ�������������ͬʱ���������������ڡ�", "������ʾ",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }


            BP.Win32.FrmLanguage lan = new BP.Win32.FrmLanguage();
            lan.ShowDialog();

            try
            {
                LoadConfigByFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show("װ�������ļ�ʱ���ִ�����������ǻ�ȡ����֧�� http://ccflow.cn \n������Ϣ��" + ex.Message, "ϵͳ���", MessageBoxButtons.OK,
                     MessageBoxIcon.Error);

                Application.Exit();
            }

            MainForm = new MainForm();
            Win.WF.Global.ToolCursors = new Cursor[7];//=[MainForm.wfToolBar1.ButtonsCount];
            Win.WF.Global.ToolCursors[0] = Cursors.Default;
            Win.WF.Global.ToolCursors[1] = Cursors.Cross;
            Win.WF.Global.ToolCursors[2] = Cursors.Cross;
            Win.WF.Global.ToolCursors[3] = Cursors.Cross;
            Win.WF.Global.ToolCursors[4] = Cursors.Cross;
            Win.WF.Global.ToolCursors[5] = Cursors.Cross;
            Win.WF.Global.ToolCursors[6] = Cursors.Cross;
            Win.WF.Global.WFTools = MainForm.wfToolBar1;
            return true;
        }
        public static void Update()
        {
            MainForm.Dispose();
            System.Diagnostics.Process.Start(Application.StartupPath + "\\OnlineUpdate.exe");
            Application.Exit();
        }

        [STAThread]
        static void Main()
        {

            if (!Application_Start())
                return;

            Application.Run(MainForm);
            if (MainForm != null)
                MainForm.Dispose();

            Application.Exit();
        }
        #endregion
    }
    public enum StartModel
    {
        /// <summary>
        /// �������������
        /// </summary>
        WorkFlow,
        /// <summary>
        /// ����
        /// </summary>
        DTS,
    }
}
