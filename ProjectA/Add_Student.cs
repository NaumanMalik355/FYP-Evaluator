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
    public partial class Add_Student : Form
    {
        public Add_Student()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Manage_Student ms = new Manage_Student();
            this.Hide();
            ms.Show();
        }
        //Male Female
        private void button1_Click(object sender, EventArgs e)
        {
            
                string countMaxId = string.Format("select max(Id) from Person");
                var re = DatabaseConnection.getInstance().readData(countMaxId);
                int count = 0;
                while (re.Read())
                {
                    count = re.GetInt32(0);
                }
                string query = string.Format("insert into Person(FirstName,LastName,Contact,Email,DateOfBirth,Gender) values('{0}','{1}','{2}','{3}','{4}',(select Id from Lookup where Value='{5}'))", txtFName.Text, txtLName.Text, txtContact.Text, txtEmail.Text, Convert.ToDateTime(txtDOB.Value), txtGender.Text);
                DatabaseConnection.getInstance().executeQuery(query);
                string qwrt = "insert into Student(Id, RegistrationNo) values('" + (count + 1) + "','" + txtRegNo.Text + "')";
                DatabaseConnection.getInstance().executeQuery(qwrt);
                MessageBox.Show("Data Inserted Successfully...");
                txtRegNo.Text = ""; txtFName.Text = ""; txtLName.Text = ""; txtContact.Text = ""; txtEmail.Text = ""; txtGender.Text = "";
            
        }

        private void Add_Student_Load(object sender, EventArgs e)
        {

        }
    }
}
