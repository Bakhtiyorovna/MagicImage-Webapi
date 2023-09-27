using Npgsql;
namespace Identity_Provider.DataAccess.Repositories;

public class BaseRepository
{
    protected NpgsqlConnection _connection;
    public BaseRepository()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
       // string _connection = "Host=dbaas-db-8432700-do-user-14588616-0.b.db.ondigitalocean.com; Port=25060; Database=magicimage-db; User Id=doadmin; Password=AVNS_DFAB2OSiTBvYrIQx4fH;";

        this._connection = new NpgsqlConnection("Host=dbaas-db-8432700-do-user-14588616-0.b.db.ondigitalocean.com; Port=25060; Database=magicimage-db; User Id=doadmin; Password=AVNS_DFAB2OSiTBvYrIQx4fH;");
    }
}
