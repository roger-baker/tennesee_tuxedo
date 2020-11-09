using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace RhodesEquipment
{
	public class FilterWindow : System.Windows.Forms.Form
	{
			public string blurb;
			//RadioButton[] engine_oil_buttons;	
			ListBox OilListBox = new ListBox();
			ListBox PFuelListBox = new ListBox();
			ListBox SFuelListBox = new ListBox();
			ListBox OAirListBox = new ListBox();
			ListBox IAirListBox = new ListBox();
			ListBox CoolListBox = new ListBox();

			protected Button reset_button;
			protected Button done_button;
			MySqlConnection _conx;
		//public FilterWindow(string equipNum, MySqlConnection conx) Changed
		public FilterWindow(string equipNum, string connection_string)
		{
			Font f = new Font("Verdana", 10, FontStyle.Bold);
			string s;
			int i;
			MySqlDataReader rdr;
			MySqlCommand sql;
			//MySqlConnection _conx = conx; Changed
			//_conx = GetaConnection();
		_conx = new MySqlConnection(connection_string);
		_conx.Open();
		if(_conx == null)
		Console.WriteLine("Database Connection Failed...");
		//return (conx);
			ClientSize =  new Size(500,680);
			Text = "Filter List " +equipNum;
			Label engine_oil_label = new Label();
			engine_oil_label.Font = f;
			engine_oil_label.Text = "Engine Oil Filter";
			engine_oil_label.Size = new Size(150,20);
			engine_oil_label.Location = new Point(10,10); 
			OilListBox.Location = new Point(10, 30);
			OilListBox.Name = "OilListBox";
			OilListBox.Size = new Size(200, 80);
			this.Controls.Add(engine_oil_label);
			Controls.Add(OilListBox);
			s = "SELECT manu, num FROM filter_stock WHERE style = (SELECT style FROM filters WHERE type = 1 && num = (SELECT id FROM equip WHERE num = \'" + equipNum + "\'))";	
			sql = new MySqlCommand(s, _conx );
			rdr = sql.ExecuteReader();
			i = 0;
			while(rdr.Read()) //&& i < 5)
			{
				string manu = (string)rdr["manu"];
				string num = (string)rdr["num"];
			OilListBox.Items.Add(manu + "  " + num);
			i++;	
						}
			rdr.Close();
			//_conx.Close();// Changed
//////////////////////////////////////////////////////////////////////////////////  Primary Fuel /////////////////////////////////////////////////////////////
			
			Label primary_fuel_label = new Label();
			primary_fuel_label.Font = f;
			primary_fuel_label.Text = "Primary Fuel Filter";
			primary_fuel_label.Size = new Size(180,20);
			primary_fuel_label.Location = new Point(10,120); 
			PFuelListBox.Location = new Point(10, 140);
			PFuelListBox.Name = "PFuelListBox";
			PFuelListBox.Size = new Size(200, 80);
			this.Controls.Add(primary_fuel_label);
			Controls.Add(PFuelListBox);
			s = "SELECT manu, num FROM filter_stock WHERE style = (SELECT style FROM filters WHERE type = 2 && num = (SELECT id FROM equip WHERE num = \'" + equipNum + "\'))";	
			sql = new MySqlCommand(s, _conx );
			rdr = sql.ExecuteReader();
			i = 0;
			while(rdr.Read()) //&& i < 5)
			{
				string manu = (string)rdr["manu"];
				string num = (string)rdr["num"];
			PFuelListBox.Items.Add(manu + "  " + num);
			i++;	
						}
			rdr.Close();
///////////////////////////////////////////////////////////////////////////
///  Secondary Fuel

			Label secondary_fuel_label = new Label();
			secondary_fuel_label.Font = f;
			secondary_fuel_label.Text = "Secondary Fuel Filter";
			secondary_fuel_label.Size = new Size(180,20);
			secondary_fuel_label.Location = new Point(10,230); 
			secondary_fuel_label.Font = f;
			SFuelListBox.Location = new Point(10, 250);
			SFuelListBox.Name = "PFuelListBox";
			SFuelListBox.Size = new Size(200, 80);
			this.Controls.Add(secondary_fuel_label);
			Controls.Add(SFuelListBox);
			s = "SELECT manu, num FROM filter_stock WHERE style = (SELECT style FROM filters WHERE type = 3 && num = (SELECT id FROM equip WHERE num = \'" + equipNum + "\'))";	
			sql = new MySqlCommand(s, _conx );
			rdr = sql.ExecuteReader();
			i = 0;
			while(rdr.Read()) //&& i < 5)
			{
				string manu = (string)rdr["manu"];
				string num = (string)rdr["num"];
			SFuelListBox.Items.Add(manu + "  " + num);
			i++;	
						}
			rdr.Close();

/////////////////////////////////////////////////////////////////////////////
///   Outer Air

			Label outer_air_label = new Label();
			outer_air_label.Font = f;
			outer_air_label.Text = "Outer Air Filter";
			outer_air_label.Size = new Size(180,20);
			outer_air_label.Location = new Point(10,340); 
			OAirListBox.Location = new Point(10, 360);
			OAirListBox.Name = "PFuelListBox";
			OAirListBox.Size = new Size(200, 80);
			this.Controls.Add(outer_air_label);
			Controls.Add(OAirListBox);
			s = "SELECT manu, num FROM filter_stock WHERE style = (SELECT style FROM filters WHERE type = 4 && num = (SELECT id FROM equip WHERE num = \'" + equipNum + "\'))";	
			sql = new MySqlCommand(s, _conx );
			rdr = sql.ExecuteReader();
			i = 0;
			while(rdr.Read()) //&& i < 5)
			{
				string manu = (string)rdr["manu"];
				string num = (string)rdr["num"];
			OAirListBox.Items.Add(manu + "  " + num);
			i++;	
						}
			rdr.Close();
//////////////////////////////////////////////////////////////////////////////
///    Inner Air

			Label inner_air_label = new Label();
			inner_air_label.Font = f;
			inner_air_label.Text = "Inner Air Filter";
			inner_air_label.Size = new Size(180,20);
			inner_air_label.Location = new Point(10,450); 
			IAirListBox.Location = new Point(10, 470);
			IAirListBox.Name = "IFuelListBox";
			IAirListBox.Size = new Size(200, 80);
			this.Controls.Add(inner_air_label);
			Controls.Add(IAirListBox);
			s = "SELECT manu, num FROM filter_stock WHERE style = (SELECT style FROM filters WHERE type = 5 && num = (SELECT id FROM equip WHERE num = \'" + equipNum + "\'))";	
			sql = new MySqlCommand(s, _conx );
			rdr = sql.ExecuteReader();
			i = 0;
			while(rdr.Read()) //&& i < 5)
			{
				string manu = (string)rdr["manu"];
				string num = (string)rdr["num"];
			IAirListBox.Items.Add(manu + "  " + num);
			i++;	
						}
			rdr.Close();
/////////////////////////////////////////////////////////////////////////////
///   Water Filter

			Label coolant_label = new Label();
			coolant_label.Font = f;
			coolant_label.Text = "Coolant Filter";
			coolant_label.Size = new Size(180,20);
			coolant_label.Location = new Point(250,10); 
			CoolListBox.Location = new Point(250,30);
			CoolListBox.Name = "CoolListBox";
			CoolListBox.Size = new Size(200, 80);
			this.Controls.Add(coolant_label);
			Controls.Add(CoolListBox);
			s = "SELECT manu, num FROM filter_stock WHERE style = (SELECT style FROM filters WHERE type = 6 && num = (SELECT id FROM equip WHERE num = \'" + equipNum + "\'))";	
			sql = new MySqlCommand(s, _conx );
			rdr = sql.ExecuteReader();
			i = 0;
			while(rdr.Read()) //&& i < 5)
			{
				string manu = (string)rdr["manu"];
				string num = (string)rdr["num"];
			CoolListBox.Items.Add(manu + "  " + num);
			i++;	
						}
			rdr.Close();
/////////////////////////////////////////////////////////////////////////////
///	Buttons
			reset_button = new Button();
			reset_button.Text = "Reset";
			reset_button.Location = new Point(30,600);
			reset_button.Visible = true;
			reset_button.Click += new EventHandler(reset_button_clicked);
			this.Controls.Add(reset_button);
			done_button = new Button();
			done_button.Text = "Done";
			done_button.Location = new Point(300,600);
			done_button.Click += new EventHandler(done_button_clicked);
			this.Controls.Add(done_button);



		}// End Constructor
///////////////////////////////////////////////////////////////////////
		void buttonHandler(Object sender, System.EventArgs e)
		{
			this.Close();
		}
////////////////////////////////////////////////////////////////////////////		
		void reset_button_clicked(Object sender, System.EventArgs e)
		{
			OilListBox.ClearSelected();
			PFuelListBox.ClearSelected();
			SFuelListBox.ClearSelected();
			IAirListBox.ClearSelected();
			OAirListBox.ClearSelected();
			CoolListBox.ClearSelected();
		}


////////////////////////////////////////////////////////////////////////////
		void done_button_clicked(Object sender, System.EventArgs e)
		{
			this.Close();
			_conx.Close();
		}
///////////////////////////////////////////////////////////////////////////		
					
		public string GetText()
		{
			string str = "";
			blurb = "";
			 if(OilListBox.SelectedIndex >= 0)
			 {
				str = OilListBox.Text + "\n";	
				blurb = "Changed Engine Oil & Filter\n";
			}
			
			if(PFuelListBox.SelectedIndex >= 0)
			{
				str = str + PFuelListBox.Text + "\n";
				blurb = blurb + "Changed Primary Fuel Filter\n";
			}
			
			if(SFuelListBox.SelectedIndex >= 0)
			{

				str = str + SFuelListBox.Text + "\n";
				blurb = blurb + "Changed Secondary Fuel Filter\n";
			}
			
			if(OAirListBox.SelectedIndex >= 0)
			{
				str = str + OAirListBox.Text + "\n";
				blurb = blurb + "Replaced Air Filter\n";	
			}
		
			if(IAirListBox.SelectedIndex >= 0)
			{
				str = str + IAirListBox.Text + "\n";
				blurb = blurb +"Replaced Inner Air Filter\n";
			}

			if(CoolListBox.SelectedIndex >= 0)
			{
				blurb = blurb + "Replaced Coolant Filter\n";
				str = str + CoolListBox.Text + "\n";
			}
			_conx.Close();
			return str;
			
		} // end method
		
/////////////////////////////////////////////////////////////////////////////
		static MySqlConnection  GetaConnection()
		{
		MySqlConnection conx;

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
		return (conx);
		}

	}// end class
} // end namespace
