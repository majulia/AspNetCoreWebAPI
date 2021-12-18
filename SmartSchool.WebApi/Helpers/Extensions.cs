using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SmartSchool.WebApi.Helpers
{
    public static class Extensions
    {
        public static void AddPagination(
            this HttpResponse response,
            int currentPage,
            int itemsPage,
            int totalItems,
            int totalCount
        )
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPage, totalItems, totalCount);

            var camelCaseFormatter = new JsonSerializerSettings();

            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Header", "Pagination");
        }
    }
}
