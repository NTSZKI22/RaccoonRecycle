const app = require('../server')
const request = require('supertest')(app)
const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
const dotenv = require('dotenv').config()
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY

const data = {
    date: Date(),
    username: 'alexanderh',
    email: 'lxndrhjto@gmail.com'
}
const token = jwt.sign(data, jwtKey)

describe('POST /api/getsaveid', () => {
    it('should return 401', async () => {
        // create a JWT token for an authenticated user

        // make a request to the endpoint with the token
        const response = await request.post('/api/getsaveid')
            .set('Authorization', 'Bearer ' + token)
            .expect(200)

    })
})

describe('POST /api/getsaveid', () => {
    it('should return 401', async () => {
        // create a JWT token for an authenticated user

        // make a request to the endpoint with the token
        const response = await request.post('/api/getsaveid')
            .expect(401)

    })
})