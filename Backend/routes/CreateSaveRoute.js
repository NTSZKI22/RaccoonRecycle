const mongoose = require('mongoose')
const save = mongoose.model('save')
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })

module.exports = app => {
    app.post("/api/createsave", urlencodedParser, async (req, res, next) => {  //post-ot használunk, mivel szeretnénk adatot kérni a szervertől a /authorize aloldalon.
        var userSave = await save.findOne({ username: req.body.username })
        if (userSave == null) {
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
            res.send('Save data created!')
            return
        }
        else {
            res.send(userSave);
            return
        }

    })

}