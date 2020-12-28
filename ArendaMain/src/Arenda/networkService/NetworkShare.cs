using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Arenda;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System.Diagnostics;

public class NetworkShare : IDisposable
{
    readonly Procedures _proc = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

    private string prefix = "";
    public NetworkShare()
    {

    }
    /// <summary>
    /// Создание эксемпляра с инициализацией
    /// </summary>
    /// <param name="init">Need data</param>
    public NetworkShare(bool init,bool isLandLord)
    {
        if (init)
        {
           
            DataTable dt = _proc.EditGetConf(ConnectionSettings.GetIdProgram(), "", "");
            this.server = dt.AsEnumerable().Where(r => r.Field<string>("id_value") == "psss").CopyToDataTable().Rows[0]["value"].ToString();
           
           
            this.login = dt.AsEnumerable().Where(r => r.Field<string>("id_value") == "pslg").CopyToDataTable().Rows[0]["value"].ToString();
            this.password = dt.AsEnumerable().Where(r => r.Field<string>("id_value") == "pspw").CopyToDataTable().Rows[0]["value"].ToString();
        }

        if (isLandLord)
            prefix = "\\sign";
    }
    public string server;
    public string password;
    public string login;
    /// <summary>
    /// ну это чисто для себя, для прикола сделал )
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
    /// <summary>
    /// Connects to the remote share
    /// </summary>
    /// <returns>Null if successful, otherwise error message.</returns>
    public string ConnectToShare()
    {
        //Create netresource and point it at the share
        
        NETRESOURCE nr = new NETRESOURCE();
        nr.dwType = RESOURCETYPE_DISK;
        nr.lpRemoteName = server;
        //Disconnect531();
        //Create the share
        //удаляем текущие подключения

        Process proc = new Process();
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.FileName = "cmd.exe";
        proc.StartInfo.Arguments = "/c net use * /delete /y";
        proc.StartInfo.CreateNoWindow = true;
        proc.Start();
        int ret = WNetUseConnection(IntPtr.Zero, nr, password, login, 0, null, null, null);
        //Check for errors
        if (ret == NO_ERROR)
            return null;
        else
        {   
            if (ret == ERROR_SESSION_CREDENTIAL_CONFLICT)
            {
                
            }
            return GetError(ret);
        }
    }

    /// <summary>
    /// Remove the share from cache.
    /// </summary>
    /// <returns>Null if successful, otherwise error message.</returns>
    public string DisconnectFromShare(bool force)
    {
        //remove the share
        int ret = WNetCancelConnection(server, force);

        //Check for errors
        if (ret == NO_ERROR)
            return null;
        else
            return GetError(ret);
    }
    /// <summary>
    /// Копирование с сервера
    /// </summary>
    /// <param name="id_doc">id документа</param>
    /// <param name="shortname">Название файла</param>
    /// <param name = "ext">расширение файла</param>
    /// <param name="filepath">Путь куда копировать</param>
    public void CopyFromServer(string id_doc, string shortname,string ext, string filepath)
    {
        try
        {
            ConnectToShare();
            //if (!Directory.Exists(filepath))
            //   Directory.CreateDirectory(filepath);
            string PathFile = $"{server}{prefix}\\{id_doc}\\{shortname}{ext}";
            if (File.Exists(PathFile))
            {
                byte[] file = File.ReadAllBytes(PathFile);
                File.WriteAllBytes(filepath, file);
            }
          //  File.Copy($"{server}\\{id_doc}\\{shortname}{ext}", $"{filepath}");
            DisconnectFromShare(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка в копировании");
            Console.WriteLine(ex.Message);
        }
    }
    public bool CopyFile(string id_doc, string fullname, string shortname)
    {
        try
        {
            ConnectToShare();
            if (!Directory.Exists(server + "\\" + id_doc))
                Directory.CreateDirectory(server + "\\" + id_doc);
            File.Copy(fullname, server + "\\" + id_doc + "\\" + shortname);
            DisconnectFromShare(false);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool CopyBytes(byte[] bytes, string namefile, string id_doc)
    {
        try
        {
            ConnectToShare();
            
            string folder = $"{server} {prefix}\\{id_doc}";

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            
            File.WriteAllBytes($"{folder}\\{namefile}", bytes);
            
            DisconnectFromShare(false);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool CopyBytesWithServer(byte[] bytes, string pathWithID, string name)
    {
        try
        {
            ConnectToShare();
            if (!Directory.Exists(pathWithID))
                Directory.CreateDirectory(pathWithID);
            File.WriteAllBytes(pathWithID + "\\" + name, bytes);
            DisconnectFromShare(false);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public DataTable GetListFiles(string id_doc)
    {
        DataTable dtFiles = new DataTable();
        
        string[] allfiles;
        if (Directory.Exists(server + "\\" + id_doc))
        {
            allfiles = Directory.GetFiles(server + "\\" + id_doc);
            foreach (string file in allfiles)
            {

            }
        }
        return dtFiles;
    }

    public byte[] GetFileBytes(string id_doc, string nameFile, string ext)
    {
        byte[] file = null;
        ConnectToShare();

        string path = server + prefix + "\\" + id_doc + "\\" + nameFile + ext;
        if (File.Exists(path))
            file = File.ReadAllBytes(path);

        DisconnectFromShare(false);
        return file;
    }

    public DataTable GetFileToFolde(int id_doc)
    {
        DataTable dtScan = _proc.getScan(0, -1);
        DataTable dtToSend = dtScan.Clone();
        try
        {         
            ConnectToShare();

            string folder = $"{server} {prefix}\\{id_doc}";

            string[] listFile = Directory.GetFiles(folder);
            
            foreach (string file in listFile)
            {
                FileInfo infoFile = new FileInfo(file);
                DataRow newRow = dtToSend.NewRow();
                newRow["id"] = -1;
                newRow["id_Doc"] = id_doc;
                newRow["cName"] = Path.GetFileNameWithoutExtension(file);
                newRow["Scan"] = null;
                newRow["Extension"] = Path.GetExtension(file);
                newRow["id_DocType"] = 11;
                newRow["DateDocument"] = DateTime.Now;
                newRow["Path"] = Path.GetDirectoryName(file);
                dtToSend.Rows.Add(newRow);

            }
            
            DisconnectFromShare(false);

            return dtToSend;
        }
        catch
        {
            return dtScan.Clone();
        }
    }

    #region P/Invoke Stuff
    [DllImport("Mpr.dll")]
    private static extern int WNetUseConnection(
        IntPtr hwndOwner,
        NETRESOURCE lpNetResource,
        string lpPassword,
        string lpUserID,
        int dwFlags,
        string lpAccessName,
        string lpBufferSize,
        string lpResult
        );

    [DllImport("Mpr.dll")]
    private static extern int WNetCancelConnection(
        string lpName,
        bool fForce
        );
    [DllImport("Mpr.dll")]
    private static extern string WNetCancelConnection2(
        string lpName,
        int q,
        bool w
        );



    [StructLayout(LayoutKind.Sequential)]
    private class NETRESOURCE
    {
        public int dwScope = 0;
        public int dwType = 0;
        public int dwDisplayType = 0;
        public int dwUsage = 0;
        public string lpLocalName = "";
        public string lpRemoteName = "";
        public string lpComment = "";
        public string lpProvider = "";
    }

    #region Consts
    const int RESOURCETYPE_DISK = 0x00000001;
    const int CONNECT_UPDATE_PROFILE = 0x00000001;
    #endregion

    #region Errors
    const int NO_ERROR = 0;

    const int ERROR_ACCESS_DENIED = 5;
    const int ERROR_ALREADY_ASSIGNED = 85;
    const int ERROR_BAD_DEVICE = 1200;
    const int ERROR_BAD_NET_NAME = 67;
    const int ERROR_BAD_PROVIDER = 1204;
    const int ERROR_CANCELLED = 1223;
    const int ERROR_EXTENDED_ERROR = 1208;
    const int ERROR_INVALID_ADDRESS = 487;
    const int ERROR_INVALID_PARAMETER = 87;
    const int ERROR_INVALID_PASSWORD = 1216;
    const int ERROR_MORE_DATA = 234;
    const int ERROR_NO_MORE_ITEMS = 259;
    const int ERROR_NO_NET_OR_BAD_PATH = 1203;
    const int ERROR_NO_NETWORK = 1222;
    const int ERROR_SESSION_CREDENTIAL_CONFLICT = 1219;

    const int ERROR_BAD_PROFILE = 1206;
    const int ERROR_CANNOT_OPEN_PROFILE = 1205;
    const int ERROR_DEVICE_IN_USE = 2404;
    const int ERROR_NOT_CONNECTED = 2250;
    const int ERROR_OPEN_FILES = 2401;

    private struct ErrorClass
    {
        public int num;
        public string message;
        public ErrorClass(int num, string message)
        {
            this.num = num;
            this.message = message;
        }
    }

    private static ErrorClass[] ERROR_LIST = new ErrorClass[] {
        new ErrorClass(ERROR_ACCESS_DENIED, "Error: Access Denied"),
        new ErrorClass(ERROR_ALREADY_ASSIGNED, "Error: Already Assigned"),
        new ErrorClass(ERROR_BAD_DEVICE, "Error: Bad Device"),
        new ErrorClass(ERROR_BAD_NET_NAME, "Error: Bad Net Name"),
        new ErrorClass(ERROR_BAD_PROVIDER, "Error: Bad Provider"),
        new ErrorClass(ERROR_CANCELLED, "Error: Cancelled"),
        new ErrorClass(ERROR_EXTENDED_ERROR, "Error: Extended Error"),
        new ErrorClass(ERROR_INVALID_ADDRESS, "Error: Invalid Address"),
        new ErrorClass(ERROR_INVALID_PARAMETER, "Error: Invalid Parameter"),
        new ErrorClass(ERROR_INVALID_PASSWORD, "Error: Invalid Password"),
        new ErrorClass(ERROR_MORE_DATA, "Error: More Data"),
        new ErrorClass(ERROR_NO_MORE_ITEMS, "Error: No More Items"),
        new ErrorClass(ERROR_NO_NET_OR_BAD_PATH, "Error: No Net Or Bad Path"),
        new ErrorClass(ERROR_NO_NETWORK, "Error: No Network"),
        new ErrorClass(ERROR_BAD_PROFILE, "Error: Bad Profile"),
        new ErrorClass(ERROR_CANNOT_OPEN_PROFILE, "Error: Cannot Open Profile"),
        new ErrorClass(ERROR_DEVICE_IN_USE, "Error: Device In Use"),
        new ErrorClass(ERROR_EXTENDED_ERROR, "Error: Extended Error"),
        new ErrorClass(ERROR_NOT_CONNECTED, "Error: Not Connected"),
        new ErrorClass(ERROR_OPEN_FILES, "Error: Open Files"),
        new ErrorClass(ERROR_SESSION_CREDENTIAL_CONFLICT, "Error: Credential Conflict"),
    };


    private static string GetError(int errNum)
    {
        foreach (ErrorClass er in ERROR_LIST)
        {
            if (er.num == errNum) return er.message;
        }
        return "Error: Unknown, " + errNum;
    }
    #endregion

    #endregion
}