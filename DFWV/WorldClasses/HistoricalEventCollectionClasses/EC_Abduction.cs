﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using System.Collections.Concurrent;

namespace DFWV.WorldClasses.HistoricalEventCollectionClasses
{
    public class EC_Abduction : HistoricalEventCollection
    {
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private Point Coords { get; }
        private int? ParentEventCol_ { get; }
        private HistoricalEventCollection ParentEventCol { get; set; }
        private int Ordinal { get; }
        private int? DefendingEnid { get; }
        private Entity DefendingEn { get; set; }
        private int? AttackingEnid { get; }
        private Entity AttackingEn { get; set; }
        private List<int> EventCol_ { get; }
        private List<HistoricalEventCollection> EventCol { get; set; }

        override public Point Location => Site.Coords;

        public EC_Abduction(XDocument xdoc, World world)
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
                    case "start_year":
                    case "start_seconds72":
                    case "end_year":
                    case "end_seconds72":
                    case "event":
                    case "type":
                        break;

                    case "ordinal":
                        Ordinal = valI;
                        break;
                    case "parent_eventcol":
                        ParentEventCol_ = valI;
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SubregionId = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerId = valI;
                        break;
                    case "eventcol":
                        if (EventCol_ == null)
                            EventCol_ = new List<int>();
                        EventCol_.Add(valI);
                        break;
                    case "site_id":
                        if (valI != -1)
                            SiteId = valI;
                        break;
                    case "coords":
                        Coords = new Point(Convert.ToInt32(val.Split(',')[0]), Convert.ToInt32(val.Split(',')[1]));
                        break;
                    case "defending_enid":
                        DefendingEnid = valI;
                        break;
                    case "attacking_enid":
                        AttackingEnid = valI;
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
            if (ParentEventCol_.HasValue && World.HistoricalEventCollections.ContainsKey(ParentEventCol_.Value))
                ParentEventCol = World.HistoricalEventCollections[ParentEventCol_.Value];
            if (EventCol_ != null)
                EventCol = new List<HistoricalEventCollection>();
            LinkFieldList(EventCol_,
                EventCol, World.HistoricalEventCollections);

            if (SubregionId.HasValue && World.Regions.ContainsKey(SubregionId.Value))
                Subregion = World.Regions[SubregionId.Value];
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (DefendingEnid.HasValue && World.Entities.ContainsKey(DefendingEnid.Value))
                DefendingEn = World.Entities[DefendingEnid.Value];
            if (AttackingEnid.HasValue && World.Entities.ContainsKey(AttackingEnid.Value))
                AttackingEn = World.Entities[AttackingEnid.Value];

        }

        public override void Select(MainForm frm)
        {

            base.Select(frm);

            foreach (var tabpage in frm.MainTabEventCollectionTypes.TabPages.Cast<TabPage>().Where(tabpage => tabpage != frm.tabEventCollectionAbduction))
            {
                frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionAbduction))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionAbduction);

            frm.lblAbductionRegion.Data = Subregion;
            frm.lblAbductionSite.Data = Site;
            frm.lblAbductionCoords.Data = new Coordinate(Coords);
            frm.lblAbductionAttacker.Data = AttackingEn;
            frm.lblAbductionDefender.Data = DefendingEn;
            frm.lblAbductionParent.Data = ParentEventCol;
            if (StartTime != null || EndTime != null)
            {
                frm.lblAbductionTime.Text = $"{StartTime} - {EndTime}";
                frm.lblAbductionDuration.Text = WorldTime.Duration(EndTime, StartTime);
            }
            else
            {
                frm.lblAbductionTime.Text = "";
                frm.lblAbductionDuration.Text = "";
            }
            frm.lblAbductionOrdinal.Text = Ordinal.ToString();

            frm.lstAbductionEvents.Items.Clear();
            if (Event != null)
                frm.lstAbductionEvents.Items.AddRange(Event.ToArray());

            frm.grpAbductionEvents.Visible = frm.lstAbductionEvents.Items.Count > 0;

            if (frm.lstAbductionEvents.Items.Count > 0)
                frm.lstAbductionEvents.SelectedIndex = 0;

            frm.lstAbductionEventCols.Items.Clear();
            if (EventCol != null)
                frm.lstAbductionEventCols.Items.AddRange(EventCol.ToArray());

            frm.grpAbductionEventCols.Visible = frm.lstAbductionEventCols.Items.Count > 0;

            SelectTab(frm);
        }

        public override void Process()
        {
            base.Process();
            if (Subregion != null)
            {
                if (Subregion.AbductionEventCollections == null)
                    Subregion.AbductionEventCollections = new ConcurrentBag<EC_Abduction>();
                Subregion.AbductionEventCollections.Add(this);
            }
            if (Site != null)
            {
                if (Site.AbductionEventCollections == null)
                    Site.AbductionEventCollections = new ConcurrentBag<EC_Abduction>();
                Site.AbductionEventCollections.Add(this);
            }
            if (AttackingEn.AbductionEventCollections == null)
                AttackingEn.AbductionEventCollections = new ConcurrentBag<EC_Abduction>();
            AttackingEn.AbductionEventCollections.Add(this);
            if (DefendingEn.AbductionEventCollections == null)
                DefendingEn.AbductionEventCollections = new ConcurrentBag<EC_Abduction>();
            DefendingEn.AbductionEventCollections.Add(this);

        }

        internal override void Evaluate()
        {
            base.Evaluate();

            // For abduction event collections, if we have a new HF entity link following an hf abducted link, 
            //      then we can say that the hf entity link is of type "prisoner" or "former prisoner", and the HF in 
            //       Add HF Entity link is the one that was abducted in our abduction event.
            for (var i = 1; i < Event.Count; i++)
            {
                if (HistoricalEvent.Types[Event[i].Type] != "add hf entity link" ||
                    HistoricalEvent.Types[Event[i - 1].Type] != "hf abducted")
                    continue;
                var abductedHf = ((HE_HFAbducted)Event[i - 1]).Hf_Target;
                var addLinkEvent = ((HE_AddHFEntityLink)Event[i]);
                addLinkEvent.Hf = abductedHf;

                if (abductedHf != null)
                {
                    if (abductedHf.EntityLinks.ContainsKey(HFEntityLink.LinkTypes.IndexOf("prisoner")))
                    {
                        foreach (
                            var entityLink in
                                abductedHf.EntityLinks[HFEntityLink.LinkTypes.IndexOf("prisoner")].Where(
                                    entityLink => entityLink.Entity == addLinkEvent.Entity))
                        {
                            addLinkEvent.HfEntityLink = entityLink;
                            break;
                        }
                    }
                    if ((addLinkEvent.HfEntityLink?.Hf == null) &&
                        abductedHf.EntityLinks.ContainsKey(HFEntityLink.LinkTypes.IndexOf("former prisoner")))
                    {
                        foreach (
                            var entityLink in
                                abductedHf.EntityLinks[HFEntityLink.LinkTypes.IndexOf("former prisoner")].Where(
                                    entityLink => entityLink.Entity == addLinkEvent.Entity))
                        {
                            addLinkEvent.HfEntityLink = entityLink;
                            break;
                        }
                    }
                }

            }
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object> { Id, ParentEventCol, Ordinal, AttackingEnid, DefendingEnid, SiteId, SubregionId, FeatureLayerId };

            if (Coords.IsEmpty)
                vals.Add(DBNull.Value);
            else
                vals.Add(Coords.X + "," + Coords.Y);

            Database.ExportWorldItem(table, vals);

        }

    }
}
