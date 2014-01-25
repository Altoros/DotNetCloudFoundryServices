var http = require('http');

var host = process.env.VCAP_APP_HOST || 'localhost';
var port = process.env.VCAP_APP_PORT || 1245;

var output = "Hello World listing all environment variables\n";

for(var key in process.env) {
    output += key + ':\n' + process.env[key] + '\n\n';
}

http.createServer(function (req, res) {
  res.writeHead(200, {'Content-Type': 'text/plain'});
  res.end(output);
}).listen(port, host);
console.log('Server running at ' + host + ':' + port);