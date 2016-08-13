using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Management;
using Microsoft.Win32;
using System.IO;

namespace ModernUI.Common.Utility.Security
{
    public class RsaUtil
    {
        #region 读取密钥文件
        /// <summary>
        /// 读取公钥
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadKeyFile(string path)
        {
            StreamReader reader = new StreamReader(path);
            string keystr = reader.ReadToEnd();
            reader.Close();
            return keystr;
        }

        /// <summary>
        /// 读取公钥
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadKeyFile(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string keystr = reader.ReadToEnd();
            reader.Close();
            return keystr;
        }

        #endregion 读取密钥文件

        #region 签名以及签名验证

        /// <summary>
        /// 对SHA1加密后的密文进行签名
        /// </summary>
        /// <param name="p_strKeyPrivate">私钥</param>
        /// <param name="m_strHashbyteSignature">SHA1加密后的密文</param>
        /// <returns></returns>
        public string SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature)
        {
            byte[] rgbHash = Convert.FromBase64String(m_strHashbyteSignature);
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.FromXmlString(p_strKeyPrivate);
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
            formatter.SetHashAlgorithm("SHA1");
            SHA1Managed sha = new SHA1Managed();
            byte[] result = sha.ComputeHash(rgbHash);
            byte[] inArray = formatter.CreateSignature(result);
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="p_strKeyPublic">公钥</param>
        /// <param name="p_strHashbyteDeformatter">待验证的用户名</param>
        /// <param name="p_strDeformatterData">注册码</param>
        /// <returns></returns>
        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, string p_strDeformatterData)
        {
            try
            {
                byte[] rgbHash = Convert.FromBase64String(p_strHashbyteDeformatter);
                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                key.FromXmlString(p_strKeyPublic);
                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
                deformatter.SetHashAlgorithm("SHA1");
                byte[] rgbSignature = Convert.FromBase64String(p_strDeformatterData);
                SHA1Managed sha = new SHA1Managed();

                if (deformatter.VerifySignature(sha.ComputeHash(rgbHash), rgbSignature))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        
        #endregion 签名以及签名验证

        #region 加密解密
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="m_strEncryptString">MD5加密后的数据</param>
        /// <returns>RSA公钥加密后的数据</returns>
        public string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {
            string str2;
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPublicKey);
                byte[] bytes = new UnicodeEncoding().GetBytes(m_strEncryptString);
                str2 = Convert.ToBase64String(provider.Encrypt(bytes, false));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="m_strDecryptString">待解密的数据</param>
        /// <returns>解密后的结果</returns>
        public string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {
            string str2;
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPrivateKey);
                byte[] rgb = Convert.FromBase64String(m_strDecryptString);
                byte[] buffer2 = provider.Decrypt(rgb, false);
                str2 = new UnicodeEncoding().GetString(buffer2);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }
        
        #endregion

    }
}
