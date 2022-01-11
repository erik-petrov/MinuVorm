using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace MinuVorm
{
	public partial class MyForm : Form
	{
		readonly TableLayoutPanel tlp = new TableLayoutPanel();
		readonly Button btn;
		readonly int w, h;
		readonly int _x, _y;
		int[][] buttonArr;
		List<string> aboutToBuy, bought;
		public MyForm() {}
		
		public MyForm(int x, int y)
		{
			_x = x;
			_y = y;
			using (StreamWriter w = new StreamWriter("../../bought.txt", true)) { w.Write(""); }
			using (StreamReader r = new StreamReader("../../bought.txt"))
			{
				string[] tickets = r.ReadToEnd().Split(',');
				bought = new List<string>();
				string temp = "";
				foreach (var item in tickets)
				{
					temp = item.Replace("\r\n", string.Empty);
					bought.Add(temp);
				}
				Console.WriteLine(bought.Contains("1;1"));
			}
				aboutToBuy = new List<string>();
				buttonArr = new int[x][];
				tlp.ColumnCount = x;
				tlp.RowCount = y;
				tlp.ColumnStyles.Clear();
				tlp.RowStyles.Clear();
				//this.Size = new Size(600, 600);
				for (int i = 0; i < x; i++)
				{
					tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
					tlp.ColumnStyles[i].Width = 100 / x;
				}
				for (int i = 0; i < y; i++)
				{
					tlp.RowStyles.Add(new RowStyle(SizeType.Percent));
					tlp.RowStyles[i].Height = 100 / y;
				}
				for (int i = 0; i < x; i++)
				{
					buttonArr[i] = new int[y];
					for (int j = 0; j < y; j++)
					{
						btn = new Button
						{
							Text = $"{i + 1};{j + 1}",
							Name = $"{i + 1};{j + 1}",
							BackColor = Color.LightGreen,
							Dock = DockStyle.Fill
						};
						btn.Click += TryBuy;
						if (bought.Contains(btn.Name))
						{
							btn.BackColor = Color.Red;
						} else buttonArr[i][j] = 0;
						tlp.Controls.Add(btn, i, j);
					}
				}
				w = (100 / x);
				tlp.Dock = DockStyle.Fill;
				h = (100 / y);
				tlp.Size = new Size(tlp.ColumnCount * w * 2, tlp.RowCount * h * 2);
				this.Controls.Add(tlp);

				MainMenu mainMenu = new MainMenu();
				MenuItem File = mainMenu.MenuItems.Add("&File");
				File.MenuItems.Add(new MenuItem("&Buy", new EventHandler(this.Buy), Shortcut.CtrlS));
				this.Menu = mainMenu;
			}
		private void Buy(object sender, EventArgs e)
		{
			using (StreamWriter w = File.AppendText(@"../../bought.txt"))
			{
				//aboutToBuy.ForEach(x => { w.WriteLine(x[0]+","+x[1]+";"); });
				aboutToBuy.ForEach(x => { w.WriteLine(x + ","); });
			}
			MyForm cin = new MyForm(_x, _y);
			cin.Size = this.Size;
			cin.Show();
			this.Close();
		}
		private void TryBuy(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			if (btn.BackColor == Color.LightGreen) { btn.BackColor = Color.Orange; aboutToBuy.Add(btn.Name); }
			else if (btn.BackColor == Color.Orange) { btn.BackColor = Color.LightGreen; aboutToBuy.Remove(btn.Name); }
			//string[] btnCoords = btn.Name.Split(',');
		}
		private void ReDraw()
		{

		}
	}
}
