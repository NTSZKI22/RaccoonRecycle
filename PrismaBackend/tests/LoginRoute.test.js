const app = require('../server')
const request = require('supertest')(app)
const dotenv = require('dotenv').config()
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY

const startServer = require('../utils/startServer')

beforeAll(() => {
    //startServer(3333);
})

const data = {
    username: 'kortefa',
    password: 'kortefacska',
}

const validData = {
    username: 'hellokamanoka',
    password: 'hellokamanoka',
}

const invalidData = {
    username: 'alexanderhdsadsadsa',
    password: 'Braszerelmem18',
}

const inavlidCredentialsData = {
    username: 'alexanderh',
    password: 'Braszerelmem1833',
}

describe('POST /api/login', () => {
    it('should return 200', async () => {
    // create a JWT token for an authenticated user

        // make a request to the endpoint with the token
        const response = await request
            .post('/api/login')
            .type('form')
            .send(data)
            .expect(200)
    })
})


describe('POST /api/login', () => {
    it('should return a 401', async () => {
        // create a JWT token for an authenticated user
  
        // make a request to the endpoint with the token
        const response = await request
            .post('/api/login')
            .type('form')
            .send(inavlidCredentialsData)
            .expect(401)
    })
})

describe('POST /api/login', () => {
    it('should return 401', async () => {
        // create a JWT token for an authenticated user
  
        // make a request to the endpoint with the token
        const response = await request
            .post('/api/login')
            .type('form')
            .send(invalidData)
            .expect(401)
    })
})

describe('POST /api/login', () => {
    it('should return token', async () => {
        // create a JWT token for an authenticated user
  
        // make a request to the endpoint with the token
        const response = await request
            .post('/api/login')
            .type('form')
            .send(validData)
            .expect(401)
    })
})