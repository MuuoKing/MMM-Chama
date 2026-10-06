namespace MMM.Chama.Api.Models;

public sealed record Chama(
    Guid Id,
    string Name,
    string Description,
    decimal ContributionAmount,
    string ContributionFrequency,
    DateTime CreatedAt,
    IReadOnlyCollection<Member> Members);

public sealed record Member(
    Guid Id,
    Guid ChamaId,
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime JoinedAt);

public sealed record CreateChamaRequest(
    string Name,
    string? Description,
    decimal ContributionAmount,
    string? ContributionFrequency);

public sealed record AddMemberRequest(
    string FullName,
    string Email,
    string? PhoneNumber);
