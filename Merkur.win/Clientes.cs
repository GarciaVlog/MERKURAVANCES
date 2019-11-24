using Merkur.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Merkur.win
{
    public partial class Clientes : Form
    {
        ClientesBL _clientesBL;
        public Clientes()
        {

            _clientesBL = new ClientesBL();
            
            InitializeComponent();
            listadeClientesBindingSource.DataSource = _clientesBL.ObtenerClientes();            
        }

      
        private void listadeClientesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            var nuevoCliente = new Cliente();
            nuevoCliente.Nombres = nombresTextBox.Text;
            nuevoCliente.Apellidos = apellidosTextBox.Text;
            nuevoCliente.Cedula = cedulaTextBox.Text;
            //clear
            nombresTextBox.Clear();
            apellidosTextBox.Clear();
            cedulaTextBox.Clear();
            //focus
            nombresTextBox.Focus();
            apellidosTextBox.Focus();
            cedulaTextBox.Focus();

            listadeClientesBindingSource.EndEdit();
            var clientes = (Cliente)listadeClientesBindingSource.Current;

            if (fotoPictureBox.Image != null)
            {
                clientes.Foto = Program.imageToByArray(fotoPictureBox.Image);
            }
            else
            {
                clientes.Foto = null;
            }
            var resultado2 = _clientesBL.GuardarClientes(clientes);

            if (resultado2.Exitoso == true)
            {
                listadeClientesBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Cliente Guardado!");
            }
            else
            {
                MessageBox.Show(resultado2.Mensaje);
            }
        }

        private void membresiaCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _clientesBL.AgregarClientes();
            listadeClientesBindingSource.MoveLast();

            DeshabilitarHabilitarBotones(false);
        }

        private void DeshabilitarHabilitarBotones(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled = valor;
            bindingNavigatorMoveLastItem.Enabled = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled = valor;

            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;

            toolStripButtoncancelar.Visible = !valor;

        }

        private void toolStripButtoncancelar_Click(object sender, EventArgs e)
        {
            DeshabilitarHabilitarBotones(true);
            Eliminar(0);
        }
        private void Eliminar(int id)
        {

            var resultado2 = _clientesBL.EliminarClientes(id);
            if (resultado2 == true)
            {
                listadeClientesBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar al Cliente");
            }


        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var clientes = (Cliente)listadeClientesBindingSource.Current;
            if (clientes != null)
            {
                openFileDialog1.ShowDialog();
                var archivo = openFileDialog1.FileName;

                if (archivo != "")
                {
                    var fileInfo = new FileInfo(archivo);
                    var fileStream = fileInfo.OpenRead();

                    fotoPictureBox.Image = Image.FromStream(fileStream);
                }
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            listadeClientesBindingSource.DataSource = _clientesBL.ObtenerClientes();
            
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            fotoPictureBox.Image = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form webcam = new WEBCAM();
            webcam.Show();
        }

        private void listadeClientesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            listadeClientesBindingSource.DataSource = Visible;
        }

        private void Clientes_Load(object sender, EventArgs e)
        {

        }
    }



    
}
