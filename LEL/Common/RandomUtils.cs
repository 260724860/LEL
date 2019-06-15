using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RandomUtils
    {
        /// <summary>
        /// 生成随机字母字符串(数字字母混和)
        /// </summary>
        /// <param name="codeCount">待生成的位数</param>
        public static string GetCheckCode(int codeCount)
        {
            string str = string.Empty;
            int rep = 0;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }

        /// <summary>
        /// 生成随机电话
        /// </summary>
        /// <returns></returns>
        public static string GetRandomTel()
        {
            string[] telStarts = "134,135,136,137,138,139,150,151,152,157,158,159,130,131,132,155,156,133,153,180,181,182,183,185,186,176,187,188,189,177,178".Split(',');
            Random ran = new Random();
            int n = ran.Next(10, 1000);
            int index = ran.Next(0, telStarts.Length - 1);
            string first = telStarts[index];
            string second = (ran.Next(100, 888) + 10000).ToString().Substring(1);
            string thrid = (ran.Next(1, 9100) + 10000).ToString().Substring(1);
            return first + second + thrid;
        }
        /// <summary>
        /// 生成随机中文姓名
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<string> GetNames(int count)
        {
            Random ran = new Random();
            List<string> s = new List<string> { };
            string[] nameS3 = new string[] { "赵", "钱", "孙", "李", "周", "吴", "郑", "王", "冯",
 "陈", "褚", "卫", "蒋", "沈", "韩", "杨", "朱", "秦", "尤", "许", "何", "吕", "施",
 "张", "孔", "曹", "严", "华", "金", "魏", "陶", "姜", "戚", "谢", "邹", "喻", "柏",
 "水", "窦", "章", "云", "苏", "潘", "葛", "奚", "范", "彭", "郎" };
            string[] nameS2 = new string[] {"鲁","韦","昌","马","苗","凤","花","方","俞","任","袁"
 ,"柳","酆","鲍","史","唐","费","廉","岑","薛","雷","贺","倪","汤","滕","殷","罗",
 "毕","郝","邬","安","常","乐","于","时","傅","皮","卞","齐","康","伍","余","元",
 "卜","顾","孟","平","黄"};
            string[] nameS1 = new string[] { "梅", "盛", "林", "刁", "锺", "徐", "邱", "骆", "高",
 "夏", "蔡", "樊", "胡", "凌", "霍", "虞", "万", "支", "柯", "昝", "管", "卢", "莫",
 "经", "房", "裘", "缪", "干", "解", "应", "宗", "丁", "宣", "贲", "邓", "郁", "单",
 "杭", "洪", "包", "诸", "左", "石", "崔", "吉", "钮", "龚", "程", "嵇", "邢", "滑",
 "裴", "陆", "荣", "翁", "荀", "羊", "於", "惠", "甄", "麴", "家", "封", "芮", "羿",
 "储", "靳", "汲", "邴", "糜", "松", "井" };
            for (int i = 0; i < count; i++)
            {
                string s1 = nameS1[ran.Next(0, nameS1.Length - 1)];
                string s2 = nameS2[ran.Next(0, nameS2.Length - 1)];
                string s3 = nameS3[ran.Next(0, nameS3.Length - 1)];
                string name = s1 + s2 + s3;
                if (!s.Contains(name))
                {
                    s.Add(name);
                }
                else
                {
                    i--;
                }
            }
            return s;
        }

        /// <summary>
        /// 生成随机邮箱
        /// </summary>
        /// <returns></returns>
        public static string GetRandomEmail()
        {
            Random ran = new Random();
            int n = ran.Next(0, 18);
            string[] email_suffix = "@gmail.com,@yahoo.com,@msn.com,@hotmail.com,@aol.com,@ask.com,@live.com,@qq.com,@0355.net,@163.com,@163.net,@263.net,@3721.net,@yeah.net,@googlemail.com,@126.com,@sina.com,@sohu.com,@yahoo.com.cn".Split(',');
            string number = GetRandomTel();
            string result = number + email_suffix[n];
            return
                result;
        }

        /// <summary>
        /// 随机产生常用汉字
        /// </summary>
        /// <param name="count">要产生汉字的个数</param>
        /// <returns>常用汉字</returns>
        public static List<string> GenerateChineseWords(int count)
        {
            List<string> chineseWords = new List<string>();
            Random rm = new Random();
            Encoding gb = Encoding.GetEncoding("gb2312");

            for (int i = 0; i < count; i++)
            {
                // 获取区码(常用汉字的区码范围为16-55)
                int regionCode = rm.Next(16, 56);
                // 获取位码(位码范围为1-94 由于55区的90,91,92,93,94为空,故将其排除)
                int positionCode;
                if (regionCode == 55)
                {
                    // 55区排除90,91,92,93,94
                    positionCode = rm.Next(1, 90);
                }
                else
                {
                    positionCode = rm.Next(1, 95);
                }

                // 转换区位码为机内码
                int regionCode_Machine = regionCode + 160;// 160即为十六进制的20H+80H=A0H
                int positionCode_Machine = positionCode + 160;// 160即为十六进制的20H+80H=A0H

                // 转换为汉字
                byte[] bytes = new byte[] { (byte)regionCode_Machine, (byte)positionCode_Machine };
                chineseWords.Add(gb.GetString(bytes));
            }

            return chineseWords;
        }

        /// <summary>
        /// 生成单号
        /// </summary>
        /// <param name="HeadStr">头字符串</param>
        /// <returns></returns>
        public static string GenerateOutTradeNo(string HeadStr)
        {
            var ran = new Random();
            return string.Format("{0}{1}{2}", HeadStr, DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }


        /// <summary>
        /// 生成随机数字 请勿在for循环内使用此方法，会导致产生随机数重复问题
        /// </summary>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static string RandomCode(int Count)
        {
            Random rd = new Random();
            string str = "0123456789";
            string result = "";

            for (int j = 0; j < Count; j++)
            {
                result += str[rd.Next(str.Length)];
            }

            return result;
        }
    }
}
