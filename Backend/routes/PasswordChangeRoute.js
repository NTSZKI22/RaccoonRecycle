const mongoose = require('mongoose')
const Account = mongoose.model('account')
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })
const {
    v4: uuidv4
} = require('uuid')

module.exports = app => {
    app.post("/api/passwordchange", urlencodedParser, async (req, res, next) => {  //post-ot használunk, mivel szeretnénk adatot kérni a szervertől a /authorize aloldalon.
        var userAccount = await Account.findOne({ password: req.body.generatedCode })
        if (userAccount == null) {
            res.send(200);
            return
        }
        else {
            userAccount.password = req.body.newPassword
            userAccount.save()
            res.send(userAccount.username)
            return
        }
    })
}