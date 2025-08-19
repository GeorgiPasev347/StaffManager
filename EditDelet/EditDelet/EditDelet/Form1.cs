namespace EditDelet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManagePage form2 = new ManagePage();
            form2.Show();
        }
    }
}
