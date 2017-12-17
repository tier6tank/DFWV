using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DFWV.WorldClasses;

namespace DFWV
{
    public partial class StatsForm : Form
    {
        private readonly World _world;

        internal StatsForm(World world)
        {
            InitializeComponent();
            _world = world;

            DisplayStats();
        }

        private void DisplayStats()
        {
            DisplayHfPopulationChart();
            DisplaySiteCountChart();
        }

        private void DisplaySiteCountChart()
        {
            var yValues = _world.Stats.SitesInYear.Values.ToArray();
            var xValues = _world.Stats.SitesInYear.Keys.ToArray();

            if (!xValues.Any())
                return;

            var yAxis = new Axis(SiteCountChart.ChartAreas[0], AxisName.Y)
            {
                IsStartedFromZero = true,
                Title = "Site Count",
                Name = "Site Count"
            };
            var xAxis = new Axis(SiteCountChart.ChartAreas[0], AxisName.X) {Title = "Year"};

            SiteCountChart.ChartAreas[0].AxisX = xAxis;
            SiteCountChart.ChartAreas[0].AxisY = yAxis;

            // Bind the data to the chart
            SiteCountChart.Series["Default"].Points.DataBindXY(xValues, yValues);
            SiteCountChart.ChartAreas[0].AxisX.Interval = Math.Round((xValues[xValues.Length - 1] - xValues[0]) / 100.0, 0) * 10.0;
            SiteCountChart.ChartAreas[0].AxisX.IntervalOffset = -xValues[0] %
                SiteCountChart.ChartAreas[0].AxisX.Interval + 1;
        }

        private void DisplayHfPopulationChart()
        {
            var yValues = _world.Stats.HfAliveInYear.Values.ToArray();
            var xValues = _world.Stats.HfAliveInYear.Keys.ToArray();

            var yAxis = new Axis(HFPopulationChart.ChartAreas[0], AxisName.Y)
            {
                IsStartedFromZero = true,
                Title = "Population",
                Name = "Population"
            };
            var xAxis = new Axis(HFPopulationChart.ChartAreas[0], AxisName.X) {Title = "Year"};

            HFPopulationChart.ChartAreas[0].AxisX = xAxis;
            HFPopulationChart.ChartAreas[0].AxisY = yAxis;

            // Bind the data to the chart
            HFPopulationChart.Series["Default"].Points.DataBindXY(xValues, yValues);
            HFPopulationChart.ChartAreas[0].AxisX.Interval = Math.Round((xValues[xValues.Length - 1] - xValues[0]) / 100.0, 0) * 10.0;
            HFPopulationChart.ChartAreas[0].AxisX.IntervalOffset = -xValues[0] %
                HFPopulationChart.ChartAreas[0].AxisX.Interval + 1;
        }

        private void StatsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
