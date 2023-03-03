const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY
var bodyParser = require('body-parser')
var urlencodedParser = bodyParser.urlencoded({ extended: false })

module.exports = (app) => {
    app.get('/api/admin/listOnlinePlayers', urlencodedParser, async (req, res) => {
        if (req.headers['authorization']) {
            const bearerHeader = req.headers['authorization']
            const bearerToken = bearerHeader.split(' ')[1]
            jwt.verify(bearerToken, jwtKey, async (err, decoded) => {
                if (err) {
                    return res.status(401).json({ message: 'Unauthorized!' })
                } else {
                    const onlinePlayers = await prisma.users.findMany({
                        where: {
                            isOnline: true,
                        },
                        select: {
                            username: true,
                            saves: false,
                        },
                    })
                    return res.status(200).json({ onlinePlayers })
                }
            })
        }
        else {
            return res.status(401).json({ message: 'Unauthorized!' })
        }
    }
    )
}
