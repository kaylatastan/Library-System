using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_List
{
	public class BookList
	{
		private string text1;
		private string text2;
		private string text3;
		private object text4;
		private int v;
		private int copyNum;
		private int barrowCopyNum;

		public string author_name { get; set; }
		public string book_name { get; set; }
		public string book_publication { get; set; }
		public int ISBN { get; set; }
		public int copy_num { get; set; }
		public int borrow_copy_num { get; set; }
		public bool IsBorrowed { get; set; }

		public BookList(string kauthor_name, string kbook_name, string kbook_publication, int kISBN, int kcopy_num, int kbarrow_copy_num)
		{
			author_name = kauthor_name;
			book_name = kbook_name;
			book_publication = kbook_publication;
			ISBN = kISBN;
			copy_num = kcopy_num;
			borrow_copy_num = kbarrow_copy_num;

		}

		public BookList(string text1, string text2, string text3, object text4, int v, int copyNum, int barrowCopyNum)
		{
			this.text1 = text1;
			this.text2 = text2;
			this.text3 = text3;
			this.text4 = text4;
			this.v = v;
			this.copyNum = copyNum;
			this.barrowCopyNum = barrowCopyNum;
		}
	}
}
