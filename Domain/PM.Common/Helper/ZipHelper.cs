using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Common.Helper
{
    public class ZipHelper
    {
        static ZipHelper()
        {
            //消除文件名中文乱码
            //ICSharpCode.SharpZipLib.Zip.ZipStrings.CodePage = Encoding.UTF8.CodePage;
            //旧版本
            ICSharpCode.SharpZipLib.Zip.ZipConstants.DefaultCodePage = Encoding.UTF8.CodePage;
        }

        #region 压缩文件
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="dest">目的文件,如: c:\test.zip</param>
        /// <param name="fileAbsPaths">文件来源列表,如: c:\test.txt,d:\test.db</param>
        public static void ZipFile(string dest, params string[] fileAbsPaths)
        {
            using (var fs = new FileStream(dest, FileMode.Create))
            {
                var zipOutputStream = new ZipOutputStream(fs);
                foreach (var i in fileAbsPaths)
                {
                    ZipSingleFile(zipOutputStream, i);
                };
                zipOutputStream.Finish();
                zipOutputStream.Close();
            }
        }

        private static void ZipSingleFile(ZipOutputStream zipOutputStream, string filePath, string folderName = "")
        {
            var fileInfo = new FileInfo(filePath);
            string fileName = Path.Combine(folderName, fileInfo.Name);
            ZipEntry zipEntry = new ZipEntry(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(fileName)));
            zipEntry.DateTime = DateTime.Now;
            zipEntry.Size = fileInfo.Length;
            zipOutputStream.PutNextEntry(zipEntry);
            using (var fs = new FileStream(fileInfo.FullName, FileMode.Open))
            {
                fs.CopyTo(zipOutputStream);
            }
        }

        private static void ZipSingleFolder(ZipOutputStream zipOutputStream, string folderPath, string folderName = "")
        {
            string[] directorys = Directory.GetDirectories(folderPath);
            foreach (var i in directorys)
            {
                var tmp = Path.Combine(folderName, new DirectoryInfo(i).Name);
                ZipSingleFolder(zipOutputStream, i, tmp);
            }
            string[] files = Directory.GetFiles(folderPath);
            foreach (var i in files)
            {
                ZipSingleFile(zipOutputStream, i, folderName);
            }
        }

        /// <summary>
        /// 递归压缩文件夹
        /// </summary>
        /// <param name="dest">目的文件名称,如: c:\test.zip</param>
        /// <param name="folderPaths">压缩的目录列表,如: c:\test,d:\jack</param>
        public static void ZipFolder(string dest, params string[] folderPaths)
        {
            using (var fs = new FileStream(dest, FileMode.Create))
            {
                var zipOutputStream = new ZipOutputStream(fs);
                foreach (var i in folderPaths)
                {
                    ZipSingleFolder(zipOutputStream, i, new DirectoryInfo(i).Name);
                };
                zipOutputStream.Finish();
                zipOutputStream.Close();
            }
        }
        #endregion

        #region 解压缩文件
        /// <summary>
        /// 解压缩文件到指定文件夹
        /// </summary>
        /// <param name="srcZipFile">源压缩文件</param>
        /// <param name="destDir">目标文件夹</param>
        /// <returns></returns>
        public static List<string> UnZipFile(string srcZipFile, string destDir)
        {
            List<string> files = new List<string>();
            //读取压缩文件（zip文件），准备解压缩
            ZipInputStream inputstream = new ZipInputStream(File.OpenRead(srcZipFile.Trim()));
            ZipEntry entry;
            string path = destDir;
            //解压出来的文件保存路径
            string rootDir = "";
            //根目录下的第一个子文件夹的名称
            while ((entry = inputstream.GetNextEntry()) != null)
            {
                rootDir = Path.GetDirectoryName(entry.Name);
                //得到根目录下的第一级子文件夹的名称
                if (rootDir.IndexOf("\\") >= 0)
                {
                    rootDir = rootDir.Substring(0, rootDir.IndexOf("\\") + 1);
                }
                string dir = Path.GetDirectoryName(entry.Name);
                //得到根目录下的第一级子文件夹下的子文件夹名称
                string fileName = Path.GetFileName(entry.Name);
                //根目录下的文件名称
                if (dir != "")
                {
                    //创建根目录下的子文件夹，不限制级别
                    if (!Directory.Exists(destDir + "\\" + dir))
                    {
                        path = destDir + "\\" + dir;
                        //在指定的路径创建文件夹
                        Directory.CreateDirectory(path);
                    }
                }
                else if (dir == "" && fileName != "")
                {
                    //根目录下的文件
                    path = destDir;
                    files.Add(fileName);
                }
                else if (dir != "" && fileName != "")
                {
                    //根目录下的第一级子文件夹下的文件
                    if (dir.IndexOf("\\") > 0)
                    {
                        //指定文件保存路径
                        path = destDir + "\\" + dir;
                    }
                }
                if (dir == rootDir)
                {
                    //判断是不是需要保存在根目录下的文件
                    path = destDir + "\\" + rootDir;
                }

                //以下为解压zip文件的基本步骤
                //基本思路：遍历压缩文件里的所有文件，创建一个相同的文件
                if (fileName != String.Empty)
                {
                    FileStream fs = File.Create(path + "\\" + fileName);
                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = inputstream.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            fs.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    fs.Close();
                }
            }
            inputstream.Close();
            //删除文件
            // File.Delete(srcZipFile);
            return files;
        }

        public static string UnZipFile(string srcZipFile)
        {
            //读取压缩文件（zip文件），准备解压缩
            ZipInputStream inputstream = new ZipInputStream(File.OpenRead(srcZipFile.Trim()));
            ZipEntry entry;
            string retFileName = "";
            //根目录下的第一个子文件夹的名称
            while ((entry = inputstream.GetNextEntry()) != null)
            {
                string dir = Path.GetDirectoryName(entry.Name);
                string fileName = Path.GetFileName(entry.Name);
                if (dir == "" && fileName != "")
                {
                    if (fileName.Contains(".db"))
                    {
                        retFileName = fileName;
                    }
                }
            }
            inputstream.Close();
            return retFileName;
        }
        #endregion

        #region 删除文件

        public static void DelectDir(string srcPath)
        {
            try
            {
                if (Directory.Exists(srcPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(srcPath);
                    FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)            //判断是否文件夹
                        {
                            DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                            subdir.Delete(true);          //删除子目录和文件
                        }
                        else
                        {
                            File.Delete(i.FullName);      //删除指定文件
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion
    }
}
