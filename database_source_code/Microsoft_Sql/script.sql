USE [master]
GO
/****** Object:  Database [austin_stock_processor]    Script Date: 2/13/2018 7:09:15 AM ******/
CREATE DATABASE [austin_stock_processor]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'austin_stock_processor', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\austin_stock_processor.mdf' , SIZE = 72704KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'austin_stock_processor_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\austin_stock_processor_log.ldf' , SIZE = 13888KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
ALTER DATABASE [austin_stock_processor] SET  DISABLE_BROKER 
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
EXEC sys.sp_db_vardecimal_storage_format N'austin_stock_processor', N'ON'
GO
ALTER DATABASE [austin_stock_processor] SET QUERY_STORE = OFF
GO
USE [austin_stock_processor]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [austin_stock_processor]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 2/13/2018 7:09:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ticker] [varchar](32) NOT NULL,
	[Name] [varchar](100) NULL,
	[Lei] [varchar](32) NULL,
	[Cik] [varchar](32) NULL,
	[Latest_Filing_Date] [date] NULL,
	[Next_In_Line] [int] NOT NULL,
	[Update_Timestamp] [int] NULL,
	[Share_Updated] [bigint] NULL,
	[Robinhood] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Ticker] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company_WebApi]    Script Date: 2/13/2018 7:09:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_WebApi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ticker] [varchar](32) NOT NULL,
	[AlphaVantage] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ticker] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 2/13/2018 7:09:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [dbo].[Share]    Script Date: 2/13/2018 7:09:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Share](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ticker] [varchar](32) NOT NULL,
	[date] [datetime] NOT NULL,
	[open] [decimal](7, 2) NOT NULL,
	[high] [decimal](7, 2) NOT NULL,
	[low] [decimal](7, 2) NOT NULL,
	[close] [decimal](7, 2) NOT NULL,
	[volume] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ticker] ASC,
	[date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((0)) FOR [Next_In_Line]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((1)) FOR [Robinhood]
GO
/****** Object:  StoredProcedure [dbo].[Auth_Company_Get_Unique_Ticker]    Script Date: 2/13/2018 7:09:16 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Auth_Company_Insert_Update]    Script Date: 2/13/2018 7:09:16 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Company_Get_Filed]    Script Date: 2/13/2018 7:09:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Company_Get_Filed]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select 
		company_filter.Name, company_filter.ticker Ticker, company_filter.latest_Filing_Date Latest_Filing_Date, company_filter.latest_share_date Latest_Share_Date, company_filter.robinhood Robinhood, share.[close] [Close], share.[open] [Open], share.[high] [High], share.[low] [Low], share.[volume] [Volume] 
		from 
		dbo.share share
		, (select 
				company.Ticker ticker, company.Name name, company.Latest_Filing_Date latest_Filing_Date, company.Update_Timestamp update_Timestamp, company.Robinhood robinhood, max(share.[date]) latest_share_date
			from 
				dbo.Company company, dbo.share share
			where Latest_Filing_Date is not null and datediff(month,latest_filing_date,getdate()) <= 6
				and share.Ticker = company.Ticker
			group by 
				company.Ticker, company.Name, company.Latest_Filing_Date, company.Update_Timestamp, company.Robinhood) company_filter
		where
			share.ticker = company_filter.ticker and share.[date] = company_filter.latest_share_date;
	    
END

GO
/****** Object:  StoredProcedure [dbo].[Company_Get_Robinhood]    Script Date: 2/13/2018 7:09:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Company_Get_Robinhood]
	@Open int = 200
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select 
		company_filter.Name, company_filter.ticker Ticker, company_filter.latest_Filing_Date Latest_Filing_Date, company_filter.latest_share_date Latest_Share_Date
		, company_filter.robinhood Robinhood, share.[close] [Close], share.[open] [Open], share.[high] [High], share.[low] [Low], share.[volume] [Volume] 
		from 
		dbo.share share
		, (select 
				company.Ticker ticker, company.Name name, company.Latest_Filing_Date latest_Filing_Date, company.Update_Timestamp update_Timestamp
				, company.Robinhood robinhood, max(share.[date]) latest_share_date
			from 
				dbo.Company company, 
				(select _share.* from 
					dbo.Share _share,
					(select ticker, max([date]) [date] from dbo.Share group by ticker) _share_1
				where 
					_share.Ticker = _share_1.Ticker 
					and _share.[date] = _share_1.[date]
					and _share.[close] <= @Open) share
			where share.Ticker = company.Ticker and company.Robinhood = 1
			group by 
				company.Ticker, company.Name, company.Latest_Filing_Date, company.Update_Timestamp, company.Robinhood) company_filter
		where
			share.ticker = company_filter.ticker and share.[date] = company_filter.latest_share_date;
	    
END

GO
/****** Object:  StoredProcedure [dbo].[Company_Get_Unique_Ticker]    Script Date: 2/13/2018 7:09:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Company_Get_Unique_Ticker]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @ticker varchar(32);
	declare @id int;
	
	if NOT Exists(Select * from dbo.Company where Next_In_Line = 1)
	begin
	update dbo.Company set next_in_line = 0;
	update dbo.Company set next_in_line = 1 
		where id = (
					select min(company.id) 
						from dbo.Company company, dbo.Company_WebApi webapi
						where company.Ticker = webapi.ticker and (webapi.alphavantage = 1 or webapi.alphavantage = 2) and 
						len(company.Ticker) > 0 and company.Robinhood = 1 
						and DATEDIFF(day, cast(dateadd(S, company.Share_Updated , '1970-01-01') as date), GETDATE()) > 0
					);
	end
	
	select @ticker = Ticker, @id = ID from dbo.Company where id = (select min(id) from dbo.Company where next_in_line = 1);

	update dbo.Company set Next_In_Line = 0 where ID = @id;
	
	if Exists(select min(company.id) from dbo.Company company, dbo.Company_WebApi webapi 
					where company.Ticker = webapi.ticker and (webapi.alphavantage = 1 or webapi.alphavantage = 2) 
					and company.id > @id and len(company.Ticker) > 0 and company.Robinhood = 1)
	begin
		update dbo.Company set Next_In_Line = 1 
			where ID =(select min(company.id) from dbo.Company company, dbo.Company_WebApi webapi 
					where company.Ticker = webapi.ticker and (webapi.alphavantage = 1 or webapi.alphavantage = 2) 
					and company.id > @id and len(company.Ticker) > 0 and company.Robinhood = 1);
	end
	else
	begin
		update dbo.Company set Next_In_Line = 1 
			where ID =(select min(company.id) from dbo.Company company, dbo.Company_WebApi webapi 
					where company.Ticker = webapi.ticker and (webapi.alphavantage = 1 or webapi.alphavantage = 2) 
					and len(company.Ticker) > 0 and company.Robinhood = 1);
	end
	
	Select @ticker as Ticker;	
END

GO
/****** Object:  StoredProcedure [dbo].[Company_Insert_Update]    Script Date: 2/13/2018 7:09:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Company_Insert_Update]
	@ticker varchar(32) = '',
	@name varchar(32) = '',
	@lei varchar(32) = '',
	@cik varchar(32) = '',
	@latest_filing_date date = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if Exists(select * from dbo.Company where Ticker = @ticker)
    begin
		Update Company set Name = @name, Lei=@lei, Cik=@cik,Latest_Filing_Date=@latest_filing_date, Update_Timestamp=DATEDIFF(SECOND,{d '1970-01-01'}, GETDATE()) where Ticker = @ticker;
        select 0;
	end
    else
    begin
		Insert into Company(Ticker,Name,Lei,Cik,Latest_Filing_Date,Update_Timestamp) values (@ticker, @name, @lei, @cik, @latest_filing_date,DATEDIFF(SECOND,{d '1970-01-01'}, GETDATE()));
        select 0;
    end    
END

GO
/****** Object:  StoredProcedure [dbo].[Company_Update_Robinhood]    Script Date: 2/13/2018 7:09:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Company_Update_Robinhood]
	@Ticker varchar(32) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if Exists(select * from dbo.Company where Ticker = @Ticker)
    begin
		declare @check bit;
		select @check = Robinhood from dbo.Company where Ticker = @Ticker;
		
		if (@check = 0)
		begin
			update dbo.Company set Robinhood = 1 where Ticker = @Ticker;
		end
		else
		begin
			update dbo.Company set Robinhood = 0 where Ticker = @Ticker;
		end
		select 0 as Result;
	end
    else
    begin
		select 1 as Result;
    end    
END

GO
/****** Object:  StoredProcedure [dbo].[Company_WebApi_AlphaVantage_Insert_Update]    Script Date: 2/13/2018 7:09:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Company_WebApi_AlphaVantage_Insert_Update]
	@ticker varchar(32) = '',
	@alpha bit = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if LEN(@ticker) < 1
	begin
		return -1;
	end

	if (@alpha = null)
	begin
		return -1;
	end

	if Exists(select ticker from dbo.Company_WebApi where Ticker = @ticker)
    begin
		Update dbo.Company_WebApi set AlphaVantage = @alpha where Ticker = @ticker;
		select 0 as result;

	end
    else
    begin
		INSERT INTO [dbo].[Company_WebApi]([Ticker],[AlphaVantage])VALUES(@ticker,2);
		select 1 as result;
    end	
END

GO
/****** Object:  StoredProcedure [dbo].[Person_Insert]    Script Date: 2/13/2018 7:09:16 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Person_Validate]    Script Date: 2/13/2018 7:09:16 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Share_Get]    Script Date: 2/13/2018 7:09:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[Share_Get]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select share.* from
			dbo.Share share 
			, (select ticker, max([date]) [date] from dbo.Share group by ticker) share_group
		where 
			share.Ticker = share_group.Ticker and share.date = share_group.date;
	    
END

GO
/****** Object:  StoredProcedure [dbo].[Share_Get_One]    Script Date: 2/13/2018 7:09:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Share_Get_One]
	@ticker varchar(32) = '',
	@start_date date = NULL,
	@end_date date = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if @start_date is null
	begin
		set @start_date = Cast(GetDate() as date);
	end

	if @end_date is null
	begin
		set @end_date = Cast(GetDate() as date);
	end

	Select * from dbo.Share where Ticker = @ticker and [date] <= @end_date and [date] >= @start_date;
	    
END

GO
/****** Object:  StoredProcedure [dbo].[Share_Get_Ticker]    Script Date: 2/13/2018 7:09:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Share_Get_Ticker]
	@ticker varchar(32) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select ID, Ticker, [open], high, low, [close], volume, [date], DATEDIFF(second,{d '1970-01-01'},[date]) unix_timestamp from
			dbo.Share
		where 
			Ticker = @ticker;
	    
END

GO
/****** Object:  StoredProcedure [dbo].[Share_Insert_Update]    Script Date: 2/13/2018 7:09:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Share_Insert_Update]
	@ticker varchar(32) = '',
	@_date datetime = NULL,
	@open decimal(7,2) = 0,
	@high decimal(7,2) = 0,
	@low decimal(7,2) = 0,
	@close decimal(7,2) = 0,
	@volume int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if Exists(select ticker from dbo.Share where Ticker = @ticker and [date] = @_date)
    begin
		Update dbo.Share set [open]=@open, [high]=@high, [low]=@low,[close]=@close,[volume]=@volume where Ticker = @ticker and [date] = @_date;
		Update dbo.Company set Share_Updated = DATEDIFF(SECOND,{d '1970-01-01'}, GETDATE()) where Ticker = @ticker;
        
		select @ticker as Ticker, 'Update' as result, @_date as _date;
	end
    else
    begin
		INSERT INTO [dbo].[Share]([Ticker],[date],[open],[high],[low],[close],[volume])VALUES(@Ticker,@_date,@open,@high,@low,@close,@volume);
		Update dbo.Company set Share_Updated = DATEDIFF(SECOND,{d '1970-01-01'}, GETDATE()) where Ticker = @ticker;       
		
		select @ticker as Ticker, 'Insert' as result, @_date as _date;
    end	
END

GO
USE [master]
GO
ALTER DATABASE [austin_stock_processor] SET  READ_WRITE 
GO
