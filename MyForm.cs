using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MinuVorm
{
	public partial class MyForm : Form
	{
		Label msg = new Label();
		Button[] btnArr = new Button[4];
		string[] btnNames = new string[4];
		TableLayoutPanel tlp = new TableLayoutPanel();
		public MyForm() {}
		public MyForm(string header, string text, string[] btns)
		{
			for (int i = 0; i < btnNames.Length; i++) { btnNames[i] = btns[i]; }
			this.ClientSize = new Size(500, 100);
			this.Text = header;
			int x = 50;
			for (int i = 0; i < btnArr.Length; i++)
			{
				btnArr[i] = new Button
				{
					Location = new Point(x, 50),
					Size = new Size(100, 25),
					Text = btnNames[i]
				};
				btnArr[i].Click += clicked;
				this.Controls.Add(btnArr[i]);
				x += 100;
			}
			msg.Location = new Point(10, 10);
			msg.Text = text;
			this.Controls.Add(msg);
		}
		public MyForm(int x, int y)
		{
			tlp.ColumnCount = x;
			tlp.RowCount = y;
			tlp.ColumnStyles.Clear();
			tlp.RowStyles.Clear();
			this.Size = new Size(875, 650);
			tlp.Size = this.Size;
			for (int i = 0; i < x; i++)
			{
				tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100 / x));
			}
			for (int i = 0; i < y; i++)
			{
				tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / y));
			}
			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					Button btn = new Button
					{
						Text = $"{i}{j}",
						Name = $"btn{i}{j}"
					};
					tlp.Controls.Add(btn, i, j);
				}
			}
			this.Controls.Add(tlp);
		}
		private void clicked(Object sender, EventArgs e)
		{
			Button btn = sender as Button;
			MessageBox.Show("You've clicked on the " + btn.Text + " button!");
		}
	}
}
