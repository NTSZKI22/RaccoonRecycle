const app = require('../server');

function startServer() {
  const port = process.env.PORT || 3000;

  app.listen(3000, () => {
    console.log(`API: API is running on port: 3000`);
  });
}

module.exports = startServer;