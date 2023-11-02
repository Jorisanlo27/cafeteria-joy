using cafeteria_joy.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;

namespace cafeteria_joy.Validators
{
    public class CedulaValidatorAttribute : ValidationAttribute
    {
        private JoyContext? _context;
        private Empleado? empleado;
        private Usuario? usuario;
        private string modelUsuario;

        private string ModelName { get; set; }
        private string PropertyName { get; set; }


        public CedulaValidatorAttribute(string modelName, string propertyName)
        {
            ModelName = modelName;
            PropertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null)
            {
                bool isDuplicated = false;


                _context = validationContext.GetService(typeof(JoyContext)) as JoyContext;

                //Verificar la cedula esté registrada en la base de datos


                switch (ModelName)
                {

                    case "Usuario":

                        usuario = _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Cedula == value.ToString()).Result;
                        if (usuario is not null)
                            modelUsuario = "Usuario";
                        isDuplicated = true;

                        break;
                    case "Empleado":

                        empleado = _context.Empleados.AsNoTracking().FirstOrDefaultAsync(u => u.Cedula == value.ToString()).Result;
                        if (empleado is not null)
                            modelUsuario = "Empleado";
                        isDuplicated = true;
                        break;

                }

                

                if (isDuplicated == true)
                {

                    if (modelUsuario == "Empleado" && usuario is not null)
                    {

                        PropertyInfo? modelPropertyInfo = validationContext.ObjectType.GetProperty(PropertyName);

                        var idProperty = modelPropertyInfo.GetValue(validationContext.ObjectInstance);

                        if (usuario.UsuariosId != (int)idProperty)
                        {
                            return new ValidationResult("Cédula ya registrada");
                        }
                    }

                    if (modelUsuario == "Usuario" && empleado is not null)
                    {

                        PropertyInfo? modelPropertyInfo = validationContext.ObjectType.GetProperty(PropertyName);

                        var idProperty = modelPropertyInfo.GetValue(validationContext.ObjectInstance);

                        if (empleado.EmpleadosId != (int)idProperty)
                        {
                            return new ValidationResult("Cédula ya registrada");
                        }
                    }

                }




                //Verificar la cédula es valida, mediante la API de la Junta Central

                var client = new HttpClient();
                var uri = new Uri($"https://api.digital.gob.do/v3/cedulas/{value.ToString()}/validate");

                var response = client.GetAsync(uri).Result;

                if (response.IsSuccessStatusCode)
                {

                    return ValidationResult.Success;

                }

                else if (response.StatusCode.ToString() == "NotFound")
                {
                    return new ValidationResult("Cédula no válida");

                }

            //Validacion cedula en local,en caso de no comunicación con la API del
            //"portal de APIs Dominicano"

            if (LocalCedulaValidation(value.ToString()) == true)
            {
                return ValidationResult.Success;

            }

            }
            return new ValidationResult("Cédula no válida");
        }


        public bool LocalCedulaValidation(string cedulaUsuario)
        {
            int acumulado = 0;
            string cedula = cedulaUsuario.Replace("-", "");
            int longitudCedula = cedula.Trim().Length;
            int[] valorMultiplicar = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };

            if (longitudCedula < 11 || longitudCedula > 11)
                return false;

            for (int i = 1; i <= longitudCedula; i++)
            {
                int vCalculo = Int32.Parse(cedula.Substring(i - 1, 1)) * valorMultiplicar[i - 1];
                if (vCalculo < 10)
                    acumulado += vCalculo;
                else
                    acumulado += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
            }

            if (acumulado % 10 == 0)
                return true;
            else
                return false;
        }

    }
}

