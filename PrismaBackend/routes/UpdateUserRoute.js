const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })
//ez a sor felett csak importok találhatóak.

module.exports = app => {
    app.post("/api/updateuser", urlencodedParser, async (req, res) => {  //postot használunk mivel a kérés küldésekor a bodyban szeretnénk küldeni az adatokat.
        var userAccount = await prisma.users.findFirst({ where: { username: req.body.username } }) // keresünk egy accountot a bodyban kapott username alapján.
        //lementjük az adatbázisba a frissített fiókot.
        res.send('Info: Successful save!') //küldünk egy választ a kérőnek.
        await prisma.users.update({
            where: {
                username: "" + req.body.username
            },
            data: {
                isOnline: false
            }
        })
    })

}