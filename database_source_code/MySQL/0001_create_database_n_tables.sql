
create database austin_db;

use austin_db;

create table Person (ID int not null unique auto_increment, Email VARCHAR(50) NOT NUll Primary Key, Access_Key varchar(32));

create table Company(
	ID int not null unique auto_increment
	, Ticker varchar(32) not null unique
    , Name varchar(32)
	, Lei varchar(32)
	, Cik varchar(32)
	, Latest_Filing_Date date
    , Update_Timestamp int);
    
