using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RhodesEquipment
{
	public class AddFilter : System.Windows.Forms.Form
	{
		protected TextBox nt1;
		protected TextBox mt1;
		protected TextBox cmBox;
		static string _connectionString;

		public AddFilter(string connectionString)
		{
						_connectionString = connectionString;

						Text = "Add A Filter";
						Font font = new Font("Verdana", 10);
						ClientSize = new Size(600, 200);
					//	f.Show();
						Label n1 = new Label();
						n1.Size = new Size(90, 15);
						n1.Font = font;
						n1.Location = new Point(10, 10);
						n1.Text = "Number";
						this.Controls.Add(n1);
						nt1 = new TextBox();
						nt1.Size = new Size(100, 15);
					//	nt1.Text = EngOilFilBox.Text;
						nt1.Location = new Point(100, 10);
						this.Controls.Add(nt1);
						Label m1 = new Label();
						m1.Size = new Size(120, 15);
						m1.Location = new Point(265,10);
						m1.Font = font;
						m1.Text = "Manufacturer";
						this.Controls.Add(m1);
						mt1 = new TextBox();
						mt1.Size = new Size(100, 15);
						mt1.Location = new Point(400, 10);
						this.Controls.Add(mt1);
						Label cmLabel = new Label();
						cmLabel.Font = font;
						cmLabel.Size = new Size(140, 50);
						cmLabel.Text = "Interchange With";
						cmLabel.Location = new Point(10, 50);
						this.Controls.Add(cmLabel);
						cmBox = new TextBox();
						cmBox.Size = new Size(100, 15);
						cmBox.Location = new Point(150, 50);
						cmBox.Text = "";
						this.Controls.Add(cmBox);
						Button b = new Button();
						b.Font = font;
						b.Text = "Submit";
						b.Location = new Point(120, 100);
						b.Click += new EventHandler(AddFilterClick);
						this.Controls.Add(b);
		}///end constructor
		void AddFilterClick(object sender, System.EventArgs e)
		{
				//if(_conx == null) MessageBox.Show("Conx is dead");
				MySqlConnection _conx = new MySqlConnection(_connectionString);
				_conx.Open();
				if(_conx == null) MessageBox.Show("No Connection");
			int maxStyle = 0;
			if(cmBox.Text == "") 
			{
				string query = "SELECT style FROM filter_stock WHERE style = (SELECT MAX(style) FROM filter_stock)";
				MySqlCommand sql = new MySqlCommand(query, _conx);
				MySqlDataReader rdr = null;
				rdr = sql.ExecuteReader();
				while(rdr.Read())
				{
					maxStyle = ((int)rdr["style"]);
				}
				rdr.Close();
				maxStyle += 1;
			}


		try
		{
			MySqlCommand sql = new MySqlCommand();
			sql.Connection = _conx;
			if(_conx == null) MessageBox.Show("No Connection");
			sql.CommandText = "INSERT INTO filter_stock VALUES ('',@manu, @num, @style)";
			sql.Prepare();
			sql.Parameters.AddWithValue("@manu",mt1.Text);
			sql.Parameters.AddWithValue("@num",nt1.Text);
			sql.Parameters.AddWithValue("@style",maxStyle);
			//sql.Parameters.AddWithValue("@style,style);
			sql.ExecuteNonQuery();
			sql.Dispose();
		} catch (MySqlException ex)
		{
			MessageBox.Show("Error: " +ex.ToString());
		}
			_conx.Close();

		this.Close();
		}///end Handler
	}/// end class
}///end namespace`
