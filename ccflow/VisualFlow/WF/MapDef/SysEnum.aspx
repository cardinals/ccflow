﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysEnum.aspx.cs" Inherits="Comm_MapDef_NewEnum" %>
<%@ Register src="../UC/Pub.ascx" tagname="Pub" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title runat=server />
                <link href="../../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
	<script language="JavaScript" src="../../Comm/JScript.js" ></script>

    <script language=javascript>
    /* ESC Key Down  */
    function Esc()
    {
        if (event.keyCode == 27)
            window.close();
       return true;
    }
    </script>
    <base target=_self /> 
</head>
<body topmargin="0" leftmargin="0" onkeypress="Esc()"   onload="RSize()" >

 <form id="form1" runat="server">
<uc2:Pub ID="Pub1" runat="server" />
</form>

</body>
</html>


