namespace House_Rental_Management_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          

        }
        private void Reset()
        {
            UnameTb.Text = "";
            PasswordTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void PasswordTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Enter Both UserName and Password!!");
            }
            else if (UnameTb.Text == "admin" && PasswordTb.Text == "admin") 
            {
                Tenants Obj = new Tenants();
                Obj.Show();
                this.Hide();
            }
            else 
            {
                MessageBox.Show("Wrong UserName Or Password!!");
                Reset();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }
    }
}