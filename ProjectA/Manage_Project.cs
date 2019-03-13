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
    public partial class Manage_Project : Form
    {
        public Manage_Project()
        {
            InitializeComponent();
        }

        private void Manage_Project_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'projectADataSet4.Project' table. You can move, or remove it, as needed.
            this.projectTableAdapter.Fill(this.projectADataSet4.Project);

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.Name = "delete";
            button.HeaderText = "Delete";
            button.Text = "DEL";
            button.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(button);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard();
            this.Hide();
            d.Show();
        }
        DataTable table = new DataTable();
        private void btnAddProject_Click(object sender, EventArgs e)
        {
            try {
                string query = string.Format("insert into Project(Description,Title) values('{0}','{1}')", rtBoxDescription.Text, txtTitle.Text);
                DatabaseConnection.getInstance().executeQuery(query);
                MessageBox.Show("Data Inserted Successfully...");
                //this.Refresh();
                //dataGridView1 = null;
                //string show = "select * from Project";
                //var data = DatabaseConnection.getInstance().getAllData(show);
                //data.Fill(table);
                //dataGridView1.DataSource = table;

                //dataGridView1 = null;
                //this.projectTableAdapter.Fill(this.projectADataSet4.Project);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error "+ex.Message);
            }
        }
        //DataTable table = new DataTable();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
               string sID = dataGridView1.CurrentRow.Cells["idDataGridViewTextBoxColumn"].Value.ToString();
                string query = "delete Project where Id='"+int.Parse(sID)+"'";
                DatabaseConnection.getInstance().executeQuery(query);
            //    dataGridView1 = null;
                this.Refresh();
                // this.projectTableAdapter.Fill(this.projectADataSet4.Project); 
               // string show = "select * from Project";
               // var data = DatabaseConnection.getInstance().getAllData(show);
               // data.Fill(table);
               // dataGridView1.DataSource = table;

               
            }
        }
    }
}
