USE [austin_stock_processor]
GO
/****** Object:  StoredProcedure [fsn].[IEXTrading_Get_Symbol_For_Chart]    Script Date: 2/21/2018 9:57:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [fsn].[Get_Symbol_For_Chart_5y]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT TOP 1
	  s_iex.symbol as Symbol
	FROM fsn.Symbol symbol,
		 fsn.Symbol_IEXTrading s_iex
	WHERE symbol.Symbol = s_iex.Symbol

	AND (
			DATEDIFF(SECOND, { D '1970-01-01' }, GETUTCDATE())
			- s_iex.Chart_5y
			) > (365 * 24 * 60 * 60)

	AND (ABS(CAST((BINARY_CHECKSUM(*) * RAND()) AS int)) % 100) < 10;
		
END

