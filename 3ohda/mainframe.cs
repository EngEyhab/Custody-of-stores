using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3ohda
{
    public partial class mainframe : Form
    {
        public mainframe()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Queries rep = new Queries();
            rep.Show();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddData rep2 = new AddData();
            rep2.Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           /* Form3 rep3 = new Form3();
            rep3.Show();*/
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mainframe_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            this.Hide();
            Reports rep2 = new Reports();
            rep2.Show();


        }
    }
}
