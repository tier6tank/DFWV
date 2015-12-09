using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using LinkLabel = DFWV.Controls.LinkLabel;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HistoricalEvent : XmlObject
    {
        [UsedImplicitly]
        public int Year { get; set; }
        private int Seconds { get; }
        public int Type { get; }
        public static List<string> Types = new List<string>();

        public IEnumerable<XmlObject> Relationships => from propertyInfo in GetType().GetProperties() where propertyInfo.GetValue(this, null) is XmlObject select propertyInfo.GetValue(this, null) as XmlObject;

        public virtual IEnumerable<HistoricalFigure> HFsInvolved => Enumerable.Empty<HistoricalFigure>();

        public virtual IEnumerable<Entity> EntitiesInvolved => Enumerable.Empty<Entity>();

        public virtual IEnumerable<Site> SitesInvolved => Enumerable.Empty<Site>();

        public virtual IEnumerable<Region> RegionsInvolved => Enumerable.Empty<Region>();

        public HistoricalEventCollection EventCollection { get; set; }
        public WorldTime Time => new WorldTime(Year, Seconds);

        [UsedImplicitly]
        public bool InCollection => EventCollection != null;

        [UsedImplicitly]
        public string EventType => Types[Type];

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        override public Point Location => Point.Empty;

        public static List<string> ItemTypes = new List<string>();
        public static List<string> ItemSubTypes = new List<string>();
        public static List<string> Materials = new List<string>();
        public static List<string> MeetingTopics = new List<string>();
        public static List<string> MeetingResults = new List<string>();
        public static List<string> Buildings = new List<string>();

        public static HistoricalEvent Create(XDocument xdoc, World world)
        {
            switch (xdoc.Root.Element("type").Value)
            {
                case "create entity position":
                    return new HeCreateEntityPosition(xdoc, world);
                case "merchant":
                    return new HeMerchant(xdoc, world);
                case "item stolen":
                    return new HeItemStolen(xdoc, world);
                case "masterpiece lost":
                    return new HeMasterpieceLost(xdoc, world);
                case "hf simple battle event":
                    return new HeHfSimpleBattleEvent(xdoc, world);
                case "change hf state":
                    return new HeChangeHfState(xdoc, world);
                case "add hf entity link":
                    return new HeAddHfEntityLink(xdoc, world);
                case "created site":
                    return new HeCreatedSite(xdoc, world);
                case "add hf hf link":
                    return new HeAddHfhfLink(xdoc, world);
                case "entity created":
                    return new HeEntityCreated(xdoc, world);
                case "add hf site link":
                    return new HeAddHfSiteLink(xdoc, world);
                case "created structure":
                    return new HeCreatedStructure(xdoc, world);
                case "change hf job":
                    return new HeChangeHfJob(xdoc, world);
                case "hf travel":
                    return new HeHfTravel(xdoc, world);
                case "hf new pet":
                    return new HeHfNewPet(xdoc, world);
                case "field battle":
                    return new HeFieldBattle(xdoc, world);
                case "peace accepted":
                    return new HePeaceAccepted(xdoc, world);
                case "hf died":
                    return new HeHfDied(xdoc, world);
                case "hf wounded":
                    return new HeHfWounded(xdoc, world);
                case "creature devoured":
                    return new HeCreatureDevoured(xdoc, world);
                case "attacked site":
                    return new HeAttackedSite(xdoc, world);
                case "hf profaned structure":
                    return new HeHfProfanedStructure(xdoc, world);
                case "hf does interaction":
                    return new HeHfDoesInteraction(xdoc, world);
                case "hf abducted":
                    return new HeHfAbducted(xdoc, world);
                case "changed creature type":
                    return new HeChangedCreatureType(xdoc, world);
                case "entity relocate":
                    return new HeEntityRelocate(xdoc, world);
                case "assume identity":
                    return new HeAssumeIdentity(xdoc, world);
                case "remove hf entity link":
                    return new HeRemoveHfEntityLink(xdoc, world);
                case "entity primary criminals":
                    return new HeEntityPrimaryCriminals(xdoc, world);
                case "hf reunion":
                    return new HeHfReunion(xdoc, world);
                case "plundered site":
                    return new HePlunderedSite(xdoc, world);
                case "body abused":
                    return new HeBodyAbused(xdoc, world);
                case "hf gains secret goal":
                    return new HeHfGainsSecretGoal(xdoc, world);
                case "hf learns secret":
                    return new HeHfLearnsSecret(xdoc, world);
                case "created world construction":
                    return new HeCreatedWorldConstruction(xdoc, world);
                case "remove hf site link":
                    return new HeRemoveHfSiteLink(xdoc, world);
                case "artifact created":
                    return new HeArtifactCreated(xdoc, world);
                case "artifact possessed":
                    return new HeArtifactPossessed(xdoc, world);
                case "hf confronted":
                    return new HeHfConfronted(xdoc, world);
                case "peace rejected":
                    return new HePeaceRejected(xdoc, world);
                case "reclaim site":
                    return new HeReclaimSite(xdoc, world);
                case "razed structure":
                    return new HeRazedStructure(xdoc, world);
                case "artifact stored":
                    return new HeArtifactStored(xdoc, world);
                case "entity law":
                    return new HeEntityLaw(xdoc, world);
                case "masterpiece item":
                    return new HeMasterpieceItem(xdoc, world);
                case "artifact lost":
                    return new HeArtifactLost(xdoc, world);
                case "diplomat lost":
                    return new HeDiplomatLost(xdoc, world);
                case "hf revived":
                    return new HeHfRevived(xdoc, world);
                case "agreement rejected":
                    return new HeAgreementRejected(xdoc, world);
                case "first contact":
                    return new HeFirstContact(xdoc, world);
                case "destroyed site":
                    return new HeDestroyedSite(xdoc, world);
                case "change hf body state":
                    return new HeChangeHfBodyState(xdoc, world);
                case "new site leader":
                    return new HeNewSiteLeader(xdoc, world);
                case "site taken over":
                    return new HeSiteTakenOver(xdoc, world);
                case "site abandoned":
                    return new HeSiteAbandoned(xdoc, world);
                case "masterpiece arch constructed":
                    return new HeMasterpieceArchConstructed(xdoc, world);
                case "masterpiece food":
                    return new HeMasterpieceFood(xdoc, world);
                case "masterpiece engraving":
                    return new HeMasterpieceEngraving(xdoc, world);
                case "masterpiece item improvement":
                    return new HeMasterpieceItemImprovement(xdoc, world);
                case "agreement made":
                    return new HeAgreementMade(xdoc, world);
                case "masterpiece dye":
                    return new HeMasterpieceDye(xdoc, world);
                case "first contact failed":
                    return new HeFirstContactFailed(xdoc, world);
                case "masterpiece arch design":
                    return new HeMasterpieceArchDesign(xdoc, world);
                case "agreement concluded":
                    return new HeAgreementConcluded(xdoc, world);
                case "hf reach summit":
                    return new HeHfReachSummit(xdoc, world);
                case "site died":
                    return new HeSiteDied(xdoc, world);
                case "hf disturbed structure":
                    return new HeHfDisturbedStructure(xdoc, world);
                case "site dispute":
                    return new HeSiteDispute(xdoc, world);
                case "agreement formed":
                    return new HeAgreementFormed(xdoc, world);
                case "replaced structure":
                    return new HeReplacedStructure(xdoc, world);
                case "hf attacked site":
                    return new HeHfAttackedSite(xdoc, world);
                case "hf destroyed site":
                    return new HeHfDestroyedSite(xdoc, world);
                case "site tribute forced":
                    return new HeSiteTributeForced(xdoc, world);
                case "site retired":
                    return new HeSiteRetired(xdoc, world);
                case "ceremony":
                    return new HeCeremony(xdoc, world);
                case "procession":
                    return new HeProcession(xdoc, world);
                case "performance":
                    return new HePerformance(xdoc, world);
                case "competition":
                    return new HeCompetition(xdoc, world);
                case "written content composed":
                    return new HeWrittenContentComposed(xdoc, world);
                case "hf relationship denied":
                    return new HeHfRelationshipDenied(xdoc, world);
                case "knowledge discovered":
                    return new HeKnowledgeDiscovered(xdoc, world);
                case "poetic form created":
                    return new HePoeticFormCreated(xdoc, world);
                case "dance form created":
                    return new HeDanceFormCreated(xdoc, world);
                case "musical form created":
                    return new HeMusicalFormCreated(xdoc, world);
                case "artifact destroyed":
                    return new HeArtifactDestroyed(xdoc, world);
                case "regionpop incorporated into entity":
                    return new HeRegionPopIncorporatedIntoEntity(xdoc, world);

                // ReSharper disable RedundantCaseLabel
                case "agreement void": //Unknown events
                case "hf razed structure":
                case "remove hf hf link":
                case "artifact hidden":
                case "artifact found":
                case "artifact recovered":
                case "artifact dropped":
                case "entity incorporated":
                case "impersonate hf":


// ReSharper restore RedundantCaseLabel
                default:
                    var logtext = "";
                    if (xdoc.Root.Element("type").Value != "hf disturbed structure" && xdoc.Root.Element("type").Value != "site died" && xdoc.Root.Element("type").Value != "agreement concluded" && xdoc.Root.Element("type").Value != "hf reach summit")
                        logtext = "Unassessed Event Type: " + (xdoc.Root.Element("type").Value);// + raw.Replace("<", "//<") + "\n\t\t\tbreak;");
#if DEBUG
                    logtext = ("\t\t" + xdoc.Root.ToString().Replace("<", "//<")).Split('\n').Where(
                        ln => !ln.Contains("<historical_event>") && 
                            !ln.Contains("<id>") && 
                            !ln.Contains("<year>") && 
                            !ln.Contains("<seconds72>") && 
                            !ln.Contains("<type>") && 
                            !ln.Contains("</historical_event>")
                            ).Aggregate(logtext, (current, ln) => current + ln);
                    logtext += "\t\t\treturn new HistoricalEvent(xdoc, world);";
#else
                    foreach (var ln in xdoc.Root.ToString().Split('\n'))
                    {
                        if (!ln.Contains("<historical_event>") && 
                            !ln.Contains("<id>") && 
                            !ln.Contains("<year>") &&
                            !ln.Contains("<seconds72>") &&
                            !ln.Contains("<type>") &&
                            !ln.Contains("</historical_event>"))
                                logtext += ln;
                    }
#endif
                    Program.Log(LogType.Warning, logtext);
                    return new HeUnassessedEvent(xdoc, world);
                    //Clipboard.SetText("\t\t\t\tcase \"" + Type + "\":\n" + raw.Replace("\t\t", "\t\t//") + "\n");
            }
        }

        public HistoricalEvent(XDocument xdoc, World world)
            : base(xdoc, world)
        {

            Year = int.Parse(xdoc.Root.Element("year").Value);
            Seconds = int.Parse(xdoc.Root.Element("seconds72").Value);

            var type = xdoc.Root.Element("type").Value;
            if (!Types.Contains(type))
                Types.Add(type);
            Type = Types.IndexOf(type);

            if (Time > WorldTime.Present)
                WorldTime.Present = Time;

        }

        public override string ToString()
        {
            return Year + " - " + Types[Type].ToTitleCase();
        }

        internal virtual string ToTimelineString()
        {
            var timelinestring = "";
            if (EventCollection != null)
                timelinestring = "  ";

            return timelinestring + Time.ToStringRev() + " -";
           
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpHistoricalEvent.Text == ToString() && frm.MainTab.SelectedTab == frm.tabHistoricalEvent)
                return;
            Program.MakeSelected(frm.tabHistoricalEvent, frm.lstHistoricalEvent, this);

            frm.grpHistoricalEvent.Text = ToString();
#if DEBUG
            frm.grpHistoricalEvent.Text += string.Format(string.Format($" - ID: {Id}", Id), Id);
#endif
            frm.grpHistoricalEvent.Show();

            WriteDetailsOnParent(frm, frm.grpHistoricalEvent, new Point(16, 26));


            
        }

        internal override void Link()
        {
            
        }

        internal override void Process()
        {
            
        }

        internal override void Plus(XDocument xdoc)
        {
            if (Types[Type] == "entity relocate" ||
                Types[Type] == "entity primary criminals")
                return;
            //Entity Relocate
        }

        protected virtual void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {

        }

        public HistoricalEvent LastEvent()
        {
            var checkId = Id - 1;
            while (!World.HistoricalEvents.ContainsKey(checkId) && checkId >= 0)
                checkId--;
            return checkId >= 0 ? World.HistoricalEvents[checkId] : null;
        }

        public HistoricalEvent NextEvent()
        {
            var checkId = Id + 1;
            while (!World.HistoricalEvents.ContainsKey(checkId))
            {
                checkId++;
                if (WorldTime.Present.Year == Time.Year && WorldTime.Present.TotalSeconds == Time.TotalSeconds && checkId > Id * 2)
                    break;
            }
            return checkId <= Id * 2 ? World.HistoricalEvents[checkId] : null;
        }

        public void WriteDetailsOnParent(MainForm frm, Control parent, Point location)
        {
            frm.ClearEventDetails();

            if (Time == null)
                EventLabel(frm, parent, ref location, "Time:", Year.ToString());
            else
                EventLabel(frm, parent, ref location, "Time: ", this, Time.ToString());


            WriteDataOnParent(frm, parent, ref location);

            if (parent == frm.grpHistoricalEvent)
            {
                EventLabel(frm, parent, ref location, "Previous:", LastEvent());
                EventLabel(frm, parent, ref location, "Next:", NextEvent());
            }
            if (EventCollection != null)
            { 
                EventLabel(frm, parent, ref location, "Event Col:", EventCollection);
                if (parent == frm.grpHistoricalEvent)
                {
                    EventLabel(frm, parent, ref location, "  Previous:", EventCollection.GetPreviousEvent(this));
                    EventLabel(frm, parent, ref location, "  Next:", EventCollection.GetNextEvent(this));
                }
            }
            if (parent == frm.grpHistoricalEvent)
            {
                try
                {
                    var description = LegendsDescription();
                    if (description.Contains("UNKNOWN"))
                        Console.WriteLine("");
                    EventLabel(frm, parent, ref location, description, "");
                }
                catch (Exception)
                {
                    EventLabel(frm, parent, ref location, "", "");
                }
                
            }
        }

        protected virtual string LegendsDescription()
        {
            return "In " + Time.LegendsTime() + ",";
        }

        internal static void EventLabel(MainForm frm, Control parent, ref Point location, string caption, string data)
        {

            
            var newlabel = new Label
            {
                Parent = parent,
                Text = caption,
                AutoSize = true,
                Location = new Point(location.X, location.Y),
                BackColor = Color.Transparent
            };
            frm.EventDetailControls.Add(newlabel);
            newlabel.BringToFront();

            var newData = new Label
            {
                Parent = parent,
                Text = data,
                AutoSize = true,
                Location = new Point(location.X + 75, location.Y),
                BackColor = Color.Transparent
            };
            frm.EventDetailControls.Add(newData);
            newData.BringToFront();

            location.Y += 20;
        }
        
        internal static void EventLabel(MainForm frm, Control parent, ref Point location, string caption, WorldObject data, string dataCaption = "")
        {
            if (data == null)
                return;
            var newlabel = new Label
            {
                Parent = parent,
                Text = caption,
                Location = new Point(location.X, location.Y),
                BackColor = Color.Transparent
            };
            frm.EventDetailControls.Add(newlabel);
            newlabel.BringToFront();

            var newlinklabel = new LinkLabel
            {
                Parent = parent,
                Data = data,
                AutoSize = true,
                Location = new Point(location.X + 75, location.Y),
                BackColor = Color.Transparent
            };
            if (dataCaption != "")
                newlinklabel.Text = dataCaption;
            frm.EventDetailControls.Add(newlinklabel);
            newlinklabel.BringToFront();

            location.Y += 20;
        }        
        
        internal override void Export(string table)
        {
            var vals = new List<object>
            {
                Id, 
                Type.DBExport(Types), 
                Year, 
                Seconds
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}
