use austin_db;

DELIMITER //
create PROCEDURE Company_Insert_Update
(IN Ticker_Input varchar(32),IN Name_Input varchar(32),IN Lei_Input varchar(32),IN Cik_Input varchar(32),IN LatestFilingDate date)
BEGIN
	
    if (select 1=1 from Company where Ticker = Ticker_Input) then
    begin
        Update Company set Name = Name_Input, Lei=Lei_Input, Cik=Cik_Input,Latest_Filing_Date=LatestFilingDate, Update_Timestamp=unix_timestamp(utc_timestamp()) where Ticker = Ticker_Input;
        select 0;
    end;
    else
    begin
        Insert into Company(Ticker,Name,Lei,Cik,Latest_Filing_Date,Update_Timestamp) values (Ticker_Input,Name_Input,Lei_Input,Cik_Input,LatestFilingDate,unix_timestamp(utc_timestamp()));
        select 0;
    end;
    end if;
    
END //
DELIMITER ;

