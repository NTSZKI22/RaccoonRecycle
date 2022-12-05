var express = require('express')
var mysql = require('mysql')
require('dotenv').config()
var app = express()

const mongoose = require('mongoose')
mongoose.connect(process.env.MONGOURI)

//Adatbázis sémák.
require('./models/accountModel')
require('./models/saveModel')

require('./routes/RegisterRoute')(app)
require('./routes/LoginRoute')(app)
require('./routes/MailSenderRoute')(app)
require('./routes/PasswordChangeRoute')(app)
require('./routes/GetSaveRoute')(app)
require('./routes/SaveRoute')(app)

app.listen(process.env.PORT, () => {
    console.log("Server has started on port: "+process.env.PORT)
});