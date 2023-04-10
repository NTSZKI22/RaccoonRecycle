const app = require('../server')
const request = require('supertest')(app)
const { PrismaClient } = require('@prisma/client')
const prisma = new PrismaClient()
const dotenv = require('dotenv').config()
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY

const data = {
    generatedCode: 'SGrxAxp0NT',
    newPassword: 'asd123B2'
}

const validData = {
    generatedCode: 'SGrxAxp0NT',
    newPassword: 'asd123B2'
}

describe('POST /api/passwordchange', () => {
    it('should return 401', async () => {
        // create a JWT token for an authenticated user

        // make a request to the endpoint with the token
        const response = await request.post('/api/passwordchange')
            .type('form')
            .send(data)
            .expect(401)

    })
})