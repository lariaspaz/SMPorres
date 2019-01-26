insert into usuarios(id, nombre, contraseña, fechaalta, estado, fechabaja, NombreCompleto)
values(	1, 
		'admin',	
		'BA-32-53-87-6A-ED-6B-C2-2D-4A-6F-F5-3D-84-06-C6-AD-86-41-95-ED-14-4A-B5-C8-76-21-' + 
		'B6-C2-33-B5-48-BA-EA-E6-95-6D-F3-46-EC-8C-17-F5-EA-10-F3-5E-E3-CB-C5-14-79-7E-D7-' + 
		'DD-D3-14-54-64-E2-A0-BA-B4-13', 
		'2018-12-27',	
		1, 
		NULL, 
		'Administrador')


insert into UsuariosItemsMenu(Id, IdUsuario, IdItemMenu)
select Id, 1, Id from ItemsMenu