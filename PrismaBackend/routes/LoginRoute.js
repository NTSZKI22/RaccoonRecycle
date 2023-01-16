const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
const bcrypt = require('bcrypt')
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })
//ez a sor felett csak importok találhatóak.

module.exports = app => {
    app.post("/api/login", urlencodedParser, async (req, res) => {  //postot használunk mivel a kérés küldésekor a bodyban szeretnénk küldeni az adatokat.
        var userAccount = await prisma.users.findFirst({ where: { username: req.body.username } }) // keresünk egy accountot a bodyban kapott username alapján.
        if (userAccount == null) { //megnézzük, hogy van e ilyen account.
            res.send('Error: Invalid credentials!') //amennyiben nincs akkor visszaküldjük, hogy 'Error: Invalid credentials!'.
            return
        }
        else {
            if (await bcrypt.compare(req.body.password, userAccount.password)) { //ha a megadott jelszó egyezik a meglévő felhsználónévhez társult jelszóval.
                //lementjük az adatbázisba a frissített fiókot.
                if (userAccount.isOnline == true) {
                    res.send('Info: You are logged in currently with your account on another device.')
                }
                else {
                    var data = {
                        time: Date(),
                        username: userAccount.username,
                        emailAddress: userAccount.email
                    }
                    const token = jwt.sign(data, jwtKey);
                    res.send(token) //küldünk egy választ a kérőnek.
                    await prisma.users.update({
                        where: {
                            username: "" + req.body.username
                        },
                        data: {
                            isOnline: true
                        }
                    })
                }
            }
            else {
                res.send('Error: Invalid credentials!')//hibás adatokat adott meg a felhasználó, ezért visszaküldjük, hogy 'Error: Invalid credentials!'.
            }
        }

    })

}