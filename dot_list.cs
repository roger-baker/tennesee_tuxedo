using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;

public class DotList : System.Windows.Forms.Form
	{
		//public static MySqlConnection conx;
	DataGridView dataGridView1 = new DataGridView();

		//public DotList( int modifier, MySqlConnection conx)
		public DotList( int modifier, string connectionString)
		{
			Text = "DOT List";
			ClientSize = new System.Drawing.Size(500, 900);
			Font f = new Font("Verdana", 10);
			Button bn = new Button();
			bn.Text = "Number";
			bn.Location = new Point(100, 850);
			bn.Click += new EventHandler(bn_button_Click);

			string equip;
			string date;
			string query;
			string[] mod = {"num", "date"};
			int y = 10;
			string expired = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
			//MessageBox.Show(expired);

			query = "SELECT a.num, a.make, a.model, DATE_FORMAT(b.date, '%Y-%m-%d') as date from equip a join dot b on a.id = b.num WHERE date < '" + expired + "' && loc = '1' order by  " + mod[modifier] + ";";
		//		MySqlConnection _conx;
/*MySqlConnection _conx = new MySqlConnection(
			"Server=localhost;" +
			"Database=equip;" +
			"User ID=shop;" +
			"Password=shop;" +
			"Pooling=false"); */   
MySqlConnection _conx = new MySqlConnection(connectionString);
		//	_conx = conx;
			MySqlDataAdapter MyDA = new MySqlDataAdapter();
			MyDA.SelectCommand = new MySqlCommand(query, _conx);
			DataTable table = new DataTable();
			MyDA.Fill(table);
			BindingSource bs = new BindingSource();
			bs.DataSource = table;
			dataGridView1.DataSource = bs;
			dataGridView1.Size = new Size(500, 800);
			this.Controls.Add(dataGridView1);
			this.Controls.Add(bn);
			_conx.Close();
			
			
			}
/////////////////////////////////////////////////////////////
		void bn_button_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("Closing?");
		}
	}
