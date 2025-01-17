using System.Text.Json;
using BudgetMate.Model;

namespace BudgetMate.Abstraction;

public class Userbase
{
    private static string _filePath = Path.Combine(FileSystem.AppDataDirectory, "user.json");
    protected List<User> LoadUser()
    {
        if (!File.Exists(_filePath)) return new List<User>();
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }
    protected void SaveUser(List<User> user)
    {
        var json = JsonSerializer.Serialize(LoadUser());
        File.WriteAllText(_filePath, json);
    }
}