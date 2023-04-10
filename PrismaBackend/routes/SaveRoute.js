const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY
var bodyParser = require('body-parser')
var urlencodedParser = bodyParser.urlencoded({ extended: false })

module.exports = app => {
    app.post('/api/save', urlencodedParser, async (req, res) => {
       try {
        const bearerHeader = req.headers['authorization']
        const bearerToken = bearerHeader.split(' ')[1]
        const verified = jwt.verify(bearerToken, jwtKey)
        if (verified) {
            var pbu, bxu, glu, byu
            if(req.body.pbUnlocked != 0){pbu = true}
            if(req.body.bxUnlocked != 0){bxu = true}
            if(req.body.byUnlocked != 0){byu = true}
            if(req.body.glUnlocked != 0){glu = true}
            await prisma.saves.update({
                where: {
                    usersId: req.body.id
                },
                data: {
                    normalCurrency: parseInt(req.body.normalCurrency),
                    prestigeCurrency: parseInt(req.body.prestigeCurrency),
                    totalEarnings: parseInt(req.body.totalEarnings),

                    pbUnlocked: pbu,
                    pbSoldAmount: parseInt(req.body.pbSoldAmount),
                    pbValue: parseInt(req.body.pbValue),
                    pbFrequency: parseInt(req.body.pbFrequency),
                    pbSpeed: parseInt(req.body.pbSpeed),

                    bxUnlocked: bxu,
                    bxSoldAmount: parseInt(req.body.bxSoldAmount),
                    bxValue: parseInt(req.body.bxValue),
                    bxFrequency: parseInt(req.body.bxFrequency),
                    bxSpeed: parseInt(req.body.bxSpeed),

                    glUnlocked: glu,
                    glSoldAmount: parseInt(req.body.glSoldAmount),
                    glValue: parseInt(req.body.glValue),
                    glFrequency: parseInt(req.body.glFrequency),
                    glSpeed: parseInt(req.body.glSpeed),

                    byUnlocked: byu,
                    bySoldAmount: parseInt(req.body.bySoldAmount),
                    byValue: parseInt(req.body.byValue),
                    byFrequency: parseInt(req.body.byFrequency),
                    bySpeed: parseInt(req.body.bySpeed)
                }
            })
            return res.json({message: 'Info: The saving was successful!'})
        }
        else {
            return res.json({message: 'Unauthorized!'},401)
        }
       } catch {
        return res.json({message: 'Unauthorized!'},401)
       }

    })

}