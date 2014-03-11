using DFWV.WorldClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DFWV
{
    public partial class StatsForm : Form
    {
        private World World;

        internal StatsForm(World world)
        {
            InitializeComponent();
            World = world;

            DisplayStats();
        }

        private void DisplayStats()
        {
            DisplayHFPopulationChart();
            DisplaySiteCountChart();
        }

        private void DisplaySiteCountChart()
        {
            int[] yValues = World.Stats.SitesInYear.Values.ToArray<int>();
            int[] xValues = World.Stats.SitesInYear.Keys.ToArray<int>();

            Axis yAxis = new Axis(SiteCountChart.ChartAreas[0], AxisName.Y);
            yAxis.IsStartedFromZero = true;
            yAxis.Title = "Site Count";
            yAxis.Name = "Site Count";
            Axis xAxis = new Axis(SiteCountChart.ChartAreas[0], AxisName.X);
            xAxis.Title = "Year";

            // Bind the data to the chart
            SiteCountChart.Series["Default"].Points.DataBindXY(xValues, yValues);
            SiteCountChart.ChartAreas[0].AxisX.Interval = Math.Round((xValues[xValues.Length - 1] - xValues[0]) / 100.0, 0) * 10.0;
            SiteCountChart.ChartAreas[0].AxisX.IntervalOffset = -xValues[0] %
                SiteCountChart.ChartAreas[0].AxisX.Interval + 1;
        }

        private void DisplayHFPopulationChart()
        {
            int[] yValues = World.Stats.HFAliveInYear.Values.ToArray<int>();
            int[] xValues = World.Stats.HFAliveInYear.Keys.ToArray<int>();

            Axis yAxis = new Axis(HFPopulationChart.ChartAreas[0], AxisName.Y);
            yAxis.IsStartedFromZero = true;
            yAxis.Title = "Population";
            yAxis.Name = "Population";
            Axis xAxis = new Axis(HFPopulationChart.ChartAreas[0], AxisName.X);
            xAxis.Title = "Year";

            // Bind the data to the chart
            HFPopulationChart.Series["Default"].Points.DataBindXY(xValues, yValues);
            HFPopulationChart.ChartAreas[0].AxisX.Interval = Math.Round((xValues[xValues.Length - 1] - xValues[0]) / 100.0, 0) * 10.0;
            HFPopulationChart.ChartAreas[0].AxisX.IntervalOffset = -xValues[0] %
                HFPopulationChart.ChartAreas[0].AxisX.Interval + 1;
        }
    }
}
