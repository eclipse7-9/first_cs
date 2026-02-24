using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class CrearProductoModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CrearProductoModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public Producto Producto { get; set; } = new();

        public string? Mensaje { get; set; }

        public void OnPost()
        {
            string conexionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conexion = new SqlConnection(conexionString))
            {
                SqlCommand cmd = new SqlCommand("InsertarProducto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", Producto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", Producto.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", Producto.Precio);
                cmd.Parameters.AddWithValue("@Stock", Producto.Stock);

                SqlParameter mensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 255);
                mensaje.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(mensaje);

                SqlParameter idNuevo = new SqlParameter("@Id_nuevo", SqlDbType.Int);
                idNuevo.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(idNuevo);

                conexion.Open();
                cmd.ExecuteNonQuery();

                Mensaje = mensaje.Value?.ToString();
            }
        }
    }
}