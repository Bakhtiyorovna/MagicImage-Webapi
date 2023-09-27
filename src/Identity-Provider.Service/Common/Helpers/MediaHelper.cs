
namespace Identity_Provider.Service.Common.Helpers;

public class MediaHelper
{
    public static string MakeFileName(string filename)
    {
        FileInfo fileInfo = new FileInfo(filename);
        string extension = fileInfo.Extension;
        string name = "File_" + Guid.NewGuid() + extension;
        return name;
    }
}
