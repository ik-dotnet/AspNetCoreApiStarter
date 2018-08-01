using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using CodeStream.logDNA;
using CodeStresmAspNetCoreApiStarter.Infrastructure.MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace CodeStresmAspNetCoreApiStarter.Infrastructure
{
    public static class GlobalExceptionHandler
    {
        public static Action<IApplicationBuilder> Handler(ILogDNALogger logger) => errorApp =>
        {
            errorApp.Run(async context =>
            {
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = errorFeature.Error;

                // the IsTrusted() extension method doesn't exist and
                // you should implement your own as you may want to interpret it differently
                // i.e. based on the current principal
                var correlationId = Guid.NewGuid().ToString();

                var problemDetails = new ProblemDetails
                {
                    //TODO: fill in organization name below such as: $"urn:MYORGANIZATION:error:{correlationId}"
                    Instance = $"urn:error:{correlationId}"
                };

                if (exception is BadHttpRequestException badHttpRequestException)
                {
                    problemDetails.Title = "Invalid request";
                    problemDetails.Status = (int)typeof(BadHttpRequestException).GetProperty("StatusCode",
                        BindingFlags.NonPublic | BindingFlags.Instance).GetValue(badHttpRequestException);
                    problemDetails.Detail = badHttpRequestException.Message;
                }
                else
                {
                    problemDetails.Title = "An unexpected error occurred!";
                    problemDetails.Status = 500;
                    problemDetails.Detail = exception.Demystify().ToString();
                }

                // log the exception etc..
                if (!(exception is UnhandledEventingPipelineException))
                {
                    //TODO: figure out how to get request info and request content in here
                    logger.LogError(exception, correlationId);
                }

                context.Response.StatusCode = problemDetails.Status.Value;
                context.Response.WriteJson(problemDetails, "application/problem+json");
            });
        };
    }

    public static class HttpExtensions
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public static void WriteJson<T>(this HttpResponse response, T obj, string contentType = null)
        {
            response.ContentType = contentType ?? "application/json";
            using (var writer = new HttpResponseStreamWriter(response.Body, Encoding.UTF8))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.CloseOutput = false;
                    jsonWriter.AutoCompleteOnClose = false;

                    Serializer.Serialize(jsonWriter, obj);
                }
            }
        }
    }
}