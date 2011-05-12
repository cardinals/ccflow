using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Win32;  
using BP.Sys;
using BP.DA;
using BP.En;
using BP;

namespace BP.WF
{
    public class Glo
    {
        public static void Rtf2PDF(object pathOfRtf, object pathOfPDF)
        {
            Object Nothing = System.Reflection.Missing.Value;
            //����һ����ΪWordApp���������    
            Microsoft.Office.Interop.Word.Application wordApp =
    new Microsoft.Office.Interop.Word.ApplicationClass();
            //����һ����ΪWordDoc���ĵ����󲢴�    
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref pathOfRtf, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
     ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
    ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);

            //���ñ���ĸ�ʽ    
            object filefarmat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;

            //����ΪPDF    
            doc.SaveAs(ref pathOfPDF, ref filefarmat, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
    ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
     ref Nothing, ref Nothing, ref Nothing);
            //�ر��ĵ�����    
            doc.Close(ref Nothing, ref Nothing, ref Nothing);
            //�Ƴ��齨    
            wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
            GC.Collect();
        }

        #region ����
        public static string SessionMsg
        {
            get
            {
                Paras p = new Paras();
                p.SQL = "SELECT Msg FROM WF_Emp where No=@FK_Emp";
                p.AddFK_Emp();
                return DBAccess.RunSQLReturnString(p);
            }
            set
            {
                Paras p = new Paras();
                p.SQL = "UPDATE WF_Emp SET Msg=@v WHERE No=@FK_Emp";
                p.AddFK_Emp();
                p.Add("v", value);
                DBAccess.RunSQL(p);
            }
        }
        #endregion ����

        #region ����
        private static string _FromPageType = null;
        public static string FromPageType
        {
            get
            {
                _FromPageType = null;
                if (_FromPageType == null)
                {
                    try
                    {
                        string url = System.Web.HttpContext.Current.Request.RawUrl;
                        int i = url.LastIndexOf("/") + 1;
                        int i2 = url.IndexOf(".aspx") - 6;

                        url = url.Substring(i);
                        url = url.Substring(0, url.IndexOf(".aspx"));
                        _FromPageType = url;
                        if (_FromPageType.Contains("SmallSingle"))
                            _FromPageType = "SmallSingle";
                        else if (_FromPageType.Contains("Small"))
                            _FromPageType = "Small";
                        else
                            _FromPageType = "";
                    }
                    catch (Exception ex)
                    {
                        _FromPageType = "";
                        //  throw new Exception(ex.Message + url + " i=" + i + " i2=" + i2);
                    }
                }
                return _FromPageType;
            }
        }

        private static Attrs _AttrsOfRpt = null;
        public static Attrs AttrsOfRpt
        {
            get
            {
                if (_AttrsOfRpt == null)
                {
                    _AttrsOfRpt = new Attrs();
                    _AttrsOfRpt.AddTBInt(GERptAttr.OID, 0, "WorkID", true, true);
                    _AttrsOfRpt.AddTBInt(GERptAttr.FID, 0, "FlowID", false, false);

                    _AttrsOfRpt.AddTBString(GERptAttr.Title, null, "����", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBString(GERptAttr.FlowStarter, null, "������", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBString(GERptAttr.FlowStartRDT, null, "����ʱ��", true, false, 0, 10, 10);

                    _AttrsOfRpt.AddTBString(GERptAttr.WFState, null, "״̬", true, false, 0, 10, 10);

                    //Attr attr = new Attr();
                    //attr.Desc = "����״̬";
                    //attr.Key = "WFState";
                    //attr.MyFieldType = FieldType.Enum;
                    //attr.UIBindKey = "WFState";
                    //attr.UITag = "@0=������@1=�Ѿ����";

                    _AttrsOfRpt.AddDDLSysEnum(GERptAttr.WFState, 0, "����״̬", true, true, GERptAttr.WFState);

                    _AttrsOfRpt.AddTBString(GERptAttr.FlowEmps, null, "������", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBString(GERptAttr.FlowEnder, null, "������", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBString(GERptAttr.FlowEnderRDT, null, "����ʱ��", true, false, 0, 10, 10);
                    _AttrsOfRpt.AddTBDecimal(GERptAttr.FlowDaySpan, 0, "���(��)", true, false);
                    //_AttrsOfRpt.AddTBString(GERptAttr.FK_NY, null, "�����·�", true, false, 0, 10, 10);
                }
                return _AttrsOfRpt;
            }
        }
        #endregion ����

        /// <summary>
        /// ��װ��
        /// </summary>
        public static void DoInstallDataBase(string lang,string yunXingHuanjing)
        {
            ArrayList al = null;
            string info = "BP.En.Entity";
            al = BP.DA.ClassFactory.GetObjects(info);

            #region 1, �޸���
            foreach (Object obj in al)
            {
                Entity en = null;
                en = obj as Entity;
                if (en == null)
                    continue;

                string table = null;
                try
                {
                    table = en.EnMap.PhysicsTable;
                    if (table == null)
                        continue;
                }
                catch
                {
                    continue;
                }

                switch (table)
                {
                    case "WF_EmpWorks":
                    case "WF_GenerEmpWorkDtls":
                    case "WF_GenerEmpWorks":
                    case "WF_NodeExt":
                        continue;
                    default:
                        en.CheckPhysicsTable();
                        break;
                }

                en.PKVal = "123";
                en.RetrieveFromDBSources();
            }
            #endregion �޸�

            #region 2,  ע��ö������ sql
            // 2, ע��ö�����͡�
            BP.Sys.Xml.EnumInfoXmls xmls = new BP.Sys.Xml.EnumInfoXmls();
            xmls.RetrieveAll();
            foreach (BP.Sys.Xml.EnumInfoXml xml in xmls)
            {
                BP.Sys.SysEnums ses = new BP.Sys.SysEnums();
                ses.RegIt(xml.Key, xml.Vals);
            }
            #endregion ע��ö������

            #region 3, ִ�л����� sql
            string sqlscript = BP.DA.DataType.ReadTextFile(SystemConfig.PathOfData + "\\Install\\SQLScript\\Port_" + yunXingHuanjing + "_" + lang + ".sql");
            BP.DA.DBAccess.RunSQLs(sqlscript);
            #endregion �޸�

            #region 4, ������ͼ��ϵͳ����
            sqlscript = BP.DA.DataType.ReadTextFile(SystemConfig.PathOfData + "\\Install\\SQLScript\\CreateViewSQL.sql");
            BP.DA.DBAccess.RunSQLs(sqlscript);
            #endregion ������ͼ��ϵͳ����
        }
        public static void KillProcess(string processName) //ɱ�����̵ķ���
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process pro in processes)
            {
                string name = pro.ProcessName + ".exe";
                if (name.ToLower() == processName.ToLower())
                    pro.Kill();
            }
        }
        public static string GenerFlowNo(string rptKey)
        {
            rptKey = rptKey.Replace("ND", "");
            rptKey = rptKey.Replace("Rpt", "");
            switch (rptKey.Length)
            {
                case 0:
                    return "001";
                case 1:
                    return "00" + rptKey;
                case 2:
                    return "0" + rptKey;
                case 3:
                    return rptKey;
                default:
                    return "001";
            }
            return rptKey;
        }
        /// <summary>
        /// 
        /// </summary>
        public static bool IsShowFlowNum
        {
            get
            {
                switch (SystemConfig.AppSettings["IsShowFlowNum"])
                {
                    case "1":
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// ������ʾ�û���ţ�
        /// ���ǵ����ĵĲ���ϵͳ����Ӣ�ĵĲ���ϵͳ��ͬ��
        /// 
        /// </summary>
        public static bool IsShowUserNoOnly
        {
            get
            {
                switch (SystemConfig.AppSettings["IsShowUserNoOnly"])
                {
                    case "1":
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// ����word�ĵ�.
        /// </summary>
        /// <param name="wk"></param>
        public static void GenerWord(object filename, Work wk)
        {
            BP.WF.Glo.KillProcess("WINWORD.EXE");
            string enName = wk.EnMap.PhysicsTable;
            try
            {
                RegistryKey delKey = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Shared Tools\Text Converters\Import\",
                    true);
                delKey.DeleteValue("MSWord6.wpc");
                delKey.Close();
            }
            catch
            {
            }

            GroupField currGF = new GroupField();
            MapAttrs mattrs = new MapAttrs(enName);
            GroupFields gfs = new GroupFields(enName);
            MapDtls dtls = new MapDtls(enName);
            foreach (MapDtl dtl in dtls)
                dtl.IsUse = false;

            // ���������Ԫ���������
            int rowNum = 0;
            foreach (GroupField gf in gfs)
            {
                rowNum++;
                bool isLeft = true;
                foreach (MapAttr attr in mattrs)
                {
                    if (attr.UIVisible == false)
                        continue;

                    if (attr.GroupID != gf.OID)
                        continue;

                    if (attr.UIIsLine)
                    {
                        rowNum++;
                        isLeft = true;
                        continue;
                    }

                    if (isLeft == false)
                        rowNum++;
                    isLeft = !isLeft;
                }
            }

            rowNum = rowNum + 2 + dtls.Count;

            // ����Word�ĵ�
            string CheckedInfo = "";
            string message = "";
            Object Nothing = System.Reflection.Missing.Value;

            //  object filename = fileName;

            Word.Application WordApp = new Word.ApplicationClass();
            Word.Document WordDoc = WordApp.Documents.Add(ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing);
            try
            {
                WordApp.ActiveWindow.View.Type = Word.WdViewType.wdOutlineView;
                WordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekPrimaryHeader;

                #region ����ҳü
                // ���ҳü ����ͼƬ
                string pict = SystemConfig.PathOfDataUser + "log.jpg"; // ͼƬ����·��
                if (System.IO.File.Exists(pict))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(pict);
                    object LinkToFile = false;
                    object SaveWithDocument = true;
                    object Anchor = WordDoc.Application.Selection.Range;
                    WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(pict, ref  LinkToFile,
                        ref  SaveWithDocument, ref  Anchor);
                    //    WordDoc.Application.ActiveDocument.InlineShapes[1].Width = img.Width; // ͼƬ���
                    //    WordDoc.Application.ActiveDocument.InlineShapes[1].Height = img.Height; // ͼƬ�߶�
                }
                WordApp.ActiveWindow.ActivePane.Selection.InsertAfter("[�۳�ҵ�����̹���ϵͳ http://ccflow.cn]");
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft; // �����Ҷ���
                WordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekMainDocument; // ����ҳü����
                WordApp.Selection.ParagraphFormat.LineSpacing = 15f; // �����ĵ����м��
                #endregion

                // �ƶ����㲢����
                object count = 14;
                object WdLine = Word.WdUnits.wdLine; // ��һ��;
                WordApp.Selection.MoveDown(ref  WdLine, ref  count, ref  Nothing); // �ƶ�����
                WordApp.Selection.TypeParagraph(); // �������

                // �ĵ��д������
                Word.Table newTable = WordDoc.Tables.Add(WordApp.Selection.Range, rowNum, 4, ref  Nothing, ref  Nothing);

                // ���ñ����ʽ
                newTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleThickThinLargeGap;
                newTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

                newTable.Columns[1].Width = 100f;
                newTable.Columns[2].Width = 100f;
                newTable.Columns[3].Width = 100f;
                newTable.Columns[4].Width = 100f;

                // ���������
                newTable.Cell(1, 1).Range.Text = wk.EnDesc;
                newTable.Cell(1, 1).Range.Bold = 2; // ���õ�Ԫ��������Ϊ����

                // �ϲ���Ԫ��
                newTable.Cell(1, 1).Merge(newTable.Cell(1, 4));
                WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter; // ��ֱ����
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter; // ˮƽ����

                int groupIdx = 1;
                foreach (GroupField gf in gfs)
                {
                    groupIdx++;
                    // ���������
                    newTable.Cell(groupIdx, 1).Range.Text = gf.Lab;
                    newTable.Cell(groupIdx, 1).Range.Font.Color = Word.WdColor.wdColorDarkBlue; // ���õ�Ԫ����������ɫ
                    newTable.Cell(groupIdx, 1).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray25;
                    // �ϲ���Ԫ��
                    newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 4));
                    WordApp.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    groupIdx++;

                    bool isLeft = true;
                    bool isColumns2 = false;
                    int currColumnIndex = 0;
                    foreach (MapAttr attr in mattrs)
                    {
                        if (attr.UIVisible == false)
                            continue;

                        if (attr.GroupID != gf.OID)
                            continue;

                        if (newTable.Rows.Count < groupIdx)
                            continue;

                        #region ������ϸ��
                        foreach (MapDtl dtl in dtls)
                        {
                            if (dtl.IsUse)
                                continue;

                            if (dtl.RowIdx != groupIdx - 3)
                                continue;

                            if (gf.OID != dtl.GroupID)
                                continue;

                            GEDtls dtlsDB = new GEDtls(dtl.No);
                            QueryObject qo = new QueryObject(dtlsDB);
                            switch (dtl.DtlOpenType)
                            {
                                case DtlOpenType.ForEmp:
                                    qo.AddWhere(GEDtlAttr.RefPK, wk.OID);
                                    break;
                                case DtlOpenType.ForWorkID:
                                    qo.AddWhere(GEDtlAttr.RefPK, wk.OID);
                                    break;
                                case DtlOpenType.ForFID:
                                    qo.AddWhere(GEDtlAttr.FID, wk.OID);
                                    break;
                            }
                            qo.DoQuery();

                            newTable.Rows[groupIdx].SetHeight(100f, Word.WdRowHeightRule.wdRowHeightAtLeast);
                            newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 4));

                            Attrs dtlAttrs = dtl.GenerMap().Attrs;
                            int colNum = 0;
                            foreach (Attr attrDtl in dtlAttrs)
                            {
                                if (attrDtl.UIVisible == false)
                                    continue;
                                colNum++;
                            }

                            newTable.Cell(groupIdx, 1).Select();
                            WordApp.Selection.Delete(ref Nothing, ref Nothing);
                            Word.Table newTableDtl = WordDoc.Tables.Add(WordApp.Selection.Range, dtlsDB.Count + 1, colNum,
                                ref Nothing, ref Nothing);

                            newTableDtl.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                            newTableDtl.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

                            int colIdx = 1;
                            foreach (Attr attrDtl in dtlAttrs)
                            {
                                if (attrDtl.UIVisible == false)
                                    continue;
                                newTableDtl.Cell(1, colIdx).Range.Text = attrDtl.Desc;
                                colIdx++;
                            }

                            int idxRow = 1;
                            foreach (GEDtl item in dtlsDB)
                            {
                                idxRow++;
                                int columIdx = 0;
                                foreach (Attr attrDtl in dtlAttrs)
                                {
                                    if (attrDtl.UIVisible == false)
                                        continue;
                                    columIdx++;

                                    if (attrDtl.IsFKorEnum)
                                        newTableDtl.Cell(idxRow, columIdx).Range.Text = item.GetValRefTextByKey(attrDtl.Key);
                                    else
                                    {
                                        if (attrDtl.MyDataType == DataType.AppMoney)
                                            newTableDtl.Cell(idxRow, columIdx).Range.Text = item.GetValMoneyByKey(attrDtl.Key).ToString("0.00");
                                        else
                                            newTableDtl.Cell(idxRow, columIdx).Range.Text = item.GetValStrByKey(attrDtl.Key);

                                        if (attrDtl.IsNum)
                                            newTableDtl.Cell(idxRow, columIdx).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                    }
                                }
                            }

                            groupIdx++;
                            isLeft = true;
                        }
                        #endregion ������ϸ��

                        if (attr.UIIsLine)
                        {
                            currColumnIndex = 0;
                            isLeft = true;
                            if (attr.IsBigDoc)
                            {
                                newTable.Rows[groupIdx].SetHeight(100f, Word.WdRowHeightRule.wdRowHeightAtLeast);
                                newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 4));
                                newTable.Cell(groupIdx, 1).Range.Text = attr.Name + ":\r\n" + wk.GetValStrByKey(attr.KeyOfEn);
                            }
                            else
                            {
                                newTable.Cell(groupIdx, 2).Merge(newTable.Cell(groupIdx, 4));
                                newTable.Cell(groupIdx, 1).Range.Text = attr.Name;
                                newTable.Cell(groupIdx, 2).Range.Text = wk.GetValStrByKey(attr.KeyOfEn);
                            }
                            groupIdx++;
                            continue;
                        }
                        else
                        {
                            if (attr.IsBigDoc)
                            {
                                if (currColumnIndex == 2)
                                {
                                    currColumnIndex = 0;
                                }

                                newTable.Rows[groupIdx].SetHeight(100f, Word.WdRowHeightRule.wdRowHeightAtLeast);
                                if (currColumnIndex == 0)
                                {
                                    newTable.Cell(groupIdx, 1).Merge(newTable.Cell(groupIdx, 2));
                                    newTable.Cell(groupIdx, 1).Range.Text = attr.Name + ":\r\n" + wk.GetValStrByKey(attr.KeyOfEn);
                                    currColumnIndex = 3;
                                    continue;
                                }
                                else if (currColumnIndex == 3)
                                {
                                    newTable.Cell(groupIdx, 2).Merge(newTable.Cell(groupIdx, 3));
                                    newTable.Cell(groupIdx, 2).Range.Text = attr.Name + ":\r\n" + wk.GetValStrByKey(attr.KeyOfEn);
                                    currColumnIndex = 0;
                                    groupIdx++;
                                    continue;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                string s = "";
                                if (attr.LGType == FieldTypeS.Normal)
                                {
                                    if (attr.MyDataType == DataType.AppMoney)
                                        s = wk.GetValDecimalByKey(attr.KeyOfEn).ToString("0.00");
                                    else
                                        s = wk.GetValStrByKey(attr.KeyOfEn);
                                }
                                else
                                {
                                    s = wk.GetValRefTextByKey(attr.KeyOfEn);
                                }

                                switch (currColumnIndex)
                                {
                                    case 0:
                                        newTable.Cell(groupIdx, 1).Range.Text = attr.Name;
                                        if (attr.IsSigan)
                                        {
                                            string path = BP.SystemConfig.PathOfDataUser + "\\Siganture\\" + s + ".jpg";
                                            if (System.IO.File.Exists(path))
                                            {
                                                System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                                                object LinkToFile = false;
                                                object SaveWithDocument = true;
                                                //object Anchor = WordDoc.Application.Selection.Range;
                                                object Anchor = newTable.Cell(groupIdx, 2).Range;

                                                WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(path, ref  LinkToFile,
                                                    ref  SaveWithDocument, ref  Anchor);
                                                //    WordDoc.Application.ActiveDocument.InlineShapes[1].Width = img.Width; // ͼƬ���
                                                //    WordDoc.Application.ActiveDocument.InlineShapes[1].Height = img.Height; // ͼƬ�߶�
                                            }
                                            else
                                            {
                                                newTable.Cell(groupIdx, 2).Range.Text = s;
                                            }
                                        }
                                        else
                                        {
                                            if (attr.IsNum)
                                            {
                                                newTable.Cell(groupIdx, 2).Range.Text = s;
                                                newTable.Cell(groupIdx, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                            }
                                            else
                                            {
                                                newTable.Cell(groupIdx, 2).Range.Text = s;
                                            }
                                        }
                                        currColumnIndex = 1;
                                        continue;
                                        break;
                                    case 1:
                                        newTable.Cell(groupIdx, 3).Range.Text = attr.Name;
                                        if (attr.IsSigan)
                                        {
                                            string path = BP.SystemConfig.PathOfDataUser + "\\Siganture\\" + s + ".jpg";
                                            if (System.IO.File.Exists(path))
                                            {
                                                System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                                                object LinkToFile = false;
                                                object SaveWithDocument = true;
                                                object Anchor = newTable.Cell(groupIdx, 4).Range;
                                                WordDoc.Application.ActiveDocument.InlineShapes.AddPicture(path, ref  LinkToFile,
                                                    ref  SaveWithDocument, ref  Anchor);
                                            }
                                            else
                                            {
                                                newTable.Cell(groupIdx, 4).Range.Text = s;
                                            }
                                        }
                                        else
                                        {
                                            if (attr.IsNum)
                                            {
                                                newTable.Cell(groupIdx, 4).Range.Text = s;
                                                newTable.Cell(groupIdx, 4).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                            }
                                            else
                                            {
                                                newTable.Cell(groupIdx, 4).Range.Text = s;
                                            }
                                        }
                                        currColumnIndex = 0;
                                        groupIdx++;
                                        continue;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }  //����ѭ��

                #region ���ҳ��
                WordApp.ActiveWindow.View.SeekView = Word.WdSeekView.wdSeekPrimaryFooter;
                WordApp.ActiveWindow.ActivePane.Selection.InsertAfter("ģ����ccflow�Զ����ɣ��Ͻ�ת�ء������̵���ϸ��������� http://doc.ccflow.cn�� �������̹���ϵͳ���µ�:18660153393 QQ:hiflow@qq.com");
                WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                #endregion

                // �ļ�����
                WordDoc.SaveAs(ref  filename, ref  Nothing, ref  Nothing, ref  Nothing,
                    ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing,
                    ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing, ref  Nothing,
                    ref  Nothing, ref  Nothing, ref  Nothing);

                WordDoc.Close(ref  Nothing, ref  Nothing, ref  Nothing);
                WordApp.Quit(ref  Nothing, ref  Nothing, ref  Nothing);
                try
                {
                    string docFile = filename.ToString();
                    string pdfFile = docFile.Replace(".doc", ".pdf");
                    Glo.Rtf2PDF(docFile, pdfFile);
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DebugWriteInfo("@����pdfʧ��." + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                // WordApp.Quit(ref  Nothing, ref  Nothing, ref  Nothing);
                WordDoc.Close(ref  Nothing, ref  Nothing, ref  Nothing);
                WordApp.Quit(ref  Nothing, ref  Nothing, ref  Nothing);
            }
        }
        public static string GenerHelp(string helpId)
        {
            switch (helpId)
            {
                case "Bill":
                    return "<a href=\"http://ccflow.cn\"  target=_blank><img src='../../Images/FileType/rm.gif' border=0/>" + BP.Sys.Language.GetValByUserLang("OperVideo", "����¼��") + "</a>";
                case "FAppSet":
                    return "<a href=\"http://ccflow.cn\"  target=_blank><img src='../../Images/FileType/rm.gif' border=0/>" + BP.Sys.Language.GetValByUserLang("OperVideo", "����¼��") + "</a>";
                default:
                    return "<a href=\"http://ccflow.cn\"  target=_blank><img src='../../Images/FileType/rm.gif' border=0/>" + BP.Sys.Language.GetValByUserLang("OperVideo","����¼��") + "</a>";
                    break;
            }
        }
        public static string NodeImagePath
        {
            get {
                return Glo.IntallPath + "\\Data\\Node\\";
            }
        }
        public static void ClearDBData()
        {
            string sql = "DELETE FROM WF_GenerWorkFlow WHERE fk_flow not in (select no from wf_flow )";
            BP.DA.DBAccess.RunSQL(sql);

            sql = "DELETE FROM WF_GenerWorkerlist WHERE fk_flow not in (select no from wf_flow )";
            BP.DA.DBAccess.RunSQL(sql);
        }
        public static string OEM_Flag = "CCS";
        public static string FlowFileBill
        {
            get { return Glo.IntallPath + "\\DataUser\\Bill\\"; }
        }
        private static string _IntallPath = null;
        public static string IntallPath
        {
            get
            {
                if (_IntallPath == null)
                {
                    _IntallPath = SystemConfig.PathOfWebApp;
                    //   throw new Exception("@��û�� _IntallPath ��ֵ.");
                }
                return _IntallPath;
            }
            set
            {
                _IntallPath = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public static string Language = "CH";
        public static bool IsQL
        {
            get
            {
                string s = BP.SystemConfig.AppSettings["IsQL"];
                if (s == null || s == "0")
                    return false;
                return true;
            }
        }
        /// <summary>
        /// �Ƿ���ʾ����ͼƬ��
        /// </summary>
        public static bool IsShowTitle
        {
            get
            {
                return BP.SystemConfig.GetValByKeyBoolen("IsShowTitle", true);
            }
        }

        public static string CurrPageID
        {
            get
            {
                try
                {
                    string url = System.Web.HttpContext.Current.Request.RawUrl;

                    int i = url.LastIndexOf("/") + 1;
                    int i2 = url.IndexOf(".aspx") - 6;
                    try
                    {
                        url = url.Substring(i);
                        return url.Substring(0, url.IndexOf(".aspx"));

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + url + " i=" + i + " i2=" + i2);
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception("��ȡ��ǰPageID����:"+ex.Message);
                }
            }
        }
    }
}
