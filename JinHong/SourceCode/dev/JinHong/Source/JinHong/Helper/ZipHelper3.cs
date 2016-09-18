using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace JinHong.Helper
{
    public class ZipHelper3
    {
        public static void DecompressFile(string zipFile, string path)
        {
            using (ZipFile zip = ZipFile.Read(zipFile))
                foreach (ZipEntry e in zip)
                    e.Extract(path, true);  // true => overwrite existing files
        }
    }
}
