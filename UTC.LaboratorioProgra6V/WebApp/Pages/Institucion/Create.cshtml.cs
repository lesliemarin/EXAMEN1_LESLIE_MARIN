using Entity.dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using WBL;

namespace WebApp.Pages.Institucion
{
    public class CreateModel : PageModel
    {
        private readonly IInstitucionService _institucion;

        public CreateModel(IInstitucionService institucion)
        {
            this._institucion = institucion;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(InstitucionEntity entity)
        {

            try
            {
                var result = await _institucion.CREATE(entity);

                if (result.CodeError != 0)
                {
                    throw new Exception(result.MsgError);

                }

                TempData["Msg"] = "El registro fue agregado correctamente";

                return Redirect("Grid");
            }
            catch (Exception ex)
            {

                return Content(ex.Message);

            }
        }
    }
}
