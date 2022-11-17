const mongoose = require('mongoose')
const Account = mongoose.model('account')
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })

module.exports = app => {
    app.post("/api/login", urlencodedParser, async (req, res, next) => {  //post-ot használunk, mivel szeretnénk adatot kérni a szervertől a /authorize aloldalon.
        var userAccount = await Account.findOne({ username: req.body.username })
        if (userAccount == null) {
            res.send('Error: Invalid credentials!')
            return
        }
        else {
            if (userAccount.password == req.body.password) { //ha a megadott jelszó egyezik a meglévő felhsználónévhez társult jelszóval.
                userAccount.lastAuthenticated = Date.now()//frissítjük az utolsó autentikáció időpontját.
                await userAccount.save() //lementjük az adatbázisba a frissített fiókot.

                res.send(userAccount) //visszaküldjük a kérőnek a frissített fiókot.
            }
            else{
                res.send('Error: Invalid credentials!')
            }
        }

    })

}