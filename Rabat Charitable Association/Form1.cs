using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace Rabat_Charitable_Association
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string user = "mohammad sammour";
            string password = "admin";
            if (textBox1.Text == user && textBox2.Text == password)
            {
                MessageBox.Show("مرحبا,اضغط موافق للمتابعة", "نجح الدخول", MessageBoxButtons.OK);
                main_page main_Page = new main_page();
                main_Page.Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("اسم المستخدم او كلمة المرور غير صحيح\nاعد المحاولة او اتصل بمسؤول النظام", "فشل الدخول");
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
        }
    }
}
