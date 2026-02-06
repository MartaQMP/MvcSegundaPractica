using Microsoft.Data.SqlClient;
using MvcSegundaPractica.Models;
using System.Data;

namespace MvcSegundaPractica.Repositories
{
    public class RepositoryComic
    {
        SqlConnection cn;
        SqlCommand com;
        DataTable tablaComics;

        public RepositoryComic()
        {
            string connectionString = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=COMICS;Persist Security Info=True;User ID=SA;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            string sql = "SELECT * FROM COMICS";
            SqlDataAdapter ad = new SqlDataAdapter(sql, this.cn);
            this.tablaComics = new DataTable();
            ad.Fill(this.tablaComics);
        }

        public List<Comic> GetComics()
        {
            var consulta = from datos in this.tablaComics.AsEnumerable() select datos;
            List<Comic> comics = new List<Comic>();
            foreach (var fila in consulta)
            {
                Comic comic = new Comic();
                comic.IdComic = fila.Field<int>("IDCOMIC");
                comic.Nombre = fila.Field<string>("NOMBRE");
                comic.Imagen = fila.Field<string>("IMAGEN");
                comic.Descripcion = fila.Field<string>("DESCRIPCION");
                comics.Add(comic);
            }

            return comics;
        }

        public Comic GetComicById(int idComic)
        {
            var consulta = from datos in this.tablaComics.AsEnumerable() where datos.Field<int>("IDCOMIC") == idComic select datos;
            var fila = consulta.First();
            Comic comic = new Comic();
            comic.IdComic = fila.Field<int>("IDCOMIC");
            comic.Nombre = fila.Field<string>("NOMBRE");
            comic.Imagen = fila.Field<string>("IMAGEN");
            comic.Descripcion = fila.Field<string>("DESCRIPCION");
            return comic;
        }

        public async Task InsertComicAsync(string nombre, string imagen, string descripcion)
        {
            int idComic = 0;
            var consulta = from datos in this.tablaComics.AsEnumerable()
                           select datos;
            idComic = consulta.Max(z => z.Field<int>("IDCOMIC"));
            int max = idComic + 1;
            if (idComic != 0)
            {
                string sql = "INSERT INTO COMICS VALUES (@idComic, @nombre, @imagen, @descripcion)";
                this.com.Parameters.AddWithValue("@idComic", max);
                this.com.Parameters.AddWithValue("@nombre", nombre);
                this.com.Parameters.AddWithValue("@imagen", imagen);
                this.com.Parameters.AddWithValue("@descripcion", descripcion);
                this.com.CommandType = CommandType.Text;
                this.com.CommandText = sql;
                await this.cn.OpenAsync();
                await this.com.ExecuteNonQueryAsync();
                await this.cn.CloseAsync();
                this.com.Parameters.Clear();
            }
        }

    }
}
