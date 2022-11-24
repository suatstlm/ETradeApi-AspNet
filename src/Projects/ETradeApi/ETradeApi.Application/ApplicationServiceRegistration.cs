﻿using ETradeApi.Application.Services.AuthService;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ETradeApi.Application.Features.Auths.Rules;
using ETradeApi.Application.Services.AuthServices;
using System.Reflection;
using ETradeApi.Application.Features.Products.Rules;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.Application.Pipelines.Authorization;
using ETradeApi.Application.Services.UserService;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using Core.ElasticSearch;

namespace ETradeApi.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<AuthBusinessRules>();
            services.AddScoped<ProductBusinessRules>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IUserService, UserManager>();

            services.AddSingleton<IMailService, MailKitMailService>();
            services.AddSingleton<LoggerServiceBase, FileLogger>();
            services.AddSingleton<IElasticSearch, ElasticSearchManager>(); ;

            return services;
        }
    }
}
