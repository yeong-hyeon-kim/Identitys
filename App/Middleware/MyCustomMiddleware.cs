namespace App.Middleware
{
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 미들웨어는 RequestDelegate 타입을 매개 변수로 받는 생성자가 필요합니다.
        /// </summary>
        /// <param name="next"></param>
        public MyCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Call the next delegate/middleware in the pipeline.
            // 다음 미들웨어 또는 델리게이트로 처리 순서를 넘깁니다.
            await _next(context);

        }
    }

    /// <summary>
    /// 미들웨어(Middleware) 호출 클래스에서 사용할 수 있도록 합니다.
    /// </summary>
    public static class MyCustomMiddlewareExtensions
    {
        /// <summary>
        /// IApplicationBuilder로 미들웨어(Middleware) 호출 클래스에 노출시킵니다.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>사용할 미들웨어</returns>
        public static IApplicationBuilder UseMyCustomMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
