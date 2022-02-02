using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinuVorm
{
	public partial class Admin : Form
	{
		TextBox name, pilt, nameHall, editFilmTB, editPiltTB, editHallsTB;
		Button send, send2, editHB, editFB, deleteH, deleteF;
		Label hallsL, filmL, editFL, editHL;
		NumericUpDown x, y, ex, ey;
		string connStr = @"datasource=127.0.0.1;port=3306;username=root;password=;database=cinema;";
		MySqlConnection dbConn;
		MySqlCommand cmd;
		MySqlDataAdapter adap;
		DataGridView dgvH, dgvF, dgvT;
		PictureBox picture;
		public Admin()
		{
			dbConn = new MySqlConnection(connStr);
			dbConn.Open();
			this.FormClosing += (s, e) => dbConn.Close();
			this.Size = new Size(1000,350);
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
			//tickets
			//
			TabPage tickets = new TabPage("Tickets");
			tabControl1.TabPages.Add(tickets);
			//
			//halls
			//
			adap = new MySqlDataAdapter();
			DataTable hallsT = new DataTable();
			dgvH = new DataGridView();
			dgvH.Location = new Point(170, 50);
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
            dgvH.SelectionChanged += DgvH_SelectionChanged;
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
			send2.Click += AddToDb;
			halls.Controls.Add(hallsL);
			halls.Controls.Add(nameHall);
			halls.Controls.Add(x);
			halls.Controls.Add(y);
			halls.Controls.Add(send2);
			halls.Controls.Add(xL);
			halls.Controls.Add(yL);
			halls.Controls.Add(dgvH);
			//
			//editHalls
			//
			editHL = new Label
			{
				Location = new Point(450, 50),
				Size = new Size(100, 20),
				Text = "Edit a hall: "
			};
			editHallsTB = new TextBox
			{
				Location = new Point(450, 70),
				Size = new Size(100, 20)
			};
			Label exL = new Label();
			exL.Location = new Point(455, 100);
			exL.Text = "x";
			exL.Size = new Size(20, 20);
			Label eyL = new Label();
			eyL.Location = new Point(505, 100);
			eyL.Text = "y";
			eyL.Size = new Size(20, 20);
			ex = new NumericUpDown
			{
				Location = new Point(450, 120),
				Size = new Size(40, 10)
			};
			ey = new NumericUpDown
			{
				Location = new Point(500, 120),
				Size = new Size(40, 10)
			};
			editHB = new Button
			{
				Location = new Point(450, 160),
				Text = "Edit hall",
				Name = "halls"
			};
			editHB.Click += UpdateRow;
			deleteH = new Button
			{
				Location = new Point(200, 220),
				Text = "Delete a hall",
				Name = "halls"
			};
            deleteH.Click += DeleteRow;
			halls.Controls.Add(deleteH);
			halls.Controls.Add(editHL);
			halls.Controls.Add(editHallsTB);
			halls.Controls.Add(exL);
			halls.Controls.Add(eyL);
			halls.Controls.Add(ex);
			halls.Controls.Add(ey);
			halls.Controls.Add(editHB);
			//
			//
			//movie
			//
			//
			//TODO: make edit, delete and sum crap like that, also make the movies and halls be chosen as a list of options
			DataTable filmsT = new DataTable();
			dgvF = new DataGridView();
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
            dgvF.SelectionChanged += DgvF_SelectionChanged;
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
			//
			//editFilm
			//
			editFL = new Label
			{
				Location = new Point(630, 50),
				Size = new Size(100, 20),
				Text = "Currently editing: "
			};
			editFilmTB = new TextBox
			{
				Location = new Point(630, 80),
				Size = new Size(100, 50)
			};
			editPiltTB = new TextBox
			{
				Location = new Point(630, 115),
				Size = new Size(100, 50)
			};
			editFB = new Button
			{
				Location = new Point(630, 160),
				Text = "Edit film",
				Name = "films"
			};
			editFB.Click += UpdateRow;
			deleteF = new Button
			{
				Location = new Point(200, 220),
				Text = "Delete a movie",
				AutoSize = true,
				Name = "films"
			};
			picture = new PictureBox
			{
				Location = new Point(800, 40),
				Size = new Size(100, 200),
				Image = null,
				SizeMode = PictureBoxSizeMode.StretchImage
			};
			deleteF.Click += DeleteRow;
			movies.Controls.Add(picture);
			movies.Controls.Add(deleteF);
			movies.Controls.Add(send);
			movies.Controls.Add(name);
			movies.Controls.Add(pilt);
			movies.Controls.Add(filmL);
			movies.Controls.Add(dgvF);
			movies.Controls.Add(editFL);
			movies.Controls.Add(editFilmTB);
			movies.Controls.Add(editPiltTB);
			movies.Controls.Add(editFB);
			//
			//
			//Tickets
			//
			//
			DataTable ticketsT = new DataTable();
			dgvT = new DataGridView();
			dgvT.Location = new Point(100, 20);
			dgvT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dgvT.AutoResizeColumns();
			dgvT.Width = 370;
			dgvT.DataBindingComplete += dvgDataBindingComplete;
			command = "select * from tickets";
			adap.SelectCommand = new MySqlCommand(command, dbConn);
			adap.Fill(ticketsT);
			BindingSource tSource = new BindingSource();
			tSource.DataSource = ticketsT;
			dgvT.DataSource = tSource;
            dgvT.SelectionChanged += DgvT_SelectionChanged;
			tickets.Controls.Add(dgvT);
		}
        private void DgvT_SelectionChanged(object sender, EventArgs e)
        {
			DataGridView dgv = sender as DataGridView;
			Int32 selectedRowCount = dgv.Rows.GetRowCount(DataGridViewElementStates.Selected);
			if (selectedRowCount > 0)
			{
				editHallsTB.Text = dgv.SelectedRows[0].Cells[1].Value.ToString();
				ex.Value = (int)dgv.SelectedRows[0].Cells[2].Value;
				ey.Value = (int)dgv.SelectedRows[0].Cells[3].Value;
			}
		}
        private void DeleteRow(object sender, EventArgs e)
        {
			Button btn = sender as Button;
			switch (btn.Name)
			{
				case "films":
					if (dgvF.SelectedRows.Count == 0) return;
					cmd = new MySqlCommand($"delete from {btn.Name} where id = @id", dbConn);
					cmd.Parameters.AddWithValue("@id", dgvF.SelectedRows[0].Cells[0].Value);
					break;
				case "halls":
					if (dgvH.SelectedRows.Count == 0) return;
					cmd = new MySqlCommand($"delete from {btn.Name} where id = @id", dbConn);
					cmd.Parameters.AddWithValue("@id", dgvH.SelectedRows[0].Cells[0].Value);
					break;
				default:
					break;
			}
			cmd.ExecuteNonQuery();
		}
        private void DgvH_SelectionChanged(object sender, EventArgs e)
        {
			DataGridView dgv = sender as DataGridView;
			Int32 selectedRowCount = dgv.Rows.GetRowCount(DataGridViewElementStates.Selected);
			if (selectedRowCount > 0)
			{
				editHallsTB.Text = dgv.SelectedRows[0].Cells[1].Value.ToString();
				ex.Value = (int)dgv.SelectedRows[0].Cells[2].Value;
				ey.Value = (int)dgv.SelectedRows[0].Cells[3].Value;
			}
		}
        private void DgvF_SelectionChanged(object sender, EventArgs e)
        {
			DataGridView dgv = sender as DataGridView;
			Int32 selectedRowCount = dgv.Rows.GetRowCount(DataGridViewElementStates.Selected);
			if (selectedRowCount > 0)
			{
				editFilmTB.Text = dgv.SelectedRows[0].Cells[1].Value.ToString();
				editPiltTB.Text = dgv.SelectedRows[0].Cells[2].Value.ToString();
				picture.Image = SaveImage(editPiltTB.Text, "movie", ImageFormat.Png);
			}
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
					if(name.Text == "" || pilt.Text == "") { MessageBox.Show("Can't add an empty cell."); return; }
					cmd = new MySqlCommand($"insert into {btn.Name}(name, pilt) values(@name, @pilt)", dbConn);
					cmd.Parameters.AddWithValue("@name", name.Text);
					cmd.Parameters.AddWithValue("@pilt", pilt.Text);
					break;
				case "halls":
					if (x.Text == "" || y.Text == "" || nameHall.Text == "") { MessageBox.Show("Can't add an empty cell."); return; }
					cmd = new MySqlCommand($"insert into {btn.Name}(name, x, y) values(@name, @x, @y)", dbConn);
					cmd.Parameters.AddWithValue("@name", nameHall.Text);
					cmd.Parameters.AddWithValue("@x", x.Value);
					cmd.Parameters.AddWithValue("@y", y.Value);
					break;
			}
			cmd.ExecuteNonQuery();
		}
		private void UpdateRow(object sender, EventArgs e)
        {
			Button btn = sender as Button;
			
            switch (btn.Name)
            {
				case "films":
					if(dgvF.SelectedRows.Count == 0) { MessageBox.Show("Please choose a row from the gridview."); return; }
					cmd = new MySqlCommand($"update `{btn.Name}` set name = @name, pilt = @pilt where id = @id", dbConn);
					cmd.Parameters.AddWithValue("@name", editFilmTB.Text);
					cmd.Parameters.AddWithValue("@pilt", editPiltTB.Text);
					cmd.Parameters.AddWithValue("@id", (int)dgvF.SelectedRows[0].Cells[0].Value);
					break;
				case "halls":
					cmd = new MySqlCommand($"update `{btn.Name}` set name = @name, x = @x, y = @y where id = @id", dbConn);
					cmd.Parameters.AddWithValue("@name", editHallsTB.Text);
					cmd.Parameters.AddWithValue("@x", (int)ex.Value);
					cmd.Parameters.AddWithValue("@y", (int)ey.Value);
					cmd.Parameters.AddWithValue("@id", (int)dgvH.SelectedRows[0].Cells[0].Value);
					break;
			}
			cmd.ExecuteNonQuery();
        }
		public Bitmap SaveImage(string imageUrl, string filename, ImageFormat format)
		{
			try{Uri url = new Uri(imageUrl);} catch { return null; }
			WebClient client = new WebClient();
			Stream stream; stream = client.OpenRead(imageUrl);
			Bitmap bitmap; bitmap = new Bitmap(stream);

			stream.Flush();
			stream.Close();
			client.Dispose();
			if (bitmap != null)
			{
				return bitmap;
			}
			return null;
		}
		//TODO: make a movie/hall from the table and etc jadajadajada
	}
}
