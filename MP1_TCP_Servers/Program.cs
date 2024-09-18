
using MP1_TCP_Servers;

// TCP Server - Accepts Commands as JSON
/*
JsonServer jsonServer = new JsonServer();
jsonServer.run();
*/


// TCP Server - Accepts Commands as String
StringServer stringServer = new StringServer();
stringServer.run();

