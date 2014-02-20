USE [master]
GO
/****** Object:  StoredProcedure [dbo].[sp_createUserForDatabase]    Script Date: 1/22/2014 4:40:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_createUserForDatabase]
    @UserLogin nvarchar(50), 
    @UserPassword nvarchar(50),
	@DbName nvarchar(50),
	@Result int output
AS 
begin
	DECLARE @sql nvarchar(4000);
	DECLARE @use nvarchar(1024);

	DECLARE @IsUserExist bit;
	DECLARE @IsLoginExists bit;

   SET NOCOUNT ON;
  
   SELECT @sql = 'IF NOT EXISTS (SELECT name FROM master.sys.server_principals WHERE name = ''' + @UserLogin +''') 
				  BEGIN  
					CREATE LOGIN [' + @UserLogin +'] WITH PASSWORD='''+ @UserPassword +''', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
					SET @tempOUT = 0
				  END
				  ELSE SET @tempOUT = 1'
   
   EXEC sp_executesql @sql, N'@tempOUT bit output', @IsLoginExists output;

   SELECT @use = replace('USE [$dbname$] ', '$dbname$', @DbName)

   SELECT @sql = @use + 'IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = ''' + @UserLogin + ''') 
				 BEGIN 
					CREATE USER ['+ @UserLogin +'] FOR LOGIN ['+ @UserLogin +'] WITH DEFAULT_SCHEMA = dbo
					EXEC sp_addrolemember N''db_owner'', ['+ @UserLogin +']
					SET @tempOUT = 0
				 END
				 ELSE SET @tempOUT = 1'

   EXEC sp_executesql @sql, N'@tempOUT bit output', @IsUserExist output;

   IF(@IsLoginExists & @IsUserExist = 1)
	   SET @Result = -1
   ELSE
	   SET @Result = 0
end