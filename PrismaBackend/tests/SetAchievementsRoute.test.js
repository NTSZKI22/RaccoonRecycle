data: {

}

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


const token = jwt.sign(data, jwtKey)

describe('POST /api/setAchievements', () => {
    it('should change a players isonline status', async () => {
    // create a JWT token for an authenticated user

        // make a request to the endpoint with the token
        const response = await request
            .post('/api/setAchievements')
            .set('Authorization', 'Bearer ' + token)
            .type('form')
            .send(data)
            .expect(401)

        // check that the response body is an array of objects with the expected properties
    })
})
