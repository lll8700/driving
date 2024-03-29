﻿using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Yun.Share.Voice.Controllers.Admin
{
    [ApiController]
    [Route("api/imgae")]
    public class ImageController: AuthorizeController
    {
        private readonly IConfiguration _configuration;
        public ImageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// 访问图片
        /// </summary>
        /// <param name="width">所访问图片的宽度,高度自动缩放,大于原图尺寸或者小于等于0返回原图</param>
        /// <param name="name">所要访问图片的名称或者相对地址</param>
        /// <returns>图片</returns>
        [HttpGet]
        [Route("{name}")]
        public IActionResult GetImage(string name)
        {
            int width = 0;
            //寻找Json配置文件中的路径
            var appPath = _configuration["Authentication:Oss:ImagePath"];
            var errorImage = appPath + "404.png";//没有找到图片
            var imgPath = string.IsNullOrEmpty(name) ? errorImage : appPath + name;
            //获取图片的返回类型
            var contentTypDict = new Dictionary<string, string> {
                {"jpg","image/jpeg"},
                {"jpeg","image/jpeg"},
                {"jpe","image/jpeg"},
                {"png","image/png"},
                {"gif","image/gif"},
                {"ico","image/x-ico"},
                {"tif","image/tiff"},
                {"tiff","image/tiff"},
                {"fax","image/fax"},
                {"wbmp","image/nd.wap.wbmp"},
                {"rp","imagend.rn-realpix"}
            };
            var contentTypeStr = "image/jpeg";
            var imgTypeSplit = name.Split('.');
            var imgType = imgTypeSplit[imgTypeSplit.Length - 1].ToLower();
            //未知的图片类型
            if (!contentTypDict.ContainsKey(imgType))
            {
                imgPath = errorImage;
            }
            else
            {
                contentTypeStr = contentTypDict[imgType];
            }
            //图片不存在
            if (!new FileInfo(imgPath).Exists)
            {
                imgPath = errorImage;
            }
            //原图
            if (width <= 0)
            {
                using (var sw = new FileStream(imgPath, FileMode.Open))
                {
                    var bytes = new byte[sw.Length];
                    sw.Read(bytes, 0, bytes.Length);
                    sw.Close();
                    return new FileContentResult(bytes, contentTypeStr);
                }
            }
            //缩小图片
            using (var imgBmp = new Bitmap(imgPath))
            {
                //找到新尺寸
                var oWidth = imgBmp.Width;
                var oHeight = imgBmp.Height;
                var height = oHeight;
                if (width > oWidth)
                {
                    width = oWidth;
                }
                else
                {
                    height = width * oHeight / oWidth;
                }
                var newImg = new Bitmap(imgBmp, width, height);
                newImg.SetResolution(72, 72);
                var ms = new MemoryStream();
                newImg.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                var bytes = ms.GetBuffer();
                ms.Close();
                return new FileContentResult(bytes, contentTypeStr);
            }
        }
    }
}
