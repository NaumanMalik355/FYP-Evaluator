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

            txtStudent.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtStudent.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            int number=0;
            string checkExist = "select StudentId from GroupStudent";
            var exist = DatabaseConnection.getInstance().readData(checkExist);
            while (exist.Read())
            {
                number=(int)exist.GetValue(0);

            }
            string sql = "select * from Student where Id!='"+number+"'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader sdr = null;
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                col.Add(sdr["RegistrationNo"].ToString());
            }
            sdr.Close();
            txtStudent.AutoCompleteCustomSource = col;
            con.Close();
        }
        List<Student> list = new List<Student>();

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            string countMaxId = string.Format("select Id from Student where RegistrationNo='{0}'",txtStudent.Text);
            var re = DatabaseConnection.getInstance().readData(countMaxId);
            int count =0;
            while (re.Read())
            {
                count = (int)re.GetValue(0);
            }
            Student alist = new Student();
            alist.Id = count;
            alist.RegNo = txtStudent.Text;
            list.Add(alist);
            
            BindingSource source = new BindingSource();
            source.DataSource = list;
            dataGridView1.DataSource = source;
            //txtStudent.Text = "";
        }
       
        private void txtStudent_TextChanged(object sender, EventArgs e)
        {
               
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("MM/dd/yyyy");
            string query = "insert into Group1(Created_On) values('"+date+"')";
            DatabaseConnection.getInstance().executeQuery(query);
            string countMaxId = string.Format("select max(Id) from Group1");
            var re = DatabaseConnection.getInstance().readData(countMaxId);
            int count = 0;
            while (re.Read())
            {
                count = re.GetInt32(0);
            }
            int id;
            for (int i = 0; i < list.Count; i++)
            {
                id = list[i].Id;
                string query1 = string.Format("insert into GroupStudent(GroupId,StudentId,Status,AssignmentDate) values('{0}','{1}','{2}','{3}')", count,id,4, date);
                DatabaseConnection.getInstance().executeQuery(query1);
                //string delStudent = "delete Student where Id='"+list[i].Id+"'";
                //DatabaseConnection.getInstance().executeQuery(delStudent);
                //string delPerson = "delete Person where Id='" + list[i].Id + "'";
                //DatabaseConnection.getInstance().executeQuery(delPerson);
            }
            dataGridView1.DataSource = null;
            Show_Student_Group ssg = new Show_Student_Group();
            this.Hide();
            ssg.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void comboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboStatus.Text == "Active")
            //{
            //    dataGridView1.DataSource = list;
            //}
            //else
            //{
            //    dataGridView1.DataSource = null;
            //}
        }
    }
}
