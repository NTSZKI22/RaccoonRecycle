const app = require('../server')
const request = require('supertest')(app)
const dotenv = require('dotenv').config()

const data = {
    username: 'alexanderh',
    email: 'kornelhajto2004@gmail.com',
}


const validData = {
    email: 'asd123@.hu',
}

describe('POST /api/mail', () => {
    it('should get 403/Forbidden', async () => {
    // create a JWT token for an authenticated user

        // make a request to the endpoint with the token
        const response = await request
            .post('/api/mail')
            .type('form')
            .send(data)
            .expect(403)

        // check that the response body is an array of objects with the expected properties
        expect(response.body).toEqual({message: 'Forbidden!'})
    })

    it('should get 200', async () => {
    // create a JWT token for an authenticated user

        // make a request to the endpoint with the token
        const response = await request
            .post('/api/mail')
            .type('form')
            .send(validData)
            .expect(200)

        // check that the response body is an array of objects with the expected properties
        expect(response.body).toEqual('If there was an account with this address in our system we sent an email to the address.')
    })
})

