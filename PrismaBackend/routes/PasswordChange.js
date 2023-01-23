const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY

module.exports = app => {
    app.post("/api/passwordchange", urlencodedParser, async (req, res) => {  //post-ot használunk, mivel szeretnénk adatot kérni a szervertől a /authorize aloldalon.
       const userAccount = await prisma.users.findFirst({ where: { password: req.body.generatedCode } })
        if (userAccount == null) {
            res.send(200);
            return
        }
        else {
            const user = await prisma.users.findFirst({
                where: {
                    password: req.body.generatedCode
                }
            })

            await prisma.users.update({
                where: {
                    password: req.body.generatedCode
                },
                data: {
                    password: req.body.newPassword
                }
            })
            var data = {
                time: Date(),
                username: account.username,
                emailAddress: account.email
            }
            const token = jwt.sign(data, jwtKey);
            res.json(token)
            return
        }
    })
}