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

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard();
            this.Hide();
            d.Show();
        }

        private void btnAddProject_Click(object sender, EventArgs e)
        {
            try {
                string query = string.Format("insert into Project(Description,Title) values('{0}','{1}')", rtBoxDescription.Text, txtTitle.Text);
                DatabaseConnection.getInstance().executeQuery(query);
                MessageBox.Show("Data Inserted Successfully...");
            }catch(Exception ex)
            {
                MessageBox.Show("Error "+ex.Message);
            }
        }
    }
}
