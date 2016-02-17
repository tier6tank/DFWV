using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DFWV.WorldClasses;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using Region = DFWV.WorldClasses.Region;

namespace DFWV
{
    internal partial class FilterForm : Form
    {
        readonly Dictionary<string, Type> _fields;
        readonly Dictionary<string, IEnumerable<string>> _options;

        readonly string[] _ops = { "=", "!=", ">", "<", ">=", "<=", "Contains", "Doesn't Contain", "true", "false" };

        readonly string[] _intOps = { "=", "!=", ">", "<", ">=", "<="};
        readonly string[] _stringOps = { "=", "!=", ">", "<", ">=", "<=", "Contains", "Doesn't Contain"};
        readonly string[] _boolOps = { "true", "false" };
        // string[] listOps = { "=", "!=" };

        public Filter OutFilter;


        public FilterForm(World world, Type filterType)
        {

#if DEBUG
            //TestAllFilters(world);
#endif

            InitializeComponent();


            cmbWhereOperation.Items.AddRange(_ops.ToArray<object>());
            cmbOrderOperation.Items.AddRange(_ops.ToArray<object>());

            _fields = world.Filters.Fields[filterType];
            cmbWhereField.Items.AddRange(_fields.Keys.ToArray<object>());
            cmbOrderField.Items.AddRange(_fields.Keys.ToArray<object>());

            if (world.Filters.Options.ContainsKey(filterType))
            {
                _options = world.Filters.Options[filterType];

                foreach (var option in _options.Where(option => option.Value.Any()))
                {
                    cmbWhereField.Items.Add(option.Key);
                    cmbOrderField.Items.Add(option.Key);

                    cmbGroupField.Items.Add(option.Key);
                }
            }
            else
            {
                _options = null;
            }

            lstWhere.Items.AddRange(world.Filters[filterType].Where.ToArray<object>());
            lstOrder.Items.AddRange(world.Filters[filterType].OrderBy.ToArray<object>());
            lstGroup.Items.AddRange(world.Filters[filterType].GroupBy.ToArray<object>());

            chkTake.Checked = world.Filters[filterType].Take != -1;
            txtTake.Text = world.Filters[filterType].Take != -1 ? world.Filters[filterType].Take.ToString() : "";
        }

#if DEBUG
        private void TestAllFilters(World world)
        {
 	        var testSettings = new FilterSettings(world);
            foreach (var thisType in testSettings.Fields.Keys)
            {

                var testFilter = new Filter();


                foreach (var filterItem in testSettings.Fields[thisType])
                {
                    if (filterItem.Value == typeof(int))
                        testFilter.Where.Add(filterItem.Key + " == 0");
                    else if (filterItem.Value == typeof(bool))
                        testFilter.Where.Add(filterItem.Key + " == true");
                    else if (filterItem.Value == typeof(string))
                        testFilter.Where.Add(filterItem.Key + " == \"test\"");
                }

                if (testSettings.Options.ContainsKey(thisType))
                {
                    foreach (var filterItem in testSettings.Options[thisType].Where(filterItem => filterItem.Value.Any()))
                    {
                        testFilter.Where.Add(filterItem.Key + " == \"" + filterItem.Value.ToList()[0] + "\"");
                    }
                }

                // ReSharper disable ReturnValueOfPureMethodIsNotUsed
                if (thisType == typeof(Civilization))
                    testFilter.Get(world.Civilizations).ToArray();
                else if (thisType == typeof(God))
                    testFilter.Get(world.Gods).ToArray();
                else if (thisType == typeof(Leader))
                    testFilter.Get(world.Leaders).ToArray();
                else if (thisType == typeof(Parameter))
                    testFilter.Get(world.Parameters).ToArray();
                else if (thisType == typeof(Race))
                    testFilter.Get(world.Races.Values.ToList()).ToArray();
                else if (thisType == typeof(Artifact))
                    testFilter.Get(world.Artifacts.Values.ToList()).ToArray();
                else if (thisType == typeof(Entity))
                    testFilter.Get(world.Entities.Values.ToList()).ToArray();
                else if (thisType == typeof(EntityPopulation))
                    testFilter.Get(world.EntityPopulations.Values.ToList()).ToArray();
                else if (thisType == typeof(HistoricalEra))
                    testFilter.Get(world.HistoricalEras.Values.ToList()).ToArray();
                else if (thisType == typeof(HistoricalEvent))
                    testFilter.Get(world.HistoricalEvents.Values.ToList()).ToArray();
                else if (thisType == typeof(HistoricalEventCollection))
                    testFilter.Get(world.HistoricalEventCollections.Values.ToList()).ToArray();
                else if (thisType == typeof(HistoricalFigure))
                    testFilter.Get(world.HistoricalFigures.Values.ToList()).ToArray();
                else if (thisType == typeof(Region))
                    testFilter.Get(world.Regions.Values.ToList()).ToArray();
                else if (thisType == typeof(UndergroundRegion))
                    testFilter.Get(world.UndergroundRegions.Values.ToList()).ToArray();
                else if (thisType == typeof (Site))
                    testFilter.Get(world.Sites.Values.ToList()).ToArray();
                else if (thisType == typeof (Structure))
                    testFilter.Get(world.Structures.Values.ToList()).ToArray();
                else if (thisType == typeof (WorldConstruction))
                    testFilter.Get(world.WorldConstructions.Values.ToList()).ToArray();
                else if (thisType == typeof (Dynasty))
                    testFilter.Get(world.Dynasties).ToArray();
                else if (thisType == typeof (River))
                    testFilter.Get(world.Rivers.Values.ToList()).ToArray();
                else if (thisType == typeof (Mountain))
                    testFilter.Get(world.Mountains.Values.ToList()).ToArray();
                else if (thisType == typeof (Army))
                    testFilter.Get(world.Armies.Values.ToList()).ToArray();
                else if (thisType == typeof (Unit))
                    testFilter.Get(world.Units.Values.ToList()).ToArray();
                else if (thisType == typeof (Engraving))
                    testFilter.Get(world.Engravings.Values.ToList()).ToArray();
                else if (thisType == typeof (Report))
                    testFilter.Get(world.Reports.Values.ToList()).ToArray();
                else if (thisType == typeof (Building))
                    testFilter.Get(world.Buildings.Values.ToList()).ToArray();
                else if (thisType == typeof (Construction))
                    testFilter.Get(world.Constructions.Values.ToList()).ToArray();
                else if (thisType == typeof (Item))
                    testFilter.Get(world.Items.Values.ToList()).ToArray();
                else if (thisType == typeof (Plant))
                    testFilter.Get(world.Plants.Values.ToList()).ToArray();
                else if (thisType == typeof (WorldClasses.Squad))
                    testFilter.Get(world.Squads.Values.ToList()).ToArray();
                else if (thisType == typeof (WrittenContent))
                    testFilter.Get(world.WrittenContents.Values.ToList()).ToArray();
                else if (thisType == typeof (MusicalForm))
                    testFilter.Get(world.MusicalForms.Values.ToList()).ToArray();
                else if (thisType == typeof (DanceForm))
                    testFilter.Get(world.DanceForms.Values.ToList()).ToArray();
                else if (thisType == typeof (PoeticForm))
                    testFilter.Get(world.PoeticForms.Values.ToList()).ToArray();
                else
                {
                    Console.WriteLine("else if (thisType == typeof(" + thisType.Name + "))");
                    Console.WriteLine("\ttestFilter.Get(world." + thisType.Name + "s.Values.ToList()).ToArray():");
                }


                //public readonly Dictionary<int, Army> Armies = new Dictionary<int, Army>();
                //public readonly Dictionary<int, Unit> Units = new Dictionary<int, Unit>();
                //public readonly Dictionary<int, Engraving> Engravings = new Dictionary<int, Engraving>();
                //public readonly Dictionary<int, Report> Reports = new Dictionary<int, Report>();
                //public readonly Dictionary<int, Building> Buildings = new Dictionary<int, Building>();
                //public readonly Dictionary<int, Construction> Constructions = new Dictionary<int, Construction>();
                //public readonly Dictionary<int, Item> Items = new Dictionary<int, Item>();
                //public readonly Dictionary<int, Plant> Plants = new Dictionary<int, Plant>();
                //public readonly Dictionary<int, Squad> Squads = new Dictionary<int, Squad>();
                //public readonly Dictionary<int, Race> Races = new Dictionary<int, Race>();
                //public readonly Dictionary<int, WrittenContent> WrittenContents = new Dictionary<int, WrittenContent>();
                //public readonly Dictionary<int, PoeticForm> PoeticForms = new Dictionary<int, PoeticForm>();
                //public readonly Dictionary<int, MusicalForm> MusicalForms = new Dictionary<int, MusicalForm>();
                //public readonly Dictionary<int, DanceForm> DanceForms = new Dictionary<int, DanceForm>();
                //public readonly Dictionary<int, Mountain> Mountains = new Dictionary<int, Mountain>();
                //public readonly Dictionary<int, River> Rivers = new Dictionary<int, River>();
                //public readonly Dictionary<int, Structure> Structures = new Dictionary<int, Structure>();
                //public readonly Dictionary<int, WorldConstruction> WorldConstructions = new Dictionary<int, WorldConstruction>();


                //public readonly Dictionary<int, Site> SitesFile = new Dictionary<int, Site>();
                //public readonly List<Entity> EntitiesFile = new List<Entity>();
                //public readonly List<Dynasty> Dynasties = new List<Dynasty>();

                // ReSharper restore ReturnValueOfPureMethodIsNotUsed

            }

        }
#endif

        private void chkTake_CheckedChanged(object sender, EventArgs e)
        {
            txtTake.Enabled = chkTake.Checked;
        }

        private void cmbWhereField_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = ((ComboBox)sender).SelectedItem.ToString();
            cmbWhereOperation.Items.Clear();
            if (_fields.ContainsKey(selected))
            {
                if (_fields[selected] == typeof(bool))
                {
                    cmbWhereOperation.Items.AddRange(_boolOps.ToArray<object>());
                    cmbWhereOperation.SelectedIndex = 0;
                    txtWhereData.Visible = false;
                }
                else if (_fields[selected] == typeof(int))
                {
                    cmbWhereOperation.Items.AddRange(_intOps.ToArray<object>());
                    txtWhereData.Visible = true;
                }
                else if (_fields[selected] == typeof(string))
                {
                    cmbWhereOperation.Items.AddRange(_stringOps.ToArray<object>());
                    txtWhereData.Visible = true;
                }
                optionIsNotRadioButton.Visible = false;
                optionIsRadioButton.Visible = false;
            }
            else
            {
                var addRange = _options[selected].ToList();
                addRange.Sort();          
                cmbWhereOperation.Items.AddRange(addRange.ToArray<object>());
                txtWhereData.Visible = false;
                optionIsNotRadioButton.Visible = true;
                optionIsRadioButton.Visible = true;
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
            
            var selected = cmbWhereField.SelectedItem.ToString();
            var thisOp = cmbWhereOperation.SelectedItem.ToString();
            var dataInt = 0;
            var data = "";

            if (_fields.ContainsKey(selected))
            {
                if (_fields[selected] == typeof(int))
                {
                    if (txtWhereData.Text == "")
                    {
                        WhereAdd.BackColor = Color.Red;
                        return;
                    }
                    if (!int.TryParse(txtWhereData.Text, out dataInt))
                    {
                        WhereAdd.BackColor = Color.Red;
                        return;
                    }
                }
                else if (_fields[selected] == typeof(string))
                {
                    if (thisOp.Contains("Contain") && txtWhereData.Text == "")
                    {
                        WhereAdd.BackColor = Color.Red;
                        return;
                    }
                    data = txtWhereData.Text;
                }
            }
            var addWhere = "";

            if (_fields.ContainsKey(selected))
            {
                if (_fields[selected] == typeof(int))
                    addWhere = selected + " " + thisOp + dataInt;
                else if (_fields[selected] == typeof(bool))
                    addWhere = (thisOp == "true" ? "" : "!") + selected;
                else if (_fields[selected] == typeof(string))
                {
                    switch (thisOp)
                    {
                        case "Contains":
                            addWhere = selected + ".Contains(\"" + data + "\")";
                            break;
                        case "Doesn't Contain":
                            addWhere = "!" + selected + ".Contains(\"" + data + "\")";
                            break;
                        default:
                            addWhere = selected + " " + thisOp + " \"" + data + "\"";
                            break;
                    }
                }
            }
            else
            {
                if (optionIsRadioButton.Checked)
                    addWhere = selected + " == \"" + thisOp + "\"";
                else
                    addWhere = selected + " != \"" + thisOp + "\"";

            }

            if (lstWhere.Items.Contains(addWhere) || addWhere == "")
            {
                WhereAdd.BackColor = Color.Red;
                return;
            }
            lstWhere.Items.Add(addWhere);
            WhereAdd.BackColor = DefaultBackColor;
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
            var selected = cmbOrderField.SelectedItem.ToString();
            var thisOp = cmbOrderOperation.SelectedItem?.ToString();
            var dataInt = 0;
            var data = "";

            if (_fields.ContainsKey(selected))
            {
                if (_fields[selected] == typeof (int))
                {
                    if (txtOrderData.Text == "" && cmbOrderOperation.SelectedIndex != -1)
                    {
                        OrderAdd.BackColor = Color.Red;
                        return;
                    }
                    if (!int.TryParse(txtOrderData.Text, out dataInt) && cmbOrderOperation.SelectedIndex != -1)
                    {
                        OrderAdd.BackColor = Color.Red;
                        return;
                    }
                }
                else if (_fields[selected] == typeof (string))
                {
                    if (thisOp != null && thisOp.Contains("Contain") && txtOrderData.Text == "")
                    {
                        OrderAdd.BackColor = Color.Red;
                        return;
                    }
                    data = txtOrderData.Text;
                }
            }
            var addOrder = "";

            if (thisOp == null)
               addOrder = selected;
            else
            {
                if (_fields.ContainsKey(selected))
                {
                    if (_fields[selected] == typeof (int))
                        addOrder = selected + " " + thisOp + dataInt;
                    else if (_fields[selected] == typeof (bool))
                        addOrder = (thisOp == "true" ? "" : "!") + selected;
                    else if (_fields[selected] == typeof (string))
                    {
                        switch (thisOp)
                        {
                            case "Contains":
                                addOrder = selected + ".Contains(\"" + data + "\")";
                                break;
                            case "Doesn't Contain":
                                addOrder = "!" + selected + ".Contains(\"" + data + "\")";
                                break;
                            default:
                                addOrder = selected + " " + thisOp + " \"" + data + "\"";
                                break;
                        }
                    }
                }
                else
                {
                    addOrder = selected + " = \"" + thisOp + "\"";
                }
            }
            if (addOrder != "" && chkDescending.Checked)
                addOrder = "-" + addOrder;

            if (lstOrder.Items.Contains(addOrder) || addOrder == "")
            {
                OrderAdd.BackColor = Color.Red;
                return;
            }
            lstOrder.Items.Add(addOrder);
            OrderAdd.BackColor = DefaultBackColor;
        }

        private void cmbOrderField_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = ((ComboBox)sender).SelectedItem.ToString();
            cmbOrderOperation.Items.Clear();
            if (_fields.ContainsKey(selected))
            {
                if (_fields[selected] == typeof (bool))
                {
                    cmbOrderOperation.Items.AddRange(_boolOps.ToArray<object>());
                    txtOrderData.Visible = false;
                }
                else if (_fields[selected] == typeof (int))
                {
                    cmbOrderOperation.Items.AddRange(_intOps.ToArray<object>());
                    txtOrderData.Visible = true;
                }
                else if (_fields[selected] == typeof (string))
                {
                    cmbOrderOperation.Items.AddRange(_stringOps.ToArray<object>());
                    txtOrderData.Visible = true;
                }
            }
            else
            {
                var addRange = _options[selected].ToList();
                addRange.Sort();
                cmbOrderOperation.Items.AddRange(addRange.ToArray<object>());
                txtOrderData.Visible = false;
            }
            txtOrderData.Text = "";
        }

        private void OrderMoveUp_Click(object sender, EventArgs e)
        {
            if (lstOrder.SelectedItem == null || lstOrder.SelectedIndex == 0) return;
            var curIndex = lstOrder.SelectedIndex;
            var curItem = lstOrder.SelectedItem.ToString();
            lstOrder.Items.Remove(lstOrder.SelectedItem);
            lstOrder.Items.Insert(curIndex-1,curItem);
            lstOrder.SelectedItem = curItem;
        }

        private void OrderMoveDown_Click(object sender, EventArgs e)
        {
            if (lstOrder.SelectedItem == null || lstOrder.SelectedIndex == lstOrder.Items.Count - 1) return;
            var curIndex = lstOrder.SelectedIndex;
            var curItem = lstOrder.SelectedItem.ToString();
            lstOrder.Items.Remove(lstOrder.SelectedItem);
            lstOrder.Items.Insert(curIndex + 1, curItem);
            lstOrder.SelectedItem = curItem;
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            List<string> wheres = null;
            List<string> orders = null;
            List<string> groups = null;

            if (lstWhere.Items.Count > 0)
                wheres = lstWhere.Items.Cast<string>().ToList();
            if (lstOrder.Items.Count > 0)
                orders = lstOrder.Items.Cast<string>().ToList();
            if (lstGroup.Items.Count > 0)
                groups = lstGroup.Items.Cast<string>().ToList();

            int takeI;
            if (!(chkTake.Checked && int.TryParse(txtTake.Text,out takeI)))
                takeI = -1;

            OutFilter = new Filter(orders, wheres, groups, takeI);
        }

        private void GroupAdd_Click(object sender, EventArgs e)
        {
            if (cmbGroupField.SelectedIndex == -1)
            {
                GroupAdd.BackColor = Color.Red;
                return;
            }
            var addGroup = cmbGroupField.SelectedItem.ToString();

            if (lstGroup.Items.Contains(addGroup) || addGroup == "")
            {
                GroupAdd.BackColor = Color.Red;
                return;
            }
            lstGroup.Items.Add(addGroup);
            GroupAdd.BackColor = DefaultBackColor;
        }

        private void GroupMoveUp_Click(object sender, EventArgs e)
        {
            if (lstGroup.SelectedItem == null || lstGroup.SelectedIndex == 0) return;
            var curIndex = lstGroup.SelectedIndex;
            var curItem = lstGroup.SelectedItem.ToString();
            lstGroup.Items.Remove(lstGroup.SelectedItem);
            lstGroup.Items.Insert(curIndex - 1, curItem);
            lstGroup.SelectedItem = curItem;
        }

        private void GroupMoveDown_Click(object sender, EventArgs e)
        {
            if (lstGroup.SelectedItem == null || lstGroup.SelectedIndex == lstGroup.Items.Count - 1) return;
            var curIndex = lstGroup.SelectedIndex;
            var curItem = lstGroup.SelectedItem.ToString();
            lstGroup.Items.Remove(lstGroup.SelectedItem);
            lstGroup.Items.Insert(curIndex + 1, curItem);
            lstGroup.SelectedItem = curItem;
        }

        private void GroupDelete_Click(object sender, EventArgs e)
        {
            if (lstGroup.SelectedItem != null)
                lstGroup.Items.Remove(lstGroup.SelectedItem);
        }

        private void Help_Click(object sender, EventArgs e)
        {
            Height = Height < 500 ? 570 : 300;
        }

        private void FilterForm_Load(object sender, EventArgs e)
        {
            Height = 300;
        }
    }
}
