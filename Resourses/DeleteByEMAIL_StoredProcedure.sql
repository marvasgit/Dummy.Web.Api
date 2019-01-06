USE [DummyTable]
GO
-- =============================================
-- Author:		Marin Vassilev
-- Create date: 05.01.2019
-- Description:	StoredProcedure for Deleting an Dummy person
-- =============================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE DummyPerson_Delete_BY_Email 
	-- Add the parameters for the stored procedure here
	@Email nvarchar(255) = 0,
	@Success BIT OUTPUT
AS
BEGIN TRY
BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Update Status to false 
--	DELETE FROM [dbo].[DummyPerson]
--	
--	WHERE [dbo].[DummyPerson].[Email] = @Email
--	

UPDATE [dbo].[Dummy_Persons] 
SET

[Status]=0

WHERE [dbo].[Dummy_Persons].[Email] = @Email

Set @Success=1
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