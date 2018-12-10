using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Oauth2OverOrchardCMS.DTO;

namespace Oauth2OverOrchardCMS.Services

{
    public interface IMediaService : Orchard.IDependency
    {
        /// <summary>
        /// 添加媒体文件
        /// </summary>
        /// <returns></returns>
        Task<string> AddMediaFileAsync(MediaFileDto file);

        /// <summary>
        /// 获取媒体文件Url
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="businessType"></param>
        /// <returns></returns>
        Task<string> GetMediaUrlAsync(int userId, string businessType=null);

       
        /// <summary>
        /// 删除媒体文件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="businessType"></param>
        /// <param name="deleteFile"></param>
        /// <returns></returns>
        Task DeleteMediaFileAsync(int userId, string businessType, bool deleteFile);


        /// <summary>
        /// 获取默认媒体文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        Task<Stream> GetDefaultMediaFile(string fileName=null);

    }
}
