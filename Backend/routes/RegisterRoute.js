const mongoose = require('mongoose')
const Account = mongoose.model('account')
const save = mongoose.model('save')
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })

module.exports = app => {
    app.post("/api/register", urlencodedParser, async (req, res, next) => {  //post-ot használunk, mivel szeretnénk adatot kérni a szervertől a /authorize aloldalon.
        var userAccount = await Account.findOne({ username: req.body.username })
        if(userAccount == null)
       {
        var newAccount = new Account({
        email: req.body.email,
        username: req.body.username,
        password: req.body.password,
        lastAuthenticated: Date.now(),
        registeredAt: Date.now()
       })
       var newSave = new save({
        username: req.body.username,
        lastSaveDate: Date.now(),
        normalCurrency: 0,
        prestigeCurrency: 0,
        totalEarnings: 0,
        pbUnlocked: false,
        pbSoldAmount: 0,
        pbValue: 0,
        pbFrequency: 0,

        bxUnlocked: false,
        bxSoldAmount: 0,
        bxValue: 0,
        bxFrequency: 0,

        glUnlocked: false,
        glSoldAmount: 0,
        glValue: 0,
        glFrequency: 0,

        byUnlocked: false,
        bySoldAmount: 0,
        byValue: 0,
        byFrequency: 0,
       })
       await newSave.save()
       await newAccount.save()
       res.send('Account created!')
       return
        }
        else
        {
            res.send('Error: There is an account whit this username!')
            return
        }
        
    })

}