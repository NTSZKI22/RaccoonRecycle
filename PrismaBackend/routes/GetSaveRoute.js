const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })
//ez a sor felett csak importok találhatóak.

module.exports = app => {
    app.post("/api/getsave", urlencodedParser, async (req, res) => {  //postot használunk mivel a kérés küldésekor a bodyban szeretnénk küldeni az adatokat.
        const userSave = await prisma.saves.findFirst({
            where:
            {
                Users:
                {
                    username: req.body.username
                }
            }
        }) // keresünk egy mentést a bodyban kapott username alapján.
        //when someone register, we create a save data, so there is no need to check if there is a save with this username or not.
        //ha valaki regisztrál akkor csinálunk egy alap mentést, így nincs értelme ellenőrizni, hogy van e mentése az adott illetőnek. 
        //plusz ez a kérés akkor történik, ha valaki sikeresen bejelentkezik, így a felhasználónlv is valós, emiatt azt sem kell ellenőrizni.
        res.send(userSave)//elküldjük a mentést.
    })

}