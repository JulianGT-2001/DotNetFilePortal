namespace WebApi.Middleware
{
    public class GatewayHeaderMiddleware
    {
        #region Atributos
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        public GatewayHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region Metodos
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-Forwarded-From", out var forwardedFrom) || forwardedFrom != "Gateway-Ocelot")
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access denied: request must come through the Gateway.");
                return;
            }

            await _next(context);
        }
        #endregion
    }
}