using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalEventClasses;

namespace DFWV.WorldClasses
{
    public class WorldConstruction: XMLObject
    {

        public List<WorldConstruction> Subconstructions { get; set; }
        public WorldConstruction MasterWc { get; set; }
        public HE_CreatedWorldConstruction CreatedEvent { get; set; }

        public Site From { get; set; }
        public Site To { get; set; }

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        override public Point Location => From.Location;

        public List<Point> Coords { get; set; }

        public int? Type { get; set; }
        public static List<string> Types = new List<string>();

        public string ConstructionType => Type.HasValue ? Types[Type.Value] : "";

        public WorldConstruction(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            Plus(xdoc);
        }


        public WorldConstruction(int id, World world) 
            : base(world) //Created from Historical Events if World Construction List is empty.
        {
            Id = id;
            World = world;
            Name = id.ToString();
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpWorldConstruction.Text == ToString() && frm.MainTab.SelectedTab == frm.tabWorldConstruction)
                return;
            Program.MakeSelected(frm.tabWorldConstruction, frm.lstWorldConstruction, this);

            frm.grpWorldConstruction.Text = ToString();
#if DEBUG
            frm.grpWorldConstruction.Text += string.Format(string.Format($" - ID: {Id}", Id), Id);
#endif
            frm.grpWorldConstruction.Show();

            frm.lblWorldConstructionMaster.Data = MasterWc;
            frm.lblWorldConstructionFrom.Data = From;
            frm.lblWorldConstructionTo.Data = To;
            if (Type.HasValue)
                frm.lblWorldConstructionType.Text = Types[Type.Value].ToTitleCase();
            frm.lblWorldConstructionCoord.Data = Coords != null ? new Coordinate(Coords[0]) : new Coordinate(From.Coords);
            frm.grpWorldConstruction.Visible = CreatedEvent != null;
            if (CreatedEvent != null)
            {
                frm.lblWorldConstructionCreatedBy.Data = CreatedEvent.Entity_SiteCiv;
                frm.lblWorldConstructionCreatedByCiv.Data = CreatedEvent.Entity;
                frm.lblWorldConstructionCreatedTime.Data = CreatedEvent;
                frm.lblWorldConstructionCreatedTime.Text = CreatedEvent.Time.ToString();
            }

        }

        internal override void Link()
        {

        }

        internal override void Process()
        {

        }

        internal override sealed void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "name":
                        Name = val;
                        break;
                    case "coords":
                        if (Coords == null)
                            Coords = new List<Point>();
                        foreach (var coordSplit in val.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(coord => coord.Split(',')).Where(coordSplit => coordSplit.Length == 2))
                        {
                            Coords.Add(new Point(Convert.ToInt32(coordSplit[0]), Convert.ToInt32(coordSplit[1])));
                        }
                        break;
                    case "type":
                        if (!Types.Contains(val))
                            Types.Add(val);
                        Type = Types.IndexOf(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t", element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id,
                Name.DBExport(),
                Type.DBExport(Types)
            };

            Database.ExportWorldItem(table, vals);

            if (Coords != null)
            {
                var coordId = 0;
                foreach (var coord in Coords)
                {
                    Database.ExportWorldItem("WorldConstruction_Coords", new List<object> { Id, coordId, coord.X, coord.Y });
                    coordId++;
                }
            }
        }
    }
}