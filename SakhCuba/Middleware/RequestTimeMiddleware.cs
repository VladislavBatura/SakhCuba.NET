using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakhCuba.Middleware
{
    public class RequestTimeMiddleware
    {
        public interface IHttpRequestTimeFeature
        {
            DateTime RequestTime { get; }
        }

        public class HttpRequestTimeFeature : IHttpRequestTimeFeature
        {
            public DateTime RequestTime { get; }

            public HttpRequestTimeFeature()
            {
                RequestTime = DateTime.Now;
            }
        }

        private readonly RequestDelegate _next;

        public RequestTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            var httpRequestTimeFeature = new HttpRequestTimeFeature();
            context.Features.Set<IHttpRequestTimeFeature>(httpRequestTimeFeature);

            return this._next(context);
        }
    }
}
