﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiAutores.DTOs;
using WebApiAutores.Servicios;

namespace WebApiAutores.Utilidades
{
    public class HATEOASAutorFilterAttribute:HATEOASFiltroAttribute
    {
        private readonly GeneradorDeEnlaces generadorDeEnlaces;

        public HATEOASAutorFilterAttribute(GeneradorDeEnlaces generadorDeEnlaces)
        {
            this.generadorDeEnlaces = generadorDeEnlaces;
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context,ResultExecutionDelegate next)
        {
            var debeIncluir = DebeIncluirHATEOAS(context);
                if (!debeIncluir)
            {
                await next();
                return;
            }
            var resultado = context.Result as ObjectResult;
            var modelo=resultado.Value as AutorDTO?? throw new  ArgumentNullException("Se esperaba una instancia de AutorDTO");
            await  generadorDeEnlaces.GenerarEnlaces(modelo);
            await next();
        }
    }
}
