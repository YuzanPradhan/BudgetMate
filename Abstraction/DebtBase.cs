using System.Text.Json;
using BudgetMate.Model;

namespace BudgetMate.Abstraction;

public abstract class DebtBase
{
    private static readonly string FilePath =
        Path.Combine("C:\\Users\\YUZAN\\Desktop\\_\\Coursework\\.net\\BudgetMate\\wwwroot", "Data",
            "debts.json");

    protected List<Debt> LoadDebts()
    {
        if (!File.Exists(FilePath)) return new List<Debt>();
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<Debt>>(json) ?? new List<Debt>();
    }

    protected void SaveDebts(List<Debt> debts)
    {
        var json = JsonSerializer.Serialize(debts, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }
}