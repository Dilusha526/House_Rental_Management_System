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

namespace House_Rental_Management_System
{
    public partial class Landlords : Form
    {
        public Landlords()
        {
            InitializeComponent();
            ShowLLords();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asus\Documents\HouseRentalMS.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowLLords()
        {
            Con.Open();
            string Query = "Select * from LandLordTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            landLordsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ResetData()
        {
            PhoneTb.Text = "";
            GenCb.SelectedIndex = -1;
            LLnameTb.Text = "";

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Landlords Obj = new Landlords();
            Obj.Show();
            this.Hide();
        }

        private void Landlords_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (LLnameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("MissingFieldException Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into LandLordTbl(LLName,LLPhone,LLGen)values(@LLN,@LLP,@LLG)", Con);
                    cmd.Parameters.AddWithValue("@LLN", LLnameTb.Text);
                    cmd.Parameters.AddWithValue("@LLP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@LLG", GenCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("LandLord Added!!!");
                    Con.Close();
                    ResetData();
                    ShowLLords();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int Key = 0;
        private void landLordsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LLnameTb.Text = landLordsDGV.SelectedRows[0].Cells[1].Value.ToString();
            PhoneTb.Text = landLordsDGV.SelectedRows[0].Cells[2].Value.ToString();
            GenCb.Text = landLordsDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (LLnameTb.Text == "")
            { 
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(landLordsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a LandLord");
            }
            else
            { 
               try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from LandLordTbl where LLId=@LLKey", Con); 
                    cmd.Parameters.AddWithValue("@LLKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Landlord Deleted!!!");
                    Con.Close();
                    ResetData();
                    ShowLLords();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            if (LLnameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            { 
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update LandlordTbl set LLName=@LLN,LLPhone=@LLP,LLGen=@LLG where LLId=@LLKey", Con);
                    cmd.Parameters.AddWithValue("@LLN", LLnameTb.Text);
                    cmd.Parameters.AddWithValue("@LLP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@LLG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LLKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("LandLord Updated!!!");
                    Con.Close();
                    ResetData();
                    ShowLLords();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Categories Obj= new Categories();
            Obj.Show();

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
            this.Hide();

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Apartments Obj = new Apartments();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Rents Obj = new Rents();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
