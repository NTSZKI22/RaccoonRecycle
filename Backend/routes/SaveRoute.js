const mongoose = require('mongoose')
const save = mongoose.model('save')
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })

module.exports = app => {
    app.post("/api/save", urlencodedParser, async (req, res, next) => {  //post-ot használunk, mivel szeretnénk adatot kérni a szervertől a /authorize aloldalon.
        var userSave = await save.findOne({ username: req.body.aUsername })
        if(userSave == null)
       {
        var newSave = new save({
        username: req.body.username,
        lastSaveDate: Date.now(),
        normalCurrency: req.body.normalCurrency,
        prestigeCurrency: req.body.prestigeCurrency,
        totalEarnings: req.body.totalEarnings,
        pbUnlocked: req.body.pbUnlocked,
        pbSoldAmount: req.body.pbSoldAmount,
        pbValue: req.body.pbValue,
        pbFrequency: req.body.pbFrequency,

        bxUnlocked: req.body.bxUnlocked,
        bxSoldAmount: req.body.bxSoldAmount,
        bxValue: req.body.bxValue,
        bxFrequency: req.body.bxFrequency,

        glUnlocked: req.body.glUnlocked,
        glSoldAmount: req.body.glSoldAmount,
        glValue: req.body.glValue,
        glFrequency: req.body.glFrequency,

        byUnlocked: req.body.byUnlocked,
        bySoldAmount: req.body.bySoldAmount,
        byValue: req.body.byValue,
        byFrequency: req.body.byFrequency,
       })
       await newSave.save()
       res.send('Save data created!')
       return
        }
        else
        {
            res.send(500)
            return
        }
        
    })

}