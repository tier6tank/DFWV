﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_CreatureDevoured : HistoricalEvent
    {
        private int? SiteId { get; set; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        private int? HfId_Devourer { get; set; }
        public HistoricalFigure Hf_Devourer { private get; set; }
        private int? HfId_Victim { get; set; }
        private HistoricalFigure Hf_Victim { get; set; }
        private int? EntityId { get; set; }
        private Entity Entity { get; set; }
        private Race VictimRace { get; set; }

        public int? VictimCaste { get; set; }

        override public Point Location => Site?.Location ?? Subregion.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return Hf_Devourer;
                yield return Hf_Victim;
            }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Entity; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }
        public override IEnumerable<Region> RegionsInvolved
        {
            get { yield return Subregion; }
        }

        public HE_CreatureDevoured(XDocument xdoc, World world)
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
                    case "site_id":
                        if (valI != -1)
                            SiteId = valI;
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SubregionId = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerId = valI;
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
                    case "victim":
                        if (valI != -1)
                            HfId_Victim = valI;
                        break;
                    case "race":
                        VictimRace = World.GetAddRace(val);
                        break;
                    case "caste":
                        if (!HistoricalFigure.Castes.Contains(val))
                            HistoricalFigure.Castes.Add(val);
                        VictimCaste = HistoricalFigure.Castes.IndexOf(val);
                        break;
                    case "eater":
                        HfId_Devourer = valI;
                        break;
                    case "entity":
                        EntityId = valI;
                        break;
                    case "site":
                        SiteId = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Devourer:", Hf_Devourer);
            if (Hf_Victim != null)
                EventLabel(frm, parent, ref location, "Victim:", Hf_Victim);
            else if (VictimRace != null && VictimCaste != null)
                EventLabel(frm, parent, ref location, "Victim:", HistoricalFigure.Castes[VictimCaste.Value] + " " + VictimRace);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            if (FeatureLayerId != null && FeatureLayerId != -1)
                EventLabel(frm, parent, ref location, "Layer:",FeatureLayerId.ToString());

        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            var location = "in ";
            if (Subregion != null)
                location += Subregion.ToString();
            else
                location += Site.AltName;
            //(time), (HF) devoured a (race) of (entity) in (site).
            var devourertext = "an unknown creature";
            if (Hf_Devourer != null)
                devourertext = $"the {Hf_Devourer.Race.ToString().ToLower()} {Hf_Devourer}";

            if (Hf_Victim == null)
            {
                return
                    $"{timestring} {devourertext} devoured a {VictimRace.ToString().ToLower()} of {Entity} in {location}.";
            }
            if (Entity == null)
            {
                return
                    $"{timestring} {devourertext} devoured the {VictimRace.ToString().ToLower()} {Hf_Victim} {location}.";
            }


            return $"{timestring} the {"UNKNOWN"} {"UNKNOWN"} devoured the {"UNKNOWN"} {"UNKNOWN"} {location}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            var location = "in ";
            if (Subregion != null)
                location += Subregion.ToString();
            else
                location += Site.AltName;

            if (Hf_Devourer != null)
                return $"{timelinestring} {Hf_Devourer} devoured someone {location}.";
            return $"{timelinestring} Creature devoured {location}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                SiteId.DBExport(),
                SubregionId.DBExport(), 
                FeatureLayerId.DBExport(),
                HfId_Devourer.DBExport(),
                HfId_Victim.DBExport(),
                VictimRace.DBExport(),
                VictimCaste.DBExport(HistoricalFigure.Castes),
                EntityId.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}
