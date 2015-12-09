﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_RazedStructure : HistoricalEvent
    {
        private int? CivId { get; }
        public Entity Civ { get; set; }
        private int? SiteId { get; }
        public Site Site { get; set; }
        private int? StructureId { get; }
        private Structure Structure { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Civ; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }


        public HE_RazedStructure(XDocument xdoc, World world)
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
                Site = World.Sites[SiteId.Value];
            if (CivId.HasValue && World.Entities.ContainsKey(CivId.Value))
                Civ = World.Entities[CivId.Value];

            if (!StructureId.HasValue || StructureId.Value == -1 || Site == null) return;

            Structure = Site.GetStructure(StructureId.Value);
            if (Structure == null)
            {
                Structure = new Structure(Site, StructureId.Value, World);
                Site.AddStructure(Structure);
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
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Civ:", Civ);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Structure:", Structure);   
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            return $"{timestring} {Civ} razed {Structure} in {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Civ} razed a structure in {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                CivId.DBExport(), 
                SiteId.DBExport(), 
                StructureId.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}