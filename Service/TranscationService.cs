using System.Net.Http.Json;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using BudgetMate.Abstraction;
using BudgetMate.Model;
using BudgetMate.Service.Interface;

namespace BudgetMate.Service;

public class TranscationService : TranscationBase, ITranscations
{
    private readonly List<Transaction> _transactions;

    public TranscationService()
    {
        _transactions = loadAllTransactions();
    }
    
    public List<Transaction> GetTranscations()
    {
        return _transactions;
    }
    
    public void AddTransaction(Transaction transaction)
    {
        transaction.Id = _transactions.Any() ? _transactions.Max(t => t.Id) + 1 : 1;
        _transactions.Add(transaction);
        SaveTransactions(_transactions);
    }

    public void SaveTranscation(List<Transaction> transaction)
    {
        SaveTransactions(transaction);
    }
    
}
