CREATE SCHEMA `library` ;

CREATE TABLE `library`.`user` (
  `userID` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`userID`));

CREATE TABLE `library`.`author` (
  `authorID` INT NOT NULL AUTO_INCREMENT,
  `authorName` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`authorID`),
  UNIQUE INDEX `authorName_UNIQUE` (`authorName` ASC));

CREATE TABLE `library`.`book` (
  `bookID` INT NOT NULL AUTO_INCREMENT,
  `bookName` VARCHAR(45) NOT NULL,
  `authorName` VARCHAR(45) NULL,
  PRIMARY KEY (`bookID`),
  UNIQUE INDEX `bookName_UNIQUE` (`bookName` ASC),
  INDEX `FK_authorName_idx` (`authorName` ASC),
  CONSTRAINT `FK_authorName`
    FOREIGN KEY (`authorName`)
    REFERENCES `library`.`author` (`authorName`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

CREATE TABLE `library`.`readbook` (
  `readBookID` INT NOT NULL AUTO_INCREMENT,
  `userID` INT NOT NULL,
  `bookID` INT NOT NULL,
  `rating` INT NOT NULL,
  `review` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`readBookID`),
  INDEX `FK_UserID_idx` (`userID` ASC),
  INDEX `FK_BookID_idx` (`bookID` ASC),
  CONSTRAINT `FK_UserID`
    FOREIGN KEY (`userID`)
    REFERENCES `library`.`user` (`userID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `FK_BookID`
    FOREIGN KEY (`bookID`)
    REFERENCES `library`.`book` (`bookID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

CREATE TABLE `library`.`wishlist` (
  `wishlistID` INT NOT NULL AUTO_INCREMENT,
  `userID` INT NULL,
  `bookID` INT NULL,
  PRIMARY KEY (`wishlistID`),
  INDEX `FK_UserID_idx` (`userID` ASC),
  INDEX `FK_BookID_idx` (`bookID` ASC),
  CONSTRAINT `FK_UserID1`
    FOREIGN KEY (`userID`)
    REFERENCES `library`.`user` (`userID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `FK_BookID1`
    FOREIGN KEY (`bookID`)
    REFERENCES `library`.`book` (`bookID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

ALTER TABLE `library`.`user` 
ADD COLUMN `type` VARCHAR(45) NOT NULL AFTER `password`;	
	
INSERT INTO `library`.`author` (`authorName`) VALUES ('F. M. Dostoievski');
INSERT INTO `library`.`author` (`authorName`) VALUES ('Harper Lee');
INSERT INTO `library`.`author` (`authorName`) VALUES ('Lev Tolstoi');
INSERT INTO `library`.`author` (`authorName`) VALUES ('Dante Alighieri');
INSERT INTO `library`.`author` (`authorName`) VALUES ('George Orwell');
INSERT INTO `library`.`author` (`authorName`) VALUES ('James Joyce');

INSERT INTO `library`.`book` (`bookName`, `authorName`) VALUES ('Brothers Karamazov', 'F. M. Dostoievski');
INSERT INTO `library`.`book` (`bookName`, `authorName`) VALUES ('Crime and punishment', 'F. M. Dostoievski');
INSERT INTO `library`.`book` (`bookName`, `authorName`) VALUES ('The Idiot', 'F. M. Dostoievski');
INSERT INTO `library`.`book` (`bookName`, `authorName`) VALUES ('To kill a mockingbird', 'Harper Lee');
INSERT INTO `library`.`book` (`bookName`, `authorName`) VALUES ('Anna Karenina', 'Lev Tolstoi');
INSERT INTO `library`.`book` (`bookName`, `authorName`) VALUES ('War and Piece', 'Lev Tolstoi');
INSERT INTO `library`.`book` (`bookName`, `authorName`) VALUES ('Divina Comedia', 'Dante Alighieri');
INSERT INTO `library`.`book` (`bookName`, `authorName`) VALUES ('1984', 'George Orwell');
INSERT INTO `library`.`book` (`bookName`, `authorName`) VALUES ('Ulyses', 'James Joyce');

INSERT INTO `library`.`user` (`name`, `email`, `password`, `type`) VALUES ('Marian', 'm@gmail.com', '123', 'admin');
INSERT INTO `library`.`user` (`name`, `email`, `password`, `type`) VALUES ('Anca', 'a@gmail.com', '123', 'user');
INSERT INTO `library`.`user` (`name`, `email`, `password`, `type`) VALUES ('Calin', 'c@gmail.com', '123', 'user');
INSERT INTO `library`.`user` (`name`, `email`, `password`, `type`) VALUES ('Alexandra', 'al@ymail.com', '123', 'user');
INSERT INTO `library`.`user` (`name`, `email`, `password`, `type`) VALUES ('Cosmin', 'cos@yahoo.com', '123', 'admin');

