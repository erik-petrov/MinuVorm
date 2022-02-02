using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace MinuVorm
{
	public partial class MyForm : Form
	{
		readonly TableLayoutPanel tlp = new TableLayoutPanel();
		readonly Button btn;
		MailMessage mailMessage;
		SmtpClient smtpClient;
		readonly int w, h;
		readonly int _x, _y, movieID, hallID;
		readonly string _movieName, _fileName, _hallSize;
		string userMail;
		readonly Image seatTaken = Image.FromFile("../../images/seatTaken.png");
		readonly Image seatAvailable = Image.FromFile("../../images/seatavailable.png");
		readonly Image seatOrange = Image.FromFile("../../images/seatOrang.png");
		readonly int[][] buttonArr;
		readonly List<string> aboutToBuy, bought;

		string connStr = @"datasource=127.0.0.1;port=3306;username=root;password=;database=cinema;";
		MySqlConnection dbConn;
		MySqlCommand cmd;
		MySqlDataReader reader;
		/*SqlConnection connect;
		SqlCommand order;
		SqlDataAdapter adap;*/
		public MyForm(int x, int y, string movieName, string hallSize, string fileName = "")
		{
			this.Name = "Choose the seats!";
			_movieName = movieName;
			_hallSize = hallSize;
			dbConn = new MySqlConnection(connStr);
			dbConn.Open();
			movieID = FindID("films", _movieName);
			hallID = FindID("halls", _hallSize);
			if (movieID == 0 || hallID == 0) { MessageBox.Show("Movie or Hall doesent exist in the DB."); this.Close(); }
			_x = x;
			_y = y;
			_fileName = fileName;
			seatOrange.Tag = "orang";
			seatTaken.Tag = "taken";
			seatAvailable.Tag = "green";
			bought = LoadBought();
			/*if (_fileName == "")
			{
				_fileName = MakeFileName();
			}
			Console.WriteLine(_fileName);
			using (StreamWriter w = new StreamWriter("../../tickets/" + _fileName, true)) { w.Write(""); }
			using (StreamReader r = new StreamReader("../../tickets/"+ _fileName))
			{
				string[] tickets = r.ReadToEnd().Split(',');
				bought = new List<string>();
				string temp = "";
				foreach (var item in tickets)
				{
					temp = item.Replace("\r\n", string.Empty);
					bought.Add(temp);
				}
				Console.WriteLine(bought.Contains("1;1"));
			}*/
				aboutToBuy = new List<string>();
				buttonArr = new int[x][];
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
					buttonArr[i] = new int[y];
					for (int j = 0; j < y; j++)
					{
						btn = new Button
						{
							Text = $"{i + 1};{j + 1}",
							Name = $"{i + 1};{j + 1}",
							Font = new Font("Arial", 10),
							Dock = DockStyle.Fill,
							TextAlign = ContentAlignment.MiddleCenter,
							BackgroundImage = seatAvailable,
							BackgroundImageLayout = ImageLayout.Stretch,
							FlatStyle = FlatStyle.Flat
						};
						btn.FlatAppearance.BorderColor = SystemColors.Control;
						btn.FlatAppearance.BorderSize = 0;
						btn.Click += TryBuy;
						if (bought.Contains(btn.Name))
						{
							btn.BackgroundImage = seatTaken;
						} else buttonArr[i][j] = 0;
						tlp.Controls.Add(btn, i, j);
					}
				}
				w = (100 / x);
				tlp.Dock = DockStyle.Fill;
				h = (100 / y);
				tlp.Size = new Size(tlp.ColumnCount * w * 2, tlp.RowCount * h * 2);
				this.Controls.Add(tlp);
				MainMenu mainMenu = new MainMenu();
				MenuItem File = mainMenu.MenuItems.Add("&File");
				File.MenuItems.Add(new MenuItem("&Buy", new EventHandler(this.Buy), Shortcut.CtrlS));
				this.Menu = mainMenu;
			}
		private List<string> LoadBought()
		{
			List<string> list = new List<string>();
			cmd = new MySqlCommand($"select x, y, filmID from `tickets`", dbConn);
			reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				list.Add(reader["x"] + ";" + reader["y"]);
			}
			reader.Close();
			return list;
		}
		private void GetMail()
		{
			userMail = Interaction.InputBox("Enter your email.", "Enter your email so we can send you the tickets you've bought.");
		}
		private void Buy(object sender, EventArgs e)
		{
			GetMail();
			string emailTickets = "";
			aboutToBuy.ForEach(x => { emailTickets += x + ", "; AddToDB(x); });
			emailTickets = emailTickets.Remove(emailTickets.Length - 2) + ".";
			if (SendMail(emailTickets))
			{
				MyForm cin = new MyForm(_x, _y, _movieName, _hallSize, _fileName);
				cin.Size = this.Size;
				cin.Show();
				dbConn.Close();
				this.Close();
			}
			
		}
		private void TryBuy(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			if ((string)btn.BackgroundImage.Tag == "green") { btn.BackgroundImage = seatOrange; aboutToBuy.Add(btn.Name); }
			else if ((string)btn.BackgroundImage.Tag == "orang") { btn.BackgroundImage = seatAvailable; aboutToBuy.Remove(btn.Name); }
			//string[] btnCoords = btn.Name.Split(',');
		}
		private bool SendMail(string mail)
		{
			try
			{
				smtpClient = new SmtpClient("smtp.gmail.com")
				{
					Port = 587,
					Credentials = new NetworkCredential("programmeeriminetthk2@gmail.com", "2.kuursus tarpv20"),
					EnableSsl = true,
				};
				mailMessage = new MailMessage
				{
					From = new MailAddress("Cinema.Amogus@service.com"),
					Subject = "Piletid",
					Body = $"<h1>Hello. I'm an automated cinema 'Amogus' service!</h1>\n Tickets to the movie: <strong>{_movieName}</strong><br>" +
					$"Hall: <strong>{_hallSize}</strong><br>These are the tickets you've bought: \n<strong>{mail}</strong>",
					IsBodyHtml = true,
				};

				mailMessage.To.Add(new MailAddress(userMail));
				smtpClient.Send(mailMessage);
				return true;
			}
			catch
			{
				MessageBox.Show("Invalid email.\nSent to another due to debugging.");
				mailMessage.To.Add(new MailAddress("programmeeriminetthk@gmail.com"));
				smtpClient.Send(mailMessage);
				//change to false
				return true;
			}
		}
		private void AddToDB(string coords)
        {
			cmd = new MySqlCommand($"insert into tickets(x, y, filmID, hallID) values(@x, @y, @movie, @hall)", dbConn);
			cmd.Parameters.AddWithValue("@x", coords[0]);
			cmd.Parameters.AddWithValue("@y", coords[2]);
			cmd.Parameters.AddWithValue("@movie", movieID);
			cmd.Parameters.AddWithValue("@hall", hallID);
			cmd.ExecuteNonQuery();
		}
		private int FindID(string from, string name)
		{
			cmd = new MySqlCommand($"select id from `{from}` where name = '{name}'", dbConn);
			object result = cmd.ExecuteScalar();
			if (result != null)
			{
				Console.WriteLine(Convert.ToInt32(result));
				return Convert.ToInt32(result);
			}
			return 0;
		}
	}
}


/*
 create table Halls(
    id int PRIMARY KEY AUTO_INCREMENT,
    name varchar(15) not null,
	x int not null,
	y int not null);

create table Films(
    id int PRIMARY KEY AUTO_INCREMENT,
    name varchar(70) not null unique,
	pilt text not null);
    
create table tickets(
    id int PRIMARY KEY AUTO_INCREMENT,
    x int not null,
    y int not null,
    filmID int not null,
    hallID int not null,
	FOREIGN key(filmID) REFERENCES Films(id),
    FOREIGN key(hallID) REFERENCES Halls(id));
*/