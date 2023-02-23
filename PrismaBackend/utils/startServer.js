const app = require('../server');

function startServer(portnumber) {
  const port = portnumber || 3000;

  app.listen(portnumber, () => {
    console.log(`API: API is running on port: 3000`);
  });
}

module.exports = startServer;