using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Library_List
{
	public partial class Form1 : Form
	{
		private Class1 library;
		private System.Windows.Forms.ListView listView1;

		public Form1()
		{
			InitializeComponent();
			library = new Class1();

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{

		}

		private void admin_(object sender, EventArgs e)
		{
			this.Hide();
			Form3 adminForm = new Form3(library, listView1);
			adminForm.Owner = this;
			adminForm.FormClosed += (s, args) => this.Show();
			adminForm.ShowDialog();
		}


		private void button3_Click(object sender, EventArgs e)
		{

		}

		private void user_barrowing(object sender, EventArgs e)
		{
			this.Hide();
			Form2 form2 = new Form2(library);
			form2.Owner = this;
			form2.FormClosed += (s, args) => this.Show();
			form2.ShowDialog();
		}

		private void button3_Click_1(object sender, EventArgs e)
		{

		}
	}
}
