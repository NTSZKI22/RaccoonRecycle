const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
const bcrypt = require('bcrypt')
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY
var bodyParser = require('body-parser')
var urlencodedParser = bodyParser.urlencoded({ extended: false })

//TODO: Reformatting!
module.exports = (app) => {
    app.post('/api/login', urlencodedParser, async (req, res) => {
        var userAccount = await prisma.users.findFirst({
            where: { username: req.body.username },
        })
        if (userAccount == null) {
            return res.status(401).send('Error: Invalid credentials!')
        } else {
            if (await bcrypt.compare(req.body.password, userAccount.password)) {
                if (userAccount.isOnline == true) {
                    return res.json(
                        'Info: You are logged in currently with your account on another device.'
                    )
                } else {
                    var data = {
                        time: Date(),
                        username: userAccount.username,
                        emailAddress: userAccount.email,
                    }
                    const token = jwt.sign(data, jwtKey)
                    await prisma.users.update({
                        where: {
                            username: '' + req.body.username,
                        },
                        data: {
                            isOnline: true,
                        },
                    })
                    return res.status(200).json({ token })
                }
            } else {
                return res.status(401).send('Error: Invalid credentials!')
            }
        }
    })
}
