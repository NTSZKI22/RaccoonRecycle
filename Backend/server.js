var express = require('express')
var mysql = require('mysql')
require('dotenv').config()
var app = express()

const mongoose = require('mongoose')
mongoose.connect(process.env.MONGOURI)

require('./models/accountModel')
require('./models/saveModel')

require('./routes/RegisterRoute')(app)
require('./routes/LoginRoute')(app)
require('./routes/MailSenderRoute')(app)
require('./routes/PasswordChangeRoute')(app)
require('./routes/CreateSaveRoute')(app)
require('./routes/SaveRoute')(app)

app.listen(process.env.PORT, () => {
    console.log("Server has started on port: "+process.env.PORT)
});