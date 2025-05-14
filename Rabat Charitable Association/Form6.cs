using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace Rabat_Charitable_Association
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://mail.google.com/mail/?view=cm&fs=1&to=mohammadnsammour%40gmail.com&authuser=8",
                UseShellExecute = true
            });
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.facebook.com/mohammadnafiz.summour",
                UseShellExecute = true
            });
        }
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.instagram.com/mohammadnsammour40/",
                UseShellExecute = true
            });
        }
        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/MohammadNSammour",
                UseShellExecute = true
            });
        }
    }
}
