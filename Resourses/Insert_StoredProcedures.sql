USE [DummyTable]
GO
-- =============================================
-- Author:		Marin Vassilev
-- Create date: 05.01.2019
-- Description:	StoredProcedure for inserting an Dummy person
-- =============================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE DummyPerson_Insert 
	-- Add the parameters for the stored procedure here
	@UserName nvarchar(255), 
	@FirstName nvarchar(255),
	@LastName nvarchar(255), 
	@Gender nvarchar(255), 
	@Password nvarchar(255), 
	@Email nvarchar(255), 
	@Status BIT, 
	@DateCreated nvarchar(255)
AS
BEGIN TRY
BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT [dbo].[Dummy_Persons]
(
		[UserName],
		[FirstName],
		[LastName],
		[Gender],
		[Password],
		[DateCreated],
		[Status],
		[Email]
)
VALUES
(
		@UserName,
		@FirstName,
		@LastName,
		@Gender,
		@Password,
		@DateCreated,
		@Status,
		@Email
)
SELECT SCOPE_IDENTITY()
COMMIT TRAN
END TRY

BEGIN CATCH
ROLLBACK TRAN

DECLARE @ErrorNumber_INT INT;
DECLARE @ErrorSeverity_INT INT;
DECLARE @ErrorProcedure_VC VARCHAR(200);
DECLARE @ErrorLine_INT INT;
DECLARE @ErrorMessage_NVC NVARCHAR(4000);

SELECT
		@ErrorMessage_NVC = ERROR_MESSAGE(),
		@ErrorSeverity_INT = ERROR_SEVERITY(),
		@ErrorNumber_INT = ERROR_NUMBER(),
		@ErrorProcedure_VC = ERROR_PROCEDURE(),
		@ErrorLine_INT = ERROR_LINE()

RAISERROR(@ErrorMessage_NVC,@ErrorSeverity_INT,1);

END CATCH

GO