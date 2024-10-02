using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lista_Comercio
{
    class Comercio
    {
        private int id;
        private string nombre;
        private string ciudad;
        private string direccion;
        private string tipo;
        SqlConnection command = new SqlConnection("Data Source = localhost; Initial Catalog = BD_Comercios; Integrated Security = True;");
        public Comercio(int id, string nombre, string ciudad, string direccion, string tipo)
        {
            this.id = id;
            this.nombre = nombre;
            this.ciudad = ciudad;
            this.direccion = direccion;
            this.tipo = tipo;
        }
        public Comercio()
        {

        }
        public Comercio(int id)
        {
            this.id=id;
        }
        public int AgregarComercio()
        {
            command.Open();
            SqlCommand consulta = new SqlCommand("INSERT INTO tb_Comercios VALUES (@nombre, @ciudad, @direccion, @tipo)", command);
            consulta.Parameters.AddWithValue("nombre", nombre);
            consulta.Parameters.AddWithValue("ciudad", ciudad);
            consulta.Parameters.AddWithValue("direccion", direccion);
            consulta.Parameters.AddWithValue("tipo", tipo);
            int filasAfectadas = consulta.ExecuteNonQuery();
            command.Close();
            return filasAfectadas;
        }
        public int EliminarComercio()
        {
            command.Open();
            SqlCommand consulta = new SqlCommand("DELETE FROM tb_Comercios WHERE id=@codigo", command);
            consulta.Parameters.AddWithValue("codigo", id);
            int filaAfectada = consulta.ExecuteNonQuery();
            command.Close();
            return filaAfectada;
        }
        public int ModificarComercio()
        {
            command.Open();
            SqlCommand consulta = new SqlCommand("UPDATE tb_Comercios SET nombre = @nombreComercio, ciudad = @ciudadComercio," +
                " direccion = @direccionComercio, tipo = @tipoComercio WHERE id=@codigoComercio", command);
            consulta.Parameters.AddWithValue("codigoComercio", id);
            consulta.Parameters.AddWithValue("nombreComercio", nombre);
            consulta.Parameters.AddWithValue("ciudadComercio", ciudad);
            consulta.Parameters.AddWithValue("direccionComercio", direccion);
            consulta.Parameters.AddWithValue("tipoComercio", tipo);
            int filaAfectada = consulta.ExecuteNonQuery();
            command.Close();
            return filaAfectada;
        }
        public void CargarComercio(DataGridView dgv)
        {
            string consulta = "SELECT * FROM tb_Comercios";
            command.Open();
            SqlDataAdapter data = new SqlDataAdapter(consulta, command);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dgv.DataSource = dt;
        }
    }
}
