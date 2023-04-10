const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
module.exports = app => {
    app.post('/api/setAchievements', jsonParser, async (req, res) => {  //postot használunk mivel a kérés küldésekor a bodyban szeretnénk küldeni az adatokat.    
        try {
            if(req.headers['authorization']){

                const bearerHeader = req.headers['authorization']
                const bearerToken = bearerHeader.split(' ')[1]
                const verified = jwt.verify(bearerToken, jwtKey)
                if(verified){
                    const oldAchievements = await prisma.users.findFirst({
                        where: {
                            username: verified.username
                        },
                        select:{
                            Achievements: true
                        }
                    })
                    await prisma.users.update({
                        where: {
                            username: verified.username
                        },
                        data: {
                            Achievements: {
                                update: {
                                    data: {
                                        achievementProgress: {
                                            set: req.body.achievementProgress
                                        },
                                        gemCurrency: parseInt(req.body.gemCurrency),
                                        itemLvl_1: parseInt(req.body.itemLvl_1),
                                        itemLvl_2: parseInt(req.body.itemLvl_2),
                                        itemLvl_3: parseInt(req.body.itemLvl_3),
                                        normalCurrency_spent: parseFloat(req.body.normalCurrency_spent),
                                        prestigeCurrency_spent: parseFloat(req.body.prestigeCurrency_spent)
                                    },
                                    where: {
                                        id: oldAchievements.Achievements[0].id
                                    }
                                }
                            }
                        }
                    })
                    return res.json({message: 'Successful save!'}, 200)
                }
                return res.json({message: 'Unauthorized!'},401)
            }
            else{
                return res.json({message: 'Unauthorized!'},401)
            }
        } catch {
            return res.json({message: 'Unauthorized!'},401)
        }
        
    })

}