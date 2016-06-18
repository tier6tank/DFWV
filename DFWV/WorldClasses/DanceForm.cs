using System.Xml.Linq;

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
            frm.grpDanceForm.Text += $" - ID: {Id}";
#endif
            frm.lblDanceFormName.Text = ToString();
            frm.lblDanceFormAltName.Text = AltName;
        }
    }


}