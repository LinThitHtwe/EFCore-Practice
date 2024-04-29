using EFCorePractice.StudentManagement.Data;
using EFCorePractice.StudentManagement.IRepository;
using EFCorePractice.StudentManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCorePractice.StudentManagement.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public bool Create(Account account)
        {
             _context.Accounts.Add(account);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public bool Delete(Account account)
        {
            _context.Accounts.Remove(account);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Accounts.AsNoTracking().ToList();
        }

        public Account GetByEmail(string email)
        {
            return _context.Accounts.FirstOrDefault(account => account.Email.ToLower() == email.Trim().ToLower());
        }

        public Account GetById(int id)
        {
            return _context.Accounts.FirstOrDefault(account => account.Id == id);
        }

        public IEnumerable<Account> GetPaginatedAccount(int currentPage, int itemPerPage)
        {
            return _context.Accounts.AsNoTracking()
                                   .Skip((currentPage - 1) * itemPerPage)
                                   .Take(itemPerPage)
                                   .ToList();
        }

        public bool Update(Account account)
        {
            _context.Accounts.Update(account);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public int GetTotalPages(int itemPerPage)
        {
            var rowCount = _context.Accounts.AsNoTracking().Count();
            return (int)Math.Ceiling((double)rowCount / itemPerPage);
        }
    }
}
