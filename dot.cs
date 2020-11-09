
using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;

public class DotWindow : System.Windows.Forms.Form
	{
		protected DateTimePicker date_box;
		protected ComboBox equip_box;
		protected ComboBox mech_box;
		protected Button submit_button;
		string _connectionString;

		public DotWindow(string connectionString)
		{
			_connectionString = connectionString;
			
			Text = "Update DOT Database";
			ClientSize = new System.Drawing.Size(500, 300);
			Font f = new Font("Verdana", 10);
			Label e_label = new Label();
			e_label.Text = "Equip #";
			e_label.Font = f;
			e_label.Size = new Size(60, 15);
			e_label.Location = new Point(10, 10);
			this.Controls.Add(e_label);
			equip_box = new ComboBox()
			{
				DropDownStyle = ComboBoxStyle.DropDownList,
				Location = new Point(80, 10),
				Size = new Size(150, 20)
			};
			Label m_label = new Label()
			{
				Text = "Inspector",
				Location = new Point(10, 40),
				Size = new Size(60,15),
				Font = f
			};
			this.Controls.Add(m_label);

			mech_box = new ComboBox()
			{
				DropDownStyle = ComboBoxStyle.DropDownList,
				Location = new Point(80, 40),
				Size = new Size(150, 20)
			};
		//	if (conx == null)
		//	{
		//		getConnection();
		//	}
			MySqlConnection _conx = new MySqlConnection(_connectionString);
			_conx.Open();
			if(_conx == null) MessageBox.Show("No Connection");
			MySqlCommand sql = new MySqlCommand("SELECT num FROM equip ORDER BY num", _conx);
			String s = "";
			MySqlDataReader rdr = null;
			rdr = sql.ExecuteReader();
			while(rdr.Read())
			{
				s = (string)rdr["num"];
				equip_box.Items.Add(s);
			}
			rdr.Close();
			equip_box.SelectedItem = 1;
			this.Controls.Add(equip_box);

		
			sql = new MySqlCommand("SELECT name FROM mechanics WHERE active = '1' && dot = '1' ORDER BY name", _conx);
			rdr = sql.ExecuteReader();
			while(rdr.Read())
			{
				s = (string)rdr["name"];
				mech_box.Items.Add(s);
			}
			rdr.Close();
			_conx.Close();
			mech_box.SelectedItem = 1;
			this.Controls.Add(mech_box);

			
			
			Label d_label = new Label()
			{
				Text = "Date: ",
				Location = new Point(10, 80),
				Size = new Size(60,15),
				Font = f
			};
			this.Controls.Add(d_label);

			date_box = new DateTimePicker()
			{
				Location = new Point(80, 80),
				Format = DateTimePickerFormat.Custom,
				CustomFormat = "yyyy-MM-dd"
			};
			this.Controls.Add(date_box);
			
			
			submit_button = new Button()
			{
				Text = "Update",
				Location = new Point (400,250),
			};
			submit_button.Click += new EventHandler(submit_button_clicked);
			this.Controls.Add(submit_button);

		}
	///////////////////////////////////////////////////////////
/*		public static void Main(string[] args)
		{
			Application.Run(new DotWindow());
		}
*/
///////////////////////////////////////////////////////////////////////		
		void submit_button_clicked(object sender, System.EventArgs e)
		{
			int num = 0;
			int mech = 0;
			
			MySqlConnection _conx = new MySqlConnection(_connectionString);
			_conx.Open();
			if(_conx == null) MessageBox.Show("No Connection");
			//_conx = conx;
			MySqlCommand sql1 = new MySqlCommand("SELECT id from equip where num = '" + equip_box.Text + "'", _conx);
			MySqlCommand sql2 =new MySqlCommand("SELECT id FROM mechanics WHERE name = '" + mech_box.Text + "'", _conx);
			MySqlDataReader rdr = null;
			rdr = sql1.ExecuteReader();
			while(rdr.Read())
			{
				num = (int)rdr["id"];
			}
			rdr.Close();
			rdr = sql2.ExecuteReader();
			while(rdr.Read())
			{
				mech = (int)rdr["id"];
			}
			rdr.Close();
			try {

			MySqlCommand sql = new MySqlCommand();
			sql.Connection = _conx;
			if(_conx == null) MessageBox.Show("No Connection");
			sql.CommandText = "INSERT INTO dot (num, mech, date) VALUES (@num, @mech, @date) ON DUPLICATE KEY UPDATE mech = VALUES (mech), date = VALUES(date)"; 
			sql.Prepare();
			sql.Parameters.AddWithValue("@date", date_box.Text);
			sql.Parameters.AddWithValue("@num", num);
			sql.Parameters.AddWithValue("@mech", mech);
			//MessageBox.Show(sql.CommandText);
			int rows_affected = sql.ExecuteNonQuery();
			MessageBox.Show("DOT database updated..." + rows_affected + "rows affected");
			_conx.Close();
			}
			catch (MySqlException ex)
			{

				Console.WriteLine("Error: {0}", ex.ToString());
			}
		//}//	
			mech_box.SelectedItem = 0;
			equip_box.SelectedItem = 0;
			date_box.Value = DateTime.Now;


		}

//////////////////////////////////////////////////////////////////////

/*		static void getConnection()
		{

		string connectionString =
			"Server=localhost;" +
			"Database=equip;" +
			"User ID=shop;" +
			"Password=shop;" +
			"Pooling=false";
		conx = new MySqlConnection(connectionString);
		conx.Open();
		if(conx == null)
		Console.WriteLine("Database Connection Failed...");
		}*/
///////////////////////////////////////////////////////////////////////////////
	}
