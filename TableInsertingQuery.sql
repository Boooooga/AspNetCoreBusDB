/*
INSERT BUSES
	(Model, Capacity)
VALUES
	('Mercedes-Benz O325', 31),
	('ÃÀÇåëü Ñèòè', 20),
	('ÏÀÇ', 30),
	('Èêàðóñ', 70),
	('Ford Transit', 25)

INSERT Drivers
	(Name, Age, Experience)
VALUES
	('Ïåòðîâ Ï¸òð Ïåòðîâè÷', 42, 15),
	('Ñåðãååâ Ñåðãåé Ñåðãååâè÷', 55, 30),
	('Èâàíîâ Èâàí Èâàíîâè÷', 32, 11),
	('Êîðîë¸â Êîíñòàíòèí Êîíñòàíòèíîâè÷', 50, 22),
	('Ðóáë¸âà Óëüÿíà Àíäðååâíà', 25, 5),
	('Îðëîâ Îëåã Îëåãîâè÷', 39, 10)

INSERT Conductors
	(Name, Age)
VALUES
	('Èâàíîâà Àííà Èâàíîâíà', 34),
	('Ñèäîðîâà Ñâåòëàíà Ñåðãååâíà', 57),
	('Ïàâëîâà Ïîëèíà Ïàâëîâíà', 44)

INSERT Mechanics
	(Name, Age)
VALUES
	('×åðíîâ Ãåííàäèé Ãåííàäüåâè÷', 50),
	('Áåëîâ Âèêòîð Âàñèëüåâè÷', 48),
	('Ñåðîâ Ñåì¸í Èãíàòüåâè÷', 56)

INSERT Stops
	(Name, Adress)
VALUES
	('Èëüèíñêàÿ', 'Ïë.Èëüèíñêàÿ, 1Á'),
	('Áåëîãëèíñêàÿ', 'Óë.×àïàåâà, 14/26'),
	('Ðàáî÷àÿ', 'Óë.×àïàåâà, 32/36'),
	('Ìè÷óðèíà', 'Óë.×àïàåâà, 38/40'),
	('Ñîâåòñêàÿ', 'Óë.×àïàåâà, 48/47'),
	('Êðûòûé ðûíîê', 'Ïð-êò Ñòîëûïèíà, 43'),
	('Ãëàâïî÷òàìò', 'Óë.Ìîñêîâñêàÿ, 109'),
	('Ðàõîâà', 'Óë.Ðàõîâà, 146')

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
	('Íîâîóçåíñêàÿ', 'Óë.Ñåðîâà, 3'),
	('Øåëêîâè÷íàÿ', 'Óë.×åðíûøåâñêîãî, 109')


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
*/