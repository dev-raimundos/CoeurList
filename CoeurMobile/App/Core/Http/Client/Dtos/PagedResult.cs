namespace CoeurMobile.App.Core.Http.Client.Dtos;

/// <summary>Espelha o formato de paginação da Coeur API (<c>SharedKernel.Common.PagedResult&lt;T&gt;</c>).</summary>
public sealed record PagedResult<T>(
    List<T> Items,
    int Page,
    int PageSize,
    int TotalCount
)
{
    public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
}
