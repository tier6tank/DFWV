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
    public class PoeticForm : ArtForm
    {


        public PoeticForm(XDocument xdoc, World world)
            : base(xdoc, world)
        {

        }

        public override void Select(MainForm frm)
        {
            if (frm.grpPoeticForm.Text == ToString() && frm.MainTab.SelectedTab == frm.tabPoeticForm)
                return;
            Program.MakeSelected(frm.tabPoeticForm, frm.lstPoeticForm, this);

            frm.grpPoeticForm.Text = ToString();
            frm.grpPoeticForm.Show();
#if DEBUG
            frm.grpPoeticForm.Text += string.Format(" - ID: {0}", Id);
#endif
            frm.lblPoeticFormName.Text = ToString();
            frm.lblPoeticFormAltName.Text = AltName;
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }


}