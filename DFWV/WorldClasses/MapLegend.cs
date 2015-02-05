using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DFWV
{
    public class MapLegend
    {
        public Dictionary<string, Color> LegendItem = new Dictionary<string, Color>();

        public MapLegend(string path)
        {
            var lines = File.ReadAllLines(path);
            Name = Path.GetFileNameWithoutExtension(path);
            foreach (var line in lines)
            {
                if (Regex.Matches(line, @"^([a-z,1-9,/]+ )+ *\(\d+,\d+,\d+\)$").Count > 0)
                    //Normal format - NAME NAME (###,###,###)
                {
                    var lineSplit = line.Split("(,)".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    LegendItem.Add(lineSplit[0].Trim(),
                        Color.FromArgb(Convert.ToInt32(lineSplit[1]), Convert.ToInt32(lineSplit[2]),
                            Convert.ToInt32(lineSplit[3])));
                }
                else if (Regex.Matches(line, @"^([a-z,1-9,/]+ )+ *(\(\d+,\d+,\d+\))+$").Count > 0)
                    //Multiple Colors format - NAME NAME (###,###,###)(###,###,###)(###,###,###)
                {
                    var lineSplit = line.Split("(,)".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 1; i < lineSplit.Count(); i += 3)
                    {
                        LegendItem.Add(lineSplit[0].Trim() + " " + ((i - 1)/3 + 1),
                            Color.FromArgb(Convert.ToInt32(lineSplit[i]), Convert.ToInt32(lineSplit[i + 1]),
                                Convert.ToInt32(lineSplit[i + 2])));
                    }
                }
                else if (Regex.Matches(line, @"^([a-z,1-9,/]+ )+ *\(\d+,\d+\)+$").Count > 0)
                    //two components format - NAME NAME (###,###)
                {
                    var lineSplit = line.Split("(,)".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    LegendItem.Add(lineSplit[0].Trim(),
                        Color.FromArgb(Convert.ToInt32(lineSplit[1]), Convert.ToInt32(lineSplit[2]), 0));
                }
                else if (Regex.Matches(line, @"^(([a-z,1-9,/]+ )+ *\(\d+,\d+,\d+\))+$").Count > 0)
                    //Border format - NAME NAME (###,###,###), NAME NAME (###,###,###)
                {
                    var lineSplit = line.Split("(,)".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    LegendItem.Add(lineSplit[0].Trim(),
                        Color.FromArgb(Convert.ToInt32(lineSplit[1]), Convert.ToInt32(lineSplit[2]),
                            Convert.ToInt32(lineSplit[3])));
                    LegendItem.Add(lineSplit[0].Trim() + " " + lineSplit[4].Trim(),
                        Color.FromArgb(Convert.ToInt32(lineSplit[5]), Convert.ToInt32(lineSplit[6]),
                            Convert.ToInt32(lineSplit[7])));
                }
                else if (Regex.Matches(line, @"^([a-z,1-9,/]+ )+ *\(\d+,\d+,\d+\), [a-z]*$").Count > 0)
                    //"rows" format - NAME NAME (###,###,###), TEXT
                {
                    var lineSplit = line.Split("(,)".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    LegendItem.Add(lineSplit[0].Trim(),
                        Color.FromArgb(Convert.ToInt32(lineSplit[1]), Convert.ToInt32(lineSplit[2]),
                            Convert.ToInt32(lineSplit[3])));
                }
                else if (Regex.Matches(line, @"^([a-z,1-9,/]+ )+ *(\(\d+,\d+,\d+\)/*)+$").Count > 0)
                    //Multiple Colors format 2 - NAME NAME (###,###,###)/(###,###,###)
                {
                    var lineSplit = line.Split("(,/)".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 1; i < lineSplit.Count(); i += 3)
                    {
                        LegendItem.Add(lineSplit[0].Trim() + " " + ((i - 1)/3 + 1),
                            Color.FromArgb(Convert.ToInt32(lineSplit[i]), Convert.ToInt32(lineSplit[i + 1]),
                                Convert.ToInt32(lineSplit[i + 2])));
                    }
                }
            }
        }

        public string Name { get; set; }

        internal void DrawTo(PictureBox picLegend)
        {
            Image img = new Bitmap(200, 1000);
            var g = Graphics.FromImage(img);

            g.Clear(Color.Black);
            var y = 5;
            var width = 0;
            Brush b;
            foreach (var legenditem in LegendItem)
            {
                b = new SolidBrush(legenditem.Value);
                g.DrawString(legenditem.Key, picLegend.Font, Brushes.White, new PointF(15, y - 3));
                g.FillRectangle(b, new Rectangle(5, y, 10, 10));
                g.DrawRectangle(Pens.White, new Rectangle(5, y, 10, 10));
                y += 15;
                width = Math.Max(width, (int) g.MeasureString(legenditem.Key, picLegend.Font).Width);
            }
            picLegend.Image = img;
            picLegend.Height = y;
            picLegend.Width = width + 15;
        }
    }
}