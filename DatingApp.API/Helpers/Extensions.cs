using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    // use static so that don't need create an instance to use this extension
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
        
    }
}