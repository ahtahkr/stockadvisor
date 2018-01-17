use austin_db;

DELIMITER //
CREATE PROCEDURE Person_Insert
(IN EmailAddress VARCHAR(50), IN AccessKey VARCHAR(32))
BEGIN
	
    Insert into Person(Email, Access_Key) values (EmailAddress, MD5(AccessKey));
    
END //
DELIMITER ;