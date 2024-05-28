create database NetCoreTemplateDb
alter database NetCoreTemplateDb set recovery simple
go

use NetCoreTemplateDb
go

create table Users
(
	Id int not null identity constraint PK_Users primary key,
	[Login] nvarchar(200) constraint Unique_Users_Login unique not null,
	[Password] nvarchar(max) not null,
	RoleId int not null,
	IsBlocked bit not null,
	RegistrationDate datetime not null,
)

insert into Users values
('dev', '', 0, 0, GETDATE()),
('admin', '', 1, 0, GETDATE());
go
