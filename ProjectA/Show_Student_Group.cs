using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectA
{
    public partial class Show_Student_Group : Form
    {
        public Show_Student_Group()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Manage_Student_Group mg = new Manage_Student_Group();
            this.Hide();
            mg.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard da = new Dashboard();
            this.Hide();
            da.Show();
        }
        DataTable table = new DataTable();
        private void Show_Student_Group_Load(object sender, EventArgs e)
        {
            string query = "select Group1.Id, Group1.Created_On, GroupStudent.GroupId, GroupStudent.StudentId, GroupStudent.Status, GroupStudent.AssignmentDate from Group1 join GroupStudent on Group1.Id=GroupStudent.GroupId";
            var data = DatabaseConnection.getInstance().getAllData(query);
            data.Fill(table);
            dataGridView1.DataSource = table;
        }
    }
}
