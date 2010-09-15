<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FAQDtl.aspx.cs" Inherits="R2_PSV_R2_Res" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="BP.GE" Namespace="BP.GE.Ctrl" TagPrefix="cc1" %>
<%@ Register Src="~/UC/Pub.ascx" TagName="Pub" TagPrefix="uc3" %>
<head id="Head1" runat="server">
    <title>��Դ��ϸҳչʾ</title>
    
    <link href="/edu/port/reg3706/style/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/edu/port/reg3706/style/css/style_2.css" rel="stylesheet" type="text/css" />
    <link href="/edu/port/reg3706/style/css/tab_style.css" rel="stylesheet" type="text/css" />
    <link href="/edu/port/reg3706/style/css/index_style.css" rel="stylesheet" type="text/css" />
    <link href="../Comm/Table2.css" rel="stylesheet" type="text/css" />
    
</head>
<html>
<body>
    <form id="form1" runat="server">
    <div>
   
    <style type="text/css">
        .xsli
        {
            margin-right: 20px;
        }
    </style>

    <script type="text/javascript">
       
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>
<script language="javascript">
    function DownRes(id) {
        var url2 = '/edu/sharefile/DoDown.aspx?RefNo=' + id + '&DoType=DownRes';
        // alert(url2);
        //  val=window.showModalDialog( url2 , 'd' ,'dialogHeight: 550px; dialogWidth: 650px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no'); 
        val = window.showModelessDialog(url2, 'd', 'dialogHeight: 250px; dialogWidth: 450px;center: yes; help: no');
        return;
    }
</script>
    <div class="site_map">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��ǰλ�ã�<a href="/edu/">��������</a>
        - ��Դ��Ϣ</div>
    <div id="main_body" style="padding-top:200px">
        <%--<div class="cont_left_230">
            <div class="title_03" style="padding-left: 10px;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                �����Դ &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
            <div class="cont_body">
                <ul>
                    <asp:DataList ID="DTL_Res_Static" runat="server">
                        <ItemTemplate>
                            <li class="li_xss"><a href='PSV_R2_Res.aspx?RefOID=<%#Eval("MyPK")%>'>
                                <%#Eval("Title")%></a></li>
                        </ItemTemplate>
                    </asp:DataList>
                </ul>
            </div>
            <div class="title_03" id="D1" runat="server" style="padding-left: 10px;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                ������ &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
            <div class="cont_body" id="D2" runat="server">
                <ul>
                    <asp:DataList ID="DataList1" runat="server">
                        <ItemTemplate>
                            <li class="li_xss"><a href='PSV_R2_Res.aspx?RefOID=<%#Eval("RefOBJ")%>'>
                                <%#Eval("Title")%></a></li>
                        </ItemTemplate>
                    </asp:DataList>
                    </li>
                </ul>
            </div>
        </div>--%>
        <!--������-->
        <div class="cont_right_765">
            <div class="title_03" style="padding-left: 10px;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                ��ϸ��Ϣ &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
            <div class="cont_body">
                <div class="name">
                   
                    
                    <asp:Label ID="labTitle" runat="server" Text=""></asp:Label></div>
                <div class="info">
                    <style>
                        .info li
                        {
                            line-height: 23px;
                            border-bottom: 1px #ececec solid;
                        }
                    </style>
                    <ul>
                        <div id="Div1" runat="server">
                            <asp:Image ID="Img"  style="height: 100px; width: 100px;" runat="server" /></div>
                        <div id="Div2" runat="server">
                            <asp:Label ID="Label1" runat="server" Text="Label"><%=uurl %></asp:Label>
                        </div>
                    </ul>
                    <ul>
                        <li><span class="bolder">��Դ��ţ�</span><asp:Label ID="labOID" runat="server" Text=""></asp:Label></li>
                        <li><span class="bolder">��Դ���ͣ�</span><asp:Label ID="labBType" runat="server" Text=""></asp:Label></li>
                        <li><span class="bolder">��Դ��С��</span><asp:Label ID="labSize" runat="server" Text=""></asp:Label>MB</li>
                        <li><span class="bolder">��Դ��ʽ��</span><asp:Label ID="labType" runat="server" Text=""></asp:Label></li>
                        <li><span class="bolder">���ð汾��</span><asp:Label ID="labVer" runat="server" Text=""></asp:Label></li>
                        <li><span class="bolder">�ϴ��û���</span><asp:Label ID="labWorker" runat="server" Text=""></asp:Label></li>
                        <li><span class="bolder">�ϴ����ڣ�</span><asp:Label ID="labRDT" runat="server" Text=""></asp:Label></li>
                        <%--<li><span class="bolder">�޸����ڣ�</span><asp:Label ID="labEditRDT" runat="server" Text=""></asp:Label></li>--%>
                        <li><span class="bolder">���������</span><asp:Label ID="labBrowser" runat="server" Text=""></asp:Label></li>
                        <li><span class="bolder">���ش�����</span><asp:Label ID="labDown" runat="server" Text=""></asp:Label></li>
                        <li><span class="bolder">������֣�</span><asp:Label ID="labIntegral" runat="server" Text=""></asp:Label></li>
                        <%--<li><span class="bolder">��Դ������</span><asp:Label ID="labPJ" runat="server" Text=""></asp:Label></li>--%>
                    </ul>
                </div>
            </div>
            <div class="">
                <span style="font-size: 12px; font-weight: normal; float: left; margin-right: 10px;">
                    <cc1:GEFavorite ID="GEFavorite1"
                        runat="server">
                    </cc1:GEFavorite>
                </span>
             
                  <a href="javascript:DownRes('<%=GeRefOID %>')" >  <img src="../Port/Reg3706/Style/images/xz.gif" border='0' /></a>
            </div>
            <%--��Դ��������--%>
            <div id="DivPJ" runat="server">
                <cc1:GePJ ID="GePJ1" runat="server" IsShowPic="True" IsShowTitle="True">
                </cc1:GePJ>
            </div>
            <div class="option">
                <uc3:Pub ID="Pub1" runat="server" />
            </div>
            <%--��Դ�������ӽ���--%>
            <div id="DivComment" runat="server">
                <cc1:GeComment ID="GeComment1" runat="server" ShowType="ShowModaldialog" 
                    ISAllowAnony="false" PageSize="8">
                    <GloDBColumns>
                        <cc1:MyListItem ID="MyListItem1" runat="server" DataTextField="FK_EmpT" ItemStyle="xsli">
                        </cc1:MyListItem>
                        <cc1:MyListItem ID="MyListItem2" runat="server" DataTextField="RDT" ItemStyle="xsli">
                        </cc1:MyListItem>
                        <cc1:MyListItem ID="MyListItem3" runat="server" DataTextField="IP" ItemStyle="xsli">
                        </cc1:MyListItem>
                    </GloDBColumns>
                </cc1:GeComment>
            </div>
        </div>
    </div>
    </div> 
    </form>
</body>
</html>

