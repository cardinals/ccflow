/****** 对象:  View WF_EmpWorks    脚本日期: 03/12/2011 21:42:50 ******/
/*  WF_EmpWorks  */;
CREATE VIEW  WF_EmpWorks
AS
SELECT     A.WorkID, A.Rec AS Starter, A.FK_Flow, A.FlowName,
B.FK_Node, B.FK_NodeText AS NodeName, A.Title, A.RDT, B.RDT AS ADT, 
B.SDT, B.FK_Emp,B.FID ,A.FK_FlowSort
FROM  WF_GenerWorkFlow A, WF_GenerWorkerList B
WHERE     (B.IsEnable = 1) AND (B.IsPass = 0)
 AND A.WorkID = B.WorkID AND A.FK_Node = B.FK_Node ;

/*  WF_GenerEmpWorks  */;
CREATE VIEW WF_GenerEmpWorks
AS
SELECT FK_Flow, FK_Emp, COUNT(*) AS NUM  FROM WF_GenerWorkerList
 WHERE IsEnable=1 AND IsPass=0 GROUP BY FK_FLOW, FK_Emp ;

/*  WF_NodeExt  */;
CREATE VIEW WF_NodeExt
AS
SELECT NODEID AS NO , NAME AS NAME FROM WF_Node ;