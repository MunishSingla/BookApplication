
USE [Book]
GO
/****** Object:  StoredProcedure [dbo].[Crud_Books]    Script Date: 2/21/2022 12:14:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Crud_Books]
@bookId int = 0,
@title varchar(50) = NULL,
@author varchar(500) = NULL,
@isbn varchar(20) = NULL,
@publicationDate datetime = NULL,
@mode int = 0
AS
BEGIN
IF(@mode = 1)
BEGIN
	INSERT INTO Books(Title, Authors, ISBN, PublicationDate)
	VALUES (@title, @author, @isbn,  @publicationDate)
END

ELSE IF(@mode = 2)
BEGIN
	UPDATE Books SET Title = @title,
	Authors = @author,
	ISBN = @isbn,
	PublicationDate = @publicationDate
	WHERE BookId = @bookId
END
ELSE IF(@mode = 3)
BEGIN
	DELETE FROM Books WHERE BookId = @bookId
END
ELSE IF(@mode = 4)
BEGIN
	SELECT BookId, Title, Authors, ISBN, PublicationDate FROM Books
END
ELSE IF(@mode = 5)
BEGIN
	SELECT BookId, Title, Authors, ISBN, PublicationDate FROM Books
	WHERE BookId = @bookId
END
ELSE IF(@mode = 6)
BEGIN
	SELECT BookId, Title, Authors, ISBN, PublicationDate FROM Books
	WHERE (Authors LIKE '%'+ @author +'%'
	OR Title LIKE '%'+ @title + '%'
	OR ISBN LIKE '%'+ @isbn + '%')
END
END
