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
    public partial class MoviePick : Form
    {
        PictureBox pb;
        MySqlConnection dbConn;
        MySqlCommand cmd;
        MySqlDataAdapter adap;
        DataGridView dgv;
        string connStr = @"datasource=127.0.0.1;port=3306;username=root;password=;database=cinema;";
        public MoviePick()
        {
            dbConn = new MySqlConnection(connStr);
            dbConn.Open();
            this.FormClosing += (s, e) => dbConn.Close();
            this.Size = new Size(900, 400);
            this.Name = "Choose a movie!";
            this.BackgroundImage = Image.FromFile("../../images/cinemaBG.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            Label lbl = new Label
            {
                Text = "Choose the movie: ",
                Location = new Point(400, 20),
                AutoSize = true,
                Font = new Font("Arial", 16),
                BackColor = Color.Transparent,
                ForeColor = Color.White
            };
            adap = new MySqlDataAdapter();
            DataTable films = new DataTable();
            dgv = new DataGridView();
            dgv.Location = new Point(400, 50);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AutoResizeColumns();
            dgv.Width = 370;
            string command = "select name, pilt from films";
            adap.SelectCommand = new MySqlCommand(command, dbConn);
            adap.Fill(films);
            BindingSource fSource = new BindingSource();
            fSource.DataSource = films;
            dgv.DataSource = fSource;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgv.DataBindingComplete += Dgv_DataBindingComplete;
            dgv.SelectionChanged += Dgv_SelectionChanged;

            pb = new PictureBox
            {
                Name = "",
                Image = null,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 300,
                Height = 361,
                Location = new Point(40, 0)
            };
            pb.BackColor = Color.Transparent;
            pb.Click += Choose;
            this.Controls.Add(lbl);
            this.Controls.Add(pb);
            this.Controls.Add(dgv);
        }

		private void Dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
            dgv.Columns["pilt"].Visible = false;
            dgv.Columns["name"].Name = "Choose a movie";
            var height = 20;
            DataGridView dvg = sender as DataGridView;
            foreach (DataGridViewRow dr in dvg.Rows)
            {
                height += dr.Height;
            }

            dvg.Height = height;
        }

		private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            Int32 selectedRowCount = dgv.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                pb.Image = SaveImage(dgv.SelectedRows[0].Cells[1].Value.ToString());
                pb.Name = dgv.SelectedRows[0].Cells[0].Value.ToString();
            }
        }
        private void Choose(object sender, EventArgs e)
        {
            PictureBox pv = sender as PictureBox;
            CinemaPick frm = new CinemaPick("Choose the hall", "", new string[4] { "Small", "Medium", "Big", "Huge" }, pv.Name);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }
        public Bitmap SaveImage(string imageUrl)
        {
            try { Uri url = new Uri(imageUrl); } catch { return null; }
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
    }
}
