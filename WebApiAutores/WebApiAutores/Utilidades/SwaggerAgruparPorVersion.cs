using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebApiAutores.Utilidades
{
    public class SwaggerAgruparPorVersion : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var namespaceControlador = controller.ControllerType.Namespace;
            var versionApi = namespaceControlador.Split(".").Last().ToLower();
            controller.ApiExplorer.GroupName = versionApi;
                }
    }
}
