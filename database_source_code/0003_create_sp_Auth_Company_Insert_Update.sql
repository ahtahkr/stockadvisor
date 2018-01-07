use austin_db;

DELIMITER //
create PROCEDURE Auth_Company_Insert_Update
(IN Email_Input varchar(50), IN AccessKey varchar(50), Ticker_Input varchar(32),IN Name_Input varchar(32),IN Lei_Input varchar(32),IN Cik_Input varchar(32),IN LatestFilingDate date)
BEGIN
	if (select 1=1 from Person where Email = Email_Input and Access_Key = md5(AccessKey)) then
    begin
		call Company_Insert_Update(Email_Input,AccessKey,Ticker_Input,Name_Input,Lei_Input,Cik_Input,LatestFilingDate);
	end;
    else
    begin
		select -1;
    end;
    end if;
END //
DELIMITER ;

