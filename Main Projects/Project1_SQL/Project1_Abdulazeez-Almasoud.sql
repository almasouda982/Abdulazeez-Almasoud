---1. Table Creation 


--- creating all the tables with the FK & PK connections



/*


1- Employees

EmployeeID` (int, Primary Key, -
)FirstName` (nvarchar(50) Not Null` -
)LastName` (nvarchar(50) Not Null` -
)DepartmentID` (int, Foreign Key` -
)Salary` (decimal(10,2) Check Salary >= 0` -




*/
create table Employees(

EmployeeID int,
FirstName varchar(50) not null ,
LastName varchar(50) not null ,
ManagerID int,

--- alter later
DepartmentID int,

Salary decimal(10,2),


constraint Employees_EmployeeID_PK PRIMARY KEY(EmployeeID),
constraint Employees_Salary_CK check(Salary >= 0),


);


select *
from Employees



/*


2- LeaveRequests




)LeaveID` (int, Primary Key, Identity` -
)EmployeeID` (int, Foreign Key` -
)StartDate` (date Not Null` -
)EndDate` (date Not Null` -
))Reason` (nvarchar(100` -
))'Status` (nvarchar(20) Check Status IN ('Pending', 'Approved', 'Rejected` - 


*/

create table LeaveRequests(

LeaveID  int,

---alter later
EmployeeID int,

StartDate date not null,
EndDate date not null,
Reason varchar(100),
Status varchar(20),


constraint LeaveRequest_LeaveID_PK PRIMARY KEY(LeaveID),
constraint LeaveRequest_Status_CK check(status in('Pending', 'Approved','Rejected')),




);


select *
from LeaveRequests




/*

3- Departments

)DepartmentID` (int, Primary Key, Identity` -
)DepartmentName` (nvarchar(50) Not Null Unique` -

*/

create table Departments(

DepartmentID int,
DepartmentName varchar(50) not null,



constraint Departments_DepartmentID_PK PRIMARY KEY(DepartmentID),
constraint Departments_DepartmentName_UQ UNIQUE(DepartmentName)


);

select *
from Departments


/*

4- TrainingCourses

)CourseID` (int, Primary Key, Identity` -
)CourseName` (nvarchar(100) Not Null` -
)StartDate` (date Not Null` -
)EndDate` (date Not Null` -
)InstructorID` (int, Foreign Key` - 


*/

create table TrainingCourses(


CourseID int,
CourseName varchar(100) not null,
StartDate date not null,
EndDate date not null,

--- alter later
InstructorID int,

constraint TrainingCourses_CoursesID_PK	PRIMARY KEY(CourseID),

);


/*

5- Instructors

)InstructorID` (int, Primary Key, Identity` -
)FirstName` (nvarchar(50) Not Null` -
)LastName` (nvarchar(50) Not Null` - 

*/

create table Instructors(

InstructorID int,
FirstName varchar(50) not null,
LastName varchar(50) not null,

constraint Instructors_InstructorID_PK PRIMARY KEY(InstructorID)

);

select *
from Instructors

------------------------------------------------------------------------------------
/*

FK Insertion via ALTER

Employees.departmentID ---> departments.DepartmentID

Employees.ManagerID ---> Employees.EmployeeID

LeaveRequests.EmployeeID ---> Employees.EmployeesID

Training.InstructorID ---> Instructors.InstructorID






*/

--- employees.departmentID ---> departments.DepartmentID

ALTER TABLE Employees
add constraint Employees_DepartmentID_FK FOREIGN KEY(DepartmentID) REFERENCES Departments(DepartmentID)

--- Employees.ManagerID ---> Employees.EmployeeID

AlTER TABLE Employees
add constraint Employees_ManagerID_FK FOREIGN KEY(ManagerID) REFERENCES Employees(EmployeeID)


--- LeaveRequests.EmployeeID ---> Employees.Employees.ID

ALTER TABLE LeaveRequests
add constraint LeaveRequests_EmployeeID_FK FOREIGN KEY(EmployeeID) REFERENCES Employees(EmployeeID)

--- TrainingCourses.InstructorID ---> Instructors.InstructorID

ALTER TABLE TrainingCourses
add constraint TrainingCourses_InstructorID_FK FOREIGN KEY(InstructorID) REFERENCES Instructors(InstructorID)






select *
from Employees

select *
from Departments

select *
from TrainingCourses

select *
from Instructors

select *
from LeaveRequests


------------------------------------------------------------------------------

--- 2. Values Insertion


--- Insert in order to avoid FK/PK conflicts 


/*

1- in Departments (first)

1 HR 1
2 Finance 2
3 IT 3
4 Marketing 4
5 Operations 5
6 Sales 6
7 Customer Service 7
8 Operations 8 ??????
9 Research and Development 9
10 Finance 10 ????
*/



insert into Departments
values ('1', 'HR'), 
	   ('2', 'Finance'),
	   ('3', 'IT'),
	   ('4', 'Marketing'),
	   ('5', 'Operations'),
	   ('6', 'Sales'),
	   ('7', 'Customer Service'),
	   --('8', 'Operations'), UNIQUE KEY CONFLICT 
	   ('9', 'Research and Development');
	   --('10', 'Finance'); UNIQUE KEY CONFLICT

select *
from Departments

/*

2- In Employees (Second)

1 John Doe 1 50000.00
2 Jane Smith 2 60000.00
3 Ahmad Ali 3 55000.00
4 Sara Khan 2 52000.00
5 Mohamed Hassan 1 48000.00
6 Hassan Ali 1 54000.00
7 Fatima Khaled 2 62000.00
8 Amr Mohamed 3 56000.00
9 Sara Ahmed 4 51000.00
10 Ahmed Youssef 1 59000.00
*/



INSERT INTO Employees
values('1', 'John', 'Doe','1', '1', '50000.00'),
	  ('2', 'Jane', 'Smith','2', '2', '60000.00'),
	  ('3', 'Ahmad', 'Ali','3', '3', '55000.00'),
	  ('4', 'Sara', 'Khan','4', '2', '52000.00'),
	  ('5', 'Mohamed', 'Hassan','5', '1','48000.00'),
	  ('6', 'Hassan', 'Ali','6', '1', '54000.00'),
	  ('7', 'Fatima', 'Khaled','7', '2', '62000.00'),
	  ('8', 'Amr', 'Mohamed','8', '3', '56000.00'),
	  ('9', 'Sara', 'Ahmed','9', '4', '51000.00'),
	  ('10', 'Ahmed', 'Youssef','10', '1', '59000.00');


select *
from Employees 


/*

3- in LeaveRequests (Third)


101 1 2023-10-10 2023-10-14 Annual Leave Approved
102 3 2023-11-05 2023-11-07 Conference Pending
103 2 2023-10-20 2023-10-21 Sick Leave Approved
104 4 2023-12-01 2023-12-03 Family Emergency Pending
105 5 2023-10-25 2023-10-26 Personal Approved

*/



INSERT INTO LeaveRequests
VALUES ('101', '1', '2023-10-10', '2023-10-14', 'Annual Leave', 'Approved'), 
       ('102', '3', '2023-11-05', '2023-11-07', 'Conference Pending', 'Pending'),
	   ('103', '2', '2023-10-20', '2023-10-21', 'Sick Leave', 'Approved'),
	   ('104', '4', '2023-12-01', '2023-12-03', 'Family Emergency', 'Pending'),
	   ('105', '5', '2023-10-25', '2023-10-26', 'Personal', 'Approved');



select *
from LeaveRequests

/*

4- in Instructors (Fourth)


1 Sarah Johnson
2 James Smith
3 Mary Davis
4 Ahmed Hassan
5 Fatima Ali
6 Yasmine Ali
7 Mohamed Hassan
8 Ahmed Youssef
9 Amr Mohamed
10 Sara Ahmed



*/


	   
INSERT INTO Instructors
VALUES ('1' ,'Sarah' ,'Johnson') ,
	   ('2', 'James', 'Smith'  ),
	   ('3' ,'Mary' ,'Davis'),
	   ('4' ,'Ahmed' ,'Hassan'),
	   ('5' ,'Fatima' ,'Ali'),
	   ('6' ,'Yasmine' ,'Ali'),
	   ('7' ,'Mohamed' ,'Hassan'),
	   ('8' ,'Ahmed' ,'Youssef'),
	   ('9' ,'Amr' ,'Mohamed'),
	   ('10' ,'Sarah' ,'Ahmed');


select *
from Instructors

/*
5- in TrainingCourses (Fifth)


201 Leadership Skills 2023-11-05 2023-11-07 1
202 Excel Advanced 2023-11-12 2023-11-14 2
203 Time Management 2023-11-20 2023-11-20 3
204 Communication Tips 2023-12-02 2023-12-04 4
205 Conflict Resolution 2023-12-10 2023-12-11 5
206 Negotiation Skills 2023-12-20 2023-12-22 6
207 Financial Planning 2024-01-05 2024-01-07 7
208 Leadership Workshop 2024-01-10 2024-01-12 8
209 Effective Communication 2024-01-15 2024-01-17 9
210 Time Management 2024-01-20 2024-01-22 10


*/



INSERT INTO TrainingCourses
VALUES ('201', 'Leadership Skills', '2023-11-05', '2023-11-07', '1'),
       ('202', 'Excel Advanced', '2023-11-12', '2023-11-14', '2'),
       ('203', 'Time Management', '2023-11-20', '2023-11-21', '3'),
	   ('204', 'Communication Tips', '2023-12-02', '2023-12-04', '4'),
	   ('205', 'Conflict Resolution', '2023-12-10', '2023-12-11', '5'),
	   ('206', 'Negotiation Skills', '2023-12-20', '2023-12-22', '6'),
	   ('207', 'Financial Planning', '2024-01-05', '2024-01-07', '7'),
	   ('208', 'Leadership Workshop', '2024-01-10', '2024-01-12', '8'),
	   ('209', 'Effective Communication', '2024-01-15', '2024-01-17', '9'),
	   ('210', 'Time Management', '2024-01-20', '2024-01-22', '10');


select *
from TrainingCourses





---- Display all tables

select *
from Departments

select *
from Employees 

select *
from LeaveRequests

select *
from Instructors

select *
from TrainingCourses




--------------------------------------------------------

---3. EditDataQuery






--- 1. employee num 5 salary update to be 52000.00


update Employees 
set Salary = 52000
where EmployeeID = 5


--- 2.  update department name from HR to Human Resourses


update Departments
set DepartmentName = 'Human Resourses'
where DepartmentID in(1)


--- 3. update managerid 3 to be 10


update Employees
set ManagerID = 10
where DepartmentID in(3)



--- 4. update starting date for Training course to be 12-11-2023 for 202

update TrainingCourses
set StartDate = '2023-11-13'
where CourseID = 202



--- 5. delete employee number 8

delete from Employees
where EmployeeID = 8


--- 6. delete department number 7


delete from Departments
where DepartmentID = 7


--- 7. delete Training course number 205

delete from TrainingCourses
where CourseID = 205


/*


select *
from Departments

select *
from Employees 

select *
from LeaveRequests

select *
from Instructors

select *
from TrainingCourses

*/


-----------------------------------------------

---4. Simple Data Querys (From One Table)




--1. retrieve employee name and salary

select concat(FirstName, ' ', LastName) "Full Name", Salary
from Employees



---2. count their average salary 

select avg(Salary) "Employees Average Salary"
from Employees 

---1. get employees name and salary

select concat(FirstName, ' ', LastName) "Full Name", Salary
from Employees


 -- 2. return department names 

select DepartmentName as "Department Names"
from Departments

-- 3. return course names and their start and end dates 

select CourseName "Course Name", StartDate "Start Date", EndDate "End Date"
from TrainingCourses












/*


select *
from Departments

select *
from Employees 

select *
from LeaveRequests

select *
from Instructors


select *
from TrainingCourses

*/





---------------------------------------------------------------------
---5. Complex Data Querys (From Multiple Tables)




--- 1. get the number of employees in every department with their average salary (check)
 



select DepartmentName "Department Name", count(e.EmployeeID) "Number of employees", avg(Salary) "Average Salary"
from  Employees e, Departments d
where e.DepartmentID = d.DepartmentID
group by DepartmentName




--- 2. get the name of the instructors and the courses they give (check)


select concat(FirstName, ' ', LastName) "Instructor's Name", CourseName as "Course Name"
from TrainingCourses t right join Instructors i
on t.InstructorID = i.InstructorID 



--- 3. get the names of the employees and departmlents they work in with counting the average salary of each department  (DO NOT SOLVE)


--- 4. get the number of leaves approved or denied for every employee (check)

select concat(FirstName, ' ', LastName) as "Employee name", count(LeaveID) as "Number of Leaves Approved or Rejected"
from LeaveRequests l , Employees e
where l.EmployeeID = e.EmployeeID and status in ('Approved','Rejected')
group by FirstName, LastName




--- 5. return the number of employees in every deparltment

select  DepartmentName as "Department Name", count(EmployeeID) as "Number of Empolyees"
from Employees e, Departments d
where e.DepartmentID = d.DepartmentID
group by DepartmentName




--- 6. return the name of instructor and the courses they teach 

select concat(FirstName, ' ', LastName) "Instructor's name",  CourseName "Course Name"
from Instructors i, TrainingCourses t
where t.InstructorID = i.InstructorID
order by CourseName desc


---7. return the names of the employees and the departments they work in 

select concat(FirstName, ' ', LastName) "Employee name", DepartmentName "Department Name"
from Employees e, Departments d
where e.DepartmentID = d.DepartmentID



---8 return the names of employees who work in departments of more than 3 employees (check)


------------------------ Solution -------------------------------

select concat(FirstName, ' ', LastName) "Employee name" , DepartmentName "Department Name"
from Employees e, Departments d
where e.DepartmentID = d.DepartmentID and e.DepartmentID in('1')



------------------------- Analysis ------------------------------

--- Display departments with more than 3 emplyees
select DepartmentName "Department Name", count(*) "Number of Employees"
from Employees e, Departments d
where e.DepartmentID = d.DepartmentID
group by DepartmentName
having count(*) > 3

-- Find only Human Resourses employees and display them
select concat(FirstName, ' ', LastName) "Employee name" ---, DepartmentName "Department Name"
from Employees e, Departments d
where e.DepartmentID = d.DepartmentID and e.DepartmentID in('1')




---9. return the names of the employees which has the same department name (check)
 

------------------------ Solution -------------------------------

select concat(FirstName, ' ', LastName) "Employee name", DepartmentName "Department Name"
from Employees e, Departments d
where e.DepartmentID = d.DepartmentID and DepartmentName in('Human Resourses', 'Finance') 
order by DepartmentName desc

------------------------- Analysis ------------------------------


select concat(FirstName, ' ', LastName) "Employee name", DepartmentName "Department Name"
from Employees e, Departments d
where e.DepartmentID = d.DepartmentID 
order by DepartmentName desc

select concat(FirstName, ' ', LastName) "Employee name", DepartmentName "Department Name"
from Employees e, Departments d
where e.DepartmentID = d.DepartmentID and DepartmentName in('Human Resourses', 'Finance') 
order by DepartmentName desc








---10. return the course taught by sara johnson (check)

select CourseName "Training Course"
from TrainingCourses t, Instructors i
where t.InstructorID = i.InstructorID and (FirstName in('Sarah') and LastName in ('Johnson'))





-- 11. Return the employees with no managers assgined to them (no self manage)


select *
from Employees
where ManagerID = EmployeeID;



-- 12. return each department with the salary sum in each one

select DepartmentName "Department Name", sum(Salary) "Department Salary Total"
from Departments d, Employees e
where e.DepartmentID = d.DepartmentID 
group by DepartmentName



-- 13. return the department names and the employee with the highest salary in it 


------------------------ Solution -------------------------------

select DepartmentName "Department Name",concat(FirstName, ' ', LastName) "Employee name", Salary 
from Departments d, Employees e
where e.DepartmentID = d.DepartmentID and EmployeeID in('7','10','3','9')
order by Salary desc

------------------------- Analysis ------------------------------

--- finding employees, the department they work in and their salaries
select concat(FirstName, ' ', LastName) "Employee name", DepartmentName "Department Name", Salary 
from Departments d, Employees e
where e.DepartmentID = d.DepartmentID 
order by Salary desc

--- Filtering The Data

select concat(FirstName, ' ', LastName) "Employee name", DepartmentName "Department Name", Salary 
from Departments d, Employees e
where e.DepartmentID = d.DepartmentID and EmployeeID in('7','10','3','9')
order by Salary desc





-- 14. return the number training courses starting after 2023-12-01 

select count(*) "number of courses starting after (2023-12-01)"
from TrainingCourses
where StartDate > '2023-12-01'


-- 15. return the department names and the employee with the highest salary in it



select concat(FirstName, ' ', LastName) "Employee name", Salary 
from Departments d, Employees e
where e.DepartmentID = d.DepartmentID and EmployeeID in('7','10','3','9')
order by Salary desc

------------------------- Analysis ------------------------------

--- finding employees, the department they work in and their salaries
select concat(FirstName, ' ', LastName) "Employee name", DepartmentName "Department Name", Salary 
from Departments d, Employees e
where e.DepartmentID = d.DepartmentID 
order by Salary desc

--- Filtering The Data

select concat(FirstName, ' ', LastName) "Employee name", DepartmentName "Department Name", Salary 
from Departments d, Employees e
where e.DepartmentID = d.DepartmentID and EmployeeID in('7','10','3','9')
order by Salary desc

--- Finding the max salary 
select DepartmentName "Department Name", max(Salary) "Max Department Salary"
from Departments d, Employees e
where e.DepartmentID = d.DepartmentID
group by DepartmentName


