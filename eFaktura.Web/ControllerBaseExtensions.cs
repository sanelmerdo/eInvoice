using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace eFaktura.Web
{
    public static class ControllerBaseExtensions
    {
        /// <summary>
        /// Converts specified result object to <see cref="JsonResult"/> used to create an HTTP response.
        /// </summary>
        /// <typeparam name="T">The type of result to create.</typeparam>
        /// <param name="controller">Controller.</param>
        /// <param name="result">An instance of the result to return.</param>
        /// <returns><see cref="JsonResult"/>.</returns>
        public static JsonResult ToJsonResult<T>(this ControllerBase controller, T result)
        {
            return new JsonResult(result, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = "MM-dd-yyyy",
            });
        }
    }
}
