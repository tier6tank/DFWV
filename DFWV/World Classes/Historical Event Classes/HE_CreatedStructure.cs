using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_CreatedStructure : HistoricalEvent
    {
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int? StructureID { get; set; }
        public Structure Structure { get; set; }
        public int? SiteCivID { get; set; }
        public Entity SiteCiv { get; set; }
        public int? CivID { get; set; }
        public Entity Civ { get; set; }

        override public Point Location { get { return Site.Location; } }

        public HE_CreatedStructure(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
                int valI;
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "year":
                    case "seconds72":
                    case "type":
                        break;
                    case "civ_id":
                        CivID = valI;
                        break;
                    case "site_civ_id":
                        SiteCivID = valI;
                        break;
                    case "site_id":
                        SiteID = valI;
                        break;
                    case "structure_id":
                        StructureID = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + HistoricalEvent.Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }
        internal override void Link()
        {
            base.Link();
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (CivID.HasValue && World.Entities.ContainsKey(CivID.Value))
                Civ = World.Entities[CivID.Value];
            if (SiteCivID.HasValue && World.Entities.ContainsKey(SiteCivID.Value))
                SiteCiv = World.Entities[SiteCivID.Value];
            if (StructureID.HasValue && StructureID.Value != -1 && Site != null)
            {
                if (Site.Structures == null)
                    Site.Structures = new Dictionary<int, Structure>();
                if (Site.Structures.ContainsKey(StructureID.Value))
                    Structure = Site.Structures[StructureID.Value];
                else
                {
                    Structure = new Structure(Site, StructureID.Value, World);
                    Site.Structures.Add(StructureID.Value, Structure);
                }

            }
        }

        internal override void Process()
        {
            base.Process();
            if (Site.Structures.ContainsValue(Structure))
                Structure.CreatedEvent = this;


            if (SiteCiv != null)
            {
                if (SiteCiv.Events == null)
                    SiteCiv.Events = new List<HistoricalEvent>();
                SiteCiv.Events.Add(this);
            }

            if (Civ != null)
            {
                if (Civ.Events == null)
                    Civ.Events = new List<HistoricalEvent>();
                Civ.Events.Add(this);
            }
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Civ:", Civ);
            EventLabel(frm, parent, ref location, "Owner:", SiteCiv);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Structure:", Structure);
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            if (SiteCiv == null)
                return string.Format("{0} {1} constructed {2} in {3}.",
                                timestring, Civ.ToString(), "UNKNOWN",
                                Site.AltName);

            else
                return string.Format("{0} {1} of {2} constructed {3} in {4}.",
                            timestring, SiteCiv.ToString(), Civ.ToString(), "UNKNOWN",
                            Site.AltName);

        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} built a structure in {2}.",
                        timelinestring, Civ.ToString(),
                                Site.AltName);
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();


            
            vals = new List<object>() { ID, SiteID, StructureID, SiteCivID, CivID };


            Database.ExportWorldItem(table, vals);

        }

    }
}
