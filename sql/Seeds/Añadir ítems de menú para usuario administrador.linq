<Query Kind="Statements">
  <Connection>
    <ID>74aaa33c-daa6-4d8e-ac59-94b5d024c385</ID>
    <Persist>true</Persist>
    <Server>LEONARDO\SQLEXPRESS</Server>
    <Database>SMPorres_Prod</Database>
    <IsProduction>true</IsProduction>
  </Connection>
</Query>

var items = UsuariosItemsMenus.Where(usritems => usritems.IdUsuario == 1);
var i = UsuariosItemsMenus.Max(usritems => usritems.Id);
foreach	(var item in items)
{
	UsuariosItemsMenus.InsertOnSubmit(new UsuariosItemsMenu { Id = ++i, IdItemMenu = item.IdItemMenu, IdUsuario = 10000 });
	SubmitChanges();
}

var usr = Usuarios.First(u => u.Id == 10000);
usr.Nombre = "admin";
SubmitChanges();