<%@ Register TagPrefix="uc1" TagName="UCPub" Src="UCComm/UCPub.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCLinkBar" Src="UCComm/UCLinkBar.ascx" %>
<%@ Page language="c#" Inherits="BP.Web.YG.HiTax.IRegUser" CodeFile="RegUser.aspx.cs" CodeFileBaseClass="BP.YG.YGPage" %>
<%@ Register TagPrefix="uc1" TagName="UCEnd" Src="UCComm/UCEnd.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCMy" Src="UCComm/UCMy.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCTop" Src="UCComm/UCTop.ascx" %>
<!DocType HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>���û�ע��</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/Style/Glo.css" type="text/css" rel="stylesheet">
		<LINK href="/Style/Table.css" type="text/css" rel="stylesheet">
		<LINK href="/Style/Style0.css" type=text/css rel=stylesheet >
		<script language="JavaScript" src="/Style/Comm.js"></script>
		
		
		<style type="text/css" id='sd'>
		.Note
		{
		  	border-top: inactivecaptiontext 1px solid;
	font: messagebox;
	color: olive;
		}
		</style>
	</HEAD>
	<body leftMargin="0" topMargin="0"  >
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" class="Table" align="center" width='80%' border=0   >
				 
				<TR>
					<TD colspan="2" class=TD>
						 <uc1:UCTop id="UCTop1" runat="server"></uc1:UCTop></TD>
				</TR>
				 
				<TR   valign="top"  >
					<TD width='30%'  class='BigDoc' valign=top  bgcolor=InfoBackground  >
						<P><B>ΪʲôҪ��Ϊcaishui800.cn ���û���</B></P>
						<P>&nbsp; ע��caishui800.cn �û��������������·���</P>
						<P>1,&nbsp; 10000 �������˰�񡢰칫�ļ����ɹ���������ء�</P>
						<P>2�������Իش���˵����⣬���������,��ȡ���֣��һ�����Ʒ��</P>
						<P>3�������Խ����Լ���blog��չʾ���ķ�ɡ�</P>
						<P>4�������Է������ģ�����Լ��Ĺ۵㡣</P>
						<P>5���������ϴ��Լ����ļ���������ˣ�ͬʱ�㻹���Է�������ϴ����ļ���</P>
						<P>6, ���������˰ר������潻����������ǲ�˰���֣������ҳ�Ϊ���ǲ�˰���ʵĳ�Ա��</P>
					</TD>
					<TD width='70%' class=BigDoc  >
						<uc1:UCPub id="UCPub1" runat="server"></uc1:UCPub></TD>
				</TR>
				<TR>
					<TD class="BigDoc" colspan="2" align=center >
						<uc1:UCEnd id="UCEnd1" runat="server"></uc1:UCEnd></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
