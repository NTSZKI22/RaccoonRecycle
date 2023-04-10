const app = require('../server')
const request = require('supertest')(app)
const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
const dotenv = require('dotenv').config()
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY


const data = {
    date: Date(),
    username: 'testuser',
    email: 'test@test.test'
}
const token = jwt.sign(data, jwtKey)

describe('GET /api/admin/listOnlinePlayers', () => {
    it('should return a list of online players if authenticated', async () => {
    // create a JWT token for an authenticated user

        // make a request to the endpoint with the token
        const response = await request.get('/api/admin/listOnlinePlayers')
            .set('Authorization', 'Bearer ' + token)
            .expect(200)

        // check that the response body is an array of objects with the expected properties
        expect(response.body).toEqual(expect.arrayContaining([
        ]))
    })
})

describe('GET /api/admin/listOnlinePlayers', () => {
    it('should return 401/unathorized', async () => {
    // create a JWT token for an authenticated user

        // make a request to the endpoint with the token
        const response = await request.get('/api/admin/listOnlinePlayers')
            .set('Authorization', 'Bearer ' + token+'adsadsds')
            .expect(401)
    })
})

describe('GET /api/admin/listOnlinePlayers', () => {
    it('should return a 401 status code if not authenticated', async () => {
    // make a request to the endpoint without a token
        const response = await request.get('/api/admin/listOnlinePlayers')
            .expect(401)
    })
})