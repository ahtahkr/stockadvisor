USE [austin_stock_processor]
GO
/****** Object:  StoredProcedure [fsn].[IEXTrading_Get_Symbol_For_Chart]    Script Date: 2/21/2018 9:57:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [fsn].[Symbol_IEXTrading_Update_Chart]
	@Symbol varchar(10) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	update fsn.Symbol_IEXTrading set Chart = DATEDIFF(SECOND, { D '1970-01-01' }, GETUTCDATE()) where Symbol = @Symbol;
		
	Select 0 as Result;
END



