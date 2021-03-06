﻿using System.Collections.Generic;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using System.Windows.Forms;
using System.Drawing;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_AgreementConcluded : HistoricalEvent
    {
        private int? SiteId { get; set; }
        private Site Site { get; set; }

        public int? Topic { get; set; }
        public int? Result { get; set; }
        public int? EntityId_Source { get; set; }
        public int? EntityId_Destination { get; set; }
        public Entity Entity_Source { get; set; }
        public Entity Entity_Destination { get; set; }

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity_Source;
                yield return Entity_Destination;
            }
        }

        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }
        public HE_AgreementConcluded(XDocument xdoc, World world)
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
                    case "topic":
                        if (!MeetingTopics.Contains(val))
                            MeetingTopics.Add(val);
                        Topic = MeetingTopics.IndexOf(val);
                        break;
                    case "source":
                        EntityId_Source = valI;
                        break;
                    case "destination":
                        EntityId_Destination = valI;
                        break;
                    case "site":
                        SiteId = valI;
                        break;
                    case "result":
                        if (!MeetingResults.Contains(val))
                            MeetingResults.Add(val);
                        Result = MeetingResults.IndexOf(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            base.WriteDataOnParent(frm, parent, ref location);
        }

        protected override string LegendsDescription()
        {
            //TODO: Incorporate new data
            var timestring = base.LegendsDescription();

            
            return $"{timestring} agreement concluded.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} agreement concluded.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id,
                Topic.DBExport(MeetingTopics),
                EntityId_Source.DBExport(),
                EntityId_Destination.DBExport(),
                SiteId.DBExport(),
                Result.DBExport(MeetingResults)
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}