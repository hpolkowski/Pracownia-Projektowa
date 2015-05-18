using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tamir.SharpSsh;

namespace PProj
{
    class FileDownloader
    {
        Scp myScp;
        SshExec mySshExec;
        private String ftpAdr;
        private String user;
        private String password;
                
        public FileDownloader(String fftpAdr, String uuser, String ppassword)
        {
            ftpAdr = fftpAdr;
            user = uuser;
            password = ppassword;
        }

        public void download(String serverPath, String localPath)
        {
            myScp = new Scp(ftpAdr, user, password);
            myScp.Connect(22);
            myScp.Get(serverPath, localPath);
            myScp.Close();
        }

        private String getMeLastModificationTime(String serverPath)
        {
            String stdOut = null;
            String stdErr = null;
            // need to send following cmd: stat serverPath | grep -E ^M | cut -d' ' -f2
            String cmdToExec = "stat " + serverPath + " | grep -E ^M | cut -d' ' -f2";
            mySshExec = new SshExec(ftpAdr, user, password);
            mySshExec.Connect();
            mySshExec.RunCommand(cmdToExec, ref stdOut, ref stdErr);
            mySshExec.Close();
            return stdOut;

        }

        // return true if remote db was modified
        public bool shouldDownloadUpdate(String localDate, String serverPath)
        {
            if (String.Compare(localDate, getMeLastModificationTime(serverPath)) == 0)
                return false;
            else
                return true;
        }

    }
}
