using EFCorePractice.StudentManagement.DTOs;
using EFCorePractice.StudentManagement.Models;

namespace EFCorePractice.StudentManagement.IRepository
{
    public interface IAccountRepository
    {
        public IEnumerable<Account> GetAll();
        public IEnumerable<Account> GetPaginatedAccount(int currentPage,int itemPerPage);
        public Account GetByEmail(string email);
        public Account? GetById(int id);
        public bool Create(Account account);
        public bool Update(Account account);
        public bool Delete(Account account);
        public int GetTotalPages(int itemPerPage);
    }
}
