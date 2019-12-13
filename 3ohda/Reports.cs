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
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        ArrayList keta_id = new ArrayList();
        string selected_keta_id_Str = "0";

        ArrayList mnfaz_id = new ArrayList();
        string selected_manfaz_id_Str = "0";

        ArrayList fea_id = new ArrayList();
        string selected_fea_id_Str = "0";

        ArrayList sanf_id = new ArrayList();
        string selected_sanf_id_Str = "0";

        ArrayList sanf_name_id = new ArrayList();
        string selected_sanf_name_id_Str = "0";

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

                comboBox3.Items.Add("الكل");
                keta_id.Add("0");
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

                comboBox5.Items.Add("الكل");
                fea_id.Add("0");

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

            this.DataTable1TableAdapter.Fill(this.DataSet1.DataTable1, Int32.Parse(selected_keta_id_Str), Int32.Parse(selected_manfaz_id_Str), Int32.Parse(selected_fea_id_Str), Int32.Parse(selected_sanf_id_Str), Int32.Parse(selected_sanf_name_id_Str));
            this.reportViewer1.RefreshReport();


/*
            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= 3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("SELECT [3hdt-shon-edaria].dbo.fea.fea_name as الفئة, [3hdt-shon-edaria].dbo.fea_type.fea_type_name as الصنف,  [3hdt-shon-edaria].dbo.fea_description.fea_description_name as 'نوع الصنف',  [3hdt-shon-edaria].dbo.store.count as العدد,  [3hdt-shon-edaria].dbo.store.insert_date as 'تاريخ الادخال',  [3hdt-shon-edaria].dbo.Keta.keta_name as القطاع, [3hdt-shon-edaria].dbo.manfaz.manfaz_name as المنفذ from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id and ( [3hdt-shon-edaria].dbo.store.keta_id =@keta_id OR @keta_id = 0) and ( [3hdt-shon-edaria].dbo.store.manfaz_id =@manfaz_id OR @manfaz_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_id =@fea_id OR @fea_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_type_id =@fea_type_id OR @fea_type_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_descripton_id =@fea_descripton_id OR @fea_descripton_id = 0)", con);


                command.Parameters.AddWithValue("@keta_id", selected_keta_id_Str);
                command.Parameters.AddWithValue("@manfaz_id", selected_manfaz_id_Str);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);
                command.Parameters.AddWithValue("@fea_descripton_id", selected_sanf_name_id_Str);

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();

            }
            catch (Exception ec)
            {


                MessageBox.Show("حدد موقف العربة");
            }*/



            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= 3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].manfaz where keta_id =@keta_id ", con);
                command.Parameters.AddWithValue("@keta_id", selected_keta_id_obj);

               /* SqlCommand command = new SqlCommand("SELECT [3hdt-shon-edaria].dbo.fea.fea_name as الفئة, [3hdt-shon-edaria].dbo.fea_type.fea_type_name as الصنف,  [3hdt-shon-edaria].dbo.fea_description.fea_description_name as 'نوع الصنف',  [3hdt-shon-edaria].dbo.store.count as العدد,  [3hdt-shon-edaria].dbo.store.insert_date as 'تاريخ الادخال',  [3hdt-shon-edaria].dbo.Keta.keta_name as القطاع, [3hdt-shon-edaria].dbo.manfaz.manfaz_name as المنفذ from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id and ( [3hdt-shon-edaria].dbo.store.keta_id =@keta_id OR @keta_id = 0) and ( [3hdt-shon-edaria].dbo.store.manfaz_id =@manfaz_id OR @manfaz_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_id =@fea_id OR @fea_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_type_id =@fea_type_id OR @fea_type_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_descripton_id =@fea_descripton_id OR @fea_descripton_id = 0)", con);


                command.Parameters.AddWithValue("@keta_id", selected_keta_id_Str);
                command.Parameters.AddWithValue("@manfaz_id", selected_manfaz_id_Str);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);
                command.Parameters.AddWithValue("@fea_descripton_id", selected_sanf_name_id_Str);*/

                SqlDataReader DR = command.ExecuteReader();

                comboBox2.Items.Clear();
                mnfaz_id.Clear();

                comboBox2.Items.Add("الكل");
                mnfaz_id.Add("0");

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


            try
            {
               /* SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= cars; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("select count(type) as 'asd' from carDB", con);*/

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= 3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("SELECT  sum([3hdt-shon-edaria].dbo.store.count) as num from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id and ( [3hdt-shon-edaria].dbo.store.keta_id =@keta_id OR @keta_id = 0) and ( [3hdt-shon-edaria].dbo.store.manfaz_id =@manfaz_id OR @manfaz_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_id =@fea_id OR @fea_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_type_id =@fea_type_id OR @fea_type_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_descripton_id =@fea_descripton_id OR @fea_descripton_id = 0)", con);


                command.Parameters.AddWithValue("@keta_id", selected_keta_id_Str);
                command.Parameters.AddWithValue("@manfaz_id", selected_manfaz_id_Str);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);
                command.Parameters.AddWithValue("@fea_descripton_id", selected_sanf_name_id_Str);

                SqlDataReader DR = command.ExecuteReader();

                while (DR.Read())
                {
                    label7.Text = (DR.GetValue(0).ToString());
                }
                DR.Close();

            }
            catch
            {
                MessageBox.Show("");
            }


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected_manfaz_index = comboBox2.SelectedIndex;
            object selected_manfaz_id_obj = mnfaz_id[selected_manfaz_index];
            selected_manfaz_id_Str = selected_manfaz_id_obj.ToString();
            Console.WriteLine("selected_manfaz_id_Str" + selected_manfaz_id_Str);


            this.DataTable1TableAdapter.Fill(this.DataSet1.DataTable1, Int32.Parse(selected_keta_id_Str), Int32.Parse(selected_manfaz_id_Str), Int32.Parse(selected_fea_id_Str), Int32.Parse(selected_sanf_id_Str), Int32.Parse(selected_sanf_name_id_Str));
            this.reportViewer1.RefreshReport();

           /* try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= 3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();

               /* SqlCommand command = new SqlCommand("SELECT [3hdt-shon-edaria].dbo.fea.fea_name as الفئة, [3hdt-shon-edaria].dbo.fea_type.fea_type_name as الصنف,  [3hdt-shon-edaria].dbo.fea_description.fea_description_name as 'نوع الصنف',  [3hdt-shon-edaria].dbo.store.count as العدد,  [3hdt-shon-edaria].dbo.store.insert_date as 'تاريخ الادخال',  [3hdt-shon-edaria].dbo.Keta.keta_name as القطاع, [3hdt-shon-edaria].dbo.manfaz.manfaz_name as المنفذ from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id  and [3hdt-shon-edaria].dbo.store.manfaz_id =@m ", con);


                command.Parameters.AddWithValue("@m", selected_manfaz_id_Str);*/

               /* SqlCommand command = new SqlCommand("SELECT [3hdt-shon-edaria].dbo.fea.fea_name as الفئة, [3hdt-shon-edaria].dbo.fea_type.fea_type_name as الصنف,  [3hdt-shon-edaria].dbo.fea_description.fea_description_name as 'نوع الصنف',  [3hdt-shon-edaria].dbo.store.count as العدد,  [3hdt-shon-edaria].dbo.store.insert_date as 'تاريخ الادخال',  [3hdt-shon-edaria].dbo.Keta.keta_name as القطاع, [3hdt-shon-edaria].dbo.manfaz.manfaz_name as المنفذ from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id and ( [3hdt-shon-edaria].dbo.store.keta_id =@keta_id OR @keta_id = 0) and ( [3hdt-shon-edaria].dbo.store.manfaz_id =@manfaz_id OR @manfaz_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_id =@fea_id OR @fea_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_type_id =@fea_type_id OR @fea_type_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_descripton_id =@fea_descripton_id OR @fea_descripton_id = 0)", con);


                command.Parameters.AddWithValue("@keta_id", selected_keta_id_Str);
                command.Parameters.AddWithValue("@manfaz_id", selected_manfaz_id_Str);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);
                command.Parameters.AddWithValue("@fea_descripton_id", selected_sanf_name_id_Str);

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();

            }
            catch (Exception ec)
            {


                MessageBox.Show("حدد موقف العربة");
            }*/

            try
            {
                /* SqlConnection con = new SqlConnection();
                 con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= cars; Integrated Security=SSPI";
                 con.Open();

                 SqlCommand command = new SqlCommand("select count(type) as 'asd' from carDB", con);*/

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= 3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("SELECT  sum([3hdt-shon-edaria].dbo.store.count) as num from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id and ( [3hdt-shon-edaria].dbo.store.keta_id =@keta_id OR @keta_id = 0) and ( [3hdt-shon-edaria].dbo.store.manfaz_id =@manfaz_id OR @manfaz_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_id =@fea_id OR @fea_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_type_id =@fea_type_id OR @fea_type_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_descripton_id =@fea_descripton_id OR @fea_descripton_id = 0)", con);


                command.Parameters.AddWithValue("@keta_id", selected_keta_id_Str);
                command.Parameters.AddWithValue("@manfaz_id", selected_manfaz_id_Str);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);
                command.Parameters.AddWithValue("@fea_descripton_id", selected_sanf_name_id_Str);

                SqlDataReader DR = command.ExecuteReader();

                while (DR.Read())
                {
                    label7.Text = (DR.GetValue(0).ToString());
                }
                DR.Close();

            }
            catch
            {
                MessageBox.Show("");
            }

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


            this.DataTable1TableAdapter.Fill(this.DataSet1.DataTable1, Int32.Parse(selected_keta_id_Str), Int32.Parse(selected_manfaz_id_Str), Int32.Parse(selected_fea_id_Str), Int32.Parse(selected_sanf_id_Str), Int32.Parse(selected_sanf_name_id_Str));
            this.reportViewer1.RefreshReport();

         /*   try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= 3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();
/*
                SqlCommand command = new SqlCommand("SELECT [3hdt-shon-edaria].dbo.fea.fea_name as الفئة, [3hdt-shon-edaria].dbo.fea_type.fea_type_name as الصنف,  [3hdt-shon-edaria].dbo.fea_description.fea_description_name as 'نوع الصنف',  [3hdt-shon-edaria].dbo.store.count as العدد,  [3hdt-shon-edaria].dbo.store.insert_date as 'تاريخ الادخال',  [3hdt-shon-edaria].dbo.Keta.keta_name as القطاع, [3hdt-shon-edaria].dbo.manfaz.manfaz_name as المنفذ from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id  and [3hdt-shon-edaria].dbo.store.fea_id =@m ", con);


                command.Parameters.AddWithValue("@m", selected_fea_id_obj);*/

              /*  SqlCommand command = new SqlCommand("SELECT [3hdt-shon-edaria].dbo.fea.fea_name as الفئة, [3hdt-shon-edaria].dbo.fea_type.fea_type_name as الصنف,  [3hdt-shon-edaria].dbo.fea_description.fea_description_name as 'نوع الصنف',  [3hdt-shon-edaria].dbo.store.count as العدد,  [3hdt-shon-edaria].dbo.store.insert_date as 'تاريخ الادخال',  [3hdt-shon-edaria].dbo.Keta.keta_name as القطاع, [3hdt-shon-edaria].dbo.manfaz.manfaz_name as المنفذ from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id and ( [3hdt-shon-edaria].dbo.store.keta_id =@keta_id OR @keta_id = 0) and ( [3hdt-shon-edaria].dbo.store.manfaz_id =@manfaz_id OR @manfaz_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_id =@fea_id OR @fea_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_type_id =@fea_type_id OR @fea_type_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_descripton_id =@fea_descripton_id OR @fea_descripton_id = 0)", con);


                command.Parameters.AddWithValue("@keta_id", selected_keta_id_Str);
                command.Parameters.AddWithValue("@manfaz_id", selected_manfaz_id_Str);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);
                command.Parameters.AddWithValue("@fea_descripton_id", selected_sanf_name_id_Str);

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();

            }
            catch (Exception ec)
            {


                MessageBox.Show("حدد موقف العربة");
            }*/

            
            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog=3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].fea_type where fea_id =@fea_id ", con);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);

               /* SqlCommand command = new SqlCommand("SELECT [3hdt-shon-edaria].dbo.fea.fea_name as الفئة, [3hdt-shon-edaria].dbo.fea_type.fea_type_name as الصنف,  [3hdt-shon-edaria].dbo.fea_description.fea_description_name as 'نوع الصنف',  [3hdt-shon-edaria].dbo.store.count as العدد,  [3hdt-shon-edaria].dbo.store.insert_date as 'تاريخ الادخال',  [3hdt-shon-edaria].dbo.Keta.keta_name as القطاع, [3hdt-shon-edaria].dbo.manfaz.manfaz_name as المنفذ from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id and ( [3hdt-shon-edaria].dbo.store.keta_id =@keta_id OR @keta_id = 0) and ( [3hdt-shon-edaria].dbo.store.manfaz_id =@manfaz_id OR @manfaz_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_id =@fea_id OR @fea_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_type_id =@fea_type_id OR @fea_type_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_descripton_id =@fea_descripton_id OR @fea_descripton_id = 0)", con);


                command.Parameters.AddWithValue("@keta_id", selected_keta_id_Str);
                command.Parameters.AddWithValue("@manfaz_id", selected_manfaz_id_Str);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);
                command.Parameters.AddWithValue("@fea_descripton_id", selected_sanf_name_id_Str);*/

                SqlDataReader DR = command.ExecuteReader();

                comboBox1.Items.Clear();
                sanf_id.Clear();

                comboBox1.Items.Add("الكل");
                sanf_id.Add("0");

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

            try
            {
                /* SqlConnection con = new SqlConnection();
                 con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= cars; Integrated Security=SSPI";
                 con.Open();

                 SqlCommand command = new SqlCommand("select count(type) as 'asd' from carDB", con);*/

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= 3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("SELECT  sum([3hdt-shon-edaria].dbo.store.count) as num from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id and ( [3hdt-shon-edaria].dbo.store.keta_id =@keta_id OR @keta_id = 0) and ( [3hdt-shon-edaria].dbo.store.manfaz_id =@manfaz_id OR @manfaz_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_id =@fea_id OR @fea_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_type_id =@fea_type_id OR @fea_type_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_descripton_id =@fea_descripton_id OR @fea_descripton_id = 0)", con);


                command.Parameters.AddWithValue("@keta_id", selected_keta_id_Str);
                command.Parameters.AddWithValue("@manfaz_id", selected_manfaz_id_Str);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);
                command.Parameters.AddWithValue("@fea_descripton_id", selected_sanf_name_id_Str);

                SqlDataReader DR = command.ExecuteReader();

                while (DR.Read())
                {
                    label7.Text = (DR.GetValue(0).ToString());
                }
                DR.Close();

            }
            catch
            {
                MessageBox.Show("");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected_sanf_index = comboBox1.SelectedIndex;
            object selected_sanf_id_obj = sanf_id[selected_sanf_index];
            selected_sanf_id_Str = selected_sanf_id_obj.ToString();
            Console.WriteLine("selected_sanf_id_obj" + selected_sanf_id_obj);


            this.DataTable1TableAdapter.Fill(this.DataSet1.DataTable1, Int32.Parse(selected_keta_id_Str), Int32.Parse(selected_manfaz_id_Str), Int32.Parse(selected_fea_id_Str), Int32.Parse(selected_sanf_id_Str), Int32.Parse(selected_sanf_name_id_Str));
            this.reportViewer1.RefreshReport();

         /*   try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= 3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();

               /* SqlCommand command = new SqlCommand("SELECT [3hdt-shon-edaria].dbo.fea.fea_name as الفئة, [3hdt-shon-edaria].dbo.fea_type.fea_type_name as الصنف,  [3hdt-shon-edaria].dbo.fea_description.fea_description_name as 'نوع الصنف',  [3hdt-shon-edaria].dbo.store.count as العدد,  [3hdt-shon-edaria].dbo.store.insert_date as 'تاريخ الادخال',  [3hdt-shon-edaria].dbo.Keta.keta_name as القطاع, [3hdt-shon-edaria].dbo.manfaz.manfaz_name as المنفذ from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id  and [3hdt-shon-edaria].dbo.store.fea_type_id =@m ", con);


                command.Parameters.AddWithValue("@m", selected_sanf_id_obj);*/

            /*    SqlCommand command = new SqlCommand("SELECT [3hdt-shon-edaria].dbo.fea.fea_name as الفئة, [3hdt-shon-edaria].dbo.fea_type.fea_type_name as الصنف,  [3hdt-shon-edaria].dbo.fea_description.fea_description_name as 'نوع الصنف',  [3hdt-shon-edaria].dbo.store.count as العدد,  [3hdt-shon-edaria].dbo.store.insert_date as 'تاريخ الادخال',  [3hdt-shon-edaria].dbo.Keta.keta_name as القطاع, [3hdt-shon-edaria].dbo.manfaz.manfaz_name as المنفذ from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id and ( [3hdt-shon-edaria].dbo.store.keta_id =@keta_id OR @keta_id = 0) and ( [3hdt-shon-edaria].dbo.store.manfaz_id =@manfaz_id OR @manfaz_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_id =@fea_id OR @fea_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_type_id =@fea_type_id OR @fea_type_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_descripton_id =@fea_descripton_id OR @fea_descripton_id = 0)", con);


                command.Parameters.AddWithValue("@keta_id", selected_keta_id_Str);
                command.Parameters.AddWithValue("@manfaz_id", selected_manfaz_id_Str);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);
                command.Parameters.AddWithValue("@fea_descripton_id", selected_sanf_name_id_Str);

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();

            }
            catch (Exception ec)
            {


                MessageBox.Show("حدد موقف العربة");
            }*/

            
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog=3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[fea_description] where fea_id =@fea_id and fea_type_id =@fea_type_id ", con);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);

              /*  SqlCommand command = new SqlCommand("SELECT [3hdt-shon-edaria].dbo.fea.fea_name as الفئة, [3hdt-shon-edaria].dbo.fea_type.fea_type_name as الصنف,  [3hdt-shon-edaria].dbo.fea_description.fea_description_name as 'نوع الصنف',  [3hdt-shon-edaria].dbo.store.count as العدد,  [3hdt-shon-edaria].dbo.store.insert_date as 'تاريخ الادخال',  [3hdt-shon-edaria].dbo.Keta.keta_name as القطاع, [3hdt-shon-edaria].dbo.manfaz.manfaz_name as المنفذ from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id and ( [3hdt-shon-edaria].dbo.store.keta_id =@keta_id OR @keta_id = 0) and ( [3hdt-shon-edaria].dbo.store.manfaz_id =@manfaz_id OR @manfaz_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_id =@fea_id OR @fea_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_type_id =@fea_type_id OR @fea_type_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_descripton_id =@fea_descripton_id OR @fea_descripton_id = 0)", con);


                command.Parameters.AddWithValue("@keta_id", selected_keta_id_Str);
                command.Parameters.AddWithValue("@manfaz_id", selected_manfaz_id_Str);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);
                command.Parameters.AddWithValue("@fea_descripton_id", selected_sanf_name_id_Str);*/

                SqlDataReader DR = command.ExecuteReader();

                comboBox4.Items.Clear();
                sanf_name_id.Clear();

                comboBox4.Items.Add("الكل");
                sanf_name_id.Add("0");
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

            try
            {
                /* SqlConnection con = new SqlConnection();
                 con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= cars; Integrated Security=SSPI";
                 con.Open();

                 SqlCommand command = new SqlCommand("select count(type) as 'asd' from carDB", con);*/

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= 3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("SELECT  sum([3hdt-shon-edaria].dbo.store.count) as num from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id and ( [3hdt-shon-edaria].dbo.store.keta_id =@keta_id OR @keta_id = 0) and ( [3hdt-shon-edaria].dbo.store.manfaz_id =@manfaz_id OR @manfaz_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_id =@fea_id OR @fea_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_type_id =@fea_type_id OR @fea_type_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_descripton_id =@fea_descripton_id OR @fea_descripton_id = 0)", con);


                command.Parameters.AddWithValue("@keta_id", selected_keta_id_Str);
                command.Parameters.AddWithValue("@manfaz_id", selected_manfaz_id_Str);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);
                command.Parameters.AddWithValue("@fea_descripton_id", selected_sanf_name_id_Str);

                SqlDataReader DR = command.ExecuteReader();

                while (DR.Read())
                {
                    label7.Text = (DR.GetValue(0).ToString());
                }
                DR.Close();

            }
            catch
            {
                MessageBox.Show("");
            }

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected_sanf_name_index = comboBox4.SelectedIndex;
            object selected_sanf_name_id_obj = sanf_name_id[selected_sanf_name_index];
            selected_sanf_name_id_Str = selected_sanf_name_id_obj.ToString();
            Console.WriteLine("selected_sanf_name_id_obj" + selected_sanf_name_id_obj);


            this.DataTable1TableAdapter.Fill(this.DataSet1.DataTable1, Int32.Parse(selected_keta_id_Str), Int32.Parse(selected_manfaz_id_Str), Int32.Parse(selected_fea_id_Str), Int32.Parse(selected_sanf_id_Str), Int32.Parse(selected_sanf_name_id_Str));
            this.reportViewer1.RefreshReport();

          /*  try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= 3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();

                /*SqlCommand command = new SqlCommand("SELECT [3hdt-shon-edaria].dbo.fea.fea_name as الفئة, [3hdt-shon-edaria].dbo.fea_type.fea_type_name as الصنف,  [3hdt-shon-edaria].dbo.fea_description.fea_description_name as 'نوع الصنف',  [3hdt-shon-edaria].dbo.store.count as العدد,  [3hdt-shon-edaria].dbo.store.insert_date as 'تاريخ الادخال',  [3hdt-shon-edaria].dbo.Keta.keta_name as القطاع, [3hdt-shon-edaria].dbo.manfaz.manfaz_name as المنفذ from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id  and [3hdt-shon-edaria].dbo.store.fea_descripton_id =@m ", con);


                command.Parameters.AddWithValue("@m", selected_sanf_name_id_obj);*/
            /*
                SqlCommand command = new SqlCommand("SELECT [3hdt-shon-edaria].dbo.fea.fea_name as الفئة, [3hdt-shon-edaria].dbo.fea_type.fea_type_name as الصنف,  [3hdt-shon-edaria].dbo.fea_description.fea_description_name as 'نوع الصنف',  [3hdt-shon-edaria].dbo.store.count as العدد,  [3hdt-shon-edaria].dbo.store.insert_date as 'تاريخ الادخال',  [3hdt-shon-edaria].dbo.Keta.keta_name as القطاع, [3hdt-shon-edaria].dbo.manfaz.manfaz_name as المنفذ from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id and ( [3hdt-shon-edaria].dbo.store.keta_id =@keta_id OR @keta_id = 0) and ( [3hdt-shon-edaria].dbo.store.manfaz_id =@manfaz_id OR @manfaz_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_id =@fea_id OR @fea_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_type_id =@fea_type_id OR @fea_type_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_descripton_id =@fea_descripton_id OR @fea_descripton_id = 0)", con);


                command.Parameters.AddWithValue("@keta_id", selected_keta_id_Str);
                command.Parameters.AddWithValue("@manfaz_id", selected_manfaz_id_Str);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);
                command.Parameters.AddWithValue("@fea_descripton_id", selected_sanf_name_id_Str);

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();

            }
            catch (Exception ec)
            {


                MessageBox.Show("حدد موقف العربة");
            }*/


            try
            {
                /* SqlConnection con = new SqlConnection();
                 con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= cars; Integrated Security=SSPI";
                 con.Open();

                 SqlCommand command = new SqlCommand("select count(type) as 'asd' from carDB", con);*/

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= 3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("SELECT  sum([3hdt-shon-edaria].dbo.store.count) as num from [3hdt-shon-edaria].dbo.store  ,[3hdt-shon-edaria].dbo.fea  ,[3hdt-shon-edaria].dbo.fea_description  ,[3hdt-shon-edaria].dbo.fea_type  ,[3hdt-shon-edaria].dbo.manfaz ,[3hdt-shon-edaria].dbo.Keta   where  [3hdt-shon-edaria].dbo.store.fea_descripton_id = [3hdt-shon-edaria].dbo.fea_description.fea_descripton_id  and [3hdt-shon-edaria].dbo.store.keta_id = [3hdt-shon-edaria].dbo.Keta.keta_id  and [3hdt-shon-edaria].dbo.store.manfaz_id = [3hdt-shon-edaria].dbo.manfaz.manfaz_id  and [3hdt-shon-edaria].dbo.store.fea_id = [3hdt-shon-edaria].dbo.fea.fea_id  and [3hdt-shon-edaria].dbo.store.fea_type_id = [3hdt-shon-edaria].dbo.fea_type.fea_type_id and ( [3hdt-shon-edaria].dbo.store.keta_id =@keta_id OR @keta_id = 0) and ( [3hdt-shon-edaria].dbo.store.manfaz_id =@manfaz_id OR @manfaz_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_id =@fea_id OR @fea_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_type_id =@fea_type_id OR @fea_type_id = 0) and ( [3hdt-shon-edaria].dbo.store.fea_descripton_id =@fea_descripton_id OR @fea_descripton_id = 0)", con);


                command.Parameters.AddWithValue("@keta_id", selected_keta_id_Str);
                command.Parameters.AddWithValue("@manfaz_id", selected_manfaz_id_Str);
                command.Parameters.AddWithValue("@fea_id", selected_fea_id_Str);
                command.Parameters.AddWithValue("@fea_type_id", selected_sanf_id_Str);
                command.Parameters.AddWithValue("@fea_descripton_id", selected_sanf_name_id_Str);

                SqlDataReader DR = command.ExecuteReader();

                while (DR.Read())
                {
                    label7.Text = (DR.GetValue(0).ToString());
                }
                DR.Close();

            }
            catch
            {
                MessageBox.Show("");
            }

        }

        private void AddButton_Click(object sender, EventArgs e)
        {

/*
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog=3hdt-shon-edaria; Integrated Security=SSPI";
                con.Open();



                string query = "insert into store([fea_descripton_id],[count],[insert_date],[keta_id],[manfaz_id],[fea_id],[fea_type_id]) values('" + selected_sanf_name_id_Str + "','" + textBox5.Text.ToString() + "','" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "','" + selected_keta_id_Str + "','" + selected_manfaz_id_Str + "','" + selected_fea_id_Str + "','" + selected_sanf_name_id_Str + "')";
                //    string query = "insert into ketaa(name) values('" + comboBox4.SelectedItem.ToString() + "')";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("تم حفظ البيانات بنجاح ");
                this.Refresh();


            }
            catch
            {
                MessageBox.Show("! من فضلك أدخل البيانات");
            }*/


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            mainframe rep2 = new mainframe();
            rep2.Show();
        }
    }
}
