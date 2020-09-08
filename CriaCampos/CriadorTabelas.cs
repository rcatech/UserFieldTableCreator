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
    public static class CriadorTabelas
    {
        /// <summary>
        /// Cria a tabela dentro do SAP Business One
        /// </summary>
        /// <param name="oCmp">Objeto da conexão do SAP que receberá essa tabela</param>
        /// <param name="oUserTb"></param>
        public static void CriarTabela(Company oCmp, Tabela oUserTb)
        {
            ValidarTabela(oUserTb);

            UserTablesMD oTbMd = (UserTablesMD)oCmp.GetBusinessObject(BoObjectTypes.oUserTables);
            oTbMd.TableName = oUserTb.TableName;
            oTbMd.TableDescription = oUserTb.Description;
            oTbMd.TableType = oUserTb.TipoTabela;
            oTbMd.Archivable = oUserTb.Arquivavel;

            int res = oTbMd.Add();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oTbMd);
            oTbMd = null;
            GC.Collect();

            if (res != 0)
                throw new Exception(oCmp.GetLastErrorDescription());
        }

        /// <summary>
        /// Valida se a tabela possui os campos necessários para ser adicionada ao SAP
        /// </summary>
        /// <param name="oUserTb">Campo de tabela do modelo do CriaCampos</param>
        internal static void ValidarTabela(Tabela oUserTb)
        {
            if(String.IsNullOrWhiteSpace(oUserTb.Description))
                throw new DescriptionIsNullOrWhiteSpace();
            if(String.IsNullOrWhiteSpace(oUserTb.TableName))
                throw new TableNameIsNullOrWhiteSpace();
            if(oUserTb.TipoTabela == null)
                throw new TipoTabelaIsNull();
            if(oUserTb.Arquivavel == null)
                throw new ArquivavelIsNull();
        }

        /// <summary>
        /// Cria as tabelas enviadas por uma lista pelo Company definido
        /// </summary>
        /// <param name="oCmp">Company em que deve ser criada as tabelas</param>
        /// <param name="ListaTabelas">Lista de tabelas a serem criadas</param>
        /// <returns>Caso erro, retorna uma tupla com true e dicionário contendo a tabela e o respectivo erro. Caso contrário retorna falso e null</returns>
        public static Tuple<bool, Dictionary<Tabela, Exception>> CriarTabela(Company oCmp, List<Tabela> ListaTabelas)
        {
            Dictionary<Tabela, Exception> ListaExceptions = new Dictionary<Tabela, Exception>();

            foreach(Tabela tabela in ListaTabelas)
            {
                try
                {
                    CriarTabela(oCmp, tabela);
                }
                catch (Exception ex)
                {
                    ListaExceptions.Add(tabela, ex);
                }
            }

            if (ListaExceptions.Count > 0)
                return new Tuple<bool, Dictionary<Tabela, Exception>>(true, ListaExceptions);
            else
                return new Tuple<bool, Dictionary<Tabela, Exception>>(false, null);
        }
    }
}
