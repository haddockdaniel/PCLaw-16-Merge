using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace PCLaw_16_Merge
{
    public partial class ProgressForm : Form
    {
        public ProgressForm(string server, string primarydb, string secondarydb, bool tol, bool resp, List<string> IDs)
        {
            InitializeComponent();
            connectionString = "Data Source=" + server + ";Initial Catalog=" + primarydb + "; Integrated Security=SSPI;";
            origConnString = "Data Source=" + server + ";Initial Catalog=" + secondarydb + "; Integrated Security=SSPI;";
            if (IDs != null)
                ids = IDs.ToList();
            typeOfLaw = tol;
            respAtty = resp;
            secondaryDB = secondarydb;
            primaryDB = primarydb;
            doSetUp();

        }

        string connectionString = "";
        string origConnString = "";
        string IDsToKeep = "";
        List<string> ids = new List<string>();
        bool typeOfLaw;
        bool respAtty;
        public List<ConversionObject> convObjets = new List<ConversionObject>();
        public ConversionControl Conversion;
        string secondaryDB = "";
        string primaryDB = "";

        private void doSetUp()
        {
            foreach (string a in ids)
            {
                IDsToKeep = IDsToKeep + a + ",";

            }
            IDsToKeep = IDsToKeep.TrimEnd(',');
            convObjets = getCObjects().ToList();
            progressBarTotal.Value = 0;
            progressBarTotal.Maximum = convObjets.Count;
            ConversionControl demoConverter = new ConversionControl(origConnString, connectionString);
            backgroundWorker1.RunWorkerAsync(demoConverter);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to cancel? If so, the process" + "\r\n" + "will wait until the current item is finished processing" + "\r\n" + "to preserve your data integrity", "Cancel?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
                backgroundWorker1.CancelAsync();
        }

        private void buttonOpenLog_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Conversion = e.Argument as ConversionControl;
            int current = 0;
            BackgroundWorker worker = sender as BackgroundWorker;

            string errors = "";
            foreach (ConversionObject co in convObjets)
            {
                try
                {
                    List<Label> tempList = tabControl1.TabPages[0].Controls.OfType<Label>().ToList();
                    // MessageBox.Show(tempList.Count.ToString());
                    Label tempLabel = null;
                    foreach (Label c in tempList)
                    {
                        if (c.Name == "label" + co.method)
                        {
                            c.ForeColor = Color.Red;
                            tempLabel = c;
                            break;
                        }
                    }


                    Type type = typeof(ConversionControl);
                    MethodInfo info = type.GetMethod("Merge" + co.method);
                    object[] parametersArray = new object[] { IDsToKeep, respAtty, typeOfLaw, connectionString, secondaryDB, primaryDB };

                    try
                    {
                        var output = info.Invoke(Conversion, parametersArray);
                        errors = errors + co.method;
                        errors = errors + "\r\n" + output.ToString() + "\r\n" + "Done!";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Message: " + ex.Message + " : Error: " + errors);

                    }
                    current++;
                    backgroundWorker1.ReportProgress(current);
                    tempLabel.ForeColor = Color.Green;
                }
                catch (Exception ex4)
                { MessageBox.Show("Error: " + ex4.Message); }
            }
            richTextBox1.Text = errors;
            MessageBox.Show("Done! Check the Error Log tab for details.");
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarTotal.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private List<ConversionObject> getCObjects()
        {
            List<ConversionObject> clist = new List<ConversionObject>();
            ConversionObject cobj;
            cobj = new ConversionObject();
            cobj.method = "Lawyers";
            cobj.order = 1;
            cobj.runMethod = true;
            clist.Add(cobj);
            cobj = new ConversionObject();
            cobj.method = "Rates";
            cobj.order = 2;
            cobj.runMethod = true;
            clist.Add(cobj);
            cobj = new ConversionObject();
            cobj.method = "Users";
            cobj.order = 3;
            cobj.runMethod = true;
            clist.Add(cobj);
            cobj = new ConversionObject();
            cobj.method = "TypesOfLaw";
            cobj.order = 4;
            cobj.runMethod = true;
            clist.Add(cobj);
            cobj = new ConversionObject();
            cobj.method = "Clients";
            cobj.order = 5;
            cobj.runMethod = true;
            clist.Add(cobj);
            cobj = new ConversionObject();
            cobj.method = "Matters";
            cobj.order = 6;
            cobj.runMethod = true;
            clist.Add(cobj);

            clist.Sort((x, y) => x.order.CompareTo(y.order));
            return clist;
        }
    }
}
