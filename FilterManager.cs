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
			RadioButton[] engine_oil_buttons;	
			RadioButton[] primary_fuel_buttons;	
			RadioButton[] secondary_fuel_buttons;	
			RadioButton[] outer_air_buttons;	
			RadioButton[] inner_air_buttons;	
			RadioButton[] hydraulic_buttons;	
			protected Button reset_button;
			protected Button done_button;
		public FilterWindow(string equipNum, MySqlConnection conx)
		{
			Font f = new Font("Verdana", 10, FontStyle.Bold);
			string s;
			int i;
			MySqlDataReader rdr;
			MySqlCommand sql;
			MySqlConnection _conx = conx;
			ClientSize =  new Size(500,680);
			Text = "Filter List " +equipNum;
			Label engine_oil_label = new Label();
			engine_oil_label.Font = f;
			engine_oil_label.Text = "Engine Oil Filter";
			engine_oil_label.Size = new Size(180,20);
			engine_oil_label.Location = new Point(10,10); 
			engine_oil_buttons = new RadioButton[3];
			engine_oil_buttons[0] = new RadioButton();
			engine_oil_buttons[0].Size = new Size(150,12);
			engine_oil_buttons[0].Text = "";
			engine_oil_buttons[0].Location = new Point (10,20);	
			engine_oil_buttons[0].Visible = false;
			engine_oil_buttons[0].Checked = false;	
			
			engine_oil_buttons[1] = new RadioButton();
			engine_oil_buttons[1].Text = "";
			engine_oil_buttons[1].Size = new Size(150,12);
			engine_oil_buttons[1].Location = new Point (10,40);	
			engine_oil_buttons[1].Visible = false;
			engine_oil_buttons[1].Checked = false;	

			
			engine_oil_buttons[2] = new RadioButton();
			engine_oil_buttons[2].Text = "";
			engine_oil_buttons[2].Size = new Size(150,12);
			engine_oil_buttons[2].Location = new Point (10,60);	
			engine_oil_buttons[2].Visible = false;
			engine_oil_buttons[2].Checked = false;	
			GroupBox gb1 = new GroupBox();
			gb1.Size = new Size(200,80);
			gb1.Location = new Point(10,10);
			gb1.Controls.Add(engine_oil_buttons[0]);
			gb1.Controls.Add(engine_oil_buttons[1]);
			gb1.Controls.Add(engine_oil_buttons[2]);
			s = "SELECT manu, num FROM filter_stock WHERE style = (SELECT style FROM filters WHERE type = 1 && num = (SELECT id FROM equip WHERE num = \'" + equipNum + "\'))";	
			sql = new MySqlCommand(s, _conx );
			rdr = sql.ExecuteReader();
			i = 0;
			while(rdr.Read())
			{
				string manu = (string)rdr["manu"];
				string num = (string)rdr["num"];
			engine_oil_buttons[i].Text = (manu + "   " + num);
			engine_oil_buttons[i].Visible = true;
			i++;	
						}
			rdr.Close();

			this.Controls.Add(engine_oil_label);
			this.Controls.Add(gb1);
			
			Label primary_fuel_label = new Label();
			primary_fuel_label.Font = f;
			primary_fuel_label.Text = "Primary Fuel Filter";
			primary_fuel_label.Size = new Size(180,20);
			primary_fuel_label.Location = new Point(10,120); 
			primary_fuel_buttons = new RadioButton[3];
			primary_fuel_buttons[0] = new RadioButton();
			primary_fuel_buttons[0].Size = new Size(150,12);
			primary_fuel_buttons[0].Text = "";
			primary_fuel_buttons[0].Location = new Point (10,20);	
			primary_fuel_buttons[0].Visible = false;
			primary_fuel_buttons[0].Checked = false;	
			
			primary_fuel_buttons[1] = new RadioButton();
			primary_fuel_buttons[1].Text = "";
			primary_fuel_buttons[1].Size = new Size(150,12);
			primary_fuel_buttons[1].Location = new Point (10,40);	
			primary_fuel_buttons[1].Visible = false;
			primary_fuel_buttons[1].Checked = false;	

			
			primary_fuel_buttons[2] = new RadioButton();
			primary_fuel_buttons[2].Text = "";
			primary_fuel_buttons[2].Size = new Size(150,12);
			primary_fuel_buttons[2].Location = new Point (10,60);	
			primary_fuel_buttons[2].Visible = false;
			primary_fuel_buttons[2].Checked = false;	
			GroupBox gb2 = new GroupBox();
			gb2.Size = new Size(200,80);
			gb2.Location = new Point(10,120);
			gb2.Controls.Add(primary_fuel_buttons[0]);
			gb2.Controls.Add(primary_fuel_buttons[1]);
			gb2.Controls.Add(primary_fuel_buttons[2]);
			this.Controls.Add(primary_fuel_label);
			this.Controls.Add(gb2);
			s = "SELECT manu, num FROM filter_stock WHERE style = (SELECT style FROM filters WHERE type = 2 && num = (SELECT id FROM equip WHERE num = \'" + equipNum + "\'))";	
			sql = new MySqlCommand(s, _conx );
			rdr = sql.ExecuteReader();
			i = 0;
			while(rdr.Read())
			{
				string manu = (string)rdr["manu"];
				string num = (string)rdr["num"];
			primary_fuel_buttons[i].Text = (manu + "   " + num);
			primary_fuel_buttons[i].Visible = true;
			i++;	
						}
			rdr.Close();

			
			Label secondary_fuel_label = new Label();
			secondary_fuel_label.Font = f;
			secondary_fuel_label.Text = "Secondary Fuel Filter";
			secondary_fuel_label.Size = new Size(180,20);
			secondary_fuel_label.Location = new Point(10,230); 
			secondary_fuel_buttons = new RadioButton[3];
			secondary_fuel_buttons[0] = new RadioButton();
			secondary_fuel_buttons[0].Size = new Size(150,12);
			secondary_fuel_buttons[0].Text = "";
			secondary_fuel_buttons[0].Location = new Point (10,20);	
			secondary_fuel_buttons[0].Visible = false;
			secondary_fuel_buttons[0].Checked = false;	
			
			secondary_fuel_buttons[1] = new RadioButton();
			secondary_fuel_buttons[1].Text = "";
			secondary_fuel_buttons[1].Size = new Size(150,12);
			secondary_fuel_buttons[1].Location = new Point (10,40);	
			secondary_fuel_buttons[1].Visible = false;
			secondary_fuel_buttons[1].Checked = false;	

			
			secondary_fuel_buttons[2] = new RadioButton();
			secondary_fuel_buttons[2].Text = "";
			secondary_fuel_buttons[2].Size = new Size(150,12);
			secondary_fuel_buttons[2].Location = new Point (10,60);	
			secondary_fuel_buttons[2].Visible = false;
			secondary_fuel_buttons[2].Checked = false;	
			s = "SELECT manu, num FROM filter_stock WHERE style = (SELECT style FROM filters WHERE type = 3 && num = (SELECT id FROM equip WHERE num = \'" + equipNum + "\'))";	
			sql = new MySqlCommand(s, _conx );
			rdr = sql.ExecuteReader();
			i = 0;
			while(rdr.Read())
			{
				string manu = (string)rdr["manu"];
				string num = (string)rdr["num"];
			secondary_fuel_buttons[i].Text = (manu + "   " + num);
			secondary_fuel_buttons[i].Visible = true;
			i++;	
						}
			rdr.Close();

			GroupBox gb3 = new GroupBox();
			gb3.Size =  new Size(200,80);
			gb3.Location = new Point(10,230);
			this.Controls.Add(secondary_fuel_label);
			gb3.Controls.Add(secondary_fuel_buttons[0]);
			gb3.Controls.Add(secondary_fuel_buttons[1]);
			gb3.Controls.Add(secondary_fuel_buttons[2]);
			this.Controls.Add(gb3);

			Label outer_air_label = new Label();
			outer_air_label.Font = f;
			outer_air_label.Text = "Outer Air Filter";
			outer_air_label.Size = new Size(180,20);
			outer_air_label.Location = new Point(10,340); 
			outer_air_buttons = new RadioButton[3];
			outer_air_buttons[0] = new RadioButton();
			outer_air_buttons[0].Size = new Size(150,12);
			outer_air_buttons[0].Text = "Cleaned";
			outer_air_buttons[0].Location = new Point (10,20);	
			outer_air_buttons[0].Visible = true;
			outer_air_buttons[0].Checked = false;	
			
			outer_air_buttons[1] = new RadioButton();
			outer_air_buttons[1].Text = "";
			outer_air_buttons[1].Size = new Size(150,12);
			outer_air_buttons[1].Location = new Point (10,40);	
			outer_air_buttons[1].Visible = false;
			outer_air_buttons[1].Checked = false;	

			
			outer_air_buttons[2] = new RadioButton();
			outer_air_buttons[2].Text = "";
			outer_air_buttons[2].Size = new Size(150,12);
			outer_air_buttons[2].Location = new Point (10,60);	
			outer_air_buttons[2].Visible = false;
			outer_air_buttons[2].Checked = false;	
			s = "SELECT manu, num FROM filter_stock WHERE style = (SELECT style FROM filters WHERE type = 4 && num = (SELECT id FROM equip WHERE num = \'" + equipNum + "\'))";	
			sql = new MySqlCommand(s, _conx );
			rdr = sql.ExecuteReader();
			i = 1;
			while(rdr.Read())
			{
				string manu = (string)rdr["manu"];
				string num = (string)rdr["num"];
			outer_air_buttons[i].Text = (manu + "   " + num);
			outer_air_buttons[i].Visible = true;
			i++;	
						}
			rdr.Close();

			GroupBox gb4 = new GroupBox();
			gb4.Size = new Size(200,80);
			gb4.Location = new Point(10,340);

			this.Controls.Add(outer_air_label);
			gb4.Controls.Add(outer_air_buttons[0]);
			gb4.Controls.Add(outer_air_buttons[1]);
			gb4.Controls.Add(outer_air_buttons[2]);
			this.Controls.Add(gb4);
			
			Label inner_air_label = new Label();
			inner_air_label.Font = f;
			inner_air_label.Text = "Inner Air Filter";
			inner_air_label.Size = new Size(180,20);
			inner_air_label.Location = new Point(10,450); 
			inner_air_buttons = new RadioButton[3];
			inner_air_buttons[0] = new RadioButton();
			inner_air_buttons[0].Size = new Size(150,12);
			inner_air_buttons[0].Text = "";
			inner_air_buttons[0].Location = new Point (10,20);	
			inner_air_buttons[0].Visible = false;
			inner_air_buttons[0].Checked = false;	
			
			inner_air_buttons[1] = new RadioButton();
			inner_air_buttons[1].Text = "";
			inner_air_buttons[1].Size = new Size(150,12);
			inner_air_buttons[1].Location = new Point (10,40);	
			inner_air_buttons[1].Visible = false;
			inner_air_buttons[1].Checked = false;	

			
			inner_air_buttons[2] = new RadioButton();
			inner_air_buttons[2].Text = "";
			inner_air_buttons[2].Size = new Size(150,12);
			inner_air_buttons[2].Location = new Point (10,60);	
			inner_air_buttons[2].Visible = false;
			inner_air_buttons[2].Checked = false;	
			s = "SELECT manu, num FROM filter_stock WHERE style = (SELECT style FROM filters WHERE type = 5 && num = (SELECT id FROM equip WHERE num = \'" + equipNum + "\'))";	
			sql = new MySqlCommand(s, _conx );
			rdr = sql.ExecuteReader();
			i = 0;
			while(rdr.Read())
			{
				string manu = (string)rdr["manu"];
				string num = (string)rdr["num"];
			inner_air_buttons[i].Text = (manu + "   " + num);
			inner_air_buttons[i].Visible = true;
			i++;	
						}
			rdr.Close();

			GroupBox gb5 = new GroupBox();
			gb5.Size = new Size(200,80);
			gb5.Location = new Point(10,450);

			this.Controls.Add(inner_air_label);
			gb5.Controls.Add(inner_air_buttons[0]);
			gb5.Controls.Add(inner_air_buttons[1]);
			gb5.Controls.Add(inner_air_buttons[2]);
			this.Controls.Add(gb5);
			
			Label hydraulic_label = new Label();
			hydraulic_label.Font = f;
			hydraulic_label.Text = "Hydraulic Filter";
			hydraulic_label.Size = new Size(180,20);
			hydraulic_label.Location = new Point(250,10); 
			hydraulic_buttons = new RadioButton[3];
			hydraulic_buttons[0] = new RadioButton();
			hydraulic_buttons[0].Size = new Size(150,12);
			hydraulic_buttons[0].Text = "Cleaned";
			hydraulic_buttons[0].Location = new Point (10,20);	
			hydraulic_buttons[0].Visible = true;
			hydraulic_buttons[0].Checked = false;	
			
			hydraulic_buttons[1] = new RadioButton();
			hydraulic_buttons[1].Text = "";
			hydraulic_buttons[1].Size = new Size(150,12);
			hydraulic_buttons[1].Location = new Point (10,40);	
			hydraulic_buttons[1].Visible = false;
			hydraulic_buttons[1].Checked = false;	

			
			hydraulic_buttons[2] = new RadioButton();
			hydraulic_buttons[2].Text = "";
			hydraulic_buttons[2].Size = new Size(150,12);
			hydraulic_buttons[2].Location = new Point (10,60);	
			hydraulic_buttons[2].Visible = false;
			hydraulic_buttons[2].Checked = false;	
			s = "SELECT manu, num FROM filter_stock WHERE style = (SELECT style FROM filters WHERE type = 6 && num = (SELECT id FROM equip WHERE num = \'" + equipNum + "\'))";	
			sql = new MySqlCommand(s, _conx );
			rdr = sql.ExecuteReader();
			i = 0;
			while(rdr.Read())
			{
				string manu = (string)rdr["manu"];
				string num = (string)rdr["num"];
			hydraulic_buttons[i].Text = (manu + "   " + num);
			hydraulic_buttons[i].Visible = true;
			i++;	
						}
			rdr.Close();

			GroupBox gb6 = new GroupBox();
			gb6.Size = new Size(200,80);
			gb6.Location = new Point(250,10);

			this.Controls.Add(hydraulic_label);
			gb6.Controls.Add(hydraulic_buttons[0]);
			gb6.Controls.Add(hydraulic_buttons[1]);
			gb6.Controls.Add(hydraulic_buttons[2]);
			this.Controls.Add(gb6);
			
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
			engine_oil_buttons[0].Checked = false;
			engine_oil_buttons[1].Checked = false;
			engine_oil_buttons[2].Checked = false;
			primary_fuel_buttons[0].Checked = false;
			primary_fuel_buttons[1].Checked = false;
			primary_fuel_buttons[2].Checked = false;
			secondary_fuel_buttons[0].Checked = false;
			secondary_fuel_buttons[1].Checked = false;
			secondary_fuel_buttons[2].Checked = false;
			inner_air_buttons[0].Checked = false;
			inner_air_buttons[1].Checked = false;
			inner_air_buttons[2].Checked = false;
			outer_air_buttons[0].Checked = false;
			outer_air_buttons[1].Checked = false;
			outer_air_buttons[2].Checked = false;
			hydraulic_buttons[0].Checked = false;
			hydraulic_buttons[1].Checked = false;
			hydraulic_buttons[2].Checked = false;
		}

////////////////////////////////////////////////////////////////////////////
		void done_button_clicked(Object sender, System.EventArgs e)
		{
			this.Close();
		}
///////////////////////////////////////////////////////////////////////////		
					
		public string GetText()
		{
			RadioButton rb = null;
			string str = "";
			blurb = "";
			if(engine_oil_buttons[0].Checked == true) rb = engine_oil_buttons[0];
			else if(engine_oil_buttons[1].Checked == true) rb = engine_oil_buttons[1];
			else if(engine_oil_buttons[2].Checked == true) rb = engine_oil_buttons[2];
			if(rb != null)	
			{
				str = rb.Text;	
				blurb = "Changed Engine Oil & Filter";
			}
			
			rb = null;
			if(primary_fuel_buttons[0].Checked == true) rb = primary_fuel_buttons[0];
			else if(primary_fuel_buttons[1].Checked == true) rb = primary_fuel_buttons[1];
			else if(primary_fuel_buttons[2].Checked == true) rb = primary_fuel_buttons[2];
			if(rb != null)
			{
				str = str + "\n" + rb.Text;
				blurb = blurb + "\nChanged Primary Fuel Filter";
			}
			
			rb = null;
			if(secondary_fuel_buttons[0].Checked == true) rb = secondary_fuel_buttons[0];
			else if(secondary_fuel_buttons[1].Checked == true) rb = secondary_fuel_buttons[1];
			else if(secondary_fuel_buttons[2].Checked == true) rb = secondary_fuel_buttons[2];
			if(rb != null)
			{
				str = str + "\n" + rb.Text;
				blurb = blurb + "\nChanged Secondary Fuel Filter";
			}
			
			rb = null;
			if(outer_air_buttons[1].Checked == true) rb = outer_air_buttons[1];
			else if(outer_air_buttons[2].Checked == true) rb = outer_air_buttons[2];
			if(rb != null)
			{
				if(rb == outer_air_buttons[0])
				{	blurb = blurb + "\nCleaned Outer Air Filter";} else blurb = blurb + "\nReplaced Outer Air Filter";
				str = str + "\n" + rb.Text;
				}
		
			rb = null;
			if(inner_air_buttons[0].Checked == true) rb = inner_air_buttons[0];
			else if(inner_air_buttons[1].Checked == true) rb = inner_air_buttons[1];
			else if(inner_air_buttons[2].Checked == true) rb = inner_air_buttons[2];
			if(rb != null)
			{
				blurb = blurb +"\nReplaced Inner Air Filter";
				str = str + "\n" + rb.Text;
				}
			rb = null;
			if(hydraulic_buttons[0].Checked == true) rb = hydraulic_buttons[0];
			else if(hydraulic_buttons[1].Checked == true) rb = hydraulic_buttons[1];
			else if(hydraulic_buttons[2].Checked == true) rb = hydraulic_buttons[2];
			if(rb != null)
			{
			blurb = blurb + "\nReplaced Hydraulic Filter";
				str = str + "\n" + rb.Text;
			}
			return str;
		} // end method
		
/////////////////////////////////////////////////////////////////////////////

	}// end class
} // end namespace

