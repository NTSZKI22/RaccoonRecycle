
# RaccoonRecycle

A 2D Idle game made with Unity, with a JavaScript Backend.

Chapters: 
 - [API Endpoints](#api)

## API
## Endpoints

#### Register endpoint

```
  POST /api/register
```
| Header Type     | Description          |
| :-------------- | :-------------------------       |
| `Content-Type` |  application/x-www-form-urlencoded|
| `Accept` |  application/x-www-form-urlencoded      |

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `email` | `string`    | **Required**. Your email address |
| `username` | `string` | **Required**. Your username |
| `password` | `string` | **Required**. Your password |

#### Login endpoint

```
  POST /api/login
```
| Header Type     | Description          |
| :-------------- | :-------------------------       |
| `Content-Type` |  application/x-www-form-urlencoded|
| `Accept` |  application/x-www-form-urlencoded      |

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
|There is no need for extra parameters, because we get what we need from JWT token.|

#### Achievement setter endpoint

```
  POST /api/setAchievements
```
| Header Type     | Description          |
| :-------------- | :-------------------------       |
| `Authorization` |  **Required** Bearer token (JWT) |
| `Content-Type` |  application/x-www-form-urlencoded|
| `Accept` |         application/x-www-form-urlencoded      |


| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
|gemCurrency| `string` | **Required**.  How many gem do the player have?|
|itemLvl_1 | `string` | **Required**. Your firstItemLevel |
|itemLvl_2 | `string` | **Required**. Your secondItemLevel |
|itemLvl_3 | `string` | **Required**. Your thirdItemLevel |
|normalCurrency_spent | `string` | **Required**. How much normal currency does the player spent |
|prestigeCurrency_spent| `string` | **Required**. How much prestige currency does the player spent |
|achievemetnProgress| `string[]` | **Required**. How the player is progressed, e.g: 0_0_0, 1_0_1 |




