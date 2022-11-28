using Core.Application.Dtos;

namespace ETradeApi.Application.Features.UserOperationClaims.Dtos;

public class UserOperationClaimListDto : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}