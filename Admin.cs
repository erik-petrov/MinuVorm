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
		MySqlDataAdapter adap;
		public Admin()
		{
			dbConn = new MySqlConnection(connStr);
			dbConn.Open();
			this.FormClosing += (s, e) => dbConn.Close();
			this.Size = new Size(1600,800);
			//
			//tabs
			//
			TabControl tabControl1 = new TabControl();
			SuspendLayout();
			//   
			// tabControl1  
			//   
			tabControl1.Location = new Point(8, 16);
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			tabControl1.Size = new Size(this.Width-50, this.Height-75);
			tabControl1.TabIndex = 0;
			this.Controls.Add(tabControl1);
			//
			// movies
			//
			TabPage movies = new TabPage("Movies");
			tabControl1.TabPages.Add(movies);
			//
			//halls
			//
			TabPage halls = new TabPage("Halls");
			tabControl1.TabPages.Add(halls);
			//
			//halls
			//
			adap = new MySqlDataAdapter();

			DataTable hallsT = new DataTable();
			DataGridView dgvH = new DataGridView();
			dgvH.Location = new Point(200, 50);
			dgvH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dgvH.AutoResizeColumns();
			dgvH.Width = 235;
			dgvH.DataBindingComplete += dvgDataBindingComplete;
			string command = "select * from halls";
			adap.SelectCommand = new MySqlCommand(command, dbConn);
			adap.Fill(hallsT);
			BindingSource hSource = new BindingSource();
			hSource.DataSource = hallsT;
			dgvH.DataSource = hSource;

			hallsL = new Label
			{
				Location = new Point(30, 50),
				Size = new Size(100, 20),
				Text = "Add a hall: "
			};
			nameHall = new TextBox
			{
				Location = new Point(30, 70),
				Size = new Size(100, 20)
			};
			Label xL = new Label();
			xL.Location = new Point(35, 100);
			xL.Text = "x";
			xL.Size = new Size(20, 20);
			Label yL = new Label();
			yL.Location = new Point(85, 100);
			yL.Text = "y";
			yL.Size = new Size(20, 20);
			x = new NumericUpDown
			{
				Location = new Point(30, 120),
				Maximum = 30,
				Minimum = 5,
				Size = new Size(40,10)
			};
			y = new NumericUpDown
			{
				Location = new Point(80, 120),
				Maximum = 30,
				Minimum = 5,
				Size = new Size(40, 10)
			};
			send2 = new Button
			{
				Location = new Point(30, 160),
				Text = "Add hall",
				Name = "halls"
			};
			halls.Controls.Add(hallsL);
			halls.Controls.Add(nameHall);
			halls.Controls.Add(x);
			halls.Controls.Add(y);
			halls.Controls.Add(send2);
			halls.Controls.Add(xL);
			halls.Controls.Add(yL);
			halls.Controls.Add(dgvH);
			//
			//movie
			//
			//TODO: make edit, delete and sum crap like that, also make the movies and halls be chosen as a list of options
			DataTable filmsT = new DataTable();
			DataGridView dgvF = new DataGridView();
			dgvF.Location = new Point(200, 50);
			dgvF.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dgvF.AutoResizeColumns();
			dgvF.Width = 370;
			dgvF.DataBindingComplete += dvgDataBindingComplete;
			command = "select * from films";
			adap.SelectCommand = new MySqlCommand(command, dbConn);
			adap.Fill(filmsT);
			BindingSource fSource = new BindingSource();
			fSource.DataSource = filmsT;
			dgvF.DataSource = fSource;
			filmL = new Label
			{
				Location = new Point(30, 50),
				Size = new Size(100, 20),
				Text = "Add a movie: "
			};
			name = new TextBox
			{
				Location = new Point(30, 80),
				Size = new Size(100, 20)
			};
			pilt = new TextBox
			{
				Location = new Point(30, 115),
				Size = new Size(100, 20)
			};
			send = new Button
			{
				Location = new Point(30, 160),
				Text = "Add film",
				Name = "films"
			};
            send.Click += AddToDb;
			send2.Click += AddToDb;
			movies.Controls.Add(send);
			movies.Controls.Add(name);
			movies.Controls.Add(pilt);
			movies.Controls.Add(filmL);
			movies.Controls.Add(dgvF);
		}
		private void dvgDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			var height = 20;
			DataGridView dvg = sender as DataGridView;
			foreach (DataGridViewRow dr in dvg.Rows)
			{
				height += dr.Height;
			}

			dvg.Height = height;
		}
		private void AddToDb(object sender, EventArgs e)
		{
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
			//TODO: make a movie/hall from the table and etc jadajadajada
	}
}
