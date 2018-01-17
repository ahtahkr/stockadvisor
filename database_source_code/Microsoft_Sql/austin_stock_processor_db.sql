USE [master]
GO
/****** Object:  Database [austin_stock_processor]    Script Date: 1/17/2018 4:31:57 PM ******/
CREATE DATABASE [austin_stock_processor]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'austin_stock_processor', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\austin_stock_processor.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'austin_stock_processor_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\austin_stock_processor_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [austin_stock_processor] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [austin_stock_processor].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [austin_stock_processor] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [austin_stock_processor] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [austin_stock_processor] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [austin_stock_processor] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [austin_stock_processor] SET ARITHABORT OFF 
GO
ALTER DATABASE [austin_stock_processor] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [austin_stock_processor] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [austin_stock_processor] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [austin_stock_processor] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [austin_stock_processor] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [austin_stock_processor] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [austin_stock_processor] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [austin_stock_processor] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [austin_stock_processor] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [austin_stock_processor] SET  ENABLE_BROKER 
GO
ALTER DATABASE [austin_stock_processor] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [austin_stock_processor] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [austin_stock_processor] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [austin_stock_processor] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [austin_stock_processor] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [austin_stock_processor] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [austin_stock_processor] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [austin_stock_processor] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [austin_stock_processor] SET  MULTI_USER 
GO
ALTER DATABASE [austin_stock_processor] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [austin_stock_processor] SET DB_CHAINING OFF 
GO
ALTER DATABASE [austin_stock_processor] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [austin_stock_processor] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [austin_stock_processor] SET DELAYED_DURABILITY = DISABLED 
GO
USE [austin_stock_processor]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 1/17/2018 4:31:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Company](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ticker] [varchar](32) NOT NULL,
	[Name] [varchar](32) NULL,
	[Lei] [varchar](32) NULL,
	[Cik] [varchar](32) NULL,
	[Latest_Filing_Date] [date] NULL,
	[Update_Timestamp] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Ticker] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Person]    Script Date: 1/17/2018 4:31:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Person](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Access_Key] [varchar](32) NULL,
PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[Auth_Company_Get_Unique_Ticker]    Script Date: 1/17/2018 4:31:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Auth_Company_Get_Unique_Ticker]
	@EmailAddress VARCHAR(50), 
	@AccessKey VARCHAR(32)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if Exists(select * from dbo.Person where Email = @EmailAddress and Access_Key = CONVERT(VARCHAR(32), HashBytes('MD5', @AccessKey), 2))
    begin
		select distinct Ticker from dbo.Company;
	end
    else
    begin
		select '' as Ticker;
    end
    
END

GO
/****** Object:  StoredProcedure [dbo].[Auth_Company_Insert_Update]    Script Date: 1/17/2018 4:31:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Auth_Company_Insert_Update]
	@EmailAddress varchar(50),
	@AccessKey varchar(50),
	@TickerInput varchar(32),
	@NameInput varchar(32),
	@LeiInput varchar(32),
	@CikInput varchar(32),
	@LatestFilingDate_ date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if Exists(select * from dbo.Person where Email = @EmailAddress and Access_Key = CONVERT(VARCHAR(32), HashBytes('MD5', @AccessKey), 2))
    begin
		exec dbo.Company_Insert_Update @Ticker_Input=@TickerInput,@Name_Input=@NameInput,@Lei_Input=@LeiInput,@Cik_Input=@CikInput,@LatestFilingDate=@LatestFilingDate_;
	end
    else
    begin
		select -1;
    end    
END

GO
/****** Object:  StoredProcedure [dbo].[Company_Insert_Update]    Script Date: 1/17/2018 4:31:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Company_Insert_Update]
	@Ticker_Input varchar(32),
	@Name_Input varchar(32),
	@Lei_Input varchar(32),
	@Cik_Input varchar(32),
	@LatestFilingDate date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if Exists(select * from dbo.Company where Ticker = @Ticker_Input)
    begin
		Update Company set Name = @Name_Input, Lei=@Lei_Input, Cik=@Cik_Input,Latest_Filing_Date=@LatestFilingDate, Update_Timestamp=DATEDIFF(SECOND,{d '1970-01-01'}, GETDATE()) where Ticker = @Ticker_Input;
        select 0;
	end
    else
    begin
		Insert into Company(Ticker,Name,Lei,Cik,Latest_Filing_Date,Update_Timestamp) values (@Ticker_Input,@Name_Input,@Lei_Input,@Cik_Input,@LatestFilingDate,DATEDIFF(SECOND,{d '1970-01-01'}, GETDATE()));
        select 0;
    end    
END

GO
/****** Object:  StoredProcedure [dbo].[Person_Insert]    Script Date: 1/17/2018 4:31:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Person_Insert]
	@EmailAddress varchar(50),
	@AccessKey varchar(32)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Insert into Person(Email, Access_Key) values (@EmailAddress, CONVERT(VARCHAR(32), HashBytes('MD5', @AccessKey), 2));

END

GO
/****** Object:  StoredProcedure [dbo].[Person_Validate]    Script Date: 1/17/2018 4:31:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Person_Validate]
	@EmailAddress varchar(50),
	@AccessKey varchar(32)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select count(*) as [count] from dbo.Person where Email = @EmailAddress and Access_Key = CONVERT(VARCHAR(32), HashBytes('MD5', @AccessKey), 2);    
END

GO
USE [master]
GO
ALTER DATABASE [austin_stock_processor] SET  READ_WRITE 
GO
