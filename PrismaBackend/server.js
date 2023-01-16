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

    require('./routes/LoginRoute.js')(app)
    require('./routes/RegisterRoute')(app)
    require('./routes/GetSaveRoute')(app)
    require('./routes/MailSender')(app)
    require('./routes/SaveRoute')(app)
    require('./routes/PasswordChange')(app)
    require('./routes/UpdateUserRoute')(app)
    require('./routes/ListOnlineUsersRoute')(app)
    require('./routes/GetSaveIDRoute')(app)


    app.listen(port, () => {
        console.log(`API: API is running on port: ${port}`);
      });
