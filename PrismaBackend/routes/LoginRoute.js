const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
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
            if (userAccount.password == req.body.password) { //ha a megadott jelszó egyezik a meglévő felhsználónévhez társult jelszóval.
                //lementjük az adatbázisba a frissített fiókot.
                res.send('Info: Successful login!') //küldünk egy választ a kérőnek.
                console.log(await prisma.users.findUnique({
                    include: {
                        saves: true,
                    },
                    where: {
                        username: ""+req.body.username
                    }
                }))
            }
            else {
                res.send('Error: Invalid credentials!')//hibás adatokat adott meg a felhasználó, ezért visszaküldjük, hogy 'Error: Invalid credentials!'.
            }
        }

    })

}