using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

public class GraphQLService
{
    private readonly GraphQLHttpClient _client;

    public GraphQLService()
    {
        var options = new GraphQLHttpClientOptions
        {
            EndPoint = new Uri("http://localhost:7268/api/graphql")
        };

        _client = new GraphQLHttpClient(options, new SystemTextJsonSerializer());
    }

    public async Task<IEnumerable<CourseModel>> GetCourses()
    {
        var query = new GraphQLRequest
        {
            Query = @"
            query {
                getCourses {
                    id
                    isBestSeller
                    image
                    title
                    author
                    price
                    discountPrice
                    hours
                    likesInProcent
                    likesInNumbers
                }
            }"
        };

        var response = await _client.SendQueryAsync<ResponseWrapper>(query);
        return response.Data.GetCourses;
    }

    private class ResponseWrapper
    {
        public IEnumerable<CourseModel> GetCourses { get; set; }
    }
}

public class CourseModel
{
    public int Id { get; set; }
    public bool IsBestSeller { get; set; }
    public string Image { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Price { get; set; } = null!;
    public string? DiscountPrice { get; set; }
    public string Hours { get; set; } = null!;
    public string LikesInProcent { get; set; } = null!;
    public string LikesInNumbers { get; set; } = null!;
}