using Dapper;
using PhoneApi.Context;
using PhoneApi.Dto;
using PhoneApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApi.Repositories
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly IDbConnection _dbConnection;
        public PhoneRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<Phone>>GetPhones()
        {
            var query = "SELECT * FROM Phones";

            return (await _dbConnection.QueryAsync<Phone>(query)).ToList();
        }
        public async Task<Phone> GetPhone(int id)
        {
            var query = "SELECT * FROM Phones WHERE Id = @Id";
          
            return (await _dbConnection.QuerySingleOrDefaultAsync<Phone>(query, new { id })); 
        }
        public async Task<Phone> CreatePhone(PhoneForCreationDto phone)
        {
            var query = "INSERT INTO Phones (Name, Picture) VALUES (@Name, @Picture)"+ "SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", phone.Name, DbType.String);
            parameters.Add("Address", phone.Picture, DbType.String);
            using (var dbConnection = _dbConnection)
            {
                var id = await dbConnection.QuerySingleAsync<int>(query, parameters);
                var createdPhone = new Phone
                {
                    Id = id,
                    Name = phone.Name,
                    Picture = phone.Picture,
                   
                };
                return createdPhone;
            }
        }
        public async Task UpdatePhone(int id, PhoneForUpdateDto phone)
        {
            var query = "UPDATE Phones SET Name = @Name, Picture = @Picture, WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", phone.Name, DbType.String);
            parameters.Add("Picture", phone.Picture, DbType.String);
            using (var dbConnection = _dbConnection)
            {
                await dbConnection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeletePhone(int id)
        {
            var query = "DELETE FROM Phones WHERE Id = @Id";
            using (var dbConnection = _dbConnection)
            {
                await dbConnection.ExecuteAsync(query, new { id });
            }
        }
    }
}
