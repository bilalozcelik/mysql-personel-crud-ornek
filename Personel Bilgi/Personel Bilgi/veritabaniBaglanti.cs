using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Personel_Bilgi
{
    internal class veritabaniBaglanti
    {
        public MySqlConnection baglan()
        {
            MySqlConnection baglanti = new MySqlConnection(ConfigurationManager.ConnectionStrings["personelBaglantiCumlesi"].ConnectionString);
            baglanti.Open();
            MySqlConnection.ClearPool(baglanti);
            return (baglanti);
        }
    }
}
