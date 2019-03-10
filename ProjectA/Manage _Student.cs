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
    public partial class Manage_Student : Form
    {
        public Manage_Student()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Student ads= new Add_Student();
            this.Hide();
            ads.Show();
        }
        DataTable table = new DataTable();
        private void Manage_Student_Load(object sender, EventArgs e)
        {
            string query = "select Student.Id, Student.RegistrationNo, Person.Id, Person.FirstName, Person.LastName, Person.Contact, Person.Email, Person.DateOfBirth, Person.Gender from Person join Student on Person.Id=Student.Id";
            var data = DatabaseConnection.getInstance().getAllData(query);
            data.Fill(table);
            dataGridView1.DataSource = table;

            //// TODO: This line of code loads data into the 'projectADataSet.Student' table. You can move, or remove it, as needed.
            //this.studentTableAdapter.Fill(this.projectADataSet.Student);
            //// TODO: This line of code loads data into the 'projectADataSet.Person' table. You can move, or remove it, as needed.
            //this.personTableAdapter.Fill(this.projectADataSet.Person);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard ds = new Dashboard();
            this.Hide();
            ds.Show();
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
