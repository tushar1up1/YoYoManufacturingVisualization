/* 
 * YoYo data table
 */

BEGIN
	create table yoyoData (
	recordNo int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	scheduleID nchar(50) NOT NULL,
	workarea nchar(30),
	keydata nchar(50),
	line_no nchar(30),
	station_name nchar(30),
	process_name nchar(30),
	process_date nchar(30)
)
END

/* 
 * User table
 */

BEGIN
	create table userTable (
	username nchar(50) NOT NULL PRIMARY KEY,
	userpassword nchar(50) NOT NULL,
	usertype nchar(30) NOT NULL,
)
END

/* 
 * SKU Schedule table
 */

BEGIN
	create table yoyoSchedule (
	SKU nchar(15) FOREIGN KEY REFERENCES yoyoSKU(SKU),
	scheduleID int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	SKUBegindate nchar(30),
	SKUEnddate nchar(30)
)
END
/* 
 * SKU table
 */

BEGIN
	create table yoyoSKU (
	SKU nchar(15) NOT NULL PRIMARY KEY,
	prdDescr nchar(50),
	color nchar(20),
)
END