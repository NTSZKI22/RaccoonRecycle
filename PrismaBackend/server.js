const { PrismaClient } = require('@prisma/client')
const dotenv = require('dotenv').config()
const cors = require('cors')
const express = require('express')
const app = express()
const port = process.env.PORT;
const prisma = new PrismaClient()
app.use(cors({
  origin: '*'
}))
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })

require('./routes/LoginRoute.js')(app)
require('./routes/RegisterRoute')(app)
require('./routes/GetSaveRoute')(app)
require('./routes/MailSender')(app)
require('./routes/SaveRoute')(app)
require('./routes/PasswordChange')(app)
require('./routes/UpdateUserRoute')(app)
require('./routes/ListOnlineUsersRoute')(app)
require('./routes/GetSaveIDRoute')(app)
require('./routes/SaveRoute')(app)
require('./routes/GetAchievementsRoute')(app)
require('./routes/SetAchievementsRoute')(app)

//TODO WEBLOGIN
//TODO GETTINGSAVE
module.exports = app; 

app.listen(/*port*/ Math.floor(Math.random() * (4444 - 3333 + 1) + 3333), () => {
  console.log(`API: API is running on port: ${port}`);
});


