using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class DanceForm : ArtForm
    {


        public DanceForm(XDocument xdoc, World world)
            : base(xdoc, world)
        {

        }

        public override void Select(MainForm frm)
        {
            if (frm.grpDanceForm.Text == ToString() && frm.MainTab.SelectedTab == frm.tabDanceForm)
                return;
            Program.MakeSelected(frm.tabDanceForm, frm.lstDanceForm, this);

            frm.grpDanceForm.Text = ToString();
            frm.grpDanceForm.Show();
#if DEBUG
            frm.grpDanceForm.Text += string.Format(" - ID: {0}", Id);
#endif
            frm.lblDanceFormName.Text = ToString();
            frm.lblDanceFormAltName.Text = AltName;
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }


}