const mongoose = require('mongoose')
const save = mongoose.model('save')
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })

module.exports = app => {
    app.post("/api/save", urlencodedParser, async (req, res) => {  //post-ot használunk, mivel szeretnénk adatot kérni a szervertől a /authorize aloldalon.
        var userSave = await save.findOne({ username: req.body.username })
            userSave.username = req.body.username
            userSave.lastSaveDate = Date.now()
                
            userSave.normalCurrency = req.body.normalCurrency
            userSave.prestigeCurrency = req.body.prestigeCurrency,
            userSave.totalEarnings = req.body.totalEarnings,

            userSave.pbUnlocked = req.body.pbUnlocked,
            userSave.pbSoldAmount = req.body.pbSoldAmount,
            userSave.pbValue =  req.body.pbValue,
            userSave.pbFrequency = req.body.pbFrequency,

            userSave.bxUnlocked = req.body.bxUnlocked,
            userSave.bxSoldAmount = req.body.bxSoldAmount,
            userSave.bxValue = req.body.bxValue,
            userSave.bxFrequency = req.body.bxFrequency,

            userSave.glUnlocked = req.body.glUnlocked,
            userSave.glSoldAmount = req.body.glSoldAmount,
            userSave.glValue = req.body.glValue,
            userSave.glFrequency = req.body.glFrequency,

            userSave.byUnlocked = req.body.byUnlocked,
            userSave.bySoldAmount = req.body.bySoldAmount,
            userSave.byValue = req.body.byValue,
            userSave.byFrequency = req.body.byFrequency,
            await userSave.save()
            res.send('The saving was successful!')
            return

    })

}