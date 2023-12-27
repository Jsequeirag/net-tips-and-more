using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiaAutores.Tests.Mocks;
using WebApiAutores.Controllers.V1;

namespace WebApiaAutores.Tests.PruebasUnitarias
{
    [TestClass]
    public class RootControllerTests
    {
        [TestMethod]
        public async Task SiUsuarioEsAdmin_Obtenemos4Links()
        {
            //preparacion
            var authorizationService = new AuthorizationServiceMock();
            authorizationService.Resultado = AuthorizationResult.Success();
            var rootController = new RootController(authorizationService);
            rootController.Url = new URLHelperMock();

            //Ejecucion
            var resultado = await rootController.Get();

            //Verificacion
            Assert.AreEqual(4,resultado.Value.Count());
        }
        [TestMethod]
        public async Task SiUsuarioNoAdmin_Obtenemos2Links()
        {
            //preparacion
            var authorizationService = new AuthorizationServiceMock();
            authorizationService.Resultado = AuthorizationResult.Failed();
            var rootController = new RootController(authorizationService);
            rootController.Url = new URLHelperMock();

            //Ejecucion
            var resultado = await rootController.Get();

            //Verificacion
            Assert.AreEqual(2, resultado.Value.Count());
        }
        [TestMethod]
        public async Task SiUsuarioNoAdmin_Obtenemos2Links_UsandoMoq()
        {
            //preparacion
            var mockAuthorizationSeervice = new Mock<IAuthorizationService>();
            mockAuthorizationSeervice.Setup(x => x.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(),It.IsAny<object>(),It.IsAny<IEnumerable<IAuthorizationRequirement>>())).Returns(Task.FromResult(AuthorizationResult.Failed()));

            mockAuthorizationSeervice.Setup(x => x.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<object>(), It.IsAny<string>())).Returns(Task.FromResult(AuthorizationResult.Failed()));

            var mockURLHelper=new Mock<IUrlHelper>();
            mockURLHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns(string.Empty);
            
            var rootController = new RootController(mockAuthorizationSeervice.Object);
            rootController.Url = new URLHelperMock();

            //Ejecucion
            var resultado = await rootController.Get();

            //Verificacion
            Assert.AreEqual(2, resultado.Value.Count());
        }


    }
}
