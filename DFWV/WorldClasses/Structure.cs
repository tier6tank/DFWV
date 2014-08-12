using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    using HistoricalEventClasses;
    using System.Collections.Generic;
    using System.Drawing;

    class Structure : XMLObject
    {
        private Site Site { get; set; }
        public int SiteID { get; set; }

        [UsedImplicitly]
        public new string Name { get; set; }

        public static List<string> Types = new List<string>();
        public int? Type { get; set; }
        public static int numStructures;

        public List<HistoricalEvent> Events { get; set; }

        public HE_RazedStructure RazedEvent { get { return (HE_RazedStructure) Events.FirstOrDefault(e => HistoricalEvent.Types[e.Type] == "razed structure"); } }
        public HE_CreatedStructure CreatedEvent { get { return (HE_CreatedStructure) Events.FirstOrDefault(e => HistoricalEvent.Types[e.Type] == "created structure"); } }
        [UsedImplicitly]
        public bool isRazed { get { return RazedEvent != null; } }
        [UsedImplicitly]
        public string StructureType { get { return Type.HasValue ? Types[Type.Value] : "Unknown"; } }

        public IEnumerable<HE_ChangeHFBodyState> ChangeHFBodyStateEvents { get { return Events != null ? Events.Where(e => HistoricalEvent.Types[e.Type] == "change hf body state").Cast<HE_ChangeHFBodyState>() : null; } }
        [UsedImplicitly]
        public bool Tomb { get { return ChangeHFBodyStateEvents != null && (ChangeHFBodyStateEvents.Count(f=>f.BodyState == "entombed at site") > 0); } }
        [UsedImplicitly]
        public string DispNameLower { get { return ToString().ToLower(); } }


        override public Point Location { get { return Site.Location; } }

        public Structure(Site site, int id, World world) : base(world)
        {
            Site = site;
            SiteID = id;
            ID = numStructures;
            numStructures++;
            World = world;
        }

        public override void Select(MainForm frm)
        {
            frm.grpStructure.Text = ToString();
            frm.grpStructure.Show();

            frm.lblStructureID.Text = SiteID.ToString();
            frm.lblStructureSite.Data = Site;
            frm.lblStructureType.Text = StructureType;

            if (CreatedEvent != null)
            {
                frm.lblStructureCreatedSiteCiv.Data = CreatedEvent.SiteCiv;
                frm.lblStructureCreatedCiv.Data = CreatedEvent.Civ;
                frm.lblStructureCreatedSite.Data = CreatedEvent.Site;
                frm.lblStructureCreatedTime.Data = CreatedEvent;
                frm.lblStructureCreatedTime.Text = CreatedEvent.Time.ToString();
            }
            frm.grpStructureCreated.Visible = CreatedEvent != null;

            if (RazedEvent != null)
            {
                frm.lblStructureRazedCiv.Data = RazedEvent.Civ;
                frm.lblStructureRazedSite.Data = RazedEvent.Site;
                frm.lblStructureRazedTime.Data = RazedEvent;
                frm.lblStructureRazedTime.Text = RazedEvent.Time.ToString();
            }
            frm.grpStructureRazed.Visible = RazedEvent != null;

            frm.lstStructureEvents.Items.Clear();
            if (Events != null)
                frm.lstStructureEvents.Items.AddRange(Events.ToArray());
            frm.grpStructureEvents.Visible = frm.lstStructureEvents.Items.Count > 0;


            frm.lstStructureEntombedHF.Items.Clear();
            if (ChangeHFBodyStateEvents != null)
            {
                foreach (var changeHfBodyStateEvent in ChangeHFBodyStateEvents)
                {
                    if (changeHfBodyStateEvent.BodyState == "entombed at site")
                        frm.lstStructureEntombedHF.Items.Add(changeHfBodyStateEvent.HF);
                }
            }
            frm.grpStructureEntombedHF.Visible = frm.lstStructureEntombedHF.Items.Count > 0;

            Program.MakeSelected(frm.tabStructure, frm.lstStructure, this);
        }

        internal override void Link()
        {

        }

        internal override void Process()
        {

        }
        internal override void Plus(XDocument xdoc)
        {

        }

        internal override void Export(string table)
        {
            var vals = new List<object>
            {
                ID, 
                Site.ID, 
                SiteID,
                Type.DBExport(Types),
                Name.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Name))
                return Name;
            if (Site.Structures == null || Site.Structures.Count < 10)
                return Site + " - " + SiteID;
            
            if (Site.Structures.Count < 100)
                return Site + " - " + SiteID.ToString().PadLeft(2,'0');

            return Site + " - " + SiteID.ToString().PadLeft(3, '0');
        }

        
    }
}
