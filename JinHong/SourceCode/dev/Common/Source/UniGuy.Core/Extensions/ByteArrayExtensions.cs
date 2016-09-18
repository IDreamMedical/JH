using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace UniGuy.Core.Extensions
{
    /// <summary>
    /// 	Extension methods for byte-Arrays
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// 	Find the first occurence of an byte[] in another byte[]
        /// </summary>
        /// <param name = "buf1">the byte[] to search in</param>
        /// <param name = "buf2">the byte[] to find</param>
        /// <returns>the first position of the found byte[] or -1 if not found</returns>
        /// <remarks>
        /// 	Contributed by blaumeister, http://www.codeplex.com/site/users/view/blaumeiser
        /// </remarks>
        public static int FindArrayInArray(this byte[] buf1, byte[] buf2)
        {
            int i, j;
            for (j = 0; j < buf1.Length - buf2.Length; j++)
            {
                for (i = 0; i < buf2.Length; i++)
                {
                    if (buf1[j + i] != buf2[i])
                        break;
                }
                if (i == buf2.Length)
                    return j;
            }
            return -1;
        }

        /// <summary>
        /// 	Find the first occurence of an byte[] in another byte[]
        /// </summary>
        /// <param name = "buf1">the byte[] to search in</param>
        /// <param name = "buf2">the byte[] to find</param>
        /// <returns>the first position of the found byte[] or -1 if not found</returns>
        /// <remarks>
        /// 	Contributed by blaumeister, http://www.codeplex.com/site/users/view/blaumeiser
        /// </remarks>
        public static int FindArrayInArray2(this byte[] buf1, byte[] buf2)
        {
            if (buf2 == null)
                throw new ArgumentNullException("buf2");

            if (buf1 == null)
                throw new ArgumentNullException("buf1");

            if (buf2.Length == 0)
                return 0;		// by definition empty sets match immediately

            int j = -1;
            int end = buf1.Length - buf2.Length;
            while ((j = Array.IndexOf(buf1, buf2[0], j + 1)) <= end && j != -1)
            {
                int i = 1;
                while (buf1[j + i] == buf2[i])
                {
                    if (++i == buf2.Length)
                        return j;
                }
            }
            return -1;
        }

        #region Edge.Extensions
        //  http://edgeextensions.codeplex.com/SourceControl/list/changesets
        /// <summary>
        /// Deserializes a binary array.
        /// </summary>
        /// <param name="bytes">Array to deserialize.</param>
        /// <returns>Deserialized object.</returns>
        public static object BinaryDeserialize(this byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;

            return bf1.Deserialize(ms);
        }

        /// <summary>
        /// Converts a byte array to a string.
        /// </summary>
        /// <param name="bytes">Byte array to convert.</param>
        /// <returns>Resulting string.</returns>
        public static string ByteArrayToString(this byte[] bytes)
        {
            ASCIIEncoding enc = new ASCIIEncoding();
            return enc.GetString(bytes);
        }

        /// <summary>
        /// Compresses a byte array using gzip compression.
        /// </summary>
        /// <param name="bytes">Byte array to compress.</param>
        /// <returns>A compressed byte array.</returns>
        public static byte[] Compress(this byte[] bytes)
        {
            MemoryStream ms = new MemoryStream();
            GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
            zip.Write(bytes, 0, bytes.Length);
            zip.Close();
            ms.Position = 0;

            MemoryStream outStream = new MemoryStream();

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            byte[] gzipBuffer = new byte[compressed.Length + 4];
            Buffer.BlockCopy(compressed, 0, gzipBuffer, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(bytes.Length), 0, gzipBuffer, 0, 4);

            return gzipBuffer;
        }

        /// <summary>
        /// Decompresses a byte array using gzip compression.
        /// </summary>
        /// <param name="bytes">Byte array to decompress.</param>
        /// <returns>A decompressed byte array.</returns>
        public static byte[] Decompress(this byte[] bytes)
        {
            MemoryStream ms = new MemoryStream();
            int msgLength = BitConverter.ToInt32(bytes, 0);
            ms.Write(bytes, 4, bytes.Length - 4);

            byte[] buffer = new byte[msgLength];

            ms.Position = 0;
            GZipStream zip = new GZipStream(ms, CompressionMode.Decompress);
            zip.Read(buffer, 0, buffer.Length);

            return buffer;
        }

        /// <summary>
        /// Trims the length of a byte array.
        /// </summary>
        /// <param name="bytes">Byte array to trim.</param>
        /// <param name="length">Number of bytes to trim.</param>
        /// <returns>The resulting byte array.</returns>
        public static byte[] Trim(this byte[] bytes, int length)
        {
            byte[] result = new byte[bytes.Length - length];

            Array.Copy(bytes, result, bytes.Length - length);

            return result;
        }

        /// <summary>
        /// Writes a byte array to a file.
        /// </summary>
        /// <param name="bytes">Byte array to write to the file.</param>
        /// <param name="fileName">Full path and name of the file to create.</param>
        /// <param name="fileMode">File mode.</param>
        /// <returns>Success indicator.</returns>
        public static bool ToFile(this byte[] bytes, string fileName, FileMode fileMode)
        {
            bool returnValue = true;

            FileAccess fileAccess = FileAccess.ReadWrite;

            if (fileMode == FileMode.Append)
            {
                fileAccess = FileAccess.Write;
            }

            FileStream fs = new FileStream(fileName, fileMode, fileAccess);

            BinaryWriter bw = new BinaryWriter(fs);

            try
            {
                bw.Write(bytes);
            }
            catch (Exception)
            {
                returnValue = false;
            }
            finally
            {
                fs.Close();

                bw.Close();
            }

            return returnValue;
        }
        #endregion

        //  LastModified: 20120203
        #region WJ
        /// <summary>
        /// Converts a byte array to a string, using its byte order mark to convert it to the right encoding.    
        /// Original article: http://www.west-wind.com/WebLog/posts/197245.aspx ;  
        /// </summary>   
        /// <param name="buffer">An array of bytes to convert</param>   
        /// <returns>The byte as a string.</returns>   
        /// <remarks>
        /// An extension method for converting a byte array to a string (reading its BOM)
        /// Below is an extension method that adapts Rick Strahl's code (and the comments too) to stop you having to guess or read the byte order mark of a byte array or text file each time you convert it to a string.
        /// The commonest way of doing this is usually with 
        /// 1.byte[] buffer = File.ReadAllBytes(@"C:\file.txt");System.Text.Encoding.xxx.GetString(buffer)
        /// where xxx is Ascii, Unicode, or UTF8, or Default.
        /// but the snippet below allows you to simply do
        /// 1.byte[] buffer = File.ReadAllBytes(@"C:\file.txt");string content = buffer.GetString();
        /// </remarks>
        public static string GetString(this byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
                return "";
            // Ansi as default     
            Encoding encoding = Encoding.Default;
            /* EF BB BF        UTF-8            
             * FF FE UTF-16    little endian            
             * FE FF UTF-16    big endian             
             * FF FE 00 00     UTF-32, little endian             
             * 00 00 FE FF     UTF-32, big-endian          
             */
            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
                encoding = Encoding.UTF8;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
                encoding = Encoding.Unicode;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
                encoding = Encoding.BigEndianUnicode;
            // utf-16be        
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
                encoding = Encoding.UTF32;
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
                encoding = Encoding.UTF7;
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(buffer, 0, buffer.Length);
                stream.Seek(0, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// "0x03 0xA0 0xB2, 0x03 0xA0 0xB9, 0x03 0xA0 0xC0-0xB0 0xC2", etc
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IEnumerable<byte[]> GetBytesRange(string str)
        {
            string[] temp = str.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = temp[i].Trim();
                int index = temp[i].IndexOf('-');
                if (index >= 0)
                {
                    System.Diagnostics.Debug.Assert(index > 0 && index < temp[i].Length - 1);
                    byte[] btsFrom = GetBytes(temp[i].Substring(0, index).Trim());
                    byte[] btsTo = GetBytes(temp[i].Substring(index + 1).Trim());
                    if (btsTo.Length < btsFrom.Length)
                    {
                        byte[] temp2 = btsTo;
                        btsTo = new byte[btsFrom.Length];
                        Array.Copy(temp2, 0, btsTo, btsTo.Length - temp2.Length, temp2.Length);
                        Array.Copy(btsFrom, btsTo, btsFrom.Length - temp2.Length);

                        while (CompareBytes(btsFrom, btsTo) < 0)
                        {
                            yield return btsFrom;
                            Increase(btsFrom);
                        }
                    }
                }
                else
                {
                    yield return GetBytes(temp[i]);
                }
            }
        }

        /// <summary>
        /// 字符串转换为字节
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte GetByte(string str)
        {
            byte bt;
            if (byte.TryParse(str, out bt))
                return bt;
            //  TODO... 其它byte.TryParse不支持的通用格式可继续新增
            if (str.StartsWith("0x"))
                if (byte.TryParse(str.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out bt))
                    return bt;

            throw new FormatException("str not recgnizable.");
        }

        public static byte[] CreateBytes(int count, byte bti)
        {
            byte[] bts = new byte[count];
            for (int i = 0; i < count; i++)
                bts[i] = bti;
            return bts;
        }

        /// <summary>
        /// 字符串转换为字节数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string str)
        {
            string[] temp = str.Split(' ', '|');
            byte[] bts = new byte[temp.Length];
            for (int i = 0; i < temp.Length; i++)
                bts[i] = GetByte(temp[i].Trim());
            return bts;
        }

        public static string ToString(this byte[] @this, string format)
        {
            string[] temp = new string[@this.Length];
            for (int i = 0; i < @this.Length; i++)
                temp[i] = @this[i].ToString(format);
            return string.Join(" ", temp);
        }

        public static string ToString(this byte[] @this)
        {
            return string.Join(" ", @this);
        }

        /// <summary>
        /// 判断每个字节是否都相等
        /// </summary>
        /// <param name="this"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool IsEquals(this byte[] @this, byte[] other)
        {
            System.Diagnostics.Debug.Assert(@this.Length == other.Length);
            for (int i = 0; i < @this.Length; i++)
                if (@this[i] != other[i])
                    return false;
            return true;
        }

        /// <summary>
        /// 比较字节数组
        /// </summary>
        /// <param name="this"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static int CompareBytes(this byte[] @this, byte[] other)
        {
            System.Diagnostics.Debug.Assert(@this.Length == other.Length);
            for (int i = 0; i < @this.Length; i++)
                if (@this[i] != other[i])
                    return @this[i].CompareTo(other[i]);
            return 0;
        }

        /// <summary>
        /// 从最后开始++
        /// </summary>
        /// <param name="this"></param>
        public static byte[] Increase(this byte[] @this)
        {
            byte[] btsC = new byte[@this.Length];
            Array.Copy(@this, btsC, @this.Length);
            for (int i = btsC.Length - 1; i >= 0; i--)
            {
                if (btsC[i] == 255)  //  255 Byte.MaxValue
                {
                    btsC[i] = 0;
                    continue;
                }
                btsC[i]++;
                return btsC;
            }

            throw new InvalidOperationException("byte[] already in maximun state.");
        }

        /// <summary>
        /// 从最后开始--
        /// </summary>
        /// <param name="this"></param>
        public static byte[] Decrease(this byte[] @this)
        {
            byte[] btsC = new byte[@this.Length];
            Array.Copy(@this, btsC, @this.Length);
            for (int i = btsC.Length - 1; i >= 0; i--)
            {
                if (btsC[i] == 0)  //  0   Byte.MinValue
                {
                    btsC[i] = 0;
                    continue;
                }
                btsC[i]--;
                return btsC;
            }

            throw new InvalidOperationException("byte[] already in minimum state.");
        }

        public static byte[] Add(this byte[] @this, int c)
        {
            byte[] btsC = new byte[@this.Length];
            Array.Copy(@this, btsC, @this.Length);

            byte a;
            int i = btsC.Length;
            bool carry = false;
            while (c != 0 && i-- > 0)
            {
                a = (byte)(c % 256);
                c = c / 256;
                carry = btsC[i] + a >= 256;
                btsC[i] += a;
                if (carry)
                {
                    carry = false;
                    if (i == 0)
                        return CreateBytes(@this.Length, (byte)0xFF);   //  返回最大值
                    c++;
                }
            }

            return btsC;
        }

        #region bit operation
        //  remarks
        //  除移位操作外其他字节数组原位操作，如需保留原来值，请用byte[].Clone方法先
        //  这些方法可用作简单字节加密
        //  Not tested

        public static void Set(this byte[] @this, byte bt)
        {
            for (int i = 0; i < @this.Length; i++)
                @this[i] &= bt;
        }
        //  如果不移位不生成新byte[]实例
        public static byte[] Shift(this byte[] @this, int count)
        {
            if (count == 0)
                return @this;
            byte[] bts = new byte[@this.Length];
            if (count > 0)
            {
                int a = count / 8;  //  正向移动字节数
                int b = count % 8;  //  正向移动位数
                Array.Copy(@this, 0, bts, a, @this.Length - a);
                for (int i = bts.Length - 1; i >= a; i--)
                {
                    bts[i] >>= b;
                    if (i > 0)
                        bts[i] |= (byte)(bts[i - 1] << (8 - b));
                }
            }
            else
            {
                count = -count;
                int a = count / 8;  //  负向移动字节数
                int b = count % 8;  //  负向移动位数
                Array.Copy(@this, @this.Length - a, bts, 0, @this.Length - a);
                for (int i = 0; i <= a; i++)
                {
                    bts[i] <<= b;
                    if (i < @this.Length - 1)
                        bts[i] |= (byte)(bts[i + 1] >> (8 - b));
                }
            }
            return bts;
        }
        //  如果不移位不生成新byte[]实例
        public static byte[] Roll(this byte[] @this, int count)
        {
            count = count % (8 * @this.Length);
            if (count == 0)
                return @this;
            if (count < 0)
                count += (8 * @this.Length);

            byte[] bts = new byte[@this.Length];
            int a = count / 8;  //  正向移动字节数
            int b = count % 8;  //  正向移动位数
            Array.Copy(@this, 0, bts, a, @this.Length - a);
            Array.Copy(@this, a, bts, 0, a);
            bts[0] >>= b;
            bts[0] |= (byte)(bts[bts.Length - 1] >> (8 - b));
            for (int i = 1; i < @this.Length; i++)
            {
                bts[i] >>= b;
                if (i > 0)
                    bts[i] |= (byte)(bts[i - 1] >> (8 - b));
            }
            return bts;
        }
        public static void And(this byte[] @this, byte bt)
        {
            for (int i = 0; i < @this.Length; i++)
                @this[i] &= bt;
        }
        public static void Or(this byte[] @this, byte bt)
        {
            for (int i = 0; i < @this.Length; i++)
                @this[i] |= bt;
        }
        public static void Not(this byte[] @this)
        {
            for (int i = 0; i < @this.Length; i++)
                @this[i] = (byte)~@this[i]; //  shit: ~byte->int
        }
        public static void Xor(this byte[] @this, byte bt)
        {
            for (int i = 0; i < @this.Length; i++)
                @this[i] ^= bt;
        }
        #endregion

        #endregion
    }
}