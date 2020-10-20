using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptchaLibrary
{
    /// <summary>
    /// Captcha using Builder Pattern.
    /// </summary>
    public class Captcha
    {
        private readonly Random _Rand;
        private Bitmap _bitmap;
        private int _imageWidth;
        private int _imageHeight;
        public SolidBrush LinesColor
        { get; set; }
        public String ImageFilePath { get; private set; }
        public SolidBrush BackgroundColor { get; set; }
        public SolidBrush FontColor { get; set; }
        public String DirectoryPath { get; set; }
        public int CodeLength { get; set; }
        public int NumOfLines { get; set; }

        /// <summary>
        /// Get/Set Image Font
        /// </summary>
        public Font ImageFont { get; set; }

        /// <summary>
        /// Get Generate Text
        /// </summary>
        public String GeneratedCode { get; private set; }
        /// <summary>
        /// Instance of Captcha class
        /// </summary>
        private Captcha _instance;
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Captcha() {
            _instance = new Captcha();
            _Rand = new Random();
        }

        public Captcha WithTextLength(int length) {
            _instance.CodeLength = length;
            return _instance;
        }

        public Captcha WithTextLength(int length)
        {
            _instance.CodeLength = length;
            return _instance;
        }


    }
}
