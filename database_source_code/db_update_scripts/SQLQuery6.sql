USE [austin_stock_processor]
GO
/****** Object:  StoredProcedure [fsn].[Share_Insert_Update_Close]    Script Date: 2/21/2018 11:16:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [fsn].[Share_Insert_Update_Close]
	@Symbol varchar(32) = '',
	@Close [decimal](10, 5) = 0.0,
	@Date DateTime = NULL
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if (@Date is null)
	begin
		Select 0 as Result;
		Return;
	end 

	declare @Days int;
	select @Days = DateDiff( day, cast(@Date as date), cast(GetUTCDate() as date))
	if (@Days > 5)
	begin
		Select 0 as Result;
		Return;
	end

	declare @date_ Date;
	select @date = cast(@Date as date);

	declare @time time;
	select @time = '23:59';

	select @Date = DATEADD(ms, DATEDIFF(ms, '00:00:00', @time), CONVERT(DATETIME, @date));


	
	
	if Not Exists(select * from fsn.Symbol where Symbol = @Symbol)
	begin
		INSERT INTO [fsn].[Symbol]
           ([Symbol]
           ,[Name]
           ,[IsEnabled]
           ,[Type]
           ,[IecId]
           ,[Update_Timestamp]
           ,[IEXTrading]
		   ,[Get_Share_Data])
     VALUES
           (@Symbol
           ,''
           ,1
           ,''
           ,0
           ,DATEDIFF(SECOND,{d '1970-01-01'}, GETDATE())
           ,1
		   ,0)
	end 


    if Exists(select * from fsn.Share where Symbol = @Symbol and [Date] = @Date)
    begin
		Update fsn.Share set [Close] = @Close where Symbol = @Symbol and [Date] = @Date;
        select 0 as Result;
		Return;
	end
    else
    begin
		INSERT INTO [fsn].[Share]
           ([Symbol]
           ,[Date]
           ,[Open]
           ,[High]
           ,[Low]
           ,[Close]
           ,[Volume]
           ,[UnadjustedVolume]
           ,[Change]
           ,[ChangePercent]
           ,[Vwap])
     VALUES
           (@Symbol
           ,@Date
           ,0
           ,0
           ,0
           ,@Close
           ,0
           ,0
           ,0
           ,0
           ,0)
        select 0 as Result;
		Return;
    end 
END

