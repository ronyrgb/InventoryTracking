using Backend.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Text.Json;
using Xunit;

namespace Backend.Tests.Middleware
{
    public class GlobalExceptionMiddlewareTests
    {
        private readonly Mock<ILogger<GlobalExceptionMiddleware>> _loggerMock;

        public GlobalExceptionMiddlewareTests()
        {
            _loggerMock = new Mock<ILogger<GlobalExceptionMiddleware>>();
        }

        private static DefaultHttpContext CreateContext()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            return context;
        }

        [Fact]
       public async Task Returns_500_When_Generic_Exception()
        {
            var middleware = new GlobalExceptionMiddleware(
                _ => throw new Exception("erro"),
                _loggerMock.Object);
 
            var context = CreateContext();

            await middleware.InvokeAsync(context);

            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        }

        [Fact]
        public async Task Returns_404_When_KeyNotFound()
        {
            var middleware = new GlobalExceptionMiddleware(
                _ => throw new KeyNotFoundException("não encontrado"),
                _loggerMock.Object);

            var context = CreateContext();

            await middleware.InvokeAsync(context);

            Assert.Equal((int)HttpStatusCode.NotFound, context.Response.StatusCode);
        }

        [Fact]
        public async Task Returns_400_When_InvalidOperation()
        {
            var middleware = new GlobalExceptionMiddleware(
                _ => throw new InvalidOperationException("inválido"),
                _loggerMock.Object);

            var context = CreateContext();

            await middleware.InvokeAsync(context);

            Assert.Equal((int)HttpStatusCode.BadRequest, context.Response.StatusCode);
        }
    }
}
