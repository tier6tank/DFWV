using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_CreatureDevoured : HistoricalEvent
    {
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SubregionID { get; set; }
        private Region Subregion { get; set; }
        private int? FeatureLayerID { get; set; }
        private int? DevourerID { get; set; }
        public HistoricalFigure Devourer { private get; set; }
        private int? VictimID { get; set; }
        private HistoricalFigure Victim { get; set; }
        private int? EntityID { get; set; }
        private Entity Entity { get; set; }
        private Race VictimRace { get; set; }

        public int? VictimCaste { get; set; }

        override public Point Location { get { return Site != null ? Site.Location : Subregion.Location; } }

        public HE_CreatureDevoured(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "year":
                    case "seconds72":
                    case "type":
                        break;
                    case "site_id":
                        if (valI != -1)
                            SiteID = valI;
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SubregionID = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerID = valI;
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
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
            if (EntityID.HasValue && World.Entities.ContainsKey(EntityID.Value))
                Entity = World.Entities[EntityID.Value];
            if (DevourerID.HasValue && World.HistoricalFigures.ContainsKey(DevourerID.Value))
                Devourer = World.HistoricalFigures[DevourerID.Value];
            if (VictimID.HasValue && World.HistoricalFigures.ContainsKey(VictimID.Value))
                Victim = World.HistoricalFigures[VictimID.Value];
            if (DevourerID.HasValue && World.HistoricalFigures.ContainsKey(DevourerID.Value))
                Devourer = World.HistoricalFigures[DevourerID.Value];

            
        }

        internal override void Process()
        {
            base.Process();
            if (Victim != null)
            {
                if (Victim.Events == null)
                    Victim.Events = new List<HistoricalEvent>();
                Victim.Events.Add(this);
            }
            if (Devourer != null)
            {
                if (Devourer.Events == null)
                    Devourer.Events = new List<HistoricalEvent>();
                Devourer.Events.Add(this);
            }
            if (Entity != null)
            {
                if (Entity.Events == null)
                    Entity.Events = new List<HistoricalEvent>();
                Entity.Events.Add(this);
            }
        }

        internal override void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                        break;
                    case "victim":
                        if (valI != -1)
                            VictimID = valI;
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
                        DevourerID = valI;
                        break;
                    case "entity":
                        EntityID = valI;
                        break;
                    case "site":
                        SiteID = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Devourer:", Devourer);
            if (Victim != null)
                EventLabel(frm, parent, ref location, "Victim:", Victim);
            else if (VictimRace != null && VictimCaste != null)
                EventLabel(frm, parent, ref location, "Victim:", HistoricalFigure.Castes[VictimCaste.Value] + " " + VictimRace);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            if (FeatureLayerID != null && FeatureLayerID != -1)
                EventLabel(frm, parent, ref location, "Layer:",FeatureLayerID.ToString());

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
            if (Devourer != null)
                devourertext = string.Format("the {0} {1}", Devourer.Race.ToString().ToLower(), Devourer);

            if (Victim == null)
            {
                return string.Format("{0} {1} devoured a {2} of {3} in {4}.",
                    timestring, devourertext, VictimRace.ToString().ToLower(), Entity,
                    location);
            }
            if (Entity == null)
            {
                return string.Format("{0} {1} devoured the {2} {3} {4}.",
                    timestring, devourertext, VictimRace.ToString().ToLower(), Victim,
                    location);
            }


            return string.Format("{0} the {1} {2} devoured the {3} {4} {5}.",
                timestring, "UNKNOWN", "UNKNOWN", "UNKNOWN", "UNKNOWN",
                location);
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

            if (Devourer != null)
                return string.Format("{0} {1} devoured someone {2}.",
                    timelinestring, Devourer, location);
            return string.Format("{0} Creature devoured {1}.",
                timelinestring, location);
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID, 
                SiteID.DBExport(),
                SubregionID.DBExport(), 
                FeatureLayerID.DBExport(),
                DevourerID.DBExport(),
                VictimID.DBExport(),
                VictimRace.DBExport(),
                VictimCaste.DBExport(HistoricalFigure.Castes),
                EntityID.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}
