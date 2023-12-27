using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiaAutores.Tests.PruebasUnitarias
{
    [TestClass]
    public class PrimeraLetraMayusculaAttributeTests
    {
        [TestMethod]
        public void PrimeraLetraMinuscula_DevuelveError()
        {
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            var valor = "Jose";
            var valContext=new ValidationContext(new {Nombre=valor});   
            var resultado=primeraLetraMayuscula.GetValidationResult(valor, valContext);
            Assert.AreEqual("La primera letra debe ser mayuscula", resultado.ErrorMessage);
        }
        [TestMethod]
        public void ValorNulo_DevuelveError()
        {
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            string valor = null;
            var valContext = new ValidationContext(new { Nombre = valor });
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);
            Assert.IsNull(resultado);
        }
        [TestMethod]
        public void PrimeraLetraMayuscula_NoDevuelveError()
        {
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            string valor = "Felipe";
            var valContext = new ValidationContext(new { Nombre = valor });
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);
            Assert.IsNull(resultado);
        }
    }
}