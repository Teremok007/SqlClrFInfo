using System;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//namespace SqlCrlFInfo
//{
    public static class FInfo
    {

        [SqlFunction]
        public static long GetFileSize(SqlChars aPath)
        {
            string path = aPath.ToSqlString().Value;
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                return fileInfo.Length;
            }
            return -1;
        }
        [SqlFunction]
        public static long GetTotalFreeSpace(SqlChars aDriveName)
        {            
            string driveName = aDriveName.ToSqlString().Value;
            if (driveName.Length == 1)
        {
            driveName = driveName + ':';
        }
            if (driveName.Length == 2 && driveName.ToCharArray().GetValue(1).Equals(':'))
        {
            driveName = driveName + "\\";
        }
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name.ToUpper() == driveName.ToUpper())
                {
                    return drive.TotalFreeSpace;
                }
            }
            return -1;
        }

        [SqlFunction]
        public static SqlString GetDrivesList()
        {
            string driveList = string.Empty;
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && !String.IsNullOrEmpty(drive.Name))
                {
                    driveList += String.IsNullOrEmpty(driveList) ? drive.Name : "," + drive.Name;
                }
            }
            return driveList;
        }

    }
//}