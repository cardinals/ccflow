DELETE Sys_SFTable WHERE No='BP.CN.Citys';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.CN.Citys','����','FK_City','2','�й����м�����','','1');

DELETE Sys_SFTable WHERE No='BP.CN.PQs';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.CN.PQs','����','FK_DQ','2','���������������ϡ�����','','1');

DELETE Sys_SFTable WHERE No='BP.CN.SFs';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.CN.SFs','ʡ��','FK_SF','2','�й���ʡ�ݡ�','','1');

DELETE Sys_SFTable WHERE No='BP.Port.Depts';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.Port.Depts','����','FK_Dept','2','����','','1');

DELETE Sys_SFTable WHERE No='BP.Port.Emps';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.Port.Emps','��Ա','FK_Emp','2','ϵͳ�еĲ���Ա','','1');


DELETE Sys_SFTable WHERE No='BP.Port.Stations';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.Port.Stations','��λ','FK_Station','2','������λ','','1');

DELETE Sys_SFTable WHERE No='BP.Pub.Days';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.Pub.Days','��','FK_Day','0','1-31��','','1');


DELETE Sys_SFTable WHERE No='BP.Pub.YFs';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.Pub.YFs','��','FK_NY','0','1-12��','','1');

DELETE Sys_SFTable WHERE No='BP.Pub.NYs';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.Pub.NYs','����','FK_NY','0','������·�','','1');

DELETE Sys_SFTable WHERE No='BP.Pub.NDs';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.Pub.NDs','���','FK_ND','0','���','','1');

