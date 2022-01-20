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
    public partial class MoviePick : Form
    {
        Image movie1, movie2, movie3;
        PictureBox pb1, pb2, pb3;
        public MoviePick()
        {
            this.Size = new Size(900, 400);
            movie1 = Image.FromFile("../../images/nowayhome.jpg");
            movie2 = Image.FromFile("../../images/farfromhome.jpg");
            movie3 = Image.FromFile("../../images/universe.jpg");
            PictureBox pb1 = new PictureBox
            {
                Name = "Spiderman: No way home",
                Image = movie1,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 300,
                Height = 361,
                Location = new Point(0, 0)
            };
            PictureBox pb2 = new PictureBox
            {
                Name = "Spiderman: Far from home",
                Image = movie2,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 300,
                Height = 361,
                Location = new Point(300, 0)
            };
            PictureBox pb3 = new PictureBox
            {
                Name = "Spiderman: Into the Spider-Verse",
                Image = movie3,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 300,
                Height = 361,
                Location = new Point(600, 0),
            };
            pb1.Click += Click;
            pb2.Click += Click;
            pb3.Click += Click;
            this.Controls.Add(pb1);
            this.Controls.Add(pb2);
            this.Controls.Add(pb3);
        }

        private void Click(object sender, EventArgs e)
        {
            PictureBox pv = sender as PictureBox;
            CinemaPick frm = new CinemaPick("Choose the hall", "", new string[4] { "Small", "Medium", "Big", "Huge" }, pv.Name);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }
    }
}
