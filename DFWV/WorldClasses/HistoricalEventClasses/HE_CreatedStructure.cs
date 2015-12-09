using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HeCreatedStructure : HistoricalEvent
    {
        private int? SiteId { get; }
        public Site Site { get; set; }
        private int? StructureId { get; }
        private Structure Structure { get; set; }
        private int? SiteCivId { get; }
        public Entity SiteCiv { get; set; }
        private int? CivId { get; }
        public Entity Civ { get; set; }
        private int? BuilderHfid { get; }
        public HistoricalFigure BuilderHf { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return BuilderHf; }
        }
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
            get { yield return Site; }
        }

        public HeCreatedStructure(XDocument xdoc, World world)
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
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "structure_id":
                        StructureId = valI;
                        break;
                    case "builder_hfid":
                        BuilderHfid = valI;
                        break;
                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }
        internal override void Link()
        {
            base.Link();
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
            {
                Site = World.Sites[SiteId.Value];
                if (StructureId.HasValue)
                { 
                    Structure = Site.GetStructure(StructureId.Value);

                    if (Structure == null)
                    {
                        Structure = new Structure(Site, StructureId.Value, World);
                        Site.AddStructure(Structure);
                    }
                }
            }

            if (CivId.HasValue && World.Entities.ContainsKey(CivId.Value))
                Civ = World.Entities[CivId.Value];
            if (SiteCivId.HasValue && World.Entities.ContainsKey(SiteCivId.Value))
                SiteCiv = World.Entities[SiteCivId.Value];
            if (BuilderHfid.HasValue && World.HistoricalFigures.ContainsKey(BuilderHfid.Value))
                BuilderHf = World.HistoricalFigures[BuilderHfid.Value];


        }

        internal override void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                        break;
                    case "civ":
                    case "group":
                    case "site":
                    case "structure":
                    case "site_civ":
                    case "builder_hf":
                        break;
                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Process()
        {

            base.Process();
            if (Structure != null)
            {
                if (Structure.Events == null)
                    Structure.Events = new List<HistoricalEvent>();
                Structure.Events.Add(this);
            }

            if (BuilderHf == null) return;
            if (Time.Year == -1 &&
                NextEvent().Type == Types.IndexOf("add hf entity link") &&
                NextEvent().NextEvent().Type == Types.IndexOf("change hf state") &&
                NextEvent().NextEvent().NextEvent().Type == Types.IndexOf("add hf site link"))
            {
                ProcessSladeSpireEventSet();
            }
        }


        private void ProcessSladeSpireEventSet()
        {
            var addHfEntityLinkEvent = NextEvent() as HeAddHfEntityLink;
            var changeHfStateEvent = NextEvent().NextEvent() as HeChangeHfState;
            var addHfSiteLinkEvent = NextEvent().NextEvent().NextEvent() as HeAddHfSiteLink;

            addHfEntityLinkEvent.Hf = BuilderHf;
            addHfSiteLinkEvent.Hf = BuilderHf;
            addHfSiteLinkEvent.Structure = Structure;
            addHfSiteLinkEvent.Civ = addHfEntityLinkEvent.Civ;
            changeHfStateEvent.Hf = BuilderHf;
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            if (Civ != null)
                EventLabel(frm, parent, ref location, "Civ:", Civ);
            if (SiteCiv != null)
                EventLabel(frm, parent, ref location, "Owner:", SiteCiv);
            if (BuilderHf != null)
                EventLabel(frm, parent, ref location, "Builder:", BuilderHf);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Structure:", Structure);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (BuilderHf != null)
                return
                    $"{timestring} {BuilderHf} thrust a spire of slade up from the underworld naming it {Structure}, and established a gateway between worlds in {Site.AltName}.";

            if (SiteCiv == null)
                return $"{timestring} {Civ} constructed {Structure} in {Site.AltName}.";

            return $"{timestring} {SiteCiv} of {Civ} constructed {Structure} in {Site.AltName}.";

        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (BuilderHf != null)
                return $"{timelinestring} {BuilderHf} established a gateway between worlds in {Site.AltName}.";

            return $"{timelinestring} {Civ} built a structure in {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                SiteId.DBExport(), 
                StructureId.DBExport(), 
                SiteCivId.DBExport(), 
                CivId.DBExport(),
                BuilderHfid.DBExport()
            };


            Database.ExportWorldItem(table, vals);

        }

    }
}
