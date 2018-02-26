using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static partial class Utility
    {
        public static class FileUtility
        {
            public static string GetFile(string directory, int bufferMinutes = 5)
            {
                DateTime now = DateTime.Now;
                double timediff;

                if (!Directory.Exists(directory))
                {
                    return "";
                }

                DirectoryInfo parent = new DirectoryInfo(directory);
                FileInfo[] children = parent.GetFiles();
                if (children.Length == 0)
                {
                    return "";
                }

                FileInfo oldest = null;
                foreach (var child in children)
                {
                    timediff = (now - child.CreationTime).TotalMinutes;

                    if (timediff > bufferMinutes)
                    {
                        if (oldest == null)
                        {
                            oldest = child;
                        }
                        else if (oldest != null && child.CreationTime < oldest.CreationTime)
                        {
                            oldest = child;
                        }
                        break;
                    }
                }

                string return_string = "";

                try
                {
                    return_string = oldest.FullName.ToString();
                }
                catch (Exception ex)
                {
                    return_string = "";
                }
                
                return return_string;
            }
        }
    }
}

        /*
        // file_extension should contain dot in-front like .pdf, .txt
        public static string GetLatestFile(string directory, string file_extension)
            {
                string rtr = "";

                if (!Directory.Exists(directory))
                {
                    return "";
                }

                DirectoryInfo parent = new DirectoryInfo(directory);
                FileInfo[] children = parent.GetFiles();
                if (children.Length == 0)
                {
                    return "";
                }

                FileInfo newest = null;
                foreach (var child in children)
                {
                    if (child.Extension.Equals(file_extension))
                    {
                        if (newest == null)
                        {
                            newest = child;
                        }
                        else if (newest != null && child.CreationTime > newest.CreationTime)
                        {
                            newest = child;
                        }
                    }
                }
                try
                {
                    rtr = newest.FullName.ToString();
                }
                catch (Exception ex)
                {
                    rtr = "";
                }

                return rtr;
            }
            
            public static string GetOldestFile(string directory, int bufferMinutes = 15)
            {
                DateTime now = DateTime.Now;
                double timediff;

                if (!Directory.Exists(directory))
                {
                    throw new ArgumentException();
                }

                DirectoryInfo parent = new DirectoryInfo(directory);
                FileInfo[] children = parent.GetFiles();
                if (children.Length == 0)
                {
                    return "";
                }

                FileInfo oldest = null;
                foreach (var child in children)
                {
                    timediff = (now - child.CreationTime).TotalMinutes;

                    if (timediff > bufferMinutes)
                    {
                        if (oldest == null)
                        {
                            oldest = child;
                        }
                        else if (oldest != null && child.CreationTime < oldest.CreationTime)
                        {
                            oldest = child;
                        }
                    }

                }

                string return_string = "";

                try
                {
                    return_string = oldest.FullName.ToString();
                }
                catch (Exception ex)
                {
                    return_string = "";
                }

                
                return return_string;
            }

            public static string GetNewestFile(string directory, int bufferMinutes = 15)
            {
                DateTime now = DateTime.Now;
                double timediff;

                if (!Directory.Exists(directory))
                    throw new ArgumentException();

                DirectoryInfo parent = new DirectoryInfo(directory);
                FileInfo[] children = parent.GetFiles();
                if (children.Length == 0)
                {
                    return "";
                }

                FileInfo newest = null;
                foreach (var child in children)
                {
                    timediff = (now - child.CreationTime).TotalMinutes;

                    if (timediff > bufferMinutes)
                    {
                        if (newest == null)
                        {
                            newest = child;
                        }
                        else if (newest != null && child.CreationTime > newest.CreationTime)
                        {
                            newest = child;
                        }
                    }

                }
                string return_string;

                try
                {
                    return_string = newest.FullName.ToString();
                }
                catch (Exception ex)
                {
                    return_string = "";
                }

                
                return return_string;
            }

            public static string GetFilesInfo(string directory)
            {
                string return_string = "";

                if (!Directory.Exists(directory))
                    return return_string;

                DirectoryInfo parent = new DirectoryInfo(directory);
                FileInfo[] children = parent.GetFiles();
                if (children.Length == 0)
                    return "";

                foreach (var child in children)
                {
                    return_string += child.FullName + "|" + child.CreationTime + "\n";
                }

                return return_string;
            }*/