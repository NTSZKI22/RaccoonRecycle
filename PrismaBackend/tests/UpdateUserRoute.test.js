const app = require('../server')
const request = require('supertest')(app)
const dotenv = require('dotenv').config()
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY

const data = {
    date: Date(),
    username: 'hellokamanoka',
    email: 'test@test.test',
}

const invalidData = {
    date: Date(),
    username: 'hellokamanoka',
    email: 'test@test.test',
}
const token = jwt.sign(data, jwtKey)

describe('POST /api/updateuser', () => {
    it('should change a players isonline status', async () => {
    // create a JWT token for an authenticated user

        // make a request to the endpoint with the token
        const response = await request
            .post('/api/updateuser')
            .set('Authorization', 'Bearer ' + token)
            .type('form')
            .send(data)
            .expect(200)

        // check that the response body is an array of objects with the expected properties
        expect(response.body).toEqual({ message: 'Info: Successful save!' })
    })
})

describe('POST /api/updateuser', () => {
    it('should get 401', async () => {
    // create a JWT token for an authenticated user

        // make a request to the endpoint with the token
        const response = await request
            .post('/api/updateuser')
            .type('form')
            .send(data)
            .expect(401)

        // check that the response body is an array of objects with the expected properties
        expect(response.body).toEqual({ message: 'Unauthorized!' })
    })
})

describe('POST /api/updateuser', () => {
    it('should get 401', async () => {
    // create a JWT token for an authenticated user

        // make a request to the endpoint with the token
        const response = await request
            .post('/api/updateuser')
            .set('Authorization', 'Bearer ' + token + 'vfvfvfv')
            .expect(401)
    })
})
