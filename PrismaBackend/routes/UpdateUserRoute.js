const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY
var bodyParser = require('body-parser')
var urlencodedParser = bodyParser.urlencoded({ extended: false })

module.exports = (app) => {
    app.post('/api/updateuser', urlencodedParser, async (req, res) => {
        if (req.headers['authorization']) {
            const bearerHeader = req.headers['authorization']
            const bearerToken = bearerHeader.split(' ')[1]
            jwt.verify(bearerToken, jwtKey, async (err, decoded) => {
                if (err) {
                    res.status(401).json({ message: 'Unauthorized!' })
                } else {
                    await prisma.users.update({
                        where: {
                            username: '' + decoded.username,
                        },
                        data: {
                            isOnline: false,
                        },
                    })
                    return res.status(200).json({ message: 'Info: Successful save!' })
                }
            })
        }
        else {
            return res.status(401).json({ message: 'Unauthorized!' })
        }
    })
}
