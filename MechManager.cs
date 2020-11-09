using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace RhodesEquipment
{
	public class MechManager : System.Windows.Forms.Form
	{
		protected TextBox IdBox;
		protected TextBox NameBox;
		protected CheckBox cb;
		static string _connectionString;
		Form m ;

		public MechManager(string connectionString)
		{
			_connectionString = connectionString;

			Text = "Manage Mechanics";
			Font f = new Font("Verdana", 15);
			string name;
			bool active;
			int y = 40;
			Size s = new Size(600, 13);
			//_conx = conx;
			ClientSize = new Size(400, 800);
			Panel p = new Panel();
			p.AutoScroll = true;
			p.ClientSize = new Size(400,800);
			this.Controls.Add(p);
			Label NameLabel = new Label();
			//NameLabel.Font = f;
			NameLabel.Text = "Name";
			NameLabel.Size = new Size(35, 13);
			NameLabel.Location = new Point(0,5);
			p.Controls.Add(NameLabel);
			string query = "SELECT name, active FROM mechanics ORDER BY !active, name";
			MySqlConnection _conx = new MySqlConnection(_connectionString);
			_conx.Open();
			if(_conx == null) MessageBox.Show("No Connection");
			MySqlDataReader rdr = null;
			MySqlCommand sql = new MySqlCommand(query, _conx);
			rdr = sql.ExecuteReader();
			while(rdr.Read())
			{
				name = ((string)rdr["name"]).ToString();
				active = ((bool)rdr["active"]);
				Label l = new Label();
			//	l.Size = s;
				l.Text = name;
				l.Font = f;
				if(active)
				{
					l.ForeColor = Color.Green;
				} else
				{
					l.ForeColor = Color.Red;
				}
				l.Click += new EventHandler(NameClicked);
				l.Location = new Point(0, y);
				p.Controls.Add(l);
				y += 20;
			}///end while
				Label ng = new Label(); /// Add A New Guy
				ng.Text = "New Mechanic";
				ng.Font = f;
				ng.ForeColor = Color.Blue;
				ng.Click += new EventHandler(NameClicked);
				ng.Location = new Point(0, y);
				p.Controls.Add(ng);
			rdr.Close();
			_conx.Close();
			rdr.Dispose();
		}///end constructor
///////////////////////////////////////////////////////////////////////////		
		private void NameClicked(object sender, EventArgs e)
		{
			Label label = (Label)sender;
			m = new Form();

			Font font = new Font("Verdana", 10);
			m.ClientSize = new Size(400, 200);
			m.Text = "Manage Mechanic";
			Label IdLabel = new Label();
		    IdLabel.Text = "ID";
			IdLabel.Size = new Size(30, 13);
			IdLabel.Font = font;
			IdLabel.Location = new Point(10,10);
			m.Controls.Add(IdLabel);
			IdBox = new TextBox();
			IdBox.Size = new Size(50, 15);
			IdBox.Location = new Point(45, 10);
			IdBox.ReadOnly = true;
			m.Controls.Add(IdBox);
			Label NameLabel = new Label();
			NameLabel.Text = "Name";
			NameLabel.Size = new Size(60, 13);
			NameLabel.Font = font;
			NameLabel.Location = new Point(120, 10);
			m.Controls.Add(NameLabel);
			NameBox = new TextBox();
			NameBox.Font = font;
			NameBox.Size = new Size(80, 15);
			NameBox.Location = new Point(190, 10);
			NameBox.Text = label.Text;
			m.Controls.Add(NameBox);
			Label ActiveLabel = new Label();
			ActiveLabel.Size = new Size(40, 13);
			ActiveLabel.Location = new Point(290, 10);
			ActiveLabel.Font = font;
			ActiveLabel.Text = "Active";
			m.Controls.Add(ActiveLabel);
			cb = new CheckBox();
			cb.Location = new Point(350, 10);
			m.Controls.Add(cb);
			Button b = new Button();
			b.Text = "Update";
			b.Location = new Point(30, 80);
			b.Click += new EventHandler(ButtonClicked);
			m.Controls.Add(b);
			string query = "SELECT id, name, active FROM mechanics WHERE name =\'" +label.Text + "\'";
		   	MySqlDataReader rdr = null;
			MySqlConnection _conx = new MySqlConnection(_connectionString);
			_conx.Open();
			if(_conx == null) MessageBox.Show("No Connection");
	   		MySqlCommand sql = new MySqlCommand(query, _conx);	   
			rdr = sql.ExecuteReader();
			while(rdr.Read())
			{
				IdBox.Text = ((int)rdr["id"]).ToString();
				NameBox.Text = ((string)rdr["name"]).ToString();
				cb.Checked = ((bool)rdr["active"]);
			}
			m.Show();
			rdr.Close();
			_conx.Close();


			//MessageBox.Show("Name: " + label.Text);
		}
/////////////////////////////////////////////////////////////////////////	
			private void ButtonClicked(object sender, EventArgs e)
			{
				string query = "Empty";
				string state = "0";
				if(cb.Checked == true) state = "1";
				if(IdBox.Text == "")
				{
					query = "INSERT INTO mechanics VALUES(\'\',\'" +NameBox.Text+"\',\'"+state+"\')"; 
				}else
				{
					query = "UPDATE mechanics SET `name` = \'" +NameBox.Text+"\', `active` = \'"+state+" \' WHERE `id` = \'" +IdBox.Text+"\'";
				}
				MySqlConnection _conx = new MySqlConnection(_connectionString);
				_conx.Open();
				if(_conx == null) MessageBox.Show("No Connection");
				MySqlCommand cmd = new MySqlCommand(query, _conx);
				try
				{
				cmd.ExecuteNonQuery();
				} catch (MySqlException ex)
				{
					MessageBox.Show("SQL Exception: " + ex.ToString());
							}

				
				 m.Close();
				 this.Close();
			}
////////////////////////////////////////////////////////////////////			
	}////end class





}/////End Namespace
