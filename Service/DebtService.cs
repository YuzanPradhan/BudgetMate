using BudgetMate.Abstraction;
using BudgetMate.Model;
using BudgetMate.Service.Interface;

namespace BudgetMate.Service;

public class DebtService: DebtBase, IDeptService
{
    private List<Debt> _debts;

    public DebtService()
    {
        _debts = LoadDebts();
    }

    public List<Debt> GetDebts()
    {
        return _debts;
    }

    public void AddDebt(Debt debt)
    {
        debt.Id = _debts.Any() ? _debts.Max(d => d.Id) + 1 : 1;
        
        _debts.Add(debt);
        SaveDebts(_debts);
    }

    public void SaveUpdatedDept(List<Debt> debts)
    {
        SaveDebts(debts);
    }
}