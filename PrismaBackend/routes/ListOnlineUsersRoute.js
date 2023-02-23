const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })
//ez a sor felett csak importok találhatóak.

module.exports = app => {
    app.get("/api/admin/listOnlinePlayers", urlencodedParser, async (req, res) => {
        const bearerHeader = req.headers['authorization']
        const bearerToken = bearerHeader.split(' ')[1]
        const verified = jwt.verify(bearerToken, jwtKey);
        if (verified) {
            const onlinePlayers = await prisma.users.findMany({
                where: {
                    isOnline: true
                },
                select: {
                    username: true,
                    saves: false
                }
            })

            //when someone register, we create a save data, so there is no need to check if there is a save with this username or not.
            //ha valaki regisztrál akkor csinálunk egy alap mentést, így nincs értelme ellenőrizni, hogy van e mentése az adott illetőnek. 
            //plusz ez a kérés akkor történik, ha valaki sikeresen bejelentkezik, így a felhasználónlv is valós, emiatt azt sem kell ellenőrizni.
            res.status(200).send(onlinePlayers)//elküldjük a játékosokat.
        }
        else{
            res.status(422).send("")
        }
    })

}