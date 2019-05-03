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
            Add_Student ads = new Add_Student();
            this.Hide();
            ads.Show();
        }

        DataTable table = new DataTable();
        private void Manage_Student_Load(object sender, EventArgs e)
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            string query = "select Student.Id, Student.RegistrationNo, Person.Id, Person.FirstName, Person.LastName, Person.Contact, Person.Email, Person.DateOfBirth, Person.Gender from Person join Student on Person.Id=Student.Id";
            var data = DatabaseConnection.getInstance().getAllData(query);
            data.Fill(table);
            dataGridView1.DataSource = table;

            DataGridViewColumn col = dataGridView1.Columns[2];
            col.Visible = false;

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.Name = "edit";
            button.HeaderText = "Edit";
            button.Text = "Edit";
            button.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(button);

            DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            button1.Name = "delete";
            button1.HeaderText = "Delete";
            button1.Text = "DEL";
            button1.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(button1);
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

        int rowIndex;
        string id;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                try
                {
                    rowIndex = e.RowIndex;
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];
                    id = row.Cells[0].Value.ToString();
                    int idd = int.Parse(id);
                    Add_Student ass = new Add_Student(idd);
                    this.Hide();
                    ass.Show();
                }
                catch
                {
                    MessageBox.Show("Try Again!");
                }
            }
            if (e.ColumnIndex == 10 && DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    string sID = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
                    string query = string.Format("delete Student where Id='{0}'", Convert.ToInt32(sID));
                    DatabaseConnection.getInstance().executeQuery(query);
                    string queryy = string.Format("delete Person where Id='{0}'", Convert.ToInt32(sID));
                    DatabaseConnection.getInstance().executeQuery(queryy);
                    //MessageBox.Show("Data deleted successfully...");
                    dataGridView1 = null;
                    this.Refresh();

                    //string queryyy = "select Student.Id, Student.RegistrationNo, Person.Id, Person.FirstName, Person.LastName, Person.Contact, Person.Email, Person.DateOfBirth, Person.Gender from Person join Student on Person.Id=Student.Id";
                    //var data = DatabaseConnection.getInstance().getAllData(queryyy);
                    //data.Fill(table);
                    //dataGridView1.DataSource = table;

                }
                catch
                {
                    MessageBox.Show("Try Again!");
                }
            }
        }
    }
}
