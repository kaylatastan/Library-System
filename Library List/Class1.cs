using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Library_List
{
	 public class Class1
	{
		public List<BookList> books;
		private string connectionString = @"Data Source=LibraryDatabase.db;Version=3;";

		public List<BookList> GetBooks()
		{
			List<BookList> books = new List<BookList>();

			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				connection.Open();

				string sql = "SELECT * FROM Books";

				using (SQLiteCommand command = new SQLiteCommand(sql, connection))
				{
					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							BookList book = new BookList(
								reader["author_name"].ToString(),
								reader["book_name"].ToString(),
								reader["book_publication"].ToString(),
								Convert.ToInt32(reader["ISBN"]),
								Convert.ToInt32(reader["copy_num"]),
								Convert.ToInt32(reader["borrow_copy_num"])
							);
							books.Add(book);
						}
					}
				}
			}

			return books;
		}

		public BookList GetBookByISBN(int ISBN)
		{
			return books.FirstOrDefault(book => book.ISBN == ISBN);
		}
		public List<BookList> GetOverdueBooksFromDatabase()
		{
			List<BookList> overdueBooks = new List<BookList>();

			string connectionString = @"Data Source=LibraryDatabase.db;";

		string query = "SELECT * FROM BorrowedBooks WHERE isreturned < @Today";

			using (SqlConnection connection = new SqlConnection(connectionString)) // -->System.ArgumentException: 'Başlatma dizesinin biçimi, 0 dizininde başlayan belirtime uygun değil.'
			using (SqlCommand command = new SqlCommand(query, connection))
			{
				connection.Open();
				command.Parameters.AddWithValue("@Today", DateTime.Today);

				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						BookList book = new BookList(
							reader["author_name"].ToString(),
							reader["book_name"].ToString(),
							reader["book_publication"].ToString(),
							Convert.ToInt32(reader["ISBN"]),
							Convert.ToInt32(reader["copy_num"]),
							Convert.ToInt32(reader["borrow_copy_num"])
						);
						overdueBooks.Add(book);
					}
				}
			}

			return overdueBooks;
		}


		public List<BookList> GetBorrowedBooks()
		{
			List<BookList> borrowedBooks = new List<BookList>();

			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				connection.Open();

				string sql = "SELECT * FROM BorrowedBooks";

				using (SQLiteCommand command = new SQLiteCommand(sql, connection))
				{
					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							BookList book = new BookList(
								reader["author_name"].ToString(),
								reader["book_name"].ToString(),
								reader["book_publication"].ToString(),
								Convert.ToInt32(reader["ISBN"]),
								Convert.ToInt32(reader["copy_num"]),
								Convert.ToInt32(reader["borrow_copy_num"])
							);
							book.IsBorrowed = true;
							borrowedBooks.Add(book);
						}
					}
				}
			}

			return borrowedBooks;
		}


		public Class1()
		{
			books = new List<BookList>();
		}

		public void AddBook(BookList book)
		{
			books.Add(book);

			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				connection.Open();

				string sql = "INSERT INTO Books (author_name, book_name, book_publication, ISBN, copy_num, borrow_copy_num) " +
							 "VALUES (@AuthorName, @BookName, @BookPublication, @ISBN, @CopyNum, @BarrowCopyNum)";
				using (SQLiteCommand command = new SQLiteCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@AuthorName", book.author_name);
					command.Parameters.AddWithValue("@BookName", book.book_name);
					command.Parameters.AddWithValue("@BookPublication", book.book_publication);
					command.Parameters.AddWithValue("@ISBN", book.ISBN);
					command.Parameters.AddWithValue("@CopyNum", book.copy_num);
					command.Parameters.AddWithValue("@BarrowCopyNum", book.borrow_copy_num);
					command.ExecuteNonQuery();
				}
			}

			
			books = GetBooks();
		}

		public void RemoveBook(BookList bookToRemove)
		{
			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				try
				{
					connection.Open();

					// Kütüphanedeki kitapları güncelle
					books.Remove(bookToRemove);

					// Veritabanından kitabı kaldır
					string query = "DELETE FROM Books WHERE ISBN = @ISBN";
					using (SQLiteCommand command = new SQLiteCommand(query, connection))
					{
						command.Parameters.AddWithValue("@ISBN", bookToRemove.ISBN);
						command.ExecuteNonQuery();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Kitap silinirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}








	}
}