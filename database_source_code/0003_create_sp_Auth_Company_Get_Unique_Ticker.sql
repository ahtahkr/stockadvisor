use austin_db;

DELIMITER //
CREATE PROCEDURE Auth_Company_Get_Unique_Ticker
(IN EmailAddress VARCHAR(50), IN AccessKey VARCHAR(32))
BEGIN
    if (select 1=1 from Person where Email = Email_Input and Access_Key = md5(AccessKey)) then
    begin
		select distinct Ticker from Company;
	end;
    else
    begin
		select '' as Ticker;
    end;
    end if;    
    
END //
DELIMITER ;