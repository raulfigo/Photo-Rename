using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string strSourcePath = @"E:\ProgramSource";
            string strDestinationPath = @"E:\ProgramDestination";

            string[] fileArray = Directory.GetFiles(strSourcePath, "*.*", SearchOption.AllDirectories);

            foreach (string strFile in fileArray)
            {

                DateTime datLastWriteTime = File.GetLastWriteTime(strFile);

                string strYYYYMMDD = datLastWriteTime.ToString("yyyyMMdd");
                string strYYYYMMDDHHMMSS = datLastWriteTime.ToString("yyyyMMddhhmmss");
                string strExt = Path.GetExtension(strFile);
                string strFullSource = strFile;
                string strFullDestination;

                //Rename
                if (strExt == ".MP4")
                {
                    string strDirName = Path.GetDirectoryName(strFile);
                    string strNewFileName = strDirName + @"\" + strYYYYMMDDHHMMSS + strExt;

                    int intCount = 0;
                    while (File.Exists(strNewFileName))
                    {
                        strNewFileName = strDirName + @"\" + strYYYYMMDDHHMMSS + "_" + intCount + strExt;
                        intCount++;
                    }

                    File.Move(strFile, strNewFileName);
                    strFullSource = strNewFileName;
                }

                string strFolder = strDestinationPath + @"\" + strYYYYMMDD;
                bool booExists = Directory.Exists(strFolder);
                if (!booExists)
                {
                    Directory.CreateDirectory(strFolder);
                }

                strFullDestination = strFolder + @"\"+ Path.GetFileName(strFullSource);

                int intCount2 = 0;
                while (File.Exists(strFullDestination))
                {

                    strFullDestination = strFolder + @"\" + Path.GetFileNameWithoutExtension(strFullSource) + "_" + intCount2 + Path.GetExtension(strFullSource);
                    intCount2++;


                }

                File.Move(strFullSource, strFullDestination);


            }



        }
    }
}
