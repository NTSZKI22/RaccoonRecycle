var express = require('express')
var mysql = require('mysql')
require('dotenv').config()
var app = express()

const mongoose = require('mongoose')
mongoose.connect(process.env.MONGOURI)

require('./models/account')

require('./routes/RegisterRoute')(app)
require('./routes/LoginRoute')(app)

app.listen(process.env.PORT, () => {
    console.log("Server has started on port: "+process.env.PORT)
})