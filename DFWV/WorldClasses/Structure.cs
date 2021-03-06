using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses
{
    public class Structure : XMLObject
    {
        public int SiteId { get; set; }
        private Site Site { get; }
        public int? WorshipHFid { get; set; }
        private HistoricalFigure WorshipHF { get; set; }
        public int? CopiedArtifactId { get; set; }
        private Artifact CopiedArtifact { get; set; }
        public int? EntityId { get; set; }
        private Entity Entity { get; set; }
        public int? LocalId { get; set; }

        [UsedImplicitly]
        public new string Name { get; set; }

        public static List<string> SubTypes = new List<string>();
        public int? SubType { get; set; }
        public static List<string> Types = new List<string>();
        public int? Type { get; set; }
        public static int NumStructures;
        private XElement child;

        public List<HistoricalEvent> Events { get; set; }

        public HE_RazedStructure RazedEvent { get { return (HE_RazedStructure) Events?.FirstOrDefault(e => HistoricalEvent.Types[e.Type] == "razed structure"); } }
        public HE_CreatedStructure CreatedEvent { get { return (HE_CreatedStructure) Events?.FirstOrDefault(e => HistoricalEvent.Types[e.Type] == "created structure"); } }
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

        public Structure(XElement structureXML, Site site) : base(site.World)
        {
            Site = site;
            SiteId = site.Id;

            World = site.World;


            foreach (var strElement in structureXML.Elements())
            {
                var strval = strElement.Value;
                int strvalI;
                int.TryParse(strval, out strvalI);
                switch (strElement.Name.LocalName)
                {
                    case "local_id":
                        LocalId = strvalI;
                        break;
                    case "type":
                        if (!Types.Contains(strval))
                            Types.Add(strval);
                        Type = Types.IndexOf(strval);
                        break;
                    case "worship_hfid":
                        WorshipHFid = strvalI;
                        break;
                    case "entity_id":
                        EntityId = strvalI;
                        break;
                    case "copied_artifact_id":
                        CopiedArtifactId = strvalI;
                        break;
                    case "subtype":
                        if (!SubTypes.Contains(strval))
                            SubTypes.Add(strval);
                        SubType = SubTypes.IndexOf(strval);
                        break;

                    case "name":
                        Name = strval;
                        break;
                    default:
                        Program.Log(LogType.Warning, $"Unexpected structure element : {strElement.Name.LocalName}");
                        break;

                }
            }


        }

        internal override void Link()
        {
            if (WorshipHFid.HasValue && World.HistoricalFigures.ContainsKey(WorshipHFid.Value))
                WorshipHF = World.HistoricalFigures[WorshipHFid.Value];
            if (EntityId.HasValue && World.Entities.ContainsKey(EntityId.Value))
                Entity = World.Entities[EntityId.Value];
            if (CopiedArtifactId.HasValue && World.Artifacts.ContainsKey(CopiedArtifactId.Value))
                CopiedArtifact = World.Artifacts[CopiedArtifactId.Value];
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
                frm.lblStructureCreatedSiteCiv.Data = CreatedEvent.Entity_SiteCiv;
                frm.lblStructureCreatedCiv.Data = CreatedEvent.Entity;
                frm.lblStructureCreatedSite.Data = CreatedEvent.Site;
                frm.lblStructureCreatedTime.Data = CreatedEvent;
                frm.lblStructureCreatedTime.Text = CreatedEvent.Time.ToString();
            }
            frm.grpStructureCreated.Visible = CreatedEvent != null;

            if (RazedEvent != null)
            {
                frm.lblStructureRazedCiv.Data = RazedEvent.Entity;
                frm.lblStructureRazedSite.Data = RazedEvent.Site;
                frm.lblStructureRazedTime.Data = RazedEvent;
                frm.lblStructureRazedTime.Text = RazedEvent.Time.ToString();
            }
            frm.grpStructureRazed.Visible = RazedEvent != null;

            frm.grpStructureEvents.FillListboxWith(frm.lstStructureEvents, Events);
            frm.grpStructureEntombedHF.FillListboxWith(frm.lstStructureEntombedHF, 
                ChangeHfBodyStateEvents.Where(changeHfBodyStateEvent => changeHfBodyStateEvent.BodyState == "entombed at site").Select(x=>x.Hf));



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
                return Name.ToTitleCase();
            if (Site.Structures == null || Site.Structures.Count < 10)
                return Site + " - " + SiteId;
            
            if (Site.Structures.Count < 100)
                return Site + " - " + SiteId.ToString().PadLeft(2,'0');

            return Site + " - " + SiteId.ToString().PadLeft(3, '0');
        }

        
    }
}