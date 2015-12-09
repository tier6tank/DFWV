using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFProfanedStructure : HistoricalEvent
    {
        private int? HistFigId { get; }
        private HistoricalFigure HistFig { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? StructureId { get; }
        private Structure Structure { get; set; }

        private int? Action { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HistFig; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_HFProfanedStructure(XDocument xdoc, World world)
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
                    case "hist_fig_id":
                        HistFigId = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "structure_id":
                        if (valI != -1)
                        StructureId = valI;
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
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
            {
                Site = World.Sites[SiteId.Value];
                if (StructureId.HasValue)
                {
                    if (Site.GetStructure(StructureId.Value) == null)
                        Site.AddStructure(new Structure(Site, StructureId.Value, World));
                    Structure = Site.GetStructure(StructureId.Value);
                }
            }
            if (HistFigId.HasValue && World.HistoricalFigures.ContainsKey(HistFigId.Value))
                HistFig = World.HistoricalFigures[HistFigId.Value];
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
                    case "histfig":
                    case "site":
                    case "structure":
                        break;
                    case "action":
                        Action = valI;
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

            if (Structure == null) return;
            if (Structure.Events == null)
                Structure.Events = new List<HistoricalEvent>();
            Structure.Events.Add(this);
        }


        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Hist Fig:", HistFig);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Structure:", Structure);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            return $"{timestring} {HistFig.Race} {HistFig} profaned the {Structure} in {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {HistFig} profaned a structure in {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                HistFigId.DBExport(), 
                SiteId.DBExport(), 
                StructureId.DBExport(),
                Action.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}

