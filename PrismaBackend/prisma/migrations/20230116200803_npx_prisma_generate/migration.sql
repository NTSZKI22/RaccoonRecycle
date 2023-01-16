/*
  Warnings:

  - The primary key for the `saves` table will be changed. If it partially fails, the table could be left without primary key constraint.
  - You are about to drop the column `_id` on the `saves` table. All the data in the column will be lost.
  - The primary key for the `users` table will be changed. If it partially fails, the table could be left without primary key constraint.
  - You are about to drop the column `_id` on the `users` table. All the data in the column will be lost.
  - Added the required column `id` to the `Saves` table without a default value. This is not possible if the table is not empty.
  - Added the required column `id` to the `Users` table without a default value. This is not possible if the table is not empty.

*/
-- DropForeignKey
ALTER TABLE `saves` DROP FOREIGN KEY `Saves_usersId_fkey`;

-- AlterTable
ALTER TABLE `saves` DROP PRIMARY KEY,
    DROP COLUMN `_id`,
    ADD COLUMN `id` INTEGER NOT NULL AUTO_INCREMENT,
    ADD PRIMARY KEY (`id`);

-- AlterTable
ALTER TABLE `users` DROP PRIMARY KEY,
    DROP COLUMN `_id`,
    ADD COLUMN `id` INTEGER NOT NULL AUTO_INCREMENT,
    ADD PRIMARY KEY (`id`);

-- AddForeignKey
ALTER TABLE `Saves` ADD CONSTRAINT `Saves_usersId_fkey` FOREIGN KEY (`usersId`) REFERENCES `Users`(`id`) ON DELETE SET NULL ON UPDATE CASCADE;
