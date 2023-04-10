
# RaccoonRecycle

A 2D Idle game made with Unity, with a JavaScript Backend.


## API Endpoints

#### Register endpoint

```
  POST /api/register
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `email` | `string`    | **Required**. Your email address |
| `username` | `string` | **Required**. Your username |
| `password` | `string` | **Required**. Your password |

#### Login endpoint

```
  POST /api/login
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `username` | `string` | **Required**. Your username for the game |
| `password` | `string` | **Required**. Your password for your account |

#### Mail Sender endpoint

```
  POST /api/mail
```

| Header Type     | Description          |
| :-------------- | :-------------------------       |
| `Authorization` |  **Required** Bearer token (JWT) |
| `Content-Type` |  application/x-www-form-urlencoded|
| `Accept` |  application/x-www-form-urlencoded      |

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `email`   | `string` | **Required**. Your email address for your account that you registered |


#### Password change endpoint

```
  POST /api/passwordchange
```
| Header Type     | Description          |
| :-------------- | :-------------------------       |
| `Authorization` |  **Required** Bearer token (JWT) |
| `Content-Type` |  application/x-www-form-urlencoded|
| `Accept` |  application/x-www-form-urlencoded      |

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `generatedCode`   | `string (uuid)` | **Required**. The code that you received in email |
| `newPassword`     | `string`        | **Required**. Your new password |

#### Achievement getter endpoint

```
  POST /api/getAchievements
```
| Header Type     | Description          |
| :-------------- | :-------------------------       |
| `Authorization` |  **Required** Bearer token (JWT) |
| `Content-Type` |  application/x-www-form-urlencoded|
| `Accept` |  application/x-www-form-urlencoded      |


| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `generatedCode`   | `string (uuid)` | **Required**. The code that you received in email |
| `newPassword`     | `string`        | **Required**. Your new password |



