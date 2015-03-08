using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_AssumeIdentity : HistoricalEvent
    {
        private int? TricksterHFID { get; set; }
        private HistoricalFigure TricksterHF { get; set; }
        private int? TargetEnID { get; set; }
        private Entity TargetEn { get; set; }
        private int? IdentityID { get; set; }

        public int? IdentityHFID { get; set; }
        public HistoricalFigure IdentityHF { get; set; }
        public Race IdentityRace { get; set; }
        public int? IdentityCaste { get; set; }
        public string IdentityName { get; set; }
        
        override public Point Location { get { return TargetEn.Location; } }

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return TricksterHF;
                yield return IdentityHF;
            }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return TargetEn; }
        }
        public HE_AssumeIdentity(XDocument xdoc, World world)
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
                    case "trickster_hfid":
                        TricksterHFID = valI;
                        break;
                    case "identity_id":
                        IdentityID = valI;
                        break;
                    case "target_enid":
                        TargetEnID = valI;
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
            if (TricksterHFID.HasValue && World.HistoricalFigures.ContainsKey(TricksterHFID.Value))
                TricksterHF = World.HistoricalFigures[TricksterHFID.Value];
            if (TargetEnID.HasValue && World.Entities.ContainsKey(TargetEnID.Value))
                TargetEn = World.Entities[TargetEnID.Value];
            if (IdentityHFID.HasValue && World.HistoricalFigures.ContainsKey(IdentityHFID.Value))
                IdentityHF = World.HistoricalFigures[IdentityHFID.Value];
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
                    case "histfig":
                        break;
                    case "identity_hf":
                        IdentityHFID = valI;
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
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }



            
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Trickster:", TricksterHF);
            if (IdentityHF != null)
                EventLabel(frm, parent, ref location, "Identity:", IdentityHF);
            else if (IdentityRace != null && IdentityCaste != null)
            {
                EventLabel(frm, parent, ref location, "Identity Race:", IdentityRace);
                EventLabel(frm, parent, ref location, "Identity Race:", HistoricalFigure.Castes[IdentityCaste.Value]);
            }
            else
                EventLabel(frm, parent, ref location, "Identity ID:", IdentityID.ToString());
            EventLabel(frm, parent, ref location, "Target Ent:", TargetEn);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (IdentityHF != null)
                return string.Format("{0} {1} {2} fooled {3} into believing it was {4}.",
                    timestring, TricksterHF.Race, TricksterHF,
                    TargetEn, IdentityHF == null ? "Identity ID: " + IdentityID : IdentityHF.ToString());
            return string.Format("{0} {1} {2} fooled {3} into believing it was {4}.",
                timestring, TricksterHF.Race, TricksterHF,
                TargetEn, IdentityName);
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} assumed an identity to {2}",
                        timelinestring, TricksterHF,
                            TargetEn);
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object>
            {
                ID, 
                TricksterHFID.DBExport(), 
                TargetEnID.DBExport(), 
                IdentityID.DBExport(),
                IdentityHFID.DBExport(),
                IdentityName.DBExport(),
                IdentityRace.DBExport(),
                IdentityCaste.DBExport(HistoricalFigure.Castes)
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}


