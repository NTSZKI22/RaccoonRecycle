const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
var nodemailer = require('nodemailer')
var bodyParser = require('body-parser')
var urlencodedParser = bodyParser.urlencoded({ extended: false })
const ShortUniqueId = require('short-unique-id')
const uuidv4 = new ShortUniqueId({ length: 10 })

module.exports = app => {
    app.post('/api/mail', urlencodedParser, async (req, res) => {
        console.log(req.body.email)
        var userAccount = await prisma.users.findFirst({ where: { email: req.body.email } })
        console.log(userAccount)
        if (!userAccount) {
            return res.status(403).json({message: 'Forbidden!'})
        }
        else {
            var uuid = await uuidv4()
            await prisma.users.update({
                where:
                {
                    email: req.body.email
                },
                data: {
                    password: uuid
                }
            })
            var email = nodemailer.createTransport({
                service: 'Gmail',
                auth: {
                    user: process.env.EMAIL,
                    pass: process.env.PASS
                }
            })
            email.sendMail({ 
                from: 'RaccoonRecycleInfo <kornelhajto2004@gmail.com>',
                to: req.body.email,
                subject: 'Forgotten password!',
                text: 'Your code: ' + uuid,
                html: '<p>In order change your password we sent you a code. If you want to change your password just press uuid in the game and type in your code we sent there. After that you just need to type in a new password and again your new passowrd. Thats it!<p><b>' + uuid + '</b>'
            })
            return res.status(200).json('If there was an account with this address in our system we sent an email to the address.') 

        }
    })
}