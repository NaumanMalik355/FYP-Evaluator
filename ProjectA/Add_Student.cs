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
    public partial class Add_Student : Form
    {
        public int id;
        bool isEdit = false;
        public Add_Student()
        {
            
            InitializeComponent();
            
        }

        public Add_Student(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard ms = new Dashboard();
            this.Hide();
            ms.Show();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            

            if (isEdit)
            {
                string update= string.Format("update Person set FirstName='{0}', LastName='{1}', Contact='{2}',Email='{3}',DateOfBirth='{4}', Gender=(select Id from Lookup where Value='{5}') where Id='{6}'", txtFName.Text, txtLName.Text, txtContact.Text, txtEmail.Text, Convert.ToDateTime(txtDOB.Value), txtGender.Text,id);
                DatabaseConnection.getInstance().executeQuery(update);
                MessageBox.Show("Data Updated Successfully...");
            }
            else
            {
                
                if (txtRegNo.Text!="" && txtFName.Text!="" && txtLName.Text!="" && txtContact.Text!="" && txtEmail.Text!="" && txtGender.Text!="") {
                    try
                    {
                        string countMaxId = string.Format("select max(Id) from Person");
                        var re = DatabaseConnection.getInstance().readData(countMaxId);
                        int count = 0;
                        while (re.Read())
                        {
                            count = re.GetInt32(0);
                        }
                        //MessageBox.Show(count.ToString());
                        string query = string.Format("insert into Person(FirstName,LastName,Contact,Email,DateOfBirth,Gender) values('{0}','{1}','{2}','{3}','{4}',(select Id from Lookup where Value='{5}'))", txtFName.Text, txtLName.Text, txtContact.Text, txtEmail.Text, Convert.ToDateTime(txtDOB.Value), txtGender.Text);
                        DatabaseConnection.getInstance().executeQuery(query);
                        string qwrt = "insert into Student(Id, RegistrationNo) values('" + (count + 1) + "','" + txtRegNo.Text + "')";
                        DatabaseConnection.getInstance().executeQuery(qwrt);
                        MessageBox.Show("Data Inserted Successfully...");
                        txtRegNo.Text = ""; txtFName.Text = ""; txtLName.Text = ""; txtContact.Text = ""; txtEmail.Text = ""; txtGender.Text = "";label8.Text = "";
                    }
                    catch
                    {
                        int count = 1;
                        string query = string.Format("insert into Person(FirstName,LastName,Contact,Email,DateOfBirth,Gender) values('{0}','{1}','{2}','{3}','{4}',(select Id from Lookup where Value='{5}'))", txtFName.Text, txtLName.Text, txtContact.Text, txtEmail.Text, Convert.ToDateTime(txtDOB.Value), txtGender.Text);
                        DatabaseConnection.getInstance().executeQuery(query);
                        string qwrt = "insert into Student(Id, RegistrationNo) values('" + count + "','" + txtRegNo.Text + "')";
                        DatabaseConnection.getInstance().executeQuery(qwrt);
                        MessageBox.Show("Data Inserted Successfully...");
                        txtRegNo.Text = ""; txtFName.Text = ""; txtLName.Text = ""; txtContact.Text = ""; txtEmail.Text = ""; txtGender.Text = ""; label8.Text = "";
                    }
                }
                else
                {
                    label8.Visible = true;
                    label8.Text = "Please fill out this....";
                    
                }
            }
        }
        
        private void Add_Student_Load(object sender, EventArgs e)
        {
            if (id != 0)
            {

                var var = DatabaseConnection.getInstance().getConnection();
                string query = "select * from Person where Id=" + id + "";

                SqlCommand scommand = new SqlCommand(query, var);
                var asd = scommand.ExecuteReader();
                asd.Read();
                txtFName.Text = asd[1].ToString();
                txtLName.Text = asd[2].ToString();
                txtContact.Text = asd[3].ToString();
                txtEmail.Text = asd[4].ToString();
                txtDOB.Text = asd[5].ToString();
                string gender = "select Value from Lookup where Id='"+ asd[6].ToString() + "'";
                var rd1=DatabaseConnection.getInstance().readData(gender);
                rd1.Read();
                txtGender.Text = rd1[0].ToString();
                string reg = "select RegistrationNo from Student where Id='" + id + "'";
                var readReg = DatabaseConnection.getInstance().readData(reg);
                readReg.Read();
                txtRegNo.Text = readReg[0].ToString();
                isEdit = true;
            }
            else
            {
                id = 0;
                isEdit = false;
            }            
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Manage_Student ms = new Manage_Student();
            this.Hide();
            ms.Show();
        }
    }
}
