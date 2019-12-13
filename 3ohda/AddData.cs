using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

namespace _3ohda
{
    public partial class AddData : Form
    {
        public AddData()
        {
            InitializeComponent();
        }

        ArrayList keta_id = new ArrayList();
        string selected_keta_id_Str;

        ArrayList mnfaz_id = new ArrayList();
        string selected_manfaz_id_Str;

        ArrayList fea_id = new ArrayList();
        string selected_fea_id_Str;

        ArrayList sanf_id = new ArrayList();
        string selected_sanf_id_Str;

        ArrayList sanf_name_id = new ArrayList();
        string selected_sanf_name_id_Str;

        private void Form4_Load_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog=3hdt-shon-edaria; Integrated Security=SSPI";
            con.Open();
            try
            {

                SqlCommand command = new SqlCommand("SELECT * FROM Keta", con);
                SqlDataReader DR = command.ExecuteReader();

                comboBox3.Items.Clear();
                keta_id.Clear();

                while (DR.Read())
                {
                    comboBox3.Items.Add(DR.GetValue(1).ToString());
                    keta_id.Add(DR.GetValue(0).ToString());
                }
                DR.Close();

            }
            catch
            {
                MessageBox.Show("عفوا لايوجد قطاعات لدى الطريق");
            }


           
            try
            {

                SqlCommand command = new SqlCommand("SELECT * FROM fea", con);
                SqlDataReader DR = command.ExecuteReader();

                comboBox5.Items.Clear();
                fea_id.Clear();

                while (DR.Read())
                {
                    comboBox5.Items.Add(DR.GetValue(1).ToString());
                    fea_id.Add(DR.GetValue(0).ToString());
                }
                DR.Close();

            }
            catch
            {
                MessageBox.Show("عفوا لايوجد فئات");
            }



        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            int selected_keta_index = comboBox3.SelectedIndex;
            object selected_keta_id_obj = keta_id[selected_keta_index];
            selected_keta_id_Str = selected_keta_id_obj.ToString();
            Console.WriteLine("selected_keta_id_obj" + selected_keta_id_obj);


            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog=3hdt-shon-edaria; Integrated Security=SSPI";
            con.Open();
            try
            {


                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].manfaz where keta_id =@keta_id ", con);
                command.Parameters.AddWithValue("@keta_id", selected_keta_id_obj);
                SqlDataReader DR = command.ExecuteReader();

                comboBox2.Items.Clear();
                mnfaz_id.Clear();

                while (DR.Read())
                {
                    comboBox2.Items.Add(DR.GetValue(1).ToString());
                    mnfaz_id.Add(DR.GetValue(0).ToString());
                }
                DR.Close();

                

            }
            catch
            {
                MessageBox.Show("عفوا لايوجد منافذ لدى القطاع");
            }


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected_manfaz_index = comboBox2.SelectedIndex;
            object selected_manfaz_id_obj = mnfaz_id[selected_manfaz_index];
            selected_manfaz_id_Str = selected_manfaz_id_obj.ToString();
            Console.WriteLine("selected_manfaz_id_Str" + selected_manfaz_id_Str);

        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }


        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

            int selected_fea_index = comboBox5.SelectedIndex;
            object selected_fea_id_obj = fea_id[selected_fea_index];
            selected_fea_id_Str = selected_fea_id_obj.ToString();
            Console.WriteLine("selected_keta_id_obj" + selected_fea_id_obj);

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog=3hdt-shon-edaria; Integrated Security=SSPI";
            con.Open();
            try
            {

                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].fea_type where fea_id =@fea_id ", con);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                SqlDataReader DR = command.ExecuteReader();

                comboBox1.Items.Clear();
                sanf_id.Clear();

                while (DR.Read())
                {
                    comboBox1.Items.Add(DR.GetValue(1).ToString());
                    sanf_id.Add(DR.GetValue(0).ToString());
                }
                DR.Close();

            }
            catch
            {
                MessageBox.Show("عفوا لايوجد صنف لهذه المهمات");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected_sanf_index = comboBox1.SelectedIndex;
            object selected_sanf_id_obj = sanf_id[selected_sanf_index];
            selected_sanf_id_Str = selected_sanf_id_obj.ToString();
            Console.WriteLine("selected_sanf_id_obj" + selected_sanf_id_obj);

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog=3hdt-shon-edaria; Integrated Security=SSPI";
            con.Open();
            try
            {

                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[fea_description] where fea_id =@fea_id and fea_type_id =@fea_type_id ", con);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);
                SqlDataReader DR = command.ExecuteReader();

                comboBox4.Items.Clear();
                sanf_name_id.Clear();

                while (DR.Read())
                {
                    comboBox4.Items.Add(DR.GetValue(1).ToString());
                    sanf_name_id.Add(DR.GetValue(0).ToString());
                }
                DR.Close();

            }
            catch
            {
                MessageBox.Show("عفوا لايوجد صنف لهذه المهمات");
            }

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected_sanf_name_index = comboBox4.SelectedIndex;
            object selected_sanf_name_id_obj = sanf_name_id[selected_sanf_name_index];
            selected_sanf_name_id_Str = selected_sanf_name_id_obj.ToString();
            Console.WriteLine("selected_sanf_name_id_obj" + selected_sanf_name_id_obj);
            Console.WriteLine("selected_sanf_name_id_Str" + selected_sanf_name_id_Str);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {


            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog=3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();


                Console.WriteLine("selected_sanf_name_id_Str" + selected_sanf_name_id_Str);
                Console.WriteLine("selected_keta_id_Str" + selected_keta_id_Str);
                Console.WriteLine("selected_manfaz_id_Str" + selected_manfaz_id_Str);
                Console.WriteLine("selected_fea_id_Str" + selected_fea_id_Str);
                Console.WriteLine("selected_sanf_id_Str" + selected_sanf_id_Str);

                string query = "insert into store([fea_descripton_id],[count],[insert_date],[keta_id],[manfaz_id],[fea_id],[fea_type_id]) values('" + selected_sanf_name_id_Str + "','" + textBox5.Text.ToString() + "','" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "','" + selected_keta_id_Str + "','" + selected_manfaz_id_Str + "','" + selected_fea_id_Str + "','" + selected_sanf_id_Str + "')";
                //    string query = "insert into ketaa(name) values('" + comboBox4.SelectedItem.ToString() + "')";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("تم حفظ البيانات بنجاح ");

                

            }
            catch
            {
                MessageBox.Show("! من فضلك أدخل البيانات");
            }
            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.Close();
            AddData rep2 = new AddData();
            rep2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            mainframe rep2 = new mainframe();
            rep2.Show();
        }
    }
}
