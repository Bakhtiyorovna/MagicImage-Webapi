﻿using Microsoft.Net.Http.Headers;

namespace Identity_Provider.WebApi.Configurations
{
    public static class CorsPolicyConfiguration
    {
        public static void ConfigureCORSPolicy(this WebApplicationBuilder builder)
        {
            var allowOrigins = "AllowOrigins";
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
                option.AddPolicy("AllowHeaders", builder =>
                {
                    builder.WithOrigins(allowOrigins)
                            .WithHeaders(HeaderNames.ContentType, HeaderNames.Server, HeaderNames.AccessControlAllowHeaders, HeaderNames.AccessControlExposeHeaders, "x-custom-header", "x-path", "x-record-in-use", HeaderNames.ContentDisposition);
                });
            });
        } 
    }
}
