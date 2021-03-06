USE [master]
GO
/****** Object:  StoredProcedure [dbo].[sp_dropUserFromDatabase]    Script Date: 1/21/2014 8:26:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dropUserFromDatabase]
    @UserLogin nvarchar(50), 
	@DbName nvarchar(50),
	@Result int OUTPUT
AS 
begin
	DECLARE @sql varchar(4000);
	DECLARE @use varchar(1024);

   SET NOCOUNT ON;

   IF EXISTS(SELECT * FROM master..syslogins WHERE name = @UserLogin)
   BEGIN
	   EXEC SP_DROPLOGIN @UserLogin
	   SELECT @Result = 0
   END
   ELSE 
	   SELECT @Result = -1
end


  
