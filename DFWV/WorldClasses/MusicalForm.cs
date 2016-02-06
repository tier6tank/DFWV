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
    public class MusicalForm : ArtForm
    {


        public MusicalForm(XDocument xdoc, World world)
            : base(xdoc, world)
        {

        }

        public override void Select(MainForm frm)
        {
            if (frm.grpMusicalForm.Text == ToString() && frm.MainTab.SelectedTab == frm.tabMusicalForm)
                return;
            Program.MakeSelected(frm.tabMusicalForm, frm.lstMusicalForm, this);

            frm.grpMusicalForm.Text = ToString();
            frm.grpMusicalForm.Show();
#if DEBUG
            frm.grpMusicalForm.Text += string.Format(" - ID: {0}", Id);
#endif
            frm.lblMusicalFormName.Text = ToString();
            frm.lblMusicalFormAltName.Text = AltName;
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }


}