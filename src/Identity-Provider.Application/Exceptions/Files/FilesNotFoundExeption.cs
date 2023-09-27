using Identity_Provider.Application.Exceptions;

namespace HeavyService.Application.Exeptions.Files;

public class FilesNotFoundExeption : BadRequestException
{
    public FilesNotFoundExeption()
    {
        this.TitleMessage = "SUBPATH is null";
    }
}