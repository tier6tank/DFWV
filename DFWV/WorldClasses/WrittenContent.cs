using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalFigureClasses;
using System.Linq;
using System;

namespace DFWV.WorldClasses
{
    public class WrittenContent : XMLObject
    {
        override public Point Location => Point.Empty;
        public string Title { get; set; }
        private int? AuthorHfid { get; }
        public HistoricalFigure Author { get; set; }
        private int? AuthorRoll { get; }
        public static List<string> Forms = new List<string>();
        public int? Form { get; set; }
        public string FormName => Form.HasValue ? Forms[Form.Value] : "";
        private int? FormId { get; set; }
        public static List<string> StyleNames = new List<string>();
        public List<int> Styles { get; set; } = new List<int>();
        public string StyleName => String.Join("\n", Styles.Select(x => StyleNames[x]));
        public List<Reference> References { get; set; }

        [UsedImplicitly]
        public bool KnownAuthor => Author != null;
        public int? PageEnd { get; set; }
        public int? PageStart { get; set; }

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
                    case "form":
                        if (valI == 0)
                        {
                            if (!Forms.Contains(val))
                                Forms.Add(val);
                            Form = Forms.IndexOf(val);
                        }
                        break;
                    case "form_id":
                        FormId = valI;
                        break;
                    case "style":
                        if (!StyleNames.Contains(val))
                            StyleNames.Add(val);
                        Styles.Add(StyleNames.IndexOf(val));
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
                    case "author_hfid":
                        AuthorHfid = valI;
                        break;
                    case "author_roll":
                        AuthorRoll = valI;
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
            frm.grpWrittenContent.Text += $" - ID: {Id}";
#endif
            frm.lblWrittenContentTitle.Text = ToString();
            frm.lblWrittenContentAuthor.Data = Author;
            frm.lblWrittenContentType.Text = FormName;
            frm.lblWrittenContentStyle.Text = StyleName;

            frm.LabelWrittenContentPages.Visible = PageStart.HasValue;
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
            if (AuthorHfid.HasValue && World.HistoricalFigures.ContainsKey(AuthorHfid.Value))
                Author = World.HistoricalFigures[AuthorHfid.Value];
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
            return Title ?? $"{Forms[Form.Value].ToTitleCase()} by {Author}";
        }

    }


}