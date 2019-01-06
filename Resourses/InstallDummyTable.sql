
SET NOCOUNT ON
GO

USE master
GO
if exists (select * from sysdatabases where name='DummyTable')
		drop database DummyTable
go

DECLARE @device_directory NVARCHAR(520)
SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

EXECUTE (N'CREATE DATABASE DummyTable
  ON PRIMARY (NAME = N''DummyTable'', FILENAME = N''' + @device_directory + N'dummytbl.mdf'')
  LOG ON (NAME = N''DummyTable_log'',  FILENAME = N''' + @device_directory + N'dummytbl.ldf'')')
go

if CAST(SERVERPROPERTY('ProductMajorVersion') AS INT)<12 
BEGIN
  exec sp_dboption 'DummyTable','trunc. log on chkpt.','true'
  exec sp_dboption 'DummyTable','select into/bulkcopy','true'
END
ELSE ALTER DATABASE [DummyTable] SET RECOVERY SIMPLE WITH NO_WAIT
GO

set quoted_identifier on
GO

/* Set DATEFORMAT so that the date strings are interpreted correctly regardless of
   the default DATEFORMAT on the server.
*/
SET DATEFORMAT mdy
GO
use "DummyTable"
go

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Dummy_Persons' AND xtype='U')
   CREATE TABLE Dummy_Persons (
    ID int IDENTITY(1,1) PRIMARY KEY,
    UserName varchar(255) NOT NULL,
    LastName varchar(255) NOT NULL,
    FirstName varchar(255),
    Gender varchar(10),
    Password varchar(255) NOT NULL,
    Email varchar(255) NOT NULL,
    DateCreated smalldatetime NOT NULL,
    Status Bit NOT NULL
);
GO

CREATE  UNIQUE INDEX IXU_Email ON Dummy_Persons (Email)
--
-- Dumping data for table user_details
--

INSERT INTO Dummy_Persons( username, FirstName, LastName, gender, password, status,Email,DateCreated) VALUES
('rogers63', 'david', 'john', 'Female', 'e6a33eee180b07e563d74fee8c2c66b8', 1,'rogers63@test.com','2019-01-05 12:35:29.123'),
( 'mike28', 'rogers', 'paul', 'Male', '2e7dc6b8a1598f4f75c3eaa47958ee2f', 1,'mike28@test.com','2019-01-05 12:35:29.123'),
( 'rivera92', 'david', 'john', 'Male', '1c3a8e03f448d211904161a6f5849b68', 1,'rivera92@test.com','2019-01-05 12:35:29.123'),
( 'ross95', 'maria', 'sanders', 'Male', '62f0a68a4179c5cdd997189760cbcf18', 1,'ross95@test.com','2019-01-05 12:35:29.123'),
( 'paul85', 'morris', 'miller', 'Female', '61bd060b07bddfecccea56a82b850ecf', 1,'paul85@test.com','2019-01-05 12:35:29.123'),
( 'smith34', 'daniel', 'michael', 'Female', '7055b3d9f5cb2829c26cd7e0e601cde5', 1,'smith34@test.com','2019-01-05 12:35:29.123'),
( 'james84', 'sanders', 'paul', 'Female', 'b7f72d6eb92b45458020748c8d1a3573', 1,'james84@test.com','2019-01-05 12:35:29.123'),
( 'daniel53', 'mark', 'mike', 'Male', '299cbf7171ad1b2967408ed200b4e26c', 1,'daniel53@test.com','2019-01-05 12:35:29.123'),
( 'brooks80', 'morgan', 'maria', 'Female', 'aa736a35dc15934d67c0a999dccff8f6', 1,'brooks80@test.com','2019-01-05 12:35:29.123'),
( 'morgan65', 'paul', 'miller', 'Female', 'a28dca31f5aa5792e1cefd1dfd098569', 1,'morgan65@test.com','2019-01-05 12:35:29.123');
