
namespace DFWV.WorldClasses
{
    using DFWV.WorldClasses.HistoricalEventClasses;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    class Structure : XMLObject
    {
        public Site Site { get; set; }

        public HE_RazedStructure  RazedEvent { get; set; }
        public List<HE_HFProfanedStructure> ProfanedEvents { get; set; }
        public HE_CreatedStructure CreatedEvent { get; set; }

        override public Point Location { get { return Site.Location; } }

        public Structure(Site site, int id, World world) : base(world)
        {
            Site = site;
            ID = id;
            World = world;
        }

        public override void Select(MainForm frm)
        {
            //frm.grpStructure.Text = this.ToString();
            //frm.grpStructure.Show();

            //Program.MakeSelected(frm.tabStructure, frm.lstStructure, this);
        }

        internal override void Link()
        {

        }

        internal override void Process()
        {

        }

        internal override void Export(string table)
        {

        }
    }
}
