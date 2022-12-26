using Pokedex2.components.typesPokemon;
using Pokedex2.models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Text.Json;
using System.Linq;

namespace Pokedex2.components.optionsCrud
{
    class ValidateForm
    {
        private ValidateForm() { }

        private static void RegexName(string name)
        {
            Regex rgName = new Regex(@"^[a-záéíóú][a-záéíóú\s\d.-]+[a-záéíóú\d]$", RegexOptions.IgnoreCase);
            if (!rgName.IsMatch(name)) throw new Exception("El nombre no es valido");
        }

        private static void RegexUrl(string url)
        {
            Regex rgUrl = new Regex(@"^(http|https):\/\/(\w+\.){2}\w+\/.+\.(png|jpg|jpeg)$", RegexOptions.IgnoreCase);
            if (!rgUrl.IsMatch(url)) throw new Exception("La url no es valida");
        }

        private static void MaxText(string text, int max, string message)
        {
            if (text.Length > max) throw new Exception(message);
        }

        private static void NameDataBaseInsert(string name,string _)
        {
            try
            {
                Pokemon newPokemon = new Pokemon();
                var result = newPokemon
                    .Select()
                    .Where("nombre","=", name)
                    .Exec();
                if(result.Count > 0) throw new Exception("El pokemon ya existe");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void NameDataBaseUpdate(string name,string idPokemon)
        {
            try
            {
                Pokemon newPokemon = new Pokemon();
                var result = newPokemon
                    .Select()
                    .Where("nombre", "=", name)
                    .AndWhere("id_pokemon","!=", idPokemon)
                    .Exec();
                if (result.Count > 0) throw new Exception("El pokemon ya existe");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void CountTypes(List<string> types)
        {
            if (types.Count == 0) throw new Exception("No selecciono ningun tipo");
        }

        private static void RebootMessageError(TextBlock elementText)
        {
            if (elementText.Text.Length == 0) return;
            elementText.Text = "";
            elementText.Visibility = Visibility.Collapsed;
        }

        private static void ConfigError(TextBlock elementText,string message)
        {
            elementText.Text = message;
            elementText.Visibility = Visibility.Visible;
        }

        private static bool ValidateName(TextBlock elementText, string name, Action<string,string> nameDataBase,string idPokemon = "")
        {
            try
            {
                MaxText(name, 50, "Excedio la cantidad de caracteres");
                RegexName(name);
                nameDataBase(name, idPokemon);
                return true;
            }
            catch (Exception ex)
            {
                ConfigError(elementText, ex.Message);
                return false;
            }
        }

        private static bool ValidateNameInsert(TextBlock elementText, string name) => 
            ValidateName(elementText, name, NameDataBaseInsert);

        private static bool ValidateNameUpdate(TextBlock elementText, string name, string idPokemon) => 
            ValidateName(elementText, name, NameDataBaseUpdate, idPokemon);

        private static bool ValidateUrl(TextBlock elementText, string url)
        {
            try
            {
                MaxText(url, 200, "Excedio la cantidad de caracteres");
                RegexUrl(url);
                return true;
            }
            catch (Exception ex)
            {
                ConfigError(elementText, ex.Message);
                return false;
            }
        }

        private static bool ValidateTypes(TextBlock elementText)
        {
            try
            {
                List<string> types = TypesPokemon.GetObject().types;
                CountTypes(types);
                return true;
            }
            catch (Exception ex)
            {
                ConfigError(elementText, ex.Message);
                return false;
            }
        }

        private static bool ValidateEqualString(string val1, string val2) => val1 == val2;

        private static bool ValidateEqualPokemon(string idPokemon,string name,string url)
        {
            try
            {
                ReadPokemonDb newRead = new ReadPokemonDb();
                var result = newRead.ReadOne(idPokemon)[0];
                var types = TypesPokemon.GetObject().types;
                var listBool = new List<bool>();
                var typesString = JsonSerializer.Serialize(types);
                var typesOriginalString = JsonSerializer.Serialize((List<string>)result["types"]);
                listBool.Add(ValidateEqualString(name, (string)result["name"]));
                listBool.Add(ValidateEqualString(url, (string)result["url"]));
                listBool.Add(ValidateEqualString(typesString, typesOriginalString));
                var countBool = listBool.Where(val=>val).Count();
                if (countBool == listBool.Count) throw new Exception("No cambio nada");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private static bool Validate(bool validateName, string url, TextBlock errorName, TextBlock errorUrl, TextBlock errorTypes)
        {
            bool nameValidate = validateName,
                urlValidate = ValidateUrl(errorUrl, url),
                typesValidate = ValidateTypes(errorTypes);
            if (!nameValidate || !urlValidate || !typesValidate) MessageBox.Show("Hay campos invalidos");
            return nameValidate && urlValidate && typesValidate;
        }

        public static bool ValidateInsert(string name, string url, TextBlock errorName, TextBlock errorUrl, TextBlock errorTypes)
        {
            RebootMessageError(errorName);
            RebootMessageError(errorUrl);
            var validateName = ValidateNameInsert(errorName, name);
            return Validate(validateName
                , url
                , errorName
                , errorUrl
                , errorTypes);
        }

        public static bool ValidateUpdate(string name, string url, string idPokemon, TextBlock errorName, TextBlock errorUrl, TextBlock errorTypes)
        {
            if (!ValidateEqualPokemon(idPokemon, name, url)) return false;
            RebootMessageError(errorName);
            RebootMessageError(errorUrl);
            var validateName = ValidateNameUpdate(errorName, name, idPokemon);
            return Validate(validateName
                , url
                , errorName
                , errorUrl
                , errorTypes);
        }
    }
}
