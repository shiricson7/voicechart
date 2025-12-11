using System.Text.Json;
using VoiceChart.Core.Models;

namespace VoiceChart.Core.Storage;

public interface IVisitRepository
{
    Task SaveAsync(Visit visit, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Visit>> ListAsync(CancellationToken cancellationToken = default);
}

public class FileVisitRepository : IVisitRepository
{
    private readonly string _root;

    public FileVisitRepository(string root)
    {
        _root = root;
        Directory.CreateDirectory(_root);
    }

    public async Task SaveAsync(Visit visit, CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_root, $"{visit.Id}.json");
        var json = JsonSerializer.Serialize(visit, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(path, json, cancellationToken);
    }

    public async Task<IReadOnlyList<Visit>> ListAsync(CancellationToken cancellationToken = default)
    {
        var visits = new List<Visit>();
        foreach (var file in Directory.GetFiles(_root, "*.json"))
        {
            var json = await File.ReadAllTextAsync(file, cancellationToken);
            var visit = JsonSerializer.Deserialize<Visit>(json);
            if (visit != null)
            {
                visits.Add(visit);
            }
        }

        return visits;
    }
}
