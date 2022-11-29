const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })

module.exports = app => {
    app.post("/api/passwordchange", urlencodedParser, async (req, res) => {  //post-ot használunk, mivel szeretnénk adatot kérni a szervertől a /authorize aloldalon.
       const userAccount = await prisma.users.findFirst({ where: { password: req.body.generatedCode } })
        if (userAccount == null) {
            res.send(200);
            return
        }
        else {
            await prisma.users.update({
                where: {
                    password: req.body.generatedCode
                },
                data: {
                    password: req.body.newPassword
                }
            })
            res.send("Info: Successful password change.")
            return
        }
    })
}