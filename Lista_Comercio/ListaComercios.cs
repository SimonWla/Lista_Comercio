using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lista_Comercio
{
    public partial class ListaComercios : Form
    {
        public ListaComercios()
        {
            InitializeComponent();
        }

        private void Agregar_Click(object sender, EventArgs e)
        {
            string nombre = tbNombre.Text;
            string ciudad = tbCiudad.Text;
            string direccion = tbDireccion.Text;
            string tipo = tbTipo.Text;
            if(nombre == "" || ciudad == "" || direccion == "" || tipo == "")
            {
                MessageBox.Show("Debes completar los campos");
            }
            else
            {
                Comercio nuevoComercio = new Comercio(0, nombre, ciudad, direccion, tipo);
                int fila = nuevoComercio.AgregarComercio();
                LimpiarDatos();
                if (fila == 1)
                {
                    MessageBox.Show("Se ha agregado el comercio con exito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    nuevoComercio.CargarComercio(dgvComercio);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ListaComercios_Load(object sender, EventArgs e)
        {
            Comercio consulta = new Comercio();
            consulta.CargarComercio(dgvComercio);
        }

        private void dgvComercio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            if (indice == -1 || dgvComercio.SelectedCells[1].Value.ToString() == "")
            {
                MessageBox.Show("Selecione registro valido");
            }
            else
            {
                tbId.Text = dgvComercio.SelectedCells[0].Value.ToString();
                tbNombre.Text = dgvComercio.SelectedCells[1].Value.ToString();
                tbCiudad.Text = dgvComercio.SelectedCells[2].Value.ToString();
                tbDireccion.Text = dgvComercio.SelectedCells[3].Value.ToString();
                tbTipo.Text = dgvComercio.SelectedCells[4].Value.ToString();
                Agregar.Enabled = false;
                Modificar.Enabled = true;
                Eliminar.Enabled = true;
            }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            LimpiarDatos();
            Agregar.Enabled = true;
            Modificar.Enabled = false;
            Eliminar.Enabled = false;
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tbId.Text);
            DialogResult confirmar = MessageBox.Show("Quieres eliminar el comercio de tu base de datos?", "Mensaje", MessageBoxButtons.OKCancel);
            if (confirmar == DialogResult.OK)
            {
                Comercio comercio = new Comercio(id);
                int fila = comercio.EliminarComercio();
                if(fila == 1)
                {
                    comercio.CargarComercio(dgvComercio);
                    LimpiarDatos();
                    MessageBox.Show("Se ha eliminado con exito");
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al querer eliminar");
                }
            }
        }
        public void LimpiarDatos()
        {
            tbId.Text = "";
            tbNombre.Text = "";
            tbCiudad.Text = "";
            tbDireccion.Text = "";
            tbTipo.Text = "";
        }

        private void Modificar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tbId.Text);
            string nombre = tbNombre.Text;
            string ciudad = tbCiudad.Text;
            string direccion = tbDireccion.Text;
            string tipo = tbTipo.Text;
            DialogResult confirmar = MessageBox.Show("Quieres modificar los datos del comercio?", "Mensaje", MessageBoxButtons.OKCancel);
            if(confirmar == DialogResult.OK)
            {
                Comercio consulta = new Comercio(id, nombre, ciudad, direccion, tipo);
                int fila = consulta.ModificarComercio();
                if (fila == 1)
                {
                    MessageBox.Show("Ha sido modificado con exito");
                    consulta.CargarComercio(dgvComercio);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al quere modificar los datos");
                }
            }
        }
    }
}
