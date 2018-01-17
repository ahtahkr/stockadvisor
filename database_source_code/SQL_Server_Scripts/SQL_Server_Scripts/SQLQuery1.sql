use austin_stock_processor;

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.Auth_Company_Get_Unique_Ticker
	@EmailAddress varchar(50),
	@AccessKey varchar(32)
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

