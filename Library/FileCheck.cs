using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library
{
    public static class FileUtility
    {
        public enum FileExtension { TXT = 1 }

        private static bool Validate_File_Extension(FileExtension fileExtension, string _fileExtension)
        {
            if (_fileExtension[0] == '.')
            {
                _fileExtension = _fileExtension.Substring(1);
            }
            string file_ext = Enum.GetName(typeof(FileExtension), fileExtension).ToString().ToLower();
            string file_ext_2 = _fileExtension.ToLower();
            if ( file_ext == file_ext_2)
            {
                return true;
            }
            return false;
        }
        public static string GetFile(string directory, int bufferMinutes, FileExtension extension)
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

            foreach (var child in children)
            {
                timediff = (now - child.CreationTime).TotalMinutes;

                if ((timediff >= bufferMinutes) && Validate_File_Extension(extension, child.Extension))
                {
                    return child.FullName.ToString();
                }
            }

            return "";
        }
        
        static public bool IsValidPath(string a_path)
        {
            if (a_path.Trim() == string.Empty)
            {
                return false;
            }

            string pathname;
            string filename;
            try
            {
                pathname = Path.GetPathRoot(a_path);
                filename = Path.GetFileName(a_path);
            }
            catch (ArgumentException)
            {
                return false;
            }
            if (filename.Trim() == string.Empty)
            {
                return false;
            }
            if (pathname.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                return false;
            }
            if (filename.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                return false;
            }

            return true;
        }
    }
}
