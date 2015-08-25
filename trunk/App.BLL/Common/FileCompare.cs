//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using NetCMS.Model;

namespace NetCMS.Content.Common
{
    public class FileCompare
    {
        private string FileType = @".gif|.jpg|.swf|";
        private List<FileComprInfo> fllist;
        private string RootDir;
        private int dirlen;
        private string Contrast = string.Empty;
        public FileCompare(string root, string contrast)
        {
            RootDir = root;
            if (!Directory.Exists(RootDir))
                throw new Exception("文件目录不存在!");
            fllist = new List<FileComprInfo>();
            dirlen = root.Length;
            Contrast = contrast;
            GetStList();
        }
        public void GetFileList()
        {
            GetDirInfo(RootDir);
        }
        private void GetDirInfo(string ParentDir)
        {
            DirectoryInfo[] ChildDirectory;                         //子目录集
            FileInfo[] NewFileInfo;                                 //当前所有文件
            DirectoryInfo FatherDirectory = new DirectoryInfo(ParentDir); //当前目录
            string dname = FatherDirectory.Name.ToLower();
            if (dname == "bin" || dname == Config.UIConfig.dirHtml.ToLower() || dname == Config.UIConfig.dirSite.ToLower())
                return;
            NewFileInfo = FatherDirectory.GetFiles();
            foreach (FileInfo DirFile in NewFileInfo)                    //获取此级目录下的所有文件
            {
                string exname = DirFile.Extension.ToLower() + "|";
                if (FileType.IndexOf(exname) >= 0)
                    continue;
                string filenm = DirFile.FullName.Substring(dirlen);
                long filesz = DirFile.Length;
                DateTime filetm = DirFile.LastWriteTime;
                bool flag = false;
                foreach (FileComprInfo f in fllist)
                {
                    if (f.FileName.ToLower() == filenm.ToLower())
                    {
                        f.FaFileSize = filesz;
                        f.FaModifyTime = filetm;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    FileComprInfo fl = new FileComprInfo(filenm);
                    fl.FaFileSize = filesz;
                    fl.FaModifyTime = filetm;
                    fllist.Add(fl);
                }
            }
            ChildDirectory = FatherDirectory.GetDirectories("*.*"); //得到子目录集
            foreach (DirectoryInfo dirInfo in ChildDirectory)       //获取此级目录下的一级目录
            {
                GetDirInfo(dirInfo.FullName);
            }
        }
        public List<FileComprInfo> FileList
        {
            get { return fllist; }
        }
        private void GetStList()
        {
            if (Contrast == null || Contrast == string.Empty)
                return;
            string pattern = "\\<file\\ name=\"(?<f>[^\"]+)\"\\ size=\"(?<s>\\d+)\"\\ modifytime=\"(?<t>[^\"]+)\"\\ */\\>(\r\n)*";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Match m = reg.Match(Contrast);
            while (m.Success)
            {
                try
                {
                    string filename = m.Groups["f"].Value.Trim();
                    long l = long.Parse(m.Groups["s"].Value);
                    DateTime t = DateTime.Parse(m.Groups["t"].Value);
                    FileComprInfo fl = new FileComprInfo(filename);
                    fl.StFileSize = l;
                    fl.StModifyTime = t;
                    fllist.Add(fl);
                }
                catch
                { }
                m = m.NextMatch();
            }
        }

    }
}
