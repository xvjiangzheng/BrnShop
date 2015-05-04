using System;
using System.Web;
using System.Web.Mvc;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// 图片动作结果
    /// </summary>
    public class ImageResult : ActionResult
    {
        private byte[] _image;
        public byte[] Image
        {
            get { return _image; }
            set { _image = value; }
        }

        private string _contenttype;
        public string ContentType
        {
            get { return _contenttype; }
            set { _contenttype = value; }
        }

        public ImageResult(byte[] image, string contenttype)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }
            _image = image;
            _contenttype = contenttype;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrWhiteSpace(_contenttype)) response.ContentType = _contenttype;
            response.OutputStream.Write(this._image, 0, this._image.Length);
        }
    }
}
