-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema project
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema project
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `project` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `project` ;

-- -----------------------------------------------------
-- Table `project`.`category`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `project`.`category` (
  `id` VARCHAR(191) NOT NULL,
  `name` VARCHAR(191) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `Category_name_key` (`name` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `project`.`course`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `project`.`course` (
  `id` VARCHAR(191) NOT NULL,
  `userId` VARCHAR(191) NOT NULL,
  `title` TEXT NOT NULL,
  `description` TEXT NULL DEFAULT NULL,
  `imageUrl` TEXT NULL DEFAULT NULL,
  `price` DOUBLE NULL DEFAULT NULL,
  `isPublished` TINYINT(1) NOT NULL DEFAULT '0',
  `categoryId` VARCHAR(191) NULL DEFAULT NULL,
  `createdAt` DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  `updatedAt` DATETIME(3) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `Course_categoryId_idx` (`categoryId` ASC) VISIBLE,
  FULLTEXT INDEX `Course_title_idx` (`title`) VISIBLE,
  CONSTRAINT `Course_categoryId_fkey`
    FOREIGN KEY (`categoryId`)
    REFERENCES `project`.`category` (`id`)
    ON DELETE SET NULL
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `project`.`attachment`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `project`.`attachment` (
  `id` VARCHAR(191) NOT NULL,
  `name` VARCHAR(191) NOT NULL,
  `url` TEXT NOT NULL,
  `courseId` VARCHAR(191) NOT NULL,
  `createdAt` DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  `updatedAt` DATETIME(3) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `Attachment_courseId_idx` (`courseId` ASC) VISIBLE,
  CONSTRAINT `Attachment_courseId_fkey`
    FOREIGN KEY (`courseId`)
    REFERENCES `project`.`course` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `project`.`chapter`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `project`.`chapter` (
  `id` VARCHAR(191) NOT NULL,
  `title` VARCHAR(191) NOT NULL,
  `description` TEXT NULL DEFAULT NULL,
  `videoUrl` TEXT NULL DEFAULT NULL,
  `position` INT NOT NULL,
  `isPublished` TINYINT(1) NOT NULL DEFAULT '0',
  `isFree` TINYINT(1) NOT NULL DEFAULT '0',
  `courseId` VARCHAR(191) NOT NULL,
  `createdAt` DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  `updatedAt` DATETIME(3) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `Chapter_courseId_idx` (`courseId` ASC) VISIBLE,
  CONSTRAINT `Chapter_courseId_fkey`
    FOREIGN KEY (`courseId`)
    REFERENCES `project`.`course` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `project`.`muxdata`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `project`.`muxdata` (
  `id` VARCHAR(191) NOT NULL,
  `assetId` VARCHAR(191) NOT NULL,
  `playbackId` VARCHAR(191) NULL DEFAULT NULL,
  `chapterId` VARCHAR(191) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `MuxData_chapterId_key` (`chapterId` ASC) VISIBLE,
  CONSTRAINT `MuxData_chapterId_fkey`
    FOREIGN KEY (`chapterId`)
    REFERENCES `project`.`chapter` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `project`.`purchase`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `project`.`purchase` (
  `id` VARCHAR(191) NOT NULL,
  `userId` VARCHAR(191) NOT NULL,
  `courseId` VARCHAR(191) NOT NULL,
  `createdAt` DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  `updatedAt` DATETIME(3) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `Purchase_userId_courseId_key` (`userId` ASC, `courseId` ASC) VISIBLE,
  INDEX `Purchase_courseId_idx` (`courseId` ASC) VISIBLE,
  CONSTRAINT `Purchase_courseId_fkey`
    FOREIGN KEY (`courseId`)
    REFERENCES `project`.`course` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `project`.`stripecustomer`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `project`.`stripecustomer` (
  `id` VARCHAR(191) NOT NULL,
  `userId` VARCHAR(191) NOT NULL,
  `stripeCustomerId` VARCHAR(191) NOT NULL,
  `createdAt` DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  `updatedAt` DATETIME(3) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `StripeCustomer_userId_key` (`userId` ASC) VISIBLE,
  UNIQUE INDEX `StripeCustomer_stripeCustomerId_key` (`stripeCustomerId` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `project`.`userprogress`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `project`.`userprogress` (
  `id` VARCHAR(191) NOT NULL,
  `userId` VARCHAR(191) NOT NULL,
  `chapterId` VARCHAR(191) NOT NULL,
  `isCompleted` TINYINT(1) NOT NULL DEFAULT '0',
  `createdAt` DATETIME(3) NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
  `updatedAt` DATETIME(3) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `UserProgress_userId_chapterId_key` (`userId` ASC, `chapterId` ASC) VISIBLE,
  INDEX `UserProgress_chapterId_idx` (`chapterId` ASC) VISIBLE,
  CONSTRAINT `UserProgress_chapterId_fkey`
    FOREIGN KEY (`chapterId`)
    REFERENCES `project`.`chapter` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
