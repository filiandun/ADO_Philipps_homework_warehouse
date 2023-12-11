--CREATE TABLE ProductTypes
--(
--	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
--	Name NVARCHAR(100) NOT NULL UNIQUE,
--);

--CREATE TABLE Suppliers
--(
--	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
--	Name NVARCHAR(100) NOT NULL UNIQUE,
--	TIN BIGINT NOT NULL UNIQUE
--);

CREATE TABLE Warehouse
(
	Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	ProductName NVARCHAR(100) NOT NULL,
	TypeId INT NOT NULL REFERENCES ProductTypes (Id),
	SupplierId INT NOT NULL REFERENCES Suppliers (Id),
	Quantity INT NOT NULL CHECK (Quantity > 0),
	Price MONEY NOT NULL CHECK (Price > 0),
	DateAdded DATE NOT NULL
);



--INSERT INTO ProductTypes (Name)
--VALUES
--  ('Electronics'),
--  ('Clothing'),
--  ('Cosmetics'),
--  ('Food'),
--  ('Furniture'),
--  ('Sporting Goods'),
--  ('Books'),
--  ('Cars');


--INSERT INTO Suppliers (Name, TIN)
--VALUES
--  ('Global Technologies', 232572222),
--  ('Express Fashion', 862906902),
--  ('Healthy Beauty', 189548915),
--  ('Superior Products', 554928535),
--  ('Furniture Master', 129523523);


INSERT INTO Warehouse (ProductName, TypeId, SupplierId, Quantity, Price, DateAdded)
VALUES
  ('iPhone 12', 1, 1, 10, 1000.00, '2021-01-01'),
  ('T-shirt', 2, 2, 50, 20.00, '2021-01-02'),
  ('Shampoo', 3, 3, 100, 5.00, '2021-01-03'),
  ('Milk', 4, 4, 200, 2.00, '2021-01-04'),
  ('Chair', 5, 5, 20, 50.00, '2021-01-05'),
  ('Tennis racket', 6, 1, 15, 80.00, '2021-01-06'),
  ('Book "War and Peace"', 7, 2, 30, 10.00, '2021-01-07'),
  ('Toyota Car', 8, 3, 1, 20000.00, '2021-01-08');


--CREATE VIEW WarehouseView AS
--SELECT
--	Warehouse.Id AS ИД, 
--	ProductName AS 'Продукт', 
--	ProductTypes.Name AS Тип, 
--	Suppliers.Name AS Производитель, 
--	Suppliers.TIN AS ИНН, 
--	Quantity AS Количество, 
--	Price AS Цена, 
--	DateAdded AS 'Дата поставки'
--FROM Warehouse
--JOIN ProductTypes ON ProductTypes.Id = Warehouse.TypeId
--JOIN Suppliers ON Suppliers.Id = Warehouse.SupplierId;
