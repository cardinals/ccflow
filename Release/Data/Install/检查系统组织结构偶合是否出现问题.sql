
/*
  ���������������ż���Ƿ�������⣬���û�в�ѯ���κ����ݣ����п��������óɹ��ˡ�
*/
 
-- �����Ա�еĲ����Ƿ��Ӧ���ϡ�
SELECT * FROM Port_Emp WHERE FK_Dept NOT IN (SELECT No FROM PORT_DEPT)

--�����Ա���λ��Ӧ��ϵ���е���Ա���Ƿ�������.
SELECT * FROM Port_EmpStation WHERE FK_Emp NOT IN (SELECT No FROM PORT_Emp)

--�����Ա���λ��Ӧ��ϵ���еĸ�λ���Ƿ�������.
SELECT * FROM Port_EmpStation WHERE FK_Station NOT IN (SELECT No FROM Port_Station)


--�����Ա�벿�Ŷ�Ӧ��ϵ���е���Ա���Ƿ�������.
SELECT * FROM Port_EmpStation WHERE FK_Emp NOT IN (SELECT No FROM PORT_Emp)

--�����Ա�벿�Ŷ�Ӧ��ϵ���еĲ��ţ��Ƿ�������.
SELECT * FROM Port_EmpDept WHERE FK_Dept NOT IN (SELECT No FROM Port_Dept)
 

