using cafeteria_joy.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace cafeteria_joy.Validators
{
    public class RncValidatorAttribute : ValidationAttribute
    {
        private JoyContext _context;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null)
            {


                _context = (JoyContext)validationContext.GetService(typeof(JoyContext));


                var exist = _context.Proveedores.FirstOrDefaultAsync(e => e.Rnc == value.ToString()).Result;


                //Verificar el RNC no esté registrado en la base de datos
                if (exist is not null)
                {
                    return new ValidationResult("RNC ya registrado");
                }

                //Validacion cedula en local,en caso de no comunicacion con la API de la Junta central

                if (LocalRncValidation(value.ToString()) == true)
                {
                    return ValidationResult.Success;

                }

            }
            return new ValidationResult("RNC no válido");
        }


        public bool LocalRncValidation(string rnc)
        {
            char[] peso = { '7', '9', '8', '6', '5', '4', '3', '2' };
            int suma = 0;
            int division = 0;

            if (rnc.Length != 9)
                return false;
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    //para verificar si es un dígito o no
                    if (!char.IsDigit(rnc[i]))
                        return false;

                    suma += (int)(char.GetNumericValue(rnc[i]) * char.GetNumericValue(peso[i]));
                }

                division = suma / 11;
                int resto = suma - (division * 11);
                int digito = 0;

                switch (resto)
                {
                    case 0:
                        digito = 2;
                        break;
                    case 1:
                        digito = 1;
                        break;
                    default:
                        digito = 11 - resto;
                        break;
                }

                if (digito != char.GetNumericValue(rnc[8]))
                    return false;
            }

            return true;
        }

    }
}

