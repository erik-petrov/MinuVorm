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
	public partial class Admin : Form
	{
		TextBox name, pilt;
		Button send;
		public Admin()
		{
			this.Size = new Size(500,500);
			name = new TextBox
			{
				Location = new Point(200, 100),
				Size = new Size(100, 20)
			};
			pilt = new TextBox
			{
				Location = new Point(200, 150),
				Size = new Size(100, 20)
			};
			send = new Button
			{
				Location = new Point(200, 200),
				Text = "Send"
			};
			this.Controls.Add(send);
			this.Controls.Add(name);
			this.Controls.Add(pilt);
		}
	}
}
