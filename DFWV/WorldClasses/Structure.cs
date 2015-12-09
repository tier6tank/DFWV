using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalEventClasses;

namespace DFWV.WorldClasses
{
    public class Structure : XMLObject
    {
        private Site Site { get; }
        public int SiteId { get; set; }

        [UsedImplicitly]
        public new string Name { get; set; }

        public static List<string> Types = new List<string>();
        public int? Type { get; set; }
        public static int NumStructures;

        public List<HistoricalEvent> Events { get; set; }

        public HE_RazedStructure RazedEvent { get { return (HE_RazedStructure) Events.FirstOrDefault(e => HistoricalEvent.Types[e.Type] == "razed structure"); } }
        public HE_CreatedStructure CreatedEvent { get { return (HE_CreatedStructure) Events.FirstOrDefault(e => HistoricalEvent.Types[e.Type] == "created structure"); } }
        [UsedImplicitly]
        public bool IsRazed => RazedEvent != null;

        [UsedImplicitly]
        public string StructureType => Type.HasValue ? Types[Type.Value] : "Unknown";

        public IEnumerable<HE_ChangeHFBodyState> ChangeHfBodyStateEvents { get { return Events?.Where(e => HistoricalEvent.Types[e.Type] == "change hf body state").Cast<HE_ChangeHFBodyState>(); } }
        [UsedImplicitly]
        public bool Tomb { get { return ChangeHfBodyStateEvents != null && (ChangeHfBodyStateEvents.Count(f=>f.BodyState == "entombed at site") > 0); } }
        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();


        override public Point Location => Site.Location;

        public Structure(Site site, int id, World world) : base(world)
        {
            Site = site;
            SiteId = id;
            Id = NumStructures;
            NumStructures++;
            World = world;
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpStructure.Text == ToString() && frm.MainTab.SelectedTab == frm.tabStructure)
                return;
            Program.MakeSelected(frm.tabStructure, frm.lstStructure, this);

            frm.grpStructure.Text = ToString();
            frm.grpStructure.Show();

            frm.lblStructureID.Text = SiteId.ToString();
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

            frm.grpStructureEvents.FillListboxWith(frm.lstStructureEvents, Events);
            frm.grpStructureEntombedHF.FillListboxWith(frm.lstStructureEntombedHF, 
                ChangeHfBodyStateEvents.Where(changeHfBodyStateEvent => changeHfBodyStateEvent.BodyState == "entombed at site").Select(x=>x.Hf));



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
                Id, 
                Site.Id, 
                SiteId,
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
                return Site + " - " + SiteId;
            
            if (Site.Structures.Count < 100)
                return Site + " - " + SiteId.ToString().PadLeft(2,'0');

            return Site + " - " + SiteId.ToString().PadLeft(3, '0');
        }

        
    }
}