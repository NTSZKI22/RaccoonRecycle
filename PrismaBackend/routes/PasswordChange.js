const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
var bodyParser = require('body-parser')
var urlencodedParser = bodyParser.urlencoded({ extended: false })
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY

module.exports = app => {
    app.post('/api/passwordchange', urlencodedParser, async (req, res) => { 
        try {
            const userAccount = await prisma.users.findFirst({ where: { password: req.body.generatedCode } })
        if (!userAccount) {
            return res.status(401).json({})
        }
        else {
            const account = await prisma.users.update({
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
            const token = jwt.sign(data, jwtKey)
            return res.status(200).json(token)
        }
        } catch {
            return res.status(401).json({})
        }
    })
}