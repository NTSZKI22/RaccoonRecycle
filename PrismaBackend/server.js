const { PrismaClient } = require('@prisma/client')
const express = require('express')
const app = express()
const port = process.env.PORT;
const prisma = new PrismaClient()


    require('./routes/LoginRoute.js')(app)
    require('./routes/RegisterRoute')(app)
    require('./routes/GetSaveRoute')(app)
    require('./routes/MailSender')(app)
    require('./routes/SaveRoute')(app)
    require('./routes/PasswordChange')(app)
    require('./routes/UpdateUserRoute')(app)
    require('./routes/ListOnlineUsersRoute')(app)


    app.listen(port, () => {
        console.log(`API: API is running on port: ${port}`);
      });
