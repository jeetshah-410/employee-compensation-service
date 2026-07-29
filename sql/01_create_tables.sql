IF OBJECT_ID('dbo.Employee', 'U') IS NOT NULL DROP TABLE dbo.Employee;
IF OBJECT_ID('dbo.Department', 'U') IS NOT NULL DROP TABLE dbo.Department;

CREATE TABLE dbo.Department (
	DepartmentId   INT          IDENTITY(1,1) PRIMARY KEY,
	DepartmentName VARCHAR(100) NOT NULL,
	Location       VARCHAR(100) NULL
);

CREATE TABLE dbo.Employee (
	EmployeeId   INT           IDENTITY(1,1) PRIMARY KEY,
	FirstName    VARCHAR(50)   NOT NULL,
	LastName     VARCHAR(50)   NOT NULL,
	DepartmentId INT           NOT NULL,
	Salary       DECIMAL(12,2) NOT NULL,
	Bonus        DECIMAL(12,2) NULL,
	HireDate     DATE          NOT NULL

	CONSTRAINT FK_Employee_Department
		FOREIGN KEY (DepartmentId) REFERENCES dbo.Department(DepartmentId)
		ON DELETE NO ACTION
);