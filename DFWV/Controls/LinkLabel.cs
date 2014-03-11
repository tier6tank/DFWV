namespace DFWV.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using DFWV.WorldClasses;

    /// <summary>
    /// The Link Label is an extended label that allows easy navigation between various WorldObjects.  
    /// In addition to it's text it stores a data field which is the World Object that should be linked to.
    /// </summary>
    public class LinkLabel : Label
    {
        private WorldObject data;
        
        public WorldObject Data
        {
            get { return data; }
            set 
            { 
                data = value;
                if (data != null)
                {
                    ForeColor = System.Drawing.Color.Blue;
                    Text = data.ToString();
                }
                else
                { 
                    ForeColor = System.Drawing.Color.Black;
                    Text = "";
                }
            }
        }

        public LinkLabel() : base()
        {
            
        }

        protected override void OnClick(EventArgs e)
        {
            Cursor.Current = Cursors.Default;
            Font = new Font(Font.Name, Font.Size, FontStyle.Regular);
            if (data != null)
                data.Select((MainForm)this.FindForm());

            base.OnClick(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (data != null)
            {
                Cursor.Current = Cursors.Hand;
                Font = new Font(Font.Name, Font.Size, FontStyle.Underline);
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (data != null)
                Cursor.Current = Cursors.Hand;
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (data != null)
                Cursor.Current = Cursors.Hand;
            base.OnMouseDown(e);
        }
        protected override void  OnMouseLeave(EventArgs e)
        {
            if (data != null)
            {
                Cursor.Current = Cursors.Default;
                Font = new Font(Font.Name, Font.Size, FontStyle.Regular);
            }
 	        base.OnMouseLeave(e);
        }
    }
}
