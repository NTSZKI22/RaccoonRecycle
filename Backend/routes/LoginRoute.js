const mongoose = require('mongoose')
const Account = mongoose.model('account')
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })
//ez a sor felett csak importok találhatóak.

module.exports = app => {
    app.post("/api/login", urlencodedParser, async (req, res) => {  //postot használunk mivel a kérés küldésekor a bodyban szeretnénk küldeni az adatokat.
        var userAccount = await Account.findOne({ username: req.body.username })// keresünk egy accountot a bodyban kapott username alapján.
        if (userAccount == null) { //megnézzük, hogy van e ilyen account.
            res.send('Error: Invalid credentials!') //amennyiben nincs akkor visszaküldjük, hogy 'Error: Invalid credentials!'.
            return
        }
        else {
            if (userAccount.password == req.body.password) { //ha a megadott jelszó egyezik a meglévő felhsználónévhez társult jelszóval.
                userAccount.lastAuthenticated = Date.now()//frissítjük az utolsó autentikáció időpontját.
                await userAccount.save() //lementjük az adatbázisba a frissített fiókot.
                res.send('Info: Successful login!') //küldünk egy választ a kérőnek.
            }
            else {
                res.send('Error: Invalid credentials!')//hibás adatokat adott meg a felhasználó, ezért visszaküldjük, hogy 'Error: Invalid credentials!'.
            }
        }

    })

}