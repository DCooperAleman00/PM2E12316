using System;
using System.Collections.Generic;
using SQLite;
using PM2E12316.Models;
using System.Threading.Tasks;
using System.Text;


namespace PM2E12316.Controllers
{
    public class FotoController
    {

        readonly SQLiteAsyncConnection _connection;


        public FotoController()
        {
        }

        //Crear Base de datos
        public FotoController(string pathbasedatos)
        {
            _connection = new SQLiteAsyncConnection(pathbasedatos);

            _connection.CreateTableAsync<Fotografia>();

        }

        //Guardar Foto
        public Task<int> GuardarFoto(Fotografia emple)
        {
            if (emple.codigo != 0)
            {
                return _connection.UpdateAsync(emple);
            }
            else
            {
                return _connection.InsertAsync(emple);
            }
        }

        //Mostrar Lista de Fotos con detalles
        public Task<List<Fotografia>> ListaFotos()
        {

            return _connection.Table<Fotografia>().ToListAsync();
        }
        public Task<Fotografia> LeerFoto(int pcodigo)
        {
            return _connection.Table<Fotografia>().Where(i => i.codigo == pcodigo).FirstOrDefaultAsync();
        }

        //Eliminar foto seleccionada
        public Task<int> BorrarFoto(Fotografia emple)
        {
            return _connection.DeleteAsync(emple);
        }

    }
}
