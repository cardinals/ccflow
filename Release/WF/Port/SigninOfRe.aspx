<%@ page language="c#" inherits="BP.Web.SignInOfRe, App_Web_sd4z43pd" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SignInOfRe</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<!--<LINK href="../../Comm/Style.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../../Comm/JScript.js"></script>--><LINK href="Style/re_style.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		 function ReLoad()
         {
             var myParent, i ; 
            myParent = window.parent;
            if ( myParent == null ) 
               return;
            if ( myParent.frames == null )
               return;
               
            for ( i = 0; i< myParent.frames.length; i++ )
            {
               if ( window.parent.frames(i).name=='mainfrm')
                   continue;
               window.parent.frames(i).location.reload();
            }
          }
		</script>
	</HEAD>
	<body onunload="javascript:ReLoad();">
		<form id="Form1" method="post" runat="server">
			<FONT face="����"></FONT><FONT face="����"></FONT><FONT face="����"></FONT><FONT face="����">
			</FONT><FONT face="����"></FONT><FONT face="����"></FONT><FONT face="����"></FONT><FONT face="����">
			</FONT><FONT face="����"></FONT><FONT face="����"></FONT><FONT face="����"></FONT><FONT face="����">
			</FONT><FONT face="����"></FONT><FONT face="����"></FONT><FONT face="����"></FONT><FONT face="����">
			</FONT><FONT face="����"></FONT><FONT face="����"></FONT><FONT face="����"></FONT><FONT face="����">
			</FONT><FONT face="����"></FONT><FONT face="����"></FONT><FONT face="����"></FONT><FONT face="����">
			</FONT><FONT face="����"></FONT><FONT face="����"></FONT>
			<div id="LoginBox-title"></div>
			<table id="Table2" style="WIDTH: 500px; HEIGHT: 260px" height="152" cellSpacing="1" cellPadding="1"
				align="center" border="0" background="Images/re_LogOn.gif">
				<tr>
					<td>
						<table id="Table3" width="480" border="0" style="WIDTH: 480px; HEIGHT: 40px">
							<tr>
								<td height="40"><FONT face="����"></FONT></td>
							</tr>
						</table>
						<TABLE id="Table1" style="WIDTH: 198px; HEIGHT: 151px" cellSpacing="1" cellPadding="1"
							width="198" align="center" border="0">
							<TR> <!---->
								<TD align="right" width="54" height="33">�û���:</TD>
								<TD><LABEL><asp:textbox id="TB_No" onMouseOver="this.style.borderColor='#9ecc00'" onMouseOut="this.style.borderColor='#84a1bd'"
											runat="server" Width="128px" Height="23px"></asp:textbox></LABEL></TD>
							</TR>
							<TR>
								<TD align="right" height="33">��&nbsp;&nbsp;&nbsp;&nbsp;��:</TD>
								<TD><asp:textbox id="TB_Pass" onMouseOver="this.style.borderColor='#9ecc00'" onMouseOut="this.style.borderColor='#84a1bd'"
										runat="server" Width="128px" TextMode="Password" Height="23px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD height="36">&nbsp;</TD>
								<TD align="left">
									<TABLE id="Btn1-border" onmouseover="this.style.borderColor='#00FF00'" onmouseout="this.style.borderColor='#D2E1EE'"
										cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD><asp:button id="Btn1" runat="server" Text="&nbsp;&nbsp;ȷ&nbsp;&nbsp;&nbsp;��&nbsp;" onclick="Btn1_Click"></asp:button>
                                                 </TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD id="LoginBox-title2" align=center colSpan="2"><FONT face="����"></FONT></TD>
								
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
				</tr>
			</table>
<!--
<b>������Ա</b>

<HR>
ʡ����Ա:
sjz ʡ�ֳ�����<BR>

zgcsp ���ܴ�����<BR>
zgcsl ���ܴ�����<BR>

szcsp ˰��������<BR>
szcsl ˰��������<BR>

fgcsp ���洦����<BR>
fgcsl ���洦����<BR>

<hr>
��������Ա

02101 ���渻 (������Ա)<BR>
xjz  �����оֳ�����<BR>
zgsp ����˰����<BR>
zgsl ��������<BR>

szsp ˰������<BR>
szsl ˰������<BR>

dtzr ��������<BR>
<hr>
�Ĳ��ط�˰�������˰����
02141	���˾�<BR>
02147	��С��<BR>
02142	��ɽ (����)<BR>
02145	���ٲ�<BR>
02146	ׯ��<BR>
02143	����Ƽ<BR>
02144	�ַ�ׯ<BR>
02148	���һ�<BR>

<hr>
028888 ��Ϣ����Ա<BR>
admin  �����û�<BR>
-->
		</form>
	</body>
</HTML>
