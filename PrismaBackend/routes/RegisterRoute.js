const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })

module.exports = app => {
    app.post("/api/register", urlencodedParser, async (req, res) => {  //post-ot használunk, mivel szeretnénk adatot kérni a szervertől a /authorize aloldalon.
        var userAccountUsername = await prisma.users.findFirst({ where: { username: req.body.username } })
        var userAccountEmail = await prisma.users.findFirst({ where: { email: req.body.email } })
        if (userAccountUsername == null && userAccountEmail == null) {
            var newAccount = await prisma.users.create({
                data: {
                    email: req.body.email,
                    username: req.body.username,
                    password: req.body.password,
                    lastAuthenticated: ""+Date.now(),
                    registeredAt: ""+Date.now(),
                    isOnline: true,
                    saves: {
                        create: {
                            lastSaveDate: ""+Date.now(),
                            normalCurrency: 0,
                            prestigeCurrency: 0,
                            totalEarnings: 0,
                            pbSpeed: 0,
                            pbUnlocked: false,
                            pbSoldAmount: 0,
                            pbValue: 0,
                            pbFrequency: 0,

                            bxSpeed: 0,
                            bxUnlocked: false,
                            bxSoldAmount: 0,
                            bxValue: 0,
                            bxFrequency: 0,

                            glSpeed: 0,
                            glUnlocked: false,
                            glSoldAmount: 0,
                            glValue: 0,
                            glFrequency: 0,

                            bySpeed: 0,
                            byUnlocked: false,
                            bySoldAmount: 0,
                            byValue: 0,
                            byFrequency: 0,
                        }
                    }
                }
            })
            res.send('Account created!')
            return
        }
        else {
            res.send('Error: There is an account with this username or email address!')
            return
        }
    })

}