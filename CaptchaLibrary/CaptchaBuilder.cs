using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptchaLibrary
{
    public class CaptchaBuilder:IBuilder
    {
        private Captcha _instance;

        public CaptchaBuilder() {
            _instance = new Captcha();
        }

        public void WithTextLength(int length)
        {
            _instance.CodeLength = length;
        }

        /// <summary>
        /// Adding Background Color of the Bitmap
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public void WithBackgroundColor(Color color)
        {
            SolidBrush solid = new SolidBrush(color);
            _instance.BackgroundColor = solid;
        }

        /// <summary>
        /// Adding Number of Strips on the Bitmap
        /// </summary>
        /// <param name="numOfStrips"></param>
        /// <returns></returns>
        public void WithNumberOfStrips(int numOfStrips)
        {
            _instance.NumOfLines = numOfStrips;
        }

        /// <summary>
        /// Adding Strips Color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public void WithStripsColor(Color color)
        {
            _instance.LinesColor = new SolidBrush(color);
        }

        /// <summary>
        /// Add Font name, size to Captcha.
        /// </summary>
        /// <param name="fontName">Font Name</param>
        /// <param name="fontSize">Font Size</param>
        /// <returns></returns>
        public void WithFontNameAndSize(string fontName, float fontSize)
        {
            Font font = new Font(fontName, fontSize);
            if (font != null)
                _instance.CaptchaFont = font;
            else
                _instance.CaptchaFont = new Font("Tahoma", 16);//default font.
        }

        /// <summary>
        /// Return the Captcha object.
        /// </summary>
        /// <returns></returns>
        public Captcha Build()
        {
            return _instance;
        }

        /// <summary>
        /// Adding the font color.
        /// </summary>
        /// <param name="color"></param>
        public void WithFontColor(Color color)
        {
            _instance.FontColor = new SolidBrush(color);
        }
    }
}
