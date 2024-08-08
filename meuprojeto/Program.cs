using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Frozen", "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapGet("/usuarios", () =>
{
    MySqlConnection connection = new("server=localhost;database=folhadepagamento;user=root;password=123@456@789");
    connection.Open();
    var cmd = connection.CreateCommand();
    cmd.CommandText = "SELECT * FROM usuarios";
    var reader = cmd.ExecuteReader();
    var usuarios = new List<Usuario>();
    //var usuarios = new List<Usuario>();
    while (reader.Read())
    {
        usuarios.Add(new Usuario
        {
            nome = reader.GetString("nome"),
            end = reader.GetString("end"),
            cpf = reader.GetString("cpf"),
            rg = reader.GetString("rg")
        });
        /*usuarios.Add(new Usuario
        {
            Id = reader.GetInt32("id"),
Nome = reader.GetString("nome"),
            Email = reader.GetString("email")
        });*/
    }
    return usuarios;
});

app.MapGet("/mensagens", () =>
{
    MySqlConnection connection = new("server=localhost;database=folhadepagamento;user=root;password=123@456@789");
    connection.Open();
    var cmd = connection.CreateCommand();
    cmd.CommandText = "SELECT * FROM mensagens";
    var reader = cmd.ExecuteReader();
    var mensagens = new List<Mensagem>();
    //var usuarios = new List<Usuario>();
    while (reader.Read())
    {
        mensagens.Add(new Mensagem
        {
            email_usuario = reader.GetString("email_usuario"),
            enviado_recebido = reader.GetString("enviado_recebido"),
            mensagem = reader.GetString("mensagem"),
            data_registro = reader.IsDBNull(4) ? DateTime.Now : reader.GetDateTime("data_registro")
        });
        /*usuarios.Add(new Usuario
        {
            Id = reader.GetInt32("id"),
Nome = reader.GetString("nome"),
            Email = reader.GetString("email")
        });*/
    }
    return mensagens;
});

app.MapGet("/acoes_usuarios", () =>
{
    MySqlConnection connection = new("server=localhost;database=folhadepagamento;user=root;password=123@456@789");
    connection.Open();
    var cmd = connection.CreateCommand();
    cmd.CommandText = "SELECT * FROM acoes_usuarios";
    var reader = cmd.ExecuteReader();
    var mensagens = new List<acoes_usuario>();
    //var usuarios = new List<Usuario>();
    while (reader.Read())
    {
        mensagens.Add(new acoes_usuario
        {
            email_usuario = reader.GetString("email_usuario"),
            acao_usuarios = reader.GetString("acao_usuarios"),
            enviado_recebido = reader.GetString("enviado_recebido"),
            data_hora_acao = reader.IsDBNull(3) ? DateTime.Now : reader.GetDateTime("data_hora_acao")
        });

    }
    return mensagens;
});


app.Run();
class Usuario
{
    public string nome { get; set; }
    public string cpf { get; set; }
    public string rg { get; set; }
    public string end { get; set; }


}


class Mensagem
{
    public string email_usuario { get; set; }
    public string enviado_recebido { get; set; }
    public string mensagem { get; set; }
    public DateTime? data_registro { get; set; }


}

class acoes_usuario
{
    public string email_usuario { get; set; }
    public string acao_usuarios { get; set; }

    public string enviado_recebido { get; set; }
    public DateTime? data_hora_acao { get; set; }


}