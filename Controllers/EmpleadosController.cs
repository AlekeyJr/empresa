using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using pr_web_api_empresa.Models;
namespace pr_web_api_empresa.Controllers{
[Route("api/[controller]")]
    public class EmpleadosController : Controller{
        private Conexion dbConexion;
        public EmpleadosController(){
            dbConexion = Conectar.Create();
        }
        [HttpGet]
        public ActionResult Get(){
            return Ok(dbConexion.Empleado.ToArray());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id){
            var empleados = await dbConexion.Empleado.FindAsync(id);
            if (empleados !=null){
                return Ok(empleados); 
            }else{
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Empleado empleados){

            if (ModelState.IsValid){
                dbConexion.Empleado.Add(empleados);
                await dbConexion.SaveChangesAsync();
                return Ok(empleados);
            }else{
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody]Empleado empleados){
            var v_empleados = dbConexion.Empleado.SingleOrDefault(a => a.id_empleado == empleados.id_empleado);
            if(v_empleados !=null && ModelState.IsValid){
                dbConexion.Entry(v_empleados).CurrentValues.SetValues(empleados);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }else{
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id){
            var empleados = dbConexion.Empleado.SingleOrDefault(a => a.id_empleado == id);
            if (empleados !=null){
                dbConexion.Empleado.Remove(empleados);
                await dbConexion.SaveChangesAsync();
                 return Ok();
            }else{
                return NotFound();
            }

        }
    }

}