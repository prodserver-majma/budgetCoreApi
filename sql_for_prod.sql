CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    ALTER DATABASE CHARACTER SET utf8;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `acedemicyear_data` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `fromday_hijri` int(11) NULL,
        `frommonth_hijri` int(11) NULL,
        `fromyear_hijri` int(11) NULL,
        `today_hijri` int(11) NULL,
        `tomonth_hijri` int(11) NULL,
        `toyear_hijri` int(11) NULL,
        `acedemicYear` int(11) NULL,
        `acedemicName` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `azwaaj_minentry` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsid` int(11) NULL,
        `date` date NULL,
        `min` int(11) NULL,
        `deptVenueId` int(11) NULL,
        `createdBy` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `description` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `policyId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `branch_userdept_venue` (
        `deptVenueId` int NOT NULL,
        `userId` int NOT NULL,
        CONSTRAINT `PK_branch_userdept_venue` PRIMARY KEY (`deptVenueId`, `userId`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `branch_userregistrationform_dropdown_set` (
        `psetId` int NOT NULL,
        `userId` int NOT NULL,
        CONSTRAINT `PK_branch_userregistrationform_dropdown_set` PRIMARY KEY (`psetId`, `userId`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `enayat_medical_billentry` (
        `id` int NOT NULL AUTO_INCREMENT,
        `billPeriod` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `requestType` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `requestFor` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `entryDate` datetime(6) NULL,
        `billType` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `billDate` datetime(6) NULL,
        `billFrom` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `amount` int NULL,
        `illness` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `billPeriodId` int NULL,
        `aplicantItsId` int NULL,
        `relationType` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `relationTypeId` int NULL,
        `billStatus` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `status` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `updatedOn` datetime(6) NULL,
        `updatedBy` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `currencySymbol` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `attachment` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `amount_billClearance` int NULL,
        `billNumber` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `relationItsId` int NULL,
        `originalBillStatus` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `current_counter` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `count` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `campfasal_kutub` (
        `id` int NOT NULL AUTO_INCREMENT,
        `fasalclass` int NULL,
        `pdfLink` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `department` (
        `deptId` int(11) NOT NULL AUTO_INCREMENT,
        `deptName` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `tag` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`deptId`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `dropdown_dataset_header` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `export_category` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `categoryId` int(11) NULL,
        `categoryName` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `fieldActualName` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `fieldDisplayName` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `export_type` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `description` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `fileName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `export_type_displayheader` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `typeId` int(11) NULL,
        `actualName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `displayName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `form` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `description` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdByIts` int(11) NOT NULL,
        `createdOn` datetime NOT NULL,
        `isActive` tinyint(1) NOT NULL DEFAULT '1',
        `setting` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `global_constant` (
        `key` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `value` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`key`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `hijri_calender` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `hijri_day` int(11) NULL,
        `hijri_month` int(11) NULL,
        `hijri_year` int(11) NULL,
        `english_day` int(11) NULL,
        `english_month` int(11) NULL,
        `english_year` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `hijri_months` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `hijriMonthName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `holiday_calender` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `holidayDate` date NULL,
        `name` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdOn` datetime NOT NULL,
        `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `duration` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `holiday_hijri_miqaat` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `month_id` int(11) NULL,
        `date_id` int(11) NULL,
        `miqaats_title` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `miqaats_description` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `miqaats_priority` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `ikhtibaar` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `kg_faimalydetails` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `hofItsId` int(11) NULL,
        `itsId` int(11) NULL,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `age` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `relation` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `jamaat` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `idara` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `occupation` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `hifzStatus` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `nationality` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `dob` datetime NULL,
        `bloodGroup` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `kg_faimalydetails_its` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `hofItsId` int(11) NULL,
        `itsId` int(11) NULL,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `age` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `relation` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `jamaat` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `idara` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `occupation` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `hifzStatus` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `nationality` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `dob` datetime NULL,
        `bloodGroup` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `kg_identitycards` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `cardType` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `country` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `nameOnCard` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `cardNumber` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `itsId` int(11) NULL,
        `attachment` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `kg_self_assessment` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NOT NULL,
        `strength` varchar(2000) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `weakness` varchar(2000) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `longTermGoal` varchar(2000) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `roleModel` varchar(2000) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `changeAboutYourself` varchar(2000) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `alternativeCareerPath` varchar(2000) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `personalitytype` varchar(15) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `personalityReport` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `aboutYourSelf` varchar(5000) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=latin1 COLLATE=latin1_swedish_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `kg_venue_worktype` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `venueId` int(11) NULL,
        `workTypeId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `kg_worktype` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `typeName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `description` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `masterdepartment` (
        `masterDeptId` int(11) NOT NULL AUTO_INCREMENT,
        `masterDeptName` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`masterDeptId`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `module` (
        `moduleId` int(11) NOT NULL AUTO_INCREMENT,
        `moduleName` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`moduleId`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `module_rights` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `moduleId` int(11) NULL,
        `rightsId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_bank_transaction` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `bankName` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `credit` int(11) NULL,
        `debit` int(11) NULL,
        `paymentMode` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentId` int(11) NULL,
        `transactionId` int(11) NULL,
        `remarks` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_bill_master` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `billNo` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `billDate` date NULL,
        `billAmount` int(11) NULL,
        `vendorId` int(11) NULL,
        `baseItemId` int(11) NULL,
        `deptVenueId` int(11) NULL,
        `financialYear` int(11) NULL,
        `createdBy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `txn_Id` int(11) NULL,
        `paymentMode_User` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentMode_Admin` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentTo_AccNum` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentTo_AccName` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentTo_BankName` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentTo_ifsc` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentFrom_BankName` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `isWaived` tinyint(1) NULL,
        `packageId` int(11) NULL,
        `gstPercentage` float NULL,
        `gstAmount` int(11) NULL,
        `tdsApplicableAmount` int(11) NULL,
        `tdsPercentage` float NULL,
        `tdsAmount` int(11) NULL,
        `conveyanceAmount` int(11) NULL,
        `isReconciled` tinyint(1) NULL,
        `isFundRequested` tinyint(1) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_bills_package` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `amount` int(11) NULL,
        `description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentDate` date NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_budget_transfer_logs` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `fromDeptVenueId` int(11) NULL,
        `fromBaseItemId` int(11) NULL,
        `toDeptVenueId` int(11) NULL,
        `toBaseItemId` int(11) NULL,
        `amount` int(11) NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `remarks` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_deptvenue_cash_wallet` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `deptVenueId` int(11) NULL,
        `debit` int(11) NULL,
        `credit` int(11) NULL,
        `currency` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentType` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `status` tinyint(1) NULL,
        `note` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdBy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdOn` datetime NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_estimate_student` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `psetId` int(11) NULL,
        `fcId` int(11) NULL,
        `feesAmount` int(11) NULL,
        `studentCountPerMonth` int(11) NULL,
        `financialYear` int(11) NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `duration` int(11) NOT NULL DEFAULT '12',
        `stage` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT 'Initiated',
        `remarks_admin` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `remarks` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_online_payment_users` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `ifsc` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `accNum` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `accName` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `bankName` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_procurement_baseitem` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `status` tinyint(1) NULL,
        `isCapital` tinyint(1) NOT NULL,
        `isIncome` tinyint(1) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_procurement_baseitemmz_expense_procurement_item` (
        `baseItemId` int NOT NULL,
        `itemId` int NOT NULL,
        CONSTRAINT `PK_mz_expense_procurement_baseitemmz_expense_procurement_item` PRIMARY KEY (`baseItemId`, `itemId`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_procurement_item` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `type` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `uom` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_sanctioned_budget` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `deptVenueId` int(11) NULL,
        `baseItemId` int(11) NULL,
        `user_arazAmount` int(11) NULL,
        `admin_arazAmount` int(11) NULL,
        `sanctioned_amount` int(11) NULL,
        `financialYear` int(11) NULL,
        `updatedOn` datetime NULL,
        `updatedBy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_vendor_master` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `phoneNo` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `mobileNo` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `whatsappNo` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `address` varchar(5000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `state` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `city` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `ifscCode` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `bankName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `accountNo` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `accountName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `panCardNo` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `status` tinyint(1) NULL,
        `type` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `email` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `gstNumber` varchar(15) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_vendor_payment` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `vendorId` int(11) NULL,
        `paymentDate` date NULL,
        `debit` int(11) NULL,
        `credit` int(11) NULL,
        `paymentMode` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `currency` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `status` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `note` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `chequeDate` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `transactionId` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_vendor_transaction` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `billId` int(11) NULL,
        `vendorId` int(11) NULL,
        `credit` int(11) NULL,
        `debit` int(11) NULL,
        `currency` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentMode` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentId` int(11) NULL,
        `transactionId` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `remarks` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_vendor_wallet` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `vendorId` int(11) NULL,
        `debit` int(11) NULL,
        `credit` int(11) NULL,
        `currency` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentType` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `status` tinyint(1) NULL,
        `note` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdBy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdOn` datetime NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_fee_collection_center` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `description` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_faculty_loginlogs` (
        `id` bigint(20) NOT NULL AUTO_INCREMENT,
        `date` datetime NULL,
        `itsId` int(11) NULL,
        `ipAddress` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `logoutTime` datetime NULL,
        `deviceDetails` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_loginlogs` (
        `id` bigint(20) NOT NULL AUTO_INCREMENT,
        `date` datetime NULL,
        `itsId` int(11) NULL,
        `ipAddress` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `logoutTime` datetime NULL,
        `deviceDetails` varchar(1000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_kg_wajebaat_araz` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NULL,
        `hijriYear` int(11) NULL,
        `niyyatAmount` int(11) NULL,
        `takhmeenAmount` float NULL,
        `paidAmount` int(11) NULL,
        `currency` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `bankName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `draftNo` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `draftDate` datetime NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `updatedOn` datetime NULL,
        `updatedBy` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `userRemarks` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `officeRemarks` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `displayCurrency` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `currencyRate` float NULL,
        `wajebaatType` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL DEFAULT 'Wajebaat Niyat',
        `stage` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL DEFAULT 'Initialized',
        `verifiedOn` datetime NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci COMMENT='		';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_on_off_modules` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `status` tinyint(1) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_pset_elqgrpid_mapping` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `pSetId` int(11) NULL,
        `elqId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_receipt_payment_mode_rights` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NULL,
        `paymentModeId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_receipt_payment_modes` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `description` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_student` (
        `mz_id` int(11) NOT NULL AUTO_INCREMENT,
        `itsID` int(11) NOT NULL,
        `nameEng` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `nameArabic` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `gender` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `bloodGroup` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `studentEmail` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `studentMobile` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `studentWhatsapp` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `dobGregorian` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `dobHijri` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `age` int(11) NULL,
        `fatherEmail` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `fatherMobile` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `fatherWhatsapp` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `motherEmail` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `motherMobile` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `motherWhatsapp` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `address` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `jamaat` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `jamiat` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `vatan` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `maqaam` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `nationality` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `activeStatus` tinyint(1) NULL,
        `psetId` int(11) NULL,
        `photoPath` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `photoBase64` varchar(10000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `fcId` int(11) NULL,
        `elq_GroupId` int(11) NULL,
        `elq_BranchName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `trNo` int(11) NULL,
        `classId` int(11) NULL,
        `hifzSanadYear` int(11) NULL,
        `dq_fasal` int(11) NULL,
        `hifzStatus` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `idara` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`mz_id`),
        CONSTRAINT `AK_mz_student_itsID` UNIQUE (`itsID`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_student_ewallet` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `studentId` int(11) NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `debit` int(11) NULL,
        `credit` int(11) NULL,
        `paymentType` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `status` tinyint(1) NULL,
        `note` varchar(5000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `currency` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_student_fee_allotment` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `studentId` int(11) NULL,
        `pSetId` int(11) NULL,
        `createdBy` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `feeAlloted` int(11) NULL,
        `fcId` int(11) NULL,
        `reason` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `remarks` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `monthId` int(11) NULL,
        `hijriYear` int(11) NULL,
        `currency` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `txn_Id` int(11) NULL,
        `waiveStatus` tinyint(1) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_student_fee_excluding_list` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `studentMzId` int(11) NULL,
        `monthId` int(11) NULL,
        `hijriYear` int(11) NULL,
        `pSetId` int(11) NULL,
        `createdBy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdOn` datetime NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_student_fee_transaction` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `studentId` int(11) NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `currency` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `debit` int(11) NULL,
        `credit` int(11) NULL,
        `paymentMode` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `remarks` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `psetId` int(11) NULL,
        `recieptId` int(11) NULL,
        `allotmentId` int(11) NULL,
        `transactionId` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `collection_center_no` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_student_feecategory` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `categoryName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_student_feecategory_pset` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `fcId` int(11) NULL,
        `psetId` int(11) NULL,
        `amount` int(11) NULL,
        `currency` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_student_receipt` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `studentId` int(11) NULL,
        `recieptNumber` int(11) NULL,
        `recieptDate` date NULL,
        `collectionCenter` int(11) NULL,
        `paymentMode` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `currency` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `amount` int(11) NULL,
        `status` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `createdBy` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `psetId` int(11) NULL,
        `transactionId` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `bankName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `account` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `note` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `chequeDate` date NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `nisaab_classes` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `std` int(11) NULL,
        `div` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `gender` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `className` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `showTimeTable` tinyint(1) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `nisaab_classes_monitor` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `monitorItsId` int(11) NULL,
        `classId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `nisaab_jadwal` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `periodId` int(11) NULL,
        `classId` int(11) NULL,
        `dayId` int(11) NULL,
        `subjectId` int(11) NULL,
        `teacherId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `nisaab_periods` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `periodName` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `timing` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `nisaabtalabat_results` (
        `id` int NOT NULL AUTO_INCREMENT,
        `itsId` int NULL,
        `resultYear` int NULL,
        `classId` int NULL,
        `darajah` int NULL,
        `rank` int NULL,
        `alShafahi` int NULL,
        `alMaqalaInsha` int NULL,
        `alTaqyim` int NULL,
        `alFiqh` int NULL,
        `alAdab` int NULL,
        `alTadreebHikam` int NULL,
        `alUlumKauniyya` int NULL,
        `fuyuzHidayat` int NULL,
        `alIkhtibaar` int NULL,
        `alHifz` int NULL,
        `alAkhbar` int NULL,
        `alTamrinTaweel` int NULL,
        `english` int NULL,
        `gTotal` int NULL,
        `percentage` float NULL,
        `grade` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `TadbeeralManzeli` int NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `password_reset_requests` (
        `ID` int(11) NOT NULL AUTO_INCREMENT,
        `UserID` int(11) NULL,
        `UniqueKey` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `ExpiryTime` datetime NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`ID`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `payroll_salary_packages` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `amount` int(11) NOT NULL,
        `description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentDate` datetime NULL,
        `paymentFrom` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `personality_type_details` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `type` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `strength` varchar(5000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `weakness` varchar(5000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `platform_button` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `platform_moduleplatform_role` (
        `moduleId` int NOT NULL,
        `roleId` int NOT NULL,
        CONSTRAINT `PK_platform_moduleplatform_role` PRIMARY KEY (`moduleId`, `roleId`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `platform_page` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `pageName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `description` varchar(75) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `link` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `icon` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `platform_role` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `description` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `isDefault` tinyint(4) NOT NULL,
        `icon` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `link` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `platform_roleplatform_role` (
        `mainRole` int NOT NULL,
        `subRole` int NOT NULL,
        CONSTRAINT `PK_platform_roleplatform_role` PRIMARY KEY (`mainRole`, `subRole`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `printableformat_report` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `description` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `fileName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `reportName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `printableformat_report_rights` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `reportId` int(11) NULL,
        `itsId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `qism_al_tahfeez_role` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `description` varchar(500) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `qismId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=latin1 COLLATE=latin1_swedish_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `qism_al_tahfeez_role_module` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `roleId` int(11) NOT NULL,
        `moduleId` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=latin1 COLLATE=latin1_swedish_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `qism_al_tahfeez_role_right` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `roleId` int(11) NOT NULL,
        `rightId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=latin1 COLLATE=latin1_swedish_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `qism_al_tahfeez_user` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `qismId` int(11) NULL,
        `userId` int(11) NULL,
        `roleId` int(11) NULL,
        `isAdmin` tinyint(1) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=latin1 COLLATE=latin1_swedish_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `qism_al_tahfeez_api_logs` (
        `id` bigint(20) NOT NULL AUTO_INCREMENT,
        `date` datetime NULL,
        `apiString` varchar(1000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `logId` bigint(20) NULL,
        `loginName` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `ipAddress` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `deviceDetails` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `referrer` varchar(1000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `apiWithParameter` varchar(1000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `httpRequestType` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `qism_al_tahfeez_login_logs` (
        `id` bigint(20) NOT NULL AUTO_INCREMENT,
        `date` datetime NULL,
        `itsId` int(11) NULL,
        `ipAddress` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `email` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `logoutTime` datetime NULL,
        `deviceDetails` varchar(1000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `qismal_tahfeez_student` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `studentName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `studentItsId` int(11) NULL,
        `mobile1` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `mobile2` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `emailId` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `dob` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `hdob` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `age` int(11) NULL,
        `gender` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `bloodGroup` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `nationality` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `jamaat` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `activeStatus` tinyint(1) NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `updatedOn` datetime NULL,
        `deptVenueId` int(11) NULL,
        `programId` int(11) NULL,
        `feeStatus` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `category` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `inActive_Reason` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `registrationform_programs` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `registrationform_subprograms` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `rights` (
        `rightsId` int(11) NOT NULL AUTO_INCREMENT,
        `rightsName` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`rightsId`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `role` (
        `roleId` int(11) NOT NULL AUTO_INCREMENT,
        `roleName` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `description` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`roleId`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `role_module` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `roleId` int(11) NULL,
        `moduleId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `role_rights` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `roleId` int(11) NULL,
        `rightsId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `salary_querylogs` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `hijriMonth` int(11) NULL,
        `hijriYear` int(11) NULL,
        `type` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `remarks` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `fromDate` date NULL,
        `toDate` date NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `salary_type` (
        `id` int(11) NOT NULL,
        `Name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `training_subject` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT 'Not Active',
        `outOf` int(11) NOT NULL,
        `qustionare` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `accademicYear` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `user` (
        `Id` int(11) NOT NULL AUTO_INCREMENT,
        `Username` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `ItsId` int(11) NOT NULL,
        `Password` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `Accesslevel` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `DID` int(11) NOT NULL,
        `EmailId` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `Mobile` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `roleId` int(11) NULL,
        `loginStatus` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `user_dept-venue_baseitem` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NULL,
        `dept_venueId` int(11) NULL,
        `baseItemId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `user_deptvenue` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NULL,
        `deptVenueId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `userdeptassociation` (
        `Id` bigint(20) NOT NULL AUTO_INCREMENT,
        `DID` int(11) NOT NULL,
        `Idara` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `Department` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `DisplayName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `DepartmentCode` varchar(50) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`Id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `useritemassociation` (
        `Id` bigint(20) NOT NULL AUTO_INCREMENT,
        `UserId` int(11) NOT NULL,
        `DID` int(11) NOT NULL,
        `BaseItemId` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`Id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafd_fieldofinterest` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NULL,
        `fieldofInterest` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `selfRanking` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafd_languageproficiency` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NULL,
        `language` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `selfRanking` int(11) NULL,
        `speaking` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `reading` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `writing` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `listening` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafd_mahad_past_mawaze` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsIs` int(11) NULL,
        `fromYear` int(11) NULL,
        `toYear` int(11) NULL,
        `mauze` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `program` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafd_otheridara_mawaze` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NULL,
        `fromYear` int(11) NULL,
        `toYear` int(11) NULL,
        `mauze` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `khidmatNature` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafd_physicalfitness` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NULL,
        `sports` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `selfRanking` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_dropdown_authoredcategory` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_dropdown_category` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(2000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_dropdown_degree` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(2000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_dropdown_fieldofinterest` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(2000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_dropdown_mode` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_dropdown_titlecategory` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_dropdown_workshopcategory` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_english_assessment` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NULL,
        `verificationCode` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `examLink` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `userName` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `password` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_qualification_new` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsid` int(11) NULL,
        `country` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `mediumOfEducation` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `stage` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `degree` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `institutionName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `status` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `pursuingYear` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `year` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `attachment` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_qualification_stage_degree` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `stage` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `degree` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_workshop_data` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NULL,
        `subCategory` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `cetificateCredentials` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `keypoints` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `courseName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `category` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `mode` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `course` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `type` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `year` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `attachment` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `totalDays` int(11) NULL,
        `hoursPerDay` int(11) NULL,
        `totalHours` int(11) NULL,
        `completionDate` date NULL,
        `academicYear` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_workshops_category_subcategory` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `category` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `subCategory` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `yellowreceipt` (
        `Id` bigint(20) NOT NULL AUTO_INCREMENT,
        `ItsId` int(11) NOT NULL,
        `Name` varchar(1000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `Amount` int(11) NOT NULL,
        `CreatedBy` varchar(1000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `CreatedOn` datetime NOT NULL,
        `PaymentMode` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `BankName` varchar(1000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `ChequeNo` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `PaidAt` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `Account` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `FinancialYear` int(11) NOT NULL,
        `Remarks` varchar(1000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `EntryId` int(11) NULL,
        `purpose` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `cancelDate` date NULL,
        `status` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL DEFAULT 'Paid',
        `paymentDate` date NULL,
        `whatsappNo` varchar(20) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `email` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`Id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `dropdown_dataset_options` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `headerId` int(11) NOT NULL,
        `name` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_options_header_id` FOREIGN KEY (`headerId`) REFERENCES `dropdown_dataset_header` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `form_questionnaire` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `formId` int(11) NOT NULL,
        `question` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `type` varchar(25) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `isRequired` tinyint(1) NOT NULL,
        `description` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_questionnaire_form` FOREIGN KEY (`formId`) REFERENCES `form` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `form_response` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `formId` int(11) NOT NULL,
        `response` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `identifier` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_form_response` FOREIGN KEY (`formId`) REFERENCES `form` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `ikhtibaar_marksheet` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `ikhtibaarId` int(11) NOT NULL,
        `itsId` int(11) NOT NULL,
        `marks` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `totalMarks` float NULL,
        `remarks` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `type` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `attempts` int(11) NOT NULL,
        `hasAttempted` tinyint(4) NOT NULL,
        `mukhtabir` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `category` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_ikhtibaar_marksheet` FOREIGN KEY (`ikhtibaarId`) REFERENCES `ikhtibaar` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_unicode_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `ikhtibaar_questionnaire` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `ikhtibaarId` int(11) NOT NULL,
        `question` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `weightage` int(11) NOT NULL DEFAULT '1',
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_questionnaire_ikhtebaar` FOREIGN KEY (`ikhtibaarId`) REFERENCES `ikhtibaar` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_bill_logs` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `billId` int(11) NOT NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_bill_logs_bill_master` FOREIGN KEY (`billId`) REFERENCES `mz_expense_bill_master` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_student_budget_issue_logs` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `remark` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdBy` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `estimateStudentId` int(11) NOT NULL,
        `isConcerning` tinyint(1) NOT NULL DEFAULT '1',
        `arazState` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_student_budget_issue_araz` FOREIGN KEY (`estimateStudentId`) REFERENCES `mz_expense_estimate_student` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_bill_item` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `billId` int(11) NOT NULL,
        `itemId` int(11) NOT NULL,
        `quantity` float NULL,
        `amountPerPc` float NULL,
        `remarks` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `gstPercentage` float NULL,
        `gstAmount` float NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_bill_item_bill_item_master` FOREIGN KEY (`itemId`) REFERENCES `mz_expense_procurement_item` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_bill_item_bill_master` FOREIGN KEY (`billId`) REFERENCES `mz_expense_bill_master` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_procurement_item_baseitem` (
        `itemId` int(11) NOT NULL,
        `baseItemId` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`itemId`, `baseItemId`),
        CONSTRAINT `fk_baseitem_item` FOREIGN KEY (`itemId`) REFERENCES `mz_expense_procurement_item` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_item_baseitem` FOREIGN KEY (`baseItemId`) REFERENCES `mz_expense_procurement_baseitem` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_off_module_exception` (
        `moduleId` int(11) NOT NULL,
        `itsId` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`moduleId`, `itsId`),
        CONSTRAINT `fk_module_on_off_id` FOREIGN KEY (`moduleId`) REFERENCES `mz_on_off_modules` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `platform_module` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `pageId` int(11) NOT NULL,
        `buttonId` int(11) NOT NULL,
        `isDefault` tinyint(4) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_platform_module_button` FOREIGN KEY (`buttonId`) REFERENCES `platform_button` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_platform_module_page` FOREIGN KEY (`pageId`) REFERENCES `platform_page` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `platform_menu_map` (
        `mainRole` int(11) NOT NULL,
        `subRole` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`mainRole`, `subRole`),
        CONSTRAINT `fk_main_platform_role` FOREIGN KEY (`mainRole`) REFERENCES `platform_role` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_sub_platform_role` FOREIGN KEY (`subRole`) REFERENCES `platform_role` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `platform_role_module` (
        `roleId` int(11) NOT NULL,
        `moduleId` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`roleId`, `moduleId`),
        CONSTRAINT `fk_paltform_role_module_r` FOREIGN KEY (`roleId`) REFERENCES `platform_role` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_platform_role_module_mid` FOREIGN KEY (`moduleId`) REFERENCES `platform_module` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `branch_user` (
        `itsId` int(11) NOT NULL,
        `password` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `emailId` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `lastLoggedIn` datetime NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`itsId`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `qism_al_tahfeez` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `itsId` int(11) NULL,
        `emailId` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `password` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `isActive` tinyint(1) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_qism_al_tahfeez_branch_user` FOREIGN KEY (`itsId`) REFERENCES `branch_user` (`itsId`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `platform_user_module` (
        `userId` int(11) NOT NULL,
        `moduleId` int(11) NOT NULL,
        `qismId` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`userId`, `moduleId`, `qismId`),
        CONSTRAINT `fk_paltform_user_module_kg` FOREIGN KEY (`userId`) REFERENCES `branch_user` (`itsId`) ON DELETE CASCADE,
        CONSTRAINT `fk_platform_user_module_mid` FOREIGN KEY (`moduleId`) REFERENCES `platform_module` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_platform_user_module_qism` FOREIGN KEY (`qismId`) REFERENCES `qism_al_tahfeez` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `platform_user_role` (
        `userId` int(11) NOT NULL,
        `roleId` int(11) NOT NULL,
        `qismId` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`userId`, `roleId`, `qismId`),
        CONSTRAINT `fk_paltform_user_role_kg` FOREIGN KEY (`userId`) REFERENCES `branch_user` (`itsId`) ON DELETE CASCADE,
        CONSTRAINT `fk_platform_user_role_mid` FOREIGN KEY (`roleId`) REFERENCES `platform_role` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_platform_user_role_qism` FOREIGN KEY (`qismId`) REFERENCES `qism_al_tahfeez` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `venue` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CampVenue` varchar(1000) NOT NULL,
        `CampId` varchar(100) NOT NULL,
        `ActiveStatus` tinyint(1) NOT NULL,
        `CashBalance` int NOT NULL,
        `currency` varchar(200) NOT NULL,
        `qismTahfeez` varchar(500) NOT NULL,
        `ho` varchar(500) NOT NULL,
        `displayName` varchar(500) NOT NULL,
        `qismId` int NULL,
        CONSTRAINT `PK_venue` PRIMARY KEY (`Id`),
        CONSTRAINT `fk_venue_qism` FOREIGN KEY (`qismId`) REFERENCES `qism_al_tahfeez` (`id`) ON DELETE SET NULL
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `dept_venue` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `masterDeptId` int(11) NOT NULL,
        `deptId` int(11) NOT NULL,
        `venueId` int(11) NOT NULL,
        `masterDeptName` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `deptName` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `venueName` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `status` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `tag` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `qismId` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_dept_venue_deptid` FOREIGN KEY (`deptId`) REFERENCES `department` (`deptId`) ON DELETE CASCADE,
        CONSTRAINT `fk_dept_venue_masterdid` FOREIGN KEY (`masterDeptId`) REFERENCES `masterdepartment` (`masterDeptId`) ON DELETE CASCADE,
        CONSTRAINT `fk_dept_venue_qism_al_tahfeez` FOREIGN KEY (`qismId`) REFERENCES `qism_al_tahfeez` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_dept_venue_venue` FOREIGN KEY (`venueId`) REFERENCES `venue` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `khidmat_guzaar` (
        `itsId` int(11) NOT NULL,
        `id` int(11) NOT NULL,
        `photo` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `krNo` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `fullName` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `fullNameArabic` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `c_codeMobile` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `mobileNo` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `c_codeWhatsapp` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `whatsappNo` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `emailAddress` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `officialEmailAddress` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `watan` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `watanArabic` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `watanAdress` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `muqam` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `muqamArabic` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `dojGregorian` datetime NULL,
        `dojHijri` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `dobGregorian` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `dobHijri` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `bloodGroup` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `currentAddress` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `jaman` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `maritalStatus` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `mafsuhiyatYear` int(11) NULL,
        `activeStatus` tinyint(1) NULL,
        `nationality` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `its_idaras` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `its_preferredIdara` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `mz_idara` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `dawat_title` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `jamaat` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `jamiat` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `age` int(11) NULL,
        `panCardNo` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `haddiyatYear` int(11) NULL,
        `domicileParent` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `domicileAddressParents` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `personalHouseStatus` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `personalHouseType` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `personalHouseArea` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `personalHouseAddress` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `photoBase64` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `workTypeId` int(11) NULL,
        `employeeType` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `RfId` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `EduQualification` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `CreatedOn` datetime NULL,
        `UpdatedOn` datetime NULL,
        `CreatedBy` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `MzId` int(11) NULL,
        `OthDegree` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `EduCompletion` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `isMumin` tinyint(1) NULL,
        `designation` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `subDesignation` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `salaryCode` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `mauze` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`itsId`),
        CONSTRAINT `fk_venue_khidmatguzaar` FOREIGN KEY (`mauze`) REFERENCES `venue` (`Id`) ON DELETE SET NULL
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci COMMENT='		';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `dept_venue_baseitem` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `deptVenueId` int(11) NOT NULL,
        `baseItemId` int(11) NOT NULL,
        `tag` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `hasItemBlock` tinyint(4) NOT NULL DEFAULT '1',
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_dept_venue_baseitem_dept_venue` FOREIGN KEY (`deptVenueId`) REFERENCES `dept_venue` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_deptvenue_procurement_baseitem` FOREIGN KEY (`baseItemId`) REFERENCES `mz_expense_procurement_baseitem` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `holiday_allocation` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `holidayId` int(11) NOT NULL,
        `deptVenueId` int(11) NOT NULL,
        `employeeType` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_holiday_allocation_calender` FOREIGN KEY (`holidayId`) REFERENCES `hijri_calender` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_holiday_allocation_deptvenue` FOREIGN KEY (`deptVenueId`) REFERENCES `dept_venue` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_budget_araz` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `deptVenueId` int(11) NOT NULL,
        `baseItemId` int(11) NOT NULL,
        `itemId` int(11) NOT NULL,
        `amountPerUom` int(11) NOT NULL DEFAULT '1',
        `quantity` int(11) NOT NULL DEFAULT '1',
        `uom` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `justification` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `remarks_admin` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `updatedOn` datetime NULL,
        `updatedBy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `financialYear` int(11) NOT NULL,
        `stage` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT 'Initiated',
        `consumedAmount` float NULL,
        `consumedQty` float NULL,
        `transferedAmount` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_budget_araz_baseItem` FOREIGN KEY (`baseItemId`) REFERENCES `mz_expense_procurement_baseitem` (`id`),
        CONSTRAINT `fk_budget_araz_deptvenue` FOREIGN KEY (`deptVenueId`) REFERENCES `dept_venue` (`id`),
        CONSTRAINT `fk_budget_araz_items` FOREIGN KEY (`itemId`) REFERENCES `mz_expense_procurement_item` (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `qism_al_tahfeez_user_deptvenue` (
        `userId` int(11) NOT NULL,
        `deptVenueId` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`userId`, `deptVenueId`),
        CONSTRAINT `fk_branch_user_qism_deptvenue` FOREIGN KEY (`userId`) REFERENCES `branch_user` (`itsId`) ON DELETE CASCADE,
        CONSTRAINT `fk_deptvenue_qism_user` FOREIGN KEY (`deptVenueId`) REFERENCES `dept_venue` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=latin1 COLLATE=latin1_swedish_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `registrationform_dropdown_set` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `programId` int(11) NOT NULL,
        `venueId` int(11) NOT NULL,
        `subprogramId` int(11) NOT NULL,
        `deptVenueId` int(11) NOT NULL,
        `activeStatus` tinyint(1) NULL,
        `qismId` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_pset_dept_venue` FOREIGN KEY (`deptVenueId`) REFERENCES `dept_venue` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_pset_program` FOREIGN KEY (`programId`) REFERENCES `registrationform_programs` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_pset_qism_al_tahfeez` FOREIGN KEY (`qismId`) REFERENCES `qism_al_tahfeez` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_pset_sub_program` FOREIGN KEY (`subprogramId`) REFERENCES `registrationform_subprograms` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_pset_venue` FOREIGN KEY (`venueId`) REFERENCES `venue` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `employee_academic_details` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NOT NULL,
        `trNo` int(11) NULL,
        `farigDarajah` int(11) NULL,
        `farigYear` int(11) NULL,
        `aljameaDegree` varchar(50) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `hifzSanadYear` int(11) NULL,
        `wafdTrainingMasoolIts` int(11) NULL,
        `wafdTrainingMushrifIts` int(11) NULL,
        `maqaraatTeacherIts` int(11) NULL,
        `wafdClassId` int(11) NULL,
        `category` varchar(45) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `batchId` int(11) NULL,
        `hifzStatus` varchar(45) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `itsId` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=latin1 COLLATE=latin1_swedish_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `employee_bank_details` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NOT NULL,
        `bankName` varchar(500) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `bankAccountNumber` varchar(500) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `bankAccountName` varchar(500) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `ifsc` varchar(500) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `bankBranch` varchar(500) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `bankAccountType` varchar(500) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `chequeAttachment` varchar(500) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `employeeIts` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=latin1 COLLATE=latin1_swedish_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `employee_dept_salary` (
        `itsId` int(11) NOT NULL,
        `deptVenueId` int(11) NOT NULL,
        `salaryTypeId` int(11) NOT NULL,
        `srno` int(11) NULL,
        `workingMin` int(11) NULL,
        `hasSalary` tinyint(1) NULL,
        `salaryAmount` float NULL,
        `isHijriSalary` tinyint(1) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`itsId`, `deptVenueId`, `salaryTypeId`),
        CONSTRAINT `deptVenueSalaryId` FOREIGN KEY (`deptVenueId`) REFERENCES `dept_venue` (`id`) ON DELETE CASCADE,
        CONSTRAINT `empSalaryItsId` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE,
        CONSTRAINT `salaryTypeId` FOREIGN KEY (`salaryTypeId`) REFERENCES `salary_type` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `employee_e_attendence` (
        `itsId` int(11) NOT NULL,
        `date` date NOT NULL,
        `entryMorning` datetime NULL,
        `exitMorning` datetime NULL,
        `entryEvening` datetime NULL,
        `exitEvening` datetime NULL,
        `extraHour` int(11) NULL,
        `logJson` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`itsId`, `date`),
        CONSTRAINT `fk_employee_attendence` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `employee_family_details` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NOT NULL,
        `FatherName` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `FatherIts` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `MotherName` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `MotherIts` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `SpouseName` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `SpouseIts` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `empId` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=latin1 COLLATE=latin1_swedish_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `employee_khidmat_details` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NOT NULL,
        `mahad_khidmatYear` int(11) NULL,
        `khidmatYear` int(11) NULL,
        `khidmatMauzeHouseStatus` varchar(500) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `khdimatMauzeHouseType` varchar(500) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `tayeenYear` int(11) NULL,
        `tayeenMonth` int(11) NULL,
        `khidmatMonth` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `khidmatguzarIts` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=latin1 COLLATE=latin1_swedish_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `employee_logs` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NOT NULL,
        `updatedby` int(11) NOT NULL,
        `updatedon` datetime NOT NULL,
        `status` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `targetId` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=latin1 COLLATE=latin1_swedish_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `employee_passport_details` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NOT NULL,
        `passportName` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `passportNo` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `dateOfIssue` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `dateOfExpiry` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `placeOfIssue` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `passportPlaceOfBirth` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `dobPassport` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        `passportCopy` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `citizenIts` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=latin1 COLLATE=latin1_swedish_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `employee_salary` (
        `itsId` int(11) NOT NULL,
        `grossSalary` int(11) NOT NULL,
        `rentAllowance` int(11) NULL,
        `marriageAllowance` int(11) NULL,
        `mumbaiAllowance` int(11) NULL,
        `conveyanceAllowance` int(11) NULL,
        `extraAllowance` int(11) NULL,
        `professionTax` int(11) NULL,
        `tds` int(11) NULL,
        `qardanHasanah` int(11) NULL,
        `marafiqKhairiyah` int(11) NULL,
        `sabeel` int(11) NULL,
        `bqhs` int(11) NULL,
        `isHijriAllowence` tinyint(1) NULL,
        `lessDeduction` int(11) NULL,
        `installmentDeduction_Qardan` int(11) NULL,
        `husainiQardanHasanah` int(11) NULL,
        `isHusainiQardan` tinyint(1) NULL,
        `mohammediQardanHasanah` int(11) NULL,
        `isMahadSalary` tinyint(1) NOT NULL,
        `fmbAllowance` int(11) NULL,
        `fmbDeduction` int(11) NULL,
        `currency` varchar(10) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`itsId`),
        CONSTRAINT `empSalary` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=latin1 COLLATE=latin1_swedish_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_budget_smart_goals` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `deptVenueId` int(11) NOT NULL,
        `itsId` int(11) NOT NULL,
        `category` varchar(75) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `specific` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `measearable` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `attainable` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `relevant` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `timeStart` datetime NULL,
        `timeEnd` datetime NULL,
        `createdOn` datetime NULL,
        `remarks_admin` varchar(5000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `updatedBy` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `updatedOn` datetime NULL,
        `financialYear` int(11) NOT NULL,
        `stage` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT 'Initiated',
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_budget_smart_deptvenue` FOREIGN KEY (`deptVenueId`) REFERENCES `dept_venue` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_budget_smart_kg` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `nisaab_alumni` (
        `itsId` int(11) NOT NULL,
        `jamea` tinyint(1) NULL,
        `degree` varchar(75) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `farigYear` int(11) NULL,
        `farigDarajah` int(11) NULL,
        `batchId` int(11) NULL,
        `hafizAtFarig` tinyint(1) NOT NULL,
        `kgIts` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`itsId`),
        CONSTRAINT `fk_alumni_mz_kg` FOREIGN KEY (`kgIts`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE RESTRICT,
        CONSTRAINT `fk_alumni_mz_student` FOREIGN KEY (`itsId`) REFERENCES `mz_student` (`itsID`) ON DELETE RESTRICT
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `salary_allocation_gegorian` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NOT NULL,
        `salary` int(11) NOT NULL,
        `rentAllowance` int(11) NULL,
        `marriageAllowance` int(11) NULL,
        `convenienceAllowance` int(11) NULL,
        `mumbaiAllowance` int(11) NULL,
        `fmbAllowance` int(11) NULL,
        `lessDeduction` int(11) NULL,
        `extraAllowance` int(11) NULL,
        `ctc` int(11) NOT NULL,
        `professionTax` int(11) NULL,
        `tds` int(11) NULL,
        `qardanHasanah` int(11) NULL,
        `sabeel` int(11) NULL,
        `marafiqKhairiyah` int(11) NULL,
        `currency` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT 'INR',
        `fmbDeduction` int(11) NULL,
        `bqhs` int(11) NULL,
        `mohammedi_qardanHasanah` int(11) NULL,
        `husaini_qardanHasanah` int(11) NULL,
        `installmentDeduction_Qardan` int(11) NULL,
        `netEarnings` int(11) NOT NULL,
        `month` int(11) NOT NULL,
        `year` int(11) NOT NULL,
        `createdOn` datetime NOT NULL,
        `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentDate` datetime NULL,
        `packageId` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_salary_allocation_g_exp_package` FOREIGN KEY (`packageId`) REFERENCES `payroll_salary_packages` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_salary_allocation_g_kh` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `salary_allocation_hijri` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NOT NULL,
        `salary` int(11) NOT NULL,
        `rentAllowance` int(11) NULL,
        `marriageAllowance` int(11) NULL,
        `convenienceAllowance` int(11) NULL,
        `mumbaiAllowance` int(11) NULL,
        `fmbAllowance` int(11) NULL,
        `lessDeduction` int(11) NULL,
        `extraAllowance` int(11) NULL,
        `ctc` int(11) NOT NULL,
        `professionTax` int(11) NULL,
        `tds` int(11) NULL,
        `qardanHasanah` int(11) NULL,
        `sabeel` int(11) NULL,
        `marafiqKhairiyah` int(11) NULL,
        `currency` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT 'INR',
        `fmbDeduction` int(11) NULL,
        `bqhs` int(11) NULL,
        `mohammedi_qardanHasanah` int(11) NULL,
        `husaini_qardanHasanah` int(11) NULL,
        `installmentDeduction_Qardan` int(11) NULL,
        `netEarnings` int(11) NOT NULL,
        `month` int(11) NOT NULL,
        `year` int(11) NOT NULL,
        `createdOn` datetime NOT NULL,
        `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `paymentDate` datetime NULL,
        `packageId` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_salary_allocation_exp_package` FOREIGN KEY (`packageId`) REFERENCES `payroll_salary_packages` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_salary_allocation_h_kh` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `training_class` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `className` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `masoolIts` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_class_masool_kg` FOREIGN KEY (`masoolIts`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_maqaraat_session` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `teacherItsId` int(11) NOT NULL,
        `dayId` int(11) NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `isEvaluated` tinyint(1) NULL,
        `reason` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `juz` int(11) NULL,
        `acedemicYear` int(11) NULL,
        `pages` int(11) NULL,
        `sessionDate` date NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_maqaraat_session_kg` FOREIGN KEY (`teacherItsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_maqaraat_teacher` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NOT NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `days` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_maqaraat_teacher_kg` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_budget_araz_transfer_logs` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `fromArazId` int(11) NOT NULL,
        `toArazId` int(11) NOT NULL,
        `amount` int(11) NOT NULL,
        `createdOn` datetime NOT NULL,
        `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `remarks` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `trabferModel` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_budget_araz_from` FOREIGN KEY (`fromArazId`) REFERENCES `mz_expense_budget_araz` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_budget_araz_to` FOREIGN KEY (`toArazId`) REFERENCES `mz_expense_budget_araz` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_budget_issue_logs` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `remark` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdBy` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `budgetArazId` int(11) NOT NULL,
        `isConcerning` tinyint(1) NOT NULL DEFAULT '1',
        `arazState` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_budget_issue_araz` FOREIGN KEY (`budgetArazId`) REFERENCES `mz_expense_budget_araz` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `qism_al_tahfeez_user_pset` (
        `userId` int(11) NOT NULL,
        `psetId` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`userId`, `psetId`),
        CONSTRAINT `fk_branch_user_pset_b` FOREIGN KEY (`userId`) REFERENCES `branch_user` (`itsId`) ON DELETE CASCADE,
        CONSTRAINT `fk_branch_user_pset_p` FOREIGN KEY (`psetId`) REFERENCES `registrationform_dropdown_set` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=latin1 COLLATE=latin1_swedish_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `mz_expense_budget_smart_issue_logs` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `remark` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdBy` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdOn` datetime NULL,
        `smartGoalId` int(11) NOT NULL,
        `isConcerning` tinyint(1) NOT NULL DEFAULT '1',
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_budget_smart_issue_araz` FOREIGN KEY (`smartGoalId`) REFERENCES `mz_expense_budget_smart_goals` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `salary_generation_gegorgian` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NOT NULL,
        `quantity` int(11) NOT NULL,
        `netSalary` int(11) NULL,
        `month` int(11) NOT NULL,
        `year` int(11) NOT NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `deptVenueId` int(11) NOT NULL,
        `allocationId` int(11) NOT NULL,
        `salaryType` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `empengsalarygeneration` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE,
        CONSTRAINT `fk_salary_gen_g_salary_type` FOREIGN KEY (`salaryType`) REFERENCES `salary_type` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_salary_gen_gegorian_allocation` FOREIGN KEY (`allocationId`) REFERENCES `salary_allocation_gegorian` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_salary_gen_gegorian_dept` FOREIGN KEY (`deptVenueId`) REFERENCES `dept_venue` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `salary_generation_hijri` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NOT NULL,
        `quantity` int(11) NOT NULL,
        `netSalary` int(11) NULL,
        `month` int(11) NOT NULL,
        `year` int(11) NOT NULL,
        `createdOn` datetime NULL,
        `createdBy` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        `deptVenueId` int(11) NOT NULL,
        `allocationId` int(11) NOT NULL,
        `salaryType` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `empsalarygeneration` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE,
        CONSTRAINT `fk_salary_gen_h_salary_type` FOREIGN KEY (`salaryType`) REFERENCES `salary_type` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_salary_gen_hijri_allocation` FOREIGN KEY (`allocationId`) REFERENCES `salary_allocation_hijri` (`id`) ON DELETE CASCADE,
        CONSTRAINT `sdv` FOREIGN KEY (`deptVenueId`) REFERENCES `dept_venue` (`id`)
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `training_class_student` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `classId` int(11) NOT NULL,
        `studentITS` int(11) NOT NULL,
        `rank` int(11) NULL,
        `prevRank` int(11) NULL,
        `mauze` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `academicYear` int(11) NOT NULL,
        `marks` int(11) NULL,
        `percentage` int(11) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_class_student_kg_student` FOREIGN KEY (`studentITS`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE,
        CONSTRAINT `fk_class_student_training_class` FOREIGN KEY (`classId`) REFERENCES `training_class` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `training_class_subject_teacher` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `classId` int(11) NOT NULL,
        `subjectId` int(11) NOT NULL,
        `teacherITS` int(11) NOT NULL,
        `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT 'Not Active',
        `acedemicYear` int(11) NULL,
        `createdOn` datetime NOT NULL,
        `updatedOn` datetime NULL,
        `createdBy` int(11) NOT NULL,
        `startDate` date NOT NULL,
        `endDate` date NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_cst_class` FOREIGN KEY (`classId`) REFERENCES `training_class` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_cst_subject` FOREIGN KEY (`subjectId`) REFERENCES `training_subject` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_cst_teacher` FOREIGN KEY (`teacherITS`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `wafdprofile_maqaraat_data` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `sessionId` int(11) NOT NULL,
        `studentItsId` int(11) NOT NULL,
        `marks` int(11) NULL,
        `isPresent` tinyint(1) NULL,
        `absentReason` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_maqaarat_data_kg` FOREIGN KEY (`studentItsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE,
        CONSTRAINT `fk_maqaraat_data_maqaraat_session` FOREIGN KEY (`sessionId`) REFERENCES `wafdprofile_maqaraat_session` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=cp1256 COLLATE=cp1256_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE TABLE `training_student_subject_marksheet` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `cstId` int(11) NOT NULL,
        `studentITS` int(11) NOT NULL,
        `answers` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT 'Not Atempted',
        `marks` int(11) NULL,
        `acedemicYear` int(11) NOT NULL,
        `gradedBy` int(11) NOT NULL,
        `startDate` date NOT NULL,
        `endDate` date NOT NULL,
        `remarks` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_student_marksheet_kg` FOREIGN KEY (`gradedBy`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE RESTRICT,
        CONSTRAINT `fk_student_marksheet_student` FOREIGN KEY (`studentITS`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE RESTRICT,
        CONSTRAINT `fk_student_marksheet_subject_cst` FOREIGN KEY (`cstId`) REFERENCES `training_class_subject_teacher` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `emailId_UNIQUE` ON `branch_user` (`emailId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_dept_venue_deptid_idx` ON `dept_venue` (`deptId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_dept_venue_masterdid_idx` ON `dept_venue` (`masterDeptId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_dept_venue_qism_al_tahfeez_idx` ON `dept_venue` (`qismId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_dept_venue_venue_idx` ON `dept_venue` (`venueId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_dept_venue_baseitem_dept_venue` ON `dept_venue_baseitem` (`deptVenueId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_deptvenue_procurement_baseitem_idx` ON `dept_venue_baseitem` (`baseItemId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `id_UNIQUE` ON `dropdown_dataset_header` (`id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_options_header_id_idx` ON `dropdown_dataset_options` (`headerId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `id_UNIQUE1` ON `dropdown_dataset_options` (`id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `itsId_idx` ON `employee_academic_details` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `employeeIts_idx` ON `employee_bank_details` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `deptVenueSalaryId_idx` ON `employee_dept_salary` (`deptVenueId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `salaryTypeId_idx` ON `employee_dept_salary` (`salaryTypeId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `srno_UNIQUE` ON `employee_dept_salary` (`srno`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `empId_UNIQUE` ON `employee_family_details` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `itsId_UNIQUE` ON `employee_family_details` (`id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `khidmatguzarIts_idx` ON `employee_khidmat_details` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `id_UNIQUE2` ON `employee_logs` (`id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `targetId_idx` ON `employee_logs` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `citizenIts_idx` ON `employee_passport_details` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `id_UNIQUE3` ON `employee_passport_details` (`id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `itsId_UNIQUE1` ON `employee_salary` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_questionnaire_form_idx` ON `form_questionnaire` (`formId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_form_response` ON `form_response` (`formId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_holiday_allocation_calender_idx` ON `holiday_allocation` (`holidayId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_holiday_allocation_deptvenue_idx` ON `holiday_allocation` (`deptVenueId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `Seondary` ON `holiday_allocation` (`holidayId`, `deptVenueId`, `employeeType`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `Secondary` ON `ikhtibaar_marksheet` (`ikhtibaarId`, `itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_questionnaire_ikhtebaar_idx` ON `ikhtibaar_questionnaire` (`ikhtibaarId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_venue_khidmatguzaar_idx` ON `khidmat_guzaar` (`mauze`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_bill_item_bill_item_master_idx` ON `mz_expense_bill_item` (`itemId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_bill_item_bill_master_idx` ON `mz_expense_bill_item` (`billId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_bill_logs_bill_master_idx` ON `mz_expense_bill_logs` (`billId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_budget_araz_baseItem_idx` ON `mz_expense_budget_araz` (`baseItemId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_budget_araz_items_idx` ON `mz_expense_budget_araz` (`itemId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_budget_deptvenue_idx` ON `mz_expense_budget_araz` (`deptVenueId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `secondary` ON `mz_expense_budget_araz` (`deptVenueId`, `baseItemId`, `itemId`, `financialYear`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_budget_araz_from_idx` ON `mz_expense_budget_araz_transfer_logs` (`fromArazId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_budget_araz_to_idx` ON `mz_expense_budget_araz_transfer_logs` (`toArazId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_budget_issue_araz_idx` ON `mz_expense_budget_issue_logs` (`budgetArazId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_budget_deptvenue_idx1` ON `mz_expense_budget_smart_goals` (`deptVenueId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_budget_smart_kg_idx` ON `mz_expense_budget_smart_goals` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_budget_smart_issue_araz_idx` ON `mz_expense_budget_smart_issue_logs` (`smartGoalId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_item_baseitem_idx` ON `mz_expense_procurement_item_baseitem` (`baseItemId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_student_budget_issue_araz_idx` ON `mz_expense_student_budget_issue_logs` (`estimateStudentId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `itsID_UNIQUE` ON `mz_student` (`itsID`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_alumni_mz_kg_idx` ON `nisaab_alumni` (`kgIts`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `indexed` ON `password_reset_requests` (`UserID`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `Secondary1` ON `password_reset_requests` (`UniqueKey`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_sub_platform_role_idx` ON `platform_menu_map` (`subRole`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_platform_module_button_idx` ON `platform_module` (`buttonId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_platform_module_page_idx` ON `platform_module` (`pageId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_platform_role_module_mid_idx` ON `platform_role_module` (`moduleId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_paltform_user_module_kg_idx` ON `platform_user_module` (`userId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_platform_user_module_mid_idx` ON `platform_user_module` (`moduleId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_platform_user_module_qism_idx` ON `platform_user_module` (`qismId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_paltform_user_role_kg_idx` ON `platform_user_role` (`userId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_platform_user_role_mid_idx` ON `platform_user_role` (`roleId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_platform_user_role_qism_idx` ON `platform_user_role` (`qismId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `emailId_UNIQUE1` ON `qism_al_tahfeez` (`emailId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `itsId_UNIQUE2` ON `qism_al_tahfeez` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_deptvenue_qism_user_idx` ON `qism_al_tahfeez_user_deptvenue` (`deptVenueId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_branch_user_pset_p_idx` ON `qism_al_tahfeez_user_pset` (`psetId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_pset_dept_venue_idx` ON `registrationform_dropdown_set` (`deptVenueId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_pset_program_idx` ON `registrationform_dropdown_set` (`programId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_pset_qism_al_tahfeez_idx` ON `registrationform_dropdown_set` (`qismId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_pset_sub_program_idx` ON `registrationform_dropdown_set` (`subprogramId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_pset_venue_idx` ON `registrationform_dropdown_set` (`venueId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_salary_allocation_g_exp_package_idx` ON `salary_allocation_gegorian` (`packageId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `secondary1` ON `salary_allocation_gegorian` (`itsId`, `month`, `year`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_salary_allocation_exp_package_idx` ON `salary_allocation_hijri` (`packageId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_sallary_allocation_h_kh_idx` ON `salary_allocation_hijri` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `empengsalarygeneration_idx` ON `salary_generation_gegorgian` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_salary_gen_g_salary_type_idx` ON `salary_generation_gegorgian` (`salaryType`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_salary_gen_gegorian_allocation_idx` ON `salary_generation_gegorgian` (`allocationId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_salary_gen_gegorian_dept` ON `salary_generation_gegorgian` (`deptVenueId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `Secondary2` ON `salary_generation_gegorgian` (`itsId`, `month`, `year`, `deptVenueId`, `salaryType`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `empsalarygeneration_idx` ON `salary_generation_hijri` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_salary_gen_h_salary_type_idx` ON `salary_generation_hijri` (`salaryType`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_salary_gen_hijri_allocation_idx` ON `salary_generation_hijri` (`allocationId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `sdv_idx` ON `salary_generation_hijri` (`deptVenueId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `Secondary3` ON `salary_generation_hijri` (`itsId`, `deptVenueId`, `salaryType`, `month`, `year`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_class_masool_kg_idx` ON `training_class` (`masoolIts`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_class_student_kg_student_idx` ON `training_class_student` (`studentITS`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_class_student_training_class_idx` ON `training_class_student` (`classId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_cst_class_idx` ON `training_class_subject_teacher` (`classId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_cst_subject_idx` ON `training_class_subject_teacher` (`subjectId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_cst_teacher_idx` ON `training_class_subject_teacher` (`teacherITS`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `secondary_key` ON `training_class_subject_teacher` (`classId`, `subjectId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_student_marksheet_kg_idx` ON `training_student_subject_marksheet` (`gradedBy`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_student_marksheet_student_idx` ON `training_student_subject_marksheet` (`studentITS`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_student_marksheet_subject_cst_idx` ON `training_student_subject_marksheet` (`cstId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `Secondary4` ON `training_student_subject_marksheet` (`cstId`, `studentITS`, `acedemicYear`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_user_deptvenue_baseitem_idx` ON `user_dept-venue_baseitem` (`baseItemId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_user_deptvenue_dept_idx` ON `user_dept-venue_baseitem` (`dept_venueId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_venue_qism_idx` ON `venue` (`qismId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_maqaarat_data_kg_idx` ON `wafdprofile_maqaraat_data` (`studentItsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_maqaraat_data_maqaraat_session_idx` ON `wafdprofile_maqaraat_data` (`sessionId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_maqaraat_session_kg_idx` ON `wafdprofile_maqaraat_session` (`teacherItsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE UNIQUE INDEX `secondary2` ON `wafdprofile_maqaraat_session` (`teacherItsId`, `sessionDate`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    CREATE INDEX `fk_maqaraat_teacher_kg_idx` ON `wafdprofile_maqaraat_teacher` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    ALTER TABLE `branch_user` ADD CONSTRAINT `fk_branch_user_employee` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230927114444_initial') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20230927114444_initial', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231003123744_leaveSetAutoIncrement') THEN

    ALTER TABLE `khidmat_guzaar` ADD `gender` varchar(1) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231003123744_leaveSetAutoIncrement') THEN

    CREATE TABLE `mzlm_leave_stage` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231003123744_leaveSetAutoIncrement') THEN

    CREATE TABLE `mzlm_leave_type` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `accessTo` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `daysAllotted` int(11) NOT NULL,
        `approvalFlow` json NOT NULL,
        `applicableTo` json NOT NULL,
        `active` tinyint(4) NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231003123744_leaveSetAutoIncrement') THEN

    CREATE TABLE `mzlm_leave_category` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `leaveTypeId` int(11) NOT NULL,
        `maxAllowed` int(11) NOT NULL DEFAULT '1',
        `consicutiveLimit` int(11) NOT NULL DEFAULT '1',
        `isHijri` tinyint(4) NOT NULL,
        `minApplicationDate` int(11) NOT NULL DEFAULT '3',
        `isDeductable` tinyint(4) NOT NULL DEFAULT '1',
        `isRepeated` tinyint(4) NOT NULL DEFAULT '1',
        `isCarryForward` tinyint(4) NOT NULL,
        `notifyTo` json NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_leave_type_category` FOREIGN KEY (`leaveTypeId`) REFERENCES `mzlm_leave_type` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231003123744_leaveSetAutoIncrement') THEN

    CREATE TABLE `mzlm_leave_application` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NOT NULL,
        `typeId` int(11) NOT NULL,
        `categoryId` int(11) NOT NULL,
        `fromDayId` int(11) NOT NULL,
        `fromMonthId` int(11) NOT NULL,
        `toDayId` int(11) NOT NULL,
        `toMonthId` int(11) NOT NULL,
        `morningShift` tinyint(1) NOT NULL,
        `eveningShift` tinyint(1) NOT NULL,
        `shiftCount` int(11) NOT NULL DEFAULT '1',
        `hijrAcademicYear` int(11) NOT NULL,
        `stageId` int(11) NOT NULL,
        `venueId` int(11) NOT NULL,
        `appliedBy` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `createdBy` int(11) NOT NULL,
        `createdOn` datetime NOT NULL,
        `purpose` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `FK_mzlm_leave_application_khidmat_guzaar_itsId` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE,
        CONSTRAINT `FK_mzlm_leave_application_mzlm_leave_category_categoryId` FOREIGN KEY (`categoryId`) REFERENCES `mzlm_leave_category` (`id`) ON DELETE CASCADE,
        CONSTRAINT `FK_mzlm_leave_application_mzlm_leave_type_typeId` FOREIGN KEY (`typeId`) REFERENCES `mzlm_leave_type` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231003123744_leaveSetAutoIncrement') THEN

    CREATE TABLE `mzlm_leave_logs` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `leaveId` int(11) NOT NULL,
        `remark` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `stageId` int(11) NOT NULL,
        `createdOn` datetime NOT NULL,
        `createdBy` int(11) NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_mzlm_leave_application_log` FOREIGN KEY (`leaveId`) REFERENCES `mzlm_leave_application` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_mzlm_leave_updateby` FOREIGN KEY (`createdBy`) REFERENCES `khidmat_guzaar` (`itsId`),
        CONSTRAINT `fk_mzlm_stage_id_leave_log` FOREIGN KEY (`stageId`) REFERENCES `mzlm_leave_stage` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231003123744_leaveSetAutoIncrement') THEN

    CREATE INDEX `fk_leave_apply_category_idx` ON `mzlm_leave_application` (`categoryId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231003123744_leaveSetAutoIncrement') THEN

    CREATE INDEX `fk_leave_apply_khidmatguzaar_idx` ON `mzlm_leave_application` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231003123744_leaveSetAutoIncrement') THEN

    CREATE INDEX `fk_leave_apply_type_idx` ON `mzlm_leave_application` (`typeId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231003123744_leaveSetAutoIncrement') THEN

    CREATE INDEX `fk_leave_type_category_idx` ON `mzlm_leave_category` (`leaveTypeId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231003123744_leaveSetAutoIncrement') THEN

    CREATE INDEX `fk_mzlm_leave_application_log_idx` ON `mzlm_leave_logs` (`leaveId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231003123744_leaveSetAutoIncrement') THEN

    CREATE INDEX `fk_mzlm_leave_updateby_idx` ON `mzlm_leave_logs` (`createdBy`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231003123744_leaveSetAutoIncrement') THEN

    CREATE INDEX `fk_mzlm_stage_id_idx` ON `mzlm_leave_logs` (`stageId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231003123744_leaveSetAutoIncrement') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231003123744_leaveSetAutoIncrement', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231005063355_activeStatusColumnMZLMCat') THEN

    ALTER TABLE `mzlm_leave_category` ADD `active` tinyint(4) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231005063355_activeStatusColumnMZLMCat') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231005063355_activeStatusColumnMZLMCat', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    ALTER TABLE `venue` MODIFY COLUMN `qismTahfeez` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    ALTER TABLE `venue` MODIFY COLUMN `ho` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    ALTER TABLE `venue` MODIFY COLUMN `displayName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    ALTER TABLE `venue` MODIFY COLUMN `currency` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    ALTER TABLE `venue` MODIFY COLUMN `CashBalance` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    ALTER TABLE `venue` MODIFY COLUMN `CampVenue` varchar(1000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    ALTER TABLE `venue` MODIFY COLUMN `CampId` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    ALTER TABLE `venue` MODIFY COLUMN `Id` int(11) NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    ALTER TABLE `mzlm_leave_application` ADD `packageId` int(11) NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `gender` varchar(1) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    CREATE TABLE `mzlm_leave_package` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `name` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        `stageId` int(11) NOT NULL,
        `purpose` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
        CONSTRAINT `PK_mzlm_leave_package` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    CREATE INDEX `fk_leave_apply_package_idx` ON `mzlm_leave_application` (`packageId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    CREATE INDEX `fk_leave_apply_stage_idx` ON `mzlm_leave_application` (`stageId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    ALTER TABLE `mzlm_leave_application` ADD CONSTRAINT `FK_mzlm_leave_application_mzlm_leave_package_packageId` FOREIGN KEY (`packageId`) REFERENCES `mzlm_leave_package` (`id`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    ALTER TABLE `mzlm_leave_application` ADD CONSTRAINT `FK_mzlm_leave_application_mzlm_leave_stage_stageId` FOREIGN KEY (`stageId`) REFERENCES `mzlm_leave_stage` (`id`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231027121608_add_mzlm_package') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231027121608_add_mzlm_package', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `yellowreceipt` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_workshops_category_subcategory` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_workshop_data` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_qualification_stage_degree` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_qualification_new` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_maqaraat_teacher` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_maqaraat_session` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_maqaraat_data` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_dropdown_workshopcategory` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_dropdown_titlecategory` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_dropdown_mode` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_dropdown_fieldofinterest` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_dropdown_degree` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_dropdown_category` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_dropdown_authoredcategory` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafd_physicalfitness` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafd_otheridara_mawaze` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafd_mahad_past_mawaze` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafd_languageproficiency` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafd_fieldofinterest` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `venue` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `useritemassociation` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `userdeptassociation` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `user_deptvenue` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `user_dept-venue_baseitem` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `salary_querylogs` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `salary_generation_hijri` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `salary_generation_gegorgian` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `role_rights` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `role_module` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `role` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `rights` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `registrationform_subprograms` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `registrationform_programs` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `registrationform_dropdown_set` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `qismal_tahfeez_student` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `qism_al_tahfeez_user_pset` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `qism_al_tahfeez_user_deptvenue` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `qism_al_tahfeez_user` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `qism_al_tahfeez_role_right` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `qism_al_tahfeez_role_module` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `qism_al_tahfeez_role` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `qism_al_tahfeez_login_logs` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `qism_al_tahfeez_api_logs` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `printableformat_report_rights` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `printableformat_report` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `nisaab_periods` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `nisaab_jadwal` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `nisaab_classes_monitor` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `nisaab_classes` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_student_receipt` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_student_feecategory_pset` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_student_feecategory` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_student_fee_transaction` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_student_fee_allotment` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_student_ewallet` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_receipt_payment_modes` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_receipt_payment_mode_rights` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_pset_elqgrpid_mapping` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_on_off_modules` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_loginlogs` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_kg_wajebaat_araz` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_fee_collection_center` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_faculty_loginlogs` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_expense_vendor_master` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_expense_procurement_item` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mz_expense_procurement_baseitem` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `module_rights` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `module` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `masterdepartment` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `kg_worktype` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `kg_venue_worktype` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `kg_self_assessment` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `kg_identitycards` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `kg_faimalydetails_its` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `kg_faimalydetails` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `ikhtibaar_marksheet` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `hijri_months` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `hijri_calender` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `export_type_displayheader` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `export_type` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `export_category` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `employee_salary` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `employee_passport_details` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `employee_logs` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `employee_khidmat_details` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `employee_family_details` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `employee_bank_details` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `employee_academic_details` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `dropdown_dataset_options` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `dropdown_dataset_header` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `dept_venue_baseitem` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `dept_venue` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `department` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `azwaaj_minentry` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `acedemicyear_data` COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_workshops_category_subcategory` MODIFY COLUMN `subCategory` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `wafdprofile_workshops_category_subcategory` MODIFY COLUMN `category` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mzlm_leave_application` ADD `toYear` int(11) NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    ALTER TABLE `mzlm_leave_application` ADD `fromYear` int(11) NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029110558_makePackageIdOptionalAndAddYear') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231029110558_makePackageIdOptionalAndAddYear', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `venue` SET `qismTahfeez` = ''
    WHERE `qismTahfeez` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `venue` MODIFY COLUMN `qismTahfeez` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `venue` SET `ho` = ''
    WHERE `ho` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `venue` MODIFY COLUMN `ho` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `venue` SET `displayName` = ''
    WHERE `displayName` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `venue` MODIFY COLUMN `displayName` varchar(500) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `venue` SET `currency` = ''
    WHERE `currency` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `venue` MODIFY COLUMN `currency` varchar(200) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `venue` SET `CampVenue` = ''
    WHERE `CampVenue` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `venue` MODIFY COLUMN `CampVenue` varchar(1000) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `venue` SET `CampId` = ''
    WHERE `CampId` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `venue` MODIFY COLUMN `CampId` varchar(100) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `whatsappNo` = ''
    WHERE `whatsappNo` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `whatsappNo` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `watanArabic` = ''
    WHERE `watanArabic` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `watanArabic` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `watanAdress` = ''
    WHERE `watanAdress` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `watanAdress` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `watan` = ''
    WHERE `watan` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `watan` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `subDesignation` = ''
    WHERE `subDesignation` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `subDesignation` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `salaryCode` = ''
    WHERE `salaryCode` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `salaryCode` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `photoBase64` = ''
    WHERE `photoBase64` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `photoBase64` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `photo` = ''
    WHERE `photo` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `photo` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `personalHouseType` = ''
    WHERE `personalHouseType` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `personalHouseType` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `personalHouseStatus` = ''
    WHERE `personalHouseStatus` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `personalHouseStatus` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `personalHouseArea` = ''
    WHERE `personalHouseArea` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `personalHouseArea` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `personalHouseAddress` = ''
    WHERE `personalHouseAddress` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `personalHouseAddress` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `panCardNo` = ''
    WHERE `panCardNo` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `panCardNo` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `officialEmailAddress` = ''
    WHERE `officialEmailAddress` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `officialEmailAddress` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `nationality` = ''
    WHERE `nationality` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `nationality` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `mz_idara` = ''
    WHERE `mz_idara` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `mz_idara` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `muqamArabic` = ''
    WHERE `muqamArabic` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `muqamArabic` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `muqam` = ''
    WHERE `muqam` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `muqam` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `mobileNo` = ''
    WHERE `mobileNo` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `mobileNo` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `maritalStatus` = ''
    WHERE `maritalStatus` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `maritalStatus` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `krNo` = ''
    WHERE `krNo` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `krNo` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `jamiat` = ''
    WHERE `jamiat` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `jamiat` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `jaman` = ''
    WHERE `jaman` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `jaman` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `jamaat` = ''
    WHERE `jamaat` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `jamaat` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `its_preferredIdara` = ''
    WHERE `its_preferredIdara` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `its_preferredIdara` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `its_idaras` = ''
    WHERE `its_idaras` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `its_idaras` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `fullNameArabic` = ''
    WHERE `fullNameArabic` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `fullNameArabic` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `fullName` = ''
    WHERE `fullName` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `fullName` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `emailAddress` = ''
    WHERE `emailAddress` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `emailAddress` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `domicileParent` = ''
    WHERE `domicileParent` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `domicileParent` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `domicileAddressParents` = ''
    WHERE `domicileAddressParents` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `domicileAddressParents` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `dojHijri` = ''
    WHERE `dojHijri` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `dojHijri` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `dobHijri` = ''
    WHERE `dobHijri` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `dobHijri` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `dobGregorian` = ''
    WHERE `dobGregorian` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `dobGregorian` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `designation` = ''
    WHERE `designation` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `designation` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `dawat_title` = ''
    WHERE `dawat_title` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `dawat_title` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `currentAddress` = ''
    WHERE `currentAddress` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `currentAddress` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `c_codeWhatsapp` = ''
    WHERE `c_codeWhatsapp` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `c_codeWhatsapp` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `c_codeMobile` = ''
    WHERE `c_codeMobile` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `c_codeMobile` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `bloodGroup` = ''
    WHERE `bloodGroup` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `bloodGroup` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `RfId` = ''
    WHERE `RfId` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `RfId` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `OthDegree` = ''
    WHERE `OthDegree` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `OthDegree` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `EduQualification` = ''
    WHERE `EduQualification` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `EduQualification` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `EduCompletion` = ''
    WHERE `EduCompletion` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `EduCompletion` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    UPDATE `khidmat_guzaar` SET `CreatedBy` = ''
    WHERE `CreatedBy` IS NULL;
    SELECT ROW_COUNT();


    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `CreatedBy` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231029115518_stabblelizeDb') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231029115518_stabblelizeDb', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `whatsappNo` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `watanArabic` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `watanAdress` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `watan` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `subDesignation` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `salaryCode` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `photoBase64` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `photo` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `personalHouseType` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `personalHouseStatus` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `personalHouseArea` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `personalHouseAddress` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `panCardNo` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `officialEmailAddress` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `nationality` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `mz_idara` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `muqamArabic` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `muqam` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `maritalStatus` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `krNo` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `jamiat` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `jaman` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `jamaat` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `its_preferredIdara` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `its_idaras` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `isMumin` tinyint(1) NOT NULL DEFAULT FALSE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `id` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `fullNameArabic` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `domicileParent` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `domicileAddressParents` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `dojHijri` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `dobHijri` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `designation` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `dawat_title` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `currentAddress` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `c_codeWhatsapp` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `c_codeMobile` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `bloodGroup` varchar(45) CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `RfId` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `OthDegree` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `EduQualification` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `EduCompletion` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `CreatedBy` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030093318_nullablefieldsKG') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231030093318_nullablefieldsKG', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_receipt` MODIFY COLUMN `transactionId` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_receipt` MODIFY COLUMN `status` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_receipt` MODIFY COLUMN `paymentMode` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_receipt` MODIFY COLUMN `note` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_receipt` MODIFY COLUMN `currency` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_receipt` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_receipt` MODIFY COLUMN `bankName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_receipt` MODIFY COLUMN `account` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_feecategory_pset` MODIFY COLUMN `currency` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_fee_transaction` MODIFY COLUMN `transactionId` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_fee_transaction` MODIFY COLUMN `remarks` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_fee_transaction` MODIFY COLUMN `paymentMode` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_fee_transaction` MODIFY COLUMN `currency` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_fee_transaction` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_ewallet` MODIFY COLUMN `paymentType` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_ewallet` MODIFY COLUMN `note` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_ewallet` MODIFY COLUMN `currency` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student_ewallet` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `vatan` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `studentWhatsapp` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `studentMobile` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `studentEmail` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `photoPath` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `photoBase64` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `nationality` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `nameEng` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `nameArabic` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `motherWhatsapp` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `motherMobile` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `motherEmail` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `maqaam` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `jamiat` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `jamaat` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `idara` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `hifzStatus` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `gender` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `fatherWhatsapp` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `fatherMobile` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `fatherEmail` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `elq_BranchName` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `dobHijri` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `dobGregorian` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `createdBy` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `bloodGroup` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_student` MODIFY COLUMN `address` text CHARACTER SET cp1256 COLLATE cp1256_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_receipt_payment_modes` MODIFY COLUMN `name` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_receipt_payment_modes` MODIFY COLUMN `description` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_on_off_modules` MODIFY COLUMN `name` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_loginlogs` MODIFY COLUMN `ipAddress` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_loginlogs` MODIFY COLUMN `deviceDetails` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_kg_wajebaat_araz` MODIFY COLUMN `wajebaatType` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT 'Wajebaat Niyat';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_kg_wajebaat_araz` MODIFY COLUMN `userRemarks` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_kg_wajebaat_araz` MODIFY COLUMN `updatedBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_kg_wajebaat_araz` MODIFY COLUMN `stage` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT 'Initialized';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_kg_wajebaat_araz` MODIFY COLUMN `officeRemarks` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_kg_wajebaat_araz` MODIFY COLUMN `draftNo` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_kg_wajebaat_araz` MODIFY COLUMN `displayCurrency` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_kg_wajebaat_araz` MODIFY COLUMN `currency` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_kg_wajebaat_araz` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_kg_wajebaat_araz` MODIFY COLUMN `bankName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_fee_collection_center` MODIFY COLUMN `name` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_fee_collection_center` MODIFY COLUMN `description` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_faculty_loginlogs` MODIFY COLUMN `ipAddress` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_faculty_loginlogs` MODIFY COLUMN `deviceDetails` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_wallet` MODIFY COLUMN `paymentType` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_wallet` MODIFY COLUMN `note` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_wallet` MODIFY COLUMN `currency` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_wallet` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_payment` MODIFY COLUMN `transactionId` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_payment` MODIFY COLUMN `status` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_payment` MODIFY COLUMN `paymentMode` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_payment` MODIFY COLUMN `note` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_payment` MODIFY COLUMN `currency` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_payment` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_payment` MODIFY COLUMN `chequeDate` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `whatsappNo` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `type` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `state` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `phoneNo` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `panCardNo` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `name` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `mobileNo` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `ifscCode` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `gstNumber` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `email` varchar(60) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `city` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `bankName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `address` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `accountNo` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_vendor_master` MODIFY COLUMN `accountName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_student_budget_issue_logs` MODIFY COLUMN `remark` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_student_budget_issue_logs` MODIFY COLUMN `createdBy` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_student_budget_issue_logs` MODIFY COLUMN `arazState` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_procurement_item` MODIFY COLUMN `uom` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_procurement_item` MODIFY COLUMN `type` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_procurement_item` MODIFY COLUMN `name` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_procurement_baseitem` MODIFY COLUMN `name` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_online_payment_users` MODIFY COLUMN `name` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_online_payment_users` MODIFY COLUMN `ifsc` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_online_payment_users` MODIFY COLUMN `bankName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_online_payment_users` MODIFY COLUMN `accNum` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_online_payment_users` MODIFY COLUMN `accName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_estimate_student` MODIFY COLUMN `remarks_admin` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_estimate_student` MODIFY COLUMN `remarks` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_estimate_student` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_deptvenue_cash_wallet` MODIFY COLUMN `paymentType` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_deptvenue_cash_wallet` MODIFY COLUMN `note` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_deptvenue_cash_wallet` MODIFY COLUMN `currency` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_deptvenue_cash_wallet` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_smart_issue_logs` MODIFY COLUMN `remark` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_smart_issue_logs` MODIFY COLUMN `createdBy` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_smart_goals` MODIFY COLUMN `updatedBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_smart_goals` MODIFY COLUMN `specific` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_smart_goals` MODIFY COLUMN `remarks_admin` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_smart_goals` MODIFY COLUMN `relevant` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_smart_goals` MODIFY COLUMN `measearable` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_smart_goals` MODIFY COLUMN `category` varchar(75) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_smart_goals` MODIFY COLUMN `attainable` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_araz_transfer_logs` MODIFY COLUMN `trabferModel` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_araz` MODIFY COLUMN `updatedBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_araz` MODIFY COLUMN `uom` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_araz` MODIFY COLUMN `remarks_admin` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_araz` MODIFY COLUMN `justification` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_budget_araz` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_bills_package` MODIFY COLUMN `name` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_bills_package` MODIFY COLUMN `description` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_bill_master` MODIFY COLUMN `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_bill_master` MODIFY COLUMN `paymentTo_ifsc` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_bill_master` MODIFY COLUMN `paymentTo_BankName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_bill_master` MODIFY COLUMN `paymentTo_AccNum` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_bill_master` MODIFY COLUMN `paymentTo_AccName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_bill_master` MODIFY COLUMN `paymentMode_User` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_bill_master` MODIFY COLUMN `paymentMode_Admin` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_bill_master` MODIFY COLUMN `paymentFrom_BankName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_bill_master` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_bill_master` MODIFY COLUMN `billNo` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `mz_expense_bill_item` MODIFY COLUMN `remarks` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_worktype` MODIFY COLUMN `typeName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_worktype` MODIFY COLUMN `description` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_self_assessment` MODIFY COLUMN `weakness` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_self_assessment` MODIFY COLUMN `strength` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_self_assessment` MODIFY COLUMN `roleModel` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_self_assessment` MODIFY COLUMN `personalitytype` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_self_assessment` MODIFY COLUMN `personalityReport` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_self_assessment` MODIFY COLUMN `longTermGoal` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_self_assessment` MODIFY COLUMN `changeAboutYourself` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_self_assessment` MODIFY COLUMN `alternativeCareerPath` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_self_assessment` MODIFY COLUMN `aboutYourSelf` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_identitycards` MODIFY COLUMN `nameOnCard` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_identitycards` MODIFY COLUMN `country` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_identitycards` MODIFY COLUMN `cardType` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_identitycards` MODIFY COLUMN `cardNumber` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_identitycards` MODIFY COLUMN `attachment` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails_its` MODIFY COLUMN `relation` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails_its` MODIFY COLUMN `occupation` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails_its` MODIFY COLUMN `nationality` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails_its` MODIFY COLUMN `name` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails_its` MODIFY COLUMN `jamaat` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails_its` MODIFY COLUMN `idara` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails_its` MODIFY COLUMN `hifzStatus` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails_its` MODIFY COLUMN `bloodGroup` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails_its` MODIFY COLUMN `age` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails` MODIFY COLUMN `relation` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails` MODIFY COLUMN `occupation` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails` MODIFY COLUMN `nationality` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails` MODIFY COLUMN `name` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails` MODIFY COLUMN `jamaat` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails` MODIFY COLUMN `idara` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails` MODIFY COLUMN `hifzStatus` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails` MODIFY COLUMN `bloodGroup` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `kg_faimalydetails` MODIFY COLUMN `age` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `ikhtibaar_marksheet` MODIFY COLUMN `type` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `ikhtibaar_marksheet` MODIFY COLUMN `remarks` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `ikhtibaar_marksheet` MODIFY COLUMN `mukhtabir` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `ikhtibaar_marksheet` MODIFY COLUMN `marks` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `ikhtibaar_marksheet` MODIFY COLUMN `category` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `form_response` MODIFY COLUMN `response` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `form_response` MODIFY COLUMN `identifier` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `form_questionnaire` MODIFY COLUMN `type` varchar(25) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `form_questionnaire` MODIFY COLUMN `description` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `form` MODIFY COLUMN `setting` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `form` MODIFY COLUMN `description` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `export_type_displayheader` MODIFY COLUMN `displayName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `export_type_displayheader` MODIFY COLUMN `actualName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `export_type` MODIFY COLUMN `name` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `export_type` MODIFY COLUMN `fileName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `export_type` MODIFY COLUMN `description` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `export_category` MODIFY COLUMN `fieldDisplayName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `export_category` MODIFY COLUMN `fieldActualName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `export_category` MODIFY COLUMN `categoryName` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_salary` MODIFY COLUMN `currency` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_passport_details` MODIFY COLUMN `placeOfIssue` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_passport_details` MODIFY COLUMN `passportPlaceOfBirth` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_passport_details` MODIFY COLUMN `passportNo` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_passport_details` MODIFY COLUMN `passportName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_passport_details` MODIFY COLUMN `passportCopy` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_passport_details` MODIFY COLUMN `dobPassport` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_passport_details` MODIFY COLUMN `dateOfIssue` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_passport_details` MODIFY COLUMN `dateOfExpiry` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_khidmat_details` MODIFY COLUMN `khidmatMauzeHouseStatus` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_khidmat_details` MODIFY COLUMN `khdimatMauzeHouseType` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_family_details` MODIFY COLUMN `SpouseName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_family_details` MODIFY COLUMN `SpouseIts` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_family_details` MODIFY COLUMN `MotherName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_family_details` MODIFY COLUMN `MotherIts` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_family_details` MODIFY COLUMN `FatherName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_family_details` MODIFY COLUMN `FatherIts` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_e_attendence` MODIFY COLUMN `logJson` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_bank_details` MODIFY COLUMN `ifsc` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_bank_details` MODIFY COLUMN `chequeAttachment` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_bank_details` MODIFY COLUMN `bankName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_bank_details` MODIFY COLUMN `bankBranch` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_bank_details` MODIFY COLUMN `bankAccountType` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_bank_details` MODIFY COLUMN `bankAccountNumber` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `employee_bank_details` MODIFY COLUMN `bankAccountName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `dept_venue_baseitem` MODIFY COLUMN `tag` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `dept_venue` MODIFY COLUMN `venueName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `dept_venue` MODIFY COLUMN `tag` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `dept_venue` MODIFY COLUMN `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `dept_venue` MODIFY COLUMN `masterDeptName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `dept_venue` MODIFY COLUMN `deptName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `department` MODIFY COLUMN `tag` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `azwaaj_minentry` MODIFY COLUMN `description` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    ALTER TABLE `acedemicyear_data` MODIFY COLUMN `acedemicName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030114356_resetnullablefieldstillM') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231030114356_resetnullablefieldstillM', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `salary_querylogs` MODIFY COLUMN `type` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `salary_generation_hijri` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `salary_generation_gegorgian` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `salary_allocation_hijri` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `salary_allocation_gegorian` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_student_feecategory` MODIFY COLUMN `categoryName` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_student_fee_excluding_list` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_student_fee_allotment` MODIFY COLUMN `remarks` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_student_fee_allotment` MODIFY COLUMN `reason` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_student_fee_allotment` MODIFY COLUMN `currency` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_student_fee_allotment` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_expense_vendor_transaction` MODIFY COLUMN `transactionId` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_expense_vendor_transaction` MODIFY COLUMN `remarks` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_expense_vendor_transaction` MODIFY COLUMN `paymentMode` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_expense_vendor_transaction` MODIFY COLUMN `currency` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_expense_vendor_transaction` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_expense_sanctioned_budget` MODIFY COLUMN `updatedBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_expense_budget_transfer_logs` MODIFY COLUMN `remarks` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_expense_budget_transfer_logs` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_expense_budget_issue_logs` MODIFY COLUMN `remark` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_expense_budget_issue_logs` MODIFY COLUMN `createdBy` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_expense_budget_issue_logs` MODIFY COLUMN `arazState` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_expense_bill_logs` MODIFY COLUMN `status` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_expense_bill_logs` MODIFY COLUMN `description` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `mz_expense_bill_logs` MODIFY COLUMN `createdBy` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `module` MODIFY COLUMN `moduleName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `masterdepartment` MODIFY COLUMN `masterDeptName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `holiday_hijri_miqaat` MODIFY COLUMN `miqaats_title` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `holiday_hijri_miqaat` MODIFY COLUMN `miqaats_description` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `employee_academic_details` MODIFY COLUMN `hifzStatus` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `employee_academic_details` MODIFY COLUMN `category` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    ALTER TABLE `employee_academic_details` MODIFY COLUMN `aljameaDegree` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231030115157_resetnullablefieldS') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231030115157_resetnullablefieldS', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231031080841_testMigration') THEN

    ALTER TABLE `mzlm_leave_application` MODIFY COLUMN `packageId` int(11) NULL DEFAULT '0';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231031080841_testMigration') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231031080841_testMigration', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231031102029_testMigration2') THEN

    ALTER TABLE `mzlm_leave_application` MODIFY COLUMN `packageId` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231031102029_testMigration2') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231031102029_testMigration2', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231102055407_addLeavePackageLogs') THEN

    CREATE TABLE `mzlm_leave_package_logs` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `packageId` int(11) NOT NULL,
        `remark` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `stageId` int(11) NOT NULL,
        `createdOn` datetime NOT NULL,
        `createdBy` int(11) NOT NULL,
        CONSTRAINT `PK_mzlm_leave_package_logs` PRIMARY KEY (`id`),
        CONSTRAINT `FK_mzlm_leave_package_logs_khidmat_guzaar_createdBy` FOREIGN KEY (`createdBy`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE,
        CONSTRAINT `FK_mzlm_leave_package_logs_mzlm_leave_package_packageId` FOREIGN KEY (`packageId`) REFERENCES `mzlm_leave_package` (`id`) ON DELETE CASCADE,
        CONSTRAINT `FK_mzlm_leave_package_logs_mzlm_leave_stage_stageId` FOREIGN KEY (`stageId`) REFERENCES `mzlm_leave_stage` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231102055407_addLeavePackageLogs') THEN

    CREATE INDEX `fk_mzlm_leave_package_log_idx` ON `mzlm_leave_package_logs` (`packageId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231102055407_addLeavePackageLogs') THEN

    CREATE INDEX `fk_mzlm_leave_updateby_idx1` ON `mzlm_leave_package_logs` (`createdBy`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231102055407_addLeavePackageLogs') THEN

    CREATE INDEX `fk_mzlm_stage_id_idx1` ON `mzlm_leave_package_logs` (`stageId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231102055407_addLeavePackageLogs') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231102055407_addLeavePackageLogs', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231102111010_changeApprovalFlow') THEN

    ALTER TABLE `mzlm_leave_type` MODIFY COLUMN `approvalFlow` varchar(75) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231102111010_changeApprovalFlow') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231102111010_changeApprovalFlow', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231107111343_changeBoolandAddIsCountableFlag') THEN

    ALTER TABLE `platform_role` MODIFY COLUMN `isDefault` tinyint(1) NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231107111343_changeBoolandAddIsCountableFlag') THEN

    ALTER TABLE `platform_module` MODIFY COLUMN `isDefault` tinyint(1) NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231107111343_changeBoolandAddIsCountableFlag') THEN

    ALTER TABLE `mzlm_leave_type` MODIFY COLUMN `active` tinyint(1) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231107111343_changeBoolandAddIsCountableFlag') THEN

    ALTER TABLE `mzlm_leave_stage` ADD `isDeductable` tinyint(1) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231107111343_changeBoolandAddIsCountableFlag') THEN

    ALTER TABLE `mzlm_leave_category` MODIFY COLUMN `isRepeated` tinyint(1) NOT NULL DEFAULT '1';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231107111343_changeBoolandAddIsCountableFlag') THEN

    ALTER TABLE `mzlm_leave_category` MODIFY COLUMN `isHijri` tinyint(1) NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231107111343_changeBoolandAddIsCountableFlag') THEN

    ALTER TABLE `mzlm_leave_category` MODIFY COLUMN `isDeductable` tinyint(1) NOT NULL DEFAULT '1';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231107111343_changeBoolandAddIsCountableFlag') THEN

    ALTER TABLE `mzlm_leave_category` MODIFY COLUMN `isCarryForward` tinyint(1) NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231107111343_changeBoolandAddIsCountableFlag') THEN

    ALTER TABLE `mzlm_leave_category` MODIFY COLUMN `active` tinyint(1) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231107111343_changeBoolandAddIsCountableFlag') THEN

    ALTER TABLE `ikhtibaar_marksheet` MODIFY COLUMN `hasAttempted` tinyint(1) NOT NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231107111343_changeBoolandAddIsCountableFlag') THEN

    ALTER TABLE `dept_venue_baseitem` MODIFY COLUMN `hasItemBlock` tinyint(1) NOT NULL DEFAULT '1';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231107111343_changeBoolandAddIsCountableFlag') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231107111343_changeBoolandAddIsCountableFlag', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231109074258_notificationemailtemplatetable') THEN

    ALTER TABLE `khidmat_guzaar` DROP FOREIGN KEY `fk_venue_khidmatguzaar`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231109074258_notificationemailtemplatetable') THEN

    CREATE TABLE `notification_email_template` (
        `id` int NOT NULL AUTO_INCREMENT,
        `templateName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `module` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `content` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `dynamicAttributes` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `reference` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `emailSubject` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `createdAt` datetime(6) NOT NULL,
        `updatedAt` datetime(6) NOT NULL,
        `createdBy` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `updatedBy` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231109074258_notificationemailtemplatetable') THEN

    ALTER TABLE `khidmat_guzaar` ADD CONSTRAINT `FK_khidmat_guzaar_venue_mauze` FOREIGN KEY (`mauze`) REFERENCES `venue` (`Id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231109074258_notificationemailtemplatetable') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231109074258_notificationemailtemplatetable', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231109102338_makePurposeNullable') THEN

    ALTER TABLE `mzlm_leave_package` MODIFY COLUMN `purpose` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231109102338_makePurposeNullable') THEN

    ALTER TABLE `mzlm_leave_application` MODIFY COLUMN `purpose` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231109102338_makePurposeNullable') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231109102338_makePurposeNullable', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231111065821_snycDb') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231111065821_snycDb', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231114092146_add eng date in application') THEN

    ALTER TABLE `mzlm_leave_application` ADD `fromEngDate` datetime NOT NULL DEFAULT '0001-01-01 00:00:00';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231114092146_add eng date in application') THEN

    ALTER TABLE `mzlm_leave_application` ADD `toEngDate` datetime NOT NULL DEFAULT '0001-01-01 00:00:00';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231114092146_add eng date in application') THEN

    ALTER TABLE `mzlm_leave_application` ADD `updatedOn` datetime NOT NULL DEFAULT '0001-01-01 00:00:00';

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231114092146_add eng date in application') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231114092146_add eng date in application', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231124132728_add_uploadFile_path_to_mzlm_application') THEN

    ALTER TABLE `mzlm_leave_application` ADD `UploadedDocumentUrl` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231124132728_add_uploadFile_path_to_mzlm_application') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231124132728_add_uploadFile_path_to_mzlm_application', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231129032315_add_minentry_virtual_keys') THEN

    ALTER TABLE `azwaaj_minentry` ADD CONSTRAINT `fk_minentry_deptVenue` FOREIGN KEY (`deptVenueId`) REFERENCES `dept_venue` (`id`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231129032315_add_minentry_virtual_keys') THEN

    ALTER TABLE `azwaaj_minentry` ADD CONSTRAINT `fk_minentry_salaryType` FOREIGN KEY (`policyId`) REFERENCES `salary_type` (`id`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231129032315_add_minentry_virtual_keys') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231129032315_add_minentry_virtual_keys', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231129074509_add_onLeave_minentry') THEN

    ALTER TABLE `azwaaj_minentry` ADD `isOnLeave` tinyint(1) NOT NULL DEFAULT FALSE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231129074509_add_onLeave_minentry') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231129074509_add_onLeave_minentry', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212073826_add_edit_logs_venue_tranfer') THEN

    CREATE TABLE `venue_transfer_approval` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `requested_by` int(11) NOT NULL,
        `employeeIts` int(11) NOT NULL,
        `current_venue_id` int(11) NOT NULL,
        `reviewed_by` int(11) NULL,
        `stage` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `requested_on` datetime NOT NULL,
        `approval_comment` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `updated_on` datetime NULL,
        CONSTRAINT `PK_venue_transfer_approval` PRIMARY KEY (`id`),
        CONSTRAINT `FK_venue_transfer_approval_khidmat_guzaar_employeeIts` FOREIGN KEY (`employeeIts`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE,
        CONSTRAINT `FK_venue_transfer_approval_khidmat_guzaar_requested_by` FOREIGN KEY (`requested_by`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE,
        CONSTRAINT `FK_venue_transfer_approval_khidmat_guzaar_reviewed_by` FOREIGN KEY (`reviewed_by`) REFERENCES `khidmat_guzaar` (`itsId`),
        CONSTRAINT `FK_venue_transfer_approval_venue_current_venue_id` FOREIGN KEY (`current_venue_id`) REFERENCES `venue` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212073826_add_edit_logs_venue_tranfer') THEN

    CREATE INDEX `IX_venue_transfer_approval_current_venue_id` ON `venue_transfer_approval` (`current_venue_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212073826_add_edit_logs_venue_tranfer') THEN

    CREATE INDEX `IX_venue_transfer_approval_employeeIts` ON `venue_transfer_approval` (`employeeIts`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212073826_add_edit_logs_venue_tranfer') THEN

    CREATE INDEX `IX_venue_transfer_approval_requested_by` ON `venue_transfer_approval` (`requested_by`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212073826_add_edit_logs_venue_tranfer') THEN

    CREATE INDEX `IX_venue_transfer_approval_reviewed_by` ON `venue_transfer_approval` (`reviewed_by`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212073826_add_edit_logs_venue_tranfer') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231212073826_add_edit_logs_venue_tranfer', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212074631_add_indexs_in_edit_logs') THEN

    CREATE TABLE `edit_table_column_logs` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `table_id` int(11) NOT NULL,
        `column_id` int(11) NOT NULL,
        `old_value` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `new_value` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `edited_by` int(11) NOT NULL,
        `table_name` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `column_name` varchar(25) CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `table_primary_key_value` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `edit_date_time` datetime NOT NULL,
        CONSTRAINT `PK_edit_table_column_logs` PRIMARY KEY (`id`),
        CONSTRAINT `FK_edit_table_column_logs_khidmat_guzaar_edited_by` FOREIGN KEY (`edited_by`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212074631_add_indexs_in_edit_logs') THEN

    CREATE INDEX `fk_edit_table_column_logs_column_idx` ON `edit_table_column_logs` (`column_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212074631_add_indexs_in_edit_logs') THEN

    CREATE INDEX `fk_edit_table_column_logs_column_name_idx` ON `edit_table_column_logs` (`column_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212074631_add_indexs_in_edit_logs') THEN

    CREATE INDEX `fk_edit_table_column_logs_khidmatguzaar_idx` ON `edit_table_column_logs` (`edited_by`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212074631_add_indexs_in_edit_logs') THEN

    CREATE INDEX `fk_edit_table_column_logs_table_idx` ON `edit_table_column_logs` (`table_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212074631_add_indexs_in_edit_logs') THEN

    CREATE INDEX `fk_edit_table_column_logs_table_name_idx` ON `edit_table_column_logs` (`table_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212074631_add_indexs_in_edit_logs') THEN

    CREATE INDEX `fk_edit_table_column_logs_tableid_idx` ON `edit_table_column_logs` (`table_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212074631_add_indexs_in_edit_logs') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231212074631_add_indexs_in_edit_logs', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212075935_add_indexs_in_edit_logs_fix_issue_3') THEN

    ALTER TABLE `edit_table_column_logs` RENAME INDEX `fk_edit_table_column_logs_tableid_idx` TO `edit_table_column_logs_tableid_idx`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212075935_add_indexs_in_edit_logs_fix_issue_3') THEN

    ALTER TABLE `edit_table_column_logs` RENAME INDEX `fk_edit_table_column_logs_table_name_idx` TO `edit_table_column_logs_table_name_idx`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212075935_add_indexs_in_edit_logs_fix_issue_3') THEN

    ALTER TABLE `edit_table_column_logs` RENAME INDEX `fk_edit_table_column_logs_table_idx` TO `edit_table_column_logs_table_idx`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212075935_add_indexs_in_edit_logs_fix_issue_3') THEN

    ALTER TABLE `edit_table_column_logs` RENAME INDEX `fk_edit_table_column_logs_column_name_idx` TO `edit_table_column_logs_column_name_idx`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212075935_add_indexs_in_edit_logs_fix_issue_3') THEN

    ALTER TABLE `edit_table_column_logs` RENAME INDEX `fk_edit_table_column_logs_column_idx` TO `edit_table_column_logs_column_idx`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231212075935_add_indexs_in_edit_logs_fix_issue_3') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231212075935_add_indexs_in_edit_logs_fix_issue_3', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231213052106_add_indexs_in_venue_transfer') THEN

    ALTER TABLE `venue_transfer_approval` MODIFY COLUMN `reviewed_by` int(11) NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231213052106_add_indexs_in_venue_transfer') THEN

    ALTER TABLE `edit_table_column_logs` ADD CONSTRAINT `fk_edit_table_column_logs` FOREIGN KEY (`edited_by`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231213052106_add_indexs_in_venue_transfer') THEN

    ALTER TABLE `venue_transfer_approval` ADD CONSTRAINT `fk_venue_transfer_approval_current_venue` FOREIGN KEY (`current_venue_id`) REFERENCES `venue` (`Id`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231213052106_add_indexs_in_venue_transfer') THEN

    ALTER TABLE `venue_transfer_approval` ADD CONSTRAINT `fk_venue_transfer_approval_khidmatguzaar` FOREIGN KEY (`employeeIts`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231213052106_add_indexs_in_venue_transfer') THEN

    ALTER TABLE `venue_transfer_approval` ADD CONSTRAINT `fk_venue_transfer_approval_khidmatguzaar1` FOREIGN KEY (`requested_by`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231213052106_add_indexs_in_venue_transfer') THEN

    ALTER TABLE `venue_transfer_approval` ADD CONSTRAINT `fk_venue_transfer_approval_khidmatguzaar2` FOREIGN KEY (`reviewed_by`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231213052106_add_indexs_in_venue_transfer') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231213052106_add_indexs_in_venue_transfer', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231220053900_edit_kg_and_bank_details') THEN

    ALTER TABLE `employee_bank_details` ADD `isDefault` tinyint(1) NOT NULL DEFAULT FALSE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231220053900_edit_kg_and_bank_details') THEN

    ALTER TABLE `khidmat_guzaar` ADD `workType` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231220053900_edit_kg_and_bank_details') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231220053900_edit_kg_and_bank_details', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231223041605_modify_edit_table_log_and_mzlm_pkg') THEN

    ALTER TABLE `edit_table_column_logs` DROP INDEX `edit_table_column_logs_column_idx`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231223041605_modify_edit_table_log_and_mzlm_pkg') THEN

    ALTER TABLE `edit_table_column_logs` DROP INDEX `edit_table_column_logs_table_idx`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231223041605_modify_edit_table_log_and_mzlm_pkg') THEN

    ALTER TABLE `edit_table_column_logs` DROP INDEX `edit_table_column_logs_tableid_idx`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231223041605_modify_edit_table_log_and_mzlm_pkg') THEN

    ALTER TABLE `edit_table_column_logs` DROP COLUMN `column_id`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231223041605_modify_edit_table_log_and_mzlm_pkg') THEN

    ALTER TABLE `edit_table_column_logs` DROP COLUMN `table_id`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231223041605_modify_edit_table_log_and_mzlm_pkg') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231223041605_modify_edit_table_log_and_mzlm_pkg', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231223041740_modify_mzlm_pkg') THEN

    ALTER TABLE `mzlm_leave_package` ADD `leaveBulkAssignationJson` json NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231223041740_modify_mzlm_pkg') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231223041740_modify_mzlm_pkg', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231223061916_modify_edit_table_logs') THEN

    ALTER TABLE `edit_table_column_logs` MODIFY COLUMN `table_name` varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231223061916_modify_edit_table_logs') THEN

    ALTER TABLE `edit_table_column_logs` MODIFY COLUMN `column_name` varchar(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231223061916_modify_edit_table_logs') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231223061916_modify_edit_table_logs', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231225040453_modify_user_dept_venue_add_user_venue') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231225040453_modify_user_dept_venue_add_user_venue', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231225080039_modify_branch_user_venue_relation') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231225080039_modify_branch_user_venue_relation', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20231227054521_modify salary structure for branch login') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20231227054521_modify salary structure for branch login', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240101055521_modify_bank_and_salary_details_modified') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240101055521_modify_bank_and_salary_details_modified', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240102102515_modify_employee_salary_and_track_exit') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240102102515_modify_employee_salary_and_track_exit', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240103033437_modify_salary_allocation_with_holding') THEN

    ALTER TABLE `salary_allocation_hijri` ADD `withHoldings` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240103033437_modify_salary_allocation_with_holding') THEN

    ALTER TABLE `salary_allocation_gegorian` ADD `withHoldings` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240103033437_modify_salary_allocation_with_holding') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240103033437_modify_salary_allocation_with_holding', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240106114821_modify_salary_allocations_and_others_final') THEN

    ALTER TABLE `employee_salary` DROP COLUMN `otherDeductions`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240106114821_modify_salary_allocations_and_others_final') THEN

    ALTER TABLE `employee_salary` DROP COLUMN `penalties`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240106114821_modify_salary_allocations_and_others_final') THEN

    ALTER TABLE `salary_allocation_hijri` CHANGE `penalties` `timeDelta` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240106114821_modify_salary_allocations_and_others_final') THEN

    ALTER TABLE `salary_allocation_hijri` CHANGE `otherDeductions` `shortfall` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240106114821_modify_salary_allocations_and_others_final') THEN

    ALTER TABLE `salary_allocation_gegorian` CHANGE `penalties` `timeDelta` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240106114821_modify_salary_allocations_and_others_final') THEN

    ALTER TABLE `salary_allocation_gegorian` CHANGE `otherDeductions` `shortfall` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240106114821_modify_salary_allocations_and_others_final') THEN

    ALTER TABLE `salary_allocation_hijri` ADD `dayDelta` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240106114821_modify_salary_allocations_and_others_final') THEN

    ALTER TABLE `salary_allocation_hijri` ADD `overtime` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240106114821_modify_salary_allocations_and_others_final') THEN

    ALTER TABLE `salary_allocation_gegorian` ADD `dayDelta` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240106114821_modify_salary_allocations_and_others_final') THEN

    ALTER TABLE `salary_allocation_gegorian` ADD `overtime` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240106114821_modify_salary_allocations_and_others_final') THEN

    ALTER TABLE `salary_allocation_gegorian` ADD `qismRemarks` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240106114821_modify_salary_allocations_and_others_final') THEN

    ALTER TABLE `salary_allocation_gegorian` ADD `systemRemarks` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240106114821_modify_salary_allocations_and_others_final') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240106114821_modify_salary_allocations_and_others_final', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240110054551_add_kg_venue_transfer_history') THEN

    CREATE TABLE `kg_venue_transfer_history` (
        `id` int(11) NOT NULL AUTO_INCREMENT,
        `itsId` int(11) NOT NULL,
        `mauze` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `idara` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `dojDate` datetime NOT NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
        CONSTRAINT `fk_kg_venue_tranfer_history_its` FOREIGN KEY (`itsId`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240110054551_add_kg_venue_transfer_history') THEN

    CREATE INDEX `IX_kg_venue_transfer_history_itsId` ON `kg_venue_transfer_history` (`itsId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240110054551_add_kg_venue_transfer_history') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240110054551_add_kg_venue_transfer_history', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240125055522_add_salarycalender_to_kg') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240125055522_add_salarycalender_to_kg', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240207053617_add_remarks_in_salary_allocation_hijri') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240207053617_add_remarks_in_salary_allocation_hijri', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240310071421_add_password_batchId_category') THEN

    ALTER TABLE `khidmat_guzaar` ADD `batchId` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240310071421_add_password_batchId_category') THEN

    ALTER TABLE `khidmat_guzaar` ADD `category` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240310071421_add_password_batchId_category') THEN

    ALTER TABLE `khidmat_guzaar` ADD `password` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240310071421_add_password_batchId_category') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240310071421_add_password_batchId_category', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240516062944_edit_dept_venue_model_builder') THEN

    ALTER TABLE `dept_venue` DROP FOREIGN KEY `fk_dept_venue_qism_al_tahfeez`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240516062944_edit_dept_venue_model_builder') THEN

    ALTER TABLE `nisaab_alumni` MODIFY COLUMN `kgIts` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240516062944_edit_dept_venue_model_builder') THEN

    ALTER TABLE `dept_venue` ADD CONSTRAINT `fk_dept_venue_qism_al_tahfeez` FOREIGN KEY (`qismId`) REFERENCES `qism_al_tahfeez` (`id`) ON DELETE SET NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240516062944_edit_dept_venue_model_builder') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240516062944_edit_dept_venue_model_builder', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240518054510_edit_branch_user_model_builder_2') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240518054510_edit_branch_user_model_builder_2', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240529054553_add_sholarship_and_wafd_khidmat_table') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240529054553_add_sholarship_and_wafd_khidmat_table', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240529060111_add_contenttypetable') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240529060111_add_contenttypetable', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240529061820_add_nisabstudentlog') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240529061820_add_nisabstudentlog', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240529065819_add_report_rights_fitness') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240529065819_add_report_rights_fitness', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240529070222_add_report_names') THEN

    CREATE TABLE `reports_names` (
        `id` int NOT NULL AUTO_INCREMENT,
        `name` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `description` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240529070222_add_report_names') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240529070222_add_report_names', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240529072300_add_bmi_data') THEN

    CREATE TABLE `bmi_data` (
        `id` int NOT NULL AUTO_INCREMENT,
        `itsId` int NULL,
        `height_in_cemtimeter` float NULL,
        `weight_in_kilogram` float NULL,
        `createdOn` datetime(6) NULL,
        `bmi` float NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240529072300_add_bmi_data') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240529072300_add_bmi_data', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240529091925_add_currency_converter_new') THEN

    CREATE TABLE `currency_converter_new` (
        `id` int NOT NULL AUTO_INCREMENT,
        `fromCurrencyName` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `toCurrencyName` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `value` float NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240529091925_add_currency_converter_new') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240529091925_add_currency_converter_new', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240530112507_add_student_registration_rights') THEN

    CREATE TABLE `student_registration_rights` (
        `id` int NOT NULL AUTO_INCREMENT,
        `programSetId` int NULL,
        `itsId` int NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240530112507_add_student_registration_rights') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240530112507_add_student_registration_rights', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240603031333_add_alloted_min_in_min_entry') THEN

    ALTER TABLE `azwaaj_minentry` ADD `allotedMin` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240603031333_add_alloted_min_in_min_entry') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240603031333_add_alloted_min_in_min_entry', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240605064402_add_alloted_amount_in_min_entry') THEN

    ALTER TABLE `azwaaj_minentry` ADD `rate` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240605064402_add_alloted_amount_in_min_entry') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240605064402_add_alloted_amount_in_min_entry', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240605133410_change_to_floate_alloted_rate') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240605133410_change_to_floate_alloted_rate', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240611111643_add_academic_year_in_class') THEN

    ALTER TABLE `training_class` ADD `academicYear` int(11) NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240611111643_add_academic_year_in_class') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240611111643_add_academic_year_in_class', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240626072524_azwaaj_min_entry_rate_float') THEN

    DROP TABLE `form_questionnaire`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240626072524_azwaaj_min_entry_rate_float') THEN

    DROP TABLE `form_response`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240626072524_azwaaj_min_entry_rate_float') THEN

    DROP TABLE `form`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240626072524_azwaaj_min_entry_rate_float') THEN

    ALTER TABLE `azwaaj_minentry` MODIFY COLUMN `rate` float NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240626072524_azwaaj_min_entry_rate_float') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240626072524_azwaaj_min_entry_rate_float', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240629114102_back_attachment_column') THEN

    ALTER TABLE `kg_identitycards` ADD `back_attachment` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240629114102_back_attachment_column') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240629114102_back_attachment_column', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240730105351_add_image_upload_leave_package') THEN

    ALTER TABLE `mzlm_leave_package` ADD `UploadedDocumentUrl` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240730105351_add_image_upload_leave_package') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240730105351_add_image_upload_leave_package', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240809053133_add_receipt_doc_url') THEN

    ALTER TABLE `mz_student_receipt` ADD `recieptUrl` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240809053133_add_receipt_doc_url') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240809053133_add_receipt_doc_url', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `whatsappNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `watanArabic` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `watanAdress` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `watan` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `photoBase64` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `photo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `personalHouseType` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `personalHouseStatus` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `personalHouseArea` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `personalHouseAddress` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `panCardNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `officialEmailAddress` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `nationality` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `mz_idara` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `muqamArabic` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `muqam` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `mobileNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `maritalStatus` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `krNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `jamiat` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `jaman` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `jamaat` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `its_preferredIdara` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `its_idaras` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `fullName` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `emailAddress` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `domicileParent` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `domicileAddressParents` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `dojHijri` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `dobHijri` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `dobGregorian` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `dawat_title` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `c_codeWhatsapp` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `c_codeMobile` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    ALTER TABLE `khidmat_guzaar` MODIFY COLUMN `bloodGroup` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812063924_kg_charsetmodification') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240812063924_kg_charsetmodification', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812105111_kg_personalitytypeReport') THEN

    ALTER TABLE `kg_self_assessment` MODIFY COLUMN `personalityReport` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240812105111_kg_personalitytypeReport') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240812105111_kg_personalitytypeReport', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240823103653_relate_kg_qualification') THEN

    ALTER TABLE `wafdprofile_qualification_new` MODIFY COLUMN `itsid` int(11) NOT NULL DEFAULT 0;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240823103653_relate_kg_qualification') THEN

    CREATE INDEX `IX_wafdprofile_qualification_new_itsid` ON `wafdprofile_qualification_new` (`itsid`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240823103653_relate_kg_qualification') THEN

    ALTER TABLE `wafdprofile_qualification_new` ADD CONSTRAINT `fk_qualification_kg` FOREIGN KEY (`itsid`) REFERENCES `khidmat_guzaar` (`itsId`) ON DELETE CASCADE;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240823103653_relate_kg_qualification') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240823103653_relate_kg_qualification', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240909140838_add bank master and student logs table') THEN

    CREATE TABLE `bankmaster` (
        `Id` bigint NOT NULL AUTO_INCREMENT,
        `BankId` int NOT NULL,
        `BankName` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`Id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240909140838_add bank master and student logs table') THEN

    CREATE TABLE `mz_student_log_type` (
        `id` int NOT NULL AUTO_INCREMENT,
        `name` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `description` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240909140838_add bank master and student logs table') THEN

    CREATE TABLE `mz_student_logs` (
        `id` int NOT NULL AUTO_INCREMENT,
        `typeId` int NULL,
        `studentId` int NULL,
        `description` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `createdBy` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `createdOn` datetime(6) NOT NULL,
        `currentObject` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `changes` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
        `logId` int NULL,
        CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8 COLLATE=utf8_general_ci;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20240909140838_add bank master and student logs table') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20240909140838_add bank master and student logs table', '8.0.5');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

