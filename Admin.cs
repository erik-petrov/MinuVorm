using MySql.Data.MySqlClient;
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
		TextBox name, pilt, nameHall;
		Button send, send2;
		Label hallsL, filmL;
		NumericUpDown x, y;
		string connStr = @"datasource=127.0.0.1;port=3306;username=root;password=;database=cinema;";
		MySqlConnection dbConn;
		MySqlCommand cmd;
		MySqlDataReader reader;
		public Admin()
		{
			this.Size = new Size(500,500);
			//halls
			hallsL = new Label
			{
				Location = new Point(300, 50),
				Size = new Size(100, 20),
				Text = "Add a hall: "
			};
			nameHall = new TextBox
			{
				Location = new Point(300, 100),
				Size = new Size(100, 20)
			};
			Label xL = new Label();
			xL.Location = new Point(305, 130);
			xL.Text = "x";
			xL.Size = new Size(20, 20);
			Label yL = new Label();
			yL.Location = new Point(355, 130);
			yL.Text = "y";
			yL.Size = new Size(20, 20);
			this.Controls.Add(xL);
			this.Controls.Add(yL);
			x = new NumericUpDown
			{
				Location = new Point(300, 150),
				Maximum = 30,
				Minimum = 5,
				Size = new Size(40,10)
			};
			y = new NumericUpDown
			{
				Location = new Point(350, 150),
				Maximum = 30,
				Minimum = 5,
				Size = new Size(40, 10)
			};
			send2 = new Button
			{
				Location = new Point(300, 200),
				Text = "Add hall",
				Name = "halls"
			};
			this.Controls.Add(hallsL);
			this.Controls.Add(nameHall);
			this.Controls.Add(x);
			this.Controls.Add(y);
			//movie
			filmL = new Label
			{
				Location = new Point(100, 50),
				Size = new Size(100, 20),
				Text = "Add a movie: "
			};
			name = new TextBox
			{
				Location = new Point(100, 100),
				Size = new Size(100, 20)
			};
			pilt = new TextBox
			{
				Location = new Point(100, 150),
				Size = new Size(100, 20)
			};
			send = new Button
			{
				Location = new Point(100, 200),
				Text = "Add film",
				Name = "films"
			};
            send.Click += AddToDb;
			send2.Click += AddToDb;
			this.Controls.Add(send);
			this.Controls.Add(name);
			this.Controls.Add(pilt);
			this.Controls.Add(send2);
			this.Controls.Add(filmL);	
		}
		private void AddToDb(object sender, EventArgs e)
		{
            try
            {
				dbConn = new MySqlConnection(connStr);
				dbConn.Open();
				Button btn = sender as Button;
				switch (btn.Name)
				{
					case "films":
						cmd = new MySqlCommand($"insert into {btn.Name}(name, pilt) values(@name, @pilt)", dbConn);
						cmd.Parameters.AddWithValue("@name", name.Text);
						cmd.Parameters.AddWithValue("@pilt", pilt.Text);
						break;
					case "halls":
						cmd = new MySqlCommand($"insert into {btn.Name}(name, x, y) values(@name, @x, @y)");
						cmd.Parameters.AddWithValue("@name", nameHall.Text);
						cmd.Parameters.AddWithValue("@x", x.Value);
						cmd.Parameters.AddWithValue("@y", y.Value);
						break;
				}
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
            {
				MessageBox.Show(ex.Message);
            }
			dbConn.Close();
			//TODO: make a movie/hall from the table and etc jadajadajada
		}
	}
}
