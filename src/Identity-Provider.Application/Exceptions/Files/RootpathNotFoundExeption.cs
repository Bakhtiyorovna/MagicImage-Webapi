
using Identity_Provider.Application.Exceptions;
using System.Runtime.CompilerServices;

namespace FileSystem.Service.Exeptions.Files;

public class RootpathNotFoundExeption : NotFoundException
{
    public RootpathNotFoundExeption()
    {
        TitleMessage = "ROOTPATH is null";
    }
}
