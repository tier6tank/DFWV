using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_CreatedWorldConstruction : HistoricalEvent
    {
        private int? WcId { get; }
        private WorldConstruction Wc { get; set; }
        private int? EntityId_SiteCiv { get; }
        public Entity Entity_SiteCiv { get; private set; }
        private int? EntityId { get; }
        public Entity Entity { get; private set; }
        private int? WcId_Master { get; }
        public WorldConstruction Wc_Master { get; private set; }
        private int? SiteId_1 { get; }
        public Site Site_1 { get; private set; }
        private int? SiteId_2 { get; }
        public Site Site_2 { get; private set; }

        override public Point Location => Site_1.Location;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity;
                yield return Entity_SiteCiv;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get
            {
                yield return Site_1;
                yield return Site_2;
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
                        EntityId = valI;
                        break;
                    case "site_civ_id":
                        EntityId_SiteCiv = valI;
                        break;
                    case "site_id1":
                        SiteId_1 = valI;
                        break;
                    case "site_id2":
                        SiteId_2 = valI;
                        break;
                    case "wcid":
                        WcId = valI;
                        break;
                    case "master_wcid":
                        if (valI != -1)
                            WcId_Master = valI;
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
            if (WcId.HasValue)
            {
                if (World.WorldConstructions.ContainsKey(WcId.Value))
                    Wc = World.WorldConstructions[WcId.Value];
                else
                {
                    Wc = new WorldConstruction(WcId.Value, World);
                    World.WorldConstructions[WcId.Value] = Wc;
                }
                if (WcId_Master.HasValue && WcId_Master != -1)
                {
                    if (World.WorldConstructions.ContainsKey(WcId_Master.Value))
                        Wc_Master = World.WorldConstructions[WcId_Master.Value];
                    else
                    {
                        Wc_Master = new WorldConstruction(WcId_Master.Value, World);
                        World.WorldConstructions[WcId_Master.Value] = Wc_Master;
                    }
                }
            }
        }

        public override void Process()
        {
            base.Process();
            if (Wc_Master != null)
            {
                if (Wc_Master.Subconstructions == null)
                    Wc_Master.Subconstructions = new List<WorldConstruction>();
                Wc_Master.Subconstructions.Add(Wc);

                Wc.MasterWc = Wc_Master;
            }
            Wc.CreatedEvent = this;

            if (Site_1 != null)
                Wc.From = Site_1;
            if (Site_2 != null)
                Wc.To = Site_2;


            if (Site_1 != null && Site_1.ConstructionLinks == null)
                Site_1.ConstructionLinks = new List<WorldConstruction>();
            Site_1.ConstructionLinks.Add(Wc);
            if (Site_2 != null && Site_2.ConstructionLinks == null)
                Site_2.ConstructionLinks = new List<WorldConstruction>();
            Site_2.ConstructionLinks.Add(Wc);

            if (Entity != null && Entity.ConstructionsBuilt == null)
                Entity.ConstructionsBuilt = new List<WorldConstruction>();
            Entity.ConstructionsBuilt.Add(Wc);
            if (Entity_SiteCiv != null && Entity_SiteCiv.ConstructionsBuilt == null)
                Entity_SiteCiv.ConstructionsBuilt = new List<WorldConstruction>();
            Entity_SiteCiv.ConstructionsBuilt.Add(Wc);
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Civ:", Entity);
            EventLabel(frm, parent, ref location, "Owner:", Entity_SiteCiv);
            EventLabel(frm, parent, ref location, "Construction:", Wc);
            EventLabel(frm, parent, ref location, "Master:", Wc_Master);
            EventLabel(frm, parent, ref location, "From:", Site_1);
            EventLabel(frm, parent, ref location, "To:", Site_2);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            return
                $"{timestring} {Entity_SiteCiv} of {Entity} finished contruction of {(Wc.Name == "" ? "CONSTRUCTION " + Wc : Wc.Name)} connecting {Site_1} and {Site_2}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Entity} built road from {Site_1} to {Site_2}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                Id, 
                WcId.DBExport(), 
                WcId_Master.DBExport(),
                EntityId_SiteCiv.DBExport(), 
                EntityId.DBExport(), 
                SiteId_1.DBExport(),
                SiteId_2.DBExport()
            };


            Database.ExportWorldItem(table, vals);

        }

    }
}