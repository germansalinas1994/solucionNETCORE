 using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorCrud.Cliente;
//AGREGO LA REFERENCIA a los services
using BlazorCrud.Cliente.Services;
using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


//Trabajamos con http client y por defecto le pasa una url base
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//necesitamos pasarle la base de nuestra base, la api que implementamos
//para esto, inicio el proyecto desde la capa server y obtengo la url, recordar que la url es distinta a la de la capa de cliente
//en este caso la url donde se ejecuta el servidor es http://localhost:5016

//Le instancio al http client la url de nuestro server que es 5016
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:3000") });

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5016") });

//AGREGO LAS INTERFACES Y SUS IMPLEMENTACIONES
builder.Services.AddScoped<IDepartamentosService, DepartamentoService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddSweetAlert2();


await builder.Build().RunAsync();

