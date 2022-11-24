﻿using AutoMapper;
using Core.Security.Entities;
using ETradeApi.Application.Features.Auths.Dtos;
using ETradeApi.Application.Features.Auths.Rules;
using ETradeApi.Application.Services.AuthServices;
using MediatR;

namespace ETradeApi.Application.Features.Auths.Commands.RevokeToken
{
    public class RevokeTokenCommand : IRequest<RevokedTokenDto>
    {
        public string Token { get; set; }
        public string IPAddress { get; set; }

        public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, RevokedTokenDto>
        {
            private readonly IAuthService _authService;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IMapper _mapper;

            public RevokeTokenCommandHandler(IAuthService authService, AuthBusinessRules authBusinessRules, IMapper mapper)
            {
                _authService = authService;
                _authBusinessRules = authBusinessRules;
                _mapper = mapper;
            }

            public async Task<RevokedTokenDto> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
            {
                RefreshToken? refreshToken = await _authService.GetRefreshTokenByToken(request.Token);
                await _authBusinessRules.RefreshTokenShouldBeExists(refreshToken);
                await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken);

                await _authService.RevokeRefreshToken(refreshToken, request.IPAddress, "Revoked without replacement");

                RevokedTokenDto revokedTokenDto = _mapper.Map<RevokedTokenDto>(refreshToken);
                return revokedTokenDto;
            }
        }
    }
}
