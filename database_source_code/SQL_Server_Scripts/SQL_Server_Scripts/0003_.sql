use austin_stock_processor;

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.Company_Insert_Update
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