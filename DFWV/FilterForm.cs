using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DFWV.WorldClasses;

namespace DFWV
{
    internal partial class FilterForm : Form
    {
        private Type Type;
        World World;
        Dictionary<string, Type> Fields = new Dictionary<string, Type>();
        Dictionary<string, IEnumerable<string>> Options = new Dictionary<string, IEnumerable<string>>();

        string[] Ops = new string[] { "=", "!=", ">", "<", ">=", "<=", "Contains", "Doesn't Contain", "true", "false" };
        
        string[] intOps = new string[] { "=", "!=", ">", "<", ">=", "<=",};
        string[] stringOps = new string[] { "=", "!=", ">", "<", ">=", "<=", "Contains", "Doesn't Contain"};
        string[] boolOps = new string[] { "true", "false" };
        string[] listOps = new string[] { "=", "!=" };

        public Filter outFilter;

        public FilterForm(World world, Type type)
        {
            InitializeComponent();
            cmbWhereOperation.Items.AddRange(Ops);
            cmbOrderOperation.Items.AddRange(Ops);

            Type = type;
            World = world;
            Fields = World.Filters.Fields[Type];
            Options = World.Filters.Options[Type];
            cmbWhereField.Items.AddRange(Fields.Keys.ToArray());
            cmbWhereField.Items.AddRange(Options.Keys.ToArray());

            cmbOrderField.Items.AddRange(Fields.Keys.ToArray());
            cmbOrderField.Items.AddRange(Options.Keys.ToArray());

            lstWhere.Items.AddRange(World.Filters[Type].Where.ToArray());
            lstOrder.Items.AddRange(World.Filters[Type].OrderBy.ToArray());

            chkTake.Checked = World.Filters[Type].Take != -1;
            txtTake.Text = World.Filters[Type].Take != -1 ? World.Filters[Type].Take.ToString() : "";
        }

        private void cmbWhereOperation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkTake_CheckedChanged(object sender, EventArgs e)
        {
            txtTake.Enabled = chkTake.Checked;
        }

        private void FilterForm_Load(object sender, EventArgs e)
        {

        }

        private void cmbWhereField_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = ((ComboBox)sender).SelectedItem.ToString();
            cmbWhereOperation.Items.Clear();
            if (Fields.ContainsKey(selected))
            {
                if (Fields[selected] == typeof(bool))
                {
                    cmbWhereOperation.Items.AddRange(boolOps);
                    cmbWhereOperation.SelectedIndex = 0;
                    txtWhereData.Visible = false;
                }
                else if (Fields[selected] == typeof(int))
                {
                    cmbWhereOperation.Items.AddRange(intOps);
                    txtWhereData.Visible = true;
                }
                else if (Fields[selected] == typeof(string))
                {
                    cmbWhereOperation.Items.AddRange(stringOps);
                    txtWhereData.Visible = true;
                }
                
            }
            else
            {
                var addRange = Options[selected].ToList();
                addRange.Sort();                
                cmbWhereOperation.Items.AddRange(addRange.ToArray());
                txtWhereData.Visible = false;
            }
            txtWhereData.Text = "";
        }

        private void WhereAdd_Click(object sender, EventArgs e)
        {
            if (cmbWhereOperation.SelectedIndex == -1 ||
                cmbWhereField.SelectedIndex == -1)
            {
                WhereAdd.BackColor = Color.Red;
                return;
            }
            string selected = cmbWhereField.SelectedItem.ToString();
            string thisOp = cmbWhereOperation.SelectedItem.ToString();
            int DataInt = 0;
            string Data = "";

            if (Fields.ContainsKey(selected))
            {
                if (Fields[selected] == typeof(int))
                {
                    if (txtWhereData.Text == "")
                    {
                        WhereAdd.BackColor = Color.Red;
                        return;
                    }
                    else
                    {
                        if (!Int32.TryParse(txtWhereData.Text, out DataInt))
                        {
                            WhereAdd.BackColor = Color.Red;
                            return;
                        }
                    }
                }
                else if (Fields[selected] == typeof(string))
                {
                    if (thisOp.Contains("Contain") && txtWhereData.Text == "")
                    {
                        WhereAdd.BackColor = Color.Red;
                        return;
                    }
                    else
                        Data = txtWhereData.Text;
                }
            }
            string AddWhere = "";

            if (Fields.ContainsKey(selected))
            {
                if (Fields[selected] == typeof(int))
                    AddWhere = selected + " " + thisOp + DataInt.ToString();
                else if (Fields[selected] == typeof(bool))
                    AddWhere = (thisOp == "true" ? "" : "!") + selected;
                else if (Fields[selected] == typeof(string))
                {
                    if (thisOp == "Contains")
                        AddWhere = selected + ".Contains(\"" + Data + "\")";
                    else if (thisOp == "Doesn't Contain")
                        AddWhere = "!" + selected + ".Contains(\"" + Data + "\")";
                    else
                        AddWhere = selected + " " + thisOp + " \"" + Data + "\"";
                }
            }
            else
            {
                AddWhere = selected + " = \"" + thisOp + "\"";
            }
            if (lstWhere.Items.Contains(AddWhere) || AddWhere == "")
            {
                WhereAdd.BackColor = Color.Red;
                return;
            }
            else
                lstWhere.Items.Add(AddWhere);
            WhereAdd.BackColor = Control.DefaultBackColor;
        }

        private void WhereDelete_Click(object sender, EventArgs e)
        {
            if (lstWhere.SelectedItem != null)
                lstWhere.Items.Remove(lstWhere.SelectedItem);
        }

        private void OrderDelete_Click(object sender, EventArgs e)
        {
            if (lstOrder.SelectedItem != null)
                lstOrder.Items.Remove(lstOrder.SelectedItem);
        }

        private void OrderAdd_Click(object sender, EventArgs e)
        {
            if (cmbOrderField.SelectedIndex == -1)
            {
                OrderAdd.BackColor = Color.Red;
                return;
            }
            string selected = cmbOrderField.SelectedItem.ToString();
            string thisOp = cmbOrderOperation.SelectedItem == null ? null : cmbOrderOperation.SelectedItem.ToString();
            int DataInt = 0;
            string Data = "";

            if (Fields[selected] == typeof(int))
            {
                if (txtOrderData.Text == "" && cmbOrderOperation.SelectedIndex != -1)
                {
                    OrderAdd.BackColor = Color.Red;
                    return;
                }
                else
                {
                    if (!Int32.TryParse(txtOrderData.Text, out DataInt) && cmbOrderOperation.SelectedIndex != -1)
                    {
                        OrderAdd.BackColor = Color.Red;
                        return;
                    }
                }
            }
            else if (Fields[selected] == typeof(string))
            {
                if (thisOp != null && thisOp.Contains("Contain") && txtOrderData.Text == "")
                {
                    OrderAdd.BackColor = Color.Red;
                    return;
                }
                else
                    Data = txtOrderData.Text;
            }

            string AddOrder = "";

            if (thisOp == null)
               AddOrder = selected;
            else
            { 
                if (Fields[selected] == typeof(int))
                    AddOrder = selected + " " + thisOp + DataInt.ToString();
                else if (Fields[selected] == typeof(bool))
                    AddOrder = (thisOp == "true" ? "" : "!") + selected;
                else if (Fields[selected] == typeof(string))
                {
                    if (thisOp == "Contains")
                        AddOrder = selected + ".Contains(\"" + Data + "\")";
                    else if (thisOp == "Doesn't Contain")
                        AddOrder = "!" + selected + ".Contains(\"" + Data + "\")";
                    else
                        AddOrder = selected + " " + thisOp + " \"" + Data + "\"";
                }
            }
            if (AddOrder != "" && chkDescending.Checked)
                AddOrder = "-" + AddOrder;

            if (lstOrder.Items.Contains(AddOrder) || AddOrder == "")
            {
                OrderAdd.BackColor = Color.Red;
                return;
            }
            else
                lstOrder.Items.Add(AddOrder);
            OrderAdd.BackColor = Control.DefaultBackColor;
        }

        private void cmbOrderField_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = ((ComboBox)sender).SelectedItem.ToString();
            cmbOrderOperation.Items.Clear();
            if (Fields[selected] == typeof(bool))
            {
                cmbOrderOperation.Items.AddRange(boolOps);
                txtOrderData.Visible = false;
            }
            else if (Fields[selected] == typeof(int))
            {
                cmbOrderOperation.Items.AddRange(intOps);
                txtOrderData.Visible = true;
            }
            else if (Fields[selected] == typeof(string))
            {
                cmbOrderOperation.Items.AddRange(stringOps);
                txtOrderData.Visible = true;
            }
            txtOrderData.Text = "";
        }

        private void OrderMoveUp_Click(object sender, EventArgs e)
        {
            if (lstOrder.SelectedItem != null && lstOrder.SelectedIndex != 0)
            {
                int CurIndex = lstOrder.SelectedIndex;
                string curItem = lstOrder.SelectedItem.ToString();
                lstOrder.Items.Remove(lstOrder.SelectedItem);
                lstOrder.Items.Insert(CurIndex-1,curItem);
                lstOrder.SelectedItem = curItem;
            }
        }

        private void OrderMoveDown_Click(object sender, EventArgs e)
        {
            if (lstOrder.SelectedItem != null && lstOrder.SelectedIndex != lstOrder.Items.Count - 1)
            {
                int CurIndex = lstOrder.SelectedIndex;
                string curItem = lstOrder.SelectedItem.ToString();
                lstOrder.Items.Remove(lstOrder.SelectedItem);
                lstOrder.Items.Insert(CurIndex + 1, curItem);
                lstOrder.SelectedItem = curItem;
            }
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            List<string> Wheres = null;
            List<string> Orders = null;
            if (lstWhere.Items.Count > 0)
            {
                Wheres = new List<string>();
                foreach (string str in lstWhere.Items)
	            {
                    Wheres.Add(str);		 
	            }
            }
            if (lstOrder.Items.Count > 0)
            {
                Orders = new List<string>();
                foreach (string str in lstOrder.Items)
	            {
                    Orders.Add(str);		 
	            }
            }
            int TakeI;
            if (!(chkTake.Checked && Int32.TryParse(txtTake.Text,out TakeI)))
                TakeI = -1;

            outFilter = new Filter(Orders, Wheres, TakeI);
        }
    }
}
