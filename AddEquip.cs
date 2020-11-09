using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace RhodesEquipment
{
	public class AddEquip : System.Windows.Forms.Form
	{
		protected TextBox IdBox;
		protected TextBox EquipNumBox;
		protected TextBox YearBox;
		protected TextBox MakeBox;
		protected TextBox ModelBox;
		protected TextBox VinBox;
		protected TextBox LicBox;
		protected TextBox EngOilFilBox;
		protected TextBox PriFuelFilBox;
		protected TextBox SecFuelFilBox;
		protected TextBox OutAirFilBox;
		protected TextBox InAirFilBox;
		protected TextBox TranFilBox;
		protected TextBox HydFilBox;
		private bool equip_edited, filters_edited = false;
		static MySqlConnection _conx;
		string EngOilFilStyle,PriFuelFilStyle,SecFuelFilStyle,OutAirFilStyle,InAirFilStyle,TranFilStyle;
		AddFilter af;
		public static string _connectionString;

	public AddEquip(string connectionString)
	{
		_connectionString = connectionString;
		Text = "Manage Equipment";
		Font f = new Font("Verdana", 10);
		ClientSize = new Size(500, 680);
		Label IdLabel = new Label();
		IdLabel.Text = "ID #";
		IdLabel.Size = new Size(40,20);
		IdLabel.Location = new Point(10, 10);
		IdLabel.Font = f;
		this.Controls.Add(IdLabel);
		IdBox = new TextBox();
		IdBox.Size = new Size(40,15);
		IdBox.Location = new Point(55, 10);
		IdBox.ReadOnly = true;
		this.Controls.Add(IdBox);
		Label equipment_label = new Label();
		equipment_label.Text = "Equipment No.";
		equipment_label.Font = f;
		equipment_label.Size = new Size(115,20);
		equipment_label.Location = new Point(120, 10);
		this.Controls.Add(equipment_label);
		EquipNumBox = new TextBox();
		EquipNumBox.Size = new Size(60, 15);
		EquipNumBox.Location = new Point(235, 10);
		EquipNumBox.TabIndex = 0;
		EquipNumBox.Leave += new System.EventHandler(EquipBoxHandler);
		this.Controls.Add(EquipNumBox);
		Label LicLabel = new Label();
		LicLabel.Font = f;
		LicLabel.Text = "License";
		LicLabel.Size = new Size(60, 20);
		LicLabel.Location = new Point(320, 10);
		this.Controls.Add(LicLabel);
		LicBox = new TextBox();
		LicBox.Size = new Size(80, 15);
		LicBox.Location = new Point(390, 10);
		LicBox.TabIndex = 1;
		this.Controls.Add(LicBox);
		Label YearLabel = new Label();
		YearLabel.Font = f;
		YearLabel.Text = "Year";
		YearLabel.Size = new Size(40, 20);
		YearLabel.Location = new Point(10, 50);
		this.Controls.Add(YearLabel);
		YearBox = new TextBox();
		YearBox.Size = new Size(40, 15);
		YearBox.Location = new Point(60, 50);
		YearBox.TabIndex = 2;
		this.Controls.Add(YearBox);
		Label MakeLabel = new Label();
		MakeLabel.Font = f;
		MakeLabel.Text = "Make";
		MakeLabel.Size = new Size(50, 20);
		MakeLabel.Location = new Point(120, 50);
		this.Controls.Add(MakeLabel);
		MakeBox = new TextBox();
		MakeBox.Size = new Size(100, 15);
		MakeBox.TabIndex = 3;
		MakeBox.Location = new Point(180, 50);
		this.Controls.Add(MakeBox);
		Label ModelLabel = new Label();
		ModelLabel.Font = f;
		ModelLabel.Text = "Model";
		ModelLabel.Size = new Size(50, 20);
		ModelLabel.Location = new Point(300, 50);
		this.Controls.Add(ModelLabel);
		ModelBox = new TextBox();
		ModelBox.TabIndex = 4;
		ModelBox.Size = new Size(100, 15);
		ModelBox.Location = new Point(350, 50);
		this.Controls.Add(ModelBox);
		Label VinLabel = new Label();
		VinLabel.Font = f;
		VinLabel.Text = "Vin";
		VinLabel.Size = new Size(40, 20);
		VinLabel.Location = new Point(15, 90);
		this.Controls.Add(VinLabel);
		VinBox = new TextBox();
		VinBox.TabIndex = 5;
		VinBox.Size = new Size(200, 15);
		VinBox.Location = new Point(90, 90);
		this.Controls.Add(VinBox);
		Label SepLabel = new Label();
		SepLabel.AutoSize = false;
		SepLabel.Size = new Size(480, 2);
		SepLabel.Location = new Point(10, 130);
		SepLabel.BorderStyle = BorderStyle.Fixed3D;
		this.Controls.Add(SepLabel);
		Label EngLabel = new Label();
		EngLabel.Font = f;
		EngLabel.Text = "Engine Oil Filter";
		EngLabel.Size = new Size(115, 20);
		EngLabel.Location = new Point(10, 150);
		this.Controls.Add(EngLabel);
		EngOilFilBox = new TextBox();
		EngOilFilBox.TabIndex = 6;
 	   	EngOilFilBox.Size = new Size(100, 15);
		EngOilFilBox.Location = new Point(125,150);
		EngOilFilBox.Leave += new System.EventHandler(BoxHandler);
		this.Controls.Add(EngOilFilBox);
		Label PriFuelLabel = new Label();
		PriFuelLabel.Text = "Primary Fuel Filter";
		PriFuelLabel.Size = new Size(120, 15);
		PriFuelLabel.Font = f;
		PriFuelLabel.Location = new Point(240, 150);
		this.Controls.Add(PriFuelLabel);
		PriFuelFilBox = new TextBox();
		PriFuelFilBox.TabIndex = 7;
		PriFuelFilBox.Size = new Size(100, 15);
		PriFuelFilBox.Location = new Point(375, 150);
		PriFuelFilBox.Leave += new EventHandler(BoxHandler);
		this.Controls.Add(PriFuelFilBox);
		Label SecFuelLabel = new Label();
		SecFuelLabel.Font = f;
		SecFuelLabel.Text = "Secondary Fuel Filter";
		SecFuelLabel.Size = new Size(120, 15);
		SecFuelLabel.Location = new Point(10, 190);
		this.Controls.Add(SecFuelLabel);
		SecFuelFilBox = new TextBox();
		SecFuelFilBox.TabIndex = 8;
		SecFuelFilBox.Size = new Size(100, 15);
		SecFuelFilBox.Location = new Point(130, 190);
		SecFuelFilBox.Leave += new EventHandler(BoxHandler);
		this.Controls.Add(SecFuelFilBox);
		Label OutAirLabel = new Label();
		OutAirLabel.Font = f;
		OutAirLabel.Text = "Outer Air Filter";
		OutAirLabel.Size = new Size(120, 15);
		OutAirLabel.Location = new Point(245, 190);
		this.Controls.Add(OutAirLabel);
		OutAirFilBox = new TextBox();
		OutAirFilBox.TabIndex = 9;
		OutAirFilBox.Size = new Size(100, 15);
		OutAirFilBox.Location = new Point(375, 190);
		OutAirFilBox.Leave += new EventHandler(BoxHandler);
		this.Controls.Add(OutAirFilBox);
		Label InAirLabel = new Label();
		InAirLabel.Font = f;
		InAirLabel.Text = "Inner Air Filter";
		InAirLabel.Size = new Size(120, 15);
		InAirLabel.Location = new Point(10,230);
		this.Controls.Add(InAirLabel);
		InAirFilBox = new TextBox();
		InAirFilBox.TabIndex = 10;
		InAirFilBox.Size = new Size(100, 15);
		InAirFilBox.Location = new Point(130, 230);
		InAirFilBox.Leave += new EventHandler(BoxHandler);
		this.Controls.Add(InAirFilBox);
		Label TranFilLabel = new Label();
		TranFilLabel.Font = f;
		TranFilLabel.Text = "Transmission Filter";
		TranFilLabel.Size = new Size(120, 15);
		TranFilLabel.Location = new Point(245, 230);
		this.Controls.Add(TranFilLabel);
		TranFilBox = new TextBox();
		TranFilBox.TabIndex = 11;
		TranFilBox.Size = new Size(100, 15);
		TranFilBox.Location = new Point(375,230);
		TranFilBox.Leave += new EventHandler(BoxHandler);
		this.Controls.Add(TranFilBox);
		Button b = new Button();
		b.Text = "Submit";
		b.Location = new Point(200,270);
		b.TabIndex = 12;
		b.Click += new EventHandler(AddEquipClick);
		this.Controls.Add(b);
		Button b2 = new Button();
		b2.Text = "Add Filters";
	    b2.TabIndex = 13;
		b2.Location = new Point(270,270);
		b2.Click += new EventHandler(AddFiltersClick);
		this.Controls.Add(b2);
   			   


	}// End Consructor
	void EquipBoxHandler(object sender, System.EventArgs e)
	{
		string query = "SELECT id,num,year,make,model,lic,vin FROM equip WHERE num = \'" + EquipNumBox.Text +"\';";
		MySqlDataReader reader = null;
		_conx = new MySqlConnection(_connectionString);
		MySqlCommand sql = new MySqlCommand(query, _conx);
		if(_conx == null) MessageBox.Show("Connection Failed");
		_conx.Open();
		reader = sql.ExecuteReader();
		int Id = 0;
		int Year = 0;
		string ENum = null;
		string Make = null;
		string Model = null;
		string Vin = null;
		string Lic = null;
		while(reader.Read())
		{
			Id = ((int)reader["id"]);
			Year = ((int)reader["year"]);
			ENum = ((string)reader["num"]); 
			Make = ((string)reader["make"]);
			Model = ((string)reader["model"]);
			Vin = ((string)reader["vin"]);
			Lic = ((string)reader["lic"]);
					}
					if(ENum != null)
					{
					IdBox.Text = Id.ToString(); 
					YearBox.Text = Year.ToString();
					MakeBox.Text = Make;
					ModelBox.Text = Model;
					VinBox.Text = Vin;
					LicBox.Text = Lic;
					}
					reader.Close();
					_conx.Close(); 

	}
	void BoxHandler(object sender, System.EventArgs e)
	{
		TextBox tb = (TextBox)sender;
		string query = "SELECT num,style FROM filter_stock WHERE num = \'" + tb.Text +"\';";
		MySqlDataReader reader = null;
		_conx = GetaConnection(_connectionString);
		MySqlCommand sql = new MySqlCommand(query, _conx);
		reader = sql.ExecuteReader();
		string num = null;
		int _style = 0;
		while(reader.Read())
		{
			num = ((string)reader["num"]); 
			_style = ((int)reader["style"]);
					}
					if(num != null)
					{
						MessageBox.Show("Filter Exists");
						tb.BackColor = Color.Green;
					} else
					{
						MessageBox.Show("We Should Add This Filter...");
						tb.BackColor = Color.Red;

					}
					reader.Close();
					_conx.Close(); // Added
					reader.Dispose();
					return;

	}
	void AddEquipClick(object sender, System.EventArgs e)
	{
			try
			{
			MySqlCommand sql = new MySqlCommand();
			_conx = GetaConnection(_connectionString); // Added
			sql.Connection = _conx;  

			if(_conx == null) MessageBox.Show("No Connection");
				sql.CommandText = "INSERT INTO equip (`id`, `num`, `year`, `make`, `model`, `ser`, `vin`, `lic`, `annual`) VALUES ('', @num, @year, @make, @model, @ser, @vin, @lic, @annual)";
				
			sql.Prepare();
			sql.Parameters.AddWithValue("@num", EquipNumBox.Text);
			sql.Parameters.AddWithValue("@year", Int32.Parse(YearBox.Text));
			sql.Parameters.AddWithValue("@make", MakeBox.Text);
			sql.Parameters.AddWithValue("@model", ModelBox.Text);
			sql.Parameters.AddWithValue("@vin", VinBox.Text);
			sql.Parameters.AddWithValue("@lic", LicBox.Text);
			sql.Parameters.AddWithValue("@ser", VinBox.Text);
			sql.Parameters.AddWithValue("@annual", 2018);

			sql.ExecuteNonQuery();
			sql.Dispose();
			_conx.Close(); 
			
		} catch (MySqlException ex)
		 {
			MessageBox.Show("Error: " +ex.ToString());
		}
		MessageBox.Show(EquipNumBox.Text + " Added Successfully");
		IdBox.Text = "";
		EquipNumBox.Text = "";
		LicBox.Text = "";
		YearBox.Text = "";
		MakeBox.Text = "";
		ModelBox.Text = "";
		VinBox.Text = "";

	}
	///////////////////////////////////////////////////////////////////
		void AddFiltersClick(object sender, EventArgs e)
	{
		string q;
		if(EquipNumBox.Text == "") 
		{
			MessageBox.Show("No Num");
			return;
		} else
		{
			q = "INSERT INTO filters VALUES('" +IdBox.Text+"',(SELECT style FROM filter_stock WHERE  num = '"+EngOilFilBox.Text+"'),'1')";
		}
		if(PriFuelFilBox.Text != "")
			q += ",('" +IdBox.Text+"', (SELECT style FROM filter_stock WHERE num = '" +PriFuelFilBox.Text+"'),'2')";
		if(SecFuelFilBox.Text != "")
			q += ",('" +IdBox.Text+"',(SELECT style FROM filter_stock WHERE num = '" +SecFuelFilBox.Text+"'),'3')";
		if(OutAirFilBox.Text != "")
			q += ",('" +IdBox.Text+"',(SELECT style FROM filter_stock WHERE num = '" +OutAirFilBox.Text+"'),'4')";
		if(InAirFilBox.Text != "")
			q += ",('" +IdBox.Text+"',(SELECT style FROM filter_stock WHERE num = '" +InAirFilBox.Text+"'),'5')";
		if(TranFilBox.Text != "")
			q += ",('" +IdBox.Text+"',(SELECT style FROM filter_stock WHERE num = '" +TranFilBox.Text+"'),'6')";
		q +=";";
		MessageBox.Show(q);
	////////////////////////////////////////////////////////////////////
		static MySqlConnection  GetaConnection(string connectionString)
		{
		MySqlConnection conx;

		conx = new MySqlConnection(connectionString);
		conx.Open();
		if(conx == null)
		Console.WriteLine("Database Connection Failed...");
		return (conx);
		}
	}// End Class 
}// End namespace
