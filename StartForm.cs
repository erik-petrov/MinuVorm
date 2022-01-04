using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinuVorm
{
	class StartForm : MyForm
	{
		public StartForm()
		{
			Button btn = new Button
			{
				Text = "Hi",
				Location = new Point(20, 20),
				BackColor = Color.Red
			};
			btn.Click += Btn_Click;
			this.Controls.Add(btn);
			Button btn2 = new Button
			{
				Text = "Hi2",
				Location = new Point(20, 40),
				BackColor = Color.Red
			};
			btn2.Click += Btn2_Click;
			this.Controls.Add(btn2);
		}

		private void Btn2_Click(object sender, EventArgs e)
		{
			MyForm frm = new MyForm(10,10);
			frm.StartPosition = FormStartPosition.CenterScreen;
			frm.ShowDialog();
		}

		private void Btn_Click(object sender, EventArgs e)
		{
			MyForm frm = new MyForm("amogus", "sus", new string[4] {"s", "u", "s", "y" });
			frm.StartPosition = FormStartPosition.CenterScreen;
			frm.ShowDialog();
		}
	}
}
