using Merkur.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merkur.BL
{
    public class FacturaBL
    {
        Contexto _contexto;

        public BindingList<Facturas1> ListadeFacturas { get; set; }

        public FacturaBL()
        {
            _contexto = new Contexto();
        }

        public BindingList<Facturas1> ObtenerFacturas()
        {
            _contexto.Facturas.Include("FacturaDetalle").Load();
            ListadeFacturas = _contexto.Facturas.Local.ToBindingList();

            return ListadeFacturas;
        }

        public void AgregarFactura()
        {
            var nuevaFactura = new Facturas1();
            _contexto.Facturas.Add(nuevaFactura);
        }

        public void AgregarFacturaDetalle(Facturas1 factura)
        {
            if (factura != null)
            {
                var nuevaDetalle = new FacturasDetalle();
                factura.FacturaDetalle.Add(nuevaDetalle);
            }
        }
        public void RemoverFacturaDetalle(Facturas1 factura, FacturasDetalle  facturaDetalle)
        {
            if (factura != null && facturaDetalle != null)
            {
                factura.FacturaDetalle.Remove(facturaDetalle);
            }
        }

        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;
                item.Reload();

            }
        }

        public Resultado3 GuardarFactura(Facturas1 factura)
        {
            var resultado3 = Validar(factura);
            if (resultado3.Exitoso == false)
            {
                return resultado3;
            }
            _contexto.SaveChanges();
            resultado3.Exitoso = true;
            return resultado3;
        }

        private Resultado3 Validar(Facturas1 factura)
        {
            var resultado3 = new Resultado3();
            resultado3.Exitoso = true;

            return resultado3;
        }  

        public void CalcularFactura(Facturas1 factura)
        {
            if (factura != null)
            {
                double Subtotal = 0;

                foreach (var detalle in factura.FacturaDetalle)
                {
                    var producto = _contexto.Productos.Find(detalle.ProductoId);

                    if (producto != null)
                    {
                        detalle.Precio = producto.Precio;
                        detalle.Total = detalle.Cantidad * producto.Precio;

                        Subtotal += detalle.Total;
                    }
                }
                factura.SubTotal = Subtotal;
                factura.IVS = Subtotal * 0.15;
                factura.Total = Subtotal + factura.IVS; 
            }

        }
        

       

    }

    public class Facturas1
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime EntradaPaquete { get; set; }
        public string PesoPaquete { get; set; }
        public string Destino { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public BindingList<FacturasDetalle> FacturaDetalle { get; set; }
        public double SubTotal { get; set; }
        public double IVS { get; set; }
        public double Total { get; set; }
        public bool Activo { get; set; }

        public Facturas1()
        {
            Fecha = DateTime.Now;
            FacturaDetalle = new BindingList<FacturasDetalle>();
            Activo = true;
        }
      }

        public class FacturasDetalle
        {
          public int Id { get; set; }
          public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public int Paquetes { get; set; }
        public double Precio { get; set; }
        public double Total { get; set; }

        public FacturasDetalle()
        {
            Paquetes = 1;
        }


    }



    
}
