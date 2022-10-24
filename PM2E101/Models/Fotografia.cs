using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace PM2E12316.Models
{
    public class Fotografia
    {
        [PrimaryKey, AutoIncrement]
        public int codigo { get; set; }

        [MaxLength(250)]
        public string latitud { get; set; }

        [MaxLength(250)]
        public string longitud { get; set; }
        
        [MaxLength(250)]
        public string descripcion { get; set; }

        [MaxLength(250)]
        public string imgRuta { get; set; }
    }
}
