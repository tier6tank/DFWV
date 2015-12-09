using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_CreatedWorldConstruction : HistoricalEvent
    {
        private int? Wcid { get; }
        private WorldConstruction Wc { get; set; }
        private int? SiteCivId { get; }
        public Entity SiteCiv { get; private set; }
        private int? CivId { get; }
        public Entity Civ { get; private set; }
        private int? MasterWcid { get; }
        public WorldConstruction MasterWc { get; private set; }
        private int? SiteId1 { get; }
        public Site Site1 { get; private set; }
        private int? SiteId2 { get; }
        public Site Site2 { get; private set; }

        override public Point Location => Site1.Location;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Civ;
                yield return SiteCiv;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get
            {
                yield return Site1;
                yield return Site2;
            }
        }

        public HE_CreatedWorldConstruction(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "year":
                    case "seconds72":
                    case "type":
                        break;
                    case "civ_id":
                        CivId = valI;
                        break;
                    case "site_civ_id":
                        SiteCivId = valI;
                        break;
                    case "site_id1":
                        SiteId1 = valI;
                        break;
                    case "site_id2":
                        SiteId2 = valI;
                        break;
                    case "wcid":
                        Wcid = valI;
                        break;
                    case "master_wcid":
                        if (valI != -1)
                            MasterWcid = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (CivId.HasValue && World.Entities.ContainsKey(CivId.Value))
                Civ = World.Entities[CivId.Value];
            if (SiteCivId.HasValue && World.Entities.ContainsKey(SiteCivId.Value))
                SiteCiv = World.Entities[SiteCivId.Value];
            if (Wcid.HasValue)
            {
                if (World.WorldConstructions.ContainsKey(Wcid.Value))
                    Wc = World.WorldConstructions[Wcid.Value];
                else
                {
                    Wc = new WorldConstruction(Wcid.Value, World);
                    World.WorldConstructions.Add(Wcid.Value, Wc);
                }
                if (MasterWcid.HasValue && MasterWcid != -1)
                {
                    if (World.WorldConstructions.ContainsKey(MasterWcid.Value))
                        MasterWc = World.WorldConstructions[MasterWcid.Value];
                    else
                    {
                        MasterWc = new WorldConstruction(MasterWcid.Value, World);
                        World.WorldConstructions.Add(MasterWcid.Value, MasterWc);
                    }
                }
            }
            if (SiteId1.HasValue && World.Sites.ContainsKey(SiteId1.Value))
                Site1 = World.Sites[SiteId1.Value];
            if (SiteId2.HasValue && World.Sites.ContainsKey(SiteId2.Value))
                Site2 = World.Sites[SiteId2.Value];
        }

        internal override void Process()
        {
            base.Process();
            if (MasterWc != null)
            {
                if (MasterWc.Subconstructions == null)
                    MasterWc.Subconstructions = new List<WorldConstruction>();
                MasterWc.Subconstructions.Add(Wc);

                Wc.MasterWc = MasterWc;
            }
            Wc.CreatedEvent = this;

            if (Site1 != null)
                Wc.From = Site1;
            if (Site2 != null)
                Wc.To = Site2;


            if (Site1 != null && Site1.ConstructionLinks == null)
                Site1.ConstructionLinks = new List<WorldConstruction>();
            Site1.ConstructionLinks.Add(Wc);
            if (Site2 != null && Site2.ConstructionLinks == null)
                Site2.ConstructionLinks = new List<WorldConstruction>();
            Site2.ConstructionLinks.Add(Wc);

            if (Civ != null && Civ.ConstructionsBuilt == null)
                Civ.ConstructionsBuilt = new List<WorldConstruction>();
            Civ.ConstructionsBuilt.Add(Wc);
            if (SiteCiv != null && SiteCiv.ConstructionsBuilt == null)
                SiteCiv.ConstructionsBuilt = new List<WorldConstruction>();
            SiteCiv.ConstructionsBuilt.Add(Wc);
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Civ:", Civ);
            EventLabel(frm, parent, ref location, "Owner:", SiteCiv);
            EventLabel(frm, parent, ref location, "Construction:", Wc);
            EventLabel(frm, parent, ref location, "Master:", MasterWc);
            EventLabel(frm, parent, ref location, "From:", Site1);
            EventLabel(frm, parent, ref location, "To:", Site2);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            return
                $"{timestring} {SiteCiv} of {Civ} finished contruction of {(Wc.Name == "" ? "CONSTRUCTION " + Wc : Wc.Name)} connecting {Site1} and {Site2}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Civ} built road from {Site1} to {Site2}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                Id, 
                Wcid.DBExport(), 
                MasterWcid.DBExport(),
                SiteCivId.DBExport(), 
                CivId.DBExport(), 
                SiteId1.DBExport(),
                SiteId2.DBExport()
            };


            Database.ExportWorldItem(table, vals);

        }

    }
}