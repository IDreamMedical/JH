//  http://edgeextensions.codeplex.com/SourceControl/list/changesets
// <copyright file="IOExtensions.cs" company="Edge Extensions Project">
// Copyright (c) 2009 All Rights Reserved
// </copyright>
// <author>Kevin Nessland</author>
// <email>kevinnessland@gmail.com</email>
// <date>2009-07-08</date>
// <summary>Contains IO-related extension methods.</summary>
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Microsoft.Win32;

namespace UniGuy.Core.Extensions
{
    #region //  Edge.Extensions
    /// <summary>
    /// Provides various file-related extensions.
    /// </summary>
    public static class IOExtensions
    {
        /// <summary>
        /// Contains a list of video and image extensions.
        /// </summary>
        private static ArrayList mediaExtensions;

        /// <summary>
        /// Appends text to a file.
        /// </summary>
        /// <param name="fileInfo">The file to append.</param>
        /// <param name="text">Text to append.</param>
        public static void AppendTextFile(this FileInfo fileInfo, string text)
        {
            StreamWriter sw = File.AppendText(fileInfo.FullName);

            sw.WriteLine(text);

            sw.Close();
        }

        /// <summary>
        /// Creates a new text file from a string.
        /// </summary>
        /// <param name="text">Text to write to the file.</param>
        /// <param name="filePath">Full path to the file.</param>
        public static void CreateTextFile(this string text, string filePath)
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.Write(text);
                sw.Close();
            }
        }

        /// <summary>
        /// Determines if file is video or image file based upon its extension.
        /// </summary>
        /// <param name="fileInfo">File to inspect</param>
        /// <returns>Results of the inspection.</returns>
        public static bool IsMediaFile(this FileInfo fileInfo)
        {
            if (mediaExtensions == null)
            {
                mediaExtensions = ArrayList.Synchronized(new ArrayList());

                mediaExtensions.Add(".bmp");
                mediaExtensions.Add(".gif");
                mediaExtensions.Add(".jpe");
                mediaExtensions.Add(".jpeg");
                mediaExtensions.Add(".jpg");
                mediaExtensions.Add(".png");
                mediaExtensions.Add(".tif");
                mediaExtensions.Add(".asf");
                mediaExtensions.Add(".asx");
                mediaExtensions.Add(".avi");
                mediaExtensions.Add(".mov");
                mediaExtensions.Add(".mp4");
                mediaExtensions.Add(".mpeg");
                mediaExtensions.Add(".mpg");
                mediaExtensions.Add(".wmv");
            }

            if (mediaExtensions.Contains(fileInfo.Extension))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Formats the integer length of a file to a more friendly string, e.g. "1GB", "50 bytes", etc.,
        /// </summary>
        /// <param name="fileSize">The file size for which to determine the format.</param>
        /// <returns>The resulting string.</returns>
        public static string FileSizeFormatted(this int fileSize)
        {
            return Convert.ToInt64(fileSize).FileSizeFormatted();
        }

        /// <summary>
        /// Formats the long length of a file to a more friendly string, e.g. "1GB", "50 bytes", etc.,
        /// </summary>
        /// <param name="fileSize">The file size for which to determine the format.</param>
        /// <returns>The resulting string.</returns>
        public static string FileSizeFormatted(this long fileSize)
        {
            if (fileSize < 0)
            {
                throw new ArgumentOutOfRangeException("fileSize");
            }

            if (fileSize >= 1073741824)
            {
                return string.Format("{0:########0.00} GB", ((double)fileSize) / 1073741824);
            }
            else if (fileSize >= 1024 * 1024)
            {
                return string.Format("{0:####0.00} MB", ((double)fileSize) / 1048576);
            }
            else if (fileSize >= 1024)
            {
                return string.Format("{0:####0} KB", ((double)fileSize) / 1024);
            }
            else
            {
                return string.Format("{0} bytes", fileSize);
            }
        }

        /// <summary>
        /// Formats the length value of the specified FileInfo object to a more friendly string, e.g. "1GB", "50 bytes", etc.,
        /// </summary>
        /// <param name="fileInfo">The file for which to determine the format.</param>
        /// <returns>The resulting string.</returns>
        public static string FileSizeFormatted(this FileInfo fileInfo)
        {
            long fileSize = fileInfo.Length;

            return fileSize.FileSizeFormatted();
        }

        /// <summary>
        /// Gets a list of all the lines in a text file.
        /// </summary>
        /// <param name="fileInfo">File to inspect.</param>
        /// <returns>List of lines.</returns>
        public static List<string> Lines(this FileInfo fileInfo)
        {
            StreamReader file = new StreamReader(fileInfo.FullName);

            string line;

            List<string> lines = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }

            return lines;
        }

        /// <summary>
        /// Retrieves the mime type of a file.
        /// </summary>
        /// <param name="fileInfo">The file for which to retrieve the mime type.</param>
        /// <returns>Mime type of the file.</returns>
        public static string MimeType(this FileInfo fileInfo)
        {
            string mime = "application/octetstream";

            string ext = Path.GetExtension(fileInfo.Name).ToLower();

            RegistryKey rk = Registry.ClassesRoot.OpenSubKey(ext);

            if (rk != null && rk.GetValue("Content Type") != null)
            {
                mime = rk.GetValue("Content Type").ToString();
            }

            return mime;
        }

        /// <summary>
        /// Displays a folder in Windows Explorer.
        /// </summary>
        /// <param name="directoryInfo">Folder to display.</param>
        /// <example>
        ///     <code>
        ///         DirectoryInfo di = new DirectoryInfo(textBoxFolder.Text);
        ///         di.ShowInWindowsExplorer();
        ///     </code>
        /// </example>
        public static void ShowInWindowsExplorer(this DirectoryInfo directoryInfo)
        {
            string argument = directoryInfo.FullName;

            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        /// <summary>
        /// Displays a file in Windows Explorer.
        /// </summary>
        /// <param name="fileInfo">File to display and select.</param>
        /// <example>
        ///     <code>
        ///         FileInfo fi = new FileInfo(textBoxFolder.Text);
        ///         fi.ShowInWindowsExplorer();
        ///     </code>
        /// </example>
        public static void ShowInWindowsExplorer(this FileInfo fileInfo)
        {
            string argument = @"/select, " + fileInfo.FullName;

            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        /// <summary>
        /// Gets a byte array from a file stream.
        /// </summary>
        /// <param name="stream">Stream to use when converting to byte array.</param>
        /// <returns>Byte array containing the file.</returns>
        /// <example>
        ///     <code language="c#">
        ///         FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
        ///         byte[] bytes = fs.ToByteArray();
        ///     </code>
        /// </example>
        public static byte[] ToByteArray(this FileStream stream)
        {
            byte[] bytes = new byte[stream.Length];

            stream.Read(bytes, 0, System.Convert.ToInt32(stream.Length));

            stream.Close();

            return bytes;
        }

        /// <summary>
        /// Gets a byte array from a file.
        /// </summary>
        /// <param name="fileInfo">Contains information about the file.</param>
        /// <returns>Byte array containing the file.</returns>
        public static byte[] ToByteArray(this FileInfo fileInfo)
        {
            return File.ReadAllBytes(fileInfo.FullName);
        }

    }
    #endregion
}