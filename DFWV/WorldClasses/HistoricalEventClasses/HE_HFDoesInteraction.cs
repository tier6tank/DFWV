using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFDoesInteraction : HistoricalEvent
    {
        private int? TargetHFID { get; set; }
        private HistoricalFigure TargetHF { get; set; }
        private int? DoerHFID { get; set; }
        private HistoricalFigure DoerHF { get; set; }
        private int Interaction { get; set; }
        private string InteractionString { get; set; }
        private string InteractionAction { get; set; }

        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SubregionID { get; set; }
        private Region Subregion { get; set; }
        private int? Source { get; set; }

        override public Point Location { get { return Point.Empty; } }
        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return TargetHF;
                yield return DoerHF;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_HFDoesInteraction(XDocument xdoc, World world)
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
                    case "doer_hfid":
                        DoerHFID = valI;
                        break;
                    case "target_hfid":
                        TargetHFID = valI;
                        break;
                    case "interaction":
                        if (!HistoricalFigure.Interactions.Contains(val))
                            HistoricalFigure.Interactions.Add(val);
                        Interaction = HistoricalFigure.Interactions.IndexOf(val);
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
            if (TargetHFID.HasValue && World.HistoricalFigures.ContainsKey(TargetHFID.Value))
                TargetHF = World.HistoricalFigures[TargetHFID.Value];
            if (DoerHFID.HasValue && World.HistoricalFigures.ContainsKey(DoerHFID.Value))
                DoerHF = World.HistoricalFigures[DoerHFID.Value];
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
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
                    case "attacker":
                    case "target":
                    case "doer":
                    case "interaction":
                        break;
                    case "interaction_action": //[IS_HIST_STRING_2: to assume the form of a elephant-like monster every full moon]
                        InteractionAction = val.Replace("[IS_HIST_STRING_1: ", "").TrimEnd(']').Trim();
                        break;
                    case "interaction_string": //[IS_HIST_STRING_1: cursed]
                        InteractionString = val.Replace("[IS_HIST_STRING_2: ", "").TrimEnd(']').Trim();
                        break;
                    case "site":
                        if (valI != -1)
                            SiteID = valI;
                        break;
                    case "region":
                        if (valI != -1)
                            SubregionID = valI;
                        break;
                    case "source":
                        Source = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            EventLabel(frm, parent, ref location, "HF:", DoerHF);
            EventLabel(frm, parent, ref location, "Target:", TargetHF);
            EventLabel(frm, parent, ref location, "Interaction:", HistoricalFigure.Interactions[Interaction]);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
        }

        protected override string LegendsDescription() //Not Matched
        {
            //TODO: Incorporate new data
            var timestring = base.LegendsDescription();

            if (InteractionAction != null && InteractionString != null)
                return string.Format("{0} {1} {2} {3} {4} in {5}",
                    timestring, DoerHF, InteractionAction, TargetHF, InteractionString, Site.AltName);
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("curse_vampire") || HistoricalFigure.Interactions[Interaction].ToLower().Contains("master_vampire_curse"))
                return string.Format("{0} {1} cursed {2} to prowl the night in search of blood in {3}.",
                    timestring, DoerHF, TargetHF, "UNKNOWN");
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("curse_werebeast"))
                return string.Format("{0} {1} cursed {2} to assume the form of a {3}-like monster every full moon in {4}.",
                    timestring, DoerHF, TargetHF,
                    "UNKNOWN", "UNKNOWN");
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("werelizard_curse"))
                return string.Format("{0} {1} cursed {2} to assume the form of a lizard-like monster every full moon in {3}.",
                    timestring, DoerHF, TargetHF, "UNKNOWN");
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("werewolf_curse"))
                return string.Format("{0} {1} cursed {2} to assume the form of a wolf-like monster every full moon in {3}.",
                    timestring, DoerHF, TargetHF, "UNKNOWN");
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("werebear_curse"))
                return string.Format("{0} {1} cursed {2} to assume the form of a bear-like monster every full moon in {3}.",
                    timestring, DoerHF, TargetHF, "UNKNOWN");
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("lesser_vampire_curse"))
                return string.Format("{0} {1} cursed {2} to slither through the shadows in search of blood in {3}.",
                    timestring, DoerHF, TargetHF, "UNKNOWN");
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("minor_vampire_curse"))
                return string.Format("{0} {1} cursed {2} to endlessly lust for blood in {3}.",
                    timestring, DoerHF, TargetHF, "UNKNOWN");
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("curse"))
                return string.Format("{0} {1} cursed {2} to {3} in {4}.",
                    timestring, DoerHF, TargetHF,
                    Interaction, "UNKNOWN");
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("infected_bite"))
                return string.Format("{0} {1} bit the infected {2}, infecting in {3}.",
                    timestring, DoerHF, TargetHF, "UNKNOWN");
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("murder_roar"))
                return string.Format("{0} {1} cursed {2} to kill for enjoyment in {3}.",
                    timestring, DoerHF, TargetHF,
                    "UNKNOWN");
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("chosen_one"))
                return string.Format("{0} {1} chose {2} to seek out and destroy the powers of evil in {3}.",
                    timestring, DoerHF, TargetHF,
                    "UNKNOWN");
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("dwarf_to_spawn"))
                return string.Format("{0} {1} bit {2}, mutating them into a twisted mockery of dwarvenkind {3}.",
                    timestring, DoerHF, TargetHF, "UNKNOWN");
            return timestring;
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} cursed {2}",
                        timelinestring, DoerHF != null ? DoerHF.ToString() : DoerHFID.ToString(), 
                                TargetHF != null ? TargetHF.ToString() : TargetHFID.ToString());
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                ID, 
                TargetHFID.DBExport(), 
                DoerHFID.DBExport(),
                Interaction.DBExport(HistoricalFigure.Interactions),
                SiteID.DBExport(),
                Subregion.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}

