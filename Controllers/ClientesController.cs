using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using web_api_1.Models;

namespace web_api_1.Controllers{

    [Route("api/[controller]")]
    public class ClientesController : Controller{
        private Conexion dbConexion;
        public ClientesController(){
            dbConexion = Conectar.Create();
        }
        //Metodo Mostrar
        [HttpGet]
        public ActionResult Get(){
            return Ok(dbConexion.Clientes.ToArray());
        } 
        
        //Metodo Buscar
        [HttpGet("{id}")] 
        public async Task<ActionResult>Get(int id){
            var clientes = await dbConexion.Clientes.FindAsync(id);
            if (clientes !=null){
                return Ok(clientes);
            }else{
                return NotFound();
            }
        }

        //Metodo Agregar
        [HttpPost] 
        public async Task<ActionResult> Post([FromBody] Clientes clientes){
            if (ModelState.IsValid){
                dbConexion.Clientes.Add(clientes);
                await dbConexion.SaveChangesAsync ();
                return Ok();    
            }else {
                return BadRequest();
            }
        }

        //Metodo Modificar
        public async Task<ActionResult> Put([FromBody] Clientes clientes){
            var v_clientes = dbConexion.Clientes.SingleOrDefault(a => a.id_clientes == clientes.id_clientes);
            if (v_clientes != null && ModelState.IsValid){
                dbConexion.Entry(v_clientes).CurrentValues.SetValues(clientes);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }else {
                return BadRequest();
            }
        }

        //Metodo Eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult>Delete(int id){
            var clientes = dbConexion.Clientes.SingleOrDefault(a => a.id_clientes== id);
            if (clientes!=null){
                dbConexion.Clientes.Remove(clientes);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }else{
                return BadRequest();
            }
        }
    }
}