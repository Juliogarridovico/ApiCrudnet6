using Microsoft.AspNetCore.Mvc;
using NetCoreYouTube.Models;
using Newtonsoft.Json;

namespace NetCoreYouTube.Controllers

{

    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route("listar/{id}")]
        public IActionResult ListarCliente(int id)
        {
            string path = $"jsons/{id}.json";

            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            string jsonString = System.IO.File.ReadAllText(path);
            dynamic jsonResultado = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

            return Ok(new
            {
                success = true,
                message = "conseguido",
                result = jsonResultado
            });
        }






        [HttpPost]
        [Route("crearjson")]
        public dynamic CrearJson(Cliente cliente)
        {
            // Convertir el objeto cliente a JSON
            var json = JsonConvert.SerializeObject(cliente);

            // Especificar la ruta del archivo donde deseas guardar el JSON
            var rutaArchivo = "jsons/" + cliente.Id + ".json";
            Console.WriteLine(cliente.Id);
            // Guardar el JSON en el archivo
            System.IO.File.WriteAllText(rutaArchivo, json);

            return new
            {
                success = true,
                message = "consegIdo",
                result = cliente
            };
        }
        [HttpDelete]
        [Route("borrar/{id}")]
        public IActionResult BorrarCliente(int id)
        {
            string path = $"jsons/{id}.json";

            // Verificando si el archivo existe
            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            // Borrar el archivo JSON
            System.IO.File.Delete(path);

            return Ok(new
            {
                success = true,
                message = "Cliente borrado exitosamente",
            });
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarCliente(int id, [FromBody] Cliente cliente)
        {
            string path = $"jsons/{id}.json";

            // Verificando si el archivo existe
            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            // Actualizar el archivo JSON con la nueva información del cliente
            string jsonString = JsonConvert.SerializeObject(cliente);
            System.IO.File.WriteAllText(path, jsonString);

            return Ok(new
            {
                success = true,
                message = "Cliente actualizado exitosamente",
                result = cliente
            });
        }
    }
}
