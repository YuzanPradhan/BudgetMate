using BudgetMate.Model;

namespace BudgetMate.Service.Interface;

public interface ITagsService
{
    List<Tag> GetTags();
    void AddTag(Tag tag);
    void DeleteTag(int id);
}