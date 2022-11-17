const mongoose = require('mongoose')
const { Schema } = mongoose


const saveSchema = new Schema({
    username: String, //felhsználónév, string tehát karakterlánc típúsú.
    lastSaveDate: Date,
    normalCurrency: Number,
    prestigeCurrency: Number,
    totalEarnings: Number,
    pbUnlocked: Boolean,
    pbSoldAmount: Number,
    pbValue: Number,
    pbSpeed: Number,
    pbFrequency: Number,
    bxUnlocked: Boolean,
    bxSoldAmount: Number,
    bxValue: Number,
    bxSpeed: Number,
    bxFrequency: Number,
    glUnlocked: Boolean,
    glSoldAmount: Number,
    glValue: Number,
    glSpeed: Number,
    glFrequency: Number,
    byUnlocked: Boolean,
    bySoldAmount: Number,
    byValue: Number,
    bySpeed: Number,
    byFrequency: Number,
    
})

mongoose.model('save', saveSchema)//kiexportáljuk a sémát.