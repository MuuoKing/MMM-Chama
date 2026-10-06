using System.Collections.Concurrent;
using MMM.Chama.Api.Models;

namespace MMM.Chama.Api.Services;

public interface IChamaStore
{
    IReadOnlyCollection<Chama> GetAll();
    Chama? Get(Guid id);
    Chama Create(CreateChamaRequest request);
    Member? AddMember(Guid chamaId, AddMemberRequest request);
}

public sealed class InMemoryChamaStore : IChamaStore
{
    private readonly ConcurrentDictionary<Guid, Chama> _chamas = new();

    public IReadOnlyCollection<Chama> GetAll() => _chamas.Values.OrderByDescending(x => x.CreatedAt).ToArray();

    public Chama? Get(Guid id) => _chamas.TryGetValue(id, out var chama) ? chama : null;

    public Chama Create(CreateChamaRequest request)
    {
        var chama = new Chama(
            Guid.NewGuid(),
            request.Name.Trim(),
            request.Description?.Trim() ?? string.Empty,
            request.ContributionAmount,
            string.IsNullOrWhiteSpace(request.ContributionFrequency) ? "Monthly" : request.ContributionFrequency.Trim(),
            DateTime.UtcNow,
            Array.Empty<Member>());

        _chamas[chama.Id] = chama;
        return chama;
    }

    public Member? AddMember(Guid chamaId, AddMemberRequest request)
    {
        if (!_chamas.TryGetValue(chamaId, out var chama)) return null;

        var member = new Member(Guid.NewGuid(), chamaId, request.FullName.Trim(), request.Email.Trim(), request.PhoneNumber?.Trim() ?? string.Empty, DateTime.UtcNow);
        _chamas[chamaId] = chama with { Members = chama.Members.Append(member).ToArray() };
        return member;
    }
}
