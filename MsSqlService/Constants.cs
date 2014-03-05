
namespace MsSqlService
{
    internal static class Constants
    {
        public static class AppKeys
        {
            public static string DashboardUrl = "DashboardUrl";
            public static string LogUrl = "LogUrl";
            public static string CloudFoundryConnectionString = "CloudFoundryDataContext";
        }

        public static class SqlStroredProcedures
        {
            public static string CreateUserForDatabase = "sp_createUserForDatabase";
            public static string DropUserFromDatabase = "sp_dropUserFromDatabase";
        }

        public static class ErrorMessageTemplates
        {
            public static string DatabaseAlreadyExists = "Database with name {0} already exists!";
            public static string UserAlreadyExists = "User with name {0} already exists!";
            public static string UserNotFound = "User with name {0} not found!";
            public static string DatabaseNotFound = "Database with name {0} not found!";
        }

        public static class ErrorMessages
        {
            public static string DatabaseNameCantBeNull = "Database name can't be null!";
            public static string UserNameCantBeNull = "User name can't be null!";
        }

        public static class SqlParameters
        {
            public static string UserLogin = "@UserLogin";
            public static string UserPassword = "@UserPassword";
            public static string DbName = "@DbName";
            public static string Limit = "@Limit";
            public static string Result = "@Result";
        }

        public static class CloudFoundry
        {
            public static string CredentialsTemplate = "mssql://{0}:{1}@{2}/{3}";
        }

        public static class SqlTemplates
        {
            public static string CreateDatabase = @"DECLARE @res INT;
                                              IF DB_ID('{0}') IS NULL 
                                              BEGIN
                                                CREATE DATABASE [{0}];
                                                SET @res = 0;
                                              END
                                              ELSE SET @res = -1
                                              
                                              SELECT @res";
            public static string CreateLimitedDatabase = @"DECLARE @res INT;
                                              IF DB_ID('{0}') IS NULL 
                                              BEGIN
                                                CREATE DATABASE [{0}]  ON PRIMARY (MAXSIZE = {2}MB, FILENAME='{1}{0}.mdf', Name='{0}');
                                                SET @res = 0;
                                              END
                                              ELSE SET @res = -1
                                              
                                              SELECT @res";

            public static string DropDatabase = @"DECLARE @res INT;
                                            IF DB_ID('{0}') IS NOT NULL 
                                            BEGIN
                                              DROP DATABASE [{0}] 
                                              SET @res = 0
                                            END
                                            ELSE SET @res = -1
                                            
                                            SELECT @res";
        }


    }
}
