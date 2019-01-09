USE [DummyTable]
GO
/****** Object:  StoredProcedure [dbo].[DummyPerson_Get_All_Users]    Script Date: 1/9/2019 11:22:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[DummyPerson_Get_Active_Users] 
	
AS
BEGIN TRY
BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Update Status to false 

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [ID]
      ,[UserName]
      ,[LastName]
      ,[FirstName]
      ,[Email]
      ,[DateCreated]
	   FROM [DummyTable].[dbo].[Dummy_Persons]
	   WHERE Dummy_Persons.Status != 0
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