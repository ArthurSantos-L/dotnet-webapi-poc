using System.Text.Json; 

namespace dotnet_webapi.Shared;

public class RequestContext
{
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
    public DateTime Inicio { get; set; } = DateTime.UtcNow;

    // Dados da Requisição
    public string Metodo { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? Usuario { get; set; }
    public Dictionary<string, string> Headers { get; set; } = new();

    // Dados da Resposta (Preenchidos no final)
    public int StatusCode { get; set; }
    public long TempoProcessamentoMs { get; set; }
    public string? ErroMensagem { get; set; } // Caso exploda

    // Método auxiliar para gerar o JSON do log
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
    }
}
