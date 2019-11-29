using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

//Control encontrado en http://www.doogal.co.uk/bevel.php
namespace SMPorres.Lib
{
    /// <summary>Bevel border style.</summary>
    public enum BevelStyle
    {
        /// <summary>Lowered border.</summary>
        Lowered,
        /// <summary>Raised border.</summary>
        Raised,
        /// <summary>No border.</summary>
        Flat
    }

    /// <summary>
    /// A bevel control.
    /// </summary>
    public class Bevel : System.Windows.Forms.Control
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private const Border3DSide DefaultShape = Border3DSide.Bottom;
        private const BevelStyle DefaultStyle = BevelStyle.Lowered;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bevel"/> class.
        /// </summary>
        public Bevel()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);

            style = DefaultStyle;
            shape = DefaultShape;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

        /// <summary>Paints the rule.</summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Create a local version of the graphics object for the Bevel.
            Graphics g = e.Graphics;
            Rectangle r = ClientRectangle;

            if (Style != BevelStyle.Flat)
            {
                Border3DStyle style = Border3DStyle.SunkenOuter;
                if (Style == BevelStyle.Raised)
                    style = Border3DStyle.RaisedInner;

                // Draw the Bevel.
                switch (Shape)
                {
                    case Border3DSide.All:
                        ControlPaint.DrawBorder3D(g, r.Left, r.Top, r.Width, r.Height, style);
                        break;
                    case Border3DSide.Left:
                        ControlPaint.DrawBorder3D(g, r.Left, r.Top, 2, r.Height, style);
                        break;
                    case Border3DSide.Top:
                        ControlPaint.DrawBorder3D(g, r.Left, r.Top, r.Width, 2, style);
                        break;
                    case Border3DSide.Bottom:
                        ControlPaint.DrawBorder3D(g, r.Left, r.Bottom - 2, r.Width, 2, style);
                        break;
                    case Border3DSide.Middle:
                        break;
                    case Border3DSide.Right:
                        ControlPaint.DrawBorder3D(g, r.Right - 2, r.Top, 2, r.Height, style);
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
            }

            // Calling the base class OnPaint
            base.OnPaint(e);
        }

        private Border3DSide shape;
        /// <summary>
        /// Gets or sets the shape of the bevel.
        /// </summary>
        [DefaultValue(DefaultShape)]
        public Border3DSide Shape
        {
            get
            {
                return shape;
            }
            set
            {
                shape = value;
                Invalidate();
            }
        }

        private BevelStyle style;
        /// <summary>
        /// Gets or sets the style of the bevel.
        /// </summary>
        [DefaultValue(DefaultStyle)]
        public BevelStyle Style
        {
            get
            {
                return style;
            }
            set
            {
                style = value;
                Invalidate();
            }
        }
    }
}

