const mongoose = require('mongoose')
const Account = mongoose.model('account')
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })

module.exports = app => {
    app.post("/api/register", urlencodedParser, async (req, res, next) => {  //post-et használunk, mivel szeretnénk adatot kérni a szervertől a /authorize aloldalon.
        var userAccount = await Account.findOne({ username: req.body.aUsername })
        if(userAccount == null)
       {
        var newAccount = new Account({
        email: req.body.aEmail,
        username: req.body.aUsername,
        password: req.body.aPassword,
        lastAuthenticated: Date.now(),
        registeredAt: Date.now()
       })
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