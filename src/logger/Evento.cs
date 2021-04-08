using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logger
{
    /// <summary>
    /// Evento de Log
    /// </summary>
    public class Evento
    {
        /// <summary>
        /// Data e hora do evento
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// Nível de aviso: DEBUG, INFO, WARNING ou ERROR
        /// </summary>
        public string Nivel { get; set; }

        /// <summary>
        /// Mensagem do evento
        /// </summary>
        public string Mensagem { get; set; }
    }
}
