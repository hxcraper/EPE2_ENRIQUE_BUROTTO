using Microsoft.AspNetCore.Mvc;
using SERVICIO_DE_FACTURACION.Model;


namespace SERVICIO_DE_FACTURACION.Controllers
{
    [Route("empresa")]
    [ApiController]
    public class EmpresaController:ControllerBase
    {   
       
        public List<Empresa> empresas = new List<Empresa>()
        {
            new Empresa{NombreCliente="Enrique", ApellidoCliente="Burotto", EdadCliente=30,
                RutCliente="18464548-3", NombreEmpresa="FLESAN", RutEmpresa="16383543-7", GiroEmpresa="Construccion", TotalVentas=300f, MontoVentas=3000000f,
                MontoIva= 19f, MontoUtilidades=5000000f },
            new Empresa{NombreCliente="Andres", ApellidoCliente="Perez", EdadCliente=20,
                RutCliente="18632854-6", NombreEmpresa="DVC", RutEmpresa="16683843-1", GiroEmpresa="Demolición", TotalVentas=700f, MontoVentas=8000000f,
                MontoIva= 19f, MontoUtilidades=10000000f },
            new Empresa{NombreCliente="Felipe", ApellidoCliente="Zamudio", EdadCliente=40,
                RutCliente="19364572-6", NombreEmpresa="DIFAI", RutEmpresa="64752953-5", GiroEmpresa="Venta Departamentos", TotalVentas=300f, MontoVentas=8000000f,
                MontoIva= 19f, MontoUtilidades=200000f },

        };

        // MÉTODO GET QUE LISTE TODOS LOS DATOS PEDIDOS DE 03 EMPRESAS DIFERENTES
        [HttpGet]
        public IActionResult GetEmpresasDiferentes()
        {
            List<Empresa> empresasDiferentes = empresas
                .GroupBy(e => e.NombreEmpresa) // agrupa empresas
                .Select(group => group.First()) // selecciona la primera de cada grupo
                .ToList(); // convierte el resultado en una lista

            return Ok(empresasDiferentes); // se responde con el codigo 200 de ok
        }

        // MÉTODO GET QUE LISTE TODOS LOS DATOS DE UNA EMPRESA EN PARTICULAR
        [HttpGet("{nombreEmpresa}")]
        public IActionResult GetEmpresa(string nombreEmpresa)
        {   
            // Se busca una empresa con un nombre en especifico
            Empresa empresa = empresas.FirstOrDefault(e => e.NombreEmpresa == nombreEmpresa);

            if (empresa == null)
            {
                return NotFound(); // Muestra el codigo error 404
            }

            return Ok(empresa); // se responde con el codigo 200 de ok
        }

        // MÉTODO POST QUE PERMITA CREAR Y GUARDAR UNA NUEVA EMPRESA
        [HttpPost]
        public IActionResult CrearEmpresa([FromBody] Empresa nuevaEmpresa)
        {   
            // verificamos si el valor de la empresa es nulo
            if (nuevaEmpresa == null)
            {
                //  Muestra el codigo error 404
                return BadRequest();
            }
            // si todo esta ok, crea la nueva empresa
            empresas.Add(nuevaEmpresa);

            // nos da una url para acceder a la nueva empresa
            return CreatedAtAction("GetEmpresa", new { nombreEmpresa = nuevaEmpresa.NombreEmpresa }, nuevaEmpresa);
        }

        // MÉTODO PUT QUE PERMITA EDITAR Y GUARDAR CAMBIOS A UNA EMPRESA SELECCIONADA
        [HttpPut("{nombreEmpresa}")]
        public IActionResult EditarEmpresa(string nombreEmpresa, [FromBody] Empresa empresaActualizada)
        {   // verificamos si los datos son nulos
            if (empresaActualizada == null)
            {
                return BadRequest(); //  Muestra el codigo error 404
            }

            // busca empresa existente en la lista "empresas" cuyo campo "NombreEmpresa" coincida con el valor proporcionado

            Empresa empresaExistente = empresas.FirstOrDefault(e => e.NombreEmpresa == nombreEmpresa);

            // si no lo encuentra se devuelve con un error 404
            if (empresaExistente == null)
            {
                return NotFound(); 
            }

            // Actualiza los datos de la empresa con los nuevos valores
            empresaExistente.NombreCliente = empresaActualizada.NombreCliente;
            empresaExistente.ApellidoCliente = empresaActualizada.ApellidoCliente;
            empresaExistente.EdadCliente = empresaActualizada.EdadCliente;
            empresaExistente.RutCliente = empresaActualizada.RutCliente;
            empresaExistente.RutEmpresa = empresaActualizada.RutEmpresa;
            empresaExistente.GiroEmpresa = empresaActualizada.GiroEmpresa;
            empresaExistente.TotalVentas = empresaActualizada.TotalVentas;
            empresaExistente.MontoVentas = empresaActualizada.MontoVentas;
            empresaExistente.MontoIva = empresaActualizada.MontoIva;
            empresaExistente.MontoUtilidades = empresaActualizada.MontoUtilidades;

            return Ok(empresaExistente);
        }

        // MÉTODO DELETE QUE PERMITA ELIMINAR UNA EMPRESA CREADA
        [HttpDelete("{nombreEmpresa}")]
        public IActionResult EliminarEmpresa(string nombreEmpresa)
        {
            //busca empresa en la lista "empresas" cuyo campo "NombreEmpresa" coincida con el valor proporcionado

            Empresa empresaExistente = empresas.FirstOrDefault(e => e.NombreEmpresa == nombreEmpresa);

            if (empresaExistente == null)
            {
                return NotFound(); // error 404
            }
            // si la encuentra , la elimina de la lista 
            empresas.Remove(empresaExistente);

            return NoContent(); // error 204 
        }



    }
}
