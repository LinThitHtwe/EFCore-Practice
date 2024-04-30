using EFCorePractice.StudentManagement.DTOs;
using EFCorePractice.StudentManagement.Exceptions;
using EFCorePractice.StudentManagement.IRepository;
using EFCorePractice.StudentManagement.IServices;
using EFCorePractice.StudentManagement.Models;
using EFCorePractice.StudentManagement.Repository;
using EFCorePractice.StudentManagement.Utils;
using System.Security.Principal;
using System.Text.RegularExpressions;
using static EFCorePractice.StudentManagement.Enums.Enum;

namespace EFCorePractice.StudentManagement.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Create(AccountRequestDTO accountRequest)
        {
            if (accountRequest is null)
            {
                throw new ArgumentNullException(nameof(accountRequest), "Account cannot be null");
            }
            if (!IsValidEmail(accountRequest.Email))
            {
                throw new ArgumentException("Invalid email address", nameof(accountRequest));
            }
            if (_accountRepository.GetByEmail(accountRequest.Email) is not null)
            {
                throw new DataAlreadyExistsException($"Account with email `{accountRequest.Email}` already exists");
            }
            if (!Enum.IsDefined(typeof(AccountType), accountRequest.AccountType))
            {
                throw new ArgumentException(nameof(accountRequest.AccountType), "Invalid account type. Please provide a valid account type.");
            }

            Account newAccount = new()
            {
                Email = accountRequest.Email,
                Password = accountRequest.Password,

            };
            if (Enum.TryParse<AccountType>(accountRequest.AccountType, out AccountType parsedAccountType))
            {
                newAccount.AccountType = parsedAccountType;
            }
            var result = _accountRepository.Create(newAccount);
            if (!result)
            {
                throw new InvalidOperationException("Failed to create the account.");
            }
        }

        public void Delete(int id)
        {
            if (!IsAccountExist(id))
            {
                throw new NotFoundException($"Account with id - {id} not found");
            }
            var result = _accountRepository.Delete(GetAccountModelById(id));
            if (!result)
            {
                throw new InvalidOperationException("Failed to delete the account.");
            }
        }

        public Account GetAccountModelById(int id)
        {
            if (!IsAccountExist(id))
            {
                throw new NotFoundException($"Account with {id} not found");
            }
            return _accountRepository.GetById(id);
        }

        public IEnumerable<AccountResponseDTO> GetAll()
        {
            var accounts = _accountRepository.GetAll();
            List<AccountResponseDTO> responseLists = new();
            foreach (var account in accounts)
            {

                responseLists.Add(new AccountResponseDTO() { Id = account.Id, Email = account.Email, AccountType = GetAccountType(account.AccountType) });
            };
            return responseLists;
        }

        public AccountResponseDTO GetById(int id)
        {
            if (!IsAccountExist(id))
            {
                throw new NotFoundException($"Account with {id} not found");
            }
            var account = _accountRepository.GetById(id);
            AccountResponseDTO accountResponse = new()
            {
                Id = account.Id,
                AccountType = GetAccountType(account.AccountType),
                Email = account.Email,
            };
            return accountResponse;
        }

        public IEnumerable<AccountResponseDTO> GetPaginatedAccounts(int currentPage, int itemPerPage)
        {
            if (currentPage > _accountRepository.GetTotalPages(itemPerPage))
            {
                throw new ArgumentOutOfRangeException(nameof(currentPage), "Current page exceeds the total number of pages.");
            }
            var accounts = _accountRepository.GetPaginatedAccount(currentPage, itemPerPage);
            List<AccountResponseDTO> responseLists = new();
            foreach (var account in accounts)
            {
                responseLists.Add(new AccountResponseDTO() { Id = account.Id, Email = account.Email, AccountType = GetAccountType(account.AccountType) });
            }
            return responseLists;
        }

        public int GetTotalPages(int itemPerPage)
        {
            return _accountRepository.GetTotalPages(itemPerPage);
        }

        public bool IsAccountExist(int id)
        {
            var account = _accountRepository.GetById(id);
            return account is not null;
        }

        public void Update(int id, AccountRequestDTO accountRequest)
        {
            if (accountRequest is null)
            {
                throw new ArgumentNullException(nameof(accountRequest), "Account cannot be null");
            }

            if (!IsValidEmail(accountRequest.Email))
            {
                throw new ArgumentException("Invalid email address", nameof(accountRequest));
            }

            if (!Enum.IsDefined(typeof(AccountType), accountRequest.AccountType))
            {
                throw new ArgumentException(nameof(accountRequest.AccountType), "Invalid account type. Please provide a valid account type.");
            }

            if (!IsAccountExist(id))
            {
                throw new NotFoundException($"Account with {id} not found");
            }

            var isAccountWithSameEmail = _accountRepository.GetByEmail(accountRequest.Email);

            if (isAccountWithSameEmail is not null && isAccountWithSameEmail.Id != id)
            {
                throw new DataAlreadyExistsException($"Account with email `${accountRequest.Email}` already exists");
            }

            var account = GetAccountModelById(id);
            account.Email = accountRequest.Email;
            if (Enum.TryParse<AccountType>(accountRequest.AccountType, out AccountType parsedAccountType))
            {
                account.AccountType = parsedAccountType;
            }


            var result = _accountRepository.Update(account);
            if (!result)
            {
                throw new InvalidOperationException("Failed to update the account.");
            }
        }

        private static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new(pattern);
            return regex.IsMatch(email);
        }

        private static string GetAccountType(AccountType accountType)
        {
            return accountType switch
            {
                (AccountType.admin) => "admin",
                (AccountType.parent) => "parent",
                (AccountType.student) => "student",
                _ => "null",
            };
        }
    }
}
