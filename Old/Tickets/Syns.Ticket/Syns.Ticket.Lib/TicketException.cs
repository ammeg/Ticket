using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Syns.Ticket.Lib
{
    public class TicketException: Exception
    {
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
        public int Codigo { get; set; }
        public Exception ex { get; set; }
        public TicketException() { }
        public TicketException(string message) : base(message) { }
        public TicketException(string message, int codigo, Exception ex)
            : base(message)
        {
            this.Codigo = codigo;
            this.ex = ex;
            if (ex != null)
            {
                string path = @"Logs";
                if (HttpContext.Current != null)
                    path = HttpContext.Current.Server.MapPath("~/Logs/");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                using (StreamWriter valor = new StreamWriter(String.Format("{0}\\report{1:yyyy-MM-dd-HH-mm-ss}.txt", path, DateTime.Now), true, Encoding.ASCII))
                {
                    GeraLog(ex, valor);
                    valor.Close();
                }
            }
        }

        private void GeraLog(Exception ex, StreamWriter valor)
        {
            //Abrir o arquivo

            valor.WriteLine("Mensagem: " + ex.Message);
            valor.WriteLine("Source: " + ex.Source);
            valor.WriteLine("StackTrace: " + ex.StackTrace);
            valor.WriteLine("TargetSite" + ex.TargetSite);
            valor.WriteLine("HelpLink: " + ex.HelpLink);

            if (ex.InnerException != null)
            {
                GeraLog(ex.InnerException, valor);
            }            
        }


        public TicketException(string message, System.Exception inner) { }

        // Constructor needed for serialization 
        // when exception propagates from a remoting server to the client.
        protected TicketException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
