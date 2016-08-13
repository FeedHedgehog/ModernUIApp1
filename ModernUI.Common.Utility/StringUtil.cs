using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ModernUI.Common.Utility
{
    public class StringUtil
    {
        /// <summary>
        /// 验证字符串中是否包含特殊字符
        /// </summary>
        /// <param name="sValidateOject"></param>
        /// <returns>包含true，不包含false</returns>
        public static bool VerifySpecialChar(object ValidateOject)
        {
            char[] sSpecialChar = {'~','!','@','#','$','%','^','`','&','*','=','+','"','\'','<','>','\\','/',
                                   '　', '·', '！', '￥', '…', '—', '、', '｛', '｝', '【', '】',
                                   '，', '。', '《', '》', '？', '：', '；', '“', '”', '‘', '’', '～', '＠', 
                                   '＃', '￥', '％', '＆', '×', '＝', '＋', '＼', '｜', '．', '｀' };
            for (int i = 0; i < sSpecialChar.Length; i++)
            {
                if (ValidateOject.ToString().Contains(sSpecialChar[i].ToString()))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 验证字符串是否包含SQL关键字
        /// </summary>
        /// <param name="str"></param>
        /// <returns>包含true，不包含false</returns>
        public static bool CheckBadWord(string ValidateOject)
        {
            string pattern = @"select|insert|delete|from|count\(|drop table|update|truncate|asc\(|mid\(|char\(|xp_cmdshell|exec   master|netlocalgroup administrators|net user|or|and";
            if (Regex.IsMatch(ValidateOject, pattern, RegexOptions.IgnoreCase))
                return true;
            return false;
        }

        /// <summary>
        /// 验证字符串是否为正整数
        /// </summary>
        /// <param name="sValidateString">验证字符串</param>
        /// <returns></returns>
        public static bool VerifyInt(object ValidateOject)
        {
            if (ValidateOject != null)
            {
                Regex regex = new Regex(@"^[0-9]\d*$");
                return regex.IsMatch(ValidateOject.ToString());
            }
            else
                return false;
        }

        /// <summary>
        /// 验证字符串是否为小数
        /// </summary>
        /// <param name="sValidateString">验证字符串</param>
        /// <returns></returns>
        public static bool VerifyDouble(object ValidateOject)
        {
            if (ValidateOject != null)
            {
                Regex regex = new Regex(@"^\d+(\.\d+)?$");
                return regex.IsMatch(ValidateOject.ToString());
            }
            else
                return false;
        }

        /// <summary>
        /// 验证路径是否合法
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns></returns>
        public static bool VerifyPath(string Path)
        {
            string pattern = @"^[a-zA-Z]:(((\\(?! )[^/:*?<>\""|\\]+)+\\?)|(\\)?)\s*$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(Path))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 验证手机号
        /// </summary>
        public static bool VerifyPhone(string Phone)
        {
            string pattern = @"(^18\d{9}$)|(^13\d{9}$)|(^15\d{9}$)";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(Phone))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 验证身份证
        /// </summary>
        public static bool VerifyIdCard(string IdCard)
        {
            string pattern = @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(IdCard))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 验证邮箱
        /// </summary>
        public static bool VerifyEmail(string Email)
        {
            string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(Email))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 验证IP
        /// </summary>
        public static bool VerifyIp(object ValidateOject)
        {
            if (ValidateOject != null)
            {
                Regex regex = new Regex(@"((25[0-5]|2[0-4]\d|1?\d?\d)\.){3}(25[0-5]|2[0-4]\d|1?\d?\d)");
                return regex.IsMatch(ValidateOject.ToString());
            }
            else
                return false;
        }

        ///<summary>
        ///生成随机字符串
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public string GetRnd(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="paw"></param>
        /// <returns></returns>
        public static string MD5(string paw)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] by = System.Text.Encoding.UTF8.GetBytes(paw);
            byte[] input = md5.ComputeHash(by);
            string OutString = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                OutString += input[i].ToString("x2").ToLower();
            }
            return OutString;
        }

        /// <summary> 转半角的函数(DBC case) </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }


        /// <summary>
        /// 对字符串进行base64编码
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>base64编码串</returns>
        public static string Base64StringEncode(string input)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(encbuff);
        }

        /// <summary>
        /// 对字符串进行反编码
        /// </summary>
        /// <param name="input">base64编码串</param>
        /// <returns>字符串</returns>
        public static string Base64StringDecode(string input)
        {
            byte[] decbuff = Convert.FromBase64String(input);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }

        /// <summary>
        /// 空格转换成&nbsp;
        /// </summary>
        /// <param name="input">要进行转换的字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string SpaceToNbsp(string input)
        {
            string space = "&nbsp;";
            return input.Replace(" ", space);
        }

        /// <summary>
        /// 换行符转换成Html标签的换行符<br />
        /// </summary>
        /// <param name="input">要进行处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string NewLineToBreak(string input)
        {
            Regex regEx = new Regex(@"[\n|\r]+");
            return regEx.Replace(input, "<br />");
        }

        /// <summary>
        /// 去除"<" 和 ">" 符号之间的内容
        /// </summary>
        /// <param name="input">要进行处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string StripTags(string input)
        {
            Regex stripTags = new Regex("<(.|\n)+?>");
            return stripTags.Replace(input, string.Empty);
        }

        /// <summary>
        /// 去除字符串内的空白字符
        /// </summary>
        /// <param name="input">要进行处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string TrimIntraWords(string input)
        {
            Regex regEx = new Regex(@"[\s]+");
            return regEx.Replace(input, " ");
        }

        /// <summary>   
        /// 截取左边规定字数字符串,超过字数用...结束   
        /// </summary>   
        /// <param name="str">需截取字符串</param>   
        /// <param name="length">截取字数</param>   
        /// <returns>返回截取字符串</returns>   
        public static string GetLeftStr(string str, int length)
        {
            string reStr;
            if (length < str.Length)
            {
                reStr = str.Substring(0, length) + "...";
            }
            else
            {
                reStr = str;
            }
            return reStr;
        }

        /// <summary>   
        /// 获得双字节字符串的字节数   
        /// </summary>   
        /// <param name="str">要检测的字符串</param>   
        /// <returns>返回字节数</returns>   
        public static int GetStrLength(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            ASCIIEncoding n = new ASCIIEncoding();
            byte[] b = n.GetBytes(str);
            int l = 0;  // l 为字符串之实际长度   
            for (int i = 0; i < b.Length; i++)
            {
                //判断是否为汉字或全脚符号   
                if (b[i] == 63)  
                {
                    l++;
                }
                l++;
            }
            return l;
        }

        /// <summary>
        /// 过滤低位非打印字符
        /// </summary>
        public static object ReplaceASCIICharacters(object tmp)
        {
            if (tmp == null)
                return null;
            StringBuilder info = new StringBuilder();
            foreach (char cc in tmp.ToString())
            {
                int ss = (int)cc;
                if (((ss >= 0) && (ss <= 8)) || ((ss >= 11) && ss <= 12) || ((ss >= 14) && (ss <= 32)))
                {
                    info.AppendFormat(string.Empty, ss);
                }
                else
                    info.Append(cc);
            }
            return info.ToString();
        }
    }
}
