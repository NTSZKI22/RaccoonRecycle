CREATE TABLE "Users" (
  "id" CHAR(25) NOT NULL PRIMARY KEY,
  "username" VARCHAR(255) NOT NULL UNIQUE,
  "email" VARCHAR(255) NOT NULL UNIQUE,
  "password" VARCHAR(255) NOT NULL UNIQUE,
  "lastAuthenticated" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  "registeredAt" VARCHAR(255) NOT NULL,
  "isOnline" BOOLEAN NOT NULL,
);

CREATE TABLE "Saves" (
  "id" CHAR(25) NOT NULL PRIMARY KEY,
  "usersId" CHAR(25) UNIQUE,
  "lastSaveDate" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  "normalCurrency" INT NOT NULL,
  "prestigeCurrency" INT NOT NULL,
  "totalEarnings" INT NOT NULL,
  "pbUnlocked" BOOLEAN NOT NULL DEFAULT FALSE,
  "pbSoldAmount" INT NOT NULL,
  "pbValue" INT NOT NULL,
  "pbSpeed" INT NOT NULL,
  "pbFrequency" INT NOT NULL,
  "bxUnlocked" BOOLEAN NOT NULL DEFAULT FALSE,
  "bxSoldAmount" INT NOT NULL,
  "bxValue" INT NOT NULL,
  "bxSpeed" INT NOT NULL,
  "bxFrequency" INT NOT NULL,
  "glUnlocked" BOOLEAN NOT NULL DEFAULT FALSE,
  "glSoldAmount" INT NOT NULL,
  "glValue" INT NOT NULL,
  "glSpeed" INT NOT NULL,
  "glFrequency" INT NOT NULL,
  "byUnlocked" BOOLEAN NOT NULL DEFAULT FALSE,
  "bySoldAmount" INT NOT NULL,
  "byValue" INT NOT NULL,
  "bySpeed" INT NOT NULL,
  "byFrequency" INT NOT NULL,
  FOREIGN KEY ("usersId") REFERENCES "Users"("id") ON DELETE SET NULL ON UPDATE CASCADE
);

CREATE TABLE "Achievements" (
  "id" CHAR(25) NOT NULL PRIMARY KEY,
  "normalCurrency_spent" FLOAT NOT NULL DEFAULT 0,
  "prestigeCurrency_spent" FLOAT NOT NULL DEFAULT 0,
  "gemCurrency" INT NOT NULL DEFAULT 0,
  "achievementProgress" TEXT[] NOT NULL DEFAULT '{"0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0", "0_0"}',
  "itemLvl_1" INT NOT NULL DEFAULT 0,
  "itemLvl_2" INT NOT NULL DEFAULT 0,
  "itemLvl_3" INT NOT NULL DEFAULT 0,
  "usersId" CHAR(25),
  FOREIGN KEY ("usersId") REFERENCES "Users"("id") ON DELETE SET NULL ON UPDATE CASCADE
);

CREATE TABLE "Logs" (
  "id" CHAR(25) NOT NULL PRIMARY KEY,
  "message" VARCHAR(255) NOT NULL,
  "madeBy" VARCHAR(255) NOT NULL
);