using System;
using System.Collections.Generic;
using System.IO;

namespace logger
{
    /// <summary>
    /// Classe de geração de arquivos de log
    /// </summary>
    public class Logger
    {
        #region Parâmetros Privados

        private static string Pasta { get; set; }
        private static string NomeArquivo { get; set; }
        private static string Pastainicial { get; set; }

        /// <summary>
        /// Caminho do arquivo de log
        /// </summary>
        public static string CaminhoLog { get; private set; }

        /// <summary>
        /// Texto completo do Log
        /// </summary>
        public static string Texto { get; private set; } = string.Empty;

        /// <summary>
        /// Lista de eventos detalhados do log
        /// </summary>
        public static List<Evento> Eventos { get; private set; } = new List<Evento>();

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Define a pasta inicial onde será gerada a pasta <c>Logs</c>
        /// </summary>
        /// <param name="pasta">Caminho da pasta inicial</param>
        public static void DefinirPastaInicial(string pasta)
        {
            Pastainicial = pasta;
        }

        /// <summary>
        /// Manda uma mensagem de depuração para o arquivo de log
        /// </summary>
        /// <param name="mensagem">Texto da mensagem</param>
        public static void Debug(string mensagem)
        {
            Log(mensagem, "DEBUG");
        }

        /// <summary>
        /// Manda uma mensagem de informação para o arquivo de log
        /// </summary>
        /// <param name="mensagem">Texto da mensagem</param>
        public static void Info(string mensagem)
        {
            Log(mensagem, "INFO");
        }

        /// <summary>
        /// Manda uma mensagem de aviso para o arquivo de log
        /// </summary>
        /// <param name="mensagem">Texto da mensagem</param>
        public static void Warning(string mensagem)
        {
            Log(mensagem, "WARNING");
        }

        /// <summary>
        /// Manda uma mensagem de erro para o arquivo de log
        /// </summary>
        /// <param name="mensagem">Texto da mensagem</param>
        public static void Error(string mensagem)
        {
            Log(mensagem, "ERROR");
        }

        /// <summary>
        /// Monta uma mensagem de erro no log a partir de uma Exception
        /// </summary>
        /// <param name="e">Exceção para gerar a mensagem</param>
        public static void Error(Exception e)
        {
            Log($"{e.GetType()}: {e.Message}\n{e.StackTrace}", "ERROR");
        }

        #endregion

        #region Métodos Privados

        private static void InicializaArquivo()
        {
            try
            {
                Pasta = Path.Combine(Pastainicial, "Logs", DateTime.Now.ToString("yyyy-MM"));
                NomeArquivo = $"log_{DateTime.Now:yyyyMMdd_HHmmss}.log";

                CaminhoLog = Path.Combine(Pasta, NomeArquivo);

                if (!File.Exists(CaminhoLog))
                {
                    if (!Directory.Exists(Pasta))
                    {
                        Directory.CreateDirectory(Pasta);
                    }

                    File.Create(CaminhoLog).Dispose();
                }
            }
            catch
            {

            }
        }

        private static void Log(string mensagem, string nivel)
        {
            if (CaminhoLog == null)
            {
                InicializaArquivo();
            }

            try
            {
                Evento evento = new Evento
                {
                    TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    Nivel = nivel,
                    Mensagem = mensagem
                };
                Eventos.Add(evento);

                Texto += $"{evento.TimeStamp} [{evento.Nivel}] {evento.Mensagem}\r\n";

                File.WriteAllText(CaminhoLog, Texto);
            }
            catch
            {

            }
        }

        #endregion
    }
}
