--CREATE TABLE T_TURNOS
--( id int identity(1,1) NOT NULL,
--  fecha varchar(10) NULL,
--  hora varchar(5) NULL,
--  cliente varchar(100) NULL
--  CONSTRAINT PK_T_TURNOS PRIMARY KEY CLUSTERED (id)
--)

--CREATE TABLE T_SERVICIOS
--( id int NOT NULL,
--  nombre varchar(50) NOT NULL,
--  costo int NOT NULL,
--  enPromocion varchar(1) NOT NULL
--  CONSTRAINT PK_T_SERVICIOS PRIMARY KEY CLUSTERED (id)
--)

--CREATE TABLE T_DETALLES_TURNO
--( id_turno int NOT NULL,
--  id_servicio int NOT NULL,
--  observaciones varchar(200) NULL
--  CONSTRAINT PK_T_DETALLES_TURNO PRIMARY KEY CLUSTERED (id_turno, id_servicio)
--)
	
--SELECT * FROM T_SERVICIOS
--INSERT INTO T_SERVICIOS(id, nombre, costo, enPromocion, Estado)
--VALUES(1, 'Lavado Completo', 20000, 'S', 0)

SELECT * from T_TURNOS
SELECT * from T_DETALLES_TURNO
SELECT *from T_SERVICIOS