﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_MasterpieceEngraving : HistoricalEvent
    {
        private int? HfId { get; }
        private HistoricalFigure Hf { get; set; }
        private int? EntityId { get; }
        private Entity Entity { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SkillAtTime { get; }

        public int? Mat { get; set; }
        public int? ArtId { get; set; }
        public int? ArtSubId { get; set; }
        public Engraving Engraving { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Entity; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }


        public HE_MasterpieceEngraving(XDocument xdoc, World world)
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
                    case "hfid":
                        HfId = valI;
                        break;
                    case "entity_id":
                        EntityId = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "skill_at_time":
                        SkillAtTime = valI;
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
                    case "art_id":
                        ArtId = valI;
                        break;
                    case "art_subid":
                        ArtSubId = valI;
                        break;
                    case "mat":
                        if (!Item.Materials.Contains(val))
                            Item.Materials.Add(val);
                        Mat = Item.Materials.IndexOf(val);
                        break;
                    case "maker":
                    case "maker_entity":
                    case "site":
                    case "skill_rating":
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Process()
        {
            base.Process();
            if (Site != null)
                Site.IsPlayerControlled = true;
            Entity?.MakePlayer();
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Entity:", Entity);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Skill:", SkillAtTime.ToString());
        }

        protected override string LegendsDescription()
        {
            //TODO: Incorporate new data
            var timestring = base.LegendsDescription();

            return
                $"{timestring} {Hf} created a masterful engraving \"{"Art ID: " + ArtId}\" for {Entity} at {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Hf} created a masterful engraving for {Entity} at {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                HfId.DBExport(), 
                EntityId.DBExport(), 
                SiteId.DBExport(), 
                SkillAtTime,
                ArtId.DBExport(),
                ArtSubId.DBExport(),
                Mat.DBExport(Item.Materials)
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}

