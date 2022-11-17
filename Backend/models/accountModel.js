const mongoose = require('mongoose')
const { Schema } = mongoose


//Új séma létrehozása, adunk neki egy felhasználónevet, jelszót, és tároljuk, hogy mikor jelentkezett be utoljára.
const accountSchema = new Schema({
    email: String,//email cím, string tehát karakterlánc típúsú.
    username: String, //felhsználónév, string tehát karakterlánc típúsú.
    password: String, //jelszó, string tehát karakterlánc típúsú.
    lastAuthenticated: Date, //utolsó autentikáció, dátum formátumú.
    registeredAt: Date
})

mongoose.model('account', accountSchema)//kiexportáljuk a sémát.