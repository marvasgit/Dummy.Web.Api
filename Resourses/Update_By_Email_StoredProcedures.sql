USE [DummyTable]
GO
/****** Object:  StoredProcedure [dbo].[DummyPerson_Update_By_Email]    Script Date: 1/9/2019 12:44:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[DummyPerson_Update_By_Email] 
	-- Add the parameters for the stored procedure here
	@Email nvarchar(255) ,
	@UserName nvarchar(255) , 
	@FirstName nvarchar(255) ,
	@LastName nvarchar(255) , 
	@Gender int , 
	@Password nvarchar(255) 
AS
BEGIN TRY
BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Update Status to false 

UPDATE [dbo].[Dummy_Persons] 
SET
[LastName]=@LastName,
[FirstName]=@FirstName,
[Gender]=@Gender,
[Password] = @Password,
[UserName] = @UserName

WHERE [dbo].[Dummy_Persons].[Email] = @Email

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

