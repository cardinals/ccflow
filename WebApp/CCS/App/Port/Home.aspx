﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="App_Port_Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>驰骋纳税评估软件</title>
</head>
<frameset rows='70,*' border='0' framespacing='0' frameborder='0' Id='Top' > 
					<frame name='banner' scrolling='no' noresize   src='Head.aspx' 
marginwidth='0' marginheight='0' frameborder='NO'>
					<frameset border='0' framespacing='0' cols='*' frameborder='0'> 
					<frameset Id='Bottom' cols='192,*'> 
					<frameset rows='30,101%' border='0' framespacing='0'>
					<frame src='BarOfLeftMsg.aspx' noresize scrolling='NO' frameborder='NO' marginwidth='0' 
marginheight='0' name='top1'>
					<frame src='LeftOutlook.aspx' marginwidth='1' marginheight='1' noresize=true  bordercolor=blue  style="border-right-style:solid;border-right-width:medium;border-right-color:#339966"  scrolling='no' 
frameborder='NO' name='left'>
					</frameset>
					<frameset rows='30,*' border='0' framespacing='0' frameborder='0'> 
					<frame name='top2'   src='BarOfTop.aspx' marginwidth='0' marginheight='0' 
frameborder='NO' scrolling='NO' noresize>
					<frame name='mainfrm' src='../Default.aspx' marginwidth='0' marginheight='0' scrolling='AUTO' 
frameborder='NO'>
					</frameset>
					</frameset>
					</frameset>
					</frameset>
</html>
