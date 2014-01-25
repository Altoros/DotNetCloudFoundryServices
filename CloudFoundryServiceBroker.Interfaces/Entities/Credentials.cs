using System.Runtime.Serialization;

namespace CloudFoundryServiceBroker.Interfaces.Entities
{
    /// <summary>
    /// Credentials object
    /// </summary>
    [DataContract]
    public class Credentials
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Credentials()
        {
            
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="token"></param>
        public Credentials(string token)
        {
            Token = token;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <param name="db"></param>
        public Credentials(string user, string pwd, string server, string port, string db)
        {
            ConnctionString = (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(server))?null:string.Format(ConnectionStringFormat, user, pwd, server, db, string.IsNullOrEmpty(port)?"":",",port);
            User = user;
            Password = pwd;
            Server = server;
            Database = db;
            Port = port;
        }
        /// <summary>
        /// Connection string format
        /// </summary>
        public const string ConnectionStringFormat = "mssql://{0}:{1}@{2}{4}{5}/{3}";
        /// <summary>
        /// Connection
        /// </summary>
        [DataMember(Order=0, IsRequired = false, Name = "connection")]
        public string ConnctionString { get; set; }
        /// <summary>
        /// User
        /// </summary>
        [DataMember(Order = 1, IsRequired = false, Name = "user")]
        public string User { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [DataMember(Order = 2, IsRequired = false, Name = "password")]
        public string Password { get; set; }
        /// <summary>
        /// Server
        /// </summary>
        [DataMember(Order = 3, IsRequired = false, Name = "server")]
        public string Server { get; set; }
        /// <summary>
        /// Port
        /// </summary>
        [DataMember(Order = 4, IsRequired = false, Name = "port")]
        public string Port { get; set; }
        /// <summary>
        /// Database
        /// </summary>
        [DataMember(Order = 5, IsRequired = false, Name = "database")]
        public string Database { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        [DataMember(Order = 6, IsRequired = false, Name = "token")]
        public string Token { get; set; }
    }


    /* var config = {
    user: '...',
    password: '...',
    server: 'localhost',
    database: '...'
}*/
}
