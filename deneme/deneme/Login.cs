using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneme
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection con1 = new SqlConnection("Server=POLAT;Database=DDSSVS_Login;Uid=Polat;Password=!Qaz2ws3e412345;");
        public string OTP()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;


            characters += alphabets + small_alphabets + numbers;

            int length = 11;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("" + textBox3.Text + "");
                mail.To.Add("" + textBox3.Text + "");
                mail.Subject = "OTP";
                mail.Body = "OTP şifreniz : " + otp + "";

                SmtpServer.Port = 465;
                SmtpServer.Credentials = new System.Net.NetworkCredential("*****@gmail.com", "***********");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return otp;
        }

        public string encryption(String password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.ToString();
            String password = textBox2.Text;
            //Get the encrypt the password by using the class  
            string pass = encryption(password);
            label3.Text = pass;
            //Check whether the UseName and password are Empty  
            if (username.Length > 0 && password.Length > 0)
            {
                //creating the connection string              
                String passwords = encryption(password);
                con1.Open();
                // Check whether the Username Found in the Existing DB  
                String search = "SELECT * FROM tbl_Giris WHERE (KullaniciAdi = '" + username + "');";
                SqlCommand cmds = new SqlCommand(search, con1);
                SqlDataReader sqldrs = cmds.ExecuteReader();
                if (sqldrs.Read())
                {
                    String passed = (string)sqldrs["Parola"];
                    label3.Text = "Kullanıcı Adı Alınmış!";
                }
                else
                {
                    try
                    {
                        // if the Username not found create the new user accound  
                        string sql = "INSERT INTO tbl_Giris (KullaniciAdi, Parola) VALUES ('" + username + "','" + passwords + "');";
                        SqlCommand cmd = new SqlCommand(sql, con1);
                        cmd.ExecuteNonQuery();
                        String Message = "saved Successfully";
                        label3.Text = Message.ToString();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        Form1 fm = new Form1();
                        fm.Show();
                    }
                    catch (Exception ex)
                    {
                        label3.Text = ex.ToString();
                    }
                    con1.Close();
                }
            }

            else
            {
                String Message = "Kullanıcı Adı ya da Parola boş!";
                label3.Text = Message.ToString();
            }
        }
        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            String username = textBox1.Text.ToString();
            String password = textBox2.Text;
            con1.Open();
            //ncrypt the given password
            string passwords = encryption(password);
            String query = "SELECT KullaniciAdi, Parola FROM tbl_Giris WHERE (KullaniciAdi = '" + username + "') AND (Parola = '" + passwords + "');";

            SqlCommand cmd = new SqlCommand(query, con1);
            SqlDataReader sqldr = cmd.ExecuteReader();
            if (sqldr.Read())
            {
                string anahtar = OTP().ToString();
                if (anahtar == textBox4.Text)
                {
                    Form1 fm = new Form1();
                    fm.Show();
                }
                else
                    label3.Text = "OTP anahtar yanlış tekrar deneyiniz!";
            }
            else
            {
                label3.Text = "Kullanıcı Adı ya da Parola bulunamadı!";

            }

            con1.Close();
        }

        private void buttonkayit_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.ToString();
            String password = textBox2.Text;
            //Get the encrypt the password by using the class  
            string pass = encryption(password);
            label3.Text = pass;
            //Check whether the UseName and password are Empty  
            if (username.Length > 0 && password.Length > 0)
            {
                //creating the connection string              
                String passwords = encryption(password);
                con1.Open();
                // Check whether the Username Found in the Existing DB  
                String search = "SELECT * FROM tbl_Giris WHERE (KullaniciAdi = '" + username + "');";
                SqlCommand cmds = new SqlCommand(search, con1);
                SqlDataReader sqldrs = cmds.ExecuteReader();
                if (sqldrs.Read())
                {
                    String passed = (string)sqldrs["Parola"];
                    label3.Text = "Kullanıcı Adı Alınmış!";
                }
                else
                {
                    try
                    {
                        // if the Username not found create the new user accound  
                        string sql = "INSERT INTO tbl_Giris (KullaniciAdi, Parola) VALUES ('" + username + "','" + passwords + "');";
                        SqlCommand cmd = new SqlCommand(sql, con1);
                        cmd.ExecuteNonQuery();
                        String Message = "saved Successfully";
                        label3.Text = Message.ToString();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        Form1 fm = new Form1();
                        fm.Show();
                    }
                    catch (Exception ex)
                    {
                        label3.Text = ex.ToString();
                    }
                    con1.Close();
                }
            }

            else
            {
                String Message = "Kullanıcı Adı ya da Parola boş!";
                label3.Text = Message.ToString();
            }
        }
       

        private void button3_Click(object sender, EventArgs e)
        {

            string otp=OTP();
           
        }
    }
}
