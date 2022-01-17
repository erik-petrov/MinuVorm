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
			Image bg = Image.FromFile("../../images/cinemaBG.png");
			this.BackgroundImage = Image.FromFile("../../images/cinemaBG.png");
			this.BackgroundImageLayout = ImageLayout.Stretch;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.Size = new Size(700, 500);
			this.MaximizeBox = false;
			Label welcome = new Label
			{
				Text = "Welcome to cinema 'Amogus'",
				Height = 100,
				Width = 300,
				TextAlign = ContentAlignment.MiddleCenter,
				Font = new Font("Georgia", 22),
				Location = new Point(200, 100),
				BackColor = Color.Transparent
			};
            Console.WriteLine();
			this.Controls.Add(welcome);
			Button btn = new Button
			{
				Text = "Choose the cinema hall",
				Location = new Point(194, this.Height / 2),
				Font = new Font("Arial", 18),
				Width = 300,
				Height = 50,
				//AutoSize = true,
				ForeColor = Color.White,
				BackColor = Color.DarkRed,
				FlatStyle = FlatStyle.Flat
			};
			btn.FlatAppearance.BorderColor = SystemColors.Control;
			btn.FlatAppearance.BorderSize = 0;
			btn.Click += Btn_Click;
			this.Controls.Add(btn);
		}
        private void Btn_Click(object sender, EventArgs e)
		{
			MoviePick mv = new MoviePick();
			mv.StartPosition = FormStartPosition.CenterScreen;
			mv.Show();
		}
	}
}
