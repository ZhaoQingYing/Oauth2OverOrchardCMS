using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Oauth2OverOrchardCMS
{
    public static class TokenHelper
    {
        public static string BuildHashToken(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);
            var refreshTokenId = Convert.ToBase64String(byteHash);
            return refreshTokenId;
        }

        //public static string BuildHashToken(string input)
        //{
        //    RandomNumberGenerator cryptoRandomAccessTokenGenerator = new RNGCryptoServiceProvider();
        //    byte[] buffer = new byte[50];
        //    cryptoRandomAccessTokenGenerator.GetBytes(buffer);
        //    var refreshTokenId = Convert.ToBase64String(buffer).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        //    return refreshTokenId;
        //}

    }
}