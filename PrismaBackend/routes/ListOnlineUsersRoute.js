const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })
//ez a sor felett csak importok találhatóak.

module.exports = app => {
    app.get("/api/admin/listOnlinePlayers", urlencodedParser, async (req, res) => {  //postot használunk mivel a kérés küldésekor a bodyban szeretnénk küldeni az adatokat.
        const onlinePlayers = await prisma.users.findMany({
            where: {
                isOnline: true
            },
            select: {
                username: true
            }
        })
        //when someone register, we create a save data, so there is no need to check if there is a save with this username or not.
        //ha valaki regisztrál akkor csinálunk egy alap mentést, így nincs értelme ellenőrizni, hogy van e mentése az adott illetőnek. 
        //plusz ez a kérés akkor történik, ha valaki sikeresen bejelentkezik, így a felhasználónlv is valós, emiatt azt sem kell ellenőrizni.
        res.send(onlinePlayers)//elküldjük a mentést.
    })

}