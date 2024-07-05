using System.Net;
using System.Text.Json;
using System.Text;
using TO.Core.Communication;
using TO.WebApp.MVC.Extensions;

namespace TO.WebApp.MVC.Services;

public abstract class Service
{
    protected StringContent ObterConteudo(object dado)
    {
        return new StringContent(
            JsonSerializer.Serialize(dado),
            Encoding.UTF8,
            "application/json");
    }

    protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }; 

        return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
    }

    protected bool TratarErrosResponse(HttpResponseMessage response)
    {
        switch (response.StatusCode)
        {
            case HttpStatusCode.Unauthorized:
            case HttpStatusCode.Forbidden:
            case HttpStatusCode.NotFound:
            case HttpStatusCode.InternalServerError:
                throw new CustomHttpRequestException(response.StatusCode);

            case HttpStatusCode.BadRequest:
                return false;
        }

        response.EnsureSuccessStatusCode();
        return true;
    }
    protected ResponseResult RetornoOk()
    {
        return new ResponseResult();
    }
}