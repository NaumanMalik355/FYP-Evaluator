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

namespace ProjectA
{
    public partial class Manage_Student_Group : Form
    {
        public Manage_Student_Group()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard ds = new Dashboard();
            this.Hide();
            ds.Show();
        }

        public string conStr = "Data Source=MALIK\\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True";
        private void Manage_Student_Group_Load(object sender, EventArgs e)
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            txtStudent.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtStudent.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            SqlConnection con = new SqlConnection(conStr);
            con.Open();

            string sql = "select RegistrationNo from Student where not EXISTS(select StudentId from GroupStudent where StudentId=Student.Id)";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader sdr = null;
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                //   col.Add(sdr["RegistrationNo"].ToString());
                col.Add(sdr.GetString(0));
            }
            sdr.Close();
            txtStudent.AutoCompleteCustomSource = col;
            con.Close();

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.Name = "delete";
            button.HeaderText = "Delete";
            button.Text = "DEL";
            button.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(button);
        }

        List<Student> list = new List<Student>();
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            string countMaxId = string.Format("select Id from Student where RegistrationNo='{0}'", txtStudent.Text);
            var re = DatabaseConnection.getInstance().readData(countMaxId);
            int count = 0;
            while (re.Read())
            {
                count = (int)re.GetValue(0);
            }
            if (txtStudent.Text != "")
            {
                Student slist = new Student();
                slist.Id = count;
                slist.RegNo = txtStudent.Text;
                list.Add(slist);
                BindingSource source = new BindingSource();
                source.DataSource = list;
                dataGridView1.DataSource = source;
            }
            else
            {
                label8.Visible = true;
                label8.Text = "*Required";
                txtStudent.Focus();
            }
        }

        private void txtStudent_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToShortDateString();
            string query = "insert into Group1(Created_On) values('" + date + "')";
            DatabaseConnection.getInstance().executeQuery(query);
            string countMaxId = string.Format("select max(Id) from Group1");
            var re = DatabaseConnection.getInstance().readData(countMaxId);
            int count = 0;
            while (re.Read())
            {
                count = re.GetInt32(0);
            }
            int id;

            if (list.Count() > 4 || list.Count() < 2)
            {
                label8.Visible = true;
                label8.Text = "Student Length should be between 2 and 4";
            }

            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    id = list[i].Id;
                    string query1 = string.Format("insert into GroupStudent(GroupId,StudentId,Status,AssignmentDate) values('{0}','{1}','{2}','{3}')", count, id, 4, date);
                    DatabaseConnection.getInstance().executeQuery(query1);
                }
                dataGridView1.DataSource = null;
                Show_Student_Group ssg = new Show_Student_Group();
                this.Hide();
                ssg.Show();
            }
        }

        int rowIndex;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                rowIndex = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                string id = row.Cells[1].Value.ToString();
                foreach (Student s in list.ToList())
                {
                    if (s.Id == int.Parse(id))
                    {
                        list.Remove(s);
                    }
                }
                BindingSource source = new BindingSource();
                source.DataSource = list;
                dataGridView1.DataSource = source;
            }
        }

        private void comboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
