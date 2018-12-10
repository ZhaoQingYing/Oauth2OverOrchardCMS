using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oauth2OverOrchardCMS
{
    public static class Constant
    {

         /// <summary>
        /// 账户创建时默认密码
        /// </summary>
        public const string DefaultAccountPassword = "123456";

        /// <summary>
        /// 默认头像文件名称
        /// </summary>
        public const string DefaultAvatarName = "emp_default.png";


        /// <summary>
        /// 默认头像上传
        /// </summary>
        public const string DefaultAvatar = "0010";
       

        #region 验证码类型 -01


        /// <summary>
        /// -验证码类型
        /// </summary>
        public const string SmsType = "0101";

        /// <summary>
        /// -验证码类型 - 登录
        /// </summary>
        public const string SmsTypeForLogin = "0102";


        #endregion

        #region 用户账户 02

        /// <summary>
        /// 账户头像上传
        /// </summary>
        public const string AccountForAvatar = "0201";


        #endregion


        #region  员工信息 03

        /// <summary>
        ///  员工照片上传
        /// </summary>
        public const string EmployeePhoto = "0301";

        #endregion

        #region  维修单 04

        /// <summary>
        /// 维修单现场照片/视频上传
        /// </summary>
        public const string RepairForLivePhotoOrVideo = "0401";



        #endregion


    }
}