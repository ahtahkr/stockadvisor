use austin_db;

call Person_Insert('ashadhik@yahoo.com','adhikariyahoo');
select * from person;
Select count(*) as abc FROM Person;

call Auth_Company_Insert_Update('ashadhik@yahoo.com','adhikariyahoo','ABGBad',null,'test',null,null);
select * from company;