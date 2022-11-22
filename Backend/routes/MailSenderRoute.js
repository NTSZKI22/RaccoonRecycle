const mongoose = require('mongoose')
const Account = mongoose.model('account')
var nodemailer = require('nodemailer')
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })
const ShortUniqueId = require('short-unique-id')
const uuidv4 = new ShortUniqueId({ length: 10 })

module.exports = app => {
    app.post("/api/mail", urlencodedParser, async (req, res) => {  //post-ot használunk, mivel szeretnénk adatot kérni a szervertől a /authorize aloldalon.
        var userAccount = await Account.findOne({ email: req.body.email })
        if (userAccount == null) {
            res.send(200);
            return
        }
        else {
            var uuid = uuidv4()
            userAccount.password = uuid;
            var email = nodemailer.createTransport({
                service: "Gmail",
                auth: {
                    user: process.env.EMAIL,
                    pass: process.env.PASS
                }
            });
            email.sendMail({
                from: "RaccoonRecycleInfo <kornelhajto2004@gmail.com>",
                to: req.body.email,
                subject: "Forgotten password!",
                text: "Your code: " + uuid,
                html: '<p>In order change your password we sent you a code. If you want to change your password just press uuid in the game and type in your code we sent there. After that you just need to type in a new password and again your new passowrd. Thats it!<p><b>' + uuid + '</b>'
            });
            userAccount.save()
            res.send("Email sent to the adress!")

        }
    })
}