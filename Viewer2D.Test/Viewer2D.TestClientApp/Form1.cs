using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Viewer2D.TestClientApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.viewer2DUserControl1.Image = Image.FromFile(@".\download.jpg");
            
            Size newSize = new Size(400,400);
            this.viewer2DUserControl1.ReSizeImage(newSize);
            this.viewer2DUserControl1.ScaleImageToFit();
            this.viewer2DUserControl1.ApplyFilterGreyScale();
            this.viewer2DUserControl1.CenterImage(true);
            this.viewer2DUserControl1.ZoomDisplayImage(false);
        }
    }
}
