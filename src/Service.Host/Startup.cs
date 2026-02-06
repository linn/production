namespace Linn.Production.Service.Host
{
    using System.IdentityModel.Tokens.Jwt;
    using Amazon;
    using Amazon.KeyManagementService;
    using Amazon.S3;
    using AspNetCore.DataProtection.Aws.Kms;
    using AspNetCore.DataProtection.Aws.S3;
    using Linn.Common.Authentication.Host.Extensions;
    using Linn.Common.Configuration;
    using Linn.Production.Service.Models;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.AspNetCore.Hosting;
    
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Nancy;
    using Nancy.Owin;

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var keysBucketName = ConfigurationManager.Configuration["KEYS_BUCKET_NAME"];
            var kmsKeyAlias = ConfigurationManager.Configuration["KMS_KEY_ALIAS"];

            services.TryAddSingleton<IAmazonS3>(new AmazonS3Client(new AmazonS3Config
                { RegionEndpoint = RegionEndpoint.EUWest1 }));
            services.TryAddSingleton<IAmazonKeyManagementService>(new AmazonKeyManagementServiceClient(
                new AmazonKeyManagementServiceConfig
                {
                    RegionEndpoint = RegionEndpoint.EUWest1
                }));
            services.AddDataProtection()
                .SetApplicationName("auth-oidc")
                .PersistKeysToAwsS3(new S3XmlRepositoryConfig(keysBucketName))
                .ProtectKeysWithAwsKms(new KmsXmlEncryptorConfig(kmsKeyAlias) { DiscriminatorAsContext = true });


            var appSettings = ApplicationSettings.Get();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = appSettings.CognitoHost;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = appSettings.CognitoHost,
                        ValidateAudience = false,
                        ValidAudience = appSettings.CognitoClientId,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            app.UseBearerTokenAuthentication();

            app.UseOwin(x => x.UseNancy(config =>
            {
                config.PassThroughWhenStatusCodesAre(HttpStatusCode.Unauthorized, HttpStatusCode.Forbidden);
            }));

            app.Use((context, next) => context.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme));
        }
    }
}
