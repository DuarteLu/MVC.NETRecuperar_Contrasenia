create database UnaTabla

use UnaTabla

create table Usuario(
idCliente int identity(1,1),
Email varchar(50) ,
Password varchar(50),
Token varchar(50),
FechaCreacion datetime ,
constraint PK_idcliente PRIMARY KEY (idCliente))

insert into Usuario(Email,Password,Token,FechaCreacion)
values('AyudandoPandemia@gmail.com','Pato','','24/05/2020');



