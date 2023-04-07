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
        /// ��ȡ����·���ļ�����չ��
        /// </summary>
        /// <param name="file">����·�����ļ�</param>
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
        /// ��ȡ�ļ���չ��(��".")
        /// </summary>
        /// <param name="file">�ļ���</param>
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

        #region ѹ������ѹ��������ʱ��
        /// <summary>
        /// ѹ��
        /// </summary>
        /// <param name="sourcePath">Ҫѹ���ļ�·��</param>
        /// <param name="compressPath">ѹ�����ļ�����·��</param>
        /// <param name="compressName">ѹ�����ļ�������</param>
        /// <param name="type">ѹ����������</param>
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
        /// ѹ��
        /// </summary>
        /// <param name="sourcePath">Ҫѹ���ļ�·��</param>
        /// <param name="sourcePath">Ҫѹ���ļ���</param>
        /// <param name="compressPath">ѹ�����ļ�����·��</param>
        /// <param name="compressName">ѹ�����ļ�������</param>
        /// <param name="type">ѹ����������</param>
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
        /// ��ѹ
        /// </summary>
        /// <param name="sourcePath">Ҫ��ѹ�ļ�·��</param>
        /// <param name="compressPath">��ѹ���ļ�����·��</param>
        /// <param name="compressName">��ѹ���ļ�������</param>
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
        /// �ݹ�ѹ���ļ��з���
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

                //������ǰ�ļ���
                entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/"));  //���� ��/�� �Żᵱ�����ļ��д���
                s.PutNextEntry(entry);
                s.Flush();


                //��ѹ���ļ����ٵݹ�ѹ���ļ��� 
                filenames = Directory.GetFiles(FolderToZip);
                foreach (string file in filenames)
                {
                    //��ѹ���ļ�
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
        /// ѹ��Ŀ¼
        /// </summary>
        /// <param name="FolderToZip">��ѹ�����ļ��У�ȫ·����ʽ</param>
        /// <param name="ZipedFile">ѹ������ļ�����ȫ·����ʽ</param>
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
        /// ѹ���ļ�
        /// </summary>
        /// <param name="FileToZip">Ҫ����ѹ�����ļ���</param>
        /// <param name="ZipedFile">ѹ�������ɵ�ѹ���ļ���</param>
        /// <returns></returns>
        private static bool ZipFile(string FileToZip, string ZipedFile, String Password)
        {
            //����ļ�û���ҵ����򱨴�
            if (!File.Exists(FileToZip))
            {
                throw new System.IO.FileNotFoundException("ָ��Ҫѹ�����ļ�: " + FileToZip + " ������!");
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
        /// ѹ���ļ� �� �ļ���
        /// </summary>
        /// <param name="FileToZip">��ѹ�����ļ����ļ��У�ȫ·����ʽ</param>
        /// <param name="ZipedFile">ѹ�������ɵ�ѹ���ļ�����ȫ·����ʽ</param>
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
        /// ѹ���ļ� �� �ļ���
        /// </summary>
        /// <param name="FileToZip">��ѹ�����ļ����ļ��У�ȫ·����ʽ</param>
        /// <param name="ZipedFile">ѹ�������ɵ�ѹ���ļ�����ȫ·����ʽ</param>
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
        /// ��ѹ����(��ѹѹ���ļ���ָ��Ŀ¼)
        /// </summary>
        /// <param name="FileToUpZip">����ѹ���ļ�</param>
        /// <param name="ZipedFolder">ָ����ѹĿ��Ŀ¼</param>
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
                        ///�ж��ļ�·���Ƿ����ļ���
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
        /// ���� WinRAR ����ѹ��
        /// </summary>
        /// <param name="path">��Ҫ��ѹ�����ļ��У�����·����</param>
        /// <param name="rarPath">ѹ����� .rar �Ĵ��Ŀ¼������·����</param>
        /// <param name="rarName">ѹ���ļ������ƣ�������׺��</param>
        /// <returns>true �� false��ѹ���ɹ����� true����֮��false��</returns>
        private static bool RAR(string path, string rarPath, string rarName)
        {
            bool flag = false;
            string rarexe;       //WinRAR.exe ������·��
            //RegistryKey regkey;  //ע����
            Object regvalue;     //��ֵ
            string cmd;          //WinRAR �������
            ProcessStartInfo startinfo;
            Process process;
            try
            {
                //regkey = Registry.ClassesRoot.OpenSubKey(@"Applications\WinRAR.exe\shell\open\command");
                //regvalue = regkey.GetValue("");  // ��ֵΪ "d:\Program Files\WinRAR\WinRAR.exe" "%1"
                //rarexe = regvalue.ToString();
                //regkey.Close();
                //rarexe = rarexe.Substring(1, rarexe.Length - 7);  // d:\Program Files\WinRAR\WinRAR.exe

                Directory.CreateDirectory(path);
                //ѹ������൱����Ҫѹ�����ļ���(path)�ϵ��Ҽ�->WinRAR->��ӵ�ѹ���ļ�->����ѹ���ļ���(rarName)
                cmd = string.Format("a {0} {1} -r",
                                    rarName,
                                    path);
                startinfo = new ProcessStartInfo();
                startinfo.FileName = ConfigurationManager.AppSettings["RarPath"];
                startinfo.Arguments = cmd;                          //�����������
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;  //���� WinRAR ����

                startinfo.WorkingDirectory = rarPath;
                process = new Process();
                process.StartInfo = startinfo;
                process.Start();
                process.WaitForExit(); //�����ڵȴ����� winrar.exe �˳�
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
        /// ���� WinRAR ���н�ѹ��
        /// </summary>
        /// <param name="path">�ļ���ѹ·�������ԣ�</param>
        /// <param name="rarPath">��Ҫ��ѹ���� .rar �ļ��Ĵ��Ŀ¼������·����</param>
        /// <param name="rarName">��Ҫ��ѹ���� .rar �ļ�����������׺��</param>
        /// <returns>true �� false����ѹ���ɹ����� true����֮��false��</returns>
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
                //��ѹ������൱����Ҫѹ���ļ�(rarName)�ϵ��Ҽ�->WinRAR->��ѹ����ǰ�ļ���
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
