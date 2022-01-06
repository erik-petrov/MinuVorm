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
		readonly TableLayoutPanel tlp = new TableLayoutPanel();
		readonly Button btn;
		readonly int w, h;
		public MyForm() {}
		
		public MyForm(int x, int y)
		{
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
				for (int j = 0; j < y; j++)
				{
					btn = new Button
					{
						Text = $"{i + 1};{j + 1}",
						Name = $"btn{i + 1}{j + 1}",
						Dock = DockStyle.Fill
					};
					tlp.Controls.Add(btn, i, j);
				}
			}
			w = (100 / x);
			tlp.Dock = DockStyle.Fill;
			h = (100 / y);
			tlp.Size = new Size(tlp.ColumnCount * w*2, tlp.RowCount * h*2);
			this.Controls.Add(tlp);
		}
	}
}
