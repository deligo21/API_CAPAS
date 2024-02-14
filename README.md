- Crear solucion en blanco
- Crear una aplicacion Web API
- Crear un proyecto de biblioteca de clases para Capa de Datos
- Agregar referencias: API a Negocios y Negocios a Datos
- En la capa datos instalar: entityframeworkcore, entityframeworkcore.sqlserver, entityframeworkcore.tools, entityframeworkcore.design
- En la capa de datos mediante consola insertar el siguiente comando:
```
dotnet ef dbcontext scaffold "Server=DELIGO21-PC;Database=CRUD_CAPAS;Integrated Security=true;Encrypt=false;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer
```
- En el contexto de la base de datos modificar de la siguiente forma:
```
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { 
    base.OnConfiguring(optionsBuilder);
}
```
- En la capa de negocio crear las interfaces con los metodos que se van a exponer, para eso se crea la carpeta Interfaces, con una interfaz IContacto y cambiarle a publica:
```
namespace Layer_Negocio.Interfaces
{
    public interface IContacto
    {
        List<Contacto> GetContactos();
    }
}

```
- Crear la clase que implemente la interfaz Clases/LogicaContacto
```
public class LogicaContacto: IContacto
{
    private readonly CrudCapasContext _db_context;
    public LogicaContacto(CrudCapasContext db_context)
    {
        this._db_context = db_context;
    }

    public List<Contacto> GetContactos()
    {
        return _db_context.Contactos.ToList();
    }
}
```
- Introducir el ConecctionString de la API:
```
"ConnectionStrings": {
  "DeffaultConexion": "Server=DELIGO21-PC;Database=CRUD_CAPAS;Integrated Security=true;Encrypt=false;TrustServerCertificate=True"
},
```
- Agregar lo siguiente en el Program.cs
```
// Configurar el acceso a datos
var conn = builder.Configuration.GetConnectionString("DeffaultConexion"); //crea una variable con la cadena de conexion
builder.Services.AddDbContext<CrudCapasContext>(x => x.UseSqlServer(conn)); //construye el contexto

// Configurar las interfaces para que el controlador las pueda usar
builder.Services.AddScoped<IContacto, LogicaContacto>();
```
- Agregar un controlador API Controller with read/write actions y añadir lo siguiente:
```
private readonly IContacto _contacto;

public ContactoController(IContacto contacto)
{
    this._contacto = contacto;
}

// GET: api/<ContactoController>
[HttpGet]
public IEnumerable<Contacto> Get()
{
    return _contacto.GetContactos();
}
```
- En caso de ser necesario modificar en API lo siguiente:
```
<InvariantGlobalization>false</InvariantGlobalization>
```
# Consumir la web API
- Agregar un proyecto MVC ASP NET Core
- En el Program.cs del MVC agregar lo siguiente:
```
builder.Services.AddHttpClient();
```
- Agregar en Models del MVC VMContacto
```
public class VMContacto
{
    public int IdContacto { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? FechaNacimiento { get; set; }

}
```
- Instalar el paquete Newtonsoft.json
- Luego en Controladores crear un controlador MVC en blanco: Contacto Controller
```
public class ContactoController : Controller
{
    private readonly HttpClient _httpClient;
    public ContactoController(IHttpClientFactory httpClientFactory)
    {
        this._httpClient = httpClientFactory.CreateClient();
        this._httpClient.BaseAddress = new Uri("http://localhost:5198/api");
    }
    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("api/contacto");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var contactos = JsonConvert.DeserializeObject<IEnumerable<VMContacto>>(content);

            return View("Index", contactos);
        }

        return View(new List<VMContacto>());
    }
}
```
- Agregar la vista al Index, Insertar y todos los metodos
- Configurar el inicio del proyecto para que puedan ser varios
