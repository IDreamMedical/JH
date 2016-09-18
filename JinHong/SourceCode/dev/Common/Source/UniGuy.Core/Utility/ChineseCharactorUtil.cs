using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace UniGuy.Core.Utility
{
    //把汉字转换成拼音的类
    public static class ChineseCharactorUtil
    {
        #region Pinyin
        private static int[] pyValue = new int[]
        {
            -20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,
            -20032,-20026,-20002,-19990,-19986,-19982,-19976,-19805,-19784,-19775,-19774,-19763,
            -19756,-19751,-19746,-19741,-19739,-19728,-19725,-19715,-19540,-19531,-19525,-19515,
            -19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263,-19261,-19249,
            -19243,-19242,-19238,-19235,-19227,-19224,-19218,-19212,-19038,-19023,-19018,-19006,
            -19003,-18996,-18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,
            -18731,-18722,-18710,-18697,-18696,-18526,-18518,-18501,-18490,-18478,-18463,-18448,
            -18447,-18446,-18239,-18237,-18231,-18220,-18211,-18201,-18184,-18183, -18181,-18012,
            -17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,
            -17733,-17730,-17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,
            -17468,-17454,-17433,-17427,-17417,-17202,-17185,-16983,-16970,-16942,-16915,-16733,
            -16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459,-16452,-16448,
            -16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,
            -16212,-16205,-16202,-16187,-16180,-16171,-16169,-16158,-16155,-15959,-15958,-15944,
            -15933,-15920,-15915,-15903,-15889,-15878,-15707,-15701,-15681,-15667,-15661,-15659,
            -15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416,-15408,-15394,
            -15385,-15377,-15375,-15369,-15363,-15362,-15183,-15180,-15165,-15158,-15153,-15150,
            -15149,-15144,-15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,
            -14941,-14937,-14933,-14930,-14929,-14928,-14926,-14922,-14921,-14914,-14908,-14902,
            -14894,-14889,-14882,-14873,-14871,-14857,-14678,-14674,-14670,-14668,-14663,-14654,
            -14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,
            -14170,-14159,-14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,
            -14109,-14099,-14097,-14094,-14092,-14090,-14087,-14083,-13917,-13914,-13910,-13907,
            -13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658,-13611,-13601,
            -13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,
            -13340,-13329,-13326,-13318,-13147,-13138,-13120,-13107,-13096,-13095,-13091,-13076,
            -13068,-13063,-13060,-12888,-12875,-12871,-12860,-12858,-12852,-12849,-12838,-12831,
            -12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346,-12320,-12300,
            -12120,-12099,-12089,-12074,-12067,-12058,-12039,-11867,-11861,-11847,-11831,-11798,
            -11781,-11604,-11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,
            -11055,-11052,-11045,-11041,-11038,-11024,-11020,-11019,-11018,-11014,-10838,-10832,
            -10815,-10800,-10790,-10780,-10764,-10587,-10544,-10533,-10519,-10331,-10329,-10328,
            -10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254
        };

        private static string[] pyName = new string[]
        {
            "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben",
            "Beng","Bi","Bian","Biao","Bie","Bin","Bing","Bo","Bu","Ba","Cai","Can",
            "Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng",
            "Chi","Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong",
            "Cou","Cu","Cuan","Cui","Cun","Cuo","Da","Dai","Dan","Dang","Dao","De",
            "Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
            "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo",
            "Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge","Gei","Gen","Geng","Gong",
            "Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han",
            "Hang","Hao","He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan",
            "Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang","Jiao","Jie","Jin","Jing",
            "Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke",
            "Ken","Keng","Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo",
            "La","Lai","Lan","Lang","Lao","Le","Lei","Leng","Li","Lia","Lian","Liang",
            "Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun",
            "Luo","Ma","Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian",
            "Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na","Nai","Nan","Nang",
            "Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning",
            "Niu","Nong","Nu","Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan",
            "Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin","Ping","Po",
            "Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu",
            "Quan","Que","Qun","Ran","Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou",
            "Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se","Sen",
            "Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu",
            "Shua","Shuai","Shuan","Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan",
            "Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te","Teng","Ti","Tian",
            "Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai",
            "Wan","Wang","Wei","Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao",
            "Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun","Ya","Yan","Yang",
            "Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun",
            "Za", "Zai","Zan","Zang","Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan",
            "Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu","Zhua","Zhuai","Zhuan",
            "Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"
        };
        /// <summary>
        /// 把汉字转换成拼音(全拼)
        /// </summary>
        /// <param name="hzString">汉字字符串</param>
        /// <returns>转换后的拼音(全拼)字符串</returns>
        public static string ConvertToPinyin(string hzString, bool bWholePinyin)
        {
            // 匹配中文字符
            Regex regex = new Regex("^[\u4e00-\u9fa5]$");
            byte[] array = new byte[2];
            string pyString = "";
            int chrAsc = 0;
            int i1 = 0;
            int i2 = 0;
            char[] noWChar = hzString.ToCharArray();

            for (int j = 0; j < noWChar.Length; j++)
            {
                // 中文字符
                if (regex.IsMatch(noWChar[j].ToString()))
                {
                    array = System.Text.Encoding.Default.GetBytes(noWChar[j].ToString());
                    i1 = (short)(array[0]);
                    i2 = (short)(array[1]);
                    chrAsc = i1 * 256 + i2 - 65536;
                    if (chrAsc > 0 && chrAsc < 160)
                    {
                        pyString += noWChar[j];
                    }
                    else
                    {
                        // 修正部分文字
                        if (chrAsc == -9254)  // 修正“圳”字
                            pyString += bWholePinyin?"Zhen":"Z";
                        else if (noWChar[j] == '倩')    //  不是Zhuo
                            pyString += bWholePinyin?"Qian":"Q";
                        else
                        {
                            for (int i = (pyValue.Length - 1); i >= 0; i--)
                            {
                                if (pyValue[i] <= chrAsc)
                                {
                                    if (bWholePinyin)
                                        pyString += pyName[i];
                                    else
                                        pyString += pyName[i].Substring(0, 1);
                                    break;
                                }
                            }
                        }
                    }
                }
                // 非中文字符
                else
                {
                    pyString += noWChar[j].ToString();
                }
            }
            return pyString;
        }
        public static string ConvertToPinyin(string hzString)
        {
            return ConvertToPinyin(hzString, true);
        }
        #endregion

        #region 判断是否为中文
        /*
             * C# 判断中文字符（字符串） 
             * 在unicode 字符串中，中文的范围是在4E00..9FFF:CJK Unified Ideographs。
             * 通过对字符的unicode编码进行判断来确定字符是否为中文。
             */
        public static bool IsChineseLetter(string input, int index)
        {
            int code = 0;
            int chfrom = Convert.ToInt32("4e00", 16);    //范围（0x4e00～0x9fff）转换成int（chfrom～chend）
            int chend = Convert.ToInt32("9fff", 16);
            if (input != "")
            {
                code = Char.ConvertToUtf32(input, index);    //获得字符串input中指定索引index处字符unicode编码
                if (code >= chfrom && code <= chend)
                {
                    return true;     //当code在中文范围内返回true
                }
                else
                {
                    return false;    //当code不在中文范围内返回false
                }
            }
            return false;
        }
        /// <summary>
        /// 判断句子中是否含有中文
        /// </summary>
        /// <param >字符串</param>
        public static bool ContainsChineseCharacter(string words)
        {
            string TmmP;
            for (int i = 0; i < words.Length; i++)
            {
                TmmP = words.Substring(i, 1);
                byte[] sarr = System.Text.Encoding.GetEncoding("gb2312").GetBytes(TmmP);
                if (sarr.Length == 2)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsChineseCharacter(char c)
        {
            Regex rx = new Regex("^[\u4e00-\u9fa5]$");
            return rx.IsMatch(c.ToString());
        }
        public static bool IsAllChineseCharacter(string str)
        {
            Regex rx = new Regex("^[\u4e00-\u9fa5]*$");
            return rx.IsMatch(str);
        }
        #endregion

        #region Inner types
        public class ChineseNumber
        {
            private static string MoneyNum = "零一二三四五六七八九";
            private static string MoneyUnit = "十百千万十百千亿";
            private static string CashUnit = "元角分整";
            private static string[] BeforeReplace = new string[] {  MoneyNum[0].ToString() + MoneyNum[0].ToString(),
                                                                MoneyUnit[7].ToString() + MoneyNum[0].ToString() + MoneyUnit[3].ToString() ,
                                                                MoneyNum[0].ToString() + MoneyUnit[3].ToString(),
                                                                MoneyNum[0].ToString() + MoneyUnit[7].ToString(),
                                                                MoneyUnit[7].ToString() + MoneyUnit[3].ToString(),
                                                                MoneyNum[0].ToString() + MoneyNum[0].ToString(),
                                                                MoneyNum[0].ToString() + CashUnit[1].ToString() + MoneyNum[0].ToString() + CashUnit[2].ToString(),
                                                                MoneyNum[0].ToString() + CashUnit[2].ToString()
                                                             };
            private static string[] AfterReplace = new string[] {  MoneyNum[0].ToString(),
                                                                MoneyUnit[7].ToString() + MoneyNum[0].ToString(),
                                                                MoneyUnit[3].ToString() + MoneyNum[0].ToString(),
                                                                MoneyUnit[7].ToString() + MoneyNum[0].ToString(),
                                                                MoneyUnit[7].ToString() + MoneyNum[0].ToString(),
                                                                MoneyNum[0].ToString(),
                                                                String.Empty,
                                                                String.Empty
                                                             };

            /**/
            /// <summary>
            /// 字符串替换方法，另外一种是以 4 个数字为基础的递归方法
            /// Create By HJ 2007-10-21
            /// </summary>
            /// <param name="moneyNum"></param>
            /// <returns></returns>
            public static string GetChineseNumber(decimal moneyNum)
            {
                string intNum, point;
                /**/
                ///取整数部分
                intNum = Convert.ToString(Math.Floor(moneyNum));
                /**/
                ///取小数部分
                point = Convert.ToString(Math.Floor(moneyNum * 100));
                point = point.Substring(point.Length - 2);

                /**/
                ///计算整数部分
                for (int i = 0; i < MoneyNum.Length; i++)
                {
                    intNum = intNum.Replace(i.ToString(), MoneyNum[i].ToString());
                }
                int intNumLength = intNum.Length;
                for (int i = intNumLength - 1; i > 0; i--)
                {   /**////根据位数把单位加上
                    ///如果是零则不加单位，但是 万  和  亿 需要加上
                    if (intNum[i - 1] == MoneyNum[0] && (intNumLength - i + 7) % 8 != 3 && (intNumLength - i + 7) % 8 != 7) continue;
                    intNum = intNum.Insert(i, MoneyUnit[(intNumLength - i + 7) % 8].ToString());
                }

                /**/
                ///加上 角 和 分 的单位
                intNum += CashUnit[0].ToString() + MoneyNum.Substring(Convert.ToInt16(point[0].ToString()), 1) + CashUnit[1].ToString() + MoneyNum.Substring(Convert.ToInt16(point[1].ToString()), 1) + CashUnit[2].ToString();

                /**/
                ///替换  零零 -> 零  亿零万  ->  亿零，零万  ->  万零，零亿 -> 亿零，亿万 -> 亿零，零角零分 - > ""，零分 - > ""，零零 -> 零，再调用一次，确保 亿零万 替换后的情况
                for (int i = 0; i < BeforeReplace.Length; i++)
                {
                    while (intNum.IndexOf(BeforeReplace[i]) > -1)
                    {
                        intNum = intNum.Replace(BeforeReplace[i], AfterReplace[i]);
                    }
                }

                /**/
                ///最后的 零 去掉
                if (intNum.EndsWith(MoneyNum[0].ToString())) intNum = intNum.Substring(0, intNum.Length - 1);

                return intNum + CashUnit[3].ToString();
            }
            public static string GetChineseNumber(int moneyNum)
            {
                return GetChineseNumber((decimal)moneyNum);
            }
            public static string GetChineseNumber(long moneyNum)
            {
                return GetChineseNumber((decimal)moneyNum);
            }
            public static string GetChineseNumber(double moneyNum)
            {
                return GetChineseNumber((decimal)moneyNum);
            }
        }

        public class ChineseNumber2
        {
            /*
            一个货币数字转换中文的算法，注意：本文中的算法支持小于1023 (也就是9999亿兆)货币数字转化。
　　货币中文说明： 在说明代码之前，首先让我们回顾一下货币的读法。
　　10020002.23　读为 壹仟零贰万零贰元贰角叁分
　　1020　　　　 读为 壹仟零贰拾元整。
　　100000　　　 读为 拾万元整
　　0.13　　　　 读为 壹角叁分
             */
            /// <summary>
            /// 数位
            /// </summary>
            public enum NumLevel { Cent, Chiao, Yuan, Ten, Hundred, Thousand, TenThousand, hundredMillon, Trillion };
            /// <summary>
            /// 数位的指数
            /// </summary>
            private int[] NumLevelExponent = new int[] { -2, -1, 0, 1, 2, 3, 4, 8, 12 };
            /// <summary>
            /// 数位的中文字符
            /// </summary>
            private string[] NumLeverChineseSign = new string[] { "分", "角", "元", "拾", "佰", "仟", "万", "亿", "兆" };
            /// <summary>
            /// 大写字符
            /// </summary>
            private string[] NumChineseCharacter = new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            /// <summary>
            /// 整(当没有 角分 时)
            /// </summary>
            private const string EndOfInt = "整";

            /// <summary>
            /// 是否大于兆，如果大于就把字符串分为两部分，
            /// 一部分是兆以前的数字
            /// 另一部分是兆以后的数字
            /// 把长数字分割为两个较小的数字数组，例如把9999亿兆，分割为9999亿和0兆，因为计算机不支持过长的数字。
            /// </summary>
            /// <param name="Num"></param>
            /// <returns></returns>
            private bool IsBigThanTillion(string Num)
            {
                bool isBig = false;
                if (Num.IndexOf('.') != -1)
                {
                    //如果大于兆
                    if (Num.IndexOf('.') > NumLevelExponent[(int)NumLevel.Trillion])
                    {
                        isBig = true;
                    }
                }
                else
                {
                    //如果大于兆
                    if (Num.Length > NumLevelExponent[(int)NumLevel.Trillion])
                    {
                        isBig = true;
                    }
                }
                return isBig;
            }
            /// <summary>
            /// 把数字字符串由‘兆’分开两个
            /// </summary>
            /// <returns></returns>
            private double[] SplitNum(string Num)
            {
                //兆的开始位
                double[] TillionLevelNums = new double[2];
                int trillionLevelLength;
                if (Num.IndexOf('.') == -1)
                    trillionLevelLength = Num.Length - NumLevelExponent[(int)NumLevel.Trillion];
                else
                    trillionLevelLength = Num.IndexOf('.') - NumLevelExponent[(int)NumLevel.Trillion];
                //兆以上的数字
                TillionLevelNums[0] = Convert.ToDouble(Num.Substring(0, trillionLevelLength));
                //兆以下的数字
                TillionLevelNums[1] = Convert.ToDouble(Num.Substring(trillionLevelLength));
                return TillionLevelNums;
            }

            /// <summary>
            /// 是否以“壹拾”开头，如果是就可以把它变为“拾”
            /// </summary>
            /// <param name="Num"></param>
            /// <returns></returns>
            private bool IsStartOfTen(double Num)
            {
                bool isStartOfTen = false;
                while (Num >= 10)
                {
                    if (Num == 10)
                    {
                        isStartOfTen = true;
                        break;
                    }
                    //Num的数位
                    NumLevel currentLevel = GetNumLevel(Num);
                    int numExponent = this.NumLevelExponent[(int)currentLevel];
                    Num = Convert.ToInt32(Math.Floor(Num / Math.Pow(10, numExponent)));
                    if (currentLevel == NumLevel.Ten && Num == 1)
                    {
                        isStartOfTen = true;
                        break;
                    }
                }
                return isStartOfTen;
            }

            /// <summary>
            /// 外部调用的转换方法
            /// </summary>
            /// <param name="Num"></param>
            /// <returns></returns>
            public string ConvertToChinese(string Num)
            {
                if (!IsValidated<string>(Num))
                {
                    throw new OverflowException("数值格式不正确，请输入小于9999亿兆的数字且最多精确的分的金额！");
                }
                string chineseCharactor = string.Empty;
                if (IsBigThanTillion(Num))
                {
                    double[] tillionNums = SplitNum(Num);
                    chineseCharactor = ContactNumChinese(tillionNums);
                }
                else
                {
                    double dNum = Convert.ToDouble(Num);
                    chineseCharactor = CalculateChineseSign(dNum, null, true, IsStartOfTen(dNum));
                }
                return chineseCharactor;
            }

            /// <summary>
            /// 获取数字的数位　使用log
            /// 获取数位 例如 1000的数位为 NumLevel.Thousand
            /// </summary>
            /// <param name="Num"></param>
            /// <returns></returns>
            private NumLevel GetNumLevel(double Num)
            {
                double numLevelLength;
                NumLevel NLvl = new NumLevel();
                if (Num > 0)
                {
                    numLevelLength = Math.Floor(Math.Log10(Num));
                    for (int i = NumLevelExponent.Length - 1; i >= 0; i--)
                    {
                        if (numLevelLength >= NumLevelExponent[i])
                        {
                            NLvl = (NumLevel)i;
                            break;
                        }
                    }
                }
                else
                {
                    NLvl = NumLevel.Yuan;
                }
                return NLvl;
            }

            /// <summary>
            /// 是否跳位
            /// 判断数字之间是否有跳位，也就是中文中间是否要加零，例如1020 就应该加零。
            /// </summary>
            /// <returns></returns>
            private bool IsDumpLevel(double Num)
            {
                if (Num > 0)
                {
                    NumLevel? currentLevel = GetNumLevel(Num);
                    NumLevel? nextLevel = null;
                    int numExponent = this.NumLevelExponent[(int)currentLevel];
                    double postfixNun = Math.Round(Num % (Math.Pow(10, numExponent)), 2);
                    if (postfixNun > 0)
                        nextLevel = GetNumLevel(postfixNun);
                    if (currentLevel != null && nextLevel != null)
                    {
                        if (currentLevel > nextLevel + 1)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            /// <summary>
            /// 合并分开的数组中文货币字符
            /// </summary>
            /// <param name="tillionNums"></param>
            /// <returns></returns>
            private string ContactNumChinese(double[] tillionNums)
            {
                string uptillionStr = CalculateChineseSign(tillionNums[0], NumLevel.Trillion, true, IsStartOfTen(tillionNums[0]));
                string downtrillionStr = CalculateChineseSign(tillionNums[1], null, true, false);
                string chineseCharactor = string.Empty;
                //分开后的字符是否有跳位
                if (GetNumLevel(tillionNums[1] * 10) == NumLevel.Trillion)
                {
                    chineseCharactor = uptillionStr + NumLeverChineseSign[(int)NumLevel.Trillion] + downtrillionStr;
                }
                else
                {
                    chineseCharactor = uptillionStr + NumLeverChineseSign[(int)NumLevel.Trillion];
                    if (downtrillionStr != "零元整")
                    {
                        chineseCharactor += NumChineseCharacter[0] + downtrillionStr;
                    }
                    else
                    {
                        chineseCharactor += "元整";
                    }
                }
                return chineseCharactor;
            }

            /// <summary>
            /// 计算中文字符串
            /// 递归计算货币数字的中文
            /// </summary>
            /// <param name="Num">数字</param>
            /// <param name="NL">数位级别 比如1000万的 数位级别为万</param>
            /// <param name="IsExceptTen">是否以‘壹拾’开头</param>
            /// <returns>中文大写</returns>
            public string CalculateChineseSign(double Num, NumLevel? NL, bool IsDump, bool IsExceptTen)
            {
                Num = Math.Round(Num, 2);
                bool isDump = false;
                //Num的数位
                NumLevel? currentLevel = GetNumLevel(Num);
                int numExponent = this.NumLevelExponent[(int)currentLevel];
                string Result = string.Empty;
                //整除后的结果
                int prefixNum;
                //余数 当为小数的时候 分子分母各乘100
                double postfixNun;
                if (Num >= 1)
                {
                    prefixNum = Convert.ToInt32(Math.Floor(Num / Math.Pow(10, numExponent)));
                    postfixNun = Math.Round(Num % (Math.Pow(10, numExponent)), 2);
                }
                else
                {
                    prefixNum = Convert.ToInt32(Math.Floor(Num * 100 / Math.Pow(10, numExponent + 2)));
                    postfixNun = Math.Round(Num * 100 % (Math.Pow(10, numExponent + 2)), 2);
                    postfixNun *= 0.01;
                }
                if (prefixNum < 10)
                {
                    //避免以‘壹拾’开头
                    if (!(NumChineseCharacter[(int)prefixNum] == NumChineseCharacter[1]
                    && currentLevel == NumLevel.Ten && IsExceptTen))
                    {
                        Result += NumChineseCharacter[(int)prefixNum];
                    }
                    else
                    {
                        IsExceptTen = false;
                    }
                    //加上单位
                    if (currentLevel == NumLevel.Yuan)
                    {
                        ////当为 “元” 位不为零时 加“元”。
                        if (NL == null)
                        {
                            Result += NumLeverChineseSign[(int)currentLevel];
                            //当小数点后为零时 加 "整"
                            if (postfixNun == 0)
                            {
                                Result += EndOfInt;
                            }
                        }
                    }
                    else
                    {
                        Result += NumLeverChineseSign[(int)currentLevel];
                    }
                    //当真正的个位为零时　加上“元”
                    if (NL == null && postfixNun < 1 && currentLevel > NumLevel.Yuan && postfixNun > 0)
                    {
                        Result += NumLeverChineseSign[(int)NumLevel.Yuan];
                    }
                }
                else
                {
                    //当 前缀数字未被除尽时， 递归下去
                    NumLevel? NextNL = null;
                    if ((int)currentLevel >= (int)(NumLevel.TenThousand))
                        NextNL = currentLevel;
                    Result += CalculateChineseSign((double)prefixNum, NextNL, isDump, IsExceptTen);
                    if ((int)currentLevel >= (int)(NumLevel.TenThousand))
                    {
                        Result += NumLeverChineseSign[(int)currentLevel];
                    }
                }
                //是否跳位
                // 判断是否加零， 比如302 就要给三百 后面加零，变为 三百零二。
                if (IsDumpLevel(Num))
                {
                    Result += NumChineseCharacter[0];
                    isDump = true;
                }
                //余数是否需要递归
                if (postfixNun > 0)
                {
                    Result += CalculateChineseSign(postfixNun, NL, isDump, false);
                }
                else if (postfixNun == 0 && currentLevel > NumLevel.Yuan)
                {
                    //当数字是以零元结尾的加上 元整 比如1000000一百万元整
                    if (NL == null)
                    {
                        Result += NumLeverChineseSign[(int)NumLevel.Yuan];
                        Result += EndOfInt;
                    }
                }
                return Result;
            }

            /// <summary>
            /// 正则表达验证数字是否合法
            /// </summary>
            /// <param name="Num"></param>
            /// <returns></returns>
            public bool IsValidated<T>(T Num)
            {
                Regex reg = new Regex(@"^(([0])|([1-9]d{0,23}))(.d{1,2})?$");
                if (reg.IsMatch(Num.ToString()))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Made by wj. 20111120
        /// </summary>
        public class ChineseNumber3
        {
            private static string[] chineseNumber = new string[] { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            private static string[] bigUnit = new string[] {"",  "万", "亿", "万亿" };
            private static string[] smallUnit = new string[] {"", "十", "百", "千" };
            private static string zero = "零";
            private static string dot = "点";
            private static string negetive = "负";

            /// <summary>
            /// 解析一个10000以内的整数到中文
            /// </summary>
            /// <param name="number"></param>
            /// <returns></returns>
            public static string ParsePositiveIntegerWithinThousandToChinese(long number, out bool lastZero)
            {
                lastZero = false;

                if (number == 0)
                    return zero;

                string temp = null;

                int i = 0;

                ppiwttc0:
                long temp0 = number % 10;
                if (temp0 != 0)
                {
                    temp = chineseNumber[temp0] + smallUnit[i] + temp;
                    lastZero = false;
                }
                else
                {
                    if (i != 0 && !lastZero && temp != null)
                    {
                        temp = zero + temp;
                        lastZero = true;
                    }
                }
                if ((number = number / 10) > 0)
                {
                    i++;
                    goto ppiwttc0;
                }

                return temp;
            }

            public static string ParsePositiveIntegerWithinThousandToChinese(long number)
            {
                bool lastZero = false;
                return ParsePositiveIntegerWithinThousandToChinese(number, out lastZero);
            }

            public static string ParseIntegerWithinThousandToChinese(long number, out bool lastZero)
            {
                if (number >= 0)
                    return ParsePositiveIntegerWithinThousandToChinese(number, out lastZero);
                return negetive + ParsePositiveIntegerWithinThousandToChinese(-number, out lastZero);
            }

            public static string ParseIntegerWithinThousandToChinese(long number)
            {
                bool lastZero = false;
                return ParseIntegerWithinThousandToChinese(number, out lastZero);
            }

            /// <summary>
            /// 解析小数部分(必须是1以下的数,否则不准)
            /// </summary>
            /// <param name="number"></param>
            /// <returns></returns>
            public static string ParseDecimalPartToChinese(double number)
            {
                string ret = null;

                string s = number.ToString();
                int index = s.IndexOf('.');
                for (int i = index + 1; i < s.Length; i++)
                    ret += chineseNumber[s[i] - 0x30];

                return ret;
            }

            public static string ParseDecimalPartToChinese(decimal number)
            {
                string ret = null;

                string s = number.ToString();
                int index = s.IndexOf('.');
                for (int i = index + 1; i < s.Length; i++)
                    ret += chineseNumber[s[i] - 0x30];

                return ret;
            }

            /// <summary>
            /// 解析整数
            /// </summary>
            /// <param name="number"></param>
            /// <returns></returns>
            public static string ParsePositiveIntegerToChinese(long number, out bool lastZero)
            {
                lastZero = false;

                if (number == 0)
                    return zero;

                string temp = null;
                int i = 0;

            pitt0:
                int div = 10000;
                for (int j = 1; j < i; j++)
                    div *= 10000;
                long temp0 = number % div;
                if (temp0 != 0)
                {
                    if (i == 0)
                        temp = ParseIntegerWithinThousandToChinese(temp0) + bigUnit[i] + temp;
                    else
                        temp = ParsePositiveIntegerToChinese(temp0) + bigUnit[i] + temp;
                    lastZero = false;
                }
                else
                {
                    if (i != 0 && !lastZero && temp != null)
                    {
                        temp = zero + temp;
                        lastZero = true;
                    }
                }
                if ((number = number / div) > 0)
                {
                    i++;
                    goto pitt0;
                }

                return temp;
            }

            public static string ParsePositiveIntegerToChinese(long number)
            {
                bool lastZero = false;
                return ParsePositiveIntegerToChinese(number, out lastZero);
            }

            public static string ParseIntegerToChinese(long number)
            {
                if (number >= 0)
                    return ParsePositiveIntegerToChinese(number);
                return negetive + ParsePositiveIntegerToChinese(-number);
            }

            /// <summary>
            /// 解析浮点数
            /// </summary>
            /// <param name="numer"></param>
            /// <returns></returns>
            public static string ParsePositiveDoubleToChinese(double number, out bool lastZero)
            {
                long integerPart = (long)Math.Floor(number);
                double decimalPart = number - integerPart;
                string ret = ParsePositiveIntegerToChinese(integerPart, out lastZero);
                if (decimalPart != 0)
                    ret += dot + ParseDecimalPartToChinese(decimalPart);
                return ret;
            }

            public static string ParsePositiveDoubleToChinese(double number)
            {
                bool lastZero = false;
                return ParsePositiveDoubleToChinese(number, out lastZero);
            }

            public static string ParsePositiveDecimalToChinese(decimal number, out bool lastZero)
            {
                long integerPart = (long)Math.Floor(number);
                decimal decimalPart = number - integerPart;
                string ret = ParsePositiveIntegerToChinese(integerPart, out lastZero);
                if (decimalPart != 0)
                    ret += dot + ParseDecimalPartToChinese(decimalPart);
                return ret;
            }

            public static string ParsePositiveDecimalToChinese(decimal number)
            {
                bool lastZero = false;
                return ParsePositiveDecimalToChinese(number, out lastZero);
            }

            public static string ParseDoubleToChinese(double number, out bool lastZero)
            {
                if (number >= 0)
                    return ParsePositiveDoubleToChinese(number, out lastZero);
                return negetive + ParsePositiveDoubleToChinese(-number, out lastZero);
            }

            public static string ParseDoubleToChinese(double number)
            {
                bool lastZero = false;
                return ParseDoubleToChinese(number, out lastZero);
            }

            public static string ParseDecimalToChinese(decimal number, out bool lastZero)
            {
                if (number >= 0)
                    return ParsePositiveDecimalToChinese(number, out lastZero);
                return negetive + ParsePositiveDecimalToChinese(-number, out lastZero);
            }

            public static string ParseDecimalToChinese(decimal number)
            {
                bool lastZero = false;
                return ParseDecimalToChinese(number, out lastZero);
            }
        }
        #endregion
    }
}

