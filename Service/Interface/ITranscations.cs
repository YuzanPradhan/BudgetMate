

using BudgetMate.Model;

namespace BudgetMate.Service.Interface;

public interface ITranscations
{
    List<Transaction> GetTranscations();
    void AddTransaction(Transaction transaction);

    void SaveTransactions(List<Transaction> transactions);
    void SaveTranscation(List<Transaction> transaction);
}
    