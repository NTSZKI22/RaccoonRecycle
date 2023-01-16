const request = require("supertest");
const GetSaveRoute = require("../routes/GetSaveRoute");


describe("Test the root path", () => {
    test("It should response the GET method", async () => {
      const response = await request(request).get("/api/admin/listOnlinePlayers");
      expect(response.statusCode).toBe(200);
    });
  });
  