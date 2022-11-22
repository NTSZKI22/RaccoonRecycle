const mongoose = require('mongoose')
const save = mongoose.model('save')
var bodyParser = require('body-parser')
var jsonParser = bodyParser.json()
var urlencodedParser = bodyParser.urlencoded({ extended: false })

module.exports = app => {
    app.post("/api/getsave", urlencodedParser, async (req, res) => {  //post-ot használunk, mivel szeretnénk adatot kérni a szervertől a /authorize aloldalon.
        var userSave = await save.findOne({ username: req.body.username })
            //when someone register, we create a save data, so there is no need to check if there is a save with this username or not.
            res.send(userSave)
    })

}