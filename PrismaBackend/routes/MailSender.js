const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
var nodemailer = require('nodemailer')
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })
const ShortUniqueId = require('short-unique-id')
const uuidv4 = new ShortUniqueId({ length: 10 })
//ez a sor felett csak importok és változók/konstansok találhatóak.

module.exports = app => {
    app.post("/api/mail", urlencodedParser, async (req, res) => {  //postot használunk mivel a kérés küldésekor a bodyban szeretnénk küldeni az adatokat.
        var userAccount = await prisma.users.findFirst({ where: { email: req.body.email } })// keresünk egy accountot a bodyban kapott email cím alapján.
        if (userAccount == null) { //megnézzük, hogy van e ilyen account.
            res.send("no") //amennyiben nincs akkor is elküldünk egy kérést, nem küldünk olyan válasuzt, hogy van ilyen email a rendszerben vagy nincs.
            return
        }
        else {//ha van ilyen account akkor az ez alatti rész fog lefutni.
            var uuid = uuidv4() //generálunk egy uuid-t
            await prisma.users.update({
                where:
                {
                    email: req.body.email
                },
                data: {
                    password: uuid
                }
            }) //beállítjuk a felhasználónak a jelszavát erre a generált kódot.
            var email = nodemailer.createTransport({ //email küldés beállítása.
                service: "Gmail", //gmail az email fiók domainje.
                auth: {
                    user: process.env.EMAIL, //email cím ahonnan küldjük.
                    pass: process.env.PASS // alkalmazás jelszó.
                }
            });
            email.sendMail({ //email elküldése 
                from: "RaccoonRecycleInfo <kornelhajto2004@gmail.com>", //honnan küldjük az emailt.
                to: req.body.email, //a cím email cím az ami a kérés bodyjában érkezik, tehát beállítjuk annak.
                subject: "Forgotten password!", //email tárgya.
                text: "Your code: " + uuid, //email tartalma.
                html: '<p>In order change your password we sent you a code. If you want to change your password just press uuid in the game and type in your code we sent there. After that you just need to type in a new password and again your new passowrd. Thats it!<p><b>' + uuid + '</b>' //htm-ben küldöm az email tartalmát, mert könnyen formázható.
            });
            res.send("If there was an account with this address in our system we sent an email to the address.") //elküldjük a kérőnek, hogy 'If there was an account with this address in our system we sent an email to the address.'.

        }
    })
}