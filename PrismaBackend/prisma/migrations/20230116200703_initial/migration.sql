-- CreateTable
CREATE TABLE `Users` (
    `_id` INTEGER NOT NULL AUTO_INCREMENT,
    `username` VARCHAR(191) NOT NULL,
    `email` VARCHAR(191) NOT NULL,
    `password` VARCHAR(191) NOT NULL,
    `lastAuthenticated` DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
    `registeredAt` VARCHAR(191) NOT NULL,
    `isOnline` BOOLEAN NOT NULL,

    UNIQUE INDEX `Users_username_key`(`username`),
    UNIQUE INDEX `Users_email_key`(`email`),
    UNIQUE INDEX `Users_password_key`(`password`),
    PRIMARY KEY (`_id`)
) DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- CreateTable
CREATE TABLE `Saves` (
    `_id` INTEGER NOT NULL AUTO_INCREMENT,
    `usersId` INTEGER NULL,
    `lastSaveDate` DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
    `normalCurrency` INTEGER NOT NULL,
    `prestigeCurrency` INTEGER NOT NULL,
    `totalEarnings` INTEGER NOT NULL,
    `pbUnlocked` BOOLEAN NOT NULL DEFAULT false,
    `pbSoldAmount` INTEGER NOT NULL,
    `pbValue` INTEGER NOT NULL,
    `pbSpeed` INTEGER NOT NULL,
    `pbFrequency` INTEGER NOT NULL,
    `bxUnlocked` BOOLEAN NOT NULL DEFAULT false,
    `bxSoldAmount` INTEGER NOT NULL,
    `bxValue` INTEGER NOT NULL,
    `bxSpeed` INTEGER NOT NULL,
    `bxFrequency` INTEGER NOT NULL,
    `glUnlocked` BOOLEAN NOT NULL DEFAULT false,
    `glSoldAmount` INTEGER NOT NULL,
    `glValue` INTEGER NOT NULL,
    `glSpeed` INTEGER NOT NULL,
    `glFrequency` INTEGER NOT NULL,
    `byUnlocked` BOOLEAN NOT NULL DEFAULT false,
    `bySoldAmount` INTEGER NOT NULL,
    `byValue` INTEGER NOT NULL,
    `bySpeed` INTEGER NOT NULL,
    `byFrequency` INTEGER NOT NULL,

    PRIMARY KEY (`_id`)
) DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- CreateTable
CREATE TABLE `Logs` (
    `id` INTEGER NOT NULL AUTO_INCREMENT,
    `message` VARCHAR(191) NOT NULL,
    `madeBy` VARCHAR(191) NOT NULL,
    `madeAt` DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),

    PRIMARY KEY (`id`)
) DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- AddForeignKey
ALTER TABLE `Saves` ADD CONSTRAINT `Saves_usersId_fkey` FOREIGN KEY (`usersId`) REFERENCES `Users`(`_id`) ON DELETE SET NULL ON UPDATE CASCADE;
