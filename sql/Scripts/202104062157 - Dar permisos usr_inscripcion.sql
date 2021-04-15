use SMPorres
go
GRANT SELECT ON OBJECT::SMPorres..Cuotas TO usr_inscripcion
GRANT SELECT ON OBJECT::SMPorres..Usuarios TO usr_inscripcion
GRANT SELECT ON OBJECT::SMPorres..PlanesPago TO usr_inscripcion
GRANT SELECT ON OBJECT::SMPorres..Pagos TO usr_inscripcion
GRANT SELECT ON OBJECT::SMPorres..Configuracion TO usr_inscripcion
GRANT SELECT ON OBJECT::SMPorres..CursosAlumnos TO usr_inscripcion
GRANT SELECT ON OBJECT::SMPorres..Cursos TO usr_inscripcion
GRANT SELECT ON OBJECT::SMPorres..Carreras TO usr_inscripcion
GRANT SELECT ON OBJECT::SMPorres..TiposDocumento TO usr_inscripcion
GRANT SELECT ON OBJECT::SMPorres..Alumnos TO usr_inscripcion
GRANT INSERT ON OBJECT::SMPorres..Alumnos TO usr_inscripcion
GRANT INSERT ON OBJECT::SMPorres..CursosAlumnos TO usr_inscripcion
GRANT INSERT ON OBJECT::SMPorres..PlanesPago TO usr_inscripcion
GRANT INSERT ON OBJECT::SMPorres..Pagos TO usr_inscripcion