using Microsoft.AspNetCore.Http.Extensions;
using SnapSoft.DataHandler;
using SnapSoft.Models;
using System.Text;

namespace SnapSoft.Middlewares
{
    public class HttpLogMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpLogMiddleware(RequestDelegate next) 
        { 
            _next = next; 
        }

        public async Task Invoke(HttpContext context, SnapSoftDataContext dbContext)
        {
            var originalBody = context.Response.Body;

            var request = await GetRequestAsTextAsync(context.Request);
            
           
            BaseModel req = new BaseModel()
            {
                Body = request,
                TimeStamp = DateTime.Now,
                Url = context.Request.GetDisplayUrl(),
                Method = context.Request.Method
            };

            dbContext.RequestsAndResponses.Add(req);
            await dbContext.SaveChangesAsync();


            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            var response = await GetResponseAsTextAsync(context.Response);
            BaseModel resp = new BaseModel()
            {
                Body = response,
                TimeStamp = DateTime.Now,
                RequestId = req.Id,
                Code = context.Response.StatusCode
            };

            dbContext.RequestsAndResponses.Add(resp);
            await dbContext.SaveChangesAsync();

            await responseBody.CopyToAsync(originalBody);
        }

        public async Task<string> GetRequestAsTextAsync(HttpRequest request)
        {
            request.EnableBuffering();

            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = await streamReader.ReadToEndAsync();

            request.Body.Position = 0;
            return requestBody;
        }

        public async Task<string> GetResponseAsTextAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }
    }
}
