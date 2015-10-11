using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class Engraving : XMLObject
    {
        private int? ArtistID { get; set; }
        public HistoricalFigure Artist { get; set; }
        private int? SkillRating { get; set; }
        public Point3 Coords { get; set; }
        override public Point Location { get { return Point.Empty; } }
        private int? TileID { get; set; }
        private int? ArtID { get; set; }
        private int? ArtSubID { get; set; }
        private int? Quality { get; set; }
        private string Position { get; set; }
        private bool Hidden { get; set; }

        public Engraving(XDocument xdoc, World world)
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
                    case "artist":
                        ArtistID = valI;
                        break;
                    case "skill_rating":
                        SkillRating = valI;
                        break;
                    case "coords":
                        Coords = new Point3(
                            Convert.ToInt32(val.Split(',')[0]),
                            Convert.ToInt32(val.Split(',')[1]),
                            Convert.ToInt32(val.Split(',')[2]));
                        break;
                    case "tile":
                        TileID = valI;
                        break;
                    case "art_id":
                        ArtID = valI;
                        break;
                    case "art_subid":
                        ArtSubID = valI;
                        break;
                    case "quality":
                        Quality = valI;
                        break;
                    case "location":
                        Position = val;
                        break;
                    case "hidden":
                        Hidden = true;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            try
            {
                //frm.grpEngraving.Text = ToString();
                //frm.grpEngraving.Show();
#if DEBUG
                //frm.grpEngraving.Text += string.Format(" - ID: {0}", ID);
#endif


                //frm.lblEngravingName.Text = ToString();
            }
            finally
            {
                Program.MakeSelected(frm.tabEngraving, frm.lstEngraving, this);
            }
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                ID, 
                Name.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

        internal override void Link()
        {
            if (ArtistID.HasValue)
                Artist = World.HistoricalFigures[ArtistID.Value];
        }

        internal override void Process()
        {
            
        }

        internal override void Plus(XDocument xdoc)
        {
            
        }
    }


}