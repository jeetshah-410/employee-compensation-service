-- Seed Departments
INSERT INTO dbo.Department (DepartmentName, Location) VALUES
('Engineering', 'Bengaluru'),
('Sales',       'Mumbai'),
('HR',          'New Delhi'),
('Finance',     'Hyderabad');

-- Seed Employees
-- Note: Some employees have NULL bonus deliberately (Part B scenarios)
INSERT INTO dbo.Employee (FirstName, LastName, DepartmentId, Salary, Bonus, HireDate) VALUES
-- Engineering (DepartmentId = 1)
('Arjun',   'Sharma',  1, 1800000.00,  360000.00, '2020-03-15'),  -- has bonus
('Priya',   'Patel',   1, 1500000.00,  NULL,       '2019-06-01'),  -- no bonus
('Rahul',   'Mehta',   1, 2200000.00,  100000.00,  '2021-01-10'),  -- highest base salary (22 LPA)

-- Sales (DepartmentId = 2) -- total bonus exceeds avg salary (Part B.4)
('Vikram',  'Singh',   2, 1200000.00, 1200000.00, '2018-09-20'),   -- highest total comp (24 LPA)
('Sneha',   'Joshi',   2, 1100000.00,  900000.00, '2022-04-05'),   -- high sales commission

-- HR (DepartmentId = 3)
('Ananya',  'Gupta',   3,  900000.00,  NULL,       '2020-11-30'),  -- no bonus
('Rohan',   'Verma',   3,  850000.00,   75000.00,  '2021-07-15'),  -- has bonus
('Kavya',   'Nair',    3,  780000.00,  NULL,       '2023-02-01'),  -- no bonus

-- Finance (DepartmentId = 4)
('Aditya',  'Kumar',   4, 1600000.00,  240000.00, '2017-05-22'),   -- has bonus
('Pooja',   'Reddy',   4, 1400000.00,  NULL,       '2019-08-14');  -- no bonus