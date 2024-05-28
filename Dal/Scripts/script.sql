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

CREATE TABLE Buses (
ID int IDENTITY(1,1) NOT NULL
CONSTRAINT PK_Bus PRIMARY KEY,
Model varchar(50) NOT NULL,
Capacity int NOT NULL)

CREATE TABLE Drivers (
ID int IDENTITY(1,1) NOT NULL
CONSTRAINT PK_Driver PRIMARY KEY,
Name varchar(50) NOT NULL,
Age int NOT NULL,
Experience int NOT NULL)

CREATE TABLE Conductors (
ID int IDENTITY(1,1) NOT NULL
CONSTRAINT PK_Conductor PRIMARY KEY,
Name varchar(50) NOT NULL,
Age int NOT NULL)

CREATE TABLE BusRoutes (
ID int NOT NULL
CONSTRAINT PK_Route PRIMARY KEY,
StartTime time NOT NULL,
FinishTime time NOT NULL)

CREATE TABLE Stops (
ID int IDENTITY(1,1) NOT NULL
CONSTRAINT PK_Stop PRIMARY KEY,
Name varchar(50) NOT NULL,
Adress varchar(80) NOT NULL)

CREATE TABLE ArrivalTime (
ID int IDENTITY(1,1) NOT NULL
CONSTRAINT PK_ArrivalTime PRIMARY KEY,
Arrival time NOT NULL,
ID_BusRoute_Stop int NOT NULL)

CREATE TABLE Checkups (
ID int IDENTITY(1,1) NOT NULL
CONSTRAINT PK_Checkups PRIMARY KEY,
CheckupDate date NOT NULL,
IsOk bit NOT NULL,
ID_Bus int NOT NULL,
ID_Mechanic int NOT NULL)

CREATE TABLE Mechanics (
ID_Mechanic int IDENTITY(1,1) NOT NULL
CONSTRAINT PK_Mechanic PRIMARY KEY,
Name varchar(50) NOT NULL,
Age int NOT NULL)

CREATE TABLE BusRoutes_Stops (
ID_BusRoute_Stop int IDENTITY(1,1) NOT NULL
CONSTRAINT PK_BusRoute_Stop PRIMARY KEY,
ID_Stop int NOT NULL,
ID_Route int NOT NULL)

CREATE TABLE RouteList (
RouteDate date NOT NULL,
ID_Bus int NOT NULL,
ID_Driver int NOT NULL,
ID_Conductor int NOT NULL,
ID_Route int NOT NULL)


/* Создание связей для таблицы "Маршруты-Остановки" */
ALTER TABLE BusRoutes_Stops ADD
CONSTRAINT FK_BusRoutes_Stops_Stop
FOREIGN KEY (ID_Stop) REFERENCES Stops (ID_Stop)

ALTER TABLE BusRoutes_Stops ADD
CONSTRAINT FK_BusRoutes_Stops_BusRoute
FOREIGN KEY (ID_Route) REFERENCES BusRoutes(ID_Route)

/* Создание связей для таблицы "Время прибытия" */
ALTER TABLE ArrivalTime ADD
CONSTRAINT FK_ArrivalTime_BusRoutes_Stops
FOREIGN KEY (ID_BusRoute_Stop) REFERENCES BusRoutes_Stops(ID_BusRoute_Stop)

/* Создание связей для таблицы "Путевой лист" */
ALTER TABLE RouteList ADD
CONSTRAINT FK_RouteList_Bus
FOREIGN KEY (ID_Bus) REFERENCES Buses(ID_Bus)

ALTER TABLE RouteList ADD
CONSTRAINT FK_RouteList_Driver
FOREIGN KEY (ID_Driver) REFERENCES Drivers(ID_Driver)

ALTER TABLE RouteList ADD
CONSTRAINT FK_RouteList_Conductor
FOREIGN KEY (ID_Conductor) REFERENCES Conductors(ID_Conductor)

ALTER TABLE RouteList ADD
CONSTRAINT FK_RouteList_Route
FOREIGN KEY (ID_Route) REFERENCES BusRoutes(ID_Route)

/* Создание связей для таблицы "Техосмотр" */
ALTER TABLE Checkups ADD
CONSTRAINT FK_Checkups_Bus
FOREIGN KEY (ID_Bus) REFERENCES Buses(ID_Bus)

ALTER TABLE Checkups ADD
CONSTRAINT FK_Checkups_Mechanic
FOREIGN KEY (ID_Mechanic) REFERENCES Mechanics(ID_Mechanic)

INSERT BUSES
	(Model, Capacity)
VALUES
	('Mercedes-Benz O325', 31),
	('ГАЗель Сити', 20),
	('ПАЗ', 30),
	('Икарус', 70),
	('Ford Transit', 25)

INSERT Drivers
	(Name, Age, Experience)
VALUES
	('Петров Пётр Петрович', 42, 15),
	('Сергеев Сергей Сергеевич', 55, 30),
	('Иванов Иван Иванович', 32, 11),
	('Королёв Константин Константинович', 50, 22),
	('Рублёва Ульяна Андреевна', 25, 5),
	('Орлов Олег Олегович', 39, 10)

INSERT Conductors
	(Name, Age)
VALUES
	('Иванова Анна Ивановна', 34),
	('Сидорова Светлана Сергеевна', 57),
	('Павлова Полина Павловна', 44)

INSERT Mechanics
	(Name, Age)
VALUES
	('Чернов Геннадий Геннадьевич', 50),
	('Белов Виктор Васильевич', 48),
	('Серов Семён Игнатьевич', 56)

INSERT Stops
	(Name, Adress)
VALUES
	('Ильинская', 'Пл.Ильинская, 1Б'),
	('Белоглинская', 'Ул.Чапаева, 14/26'),
	('Рабочая', 'Ул.Чапаева, 32/36'),
	('Мичурина', 'Ул.Чапаева, 38/40'),
	('Советская', 'Ул.Чапаева, 48/47'),
	('Крытый рынок', 'Пр-кт Столыпина, 43'),
	('Главпочтамт', 'Ул.Московская, 109'),
	('Рахова', 'Ул.Рахова, 146')

INSERT BusRoutes
	(ID_Route, StartTime, FinishTime)
VALUES
	(90, '06:00:00', '21:30:00'),
	(6, '06:00:00', '22:00:00'),
	(79, '06:30:00', '21:00:00'),
	(15, '05:30:00', '22:30:00'),
	(53, '05:45:00', '22:15:00')


INSERT Stops
	(Name, Adress)
VALUES
	('Новоузенская', 'Ул.Серова, 3'),
	('Шелковичная', 'Ул.Чернышевского, 109')


INSERT BusRoutes_Stops
	(ID_Stop, ID_Route)
VALUES
	(1, 6),
	(1, 15),
	(1, 53),
	(1, 79),
	(1, 90),
	(2, 6),
	(2, 15),
	(2, 53),
	(2, 79),
	(2, 90),
	(3, 6),
	(3, 15),
	(3, 53),
	(3, 79),
	(3, 90),
	(4, 6),
	(4, 15),
	(4, 53),
	(4, 79),
	(4, 90),
	(5, 6),
	(5, 15),
	(5, 53),
	(5, 79),
	(5, 90),
	(6, 6),
	(6, 15),
	(6, 53),
	(6, 79),
	(6, 90),
	(7, 6),
	(7, 15),
	(7, 53),
	(7, 90),
	(8, 6),
	(8, 15),
	(8, 90),
	(9, 6),
	(9, 53),
	(9, 90),
	(10, 15),
	(10, 79)

INSERT Checkups
	(CheckupDate, IsOk, ID_Bus, ID_Mechanic)
VALUES
	('2024-03-10', 1, 1, 2),
	('2024-03-12', 0, 1, 1),
	('2024-02-09', 0, 4, 3)

INSERT RouteList
	(RouteDate, ID_Bus, ID_Driver, ID_Conductor, ID_Route)
VALUES
	('2024-03-01', 2, 2, 3, 79),
	('2024-03-01', 1, 1, 2, 90),
	('2024-03-01', 3, 3, NULL, 6),
	('2024-03-02', 5, 2, NULL, 79),
	('2024-03-02', 3, 4, NULL, 90),
	('2024-03-03', 1, 5, 1, 15),
	('2024-03-03', 2, 2, NULL, 53)