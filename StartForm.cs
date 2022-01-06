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
			Label welcome = new Label
			{
				Text = "Welcome to cinema 'Amogus'",
				Dock = DockStyle.Top,
				TextAlign = ContentAlignment.TopCenter
					
			};
			this.Controls.Add(welcome);
			Button btn = new Button
			{
				Text = "Choose the cinema hall",
				Location = new Point(80, this.Height / 2),
				AutoSize = true,
				BackColor = Color.Red
			};
			btn.Click += Btn_Click;
			this.Controls.Add(btn);
			/*Button btn2 = new Button
			{
				Text = "Choose the cinema hall",
				Location = new Point(80, this.Height/2),
				AutoSize = true,
				BackColor = Color.Red
			};
			btn2.Click += Btn2_Click;
			this.Controls.Add(btn2);*/
		}
		/*private void Btn2_Click(object sender, EventArgs e)
		{
			MyForm frm = new MyForm(10,5);
			frm.StartPosition = FormStartPosition.CenterScreen;
			frm.ShowDialog();
		}*/
		private void Btn_Click(object sender, EventArgs e)
		{
			CinemaPick frm = new CinemaPick("Choose the hall", "", new string[4] {"Small", "Medium", "Big", "Huge" });
			frm.StartPosition = FormStartPosition.CenterScreen;
			frm.Show();
		}
	}
}
