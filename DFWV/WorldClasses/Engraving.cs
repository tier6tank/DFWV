using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class Engraving : XMLObject
    {
        private int? ArtistID { get; }
        public HistoricalFigure Artist { get; set; }
        private int? SkillRating { get; }
        public Point3 Coords { get; set; }
        override public Point Location => Point.Empty;
        private int? TileID { get; }
        private int? ArtID { get; }
        private int? ArtSubID { get; }
        private int? Quality { get; }
        private string Position { get; set; }
        private bool Hidden { get; }
        private int? CreatedEventID { get; }
        public HE_MasterpieceEngraving CreatedEvent { get; set; }

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();


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
                    case "masterpiece_event":
                        CreatedEventID = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpEngraving.Text == ToString() && frm.MainTab.SelectedTab == frm.tabEngraving)
                return;
            Program.MakeSelected(frm.tabEngraving, frm.lstEngraving, this);

            frm.grpEngraving.Text = ToString();
            frm.grpEngraving.Show();
#if DEBUG
            frm.grpEngraving.Text += $" - ID: {Id}";
#endif
            frm.lblEngravingArtist.Data = Artist;
            frm.lblEngravingSkill.Text = SkillRating.ToString();
            frm.lblEngravingCoords.Text = Coords.ToString();
            frm.lblEngravingTile.Text = TileID.ToString();
            frm.lblEngravingArtID.Text = ArtID.ToString();
            frm.lblEngravingArtSubID.Text = ArtSubID.ToString();
            frm.lblEngravingQuality.Text = Quality.ToString();
            frm.lblEngravingLocation.Text = Location.ToString();
            frm.lblEngravingHidden.Text = Hidden.ToString();
            frm.lblEngravingCreatedEvent.Data = CreatedEvent;

            frm.lblEngravingName.Text = ToString();
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
            if (ArtistID.HasValue)
                Artist = World.HistoricalFigures[ArtistID.Value];
            if (CreatedEventID.HasValue)
            {
                CreatedEvent = (HE_MasterpieceEngraving) World.HistoricalEvents[CreatedEventID.Value];
                CreatedEvent.Engraving = this;
            }
        }

        internal override void Process()
        {
            
        }

        internal override void Plus(XDocument xdoc)
        {
            
        }
    }
}