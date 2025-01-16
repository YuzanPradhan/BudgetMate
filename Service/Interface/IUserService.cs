using BudgetMate.Model;

namespace BudgetMate.Service.Interface;

public interface IUserService
{
   bool Login(User user);  
   bool Register(User user);
}

