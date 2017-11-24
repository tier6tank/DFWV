using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    public class ArtForm : XMLObject
    {
        override public Point Location => Point.Empty;
        public string AltName { get; set; }

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();
        public string Description { get; set; }

        public ArtForm(XDocument xdoc, World world)
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
                        break;
                    case "name":
                        Name = val;
                        break;
                    case "altname":
                        AltName = val;
                        break;
                    case "description":
                        Description = val;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
//            if (frm.grpSquad.Text == ToString() && frm.MainTab.SelectedTab == frm.tabSquad)
//                return;
//            Program.MakeSelected(frm.tabSquad, frm.lstSquad, this);

//            frm.grpSquad.Text = ToString();
//            frm.grpSquad.Show();
//#if DEBUG
//            frm.grpSquad.Text += string.Format(" - ID: {0}", Id);
//#endif
//            frm.lblSquadName.Text = ToString();
//            frm.lblSquadAltName.Text = AltName;
//            frm.lblSquadEntity.Data = Entity;
//            frm.lstSquadMembers.Items.Clear();
//            frm.grpSquadMembers.Visible = Members != null && Members.Count > 0;
//            if (Members != null)
//            {
                
//                frm.lstSquadMembers.Items.AddRange(Members.ToArray());
//            }
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id, 
                Name.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

        internal override void Link()
        {
            //if (EntityID.HasValue)
            //    Entity = World.Entities[EntityID.Value];
            //if (MemberIDs != null)
            //{
            //    Members = new List<HistoricalFigure>();
            //    foreach (var memberID in MemberIDs)
            //        Members.Add(World.HistoricalFigures[memberID]);
            //}
        }
    

        internal override void Process()
        {
            
        }

        internal override void Plus(XDocument xdoc)
        {
            
        }

        public override string ToString()
        {
            return Name ?? AltName ?? "Art Form";
        }

    }


}