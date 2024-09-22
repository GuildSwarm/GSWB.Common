﻿using Common.Presentation.Validation;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using TGF.CA.Application;

namespace Common.Presentation
{
    /// <summary>
    /// Provides methods for configuring and using the common infrastructure components shared across several web application projects in this solution.
    /// </summary>
    public static class CommonPresentationBootstrapper
    {
        /// <summary>
        /// Configures the necessary common persentation services for the application shared across several web application projects in this solution.
        /// </summary>
        public static WebApplicationBuilder ConfigureCommonPresentation(this WebApplicationBuilder aWebApplicationBuilder)
        {
            aWebApplicationBuilder.Services.AddValidatorsFromAssemblyContaining<PaginationValidator>();
            return aWebApplicationBuilder;
        }

        //declare here more common use middlewares, but be careful declaring common middleware use order becauyse the order matters! and it may not be a common order that suits all the applications of this project.
        public static void UseCommonPresentation(this WebApplication aWebApplication)
        {
            if (aWebApplication.Environment.IsDevelopment() || aWebApplication.Environment.IsStaging() || aWebApplication.Environment.IsEnvironment("testportal"))
            {
                aWebApplication.UseSwagger();
                aWebApplication.UseSwaggerUI();
            }
            aWebApplication.MapHealthChecks(TGFEndpointRoutes.health, new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }
    }
}
