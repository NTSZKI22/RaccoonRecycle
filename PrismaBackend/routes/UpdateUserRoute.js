const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })
//ez a sor felett csak importok találhatóak.

module.exports = app => {
    app.post("/api/updateuser", urlencodedParser, async (req, res) => {
        const bearerHeader = req.headers['authorization']
        const bearerToken = bearerHeader.split(' ')[1]
        const verified = jwt.verify(bearerToken, jwtKey)  //postot használunk mivel a kérés küldésekor a bodyban szeretnénk küldeni az adatokat.
        if (verified) {
            var userAccount = await prisma.users.findFirst({ where: { username: verified.username } }) // keresünk egy accountot a bodyban kapott username alapján.
            //lementjük az adatbázisba a frissített fiókot.
            res.json({message: 'Info: Successful save!'}) //küldünk egy választ a kérőnek.
            await prisma.users.update({
                where: {
                    username: "" + req.body.username
                },
                data: {
                    isOnline: false
                }
            })
        }
        else{
            res.json({message: "Bad Token!"},498)
        }

    })

}