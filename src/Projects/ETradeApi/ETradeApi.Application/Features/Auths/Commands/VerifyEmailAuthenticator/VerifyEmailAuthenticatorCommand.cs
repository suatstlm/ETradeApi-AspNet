﻿using Core.Security.Entities;
using ETradeApi.Application.Features.Auths.Rules;
using ETradeApi.Application.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.Features.Auths.Commands.VerifyEmailAuthenticator
{
    public class VerifyEmailAuthenticatorCommand : IRequest
    {
        public string ActivationKey { get; set; }

        public class VerifyEmailAuthenticatorCommandHandler : IRequestHandler<VerifyEmailAuthenticatorCommand>
        {
            private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
            private readonly AuthBusinessRules _authBusinessRules;

            public VerifyEmailAuthenticatorCommandHandler(IEmailAuthenticatorRepository emailAuthenticatorRepository,
                                                          AuthBusinessRules authBusinessRules)
            {
                _emailAuthenticatorRepository = emailAuthenticatorRepository;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<Unit> Handle(VerifyEmailAuthenticatorCommand request, CancellationToken cancellationToken)
            {
                EmailAuthenticator? emailAuthenticator =
                    await _emailAuthenticatorRepository.GetAsync(
                        e => e.ActivationKey == request.ActivationKey);
                await _authBusinessRules.EmailAuthenticatorShouldBeExists(emailAuthenticator);
                await _authBusinessRules.EmailAuthenticatorActivationKeyShouldBeExists(emailAuthenticator);

                emailAuthenticator.ActivationKey = null;
                emailAuthenticator.IsVerified = true;
                await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);

                return Unit.Value;
            }
        }
    }
}
