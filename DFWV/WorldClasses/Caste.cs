using System;
using System.Drawing;
using System.Xml.Linq;

namespace DFWV.WorldClasses
{
    public class Caste : XMLObject
    {
        public Race Race;

        public int CasteID  { get; set; }
        public int Gender { get; set; }
        public string Description { get; set; }


        public Caste(XContainer data, Race race) : base(race.World)
        {
            ID = -1;
            CasteID = Convert.ToInt32(data.Element("id").Value);
            Name = data.Element("name").Value;
            Gender = Convert.ToInt32(data.Element("gender").Value);
            Description = data.Element("description").Value;

            Race = race;
        }

        internal override void Link()
        {
            throw new NotImplementedException();
        }

        internal override void Process()
        {
            throw new NotImplementedException();
        }


        internal override void Plus(XDocument xdoc)
        {
            throw new NotImplementedException();
        }

        public override Point Location
        {
            get { return Point.Empty; }
        }

        internal override void Export(string table)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }

        public override void Select(MainForm frm)
        {
            try
            {
                frm.lblRaceCasteGender.Text = Gender.ToString();
                frm.lblRaceCasteDescription.Text = Description;
            }
            finally
            {

            }
        }
    }
}
