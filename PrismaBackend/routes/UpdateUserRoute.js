const { PrismaClient } = require("@prisma/client");
const prisma = new PrismaClient();
const jwt = require("jsonwebtoken");
const jwtKey = process.env.JWTKEY;
var bodyParser = require("body-parser");
var jsonParser = bodyParser.json();
var urlencodedParser = bodyParser.urlencoded({ extended: false });
//ez a sor felett csak importok találhatóak.

module.exports = (app) => {
  app.post("/api/updateuser", urlencodedParser, async (req, res) => {
    if (req.headers["authorization"]) {
      const bearerHeader = req.headers["authorization"];
      const bearerToken = bearerHeader.split(" ")[1];
      console.log(bearerToken)
      const verified = jwt.verify(bearerToken, jwtKey, //postot használunk mivel a kérés küldésekor a bodyban szeretnénk küldeni az adatokat.
      async (err, decoded) => {
        if (err) {
          return res.status(401).json({});
        } else {
            console.log(decoded)
          await prisma.users.update({
            where: {
              username: "" + decoded.username,
            },
            data: {
              isOnline: false,
            },
          });
          return res.status(200).json({ message: "Info: Successful save!" });
        }
      });
    } else {
      return res.status(401).json({ message: "Bad Token!" });
    }
  });
};
