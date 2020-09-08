using CriaCampos.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriaCampos
{
    public class CriadorObjetos
    {
        public static void CriarObjeto(Company oCmp, Objeto objeto)
        {
            UserObjectsMD oUdo = (UserObjectsMD)oCmp.GetBusinessObject(BoObjectTypes.oUserObjectsMD);

            oUdo.Code = objeto.Codigo;
            oUdo.Name = objeto.Name;
            oUdo.ObjectType = objeto.Tipo;
            oUdo.TableName = objeto.NomeTabela;
            oUdo.CanFind = objeto.CanFind;
            oUdo.CanDelete = objeto.CanDelete;
            oUdo.CanCancel = objeto.CanCancel;
            oUdo.CanLog = objeto.CanLog;
            oUdo.ManageSeries = objeto.MngSeries;
            oUdo.CanYearTransfer = objeto.CanYrTransf; 

            foreach(Campo campo in objeto.Campos)
            {
                oUdo.FindColumns.ColumnAlias = campo.Name;
                oUdo.FindColumns.ColumnDescription = campo.Description;
                oUdo.FindColumns.Add();
            }

            if (objeto.TabelaFilhas != null)
            { 
                foreach (Tabela tabela in objeto.TabelaFilhas)
                {
                    oUdo.ChildTables.TableName = tabela.TableName;
                    oUdo.ChildTables.Add();
                } 
            }

            int a = oUdo.Add();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUdo);
            oUdo = null;
            GC.Collect();
            if (a != 0)
                throw new Exception(oCmp.GetLastErrorDescription());

            
        }
    }
}
