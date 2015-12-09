using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HeAssumeIdentity : HistoricalEvent
    {
        private int? TricksterHfid { get; }
        private HistoricalFigure TricksterHf { get; set; }
        private int? TargetEnId { get; }
        private Entity TargetEn { get; set; }
        private int? IdentityId { get; }

        public int? IdentityHfid { get; set; }
        public HistoricalFigure IdentityHf { get; set; }
        public Race IdentityRace { get; set; }
        public int? IdentityCaste { get; set; }
        public string IdentityName { get; set; }
        
        override public Point Location => TargetEn.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return TricksterHf;
                yield return IdentityHf;
            }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return TargetEn; }
        }
        public HeAssumeIdentity(XDocument xdoc, World world)
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
                    case "trickster_hfid":
                        TricksterHfid = valI;
                        break;
                    case "identity_id":
                        IdentityId = valI;
                        break;
                    case "target_enid":
                        TargetEnId = valI;
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
            if (TricksterHfid.HasValue && World.HistoricalFigures.ContainsKey(TricksterHfid.Value))
                TricksterHf = World.HistoricalFigures[TricksterHfid.Value];
            if (TargetEnId.HasValue && World.Entities.ContainsKey(TargetEnId.Value))
                TargetEn = World.Entities[TargetEnId.Value];
            if (IdentityHfid.HasValue && World.HistoricalFigures.ContainsKey(IdentityHfid.Value))
                IdentityHf = World.HistoricalFigures[IdentityHfid.Value];
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
                        break;
                    case "identity_hf":
                        IdentityHfid = valI;
                        break;
                    case "identity_name":
                        IdentityName = val;
                        break;
                    case "identity_race":
                        if (valI != -1)
                            IdentityRace = World.GetAddRace(val);
                        break;
                    case "identity_caste":
                        if (valI != -1)
                        {
                            if (!HistoricalFigure.Castes.Contains(val))
                                HistoricalFigure.Castes.Add(val);
                            IdentityCaste = HistoricalFigure.Castes.IndexOf(val);
                        }
                        break;
                    case "civ":
                        break;

                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }



            
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Trickster:", TricksterHf);
            if (IdentityHf != null)
                EventLabel(frm, parent, ref location, "Identity:", IdentityHf);
            else if (IdentityRace != null && IdentityCaste != null)
            {
                EventLabel(frm, parent, ref location, "Identity Race:", IdentityRace);
                EventLabel(frm, parent, ref location, "Identity Race:", HistoricalFigure.Castes[IdentityCaste.Value]);
            }
            else
                EventLabel(frm, parent, ref location, "Identity ID:", IdentityId.ToString());
            EventLabel(frm, parent, ref location, "Target Ent:", TargetEn);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            return IdentityHf != null ? 
                $"{timestring} {TricksterHf.Race} {TricksterHf} fooled {TargetEn} into believing it was {IdentityHf}." : 
                $"{timestring} {TricksterHf.Race} {TricksterHf} fooled {TargetEn} into believing it was {IdentityName}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {TricksterHf} assumed an identity to {TargetEn}";
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                TricksterHfid.DBExport(), 
                TargetEnId.DBExport(), 
                IdentityId.DBExport(),
                IdentityHfid.DBExport(),
                IdentityName.DBExport(),
                IdentityRace.DBExport(),
                IdentityCaste.DBExport(HistoricalFigure.Castes)
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}


