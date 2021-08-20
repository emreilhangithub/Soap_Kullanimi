using System;
using System.Data;//datatable kullanmak için yazdık
using System.Web.Services;
using System.Data.SqlClient;

namespace Soap_Kullanimi
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string AdimiYaz(string Name)
        {
            return "Benim Adım " + Name;
        }

        [WebMethod]
        public int Topla(int sayi1, int sayi2)
        {
            return sayi1 + sayi2;
        }

        [WebMethod]
        public DataTable UrunlerGetir()
        {
            SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-E9UTSVL;Initial Catalog=NORTHWND;Integrated Security=True");
            SqlDataAdapter adp = new SqlDataAdapter("select * from Products", cnn);
            DataTable dt = new DataTable("Kategoriler");
            adp.Fill(dt);
            return dt;
        }

        [WebMethod]
        public Bilgiler BilgileriGetir(int ID)
        {
            Bilgiler bilgilerimiz = new Bilgiler();
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-E9UTSVL;Initial Catalog=NORTHWND;Integrated Security=True");
            SqlCommand komut = new SqlCommand("select * from Products where ProductID=@ProductID", baglan);
            komut.Parameters.AddWithValue("ProductID", ID);
            baglan.Open();
            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                bilgilerimiz.ID = Convert.ToInt32(dr["ProductID"]);
                bilgilerimiz.Adi = dr["ProductName"].ToString();
                bilgilerimiz.Soyadi = dr["QuantityPerUnit"].ToString();
            }
            return bilgilerimiz;
        }
    }
}
