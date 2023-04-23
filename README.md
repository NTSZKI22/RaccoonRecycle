<h1 align="center">
RaccoonRecycle
</h1>
<p align="center">
<img src="https://i.imgur.com/KdDqkhl.png" align="center" width=200 height=200 />
</p>



Our projectwork is a multi-platform video game centered on a factory In this factory, players must use the help of raccoons to sort and recycle waste to generate income. The objective of the game is to develop and improve the elements of the assembly line to generate the highest possible income. Players can convert their earnings into new currency which can be used to purchase further improvements and progress in the game.

The style of the game is idle, incremental, which provides an engaging and rewarding experience for players who seek to pass the time. All players can start playing after a simple registration, as soon as they enter their email address, username and password to create a player profile. Thanks to this, players can easily resume gameplay anytime and anywhere, without losing their progress.

The game's experience is enhanced by its immersive and interactive sound and graphic elements. The graphic elements have been designed to provide an uniform look and feel throughout the game, which enhances the user's experience. The user interface is in English, but thanks to its simple design and intuitive controls, it can be easily played even without knowledge of the language.

In addition to the core gameplay, the game also features various elements like achievements, prestige functions, and a gem shop. These features provide players with a sense of accomplishment and satisfaction as they strive towards greater efficiency and profit. The game's new approach to waste management, combined with its engaging gameplay and interactive elements, make it a unique and enjoyable experience for all players.


Chapters: 
 - [Tech Stack](#tech)
 - [Features](#features)
 - [Enviroment Variables](#environment)
 - [Deployment](#deployment)
 - [API Endpoints](#api)
 - [Authors](#authors)
 - [Feedback](#feedback)
 
 
 
## Tech
### Tech Stack

**Client:** Unity, C#

**Server:** Node, Express, Prisma, MongoDB

 
 
## Features
### Screenshots

- Welcome Screen
<img src="https://i.imgur.com/UtCIFYf.png" align="center" />
- Login
<img src="https://i.imgur.com/yFeuYWa.png" align="center" />
- Register
<img src="https://i.imgur.com/ryURZPs.png" align="center" />
- Offline Earning
<img src="https://i.imgur.com/JYzR5QQ.png" align="center" />
- Achievements
<img src="https://i.imgur.com/LdnKowt.png" align="center" />
<img src="https://i.imgur.com/1TGHTGb.png" align="center" />
- Achievement gem store
<img src="https://i.imgur.com/VBeK0VT.png" align="center" />
- Animations
<img src="https://i.imgur.com/xPGvBMo.png" align="center" />
- Prestige
<img src="https://i.imgur.com/4NAXm5y.png" align="center" />
- Raccoons 
<img src="https://i.imgur.com/GWyprtL.png" align="center" />

 
 
## Environment
### Variables

To run the backend, you will need to change the following environment variables in your `.env.example` file, then rename it to `.env`

`JWTKEY` -> The key for JWT authentication

`PORT` -> It sets on what port will the server run

`DATABASE_URL` -> It sets the database for Prisma ORM

`EMAIL` -> The email address for email sending on forgetting the password

`PASS` -> The application password for the email application (only if you are using GMAIL)

## Deployment

If everything in the previous section is finished all you have to do is run 
```bash
  npm run dev
``` 

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



## Authors

- [@bojszareka](https://www.github.com/BojszaReka)
- [@hajtokornel](https://www.github.com/Lxndr01)


## Feedback

If you have any feedback, please reach out to us at help.raccoonrecycle@gmail.com
