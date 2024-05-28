
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

