using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using ApiCrudAWSSeries.Repositories;
using ApiCrudAWSSeries.Models;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ApiCrudAWSSeries;

public class Functions
{
    private RepositorySeries repo;
    /// <summary>
    /// Default constructor that Lambda will invoke.
    /// </summary>
    public Functions(RepositorySeries repo)
    {
        this.repo = repo;
    }


    /// <summary>
    /// A Lambda function to respond to HTTP Get methods from API Gateway
    /// </summary>
    /// <remarks>
    /// This uses the <see href="https://github.com/aws/aws-lambda-dotnet/blob/master/Libraries/src/Amazon.Lambda.Annotations/README.md">Lambda Annotations</see> 
    /// programming model to bridge the gap between the Lambda programming model and a more idiomatic .NET model.
    /// 
    /// This automatically handles reading parameters from an APIGatewayProxyRequest
    /// as well as syncing the function definitions to serverless.template each time you build.
    /// 
    /// If you do not wish to use this model and need to manipulate the API Gateway 
    /// objects directly, see the accompanying Readme.md for instructions.
    /// </remarks>
    /// <param name="context">Information about the invocation, function, and execution environment</param>
    /// <returns>The response as an implicit <see cref="APIGatewayProxyResponse"/></returns>
    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/")]
    public async Task<IHttpResult> Get(ILambdaContext context)
    {
        List<Serie> series = await repo.GetSeriesAsync();
        return HttpResults.Ok(series);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/find/{id}")]
    public async Task<IHttpResult> Find(int id, ILambdaContext context)
    {
        Serie s = await repo.GetSerieById(id);
        return HttpResults.Ok(s);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Post, "/post")]
    public async Task<IHttpResult> Post([FromBody]Serie serie, ILambdaContext context)
    {
        await this.repo.CreateSerieAsync(serie.Nombre, serie.Imagen, serie.Anyo);
        return HttpResults.Ok();
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Put, "/put/{id}")]
    public async Task<IHttpResult> Put(int id, [FromBody] Serie serie, ILambdaContext context)
    {
        await this.repo.UpdateSerieAsync(id, serie.Nombre, serie.Imagen, serie.Anyo);
        return HttpResults.Ok();
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Delete, "/delete/{id}")]
    public async Task<IHttpResult> Delete(int id)
    {
        await this.repo.DeleteSerieAsync(id);
        return HttpResults.Ok();
    }

}
