use austin_db;

DELIMITER //
CREATE PROCEDURE Person_Validate
(IN EmailAddress VARCHAR(50), IN AccessKey VARCHAR(32))
BEGIN
    Select count(*) FROM Person Where Email = EmailAddress and Access_Key = MD5(AccessKey);    
    
END //
DELIMITER ;