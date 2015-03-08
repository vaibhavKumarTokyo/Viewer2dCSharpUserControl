using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Viewer2D.UC
{
    public partial class Viewer2DUserControl : UserControl, IViewer2D
    {
        #region Constructors
        public Viewer2DUserControl()
        {
            InitializeComponent();   
        }

        public Viewer2DUserControl(Image image)
        {
            InitializeComponent();
            this.Image = image;
        }
        #endregion //Constructors

        /// <summary>
        /// Viewer2D Interface's implementations
        /// </summary>
        #region IViewer2DInterface

        /// <summary>
        /// Gets or Sets the Image displayed by UserControl implementing interface 'IViewer2D'
        /// </summary>
        public Image Image
        {
            get
            {
                return m_image;
            }
            set
            {
                if (m_image != null)
                    m_image.Dispose();
                m_image = value;
                //re-render the control
                if(m_image != null)
                    this.RenderControl();
            }
        }

        /// <summary>
        /// Returns the size of display image of the user-control,
        /// Throws ImagePropertyNullException if display image is null
        /// </summary>
        public Size ImageSize 
        {
            get
            {
                if (m_image != null)
                    return m_image.Size;
                else
                    throw new Exception(Resources.ImagePropertyNullException);
            }
        }

        /// <summary>
        /// Saves the display image of the control to the input path else throw ImagePropertyNullException
        /// </summary>
        /// <param name="fullPath">Full-path of the desired location</param>
        public void SaveImageLocal(string fullPath)
        {
            string filePath = System.IO.Path.GetFullPath(fullPath);
            if (m_image != null)
                m_image.Save(fullPath);
            else
                throw new Exception(Resources.ImagePropertyNullException);
        }

        /// <summary>
        /// Converts the display image of the control to Grey-scale
        /// </summary>
        public void ApplyFilterGreyScale()
        {
            if (m_image == null)
                return;
            Image sourceImage = m_image;
            Bitmap greyBitmap = new Bitmap(sourceImage.Width, sourceImage.Height);
            using (Graphics g = Graphics.FromImage(greyBitmap))
            {
                ColorMatrix greyColorMatrix = new ColorMatrix(new float[][] {
                                                             new float[] {.3f, .3f, .3f, 0, 0},
                                                             new float[] {.59f, .59f, .59f, 0, 0},
                                                             new float[] {.11f, .11f, .11f, 0, 0},
                                                             new float[] {0, 0, 0, 1, 0},
                                                             new float[] {0, 0, 0, 0, 1}});

                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(greyColorMatrix);
                g.DrawImage(sourceImage, new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), 0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel, attributes);
            }
            this.Image = (Image)greyBitmap;
        }

        /// <summary>
        /// Zoom-In or Out the display image of the UserControl
        /// </summary>
        /// <param name="isZoomIn">Zooms-Out if false else Zooms-In</param>
        public void ZoomDisplayImage(bool isZoomIn = true)
        {
            if (m_image == null)
                return;
            Size zoomedSize = m_image.Size;
            double zoomFactor = 2.0;
            if (!isZoomIn)
                zoomFactor = 0.5;
            zoomedSize.Width = (int)(zoomFactor * zoomedSize.Width);
            zoomedSize.Height = (int)(zoomFactor * zoomedSize.Height);
            this.ReSizeImage(zoomedSize);
        }

        /// <summary>
        /// Scales the display image to fit UserControl
        /// </summary>
        public void ScaleImageToFit()
        {
            this.ReSizeImage(this.Size);
        }

        /// <summary>
        /// Resizes the display image of UserControl to desired Size
        /// </summary>
        /// <param name="newSize">Desired size</param>
        public void ReSizeImage(Size newSize)
        {
            if (m_image == null)
                throw new Exception(Resources.ImagePropertyNullException);
            else
            {
                int sourceWidth = m_image.Width;
                int sourceHeight = m_image.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)newSize.Width / (float)sourceWidth);
                nPercentH = ((float)newSize.Height / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap resizedImage = new Bitmap(destWidth, destHeight);
                using (Graphics tempGraphics = Graphics.FromImage((Image)resizedImage))
                {
                    tempGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    tempGraphics.DrawImage(m_image, 0, 0, destWidth, destHeight);
                }
                this.Image = (Image)resizedImage;
            }
        }

        /// <summary>
        /// Centers or De-centers the display image of UserControl based on input argument
        /// </summary>
        /// <param name="isCentreImage">Set true to Centre the image</param>
        public void CenterImage(bool isCentreImage)
        {
            m_isCentreImage = isCentreImage;
            this.ResizeRedraw = true;
        }

        #endregion //IViewer2DInterface

        /// <summary>
        /// Private methods of UserControl 'Viewer2D'
        /// </summary>
        #region 'Viewer2D' UserControl's PrivateMethods

        /// <summary>
        /// Updates the control by calling Viewer2DUserControl_Paint
        /// Throws ImagePropertyNullException if display image is null
        /// </summary>
        private void RenderControl()
        {
            if (m_image == null)
                throw new Exception(Resources.ImagePropertyNullException);
            else
                this.Update();
        }

        /// <summary>
        /// Draws the User-control
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">PaintEventArgs arguments</param>
        private void Viewer2DUserControl_Paint(object sender, PaintEventArgs e)
        {
            if (m_image == null)
                return;
            using (Graphics graphicContent = this.CreateGraphics())
            {
                if (m_isCentreImage)
                {
                    RectangleF boundRect = graphicContent.VisibleClipBounds;

                    float imageCentreX = graphicContent.DpiX * m_image.Width /
                                                       m_image.HorizontalResolution;
                    float imageCentreY = graphicContent.DpiY * m_image.Height /
                                                       m_image.VerticalResolution;

                    graphicContent.DrawImage(m_image, (boundRect.Width - imageCentreX) / 2,
                                          (boundRect.Height - imageCentreY) / 2);
                }
                else
                    graphicContent.DrawImage(m_image, this.Location);
            }

        }
        #endregion //'Viewer2D' UserControl's PrivateMethods

        /// <summary>
        /// Private attributes of UserControl 'Viewer2D'
        /// </summary>
        #region 'Viewer2D' UserControl's Private Attributes

        private Image m_image;
        private bool m_isCentreImage = false;

        #endregion //'Viewer2D' UserControl's Private Attributes
    }
}
