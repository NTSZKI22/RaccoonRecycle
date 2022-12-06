const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })

module.exports = app => {
    app.post("/api/save", urlencodedParser, async (req, res) => {  //post-ot használunk, mivel szeretnénk adatot kérni a szervertől a /authorize aloldalon.
        await prisma.saves.update({
            where: {
                id: req.body.id,
            },
            data: {
                lastSaveDate: "" + Date.now(),

                normalCurrency: parseInt(req.body.normalCurrency),
                prestigeCurrency: parseInt(req.body.prestigeCurrency),
                totalEarnings: parseInt(req.body.totalEarnings),

                pbUnlocked: Boolean(req.body.pbUnlocked),
                pbSoldAmount: parseInt(req.body.pbSoldAmount),
                pbValue: parseInt(req.body.pbValue),
                pbFrequency: parseInt(req.body.pbFrequency),
                pbSpeed: parseInt(req.body.pbSpeed),

                bxUnlocked: Boolean(req.body.bxUnlocked),
                bxSoldAmount: parseInt(req.body.bxSoldAmount),
                bxValue: parseInt(req.body.bxValue),
                bxFrequency: parseInt(req.body.bxFrequency),
                bxSpeed: parseInt(req.body.bxSpeed),

                glUnlocked: Boolean(req.body.glUnlocked),
                glSoldAmount: parseInt(req.body.glSoldAmount),
                glValue: parseInt(req.body.glValue),
                glFrequency: parseInt(req.body.glFrequency),
                glSpeed: parseInt(req.body.glSpeed),

                byUnlocked: Boolean(req.body.byUnlocked),
                bySoldAmount: parseInt(req.body.bySoldAmount),
                byValue: parseInt(req.body.byValue),
                byFrequency: parseInt(req.body.byFrequency),
                bySpeed: parseInt(req.body.bySpeed)
            }
        })
        res.send('Info: The saving was successful!')
        return

    })

}