using Entity.dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WBL;

namespace WebApp.Pages.Institucion
{
    public class GridModel : PageModel
    {
        private readonly IInstitucionService _institucion;

        public GridModel(IInstitucionService institucion)
        {
            this._institucion = institucion;
        }

        public IEnumerable<InstitucionEntity> GridList { get; set; } = new List<InstitucionEntity>();

        public string Mensaje { get; set; } = "";



        public async Task<IActionResult> OnGet()
        {

            try
            {
                GridList = await _institucion.GET();


                if (TempData.ContainsKey("Msg"))
                {
                    Mensaje = TempData["Msg"] as string;
                }

                TempData.Clear();

                return Page();
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }

        }

        public async Task<IActionResult> OnGetEliminar(int id)
        {

            try
            {
                var result = await _institucion.DELETE(new()
                { Id_Institucion = id });

                if (result.CodeError != 0)
                {
                    throw new Exception(result.MsgError);
                }

                TempData["Msg"] = "El registro fue eliminado satisfactoriamente";

                return Redirect("Grid");
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }

        }
    }
}
