using cafeteria_joy.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace cafeteria_joy.Validators
{
    public class CedulaValidatorAttribute : ValidationAttribute
    {
        private JoyContext _context;
        private string tableName;


        public CedulaValidatorAttribute(string tableName)
        {
            this.tableName = tableName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null)
            {
                bool isDuplicated = false;


                _context = (JoyContext)validationContext.GetService(typeof(JoyContext));

                switch (tableName)
                {

                    case "Usuario":

                        var userExist = _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == value.ToString()).Result;
                        if (userExist is not null)
                            isDuplicated = true;
                        break;
                    case "Empleado":

                        var employeeExist = _context.Empleados.FirstOrDefaultAsync(u => u.Cedula == value.ToString()).Result;
                        if (employeeExist is not null)
                            isDuplicated = true;
                        break;

                }

                //Verificar la cedula no esté registrada en la base de datos
                if (isDuplicated == true)
                {
                    return new ValidationResult("Cédula ya registrada");
                }

                //Verificar la cédula es valida , mediante la API de la Junta Central

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
            }

            //Validacion cedula en local,en caso de no comunicación con la API del
            //"portal de APIs Dominicano"

            if (LocalCedulaValidation(value.ToString()) == true)
            {
                return ValidationResult.Success;

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

