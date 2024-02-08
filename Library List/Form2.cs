using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_List
{
	internal partial class Form2 : Form
	{
		private Class1 library;


		public Form2(Class1 _library)
		{
			InitializeComponent();
			library = _library;
		}

		private void Form2_Load(object sender, EventArgs e)
		{
			MessageBox.Show("Kullanım geçici olarak devre dışıdır. Restorasyon nedeniyle erişim sağlanamıyor.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		private void Borrow(object sender, EventArgs e)
		{

		}

		private void Restituting(object sender, EventArgs e)
		{

		}

		private void Show_all_books(object sender, EventArgs e)
		{
		
			List<BookList> allBooks = library.GetBooks();

		
			listView1.Clear();

			
			listView1.Columns.Add("Book Name", 150);
			listView1.Columns.Add("Author Name", 150);
			listView1.Columns.Add("Publication", 150);
			listView1.Columns.Add("ISBN", 100);
			listView1.Columns.Add("Total Copies", 100);
			listView1.Columns.Add("Borrowed Copies", 120);

		
			foreach (BookList book in allBooks)
			{
				ListViewItem item = new ListViewItem();
				item.Text = book.book_name;
				item.SubItems.Add(book.author_name);
				item.SubItems.Add(book.book_publication);
				item.SubItems.Add(book.ISBN.ToString());
				item.SubItems.Add(book.copy_num.ToString());
				item.SubItems.Add(book.borrow_copy_num.ToString());
				listView1.Items.Add(item);
			}
		}



		private void Search_book(object sender, EventArgs e)
		{

		}

		private void Borrowed_books(object sender, EventArgs e)
		{

		}

		private void mainMenufromUserPage(object sender, EventArgs e)
		{
			this.Hide();
			Form1 form1 = (Form1)this.Owner;
			form1.Show();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		
	}
}
