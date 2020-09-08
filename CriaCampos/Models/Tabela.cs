using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriaCampos.Models
{
    public class Tabela
    {

        public string TableName { get; set; }

        public string Description { get; set; }

        public BoUTBTableType TipoTabela { get; set; }

        public BoYesNoEnum Arquivavel { get; set; }

    }
}
