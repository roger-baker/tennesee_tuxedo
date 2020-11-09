
using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace RhodesEquipment
{
	public class FilterListManager : System.Windows.Forms.Form
	{
		protected TextBox EqNumBox;
		protected TextBox EngOilBox;
		protected TextBox PriFuelBox;
		protected TextBox SecFuelBox;
		protected TextBox InnerAirBox;
		protected TextBox OuterAirBox;
		protected TextBox CoolBox;
		//static MySqlConnection _conx;
		static string _connectionString;
		int _id;

		public FilterListManager(string connectionString, int id)
		{
			_connectionString = connectionString;
			Text = "Manage Filter List";
			Font f = new Font("Verdana", 10);
			//_conx = conx;
			_id = id;
			ClientSize = new Size(400, 500);
			Label NameLabel = new Label();
			//NameLabel.Font = f;
			NameLabel.Text = "Equipment Number";
			NameLabel.Size = new Size(125, 13);
			NameLabel.Location = new Point(10,5);
			
			this.Controls.Add(NameLabel);
			EqNumBox = new TextBox();
			EqNumBox.Size = new Size(85,15);
			EqNumBox.Location = new Point(140,5);
			this.Controls.Add(EqNumBox);

			Label EOFLabel = new Label();
			EOFLabel.Font = f;
			EOFLabel.Size = new Size(125, 13);
			EOFLabel.Text = "Engine Oil Filter";
			EOFLabel.Location = new Point(10,45);
			this.Controls.Add(EOFLabel);

			EngOilBox = new TextBox();
			EngOilBox.Size = new Size(85, 15);
			EngOilBox.Location = new Point(140, 45);
			this.Controls.Add(EngOilBox);
			
			Label PriFuelLabel = new Label();
			//EOFLabel.Font = f;
			PriFuelLabel.Size = new Size(125, 13);
			PriFuelLabel.Text = "Primary Fuel Filter";
			PriFuelLabel.Location = new Point(10,85);
			this.Controls.Add(PriFuelLabel);

			PriFuelBox = new TextBox();
			PriFuelBox.Size = new Size(85, 15);
			PriFuelBox.Location = new Point(140, 85);
			this.Controls.Add(PriFuelBox);
			
			Label SecFuelLabel = new Label();
			//EOFLabel.Font = f;
			SecFuelLabel.Size = new Size(125, 13);
			SecFuelLabel.Text = "Secondary Fuel Filter";
			SecFuelLabel.Location = new Point(10,125);
			this.Controls.Add(SecFuelLabel);

			SecFuelBox = new TextBox();
			SecFuelBox.Size = new Size(85, 15);
			SecFuelBox.Location = new Point(140, 125);
			this.Controls.Add(SecFuelBox);
			
			Label InnerAirLabel = new Label();
			//EOFLabel.Font = f;
			InnerAirLabel.Size = new Size(125, 13);
			InnerAirLabel.Text = "Inner Air Filter";
			InnerAirLabel.Location = new Point(10,165);
			this.Controls.Add(InnerAirLabel);

			InnerAirBox = new TextBox();
			InnerAirBox.Size = new Size(85, 15);
			InnerAirBox.Location = new Point(140, 165);
			this.Controls.Add(InnerAirBox);
			
			Label OuterAirLabel = new Label();
			//EOFLabel.Font = f;
			OuterAirLabel.Size = new Size(125, 13);
			OuterAirLabel.Text = "Outer Air Filter";
			OuterAirLabel.Location = new Point(10,205);
			this.Controls.Add(OuterAirLabel);

			OuterAirBox = new TextBox();
			OuterAirBox.Size = new Size(85, 15);
			OuterAirBox.Location = new Point(140, 205);
			this.Controls.Add(OuterAirBox);
			
			Label CoolLabel = new Label();
			//EOFLabel.Font = f;
			CoolLabel.Size = new Size(125, 13);
			CoolLabel.Text = "Coolant Filter";
			CoolLabel.Location = new Point(10,245);
			this.Controls.Add(CoolLabel);

			CoolBox = new TextBox();
			CoolBox.Size = new Size(85, 15);
			CoolBox.Location = new Point(140, 245);
			this.Controls.Add(CoolBox);
			
			Button SubmitButton = new Button();
			SubmitButton.Text = "Submit";
			SubmitButton.Location = new Point(100,285);
			SubmitButton.Click += new EventHandler(SubmitButtonClicked);
			this.Controls.Add(SubmitButton);
		}
////////////////////////////////////////////////////////////////////			
			private void SubmitButtonClicked(object sender, EventArgs e)
			{
				string q = "INSERT INTO filters VALUES('" +_id;
				if(EngOilBox.Text != "")
					q += "',(SELECT style FROM filter_stock WHERE num = '" + EngOilBox.Text +"'),'1'";
				if(PriFuelBox.Text != "")
					q += "),('" + _id + "',(SELECT style FROM filter_stock WHERE num = '" + PriFuelBox.Text +"'),'2'";
				if(SecFuelBox.Text != "")
					q += "),('" + _id + "',(SELECT style FROM filter_stock WHERE num = '" + SecFuelBox.Text +"'),'3'";
				if(OuterAirBox.Text != "")
					q += "),('" + _id + "',(SELECT style FROM filter_stock WHERE num = '" + OuterAirBox.Text +"'),'4'";
				if(InnerAirBox.Text != "")
					q += "),('" + _id + "',(SELECT style FROM filter_stock WHERE num = '" + InnerAirBox.Text +"'),'5'";
				if(CoolBox.Text != "")
					q += "),('" + _id + "',(SELECT style FROM filter_stock WHERE num = '" + CoolBox.Text +"'),'6'";
				q += ");";
			//	MessageBox.Show(q);
////////////////////////////////////////////////////////////////////////////
/////////// Insert 				
	MySqlConnection _conx = new MySqlConnection(_connectionString);
	_conx.Open();
	if(_conx == null) MessageBox.Show("No Connection");
	MySqlCommand cmd = new MySqlCommand(q, _conx);
		try
		{
		cmd.ExecuteNonQuery();
		} catch (MySqlException	ex)
		{ MessageBox.Show("SQL Exception: " + ex.ToString());
		}
		_conx.Close();
	}//// end method
////////////////////////////////////////////////////////////////
	}////end class





}/////End Namespace
