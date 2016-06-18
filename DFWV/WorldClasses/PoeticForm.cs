using System.Xml.Linq;

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
            frm.grpPoeticForm.Text += $" - ID: {Id}";
#endif
            frm.lblPoeticFormName.Text = ToString();
            frm.lblPoeticFormAltName.Text = AltName;
        }

    }


}