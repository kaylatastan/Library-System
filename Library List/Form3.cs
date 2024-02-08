using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_List
{
	public partial class Form3 : Form
	{
		private Class1 library;
		private ListView listView1;
		private ListBox listBox1;


		public Form3(Class1 _library, ListView _listView1)
		{
			InitializeComponent();
			library = _library;
			listView1 = _listView1;

			listView1 = new ListView();
			listView1.Dock = DockStyle.Fill;
			listView1.View = View.Details;
			this.Controls.Add(listView1);
		}

		private void Form3_Load(object sender, EventArgs e)
		{
			List<BookList> allBooks = library.GetBooks();

			// Clear existing rows in the DataGridView
			dataGridView1.Rows.Clear();

			// Add headers to the DataGridView
			dataGridView1.Columns.Clear();
			dataGridView1.Columns.Add("BookNameColumn", "Book Name");
			dataGridView1.Columns.Add("AuthorNameColumn", "Author Name");
			dataGridView1.Columns.Add("PublicationColumn", "Publication");
			dataGridView1.Columns.Add("ISBNColumn", "ISBN");
			dataGridView1.Columns.Add("TotalCopiesColumn", "Total Copies");
			dataGridView1.Columns.Add("BorrowedCopiesColumn", "Borrowed Copies");

			// Add books to the DataGridView
			foreach (BookList book in allBooks)
			{
				dataGridView1.Rows.Add(book.book_name, book.author_name, book.book_publication, book.ISBN, book.copy_num, book.borrow_copy_num);
			}
		}


		private void mainMenufromAdminPage(object sender, EventArgs e)
		{
			this.Hide();
			Form1 form1 = (Form1)this.Owner;
			form1.Show();
		}

		private void overdueBooks(object sender, EventArgs e)
		{
			// ListView'dan tüm öğeleri al
			List<BookList> overdueBooks = library.GetOverdueBooksFromDatabase();

			// DataGridView'ı temizle
			dataGridView1.Rows.Clear();

			// DataGridView'a yeni satırlar ekle
			foreach (BookList book in overdueBooks)
			{
				dataGridView1.Rows.Add(book.book_name, book.author_name, book.book_publication, book.ISBN, book.copy_num, book.borrow_copy_num);
			}
		}


		private void UpdateDataGridView()
		{
			// DataGridView'ı temizle
			dataGridView1.Rows.Clear();
			dataGridView1.Columns.Clear();

			// Sütunları ekle
			dataGridView1.Columns.Add("BookName", "Book Name");
			dataGridView1.Columns.Add("AuthorName", "Author Name");
			dataGridView1.Columns.Add("Publication", "Publication");
			dataGridView1.Columns.Add("ISBN", "ISBN");
			dataGridView1.Columns.Add("TotalCopies", "Total Copies");
			dataGridView1.Columns.Add("BorrowedCopies", "Borrowed Copies");

			// ListView'dan tüm öğeleri al
			List<BookList> allBooks = library.GetBooks();

			// DataGridView'a yeni satırlar ekle
			foreach (BookList book in allBooks)
			{
				dataGridView1.Rows.Add(book.book_name, book.author_name, book.book_publication, book.ISBN, book.copy_num, book.borrow_copy_num);
			}
		}


		private void Add(object sender, EventArgs e)
		{
			// Check if any of the text boxes are empty
			if (string.IsNullOrWhiteSpace(textBox1.Text) ||
				string.IsNullOrWhiteSpace(textBox2.Text) ||
				string.IsNullOrWhiteSpace(textBox3.Text) ||
				string.IsNullOrWhiteSpace(textBox7.Text) ||
				string.IsNullOrWhiteSpace(textBox5.Text) ||
				string.IsNullOrWhiteSpace(textBox6.Text))
			{
				MessageBox.Show("Tüm metin kutularını doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Yeni kitap bilgilerini al
			string bookName = textBox1.Text;
			string authorName = textBox2.Text;
			string publication = textBox3.Text;
			int ISBN, copyNum, barrowCopyNum;

			if (!int.TryParse(textBox7.Text, out ISBN))
			{
				MessageBox.Show("ISBN numarası geçerli bir tamsayı değil. Lütfen doğru bir ISBN numarası girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!int.TryParse(textBox5.Text, out copyNum))
			{
				MessageBox.Show("Kopya sayısı geçerli bir tamsayı değil. Lütfen doğru bir kopya sayısı girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!int.TryParse(textBox6.Text, out barrowCopyNum))
			{
				MessageBox.Show("Ödünç kopya sayısı geçerli bir tamsayı değil. Lütfen doğru bir ödünç kopya sayısı girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Eğer aynı ISBN numarasına sahip bir kitap zaten varsa, hata mesajı göster
			if (library.GetBookByISBN(ISBN) != null)
			{
				MessageBox.Show("Bu ISBN numarasına sahip bir kitap zaten var. Lütfen farklı bir ISBN numarası girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Yeni kitap nesnesi oluştur
			BookList newBook = new BookList(bookName, authorName, publication, ISBN, copyNum, barrowCopyNum);

			// Kitabı kütüphaneye ekle
			library.AddBook(newBook);

			// DataGridView'ı güncelle
			UpdateDataGridView();
		}




		private object textBox4()
		{
			throw new NotImplementedException();
		}

		private void UpdateListBox()
		{
			listBox1.Items.Clear();


			listBox1.Items.Add("Book Name                      Author Name                    Publication         ISBN           Total Copies   Borrowed Copies");
			foreach (BookList book in library.GetBooks())
			{

				string bookInfo = string.Format(
					"{0,-40} {1,-50} {2,-20} {3,-15} {4,-15} {5,-15}",
					book.book_name,
					book.author_name,
					book.book_publication,
					book.ISBN,
					book.copy_num,
					book.borrow_copy_num

				);

				listBox1.Items.Add(bookInfo);
			}
			listBox1.HorizontalScrollbar = true;
		}

		private void UpdateListView()
		{
			listView1.Items.Clear();
			listView1.Columns.Clear();

			listView1.Columns.Add("Book Name", 150);
			listView1.Columns.Add("Author Name", 150);
			listView1.Columns.Add("Publication", 150);
			listView1.Columns.Add("ISBN", 100);
			listView1.Columns.Add("Total Copies", 100);
			listView1.Columns.Add("Borrowed Copies", 120);


			foreach (BookList book in library.GetBooks())
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


			foreach (BookList book in library.GetBorrowedBooks())
			{
				ListViewItem item = new ListViewItem(book.book_name);
				item.SubItems.Add(book.author_name);
				item.SubItems.Add(book.book_publication);
				item.SubItems.Add(book.ISBN.ToString());
				item.SubItems.Add(book.copy_num.ToString());
				item.SubItems.Add(book.borrow_copy_num.ToString());

				listView1.Items.Add(item);
			}
		}

		private void Remove(object sender, EventArgs e)
		{
			// Seçili olan satırın verisini al
			if (dataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

				// Seçilen satırdaki ISBN değerini al
				int ISBNToRemove = Convert.ToInt32(selectedRow.Cells["ISBN"].Value);

				// Kütüphanedeki kitapların listesini al
				List<BookList> allBooks = library.GetBooks();

				// ISBN değerine göre kitabı bul ve kütüphaneden kaldır
				BookList bookToRemove = allBooks.FirstOrDefault(book => book.ISBN == ISBNToRemove);
				if (bookToRemove != null)
				{
					library.RemoveBook(bookToRemove);

					// DataGridView'ı güncelle
					UpdateDataGridView();
				}
				else
				{
					MessageBox.Show("Belirtilen ISBN numarasına sahip bir kitap bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show("Lütfen bir kitap seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void textBox3_TextChanged(object sender, EventArgs e)
		{

		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void listallbooks_Click(object sender, EventArgs e)
		{
			List<BookList> allBooks = library.GetBooks();

			// Clear existing rows in the DataGridView
			dataGridView1.Rows.Clear();

			// Add headers to the DataGridView
			dataGridView1.Columns.Clear();
			dataGridView1.Columns.Add("BookNameColumn", "Book Name");
			dataGridView1.Columns.Add("AuthorNameColumn", "Author Name");
			dataGridView1.Columns.Add("PublicationColumn", "Publication");
			dataGridView1.Columns.Add("ISBNColumn", "ISBN");
			dataGridView1.Columns.Add("TotalCopiesColumn", "Total Copies");
			dataGridView1.Columns.Add("BorrowedCopiesColumn", "Borrowed Copies");

			foreach (BookList book in allBooks)
			{
				// Create a new row
				DataGridViewRow row = new DataGridViewRow();

				// Add cells to the row
				row.Cells.Add(new DataGridViewTextBoxCell { Value = book.book_name });
				row.Cells.Add(new DataGridViewTextBoxCell { Value = book.author_name });
				row.Cells.Add(new DataGridViewTextBoxCell { Value = book.book_publication });
				row.Cells.Add(new DataGridViewTextBoxCell { Value = book.ISBN });
				row.Cells.Add(new DataGridViewTextBoxCell { Value = book.copy_num });
				row.Cells.Add(new DataGridViewTextBoxCell { Value = book.borrow_copy_num });

				// Add the row to the DataGridView
				dataGridView1.Rows.Add(row);
			}
		}


		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			dataGridView1.Rows.Clear();

			// ListView'dan tüm öğeleri al
			foreach (ListViewItem item in listView1.Items)
			{
				// ListViewItem'dan her sütundaki veriyi al
				string bookName = item.SubItems[0].Text;
				string authorName = item.SubItems[1].Text;
				string publication = item.SubItems[2].Text;
				string ISBN = item.SubItems[3].Text;
				string totalCopies = item.SubItems[4].Text;
				string borrowedCopies = item.SubItems[5].Text;

				// DataGridView'a yeni bir satır ekle
				dataGridView1.Rows.Add(bookName, authorName, publication, ISBN, totalCopies, borrowedCopies);
			}
		}

	}
}
