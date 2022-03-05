using Entity.dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using WBL;

namespace WebApp.Pages.Institucion
{
    public class EditModel : PageModel
    {
        private readonly IInstitucionService _institucion;
        public InstitucionEntity institucionEntity { get; set; }

        public EditModel(IInstitucionService institucion)
        {
            this._institucion = institucion;
        }

        public async Task<IActionResult> OnGet(int id)
        {

            try
            {

                InstitucionEntity entity = new InstitucionEntity
                {
                    Id_Institucion = id
                };

                institucionEntity = await _institucion.GETBYID(entity);

                return Page();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);

            }
        }

        public async Task<IActionResult> OnPost(InstitucionEntity entity)
        {


            if (entity.Estado == true)
            {
                entity.Estado = true;
            }
            else
            {
                entity.Estado = false;
            }


            try
            {
                var result = await _institucion.UPDATE(entity);

                if (result.CodeError != 0)
                {
                    throw new Exception(result.MsgError);

                }

                TempData["Msg"] = "El registro fue editado correctamente";

                return Redirect("Grid");
            }
            catch (Exception ex)
            {

                return Content(ex.Message);

            }
        }
    }
}
