using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptchaLib.Builder
{
   public  interface IBuilder
    {
        /// <summary>
        /// Font Color
        /// </summary>
        /// <param name="color"></param>
        void WithFontColor(Color color);

        /// <summary>
        /// Font Color
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        void WithFontColorRGB(int r,int g,int b);

        /// <summary>
        /// Adding Background Color of the Bitmap
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        void WithBackgroundColor(Color color);

        /// <summary>
        /// Adding Background Color of the Bitmap
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        /// <returns></returns>
        void WithBackgroundColorRGB(int r,int g,int b);

        /// <summary>
        /// Adding Number of Strips on the Bitmap
        /// </summary>
        /// <param name="numOfStrips"></param>
        /// <returns></returns>
        void WithNumberOfStrips(int numOfStrips);

        /// <summary>
        /// Adding Strips Color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        void WithStripsColor(Color color);

        /// <summary>
        /// Adding Strips Color
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="b">Blue</param>
        /// <param name="g">Green</param>
        /// <returns></returns>
        void WithStripsColorRGB(int r,int g,int b);

        /// <summary>
        /// Add Font name, size to Captcha.
        /// </summary>
        /// <param name="fontName">Font Name</param>
        /// <param name="fontSize">Font Size</param>
        /// <returns></returns>
        void WithFontNameAndSize(string fontName, float fontSize);

        /// <summary>
        /// Build Captcha based on the properties.
        /// </summary>
        /// <returns></returns>
        Captcha Build();
    }
}
