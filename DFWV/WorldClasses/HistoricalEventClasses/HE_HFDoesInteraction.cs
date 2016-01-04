using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFDoesInteraction : HistoricalEvent
    {
        private int? TargetHfid { get; }
        private HistoricalFigure TargetHf { get; set; }
        private int? DoerHfid { get; }
        private HistoricalFigure DoerHf { get; set; }
        private int Interaction { get; }
        private string InteractionString { get; set; }
        private string InteractionAction { get; set; }

        private int? SiteId { get; set; }
        private Site Site { get; set; }
        private int? SubregionId { get; set; }
        private Region Subregion { get; set; }
        private int? Source { get; set; }

        override public Point Location => Point.Empty;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return TargetHf;
                yield return DoerHf;
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
                        DoerHfid = valI;
                        break;
                    case "target_hfid":
                        TargetHfid = valI;
                        break;
                    case "interaction":
                        if (!HistoricalFigure.Interactions.Contains(val))
                            HistoricalFigure.Interactions.Add(val);
                        Interaction = HistoricalFigure.Interactions.IndexOf(val);
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
            if (TargetHfid.HasValue && World.HistoricalFigures.ContainsKey(TargetHfid.Value))
                TargetHf = World.HistoricalFigures[TargetHfid.Value];
            if (DoerHfid.HasValue && World.HistoricalFigures.ContainsKey(DoerHfid.Value))
                DoerHf = World.HistoricalFigures[DoerHfid.Value];
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (SubregionId.HasValue && World.Regions.ContainsKey(SubregionId.Value))
                Subregion = World.Regions[SubregionId.Value];
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
                            SiteId = valI;
                        break;
                    case "region":
                        if (valI != -1)
                            SubregionId = valI;
                        break;
                    case "source":
                        Source = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            EventLabel(frm, parent, ref location, "HF:", DoerHf);
            EventLabel(frm, parent, ref location, "Target:", TargetHf);
            EventLabel(frm, parent, ref location, "Interaction:", HistoricalFigure.Interactions[Interaction]);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
        }

        protected override string LegendsDescription() //Not Matched
        {
            //TODO: Incorporate new data
            var timestring = base.LegendsDescription();

            if (InteractionAction != null && InteractionString != null)
                return $"{timestring} {DoerHf} {InteractionAction} {TargetHf} {InteractionString} in {Site.AltName}";
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("curse_vampire") || HistoricalFigure.Interactions[Interaction].ToLower().Contains("master_vampire_curse"))
                return $"{timestring} {DoerHf} cursed {TargetHf} to prowl the night in search of blood in {"UNKNOWN"}.";
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("curse_werebeast"))
                return
                    $"{timestring} {DoerHf} cursed {TargetHf} to assume the form of a {"UNKNOWN"}-like monster every full moon in {"UNKNOWN"}.";
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("werelizard_curse"))
                return
                    $"{timestring} {DoerHf} cursed {TargetHf} to assume the form of a lizard-like monster every full moon in {"UNKNOWN"}.";
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("werewolf_curse"))
                return
                    $"{timestring} {DoerHf} cursed {TargetHf} to assume the form of a wolf-like monster every full moon in {"UNKNOWN"}.";
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("werebear_curse"))
                return
                    $"{timestring} {DoerHf} cursed {TargetHf} to assume the form of a bear-like monster every full moon in {"UNKNOWN"}.";
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("lesser_vampire_curse"))
                return
                    $"{timestring} {DoerHf} cursed {TargetHf} to slither through the shadows in search of blood in {"UNKNOWN"}.";
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("minor_vampire_curse"))
                return $"{timestring} {DoerHf} cursed {TargetHf} to endlessly lust for blood in {"UNKNOWN"}.";
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("curse"))
                return $"{timestring} {DoerHf} cursed {TargetHf} to {Interaction} in {"UNKNOWN"}.";
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("infected_bite"))
                return $"{timestring} {DoerHf} bit the infected {TargetHf}, infecting in {"UNKNOWN"}.";
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("murder_roar"))
                return $"{timestring} {DoerHf} cursed {TargetHf} to kill for enjoyment in {"UNKNOWN"}.";
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("chosen_one"))
                return
                    $"{timestring} {DoerHf} chose {TargetHf} to seek out and destroy the powers of evil in {"UNKNOWN"}.";
            if (HistoricalFigure.Interactions[Interaction].ToLower().Contains("dwarf_to_spawn"))
                return
                    $"{timestring} {DoerHf} bit {TargetHf}, mutating them into a twisted mockery of dwarvenkind {"UNKNOWN"}.";
            return timestring;
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return
                $"{timelinestring} {DoerHf?.ToString() ?? DoerHfid.ToString()} cursed {TargetHf?.ToString() ?? TargetHfid.ToString()}";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                Id, 
                TargetHfid.DBExport(), 
                DoerHfid.DBExport(),
                Interaction.DBExport(HistoricalFigure.Interactions),
                SiteId.DBExport(),
                Subregion.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}

