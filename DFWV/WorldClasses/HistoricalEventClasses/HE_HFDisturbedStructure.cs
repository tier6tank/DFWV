﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFDisturbedStructure : HistoricalEvent
    {
        private int? HistFigID { get; set; }
        private HistoricalFigure HistFig { get; set; }
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? StructureID { get; set; }
        private Structure Structure { get; set; }

        private int? Action { get; set; }

        override public Point Location { get { return Site.Location; } }
        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HistFig; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_HFDisturbedStructure(XDocument xdoc, World world)
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
                        HistFigID = valI;
                        break;
                    case "site_id":
                        SiteID = valI;
                        break;
                    case "structure_id":
                        if (valI != -1)
                            StructureID = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            //TODO: Incorporate new data
            base.Link();
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (HistFigID.HasValue && World.HistoricalFigures.ContainsKey(HistFigID.Value))
                HistFig = World.HistoricalFigures[HistFigID.Value];

            if (!StructureID.HasValue || StructureID.Value == -1 || Site == null) return;

            Structure = Site.GetStructure(StructureID.Value);
            if (Structure != null) return;
            Structure = new Structure(Site, StructureID.Value, World);
            Site.AddStructure(Structure);
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
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
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
            //TODO: Incorporate new data
            EventLabel(frm, parent, ref location, "Hist Fig:", HistFig);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Structure:", Structure);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            return string.Format("{0} {1} {2} profaned the {3} in {4}.",
                            timestring, HistFig.Race, HistFig,
                            Structure, Site.AltName);
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            if (Site != null)
                return string.Format("{0} {1} {2} disturbed structure in {3}.",
                    timelinestring, HistFig.Race, HistFig,
                    Site.AltName);
            return string.Format("{0} {1} {2} disturbed structure.",
                timelinestring, HistFig.Race, HistFig);
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID, 
                HistFigID.DBExport(), 
                SiteID.DBExport(), 
                StructureID.DBExport(),
                Action.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}