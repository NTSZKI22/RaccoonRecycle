const dotenv = require('dotenv').config()
const cors = require('cors')
const express = require('express')
const app = express()
const port = process.env.PORT;
app.use(cors({
  origin: '*'
}))

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

module.exports = app

app.listen(Math.floor(Math.random() * (3333 - 2222) + 2222), () => {
    console.log('Szever elindult!')
})

/* app.listen(port, () => {
    console.log(`API: API is running on port: ${port}`)
}) */


