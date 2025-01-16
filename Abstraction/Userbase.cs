using System.Text.Json;
using BudgetMate.Model;

namespace BudgetMate.Abstraction;

public class Userbase
{
    private static string FilePath = Path.Combine(FileSystem.AppDataDirectory, "user.json");
    protected List<User> LoadUser()
    {
        if (!File.Exists(FilePath)) return new List<User>();
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }
    protected void SaveUser(List<User> user)
    {
        var json = JsonSerializer.Serialize(LoadUser());
        File.WriteAllText(FilePath, json);
    }
}