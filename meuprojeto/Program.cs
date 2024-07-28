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


app.Run();
class Usuario
{
    public string nome { get; set; }
    public string cpf { get; set; }
    public string rg { get; set; }
    public string end { get; set; }


}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
