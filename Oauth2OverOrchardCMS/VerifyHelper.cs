using System;
using System.Text.RegularExpressions;
using System.Text;

namespace Oauth2OverOrchardCMS
{
    public class VerifyHelper
    {
        /// <summary>
        /// 是否是手机号码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsMobile(string phone) {

            return RegexCheck(@"^1\d{10}$", phone);
        }

        private static bool RegexCheck(string strReg, string input)
        {
            var reg = new Regex(strReg);

            return reg.IsMatch(input);

        }


    }
}