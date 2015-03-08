using System.Drawing;

namespace Viewer2D.UC
{
    public interface IViewer2D
    {
        /// <summary>
        /// Gets or Sets the Image displayed by UserControl implementing interface 'IViewer2D'
        /// </summary>
        Image Image { get; set;}

        /// <summary>
        /// Returns the size of display image of the user-control,
        /// Throws ImagePropertyNullException if display image is null
        /// </summary>
        Size ImageSize { get; }

        /// <summary>
        /// Saves the display image of the control to the input path else throw ImagePropertyNullException
        /// </summary>
        /// <param name="fullPath">Full-path of the desired location</param>
        void SaveImageLocal(string fullPath);

        /// <summary>
        /// Converts the display image of the control to Grey-scale
        /// </summary>
        void ApplyFilterGreyScale();

        /// <summary>
        /// Zoom-In or Out the display image of the UserControl
        /// </summary>
        /// <param name="isZoomIn">Zooms-Out if false else Zooms-In</param>
        void ZoomDisplayImage(bool isZoomIn);

        /// <summary>
        /// Scales the display image to fit UserControl
        /// </summary>
        void ScaleImageToFit();

        /// <summary>
        /// Resizes the display image of UserControl to desired Size
        /// </summary>
        /// <param name="newSize">Desired size</param>
        void ReSizeImage(Size newSize);

        /// <summary>
        /// Centers or De-centers the display image of UserControl based on input argument
        /// </summary>
        /// <param name="isCentreImage">Set true to Centre the image</param>
        void CenterImage(bool isCentreImage);
    }
}
