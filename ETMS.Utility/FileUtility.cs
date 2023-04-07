using System;

using System.Web;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.Diagnostics;
using System.Configuration;

namespace ETMS.Utility
{
    public class FileUtility
    {
        /// <summary>
        /// 获取虚拟路径文件的扩展名
        /// </summary>
        /// <param name="file">虚拟路径的文件</param>
        /// <returns></returns>
        public static string GetExtension(string file)
        {
            string extension = VirtualPathUtility.GetExtension(file);
            if (!string.IsNullOrEmpty(extension) && (extension.IndexOf(".", StringComparison.InvariantCultureIgnoreCase) == 0))
            {
                extension = extension.Substring(1, extension.Length - 1);
            }

            return extension;
        }

        /// <summary>
        /// 获取文件扩展名(含".")
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns></returns>
        public static string GetFileExtension(string file)
        {
            string extension = string.Empty;
            int pos;
            if (!string.IsNullOrEmpty(file))
            {
                pos = file.LastIndexOf('.');
                if (pos > 0)
                {
                    extension = file.Substring(pos);
                }
            }

            return extension;
        }

        #region 压缩、解压方法（临时）
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="sourcePath">要压缩文件路径</param>
        /// <param name="compressPath">压缩后文件放置路径</param>
        /// <param name="compressName">压缩后文件的名称</param>
        /// <param name="type">压缩工具类型</param>
        /// <returns></returns>
        public static bool Compress(string sourcePath, string compressPath, string compressName, CompressType type, CompressModel model)
        {
            if (type == CompressType.RAR)
                return RAR(sourcePath, compressPath, compressName);
            else if (type == CompressType.Zip)
                return Zip(string.Format("{0}/{1}", compressPath, compressName),Path.Combine(sourcePath,"aaa.zip"),  string.Empty, model);
            return false;
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="sourcePath">要压缩文件路径</param>
        /// <param name="sourcePath">要压缩文件名</param>
        /// <param name="compressPath">压缩后文件放置路径</param>
        /// <param name="compressName">压缩后文件的名称</param>
        /// <param name="type">压缩工具类型</param>
        /// <returns></returns>
        public static bool Compress(string sourcePath, string sourceFileName, string compressPath, string compressName, CompressType type, CompressModel model)
        {
            if (type == CompressType.RAR)
                return RAR(sourcePath, compressPath, compressName);
            else if (type == CompressType.Zip)
                return Zip(Path.Combine(sourcePath, sourceFileName), Path.Combine(compressPath, compressName), string.Empty, model);
            return false;
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="sourcePath">要解压文件路径</param>
        /// <param name="compressPath">解压后文件放置路径</param>
        /// <param name="compressName">解压后文件的名称</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool UnCompress(string sourcePath, string compressPath, string compressName, CompressType type)
        {
            try
            {
                if (type == CompressType.RAR)
                    return UnRAR(compressPath, sourcePath, compressName);
                else if (type == CompressType.Zip)
                    return UnZip(sourcePath, string.Format("{0}/{1}", compressPath, compressName), string.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        /// <summary>
        /// 递归压缩文件夹方法
        /// </summary>
        /// <param name="FolderToZip"></param>
        /// <param name="s"></param>
        /// <param name="ParentFolderName"></param>
        private static bool ZipFileDictory(string FolderToZip, ZipOutputStream s, string ParentFolderName)
        {
            bool res = true;
            string[] folders, filenames;
            ZipEntry entry = null;
            FileStream fs = null;
            Crc32 crc = new Crc32();

            try
            {

                //创建当前文件夹
                entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/"));  //加上 “/” 才会当成是文件夹创建
                s.PutNextEntry(entry);
                s.Flush();


                //先压缩文件，再递归压缩文件夹 
                filenames = Directory.GetFiles(FolderToZip);
                foreach (string file in filenames)
                {
                    //打开压缩文件
                    fs = File.OpenRead(file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/" + Path.GetFileName(file)));

                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();

                    crc.Reset();
                    crc.Update(buffer);

                    entry.Crc = crc.Value;

                    s.PutNextEntry(entry);

                    s.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
                if (entry != null)
                {
                    entry = null;
                }
                GC.Collect();
                GC.Collect(1);
            }


            folders = Directory.GetDirectories(FolderToZip);
            foreach (string folder in folders)
            {
                if (!ZipFileDictory(folder, s, Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip))))
                {
                    return false;
                }
            }

            return res;
        }

        /// <summary>
        /// 压缩目录
        /// </summary>
        /// <param name="FolderToZip">待压缩的文件夹，全路径格式</param>
        /// <param name="ZipedFile">压缩后的文件名，全路径格式</param>
        /// <returns></returns>
        public static bool ZipFileDictory(string FolderToZip, string ZipedFile, String Password)
        {
            bool res;
            if (!Directory.Exists(FolderToZip))
            {
                return false;
            }

            ZipOutputStream s = new ZipOutputStream(File.Create(ZipedFile));
            s.SetLevel(6);
            s.Password = Password;

            res = ZipFileDictory(FolderToZip, s, "");

            s.Finish();
            s.Close();

            return res;
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="FileToZip">要进行压缩的文件名</param>
        /// <param name="ZipedFile">压缩后生成的压缩文件名</param>
        /// <returns></returns>
        private static bool ZipFile(string FileToZip, string ZipedFile, String Password)
        {
            //如果文件没有找到，则报错
            if (!File.Exists(FileToZip))
            {
                throw new System.IO.FileNotFoundException("指定要压缩的文件: " + FileToZip + " 不存在!");
            }
            //FileStream fs = null;
            FileStream ZipFile = null;
            ZipOutputStream ZipStream = null;
            ZipEntry ZipEntry = null;

            bool res = true;
            try
            {
                ZipFile = File.OpenRead(FileToZip);
                byte[] buffer = new byte[ZipFile.Length];
                ZipFile.Read(buffer, 0, buffer.Length);
                ZipFile.Close();

                ZipFile = File.Create(ZipedFile);
                ZipStream = new ZipOutputStream(ZipFile);
                ZipStream.Password = Password;
                ZipEntry = new ZipEntry(Path.GetFileName(FileToZip));
                ZipStream.PutNextEntry(ZipEntry);
                ZipStream.SetLevel(6);

                ZipStream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (ZipEntry != null)
                {
                    ZipEntry = null;
                }
                if (ZipStream != null)
                {
                    ZipStream.Finish();
                    ZipStream.Close();
                }
                if (ZipFile != null)
                {
                    ZipFile.Close();
                    ZipFile = null;
                }
                GC.Collect();
                GC.Collect(1);
            }

            return res;
        }

        /// <summary>
        /// 压缩文件 和 文件夹
        /// </summary>
        /// <param name="FileToZip">待压缩的文件或文件夹，全路径格式</param>
        /// <param name="ZipedFile">压缩后生成的压缩文件名，全路径格式</param>
        /// <returns></returns>
        private static bool Zip(String FileToZip, String ZipedFile, String Password)
        {
            if (Directory.Exists(FileToZip))
            {
                return ZipFileDictory(FileToZip, ZipedFile, Password);
            }
            else if (File.Exists(FileToZip))
            {
                return ZipFile(FileToZip, ZipedFile, Password);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 压缩文件 和 文件夹
        /// </summary>
        /// <param name="FileToZip">待压缩的文件或文件夹，全路径格式</param>
        /// <param name="ZipedFile">压缩后生成的压缩文件名，全路径格式</param>
        /// <returns></returns>
        private static bool Zip(String FileToZip, String ZipedFile, String Password, CompressModel model)
        {
            if (model == CompressModel.Directory && Directory.Exists(FileToZip))
            {
                return ZipFileDictory(FileToZip, ZipedFile, Password);
            }
            else if (File.Exists(FileToZip))
            {
                return ZipFile(FileToZip, ZipedFile, Password);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 解压功能(解压压缩文件到指定目录)
        /// </summary>
        /// <param name="FileToUpZip">待解压的文件</param>
        /// <param name="ZipedFolder">指定解压目标目录</param>
        private static bool UnZip(string FileToUpZip, string ZipedFolder, string Password)
        {
            if (!File.Exists(FileToUpZip))
            {
                return false;
            }

            if (!Directory.Exists(ZipedFolder))
            {
                Directory.CreateDirectory(ZipedFolder);
            }

            ZipInputStream s = null;
            ZipEntry theEntry = null;

            string fileName;
            FileStream streamWriter = null;
            try
            {
                s = new ZipInputStream(File.OpenRead(FileToUpZip));
                s.Password = Password;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.Name != String.Empty)
                    {
                        fileName = Path.Combine(ZipedFolder, theEntry.Name);
                        /**/
                        ///判断文件路径是否是文件夹
                        if (fileName.EndsWith("/") || fileName.EndsWith("\\"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }

                        streamWriter = File.Create(fileName);
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter = null;
                }
                if (theEntry != null)
                {
                    theEntry = null;
                }
                if (s != null)
                {
                    s.Close();
                    s = null;
                }
                GC.Collect();
                GC.Collect(1);
            }

            return true;
        }

        /// <summary>
        /// 利用 WinRAR 进行压缩
        /// </summary>
        /// <param name="path">将要被压缩的文件夹（绝对路径）</param>
        /// <param name="rarPath">压缩后的 .rar 的存放目录（绝对路径）</param>
        /// <param name="rarName">压缩文件的名称（包括后缀）</param>
        /// <returns>true 或 false。压缩成功返回 true，反之，false。</returns>
        private static bool RAR(string path, string rarPath, string rarName)
        {
            bool flag = false;
            string rarexe;       //WinRAR.exe 的完整路径
            //RegistryKey regkey;  //注册表键
            Object regvalue;     //键值
            string cmd;          //WinRAR 命令参数
            ProcessStartInfo startinfo;
            Process process;
            try
            {
                //regkey = Registry.ClassesRoot.OpenSubKey(@"Applications\WinRAR.exe\shell\open\command");
                //regvalue = regkey.GetValue("");  // 键值为 "d:\Program Files\WinRAR\WinRAR.exe" "%1"
                //rarexe = regvalue.ToString();
                //regkey.Close();
                //rarexe = rarexe.Substring(1, rarexe.Length - 7);  // d:\Program Files\WinRAR\WinRAR.exe

                Directory.CreateDirectory(path);
                //压缩命令，相当于在要压缩的文件夹(path)上点右键->WinRAR->添加到压缩文件->输入压缩文件名(rarName)
                cmd = string.Format("a {0} {1} -r",
                                    rarName,
                                    path);
                startinfo = new ProcessStartInfo();
                startinfo.FileName = ConfigurationManager.AppSettings["RarPath"];
                startinfo.Arguments = cmd;                          //设置命令参数
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;  //隐藏 WinRAR 窗口

                startinfo.WorkingDirectory = rarPath;
                process = new Process();
                process.StartInfo = startinfo;
                process.Start();
                process.WaitForExit(); //无限期等待进程 winrar.exe 退出
                if (process.HasExited)
                {
                    flag = true;
                }
                process.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return flag;
        }

        /// <summary>
        /// 利用 WinRAR 进行解压缩
        /// </summary>
        /// <param name="path">文件解压路径（绝对）</param>
        /// <param name="rarPath">将要解压缩的 .rar 文件的存放目录（绝对路径）</param>
        /// <param name="rarName">将要解压缩的 .rar 文件名（包括后缀）</param>
        /// <returns>true 或 false。解压缩成功返回 true，反之，false。</returns>
        private static bool UnRAR(string path, string rarPath, string rarName)
        {
            bool flag = false;
            string cmd;
            ProcessStartInfo startinfo;
            Process process;
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                //解压缩命令，相当于在要压缩文件(rarName)上点右键->WinRAR->解压到当前文件夹
                cmd = string.Format("x {0} {1} -y",
                                    rarName,
                                    path);
                startinfo = new ProcessStartInfo();
                startinfo.FileName = ConfigurationManager.AppSettings["RarPath"];
                startinfo.Arguments = cmd;
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;

                startinfo.WorkingDirectory = rarPath;
                process = new Process();
                process.StartInfo = startinfo;
                process.Start();
                process.WaitForExit();
                if (process.HasExited)
                {
                    flag = true;
                }
                process.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return flag;
        }

        #endregion
    }

    public enum CompressType
    {
        RAR,
        Zip
    }

    public enum CompressModel
    { 
        Directory,
        File
    }
}
