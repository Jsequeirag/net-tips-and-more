using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;
using WebApiAutores.Utilidades;

namespace WebApiAutores.Controllers.V1

{
    [ApiController]
    [Route("api/autores")]
    //[Route("api/v1/autores")]
    [CabeceraEstaPresenteAtributte("x-version","1")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Policy ="EsAdmin")]
   // [ApiConventionType(typeof(DefaultApiConventions))]
    //alternativa
    //[Route("api/[controller]")]
    public class AutoresController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAuthorizationService authorizationService;

        public AutoresController(ApplicationDbContext context, IMapper mapper, IAuthorizationService authorizationService)
        {
            this.context = context;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
        }



        [HttpGet(Name = "obtenerAutoresv1")]
        [AllowAnonymous]
        [ServiceFilter(typeof(HATEOASAutorFilterAttribute))]
        public async Task<ActionResult<List<AutorDTO>>> Get([FromQuery]PaginacionDTO paginacionDTO)
        {
            var queryable = context.Autores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var autores=await queryable.OrderBy(autor=>autor.Nombre).Paginar(paginacionDTO).ToListAsync();
         
            return mapper.Map<List<AutorDTO>>(autores);



        }

        [HttpGet("{id:int}", Name = "obtenerAutorPorIdv1")]//---->? significa que el parametro es opcional
        [AllowAnonymous]
        [ServiceFilter(typeof(HATEOASAutorFilterAttribute))]
        // [HttpGet("{id:int}/{nombre=jose)]---> valor por defecto
        //[ProducesResponseType(404)]
        //[ProducesResponseType(200)]
        public async Task<ActionResult<AutorDTOConLibros>> getAutorById(int id)
        {
            var autor = await context.Autores.Include(autorDB => autorDB.AutoresLibros).ThenInclude(autorLibroDB => autorLibroDB.Libro).FirstOrDefaultAsync(autorDB => autorDB.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            var dto = mapper.Map<AutorDTOConLibros>(autor);


            return dto;
        }

        [HttpGet("{nombre}", Name = "obtenerAutorPorNombrev1")]
        public async Task<ActionResult<List<AutorDTO>>> getAutorByName(string nombre)
        {
            // var autor = await context.Autores.FirstOrDefaultAsync(autorDB => autorDB.Nombre.Contains(nombre));
            var autores = await context.Autores.Where(autorDB => autorDB.Nombre.Contains(nombre)).ToListAsync();

            if (autores == null)
                if (autores == null)
                {
                    return NotFound();
                }

            return mapper.Map<List<AutorDTO>>(autores);
        }
        /*
       From Route
        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> getAutorByNameRoute([FromRoute] string nombre)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Nombre == nombre);
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }
        //*From Body
        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> getAutorByNameBody([FromBody] string nombre)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Nombre == nombre);
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }
        //*From Headers
        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> getAutorByNameHeader([FromHeader] string nombre)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Nombre == nombre);
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }*/
        [HttpPost(Name = "crearAutov1")]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorCreacionDTO)
        {

            var existeAutorConElMismoNombre = await context.Autores.AnyAsync(x => x.Nombre == autorCreacionDTO.Nombre);
            if (existeAutorConElMismoNombre)
            {
                return BadRequest($"Ya existe un autor  con el nombre {autorCreacionDTO.Nombre}");
            }
            var autor = mapper.Map<Autor>(autorCreacionDTO);
            context.Add(autor);

            await context.SaveChangesAsync();
            var autorDTO = mapper.Map<AutorDTO>(autor);
            return CreatedAtRoute("ObtenerAutor", new { id = autor.Id }, autorDTO);
        }

        [HttpPut("{id:int}", Name = "actualizarAutorv1")]//api/autores/1
        public async Task<ActionResult> Put(AutorCreacionDTO autorCreacionDTO, int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            var autor = mapper.Map<Autor>(autorCreacionDTO);
            autor.Id = id;

            context.Update(autor);
            await context.SaveChangesAsync();
            return NoContent();
        }
        /// <summary>
        ///borra un autor 
        /// </summary>
        /// <param name="id">Id del autor</param>
        /// <returns></returns>
        [HttpDelete("{id:int}", Name = "borrarAutorv1")]//api//autores/2
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }



    }

}
