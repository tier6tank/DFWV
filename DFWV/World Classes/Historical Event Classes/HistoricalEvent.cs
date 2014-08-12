using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HistoricalEvent : XMLObject
    {
        public int Year { get; set; }
        public int Seconds { get; set; }
        public int Type { get; set; }
        public static List<string> Types = new List<string>();

        public HistoricalEventCollection EventCollection { get; set; }
        public WorldTime Time { get; set; }

        public bool InCollection { get { return EventCollection != null; } }
        public string DispNameLower { get { return ToString().ToLower(); } }
        public string EventType { get { return Types[Type]; } }

        override public Point Location { get { return Point.Empty; } }

        public static HistoricalEvent Create(XDocument xdoc, World world)
        {
            switch (xdoc.Root.Element("type").Value.ToString())
            {
                case "create entity position":
                    return new HE_CreateEntityPosition(xdoc, world);
                case "merchant":
                    return new HE_Merchant(xdoc, world);
                case "item stolen":
                    return new HE_ItemStolen(xdoc, world);
                case "masterpiece lost":
                    return new HE_MasterpieceLost(xdoc, world);
                case "hf simple battle event":
                    return new HE_HFSimpleBattleEvent(xdoc, world);
                case "change hf state":
                    return new HE_ChangeHFState(xdoc, world);
                case "add hf entity link":
                    return new HE_AddHFEntityLink(xdoc, world);
                case "created site":
                    return new HE_CreatedSite(xdoc, world);
                case "add hf hf link":
                    return new HE_AddHFHFLink(xdoc, world);
                case "entity created":
                    return new HE_EntityCreated(xdoc, world);
                case "add hf site link":
                    return new HE_AddHFSiteLink(xdoc, world);
                case "created structure":
                    return new HE_CreatedStructure(xdoc, world);
                case "change hf job":
                    return new HE_ChangeHFJob(xdoc, world);
                case "hf travel":
                    return new HE_HFTravel(xdoc, world);
                case "hf new pet":
                    return new HE_HFNewPet(xdoc, world);
                case "field battle":
                    return new HE_FieldBattle(xdoc, world);
                case "peace accepted":
                    return new HE_PeaceAccepted(xdoc, world);
                case "hf died":
                    return new HE_HFDied(xdoc, world);
                case "hf wounded":
                    return new HE_HFWounded(xdoc, world);
                case "creature devoured":
                    return new HE_CreatureDevoured(xdoc, world);
                case "attacked site":
                    return new HE_AttackedSite(xdoc, world);
                case "hf profaned structure":
                    return new HE_HFProfanedStructure(xdoc, world);
                case "hf does interaction":
                    return new HE_HFDoesInteraction(xdoc, world);
                case "hf abducted":
                    return new HE_HFAbducted(xdoc, world);
                case "changed creature type":
                    return new HE_ChangedCreatureType(xdoc, world);
                case "entity relocate":
                    return new HE_EntityRelocate(xdoc, world);
                case "assume identity":
                    return new HE_AssumeIdentity(xdoc, world);
                case "remove hf entity link":
                    return new HE_RemoveHFEntityLink(xdoc, world);
                case "entity primary criminals":
                    return new HE_EntityPrimaryCriminals(xdoc, world);
                case "hf reunion":
                    return new HE_HFReunion(xdoc, world);
                case "plundered site":
                    return new HE_PlunderedSite(xdoc, world);
                case "body abused":
                    return new HE_BodyAbused(xdoc, world);
                case "hf gains secret goal":
                    return new HE_HFGainsSecretGoal(xdoc, world);
                case "hf learns secret":
                    return new HE_HFLearnsSecret(xdoc, world);
                case "created world construction":
                    return new HE_CreatedWorldConstruction(xdoc, world);
                case "remove hf site link":
                    return new HE_RemoveHFSiteLink(xdoc, world);
                case "artifact created":
                    return new HE_ArtifactCreated(xdoc, world);
                case "artifact possessed":
                    return new HE_ArtifactPossessed(xdoc, world);
                case "hf confronted":
                    return new HE_HFConfronted(xdoc, world);
                case "peace rejected":
                    return new HE_PeaceRejected(xdoc, world);
                case "reclaim site":
                    return new HE_ReclaimSite(xdoc, world);
                case "razed structure":
                    return new HE_RazedStructure(xdoc, world);
                case "artifact stored":
                    return new HE_ArtifactStored(xdoc, world);
                case "entity law":
                    return new HE_EntityLaw(xdoc, world);
                case "masterpiece item":
                    return new HE_MasterpieceItem(xdoc, world);
                case "artifact lost":
                    return new HE_ArtifactLost(xdoc, world);
                case "diplomat lost":
                    return new HE_DiplomatLost(xdoc, world);
                case "hf revived":
                    return new HE_HFRevived(xdoc, world);
                case "agreement rejected":
                    return new HE_AgreementRejected(xdoc, world);
                case "first contact":
                    return new HE_FirstContact(xdoc, world);
                case "destroyed site":
                    return new HE_DestroyedSite(xdoc, world);
                case "change hf body state":
                    return new HE_ChangeHFBodyState(xdoc, world);
                case "new site leader":
                    return new HE_NewSiteLeader(xdoc, world);
                case "site taken over":
                    return new HE_SiteTakenOver(xdoc, world);
                case "site abandoned":
                    return new HE_SiteAbandoned(xdoc, world);
                case "masterpiece arch constructed":
                    return new HE_MasterpieceArchConstructed(xdoc, world);
                case "masterpiece food":
                    return new HE_MasterpieceFood(xdoc, world);
                case "masterpiece engraving":
                    return new HE_MasterpieceEngraving(xdoc, world);
                case "masterpiece item improvement":
                    return new HE_MasterpieceItemImprovement(xdoc, world);
                case "agreement made":
                    return new HE_AgreementMade(xdoc, world);
                case "masterpiece dye":
                    return new HE_MasterpieceDye(xdoc, world);
                case "first contact failed":
                    return new HE_FirstContactFailed(xdoc, world);
                case "masterpiece arch design":
                    return new HE_MasterpieceArchDesign(xdoc, world);
                case "agreement void": //Unknown events
                case "site tribute forced":
                case "hf destroyed site":
                case "site died":
                case "replaced structure":
                case "hf razed structure":
                case "hf reach summit":
                case "remove hf hf link":
                case "agreement concluded":
                case "artifact hidden":
                case "artifact found":
                case "artifact recovered":
                case "artifact dropped":
                case "entity incorporated":
                case "impersonate hf":
                default:
                    string logtext = "Unassessed Event Type: " + (xdoc.Root.Element("type").Value.ToString());// + raw.Replace("<", "//<") + "\n\t\t\tbreak;");
#if DEBUG
                    foreach (var ln in ("\t\t" + xdoc.Root.ToString().Replace("<", "//<")).Split('\n'))
                    {
                        if (!ln.Contains("<historical_event>") && 
                            !ln.Contains("<id>") && 
                            !ln.Contains("<year>") &&
                            !ln.Contains("<seconds72>") &&
                            !ln.Contains("<type>") &&
                            !ln.Contains("</historical_event>"))
                                logtext += ln;
                    }
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
                    return new HE_UnassessedEvent(xdoc, world);
                    //Clipboard.SetText("\t\t\t\tcase \"" + Type + "\":\n" + raw.Replace("\t\t", "\t\t//") + "\n");
            }
        }

        public HistoricalEvent(XDocument xdoc, World world)
            : base(xdoc, world)
        {

            Year = Int32.Parse(xdoc.Root.Element("year").Value.ToString());
            Seconds = Int32.Parse(xdoc.Root.Element("seconds72").Value.ToString());

            string type = xdoc.Root.Element("type").Value.ToString();
            if (!Types.Contains(type))
                Types.Add(type);
            Type = Types.IndexOf(type);
        }

        public override string ToString()
        {
            return Year.ToString() + " - " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Types[Type]);
        }

        internal virtual string ToTimelineString()
        {
            string timelinestring = "";
            if (EventCollection != null)
                timelinestring = "  ";

            return timelinestring + Time.ToStringRev() + " -";
           
        }

        public override void Select(MainForm frm)
        {
            frm.grpHistoricalEvent.Text = this.ToString();
            frm.grpHistoricalEvent.Show();

            WriteDetailsOnParent(frm, frm.grpHistoricalEvent, new Point(16, 26));

            Program.MakeSelected(frm.tabHistoricalEvent, frm.lstHistoricalEvent, this);

            
        }

        internal override void Link()
        {
            Time = new WorldTime(Year, Seconds);
        }

        internal override void Process()
        {
            
        }
        
        public virtual void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {

        }

        public void WriteDetailsOnParent(MainForm frm, Control parent, Point location)
        {
            frm.ClearEventDetails();

            EventLabel(frm, parent, ref location, "Time:", Time == null ? Year.ToString() : Time.ToString());

            WriteDataOnParent(frm, parent, ref location);

            if (EventCollection != null)
                EventLabel(frm, parent, ref location, "Event Col:", EventCollection);
            if (parent == frm.grpHistoricalEvent)
                EventLabel(frm, parent, ref location, LegendsDescription(), "");
        }

        public virtual string LegendsDescription()
        {
            return "In " + Time.LegendsTime() + ",";
        }

        internal void EventLabel(MainForm frm, Control parent, ref Point location, string caption, string data)
        {

            
            Label newlabel = new Label();
            newlabel.Parent = parent;
            newlabel.Text = caption;
            newlabel.AutoSize = true;
            newlabel.Location = new Point(location.X, location.Y);
            newlabel.BackColor = Color.Transparent;
            frm.EventDetailControls.Add(newlabel);
            newlabel.BringToFront();

            Label newData = new Label();
            newData.Parent = parent;
            newData.Text = data;
            newData.AutoSize = true;
            newData.Location = new Point(location.X + 75, location.Y);
            newData.BackColor = Color.Transparent;
            frm.EventDetailControls.Add(newData);
            newData.BringToFront();

            location.Y += 20;
        }
        
        internal void EventLabel(MainForm frm, Control parent, ref Point location, string caption, WorldObject data)
        {
            if (data == null)
                return;
            Label newlabel = new Label();
            newlabel.Parent = parent;
            newlabel.Text = caption;
            newlabel.Location = new Point(location.X, location.Y);
            newlabel.BackColor = Color.Transparent;
            frm.EventDetailControls.Add(newlabel);
            newlabel.BringToFront();

            DFWV.Controls.LinkLabel newlinklabel = new DFWV.Controls.LinkLabel();
            newlinklabel.Parent = parent;
            newlinklabel.Data = data;
            newlinklabel.AutoSize = true;
            newlinklabel.Location = new Point(location.X + 75, location.Y);
            newlinklabel.BackColor = Color.Transparent;
            frm.EventDetailControls.Add(newlinklabel);
            newlinklabel.BringToFront();

            location.Y += 20;
        }        
        
        internal override void Export(string table)
        {

            List<object> vals = new List<object>() { ID, Types[Type], Year, Seconds };


            Database.ExportWorldItem(table, vals);

        }
    }
}
