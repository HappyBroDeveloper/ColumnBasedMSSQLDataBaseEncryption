using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography;
using System.Net.Mail;

namespace deneme
{
    public partial class Form1 : Form
    {


      

        public Form1()
        {

            InitializeComponent();
        }

        SqlConnection con1 = new SqlConnection("Server=POLAT;Database=AdventureWorks2017;Uid=Polat;Password=!Qaz2ws3e412345;");
        SqlCommand command;
        SqlDataAdapter adapter;
        DataTable table;
        DataSet ds;

        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        byte[] plaintext;
        byte[] encryptedtext;

        private void VeriGoster_Load(object sender, EventArgs e)
        {


        }

        private void getcolumns()
        {
            DataSet ds = new DataSet();

            try
            {
                con1.Open();

                SqlCommand sqlCmd = new SqlCommand();

                sqlCmd.Connection = con1;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "select COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME= '"+ comboBox1.SelectedValue +"'";

                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);

                DataTable dtRecord = new DataTable();
                sqlDataAdap.Fill(dtRecord);
                comboBox2.DataSource = dtRecord;
                comboBox2.DisplayMember = "COLUMN_NAME";
                comboBox2.ValueMember = "COLUMN_NAME";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con1.Close();
        }
        private void gettables()
        {
            DataSet ds = new DataSet();

            try
            {
                con1.Open();

                SqlCommand sqlCmd = new SqlCommand();

                sqlCmd.Connection = con1;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'";

                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);

                DataTable dtRecord = new DataTable();
                sqlDataAdap.Fill(dtRecord);
                comboBox1.DataSource = dtRecord;
                comboBox1.DisplayMember = "TABLE_NAME";
                comboBox1.ValueMember = "TABLE_NAME";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con1.Close();





        }
        private void getschema()
        {
            DataSet ds = new DataSet();

            try
            {
                con1.Open();

                SqlCommand sqlCmd = new SqlCommand();

                sqlCmd.Connection = con1;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'";

                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);

                DataTable dtRecord = new DataTable();
                sqlDataAdap.Fill(dtRecord);
                comboBox3.DataSource = dtRecord;
                comboBox3.DisplayMember = "TABLE_SCHEMA";
                comboBox3.ValueMember = "TABLE_SCHEMA";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con1.Close();





        }
       
        public void fillCombo(ComboBox combo, string query, string displayMember, string valueMember)
        {
            SqlCommand command = new SqlCommand(query, con1);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            combo.DataSource = table;
            combo.DisplayMember = displayMember;
            combo.ValueMember = valueMember;

        }


        //private void BindGrid()
        //{

        //    SqlCommand cmd = new SqlCommand("Select * from Sales.CreditCard", con1);
        //    con1.Open();
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    DataSet ds = new DataSet();
        //    da.SelectCommand = cmd;
        //    da.Fill(ds);
        //    dataGridView1.DataSource = ds.Tables[0];

        //}


        private void button1_Click(object sender, EventArgs e)
        {
            gettables();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            


        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            getcolumns();


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                con1.Open();
                string Select = "select " + comboBox1.SelectedValue + "." + comboBox2.SelectedValue + " FROM " + comboBox3.SelectedValue + "." + comboBox1.SelectedValue + "";
                SqlDataAdapter da = new SqlDataAdapter(Select, con1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            catch(Exception ex)
            {
                MessageBox.Show("\n Lütfen şema adını ya da tablo adını doğru seçiniz!"+ "\n \n (" + ex.Message + ")");
            }
            con1.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            getschema();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            try
            {
                con1.Open();

                if (textBox1.Text == "")
                {
                    MessageBox.Show("Lütfen Master Key giriniz!");
                }

                else
                {

                    SqlCommand cmd = new SqlCommand("CREATE MASTER KEY ENCRYPTION BY PASSWORD = '" + textBox1.Text + "';", con1);
                    cmd.ExecuteNonQuery();
                }
                con1.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            try
            {
                con1.Open();

                SqlCommand cmd = new SqlCommand("DROP MASTER KEY  ", con1);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con1.Close();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                con1.Open();

                if (textBox2.Text == "")
                {

                    MessageBox.Show("Lütfen Sertifika Adı giriniz!");
                }
              
                else
                {
                    
                    SqlCommand cmd = new SqlCommand("create CERTIFICATE " + textBox2.Text + " WITH SUBJECT = 'Sertifika'", con1);
                    cmd.ExecuteNonQuery();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con1.Close();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                con1.Open();

                SqlCommand cmd = new SqlCommand("DROP CERTIFICATE "+textBox2.Text+"", con1);
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con1.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            

            try
            {
                con1.Open();
                SqlCommand cmd = new SqlCommand("CREATE SYMMETRIC KEY "+textBox5.Text+" WITH ALGORITHM = AES_256 ENCRYPTION BY CERTIFICATE "+textBox3.Text+";",con1);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con1.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
            con1.Open();

                SqlCommand cmd = new SqlCommand("ALTER TABLE "+comboBox3.SelectedValue+"."+comboBox1.SelectedValue+" ADD "+comboBox2.SelectedValue+ "_Encrypted nvarchar(max);  OPEN SYMMETRIC KEY "+textBox5.Text+" DECRYPTION BY CERTIFICATE " + textBox3.Text + ";  ", con1);
                SqlCommand cmd1 = new SqlCommand("UPDATE "+comboBox3.SelectedValue+"."+comboBox1.SelectedValue+" SET "+comboBox2.SelectedValue+"_Encrypted = EncryptByKey(Key_GUID('"+textBox5.Text+"'), "+comboBox2.SelectedValue+", 1, HashBytes('SHA1', CONVERT(varbinary, "+comboBox1.SelectedValue+"ID)));",con1);
                SqlCommand cmd2 = new SqlCommand("OPEN SYMMETRIC KEY " + textBox5.Text + "  DECRYPTION BY CERTIFICATE " + textBox3.Text + ";  ",con1);
                
                
                
                
                
                
                cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Şifreleme işlemi başarılı!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con1.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                con1.Open();
                string Select = "select name from sys.certificates";
                SqlDataAdapter da = new SqlDataAdapter(Select, con1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con1.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                con1.Open();
                string Select = "select name from sys.symmetric_keys";
                SqlDataAdapter da = new SqlDataAdapter(Select, con1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView3.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con1.Close();
        }

        private void buttondesifrele_Click(object sender, EventArgs e)
        {
            try
            {
                con1.Open();
                SqlCommand cmdsertifika = new SqlCommand("OPEN SYMMETRIC KEY "+textBox5.Text+" DECRYPTION BY CERTIFICATE "+textBox3.Text+"",con1);
                SqlCommand cmddesifrele = new SqlCommand("UPDATE "+comboBox3.SelectedValue+"."+comboBox1.SelectedValue+" set "+comboBox2.SelectedValue+ " = CONVERT (nvarchar, DecryptByKey("+comboBox2.SelectedValue+", 1 , HashBytes('SHA1', CONVERT(varbinary, "+comboBox1.SelectedValue+"ID))))", con1);
                SqlCommand cmdkolonadidegistir = new SqlCommand("EXEC sp_rename '"+comboBox3.SelectedValue+"."+comboBox1.SelectedValue+"."+comboBox2.SelectedValue+"','"+comboBox2.SelectedValue+"_and_Decrypted', 'COLUMN';", con1);

                cmdsertifika.ExecuteNonQuery();
                cmddesifrele.ExecuteNonQuery();
                cmdkolonadidegistir.ExecuteNonQuery();
                MessageBox.Show("Deşifreleme işlemi başarılı!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con1.Close();
        }

       


        private void button15_Click(object sender, EventArgs e)
        {
            Login fm = new Login();
            fm.Show();
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey); encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        static public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            plaintext = ByteConverter.GetBytes(textBox1.Text);
            encryptedtext = Encryption(plaintext, RSA.ExportParameters(false), false);
            textBox6.Text = ByteConverter.GetString(encryptedtext);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            byte[] decryptedtex = Decryption(encryptedtext, RSA.ExportParameters(true), false);
            MessageBox.Show(ByteConverter.GetString(decryptedtex));
        }

        private void button16_Click(object sender, EventArgs e)
        {
            

        }
    }

    
}
