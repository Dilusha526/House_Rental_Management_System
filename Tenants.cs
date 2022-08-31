using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace House_Rental_Management_System
{
    public partial class Tenants : Form
    {
        public Tenants()
        {
            InitializeComponent();
            ShowTenants();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asus\Documents\HouseRentalMS.mdf;Integrated Security=True;Connect Timeout=30");
        

        private void ShowTenants() 
        {
            Con.Open();
            string Query = "Select * from TenantTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TenantsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ResetData()
        {
            TNameTb.Text = "";
            GenCb.SelectedIndex = -1;
            PhoneTb.Text = "";
        }
      
        

       
        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
            Obj.Show();
            this.Hide();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (TNameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information|||");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TenantTbl(TenName,TenPhone,TenGen)values(@TN,@TP,@TG)", Con);
                    cmd.Parameters.AddWithValue("@TN", TNameTb.Text);
                    cmd.Parameters.AddWithValue("@TP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@TG", GenCb.SelectedIndex.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenants Added!!!");
                    Con.Close();
                    ResetData();
                    ShowTenants();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        int Key = 0;

        

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TNameTb.Text = TenantsDGV.SelectedRows[0].Cells[1].Value.ToString();
            PhoneTb.Text = TenantsDGV.SelectedRows[0].Cells[2].Value.ToString();
            GenCb.Text = TenantsDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (TNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(TenantsDGV.SelectedRows[0].Cells[0].Value.ToString());
                    
            }
        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Tenant");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from TenantTbl where TenId=@Tkey", Con);
                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Deleted!!!");
                    Con.Close();
                    ResetData();
                    ShowTenants();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                
                
            }
                
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            if(TNameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update TenantTbl set TenName=@TN,TenPhone=@TP,TenGen=@TG where TenID=@TKey", Con);
                    cmd.Parameters.AddWithValue("@TN", TNameTb.Text);
                    cmd.Parameters.AddWithValue("@TP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@TG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    ResetData();
                    ShowTenants();


                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Tenants_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Apartments Obj = new Apartments();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Landlords Obj = new Landlords();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Rents Obj = new Rents();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
    }
}

