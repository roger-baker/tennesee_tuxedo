/* Rhodes Crane Equipment Management System  */
using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;

namespace RhodesEquipment
{
	class MainWindow : System.Windows.Forms.Form
	{

		public static string connectionString =
			"Server=localhost;" +
			"Database=equip;" +
			"User ID=shop;" +
			"Password=shop;" +
			"Pooling=false";
		public string yr, mk, md, vin, lic, ticket;
		public int id;
		FilterWindow fw;
		AddEquip ae;
		AddFilter af;
		MechManager mm;
		FilterListManager flm;
		DotList dl;
		DotWindow dw;

		protected TextBox ticket_box;
		protected TextBox parts_box;
		protected TextBox date_box;
		protected ComboBox mech_cb;
		protected ComboBox eq_num_cb;
		protected TextBox miles_box;
		protected TextBox hours_box;
		protected TextBox repairs_box;
		protected RadioButton[] radioButtons;
		protected Button ps_button;
		protected Form dlg1;
		protected DateTimePicker dp1;
		protected DateTimePicker dp2;
		protected Form results1;

		public MainWindow()
		{
			Text = "Rhodes Crane Equipment Records";
			ClientSize =  new System.Drawing.Size (500,700);
			Font f = new Font("Verdana" , 10);

////////////////	Menu Strip Starts Here	////////////////////////////

			MenuStrip ms = new MenuStrip();
			ms.Font = f;
			ms.Parent = this;
			ToolStripMenuItem e = new ToolStripMenuItem("&Equipment");
			ToolStripMenuItem ae = new ToolStripMenuItem("&Add Equipment", null, new EventHandler(AddEquipEventHandler));
			ToolStripMenuItem lm = new ToolStripMenuItem("&Add Filter List",null, new EventHandler(AddFilterListHandler));
			ToolStripMenuItem addfil = new ToolStripMenuItem("&Add Filters", null, new EventHandler(AddFilterEventHandler));

			ae.ShortcutKeys = Keys.Control | Keys.A;
			addfil.ShortcutKeys = Keys.Control | Keys.F;
			lm.ShortcutKeys = Keys.Control | Keys.L;
			e.DropDownItems.Add(ae);
			e.DropDownItems.Add(addfil);
			e.DropDownItems.Add(lm);
			ms.Items.Add(e);
			ToolStripMenuItem m = new ToolStripMenuItem("&Mechanics", null, new EventHandler(MechEventHandler));
			m.ShortcutKeys = Keys.Control | Keys.M;
			ms.Items.Add(m);
			
			ToolStripMenuItem dot_menu = new ToolStripMenuItem("&DOT");
			ToolStripMenuItem DotUpdate = new ToolStripMenuItem("&Update DOT", null, new EventHandler(UpdateDotHandler));
			dot_menu.DropDownItems.Add(DotUpdate);
			dot_menu.ShortcutKeys = Keys.Control | Keys.D;
			ms.Items.Add(dot_menu);
			ToolStripMenuItem ListDot = new ToolStripMenuItem("DOT List", null, new EventHandler(ListDotHandler));
			dot_menu.DropDownItems.Add(ListDot);
			
			ToolStripMenuItem exit = new ToolStripMenuItem("&Exit", null, new EventHandler(exit_button_Click));
			exit.ShortcutKeys = Keys.Control | Keys.Q;
			ms.Items.Add(exit);

///////////////////// End Menu Strip	////////////////////////////////////
		
			Label ticket_label = new Label();
			ticket_label.Text = "Ticket #";
			ticket_label.Font = f;
			ticket_label.Size = new Size(60,13);
			ticket_label.Location = new Point(10,40);
			this.Controls.Add(ticket_label);
			ticket_box = new TextBox();
			ticket_box.Size = new Size(60,15);
			ticket_box.Location = new Point(80,40);
			ticket_box.ReadOnly = true;
			ticket_box.TabIndex = 0;
			this.Controls.Add(ticket_box);

			Label date_label;
			date_label = new Label();
			date_label.Text = "Date";
			date_label.Size = new Size(35,13);
			date_label.Font = f;
			date_label.Location = new Point(160,40);
			this.Controls.Add(date_label);
			
			Label mech_label;
			mech_label = new Label();
			mech_label.Text = "Mechanic";
			mech_label.Font = f;
			mech_label.Size = new Size(35,13);
			mech_label.Location = new Point(290,40);
			this.Controls.Add(mech_label);

			mech_cb = new ComboBox()
			{
			DropDownStyle = ComboBoxStyle.DropDownList,
			Location = new Point(330,40),
			Size = new Size(150,20)
			};
				mech_cb.TabIndex = 2;

			Label eq_num_label;
			eq_num_label = new Label();
			eq_num_label.Text = "Equipment #";
			eq_num_label.Font = f;
			eq_num_label.Location = new Point(10,80);
			this.Controls.Add(eq_num_label);

			date_box = new TextBox();
			date_box.Location = new Point(200,40);
			date_box.Size = new Size(60,15);
			string dt = DateTime.Now.ToString("MM/dd/yy");
			date_box.Text = dt;
			date_box.TabIndex  = 1;
			this.Controls.Add(date_box);

			eq_num_cb = new ComboBox()
			{
			DropDownStyle = ComboBoxStyle.DropDownList,
			Location = new Point(120,80),
			Size = new Size(50,120),
			DropDownWidth = 70
			};
			eq_num_cb.SelectedIndexChanged += new System.EventHandler(eq_num_cb_handler);
			
			eq_num_cb.TabIndex = 3;


			Label miles_label = new Label();
			miles_label.Text = "Miles";
			miles_label.Font = f;
			miles_label.Size = new Size(55,13);
			miles_label.Location = new Point(10,120);
			this.Controls.Add(miles_label);
			
			miles_box = new TextBox();
			miles_box.Location = new Point(70,120);
			miles_box.Size = new Size(60,15);
			miles_box.TabIndex = 5;
			this.Controls.Add(miles_box);
			
			Label hours_label = new Label();
			hours_label.Text = "Hours";
			hours_label.Font = f;
			hours_label.Size = new Size(55,13);
			hours_label.Location = new Point(200,120);
			this.Controls.Add(hours_label);
			
			hours_box = new TextBox();
			hours_box.Location = new Point(260,120);
			hours_box.Size = new Size(60,15);
			hours_box.TabIndex = 6;
			this.Controls.Add(hours_box);



			Label parts_label;
			parts_label = new Label();
			parts_label.Text = "Parts Used";
			parts_label.Font = f;
			parts_label.Size = new Size(55,13);
			parts_label.Location = new Point(10,190);
			this.Controls.Add(parts_label);
			
			parts_box = new TextBox();
			parts_box.Location = new Point(70,190);
			parts_box.Size = new Size(360,150);
			parts_box.Multiline=true;
			parts_box.WordWrap = true;
			parts_box.MaxLength = 300;
			parts_box.TabIndex = 7;
			this.Controls.Add(parts_box);

			Label repairs_label = new Label();
			repairs_label.Text = "Repairs Performed";
			repairs_label.Font = f;
			repairs_label.Size = new Size(55,13);
			repairs_label.Location = new Point(10,390);
			this.Controls.Add(repairs_label);

			repairs_box = new TextBox();
			repairs_box.Location = new Point(70,390);
			repairs_box.Size = new Size(360,150);
			repairs_box.Multiline = true;
			repairs_box.WordWrap = true;
			repairs_box.MaxLength = 500;
			repairs_box.TabIndex = 8;
			this.Controls.Add(repairs_box);
			

			
			radioButtons = new System.Windows.Forms.RadioButton[2];
			radioButtons[0] = new RadioButton();
			radioButtons[0].Text = "Preventive Maintenance";
			radioButtons[0].Checked = false;
			radioButtons[0].Location = new System.Drawing.Point(200,80);
			radioButtons[0].CheckedChanged += new System.EventHandler(PM_checked_handler);
			radioButtons[1] = new RadioButton();
			radioButtons[1].Text = "Repair";
			radioButtons[1].Checked = true;
			radioButtons[1].Location = new System.Drawing.Point(310,80);
			radioButtons[0].TabIndex = 4;

			this.Controls.Add(mech_cb);	
			this.Controls.AddRange(new Control[] {eq_num_label, eq_num_cb});
			this.Controls.Add(radioButtons[0]);
			this.Controls.Add(radioButtons[1]);

			ps_button = new Button();
			ps_button.Text = "Print / Save";
			ps_button.Location = new System.Drawing.Point(50,630);
			ps_button.TabIndex = 9;
			ps_button.Click += new EventHandler(ps_button_Click);
			this.Controls.Add(ps_button);

			Button search_button = new Button();
			search_button.Text = "Search";
			search_button.Location = new System.Drawing.Point(200,630);
			search_button.TabIndex = 10;
			search_button.Click += new EventHandler(search_button_Click);
			this.Controls.Add(search_button);
			
			Button exit_button = new Button();
			exit_button.Text = "Quit";
			exit_button.Location = new System.Drawing.Point(350,630);
			exit_button.TabIndex = 11;
			exit_button.Click += new EventHandler(exit_button_Click);
			this.Controls.Add(exit_button);
			populate_me();
		}
/////////////////////////////////////////////////////////////////////////////
		public static void Main(string[] args)
		{ 
			Application.Run(new MainWindow());
		}

//////////////////////////////////////////////////////////////////////////////
///	Sets window text to equipment description	/////////////////////
/////////////////////////////////////////////////////////////////////////////

		void eq_num_cb_handler(object sender, System.EventArgs e)
		{
			MySqlDataReader reader = null;
			ComboBox combobox=(ComboBox)sender;
			string e_num = combobox.SelectedItem.ToString();
			
			MySqlConnection _conx = new MySqlConnection(connectionString);
			_conx.Open();
			if(_conx == null) MessageBox.Show("No Connection");
			MySqlCommand sql = new MySqlCommand("SELECT id, year, make, model, vin, lic FROM equip WHERE num = \'"+ e_num +"\'", _conx);
			reader = sql.ExecuteReader();
			while(reader.Read())
			{
				id = ((int)reader["id"]);
				yr = ((int)reader["year"]).ToString();
				mk = (string)reader["make"];
				md = (string)reader["model"];
				vin = (string)reader["vin"];
				lic = (string)reader["lic"];
			}
			reader.Close();		
			_conx.Close();

			e_num = e_num + " " + yr + " " + mk + " " + md;
			this.Text = e_num;
		}

///////////////////////////////////////////////////////////////////////////////	
		void AddEquipEventHandler(Object sender, System.EventArgs e)
		{
			ae = new AddEquip(connectionString);
			ae.Show();
		}
///////////////////////////////////////////////////////////////////////////////
		void MechEventHandler(Object sender, System.EventArgs e)
		{
			mm = new MechManager(connectionString);
			mm.Show();
		}
///////////////////////////////////////////////////////////////////////////		
		void AddFilterEventHandler(Object sender, System.EventArgs e)
		{
			af = new AddFilter(connectionString);
			af.Show();
		}
///////////////////////////////////////////////////////////////////////////
		void AddFilterListHandler(Object sender, System.EventArgs e)
		{
			flm = new FilterListManager(connectionString, id);
			flm.Show();
		}
//////////////////////////////////////////////////////////////////////////////
///	Opens FilterWindow, allows user to pick filters		/////////////
//////////////////////////////////////////////////////////////////////////////

		void PM_checked_handler(Object sender, System.EventArgs e)
		{

			if(eq_num_cb.SelectedItem == null)
			{ 
				MessageBox.Show("No Equipment Selected!");
				radioButtons[0].Checked = false;
				return;
			} else {
		        string num = eq_num_cb.SelectedItem.ToString(); 
			fw = new FilterWindow(num, connectionString);
			fw.FormClosed += new FormClosedEventHandler(FilterWindowClosed);
			fw.Show();
			}
		}

//////////////////////////////////////////////////////////////////////		
///	Fills parts and repairs boxes 	//////////////////////////////
///	according to selected filters	//////////////////////////////
/////////////////////////////////////////////////////////////////////
		
		void FilterWindowClosed(Object sender, FormClosedEventArgs e)
		{
			FilterWindow fw = (FilterWindow)sender;
			this.parts_box.Text=(fw.GetText());
			this.repairs_box.Text = fw.blurb;
			
		}
	
//////////////////////////////////////////////////////////////////////////////	
///	Populates equip and mech drop-downs	/////////////////////////////
/////////////////////////////////////////////////////////////////////////////

		void populate_me()
		{
			string s;

			MySqlConnection _conx = new MySqlConnection(connectionString);
			_conx.Open();
			if(_conx == null) MessageBox.Show("No Connection");
			IDbCommand dbc = _conx.CreateCommand();
			string sql = "SELECT num FROM equip ORDER BY num";
			eq_num_cb.Items.Add("");
			dbc.CommandText = sql;
			IDataReader reader = dbc.ExecuteReader();
			while(reader.Read())
			{
				s = (string)reader["num"];
				eq_num_cb.Items.Add(s);
			}
			reader.Close();		
			dbc = _conx.CreateCommand();
			sql = "SELECT name FROM mechanics WHERE active = true ORDER BY name";
			dbc.CommandText = sql;
			reader = dbc.ExecuteReader();
			mech_cb.Items.Add("");
			while(reader.Read())
			{
				s = (string)reader["name"];
				mech_cb.Items.Add(s);
			}
			reader.Close();
			dbc = _conx.CreateCommand();
			sql = "SELECT ticket FROM repair WHERE ticket = (SELECT MAX(ticket) FROM repair)";
			dbc.CommandText = sql;
			reader = dbc.ExecuteReader();
			while(reader.Read())
			{
				int i = (int)reader["ticket"];
				i++;
				ticket_box.Text = i.ToString().PadLeft(7,'0');;
			}
			reader.Close();
			_conx.Close();

			_conx = null;
			this.Text = "Rhodes Crane & Rigging Equipment Records";
			
			
		}
////////////////////////////////////////////////////////////////////////////
///	Opens dialog box allowing to pick a range of dates	///////////
///////////////////////////////////////////////////////////////////////////
		private void search_button_Click(object sender, EventArgs e)
		{
			ps_button.Text = "Print/Update";
			string d = date_box.Text;
			dlg1 = new Form();
			dlg1.ClientSize = new Size(400,200);
			dlg1.StartPosition = FormStartPosition.Manual;
			dlg1.Location = new Point(300,300);
			Label d1_label = new Label();
			Label d2_label = new Label();
			d2_label.Text = "Ending Date:";
			d1_label.Text = "Starting Date:";
			d1_label.Location = new Point(10,10);
			d2_label.Location = new Point(10,40);
			dlg1.Controls.Add(d1_label);
			dlg1.Controls.Add(d2_label);
			dp1 = new DateTimePicker();
			dp1.Value = new DateTime(2014,01,01);
			dp2 = new DateTimePicker();
			dp1.Location = new Point(110,10);
			dp2.Location = new Point(110,40);
			dp1.Format = DateTimePickerFormat.Custom;
			dp1.CustomFormat = "yyyy-MM-dd";
			dp2.Format = DateTimePickerFormat.Custom;
			dp2.CustomFormat = "yyyy-MM-dd";
			dlg1.Controls.Add(dp1);
			dlg1.Controls.Add(dp2);
			Button ok_button = new Button();
			ok_button.Text = "Okay";
			ok_button.Location = new Point(300,80);
			ok_button.Click += new EventHandler(ok_button_Click);
			dlg1.Controls.Add(ok_button);
			dlg1.Show();
		}		
/////////////////////////////////////////////////////////////////////////////
///	Update DOT inspection date for a piece of equipment	////////////
////////////////////////////////////////////////////////////////////////////

		private void UpdateDotHandler(object sender, EventArgs e)
	{
		dw = new DotWindow(connectionString);
		dw.Show();
	}

/////////////////////////////////////////////////////////////////////////
///	Opens window with list of expired DOT inspections	/////////
/////////////////////////////////////////////////////////////////////////

		private void ListDotHandler(object sender, EventArgs e)
		{
		dl = new DotList(1, connectionString);

		dl.Show();
		}
		
		
/////////////////////////////////////////////////////////////////////////////	
///	Opens window with clickable list of jobs	////////////////////
///	that match search criteria	////////////////////////////////////
///////////////////////////////////////////////////////////////////////////

		private void ok_button_Click(object sender, EventArgs e)
		{
			string query = "SELECT a.ticket,a.date,a.hrs,a.mi,a.mech,a.parts_used,a.description,b.num FROM repair a join equip b on a.equipment_num = b.id WHERE ";
			if(mech_cb.SelectedItem != null) query = query + "a.mech = \'" + mech_cb.SelectedItem.ToString() + "\'";
			if(eq_num_cb.SelectedItem != null) 
			{
				if (mech_cb.SelectedItem != null) query += " && ";
				string eq = " a.equipment_num = (SELECT id FROM equip WHERE num = \'" + eq_num_cb.SelectedItem.ToString() +"\')";
				query = query + eq;
			}	
			if(parts_box != null) query += "&& parts_used LIKE \'%" + parts_box.Text + "%\'";
			query = query + ";";
			//MessageBox.Show(query);
			string ticket = "";
			string mech = "";
			string date = "";
			string num = "";
			int x = 0;
			int y = 20;
			results1 = new Form();
			results1.ClientSize = new Size(400,800);
			results1.StartPosition = FormStartPosition.Manual;
			results1.Location = new Point(100,1500);
			Panel p = new Panel();
			p.AutoScroll = true;
			p.ClientSize = new Size(400,800);
			results1.Controls.Add(p);
			Label ticket_label = new Label();
			ticket_label.Text = "Ticket No.";
			ticket_label.Size = new Size(35,13);
			ticket_label.Location = new Point(0,5);
			Label mech_label = new Label();
			mech_label.Text = "Mechanic";
			mech_label.Size = new Size(35,13);
			mech_label.Location = new Point(60,5);
			p.Controls.Add(ticket_label);
			p.Controls.Add(mech_label);
			MySqlDataReader reader = null;
			
			MySqlConnection _conx = new MySqlConnection(connectionString);;
			_conx.Open();
			if(_conx == null) MessageBox.Show("No Connection");
			MySqlCommand sql = new MySqlCommand(query, _conx);
			reader = sql.ExecuteReader();
			
			while(reader.Read())
			{
				ticket = ((int)reader["ticket"]).ToString();
				mech = ((string)reader["mech"]).ToString();
				DateTime dt = new DateTime();
				dt = reader.GetDateTime(reader.GetOrdinal("date"));
				date = dt.ToString("yyyy-MM-dd");
				num = ((string)reader["num"]);
			Label l = new Label();
			l.Size = new Size(600,13);
			l.Text = String.Format("{0,-8} | {1,-10} | {2, -10} | {3,-10}",ticket, mech, date, num);
			l.Location = new Point(0,y);
			l.Click += new EventHandler(label_click);
			p.Controls.Add(l);
			y += 20;
			x++;

			}
			
			reader.Close();
			_conx.Close();
			reader.Dispose();
			results1.Location = new Point(100,1500);
			results1.Show();
			dlg1.Close();
}

////////////////////////////////////////////////////////////////////////////
///	Populates main window with job selected		////////////////////
////////////////////////////////////////////////////////////////////////////

		private void label_click(object sender, EventArgs e)
		{
			Label label = (Label)sender;
			label.ForeColor = Color.Red;
			MySqlDataReader reader = null;
			if(reader != null) MessageBox.Show("Reader is not null");
			
			MySqlConnection _conx = new MySqlConnection(connectionString);
			_conx.Open();
			if(_conx == null) MessageBox.Show("No Connection");
			string _ticket = label.Text.Substring(0,4);
			string eq_num,yr,make,model;
			string query = "SELECT a.date,a.hrs,a.mi,a.mech,a.parts_used,a.description,b.num, b.year, b.make, b.model from repair a join equip b on a.equipment_num = b.id where ticket = \'" + _ticket + "\'"; 
			MySqlCommand sql = new MySqlCommand(query, _conx);
			reader = null;
			reader = sql.ExecuteReader();
			while(reader.Read())
			{
				ticket_box.Text = _ticket;
				mech_cb.SelectedItem = ((string)reader["mech"]).ToString();
				DateTime dt = new DateTime();
				dt = reader.GetDateTime(reader.GetOrdinal("date"));
				date_box.Text = dt.ToString("yyyy-MM-dd");
				eq_num = ((string)reader["num"]);
				hours_box.Text = ((string)reader["hrs"].ToString());
				miles_box.Text = ((string)reader["mi"].ToString());
				parts_box.Text = ((string)reader["parts_used"]);
				repairs_box.Text = ((string)reader["description"]);
				yr = ((int)reader["year"]).ToString();
				make = ((string)reader["make"]);
				model = ((string)reader["model"]);
				this.Text = eq_num +" "+ yr + " " + ((string)reader["make"]) + " " + ((string)reader["model"]);
			}
			reader.Close();
			_conx.Close();
			
			dlg1.Close();
		}

///////////////////////////////////////////////////////////////////////////
///	Print and save			///////////////////////////////////
///////////////////////////////////////////////////////////////////////////

		private void ps_button_Click(object sender, EventArgs e)
		{
			if(date_box.Modified==true)
			{
				string d = date_box.Text;
				char[] seps = {'/'};
				string[] dates = d.Split(seps);
				d = ("20"+dates[2]+ dates[0]+dates[1]);
			}
			if(eq_num_cb.SelectedItem == null) 
			{
				MessageBox.Show("No Equip Number!");
			return;
			}
			if(mech_cb.SelectedItem == null) 
			{
				MessageBox.Show("No Mechanic Selected!");
				return;
			}
			MySqlConnection _conx = new MySqlConnection(connectionString);
			_conx.Open();
			try
			{
			int rows_affected = 0;
			string s = date_box.Text;
			//string eq = eq_num_cb.SelectedItem.ToString();
			string year = "20" + s.Substring(6,2);
			int yr = Int32.Parse(year);
			int mo = Int32.Parse(s.Substring(0,2));
			int date = Int32.Parse(s.Substring(3,2));
			DateTime dt = new DateTime(yr,mo,date);
			MySqlCommand sql = new MySqlCommand();
			sql.Connection = _conx;
				ticket = ticket_box.Text.ToString();
				if(ticket.Length < 6)
				{
				sql.CommandText = "UPDATE repair SET ticket = @ticket,date = @date, equipment_num = @equipment_num, hrs = @hrs, mi = @mi, mech = @mech, parts_used = @parts_used, description = @description WHERE ticket = " + ticket + ";";
				sql.Parameters.AddWithValue("@ticket", ticket);
				sql.Parameters.AddWithValue("@date",dt.ToString("yyyy-MM-dd"));
				sql.Parameters.AddWithValue("@equipment_num", id);
				sql.Parameters.AddWithValue("@hrs", hours_box.Text);
				sql.Parameters.AddWithValue("@mi", miles_box.Text);
				sql.Parameters.AddWithValue("@mech", mech_cb.SelectedItem.ToString());
				sql.Parameters.AddWithValue("@parts_used", parts_box.Text);
				sql.Parameters.AddWithValue("@description", repairs_box.Text);
				} else
				{ 
				sql.CommandText = "INSERT INTO repair VALUES(@ticket,@date, @equipment_num, @hrs, @mi, @mech, @parts_used, @description)";
				}
			sql.Prepare();
			sql.Parameters.AddWithValue("@ticket", '0');
			sql.Parameters.AddWithValue("@date", dt.ToString("yyyy-MM-dd"));
			sql.Parameters.AddWithValue("@equipment_num", id);
			sql.Parameters.AddWithValue("@hrs", hours_box.Text);
			sql.Parameters.AddWithValue("@mi", miles_box.Text);
			sql.Parameters.AddWithValue("@mech", mech_cb.SelectedItem.ToString());
			sql.Parameters.AddWithValue("@parts_used", parts_box.Text);
			sql.Parameters.AddWithValue("@description", repairs_box.Text);

			rows_affected = sql.ExecuteNonQuery();
			MessageBox.Show("Added Succesfully  Rows Affected: " + rows_affected);
			rows_affected = 0;
			
		} catch (MySqlException ex)
		{
			Console.WriteLine("Error: {0}", ex.ToString());
			MessageBox.Show("Error: " + ex.ToString());
		} 
			_conx.Close();

////////////////////  Print starts here		//////////////////////////////

		PrintDialog PrintDlg = new PrintDialog();
		PrintDocument pd = new PrintDocument();
		PrintDlg.Document = pd;
		PrintDlg.AllowSelection = true;
		pd.PrintPage += new PrintPageEventHandler (this.pd_PrintPage);
		if ( PrintDlg.ShowDialog() == DialogResult.OK) {
		pd.Print();
		}


		}
		private void pd_PrintPage (object sender, PrintPageEventArgs e)
		{
			StringFormat format = new StringFormat();
			Brush b = new SolidBrush(Color.Black);
			Pen p = new Pen(b);
			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Center;
			System.Drawing.Font HeaderFont = new System.Drawing.Font("Open Sans", 20);
			System.Drawing.Font SmallerFont = new System.Drawing.Font("Open Sans", 12);
			System.Drawing.Font FieldFont = new System.Drawing.Font("Times", 12);
			e.Graphics.DrawString("Rhodes Crane & Rigging, Inc.", HeaderFont, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width / 2), 100,format);
			e.Graphics.DrawString("1015 N. Thierman Rd.", SmallerFont, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width / 2), 135,format);
			e.Graphics.DrawString("Spokane Valley, WA  99212", SmallerFont, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width / 2), 160,format);
			e.Graphics.DrawString("Ticket #  "+ticket_box.Text + "         Date: " + date_box.Text + "     Miles: " + miles_box.Text + "     Hours: "+hours_box.Text, FieldFont, Brushes.Black, e.MarginBounds.Left, 270);

			e.Graphics.DrawString("Equipment #  "+eq_num_cb.SelectedItem.ToString() + "     "+yr + " " + mk + " " + md, FieldFont, Brushes.Black, e.MarginBounds.Left, 300);
			e.Graphics.DrawString("License #  " + lic + "    Vin  " + vin, FieldFont, Brushes.Black,e.MarginBounds.Left, 330);
			e.Graphics.DrawString("Mechanic:   " + mech_cb.SelectedItem.ToString(), FieldFont, Brushes.Black, e.MarginBounds.Left, 360);
			e.Graphics.DrawString("Parts Used", FieldFont, Brushes.Black,e.MarginBounds.Left, 420);
			e.Graphics.DrawRectangle(p,e.MarginBounds.Left,440,600,200);
			RectangleF pr = new RectangleF(((float)e.MarginBounds.Left + 20),450.0f, 550.0f,180.0f); 
			e.Graphics.DrawString(parts_box.Text,FieldFont, Brushes.Black, pr, StringFormat.GenericTypographic);
			e.Graphics.DrawString("Repairs Made", FieldFont, Brushes.Black, e.MarginBounds.Left, 700);
			e.Graphics.DrawRectangle(p,e.MarginBounds.Left,720,600,200);
//*******These 2 lines may have been causing trouble	
			RectangleF r = new RectangleF(((float)e.MarginBounds.Left + 20),730.0f, 550.0f,180.0f); 
			e.Graphics.DrawString(repairs_box.Text, FieldFont, Brushes.Black, r, StringFormat.GenericTypographic);
		this.Text = "Rhodes Crane Equipment Records";
			string dt = DateTime.Now.ToString("MM/dd/yy");
			date_box.Text = dt;
		miles_box.Text = "";
		hours_box.Text = "";
		parts_box.Text = "";
		repairs_box.Text = "";
		this.populate_me();
		eq_num_cb.SelectedIndex = 0;
		mech_cb.SelectedIndex = 0;
		this.ActiveControl = date_box;
		}

//////////////////////////////////////////////////////////////////////////////
		void exit_button_Click(object sender, EventArgs e)
		{
			this.Close();
		}
/////////////////////////////////////////////////////////////////////////////		
	}
}

