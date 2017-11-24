using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_CreatedStructure : HistoricalEvent
    {
        private int? SiteId { get; }
        public Site Site { get; set; }
        private int? StructureId { get; }
        private Structure Structure { get; set; }
        private int? EntityId_SiteCiv { get; }
        public Entity Entity_SiteCiv { get; set; }
        private int? EntityId { get; }
        public Entity Entity { get; set; }
        private int? HfId { get; }
        public HistoricalFigure Hf { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
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
            get { yield return Site; }
        }

        public HE_CreatedStructure(XDocument xdoc, World world)
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
                        if (valI != -1)
                            EntityId = valI;
                        break;
                    case "site_civ_id":
                        if (valI != -1)
                            EntityId_SiteCiv = valI;
                        break;
                    case "site_id":
                        if (valI != -1)
                            SiteId = valI;
                        break;
                    case "structure_id":
                        if (valI != -1)
                            StructureId = valI;
                        break;
                    case "builder_hfid":
                        if (valI != -1)
                            HfId = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
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
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
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

            if (Hf == null) return;
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
            var addHfEntityLinkEvent = NextEvent() as HE_AddHFEntityLink;
            var changeHfStateEvent = NextEvent().NextEvent() as HE_ChangeHFState;
            var addHfSiteLinkEvent = NextEvent().NextEvent().NextEvent() as HE_AddHFSiteLink;

            addHfEntityLinkEvent.Hf = Hf;
            addHfSiteLinkEvent.Hf = Hf;
            addHfSiteLinkEvent.Structure = Structure;
            addHfSiteLinkEvent.Entity = addHfEntityLinkEvent.Entity;
            changeHfStateEvent.Hf = Hf;
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            if (Entity != null)
                EventLabel(frm, parent, ref location, "Civ:", Entity);
            if (Entity_SiteCiv != null)
                EventLabel(frm, parent, ref location, "Owner:", Entity_SiteCiv);
            if (Hf != null)
                EventLabel(frm, parent, ref location, "Builder:", Hf);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Structure:", Structure);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (Hf != null)
                return
                    $"{timestring} {Hf} thrust a spire of slade up from the underworld naming it {Structure}, and established a gateway between worlds in {Site.AltName}.";

            if (Entity_SiteCiv == null)
                return $"{timestring} {Entity} constructed {Structure} in {Site.AltName}.";

            return $"{timestring} {Entity_SiteCiv} of {Entity} constructed {Structure} in {Site.AltName}.";

        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (Hf != null)
                return $"{timelinestring} {Hf} established a gateway between worlds in {Site.AltName}.";

            return $"{timelinestring} {Entity} built a structure in {Site.AltName}.";
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
                EntityId_SiteCiv.DBExport(), 
                EntityId.DBExport(),
                HfId.DBExport()
            };


            Database.ExportWorldItem(table, vals);

        }

    }
}
