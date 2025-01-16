using BudgetMate.Model;

namespace BudgetMate.Service.Interface;

public interface IDeptService
{
    List<Debt> GetDebts();
    void AddDebt(Debt debt);
    void SaveUpdatedDept(List<Debt> debts);
}