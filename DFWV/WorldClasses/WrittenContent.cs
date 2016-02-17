using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class WrittenContent : XMLObject
    {
        override public Point Location => Point.Empty;
        public string Title { get; set; }
        private int? AuthorID { get; set; }
        public Unit Author { get; set; }
        public static List<string> Types = new List<string>();
        public int? Type { get; set; }
        public string TypeName => Type.HasValue ? Types[Type.Value] : "";
        public static List<string> Styles = new List<string>();
        public int? Style { get; set; }
        public string StyleName => Style.HasValue ? Styles[Style.Value] : "";
        public List<Reference> References { get; set; }

        [UsedImplicitly]
        public bool KnownAuthor => Author != null;
        public int PageEnd { get; set; }
        public int PageStart { get; set; }

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();


        public WrittenContent(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value.Trim();
                int valI;
                int.TryParse(val, out valI);
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "name":
                        Name = val;
                        break;
                    case "type":
                        if (valI == 0)
                        {
                            if (!Types.Contains(val))
                                Types.Add(val);
                            Type = Types.IndexOf(val);
                        }
                        break;
                    case "style":
                        if (!Styles.Contains(val))
                            Styles.Add(val);
                        Style = Styles.IndexOf(val);
                        break;
                    case "title":
                        Title = val;
                        break;
                    case "page_start":
                        PageStart = valI;
                        break;
                    case "page_end":
                        PageEnd = valI;
                        break;
                    case "author":
                        AuthorID = valI;
                        break;
                    case "reference":
                        if (References == null)
                            References = new List<Reference>();
                        References.Add(new Reference(element, this));
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpWrittenContent.Text == ToString() && frm.MainTab.SelectedTab == frm.tabWrittenContent)
                return;
            Program.MakeSelected(frm.tabWrittenContent, frm.lstWrittenContent, this);

            frm.grpWrittenContent.Text = ToString();
            frm.grpWrittenContent.Show();
#if DEBUG
            frm.grpWrittenContent.Text += string.Format(" - ID: {0}", Id);
#endif
            frm.lblWrittenContentTitle.Text = ToString();
            frm.lblWrittenContentAuthor.Data = Author;
            frm.lblWrittenContentType.Text = TypeName;
            frm.lblWrittenContentStyle.Text = StyleName;
            frm.lblWrittenContentPages.Text = PageStart == -1 ? "" : $"{PageStart} - {PageEnd}";

            frm.grpWrittenContentReferences.FillListboxWith(frm.lstWrittenContentReferences, References);
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id, 
                Name.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

        internal override void Link()
        {
            if (AuthorID.HasValue && World.Units.ContainsKey(AuthorID.Value))
                Author = World.Units[AuthorID.Value];
            References?.ForEach(x => x.Link());
        }
    

        internal override void Process()
        {
            
        }

        internal override void Plus(XDocument xdoc)
        {
            
        }

        public override string ToString()
        {
            return Title;
        }

    }


}