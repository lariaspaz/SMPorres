﻿using SMPorres.Lib;
using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SMPorres.Repositories
{
    public class UsuariosRepository
    {
        public bool VerificarLoginUsuario(string nombre, string contraseña)
        {
            Usuario usr = ObtenerUsuario(nombre);
            if (usr == null)
            {
                return false;
            }
            return usr.Contraseña == Lib.Security.Cryptography.CalcularSHA512(contraseña);
        }

        internal Usuario ObtenerUsuario(string nombre)
        {
            using (var db = new SMPorresEntities())
            {
                return (from u in db.Usuarios where u.Nombre.ToLower() == nombre.ToLower() select u).FirstOrDefault();
            }
        }

        public static IEnumerable<Usuario> ObtenerUsuarios()
        {
            using (var db = new SMPorresEntities())
            {
                return (from u in db.Usuarios orderby u.Nombre select u).ToList();
            }
        }

        internal static Usuario Insertar(string nombre, string nombreCompleto, byte estado)
        {
            using (var db = new SMPorresEntities())
            {
                if (db.Usuarios.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim()))
                {
                    throw new Exception("Ya existe un usuario con este nombre.");
                }
                var id = db.Usuarios.Max(c => c.Id) + 1;
                var usr = new Usuario
                {
                    Id = id,
                    Nombre = nombre,
                    NombreCompleto = nombreCompleto,
                    FechaAlta = Configuration.CurrentDate,
                    Estado = estado,
                    FechaBaja = estado == 1 ? (DateTime?)null : Configuration.CurrentDate,
                    Contraseña = Lib.Security.Cryptography.CalcularSHA512("123456")
                };
                db.Usuarios.Add(usr);
                db.SaveChanges();
                return usr;
            }
        }

        internal static Usuario ObtenerUsuarioPorId(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Usuarios.Find(id);
            }
        }

        internal static void Actualizar(int id, string nombre, string nombreCompleto, byte estado)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Usuarios.Any(t => t.Id == id))
                {
                    throw new Exception("No existe el usuario con Id " + id);
                }
                var u = db.Usuarios.Find(id);
                u.Nombre = nombre.Trim();
                u.NombreCompleto = nombreCompleto.Trim();
                u.Estado = estado;
                db.SaveChanges();
            }
        }

        internal static void ReiniciarContraseña(int id, string contraseña)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Usuarios.Any(t => t.Id == id))
                {
                    throw new Exception("No existe el usuario con Id " + id);
                }
                var u = db.Usuarios.Find(id);
                u.Contraseña = Lib.Security.Cryptography.CalcularSHA512(contraseña);
                db.SaveChanges();
            }
        }
    }
}
