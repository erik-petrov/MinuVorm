using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinuVorm
{
	public partial class CinemaPick : Form
	{
		readonly Label msg = new Label();
		readonly Button[] btnArr = new Button[4];
		readonly string[] btnNames = new string[4];
		MyForm cin;
		public CinemaPick(string header, string text, string[] btns)
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
				btnArr[i].Click += Clicked;
				this.Controls.Add(btnArr[i]);
				x += 100;
			}
			msg.Location = new Point(10, 10);
			msg.Text = text;
			this.Controls.Add(msg);
		}
		private void Clicked(Object sender, EventArgs e)
		{
			Button btn = sender as Button;
			switch (btn.Text)
			{
				case "Small":
					cin = new MyForm(5, 5);
					cin.Size = new Size(100, 100);
					cin.Show();
					break;
				case "Medium":
					cin = new MyForm(7, 7);
					cin.Size = new Size(500, 500);
					cin.Show();
					break;
				case "Big":
					cin = new MyForm(10, 10);
					cin.Size = new Size(500, 500);
					cin.Show();
					break;
				case "Huge":
					cin = new MyForm(20, 20);
					cin.Size = new Size(1000, 1000);
					cin.Show();
					break;
			}
		}
	}
}
