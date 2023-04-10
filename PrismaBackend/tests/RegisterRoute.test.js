const app = require('../server')
const request = require('supertest')(app)
const dotenv = require('dotenv').config()
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY


const data = {
    username: '32131',
    email: 'hello@fasfas.333333',
    password: 'kortefacsk333333',
}

const validData = {
    username: 'hellokamanoka',
    email: 'hello@fasfas.hg',
    password: 'hellokamanoka',
}

const invalidData = {
    username: 'alexanderhdsadsadsa',
    email: 'hello@fasfas.hg',
    password: 'Braszerelmem18',
}

const inavlidCredentialsData = {
    username: 'alexanderh',
    email: 'hello@fasfas.hg',
    password: 'Braszerelmem1833',
}

describe('POST /api/register', () => {
    it('should return 200', async () => {
    // create a JWT token for an authenticated user

        // make a request to the endpoint with the token
        const response = await request
            .post('/api/register')
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