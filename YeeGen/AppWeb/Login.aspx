<%@ Register TagPrefix="uc1" TagName="UCTop" Src="UCComm/UCTop.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCEnd" Src="UCComm/UCEnd.ascx" %>
<%@ Page language="c#" Inherits="BP.YG.Login" CodeFile="Login.aspx.cs" CodeFileBaseClass="BP.YG.YGPage" %>
<%@ Register TagPrefix="uc1" TagName="UCPub" Src="UCComm/UCPub.ascx" %>
<!DocType HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>	<%=BP.YG.Global.BureauName%> - ��¼</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/Style/Glo.css" type="text/css" rel="stylesheet">
		<LINK href="/Style/Table.css" type="text/css" rel="stylesheet">
		<LINK href="/Style/Style<%=BP.YG.Global.Style%>.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="/Style/Comm.js"></script>
		
		
	</HEAD>
	<body leftMargin="0" topMargin="0"  >
				<FORM id="Form1" method="post" runat="server">
						<TABLE id="Table1"  cellSpacing="1" class=Table  cellPadding="1"   border="0"
							align="center">
							<TR>
								<TD colspan="2" class="TD" >
									<uc1:UCTop id="UCTop1" runat="server"></uc1:UCTop></TD>
							</TR>
							<TR  >
								<TD height='100%' valign="top" align=right  style="PADDING-RIGHT: 20px; PADDING-LEFT: 20px; MARGIN-LEFT: 20px; MARGIN-RIGHT: 20px">
										<TABLE   height='99%' class=TableGreen   cellSpacing="1" cellPadding="1"  border="0" align="center"  >
											<TR>
												<TD class="TitleGreen" >�������</TD>
											</TR>
											<TR>
												<TD class='BigDoc' valign=top >
													<P>&nbsp;&nbsp;&nbsp; ����վ���ڻ�Ա�����ƣ������Ǹ���������վ���׺Ͷ����ѵİ����Զ��ۼơ�</P>
													<P>&nbsp;&nbsp;&nbsp;&nbsp;�û��֣������Թ�����վ�ļ���Ʒ�����ڻ��ֹ�����ο�<A href="AboutCent.htm" target=_blank >���ֹ���</A>��&nbsp;</P>
													<P>&nbsp;&nbsp;&nbsp;&nbsp;<STRONG>����ϵͳ�����Ի�ȡ���·���</STRONG></p>
													<ul class=UL>
													<li>&nbsp;1�������Խ������Լ���blog��չʾ���ķ�ɡ� </li>
													<li>&nbsp;2�������Է������±���Լ��Ĺ۵㡢���⣬��ȡ���֡� </li>
													<li>&nbsp;3��������������ʱ����������������ܡ���˰���ʡ���������������⡣���Ĵ�һ����һ���ܺõĴ𰸡������Իش���˵������ȡ���֡�</li>
													<li>&nbsp;4�������Է������ӣ�չʾ�Լ��Ĺ۵㡣 </li>
													<li>&nbsp;5������������˰�񡢻��顢���񡢰칫����ҵ����ȵȷ�������ϡ� </li>
													<li>&nbsp;6���������˰����صĿƣ����������ˣ������Դ������Լ�����ҳ����������ӵ���Լ��ĵ�λ����վ������������</li>
													</ul>
													<P>&nbsp;&nbsp;&nbsp;<b> ף����һ���ܺõ��ջ�</b></P>
													 
												</TD>
											</TR>
											<TR>
												<TD></TD>
											</TR>
										</TABLE>
									 
									<P><STRONG> &nbsp;&nbsp;&nbsp;&nbsp;</STRONG></P>
								</TD>
								<TD width='50%'   valign="top"  align=left class="BigDoc">
								
								<table border=1 cellpadding='0' bgcolor=InfoBackground width='90%'  >
								<TR>
								<TD class=BigDoc >
									<BR>
									<P>�û�����
										<asp:TextBox id="TB_No" runat="server" Width="104px"></asp:TextBox></P>
									<P>��&nbsp;�룺
										<asp:TextBox id="TB_Pass" runat="server" Width="104px" TextMode="Password"></asp:TextBox></P>
									<P>
										<asp:CheckBox id="CheckBox1" runat="server" Text="��ס�ҵ��û�������." Checked="True"></asp:CheckBox></P>
									<P>
										<asp:Button id="Button1"   runat="server" Text=" �� ¼ " onclick="Button1_Click"></asp:Button><BR>
										<hr>
										<asp:Label id="Label1" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
										<ul class=UL>
										<li>
										<A href="RegUser.aspx?B=<%=BP.YG.Global.BureauNo %>&WhereGo=<%=BP.YG.Global.GoWhere%>" ><font color=green><b>ע�����û�(20�����ע��)</b></font></A></li>
										<li><A href="RequestMyPass.aspx">�һ�������</A></li>
										<li><A href="Home.aspx?B=<%=BP.YG.Global.BureauNo%>" >����</A></li>
										</ul>
									 </TD>
									 </TR>
									 
									 <TR>
									  <TD class=TitleGreen bgcolor=Window >
									  ע����������
									  </TD>
									 </TR>
									 
									 <TR>
									  <TD class=BigDoc bgcolor=Honeydew >
									    <ul class=UL>
									    <li>������http://yourname.space.caishui800.cn</li>
									    <li>���µĲ�˰��Ѷ</li>
									    <li>�������Ͽ�</li>
									    <li>�����ļ�</li>
									    <li>��̳</li>
									    <li>��ר�ҽ��������</li>
									    </ul>
									  </TD>
									 </TR>
									 </Table>
								</TD>
							</TR>
							
							<TR>
							<TD  class='TDEnd'  colspan=2>
							<uc1:UCEnd id="UCEnd1"  runat="server"></uc1:UCEnd>
							</TD>
							</TR>
							
						</TABLE>
				</FORM>
	</body>
</HTML>
