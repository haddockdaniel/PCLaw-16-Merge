using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace PCLaw_16_Merge
{
    public partial class Selections : Form
    {
        public Selections(bool respatt, bool typelaw, string server, string db, string secondarydb)
        {
            InitializeComponent();
            respAtty = respatt;
            typeOfLaw = typelaw;
            Server = server;
            primaryDB = db;
            secondaryDB = secondarydb;

            lawyerSorter = new ListViewColumnSorter();
            listViewLawyer.View = View.Details;
            listViewLawyer.ListViewItemSorter = lawyerSorter;
            listViewLawyer.Columns.Add("Name", 180);
            listViewLawyer.Columns.Add("ID", 35);
            string queryString = "";

            if (respAtty)
                queryString = "SELECT LawyerID, LawInfLawyerName FROM LawInf where LawInfStatus = 0";
            else if (typeOfLaw)
                queryString = "SELECT AreaOfLawID, AreaOfLawName FROM AreaLaw where AreaOfLawStatus = 0";
            connectionString = "Data Source=" + server + ";Initial Catalog=" + secondarydb + "; Integrated Security=SSPI;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if(respAtty)
                            listViewLawyer.Items.Add(new ListViewItem(new string[] { reader["LawInfLawyerName"].ToString().Trim(), reader["LawyerID"].ToString().Trim() }));
                        else
                            listViewLawyer.Items.Add(new ListViewItem(new string[] { reader["AreaOfLawName"].ToString().Trim(), reader["AreaOfLawID"].ToString().Trim() }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting data. Message: " + ex.Message);
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }


        }

        public ListViewColumnSorter lawyerSorter;
        List<string> IDs = new List<string>();
        string connectionString = "";
        bool respAtty;
        bool typeOfLaw;
        string Server = "";
        string primaryDB = "";
        string secondaryDB = "";

        private void listViewLawyer_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lawyerSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lawyerSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                    lawyerSorter.Order = System.Windows.Forms.SortOrder.Descending;
                else
                    lawyerSorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lawyerSorter.SortColumn = e.Column;
                lawyerSorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listViewLawyer.Sort();
        }

        private void buttonClearLawyers_Click(object sender, EventArgs e)
        {
            listViewLawyer.SelectedItems.Clear();
        }

        private void buttonLawyerSelect_Click(object sender, EventArgs e)
        {
            int index = -1;
            ListView.SelectedIndexCollection indexes = this.listViewLawyer.SelectedIndices;
            foreach (int ind in indexes)
            {
                index = int.Parse(this.listViewLawyer.Items[ind].SubItems[0].Text);
                IDs.Add(index.ToString());
            }//end outer foreach

            if (IDs.Count != 0)
            {
                this.Hide();
                var progress = new ProgressForm(Server, primaryDB, secondaryDB, typeOfLaw, respAtty, IDs);
                progress.Closed += (s, args) => this.Close();
                progress.Show();
            }
            else
                MessageBox.Show("You must select at least one item", "Selection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
