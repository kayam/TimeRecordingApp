/* Überprüfen Sie das Skript gründlich, bevor Sie es außerhalb des Datenbank-Designer-Kontexts ausführen, um potenzielle Datenverluste zu vermeiden.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Activities
	(
	Id int NOT NULL IDENTITY (1, 1),
	Description nvarchar(2000) NOT NULL,
	Priority int DEFAULT 0
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Activities ADD CONSTRAINT
	PK_Activities PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Activities SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Activities', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Activities', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Activities', 'Object', 'CONTROL') as Contr_Per 








/* Überprüfen Sie das Skript gründlich, bevor Sie es außerhalb des Datenbank-Designer-Kontexts ausführen, um potenzielle Datenverluste zu vermeiden.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Activities SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Activities', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Activities', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Activities', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.TimeRecordings
	(
	Id int NOT NULL IDENTITY (1, 1),
	Date date NOT NULL,
	StartTime time(0) NULL,
	EndTime time(0) NULL,
	Note nvarchar(2000) NULL,
	ActivityId int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.TimeRecordings ADD CONSTRAINT
	PK_TimeRecordings PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TimeRecordings ADD CONSTRAINT
	FK_TimeRecordingss_Activities FOREIGN KEY
	(
	ActivityId
	) REFERENCES dbo.Activities
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TimeRecordings SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.TimeRecordings', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.TimeRecordings', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.TimeRecordings', 'Object', 'CONTROL') as Contr_Per 









USE [TimeRecording]
GO

INSERT INTO [dbo].[Activities]
           ([Description]
           ,[Priority])
     VALUES
           ('Programmierung', 10),
           ('Analysierung', 5),
           ('Aufräumen', 2)
GO



USE [TimeRecording]
GO

INSERT INTO [dbo].[TimeRecordings]
           ([Date]
           ,[StartTime]
           ,[EndTime]
           ,[Note]
           ,[ActivityId])
     VALUES
           ('2020-10-18'
           ,'12:15:04'
           ,'13:10:08'
           ,'SQL Script Erstellung'
           ,1)
GO

INSERT INTO [dbo].[TimeRecordings]
           ([Date]
           ,[StartTime]
           ,[EndTime]
           ,[Note]
           ,[ActivityId])
     VALUES
           ('2020-10-29'
           ,'14:00:01'
           ,'15:30:02'
           ,'C# Programmierung'
           ,2)
GO




select t.*, a.* 
from TimeRecordings t
inner join Activities a on t.activityId=a.id






INSERT INTO [dbo].[Activities]
           ([Description]
           ,[Priority])
     VALUES
           ('Programmierung', 10),
           ('Analysierung', 5),
           ('Aufräumen', 2)


