using System.Xml.Linq;

namespace DFWV.WorldClasses
{
    public class PoeticForm : ArtForm
    {
        public PoeticForm(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value.Trim();
                int valI;
                int.TryParse(val, out valI);
                switch (element.Name.LocalName)
                {
                    case "id":
                    case "description":
                        break;
                        
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
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

            frm.grpPoeticFormDescription.Visible = Description != null;
            if (Description != null)
            {
                frm.txtPoeticFormDescription.Text = Description;
            }
        }

    }


}