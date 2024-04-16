using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NoSQL_Proyecto.Models;
using NoSQL_Proyecto.Services;

namespace NoSQL_Proyecto.Controllers
{
    public class MovimientosController : Controller
    {

        private readonly IngresosService _ingresosService;

        private readonly SalidasService _salidaService;



        public MovimientosController(IngresosService ingresosService, SalidasService salidaService) {

            _ingresosService = ingresosService;

            _salidaService = salidaService;


        }

        /*
            APARTADO DE INGRESOS            

         */

        public IActionResult Ingresos()
        {

            var ingresos = _ingresosService.GetAsync("Ingresos").Result;

            List<IngresosViewModel> ingresoViewModels = new List<IngresosViewModel>();

            foreach (var ingreso in ingresos)
            {
                // Crear una nueva instancia de IngresosViewModel y asignar precio y descripción
                var ingresoViewModel = new IngresosViewModel
                {
                    Cantidad_Ingresada = ingreso.Cantidad_Ingresada,
                    Precio_Compra = ingreso.Precio_Compra
                };
                ingresoViewModels.Add(ingresoViewModel);
            }

            ViewBag.Ingresos = ingresoViewModels;
            return View();
        }

        // POST CREATE
        [HttpPost]
        public async Task<IActionResult> Ingresos(IngresosViewModel model)
        {
            // Verificar si el modelo es válido
            if (ModelState.IsValid)
            {
                // Crear un nuevo salida
                var nuevoIngreso = new Ingresos
                {
                    Fecha_Ingreso = model.Fecha_Ingreso,
                    id_Articulo = ObjectId.Parse("660dc3e881c5953b240d5f51"),
                    Cantidad_Ingresada = model.Cantidad_Ingresada,
                    Precio_Compra = model.Precio_Compra,
                    id_Proveedor = ObjectId.Parse("660dc3e881c5953b240d5f51"),

                    // Asigna otros campos de usuario según sea necesario
                };

                // Intenta crear el salida
                try
                {
                    await _ingresosService.CreateAsync("Ingresos", nuevoIngreso);

                    // Si el salida se crea con éxito, configurar un mensaje de éxito en ViewBag
                    ViewBag.Success = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al registrar la salida: " + ex.Message);
                }
            }

            // Si hay errores de validación o si el usuario no se creó correctamente, volver a mostrar el formulario de registro
            return View("Ingresos", model);
        }



        // POST UPDATE





        // POST DELETE







        /*
            APARTADO DE SALIDAS
         
         */



        public IActionResult Salidas()
        {

            var salidas = _salidaService.GetAsync("Salidas").Result;

            List<SalidasViewModel> salidaViewModels = new List<SalidasViewModel>();

            foreach (var salida in salidas)
            {
                // Crear una nueva instancia de IngresosViewModel y asignar precio y descripción
                var salidaViewModel = new SalidasViewModel
                {
                    Fecha_Salida = salida.Fecha_Salida,
                    Cantidad_Vendida = salida.Cantidad_Vendida,
                    Metodo_Pago = salida.Metodo_Pago
                };
                salidaViewModels.Add(salidaViewModel);
            }

            ViewBag.Salidas = salidaViewModels;
            return View();
        }

        // POST CREATE
        [HttpPost]
        public async Task<IActionResult> Salidas(SalidasViewModel model)
        {
            // Verificar si el modelo es válido
            if (ModelState.IsValid)
            {
                // Crear un nuevo salida
                var nuevoSalida = new Salidas
                {
                    Fecha_Salida = model.Fecha_Salida,
                    id_Articulo = ObjectId.Parse("660dc3e881c5953b240d5f51"),
                    Cantidad_Vendida = model.Cantidad_Vendida,
                    Precio_Venta = model.Precio_Venta,
                    Motivo_Salida = ObjectId.Parse("660dc3e881c5953b240d5f51"),
                    Metodo_Pago = ObjectId.Parse("660dc3e881c5953b240d5f51"),

                    // Asigna otros campos de usuario según sea necesario
                };

                // Intenta crear el salida
                try
                {
                    await _salidaService.CreateAsync("Salidas", nuevoSalida);

                    // Si el salida se crea con éxito, configurar un mensaje de éxito en ViewBag
                    ViewBag.Success = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al registrar la salida: " + ex.Message);
                }
            }

            // Si hay errores de validación o si el usuario no se creó correctamente, volver a mostrar el formulario de registro
            return View("Salidas", model);
        }



        // POST UPDATE





        // POST DELETE


    }


}

