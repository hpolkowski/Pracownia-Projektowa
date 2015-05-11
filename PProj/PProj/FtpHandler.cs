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
                
        public FileDownloader(String ftpAdr, String user, String password)
        {
            myScp = new Scp(ftpAdr, user, password);
            myScp.Connect(22);
        }

        public void download(String serverPath, String localPath)
        {
            myScp.Get(serverPath, localPath);
            myScp.Close();
        }

    }
}
