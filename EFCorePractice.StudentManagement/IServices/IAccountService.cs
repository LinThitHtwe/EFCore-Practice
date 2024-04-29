using EFCorePractice.StudentManagement.DTOs;
using EFCorePractice.StudentManagement.Models;

namespace EFCorePractice.StudentManagement.IServices
{
    public interface IAccountService
    {
        public IEnumerable<AccountResponseDTO> GetAll();
        public AccountResponseDTO GetById(int id);
        public IEnumerable<AccountResponseDTO> GetPaginatedAccounts(int currentPage,int itemPerPage);
        public void Create(AccountRequestDTO accountRequest);
        public void Update(AccountRequestDTO accountRequest);
        public void Delete(int id);
        public bool IsAccountExist(int id);
        public Account GetAccountModelById(int id);

    }
}
