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
    public partial class Manage_Evaluation : Form
    {
        public Manage_Evaluation()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard();
            this.Hide();
            d.Show();
        }
        DataTable table = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            try {
                string query = string.Format("Insert into Evaluation values('{0}','{1}','{2}')", txtName.Text, Convert.ToInt32(txtTotalmarks.Text), Convert.ToInt32(txtObtainedMarks.Text));
                DatabaseConnection.getInstance().executeQuery(query);
                MessageBox.Show("Data inserted successfully...");
                txtName.Text = "";txtTotalmarks.Text = "";txtObtainedMarks.Text = "";
            }catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
            string queryy = "select * from Evaluation";
            var data=DatabaseConnection.getInstance().getAllData(queryy);
            data.Fill(table);
            dataGridView1.DataSource = table;
            //BindingSource source = new BindingSource();
            //source.DataSource = data;
            //dataGridView1.DataSource = source;
        }

        private void Manage_Evaluation_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'projectADataSet5.Evaluation' table. You can move, or remove it, as needed.
            //this.evaluationTableAdapter.Fill(this.projectADataSet5.Evaluation);
            string query = "select Id,Name,TotalMarks,TotalWeightage from Evaluation";
            var data = DatabaseConnection.getInstance().getAllData(query);
            data.Fill(table);
            dataGridView1.DataSource = table;
            
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.Name = "delete";
            button.HeaderText = "Delete";
            button.Text = "DEL";
            button.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(button);
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                try
                {
                    string eID = dataGridView1.CurrentRow.Cells["idDataGridViewTextBoxColumn"].Value.ToString();
                string query = string.Format("delete Evaluation where Id='{0}'", Convert.ToInt32(eID));
                DatabaseConnection.getInstance().executeQuery(query);
                MessageBox.Show("Deleted Successfully...");
                //string queryy = "select * from Evaluation";
                //var data = DatabaseConnection.getInstance().getAllData(queryy);
                //data.Fill(table);
               // dataGridView1.DataSource = table;
                //this is for refreshing
                //dataGridView1.Rows.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                }
            }
        }
    }
}
