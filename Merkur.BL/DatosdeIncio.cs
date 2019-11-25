using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Merkur.BL
{
    public class DatosdeIncio : CreateDatabaseIfNotExists<Contexto>
    {
        protected override void Seed(Contexto contexto)
        {
            var usuarioadmin = new Usuarios();
            usuarioadmin.Nombre = "Admin";
            usuarioadmin.Contrasena = "123";
            contexto.Usuarios.Add(usuarioadmin);

            var categoria1 = new Categoria();
            categoria1.Descripcion = "Fragil";
            contexto.Categorias.Add(categoria1);

            var categoria2 = new Categoria();
            categoria2.Descripcion = "Pesado";
            contexto.Categorias.Add(categoria2);

            var categoria3 = new Categoria();
            categoria3.Descripcion = "Documentacion";
            contexto.Categorias.Add(categoria3);

            var categoria4 = new Categoria();
            categoria4.Descripcion = "Peligroso";
            contexto.Categorias.Add(categoria4);

            var tipo1 = new Tipo();
            tipo1.Descripcion = "Urgente";
            contexto.Tipo.Add(tipo1);

            var tipo2 = new Tipo();
            tipo2.Descripcion = "No Urgente";
            contexto.Tipo.Add(tipo2);

            var clientes = new Cliente();
            clientes.Cedula = "501215245454"; 
            clientes.Nombres = "Orlando";
            clientes.Apellidos = "Garcia";             
            contexto.Cliente.Add(clientes);


            var clientes2 = new Cliente();
            clientes2.Cedula = "165648921554";
            clientes2.Nombres = "Ronaldo";
            clientes2.Apellidos = "Zelaya";
           
            contexto.Cliente.Add(clientes2);

            var producto1 = new Producto();
            producto1.Activo = true;
            producto1.Descripcion = "Prueba";
            producto1.Destino = "Desconocido";
            producto1.Tipo = tipo1;
            producto1.Categorias = categoria3;
            producto1.FechadeEntrega = DateTime.Now;
            producto1.Precio = 1;
            contexto.Productos.Add(producto1);

            base.Seed(contexto);
        }
    }
}
