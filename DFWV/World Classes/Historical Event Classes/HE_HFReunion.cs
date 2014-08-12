using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFReunion : HistoricalEvent
    {
        public List<int> Group1HFID { get; set; }
        public List<HistoricalFigure> Group1HF { get; set; }
        public List<int> Group2HFID { get; set; }
        public List<HistoricalFigure> Group2HF { get; set; }
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int? SubregionID { get; set; }
        public Region Subregion { get; set; }
        public int? FeatureLayerID { get; set; }

        override public Point Location { get { return Site.Location; } }

        public HE_HFReunion(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
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
                    case "group_1_hfid":
                        if (Group1HFID == null)
                            Group1HFID = new List<int>();
                        Group1HFID.Add(valI);
                        break;
                    case "group_2_hfid":
                        if (Group2HFID == null)
                            Group2HFID = new List<int>();
                        Group2HFID.Add(valI);
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + HistoricalEvent.Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }
        internal override void Link()
        {
            base.Link();
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
            if (Group1HFID != null)
            {
                Group1HF = new List<HistoricalFigure>();
                foreach (int group1hfid in Group1HFID)
                {
                    if (World.HistoricalFigures.ContainsKey(group1hfid))
                        Group1HF.Add(World.HistoricalFigures[group1hfid]);
                }
            }
            if (Group2HFID != null)
            {
                Group2HF = new List<HistoricalFigure>();
                foreach (int group2hfid in Group2HFID)
                {
                    if (World.HistoricalFigures.ContainsKey(group2hfid))
                        Group2HF.Add(World.HistoricalFigures[group2hfid]);
                }
            }
        }


        internal override void Process()
        {
            base.Process();
            if (Group1HF != null)
            {
                foreach (HistoricalFigure hf in Group1HF)
                {
                    if (hf.Events == null)
                        hf.Events = new List<HistoricalEvent>();
                    hf.Events.Add(this);
                }
            }
            if (Group2HF != null)
            {
                foreach (HistoricalFigure hf in Group2HF)
                {
                    if (hf.Events == null)
                        hf.Events = new List<HistoricalEvent>();
                    hf.Events.Add(this);
                }
            }
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            foreach (HistoricalFigure hf in Group1HF)
                EventLabel(frm, parent, ref location, "Group 1:", hf);
            foreach (HistoricalFigure hf in Group2HF)
                EventLabel(frm, parent, ref location, "Group 2:", hf);

            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            EventLabel(frm, parent, ref location, "Layer:", FeatureLayerID == -1 ? "" : FeatureLayerID.ToString());
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            return string.Format("{0} {1} was reunited with {2}{3} in {4}.",
                           timestring, Group1HF[0].ToString(), Group2HF.Count == 2 ? " and " + Group2HF[1].ToString() : "",
                           Group2HF[0].ToString(), Site.AltName);
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            if (Group2HF.Count == Group2HFID.Count && Group1HF.Count == Group1HFID.Count)
                return string.Format("{0} {1} was reunited with {2}{3} in {4}.",
                               timelinestring, Group1HF[0].ToString(), Group2HF.Count == 2 ? " and " + Group2HF[1].ToString() : "",
                               Group2HF[0].ToString(), Site.AltName);
            else
                return string.Format("{0} {1} was reunited with {2}{3} in {4}.",
                               timelinestring, Group1HFID[0].ToString(), Group2HF.Count == 2 ? " and " + Group2HFID[1].ToString() : "",
                               Group2HFID[0].ToString(), Site.AltName);

        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();


            
            vals = new List<object>() { ID };

            if (Group1HFID != null && Group1HFID.Count == 2)
            {
                vals.Add(Group1HFID[0]);
                vals.Add(Group1HFID[1]);
            }
            else if (Group1HFID != null && Group1HFID.Count == 1)
            {
                vals.Add(Group1HFID[0]);
                vals.Add(DBNull.Value);
            }
            else
            {
                vals.Add(DBNull.Value);
                vals.Add(DBNull.Value);
            }
            if (Group2HFID != null && Group2HFID.Count == 2)
            {
                vals.Add(Group2HFID[0]);
                vals.Add(Group2HFID[1]);
            }
            else if (Group2HFID != null && Group2HFID.Count == 1)
            {
                vals.Add(Group2HFID[0]);
                vals.Add(DBNull.Value);
            }
            else
            {
                vals.Add(DBNull.Value);
                vals.Add(DBNull.Value);
            }





            vals.AddRange(new List<object>() { SiteID, SubregionID, FeatureLayerID });


            Database.ExportWorldItem(table, vals);

        }

    }
}