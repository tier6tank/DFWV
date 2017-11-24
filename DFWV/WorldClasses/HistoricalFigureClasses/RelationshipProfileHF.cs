using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    class RelationshipProfileHF
    {

        //Historical Figures.relationship_profile_hf has unknown sub items!
        //New XML!historical_figure	relationship_profile_hf	<hf_id>23930</hf_id>
        //            <meet_count>0</meet_count>
        //            <last_meet_year>-1</last_meet_year>
        //            <last_meet_seconds72>656353024</last_meet_seconds72>
        //    <rep_friendly>1</rep_friendly>


        public int RelationshipHfid { get; set; }
        public int MeetCount { get; set; }
        public int LastMeetYear { get; set; }
        public int LastMeetSeconds { get; set; }
        public int RepFriendly { get; set; }
        public int KnownIdentityID { get; set; }
        public int RepBuddy { get; set; }
        public int RepInformationSource { get; set; }

        public HistoricalFigure ThisHf { get; }

        public HistoricalFigure Hf => ThisHf.World.HistoricalFigures.ContainsKey(RelationshipHfid) ? ThisHf.World.HistoricalFigures[RelationshipHfid] : null;

        public RelationshipProfileHF(XContainer data, HistoricalFigure hf)
        {
            foreach (var element in data.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);
                switch (element.Name.LocalName)
                {
                    case "hf_id":
                        RelationshipHfid = valI;
                        break;
                    case "meet_count":
                        MeetCount = valI;
                        break;
                    case "last_meet_year":
                        LastMeetYear = valI;
                        break;
                    case "last_meet_seconds72":
                        LastMeetSeconds = valI;
                        break;
                    case "rep_friendly":
                        RepFriendly = valI;
                        break;
                    case "known_identity_id":
                        KnownIdentityID = valI;
                        break;
                    case "rep_buddy":
                        RepBuddy = valI;
                        break;
                    case "rep_information_source":
                        RepInformationSource = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement("relationship_profile_hf_visual", element, element.ToString());
                        break;
                }
            }

            ThisHf = hf;
        }

        public override string ToString()
        {
            if (Hf == null)
                return RelationshipHfid + " - " + MeetCount + " - " + LastMeetYear + " - " + LastMeetSeconds + " - " + RepFriendly;
            return Hf + " - " + MeetCount + " - " + LastMeetYear + " - " + LastMeetSeconds + " - " + RepFriendly;
        }

        internal void Export(int hfid)
        {
            var table = "HF_" + GetType().Name;


            var vals = new List<object> { hfid, RelationshipHfid, MeetCount, LastMeetYear, LastMeetSeconds, RepFriendly };


            Database.ExportWorldItem(table, vals);

        }

    }
}