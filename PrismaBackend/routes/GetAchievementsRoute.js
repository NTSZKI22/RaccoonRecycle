const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY
var bodyParser = require('body-parser')
var urlencodedParser = bodyParser.urlencoded({ extended: false })

module.exports = app => {
    app.post('/api/getAchievements', urlencodedParser, async (req, res) => {
        try {
            if (req.headers['authorization']) {
                const bearerHeader = req.headers['authorization']
                const bearerToken = bearerHeader.split(' ')[1]
                const verified = jwt.verify(bearerToken, jwtKey)
                if (verified) {
                    const achievements = await prisma.users.findFirst({
                        where: {
                            username: verified.username
                        },
                        select: {
                            Achievements: true
                        }
                    })
                    return res.send(achievements)//elküldjük a mentést.
                }
                return res.json({ message: 'Unauthorized!' }, 401)
            }
            else {
                return res.json({ message: 'Unauthorized!' }, 401)
            }
        } catch {
            return res.json({ message: 'Unauthorized!' }, 401)
        }

    })

}