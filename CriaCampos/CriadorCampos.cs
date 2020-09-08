using CriaCampos.Exceptions;
using CriaCampos.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriaCampos
{
    static class CriadorCampos
    {
        /// <summary>
        /// Cria o campo dentro do company informado
        /// </summary>
        /// <param name="oCmp">Company para criar o campo</param>
        /// <param name="campo">Campo a ser criado</param>
        public static void CriarCampo(Company oCmp, Campo campo)
        {
            ValidarCampo(campo);

            UserFieldsMD oUser = (UserFieldsMD)oCmp.GetBusinessObject(BoObjectTypes.oUserFields);

            oUser.TableName = campo.TableName;
            oUser.Name = campo.Name;
            oUser.Description = campo.Description;
            oUser.Mandatory = campo.Obrigatorio;
            oUser.Type = campo.Tipo;

            if (campo.SubTipo != null)
                oUser.SubType = campo.SubTipo;
            if (campo.Tamanho != null)
                oUser.EditSize = campo.Tamanho;
            if (!String.IsNullOrWhiteSpace(campo.ValorPadrao))
                oUser.DefaultValue = campo.ValorPadrao;

            int res = oUser.Add();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUser);
            oUser = null;
            GC.Collect();

            if (res != 0)
                throw new Exception(oCmp.GetLastErrorDescription());
        }

        /// <summary>
        /// Valida se o campo está com os campos obrigatórios preenchidos
        /// </summary>
        /// <param name="campo">Campo a ser validado</param>
        internal static void ValidarCampo(Campo campo)
        {
            if(String.IsNullOrWhiteSpace(campo.TableName))
                throw new TableNameIsNullOrWhiteSpace();
            if(String.IsNullOrWhiteSpace(campo.Name))
                throw new NameIsNullOrWhiteSpace();
            if(String.IsNullOrWhiteSpace(campo.Description))
                throw new DescriptionIsNullOrWhiteSpace();
            if(campo.Obrigatorio == null)
                throw new ObrigatorioIsNull();
            if (campo.Tipo == null)
                throw new TipoIsNull();
        }
    
        /// <summary>
        /// Cria os campos dentro do company informado
        /// </summary>
        /// <param name="oCmp">Company para criar os campos</param>
        /// <param name="campos">campos a serem criados</param>
        /// <returns>Caso erro, retorna uma tupla com true e dicionário contendo o campo e o respectivo erro. Caso contrário retorna falso e null</returns>
        public static Tuple<bool, Dictionary<Campo, Exception>> CriarCampo(Company oCmp, List<Campo> campos)
        {
            Dictionary<Campo, Exception> ListaExceptions = new Dictionary<Campo, Exception>();

            foreach (Campo campo in campos)
            {
                try
                {
                    CriarCampo(oCmp, campo);
                }
                catch (Exception ex)
                {
                    ListaExceptions.Add(campo, ex);
                }
            }

            if (ListaExceptions.Count > 0)
                return new Tuple<bool, Dictionary<Campo, Exception>>(true, ListaExceptions);
            else
                return new Tuple<bool, Dictionary<Campo, Exception>>(false, null);
        }
    }
}
