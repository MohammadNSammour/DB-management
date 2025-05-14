using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;//
namespace Rabat_Charitable_Association
{
    public partial class Form4 : Form
    {
        string connection_path = @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\mohammad\Desktop\rebat charitable.accdb";
        public Form4()
        {
            InitializeComponent();
            this.Size = new Size(740, 705);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||string.IsNullOrWhiteSpace(textBox2.Text) ||string.IsNullOrWhiteSpace(textBox3.Text) ||string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("من فضلك عبىء جميع الفراغات", "فشلت الاضافة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(textBox1.Text, out int id))
            {
                MessageBox.Show("من فضلك ادخل بيانات صحيحة للمعرف", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(textBox5.Text, out int salary))
            {
                MessageBox.Show("من فضلك ادخل معلومات صحيحة للراتب", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "INSERT INTO employees (ID, fullname, [location], phonenumber, salary, workinghours, notes) VALUES (@ID, @fullname, @location, @phonenumber, @salary, @workinghours, @notes)";
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connection_path))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        // Add parameters to avoid SQL injection
                        command.Parameters.AddWithValue("@ID",id);
                        command.Parameters.AddWithValue("@fullname", textBox2.Text);
                        command.Parameters.AddWithValue("@location",textBox3.Text);
                        command.Parameters.AddWithValue("@phonenumber", textBox4.Text);
                        command.Parameters.AddWithValue("@salary",salary);
                        command.Parameters.AddWithValue("@workinghours", textBox6.Text);
                        command.Parameters.AddWithValue("@notes", textBox7.Text);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("تمت الاضافة بنجاح!", "!نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            button3_Click(sender, e);
                            button5_Click(sender, e);
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                            textBox5.Clear();
                            textBox6.Clear();
                            textBox7.Clear();
                        }
                        else
                        {
                            MessageBox.Show("لم يتم الاضافة", "!خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR : {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("من فضلك ادخل المعرف الذي تريد حذفه", "فشل الحذف", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(textBox1.Text, out int id))
            {
                MessageBox.Show("من فضلك ادخل بيانات صحيحة للمعرف", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "DELETE FROM employees WHERE ID = @ID";
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connection_path))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("!تم الحذف بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            button3_Click(sender, e);
                            button5_Click(sender, e);
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                            textBox5.Clear();
                            textBox6.Clear();
                            textBox7.Clear();
                        }
                        else
                        {
                            MessageBox.Show("لم يتم الحذف,احد الاسباب عدم وجود المعرف", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR : {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
        string.IsNullOrWhiteSpace(textBox2.Text) ||
        string.IsNullOrWhiteSpace(textBox3.Text) ||
        string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("من فضلك قم بتعبئة جميع الفراغات", "فشل التعديل", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(textBox1.Text, out int id))
            {
                MessageBox.Show("من فضلك ادخل بيانات صحيحة للمعرف", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(textBox5.Text, out int salary))
            {
                MessageBox.Show("من فضلك ادخل معلومات صحيحة للراتب", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "UPDATE employees SET fullname = @fullname, [location] = @location, phonenumber = @phonenumber, salary = @salary, workinghours = @workinghours, notes = @notes WHERE ID = @ID";
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connection_path))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        // Add parameters to avoid SQL injection
                        command.Parameters.AddWithValue("@fullname", textBox2.Text);
                        command.Parameters.AddWithValue("@location", textBox3.Text);
                        command.Parameters.AddWithValue("@phonenumber", textBox4.Text);
                        command.Parameters.AddWithValue("@salary", salary);
                        command.Parameters.AddWithValue("@workinghours", textBox6.Text);
                        command.Parameters.AddWithValue("@notes", textBox7.Text);
                        command.Parameters.AddWithValue("@ID", id);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("تم التعديل بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            button3_Click(sender, e);
                            button5_Click(sender, e);
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                            textBox5.Clear();
                            textBox6.Clear();
                            textBox7.Clear();
                        }
                        else
                        {
                            MessageBox.Show("فشل التعديل بنجاح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR : {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Visible = false;
            chart2.Visible = false;
            dataGridView1.Visible = true;
            string query1 = "SELECT * FROM employees ORDER BY ID";
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connection_path))
                {
                    connection.Open();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query1, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                        this.Size = new Size(740, 705);
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR : {ex.Message}");
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            chart1.Visible = true;
            chart2.Visible = true;
            string query = "SELECT fullname, SUM(salary) AS salaryammount" +
                        " FROM employees" +
                        " WHERE fullname IS NOT NULL AND salary IS NOT NULL" +
                        " GROUP BY fullname" +
                        " ORDER BY fullname";
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connection_path))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            chart1.Series.Clear();
                            chart2.Series.Clear();
                            Series series = new Series("رواتب المعلمين")
                            {
                                ChartType = SeriesChartType.Column
                            };
                            Series series1 = new Series("رواتب المعلمين")
                            {
                                ChartType = SeriesChartType.Pie
                            };
                            while (reader.Read())
                            {
                                string name = reader["fullname"].ToString();
                                if (double.TryParse(reader["salaryammount"].ToString(), out double salary))
                                {
                                    series.Points.AddXY(name, salary);
                                    series1.Points.AddXY(name, salary);
                                }
                            }
                            chart1.Series.Add(series);
                            chart2.Series.Add(series1);
                            chart1.Titles.Clear();
                            chart2.Titles.Clear();
                            chart1.Titles.Add("الرواتب");
                            chart2.Titles.Add("الرواتب");
                            chart1.ChartAreas[0].AxisX.Title = "المعلم/ـة";
                            chart1.ChartAreas[0].AxisY.Title = "الراتب";
                            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "C";
                            chart2.ChartAreas[0].AxisX.Title = "المعلم/ـة";
                            chart2.ChartAreas[0].AxisY.Title = "الراتب";
                            chart2.ChartAreas[0].AxisY.LabelStyle.Format = "C";
                            this.Size = new Size(1458, 705);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR : {ex.Message}");
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = selectedRow.Cells["ID"].Value?.ToString();
                textBox2.Text = selectedRow.Cells["fullname"].Value?.ToString();
                textBox3.Text = selectedRow.Cells["location"].Value?.ToString();
                textBox4.Text = selectedRow.Cells["phonenumber"].Value?.ToString();
                textBox5.Text = selectedRow.Cells["salary"].Value?.ToString();
                textBox6.Text = selectedRow.Cells["workinghours"].Value?.ToString();
                textBox7.Text = selectedRow.Cells["notes"].Value?.ToString();
            }
        }
        //////////////////////////////////////////////////
        private void button3_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(button3, "اظهار البيانات من قاعدة البيانات الى الجدول");
        }
        private void button5_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(button5, "اظهار الرسوم البيانية");
        }
        private void button4_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(button4, "اضافة سجل جديد الى القاعدة");
        }
        private void button1_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(button1, "حذف سجل موجود من القاعدة");
        }
        private void button2_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(button2, "تعديل سجل موجود من القاعدة");
        }
        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(textBox1, "لأدخال المعرف");
        }
        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(textBox2, "لأدخال الاسم");
        }
        private void textBox3_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(textBox3, "لأدخال ساعات العمل");
        }
        private void textBox4_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(textBox4, "لأدخال رقم الهاتف");
        }
        private void textBox5_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(textBox5, "لأدخال الراتب");
        }
        private void textBox6_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(textBox6, "لأدخال اي ملاحظات");
        }
        private void textBox7_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(textBox7, "لأدخال الموقع");
        }
        private void chart1_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(chart1, "رسم بياني يوضح الرواتب الموجهة لكل موظف");
        }
        private void chart2_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(chart2, "رسم بياني يوضح الرواتب الموجهة لكل موظف على شكل دائري");
        }
        private void dataGridView1_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(dataGridView1, "جدول بيانات الموظفين");
        }
    }
}
