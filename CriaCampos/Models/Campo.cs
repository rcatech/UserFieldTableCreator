using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriaCampos.Models
{
    public class Campo
    {
        public string TableName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public BoFieldTypes Tipo { get; set; }

        public int? Tamanho { get; set; }

        public string ValorPadrao { get; set; }

        public BoFldSubTypes? SubTipo { get; set; }

        public BoYesNoEnum Obrigatorio { get; set; }

        public string TabelaSAPLinked { get; set; }
        
        public List<ValoresValidos> ValoresValidos { get; set; }
    }
}
