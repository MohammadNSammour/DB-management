using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;//
namespace Rabat_Charitable_Association
{
    public partial class Form2 : Form
    {
        string connection_path = @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\mohammad\Desktop\rebat charitable.accdb";
        public Form2()
        {
            InitializeComponent();
            this.Size = new Size(740, 705);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Visible = false;
            chart2.Visible = false;
            dataGridView1.Visible = true;
            string query1 = "SELECT * FROM beneficiaries ORDER BY ID";
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
            string query = "SELECT takedate, SUM(cashtaken) AS TotalCash" +
                        " FROM beneficiaries" +
                        " WHERE cashtaken IS NOT NULL AND takedate IS NOT NULL" +
                        " GROUP BY takedate" +
                        " ORDER BY takedate";
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
                            Series series = new Series("المال المأخوذ")
                            {
                                ChartType = SeriesChartType.Column
                            };
                            Series series1 = new Series("المال المأخوذ")
                            {
                                ChartType = SeriesChartType.Pie
                            };
                            while (reader.Read())
                            {
                                if (DateTime.TryParse(reader["takedate"].ToString(), out DateTime takedate) &&
                                    double.TryParse(reader["TotalCash"].ToString(), out double totalCash))
                                {
                                    series.Points.AddXY(takedate.ToString("yyyy-MM-dd"), totalCash);
                                    series1.Points.AddXY(takedate.ToString("yyyy-MM-dd"), totalCash);
                                }
                            }
                            chart1.Series.Add(series);
                            chart1.Titles.Clear();
                            chart1.Titles.Add("الاوقات والاموال المأخوذة");
                            chart1.ChartAreas[0].AxisX.Title = "التاريخ";
                            chart1.ChartAreas[0].AxisY.Title = "المال الكلي المأخوذ";
                            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "C";
                            chart2.Series.Add(series1);
                            chart2.Titles.Clear();
                            chart2.Titles.Add("الاوقات والاموال المأخوذة");
                            chart2.ChartAreas[0].AxisX.Title = "التاريخ";
                            chart2.ChartAreas[0].AxisY.Title = "المال الكلي المأخوذ";
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
        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||string.IsNullOrWhiteSpace(textBox2.Text) ||string.IsNullOrWhiteSpace(textBox3.Text) ||string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("من فضلك عبىء جميع الفراغات", "فشلت الاضافة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(!int.TryParse(textBox1.Text, out int ID))
            {
                MessageBox.Show("من فضلك ادخل بيانات صحيحة للمعرف");
                return;
            }
            if(!int.TryParse(textBox3.Text,out int cash))
            {
                MessageBox.Show("من فضلك ادخل بيانات صحيحة للمبلغ");
                return;
            }
            string query = "INSERT INTO beneficiaries (ID, fullname, cashtaken, phonenumber, takedate, notes) VALUES (@ID, @fullname, @cashtaken, @phonenumber, @takedate, @notes)";
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connection_path))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        // Add parameters to avoid SQL injection
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@fullname", textBox2.Text);
                        command.Parameters.AddWithValue("@cashtaken", cash);
                        command.Parameters.AddWithValue("@phonenumber", textBox4.Text);
                        command.Parameters.AddWithValue("@takedate", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("@notes", textBox5.Text);
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
            if (!int.TryParse(textBox1.Text, out int ID))
            {
                MessageBox.Show("من فضلك ادخل بيانات صحيحة للمعرف");
                return;
            }
            string query = "DELETE FROM beneficiaries WHERE ID = @ID";
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connection_path))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("!تم الحذف بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                            textBox5.Clear();
                            button3_Click(sender, e);
                            button5_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("لم يتم الحذف بنجاح,احد الاسباب عدم وجود المعرف", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
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
            if (!int.TryParse(textBox1.Text, out int ID))
            {
                MessageBox.Show("من فضلك ادخل بيانات صحيحة للمعرف");
                return;
            }
            if (!int.TryParse(textBox3.Text, out int cash))
            {
                MessageBox.Show("من فضلك ادخل بيانات صحيحة للمبلغ");
                return;
            }
            string query = "UPDATE beneficiaries SET fullname = @fullname, cashtaken = @cashtaken, phonenumber = @phonenumber, takedate = @takedate, notes = @notes WHERE ID = @ID";
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connection_path))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        // Add parameters to avoid SQL injection
                        command.Parameters.AddWithValue("@fullname", textBox2.Text);
                        command.Parameters.AddWithValue("@cashtaken", cash);
                        command.Parameters.AddWithValue("@phonenumber", textBox4.Text);
                        command.Parameters.AddWithValue("@takedate", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("@notes", textBox5.Text);
                        command.Parameters.AddWithValue("@ID", ID);
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
                        }
                        else
                        {
                            MessageBox.Show("فشل التعديل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                textBox3.Text = selectedRow.Cells["cashtaken"].Value?.ToString();       
                textBox4.Text = selectedRow.Cells["phonenumber"].Value?.ToString();   
                textBox5.Text = selectedRow.Cells["notes"].Value?.ToString();  
                string dateValue = selectedRow.Cells["takedate"].Value?.ToString();
                if (!string.IsNullOrEmpty(dateValue))
                {
                    if (DateTime.TryParse(dateValue, out DateTime parsedDate))
                    {
                        dateTimePicker1.Value = parsedDate;
                    }
                    else
                    {
                        MessageBox.Show("Invalid date format in the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
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
            toolTip.SetToolTip(textBox3, "لأدخال المبلغ");
        }
        private void textBox4_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(textBox4, "لأدخال رقم الهاتف");
        }
        private void textBox5_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(textBox5, "لأدخال اي ملاحظات");
        }
        private void dataGridView1_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(dataGridView1, "جدول بيانات المستفيدين");
        }
        private void chart1_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(chart1, "رسم بياني يوضح المبلغ المأخوذ في تاريخ معين");
        }
        private void chart2_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(chart2, "رسم بياني يوضح المبلغ المأخوذ في تاريخ معين على شكل دائري");
        }
        private void dateTimePicker1_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(textBox4, "لأختيار التاريخ");
        }
    }
}