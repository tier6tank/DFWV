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
        private int? HfId_Trickster { get; set; }
        private HistoricalFigure Hf_Trickster { get; set; }
        private int? EntityId_Target { get; set; }
        private Entity Entity_Target { get; set; }
        private int? IdentityId { get; }

        public int? HfId_Identity { get; set; }
        public HistoricalFigure Hf_Identity { get; set; }
        public Race IdentityRace { get; set; }
        public int? IdentityCaste { get; set; }
        public string IdentityName { get; set; }
        
        override public Point Location => Entity_Target.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return Hf_Trickster;
                yield return Hf_Identity;
            }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Entity_Target; }
        }
        public HE_AssumeIdentity(XDocument xdoc, World world)
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
                        HfId_Trickster = valI;
                        break;
                    case "identity_id":
                        IdentityId = valI;
                        break;
                    case "target_enid":
                        EntityId_Target = valI;
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
                    case "histfig":
                        break;
                    case "identity_hf":
                        HfId_Identity = valI;
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
                    case "trickster":
                        HfId_Trickster = valI;
                        break;
                    case "target":
                        EntityId_Target = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }



            
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Trickster:", Hf_Trickster);
            if (Hf_Identity != null)
                EventLabel(frm, parent, ref location, "Identity:", Hf_Identity);
            else if (IdentityRace != null && IdentityCaste != null)
            {
                EventLabel(frm, parent, ref location, "Identity Race:", IdentityRace);
                EventLabel(frm, parent, ref location, "Identity Race:", HistoricalFigure.Castes[IdentityCaste.Value]);
            }
            else
                EventLabel(frm, parent, ref location, "Identity ID:", IdentityId.ToString());
            EventLabel(frm, parent, ref location, "Target Ent:", Entity_Target);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            return Hf_Identity != null ? 
                $"{timestring} {Hf_Trickster.Race} {Hf_Trickster} fooled {Entity_Target} into believing it was {Hf_Identity}." : 
                $"{timestring} {Hf_Trickster.Race} {Hf_Trickster} fooled {Entity_Target} into believing it was {IdentityName}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Hf_Trickster} assumed an identity to {Entity_Target}";
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                HfId_Trickster.DBExport(), 
                EntityId_Target.DBExport(), 
                IdentityId.DBExport(),
                HfId_Identity.DBExport(),
                IdentityName.DBExport(),
                IdentityRace.DBExport(),
                IdentityCaste.DBExport(HistoricalFigure.Castes)
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}


