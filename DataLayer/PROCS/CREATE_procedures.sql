USE CustomerData;
GO

CREATE OR ALTER PROCEDURE EditCustomer
(
	@id int,
	@first varchar(50),
	@last varchar(50)
)
AS
BEGIN
	UPDATE Customers
	SET Firstname = @first, Lastname = @last
	WHERE CustomerID = @id
END
GO

CREATE OR ALTER PROCEDURE AddCustomer
(
	@first varchar(50),
	@last varchar(50)
)
AS
BEGIN
	INSERT INTO Customers (Lastname, Firstname)
	VALUES (@last, @first);
END
GO