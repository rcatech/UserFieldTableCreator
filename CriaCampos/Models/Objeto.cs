using SAPbobsCOM;
using System.Collections.Generic;

namespace CriaCampos.Models
{
    public class Objeto
    {
        public string Codigo { get; set; }
        public string Name { get; set; }
        public BoUDOObjType Tipo { get; set; }
        public string NomeTabela { get; set; }
        public BoYesNoEnum CanFind { get; set; }
        public BoYesNoEnum CanDelete { get; set; }
        public BoYesNoEnum CanCancel { get; set; }
        public BoYesNoEnum CanLog { get; set; }
        public BoYesNoEnum MngSeries { get; set; }
        public BoYesNoEnum CanYrTransf { get; set; }
        public List<Campo> Campos { get; set; }
        public List<Tabela> TabelaFilhas { get; set; }
    }
}
