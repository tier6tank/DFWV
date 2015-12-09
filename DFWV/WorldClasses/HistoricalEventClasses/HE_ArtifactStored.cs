﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_ArtifactStored : HistoricalEvent
    {
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? HistFigureID { get; set; }
        private HistoricalFigure HistFigure { get; set; }
        private int? UnitID { get; set; }
        private HistoricalFigure Unit { get; set; }
        private int? ArtifactID { get; set; }
        private Artifact Artifact { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HistFigure; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_ArtifactStored(XDocument xdoc, World world)
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
                    case "artifact_id":
                        ArtifactID = valI;
                        break;
                    case "unit_id":
                        UnitID = valI;
                        break;
                    case "hist_figure_id":
                        HistFigureID = valI;
                        break;
                    case "site_id":
                        SiteID = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (HistFigureID.HasValue && World.HistoricalFigures.ContainsKey(HistFigureID.Value))
                HistFigure = World.HistoricalFigures[HistFigureID.Value];
            if (ArtifactID.HasValue && World.Artifacts.ContainsKey(ArtifactID.Value))
                Artifact = World.Artifacts[ArtifactID.Value];
            if (UnitID.HasValue && World.HistoricalFigures.ContainsKey(UnitID.Value))
                Unit = World.HistoricalFigures[UnitID.Value];
        }


        internal override void Process()
        {
            base.Process();

            if (Artifact == null) return;
            if (Artifact.StoredEvents == null)
                Artifact.StoredEvents = new List<HE_ArtifactStored>();
            Artifact.StoredEvents.Add(this);
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            if (Artifact.Type != "" && Artifact.Material != "")
                EventLabel(frm, parent, ref location, "Item:", Artifact.Material + " " + Artifact.Type);
            if (UnitID != null && UnitID > -1)
                EventLabel(frm, parent, ref location, "Unit ID:", UnitID.Value.ToString());
            EventLabel(frm, parent, ref location, "HF:", HistFigure);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            return $"{timestring} {Artifact} was stored in {Site.AltName} by the {HistFigure}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Artifact} was stored in {Site.AltName} by {HistFigure}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;


            var vals = new List<object>
            {
                ID, 
                ArtifactID.DBExport(), 
                UnitID.DBExport(), 
                SiteID.DBExport(), 
                HistFigureID.DBExport()
            };


            Database.ExportWorldItem(table, vals);

        }

    }
}