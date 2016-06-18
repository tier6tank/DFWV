using System.Xml.Linq;

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
            frm.grpMusicalForm.Text += $" - ID: {Id}";
#endif
            frm.lblMusicalFormName.Text = ToString();
            frm.lblMusicalFormAltName.Text = AltName;
        }

    }


}