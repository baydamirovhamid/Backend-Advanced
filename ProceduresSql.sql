use AdventureWorks2019


CREATE PROCEDURE usp_GetEmployeesByDepartment
    @Name NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [AdventureWorks2019].[HumanResources].[Department].DepartmentID,
        [AdventureWorks2019].[HumanResources].[Department].GroupName,
        [AdventureWorks2019].[HumanResources].[Department].ModifiedDate,
        [AdventureWorks2019].[HumanResources].[Department].Name
    FROM 
        [AdventureWorks2019].[HumanResources].[Department]
    
    WHERE 
        [AdventureWorks2019].[HumanResources].[Department].Name = @Name;
END;

EXEC usp_GetEmployeesByDepartment @Name = 'Engineering';






CREATE PROCEDURE usp_GetProductsByCategory
    @Name NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [AdventureWorks2019].[Production].[ProductCategory].ProductCategoryID,
        [AdventureWorks2019].[Production].[ProductCategory].Name,
        [AdventureWorks2019].[Production].[ProductCategory].rowguid,
        [AdventureWorks2019].[Production].[ProductCategory].ModifiedDate
    FROM 
        [AdventureWorks2019].[Production].[ProductCategory]

    WHERE 
        [AdventureWorks2019].[Production].[ProductCategory].Name = @Name;
END;

EXEC dbo.usp_GetProductsByCategory @Name = 'Bikes';






CREATE PROCEDURE usp_GetSalesOrdersByCustomer
    @CustomerID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [AdventureWorks2019].[Sales].[Customer].PersonID,
        [AdventureWorks2019].[Sales].[Customer].StoreID,
        [AdventureWorks2019].[Sales].[Customer].TerritoryID,
        [AdventureWorks2019].[Sales].[Customer].CustomerID,
        [AdventureWorks2019].[Sales].[Customer].AccountNumber
    FROM 
        [AdventureWorks2019].[Sales].[Customer]

    WHERE 
        [AdventureWorks2019].[Sales].[Customer].CustomerID = @CustomerID;
END;

EXEC usp_GetSalesOrdersByCustomer @CustomerID = 12345;