using System;
using System.Windows.Forms;

namespace Rabat_Charitable_Association
{
    public partial class main_page : Form
    {
        Label timelabel = new Label();
        Label datelabel = new Label();
        private System.Windows.Forms.Timer timer;
        private DateTime currentDate;
        private int time = 600000; //default time 10 minutes
        private int elapsedTime = 0;
        private Form2 form2;
        private Form3 form3;
        private Form4 form4;
        private Form5 form5;
        public main_page()
        {
            InitializeComponent();
            timelabel.AutoSize = true;
            timelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            timelabel.Location = new System.Drawing.Point(360, 155);
            timelabel.Name = "timelabel";
            timelabel.Size = new System.Drawing.Size(48, 20);
            timelabel.TabIndex = 11;
            //
            datelabel.AutoSize = true;
            datelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            datelabel.Location = new System.Drawing.Point(190, 155);
            datelabel.Name = "datelabel";
            datelabel.Size = new System.Drawing.Size(48, 20);
            datelabel.TabIndex = 12;
            this.Controls.Add(timelabel);
            this.Controls.Add(datelabel);
            currentDate = DateTime.Now.Date;
            datelabel.Text = currentDate.ToString("yyyy-MM-dd"); 
            this.FormClosed += Main_page_FormClosed;
            StartClock();
        }
        private void StartClock()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; 
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timelabel.Text = DateTime.Now.ToString("HH:mm:ss");
            elapsedTime += timer.Interval;
            if (elapsedTime >= time)
            {
                timer.Stop();
                MessageBox.Show("انتهت الجلسة يرجى تسجيل الدخول مرة اخرى", "الوقت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 form1 = new Form1();
                form1.Show();
                form2?.Close();
                form3?.Close();
                form4?.Close();
                form5?.Close();
                this.Close();
            }
        }
        private void Main_page_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int minutes))
            {
                time = minutes * 60000;
                elapsedTime = 0;
                MessageBox.Show($"سيعمل البرنامج لمدة {minutes} دقائق", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("أدخل قيمة صالحة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button4_Click(object sender, EventArgs e) // المستفيدين
        {
            if (form2 == null || form2.IsDisposed)
                form2 = new Form2();
            form2.Show();
        }
        private void button3_Click(object sender, EventArgs e) //برنامج الروضة
        {
            if (form3 == null || form3.IsDisposed)
                form3 = new Form3();
            form3.Show();
        }
        private void button2_Click(object sender, EventArgs e) //الموظفين
        {
            if (form4 == null || form4.IsDisposed)
                form4 = new Form4();
            form4.Show();
        }
        private void button1_Click(object sender, EventArgs e) //المتبرعين
        {
            if (form5 == null || form5.IsDisposed)
                form5 = new Form5();
            form5.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("جمعية الرباط الخيرية");
        }
    }
}
