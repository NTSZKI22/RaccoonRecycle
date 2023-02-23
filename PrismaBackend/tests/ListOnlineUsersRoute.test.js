const app = require('../server')
const request = require('supertest')(app)
const dotenv = require('dotenv').config()
const jwt = require('jsonwebtoken')
const jwtKey = process.env.JWTKEY

const startServer = require('../utils/startServer')

beforeAll(() => {
  startServer();
});

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
      expect.objectContaining({
        username: expect.any(String)
      })
    ]))
  })
})

describe('GET /api/admin/listOnlinePlayers', () => {
  it('should return a 422 status code if not authenticated', async () => {
    // make a request to the endpoint without a token
    const response = await request.get('/api/admin/listOnlinePlayers')
      .expect(422)

    // check that the response body is empty
    expect(response.body).toEqual({})
  })
})