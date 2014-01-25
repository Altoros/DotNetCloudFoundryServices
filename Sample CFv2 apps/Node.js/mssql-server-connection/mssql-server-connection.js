var http = require('http');
var sql = require('mssql');

var getMssqlConfig = function() {
    var vcapServicesString = process.env.VCAP_SERVICES ||
        '{"mssql":[{"name":"mssql-local","label":"mssql-local","tags":[],"plan":"mssql-local-plan","credentials":{"mssql":"Not supported", "user": "username","password": "password", "server": "server_address","database": "database_name"}}]}';

    var vcapServices = JSON.parse(vcapServicesString);
    
    var config = vcapServices.mssql[0].credentials;
    return config;
};

var host = process.env.VCAP_APP_HOST || 'localhost';
var port = process.env.VCAP_APP_PORT || 1245;

http.createServer(function (req, res) {
    var sendError = function (err) {
        var errorInfo = err.toString();
        console.log(errorInfo);
        res.writeHead(500, {'Content-Type': 'text/plain'});
        res.end(errorInfo);
    };

    try
    {
        var output = "Ms Sql Connection - successful: \n";
        
        var mssqlConfig = getMssqlConfig();
        console.log("Using mssqlConfig: " + JSON.stringify(mssqlConfig));
        
        var connection = new sql.Connection(mssqlConfig);
        connection.connect(function(err) {
            // ... error checks
            if (err) {
                sendError(err);
                return;
            }

            // Query
            var request = new sql.Request(connection);
            request.query('select 1 as number', function(requestErr, recordset) {
                // ... error checks
                if (requestErr) {
                    sendError(requestErr);
                } else { 
                    output += recordset[0].number;
                    
                    res.writeHead(200, {'Content-Type': 'text/plain'});
                    res.end(output);
                }
                
                connection.close();
            });
        });
    }
    catch(ex)
    {
        sendError(ex);
    }
}).listen(port, host);
console.log('Server running at ' + host + ':' + port);