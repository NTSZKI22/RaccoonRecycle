const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
const bcrypt = require('bcrypt')
const saltRounds = 8
var bodyParser = require('body-parser')
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY
var urlencodedParser = bodyParser.urlencoded({ extended: false })
var hash

module.exports = app => {
    app.post('/api/register', urlencodedParser, async (req, res) => {
        try {
            var userAccountUsername = await prisma.users.findFirst({ where: { username: req.body.username } })
        var userAccountEmail = await prisma.users.findFirst({ where: { email: req.body.email } })
        if (userAccountUsername == null && userAccountEmail == null) {
            var data = {
                time: Date(),
                username: req.body.username,
                emailAddress: req.body.email
            }
            const token = jwt.sign(data, jwtKey)
            hash = await bcrypt.hash(req.body.password, saltRounds)
            var newAccount = await prisma.users.create({
                data: {
                    email: req.body.email,
                    username: req.body.username,
                    password: hash,
                    registeredAt: '' + Date.now(),
                    isOnline: true,
                    saves: {
                        create: {
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
                    },
                    Achievements: {
                        create: {

                        }
                    }
                }
            })
            await prisma.logs.create({
                data: {
                    message: 'A user was created, with the username of: ' + newAccount.username,
                    madeBy: 'Server'
                }
            })
            return res.json({ message: 'Account created!', token: token }, 200)
        }
        else {
            return res.status(401).send('Error: There is an account with this username or email address!')
        }
        }
        catch {
            return res.status(401).send('Error: There was an error with the server!')
        }
    })

}