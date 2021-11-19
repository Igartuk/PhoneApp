using PhoneApi.Dto;
using PhoneApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApi.Repositories
{
    public interface IPhoneRepository
    {
        public Task<IEnumerable<Phone>> GetPhones();
        public Task<Phone> GetPhone(int id);
        public Task<Phone> CreatePhone(PhoneForCreationDto phone);
        public Task UpdatePhone(int id, PhoneForUpdateDto phone);
        public Task DeletePhone(int id);
    }
}
