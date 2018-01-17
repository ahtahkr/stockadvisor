--create database austin_stock_processor;
use austin_stock_processor;
GO
create table Person (ID int not null IDENTITY(1,1), Email VARCHAR(50) NOT NUll Primary Key, Access_Key varchar(32));
GO
create table Company(
	ID int not null IDENTITY(1,1)
	, Ticker varchar(32) Primary Key
    , Name varchar(32)
	, Lei varchar(32)
	, Cik varchar(32)
	, Latest_Filing_Date date
    , Update_Timestamp int);
GO
   

