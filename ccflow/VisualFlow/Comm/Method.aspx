<%@ Register TagPrefix="uc1" TagName="UCEn" Src="UC/UCEn.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BP.Web.Controls" Assembly="BP.Web.Controls" %>
<%@ Page language="c#" Inherits="BP.Web.Comm.UIRefMethod1" CodeFile="Method.aspx.cs" %>

<%@ Register Src="UC/ucsys.ascx" TagName="ucsys" TagPrefix="uc2" %>

<!DocType HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Esc ���رմ���.</title>
		<meta content="Microsoft FrontPage 5.0" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Table.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="JScript.js"></script>
		<script language="JavaScript" src="Menu.js"></script>
		<base target="_self" />
		<LINK href="Table.css" type="text/css" rel="stylesheet">
		<script language="javascript" for="document" event="onkeydown">
<!--
 if (window.event.srcElement.tagName="TEXTAREA") 
     return false;
  if(event.keyCode==13)
     event.keyCode=9;
-->
</script>
	</HEAD>
	<body  onkeypress=Esc() leftMargin=0 topMargin=0>
		<form id="Form1" method="post" runat="server">
            <FONT face="����">
				<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				    <TR>
				        <TD>
				        <asp:Panel ID="panConfirm" runat="server" HorizontalAlign="Center">
                            <p>��ȷ��Ҫִ�е�ǰ������</p>
                            <asp:Button ID="btnSubmit" runat="server" Text="ִ��" onclick="btnSubmit_Click" />&nbsp;&nbsp;
                            <input id="Button1" type="button" value="�ر�" onclick="javascript:window.close();" />
                        </asp:Panel>
                       
				        </TD>
				    </TR>
					<TR>
						<TD class=TD  bgcolor=InfoBackground>
                            <uc2:ucsys ID="Ucsys1" runat="server" />
                        </TD>
					</TR>
					<TR>
						<TD class=TD  bgcolor=InfoBackground>
                            <uc2:ucsys ID="Ucsys2" runat="server" />
                        </TD>
					</TR>
					<TR>
						<TD>
						    <uc1:UCEn id="UCEn1" runat="server"></uc1:UCEn>
						</TD>
					</TR>
					<TR>
						<TD class=TD bgcolor=InfoBackground>
                            <uc2:ucsys ID="UcMsg" runat="server" />
                        </TD>
					</TR>
				</TABLE>
			</FONT>      
            </ContentTemplate>
        </asp:UpdatePanel>			
		</form>
	</body>
</HTML>