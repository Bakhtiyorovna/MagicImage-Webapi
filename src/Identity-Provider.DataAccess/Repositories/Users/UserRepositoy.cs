using Dapper;
using Identity_Provider.DataAccess.Interfaces.Users;
using Identity_Provider.Domain.Entities.Users;

namespace Identity_Provider.DataAccess.Repositories.Users;

public class UserRepositoy : BaseRepository, IUserRepository
{
    public Task<long> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateAsync(User entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.users( first_name, last_name, identity_provider, password_hash, role, " +
                "confirm, salt, created_at, updated_at) " +
                    " VALUES ( @FirstName, @LastName, @IdentityProvider, " +
                        " @PasswordHash,@Role, @Confirm, @Salt, @CreatedAt, @UpdatedAt)";

            var result = await _connection.ExecuteAsync(query, entity);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"Delete  from public.users  Where id ={id};";
            var result = await _connection.ExecuteAsync(query);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "SELECT id, first_name, last_name, phone_number, region, district, address, created_at, updated_at " +
                $" FROM public.users where id = {id};";

            var result = await _connection.QuerySingleOrDefaultAsync<User>(query);

            return result;
        }
        catch
        {
            return new User();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT id, first_name, last_name, identity_provider, provider_key, clein_id, password_hash, salt, description, confirm, role, created_at, updated_at " +
                $"FROM public.users Where identity_provider ='bobonazarovhasan54@gmail.com';";
            var data = await _connection.QuerySingleOrDefaultAsync<User>(query);
            return data;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<int> UpdateAsync(long id, User entity)
    {
        throw new NotImplementedException();
    }
}
