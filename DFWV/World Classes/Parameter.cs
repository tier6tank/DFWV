namespace DFWV.WorldClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;
    using System.Collections.Specialized;

    public class Parameter : WorldObject
    {
        public string Value { get; set; }

        public string DispNameLower { get { return ToString().ToLower(); } }

        override public Point Location { get { return Point.Empty; } }


        internal Parameter(string data, World world) : base(world)
        {
            data = data.Substring(1, data.Length - 2);
            Name = data.Substring(0,data.IndexOf(':'));
            Value = data.Substring(data.IndexOf(':') + 1);
        }

        //public Parameter(NameValueCollection data, World world) 
        //    : base (world)
        //{
        //    Name = data["Name"].ToString();
        //    Value = data["Value"].ToString();
        //}

        public override string ToString()
        {
            return Name + ":" + Value;
        }

        public override void Select(MainForm frm)
        {
            frm.grpParameter.Text = Name;
            frm.grpParameter.Show();

            frm.lblParameterName.Text = Name;
            frm.lblParameterData.Text = Value;

            Program.MakeSelected(frm.tabParameter, frm.lstParameter, this);
        }

        internal override void Export(string table)
        {

            List<object> vals = new List<object>();

            vals.Add(Name);
            vals.Add(Value);


            Database.ExportWorldItem(table, vals);
        }
    }
}
