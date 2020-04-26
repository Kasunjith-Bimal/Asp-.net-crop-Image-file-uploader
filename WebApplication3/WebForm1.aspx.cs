using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SD = System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace WebApplication3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
  
        }

        protected void SignatureUploadBtn_Click(object sender, EventArgs e)
        {
            if (SignatureFileUploader.HasFile)
            {
                try
                {
                    int w =  Convert.ToInt32(imgWidth.Value);

                    int h =  Convert.ToInt32(imgHeight.Value); 

                    int x =  Convert.ToInt32(imgX1.Value);

                    int y = Convert.ToInt32(imgY1.Value);


                   
                    string fileExtension = System.IO.Path.GetExtension(SignatureFileUploader.FileName);

                    if (fileExtension.ToLower() != ".png" && fileExtension.ToLower() != ".jpg" && fileExtension.ToLower() != ".jpeg")
                    {
                        SignatureError.Text = "Only files with .doc or .docx extention are allowed";
                        SignatureError.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        int fileSize = SignatureFileUploader.PostedFile.ContentLength;
                        if (fileSize >2097152)
                        {
                            SignatureError.Text = "Maximum file Size (2MB) exceeded";
                            SignatureError.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                           
                            var fileName = Guid.NewGuid() + SignatureFileUploader.FileName;
                            SignatureFileUploader.SaveAs(Server.MapPath("~/Images/" + fileName));
                            byte[] CropImage = Crop(Server.MapPath("~/Images/" + fileName), w, h, x, y);
                            using (MemoryStream ms = new MemoryStream(CropImage, 0, CropImage.Length))
                            {
                                ms.Write(CropImage, 0, CropImage.Length);

                                using (SD.Image CroppedImage = SD.Image.FromStream(ms, true))
                                {

                                    CroppedImage.Save(Server.MapPath("~/Images/" + fileName), CroppedImage.RawFormat);
                                }

                            }
                                
                            
                            //SignatureImage.ImageUrl = Server.MapPath("~/Images/" + fileName);
                            SignatureError.Text = "File uploaded";

                        }
                    }
      
               
                }
                catch (Exception)
                {

                    SignatureError.Text = "File not uploaded";
                    SignatureError.ForeColor = System.Drawing.Color.Red;
                }
               
              
                
            } 
            else
            {
                SignatureError.Text = "Please select a file to upload";
                SignatureError.ForeColor = System.Drawing.Color.Red;
            }
        }


        static byte[] Crop(string Img, int Width, int Height, int X, int Y)
        {

            try
            {
                using (SD.Image OriginalImage = SD.Image.FromFile(Img))
                {
                    using (SD.Bitmap bmp = new SD.Bitmap(Width, Height))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);

                        using (SD.Graphics Graphic = SD.Graphics.FromImage(bmp))

                        {

                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;

                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            Graphic.DrawImage(OriginalImage, new SD.Rectangle(0, 0, Width, Height), X, Y, Width, Height, SD.GraphicsUnit.Pixel);

                            MemoryStream ms = new MemoryStream();

                            bmp.Save(ms, OriginalImage.RawFormat);

                            return ms.GetBuffer();

                        }

                    }

                }

            }

            catch (Exception Ex)

            {

                throw (Ex);

            }
        }

    }
}