using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syns.Ticket.ClientMvc
{
    public class DataTableHelper
    {
        public string sSearch = "";

        public string echo = "";
        public int iColumns = 0;
        public int iDisplayStart = 0;
        public int iDisplayLength = 0;
        public string iSortingCols = "";
        public int regExibir = 0;
        public int startExibir = 0;


        IList<string> columnNames;
        public Dictionary<string, string> columnNamesOrder;
        public string sortBy = "";

        public DataTableHelper()
        {
            var request = HttpContext.Current.Request;

            columnNames = new List<string>();
            columnNamesOrder = new Dictionary<string, string>();

            //sSearch = request.Params["sSearch"].ToString();
            echo = request.Params["sEcho"].ToString();
            iColumns = int.Parse(request.Params["iColumns"].ToString());
            iDisplayStart = int.Parse(request.Params["iDisplayStart"].ToString());
            iDisplayLength = int.Parse(request.Params["iDisplayLength"].ToString());
            if (request.Params["iSortingCols"] != null)
                iSortingCols = request.Params["iSortingCols"].ToString();
            //sSearch = request.Params["sSearch"].ToString();

            regExibir = iDisplayLength;
            startExibir = iDisplayStart;

            for (int i = 0; i < iColumns; i++)
            {
                string nomeColuna = request.Params["mDataProp_" + i.ToString()].ToString();
                columnNames.Add(nomeColuna);
            }


            for (int i = 0; i < iColumns; i++)
            {
                if (request.Params["iSortCol_" + i.ToString()] != null)
                {
                    int idxColuna = int.Parse(request.Params["iSortCol_" + i.ToString()]);
                    string nomeColuna = columnNames[idxColuna];
                    string orderDirection = request.Params["sSortDir_" + i.ToString()];
                    columnNamesOrder.Add(nomeColuna, orderDirection);

                    if (sortBy != "")
                        sortBy += ", ";

                    sortBy += nomeColuna + " " + orderDirection;
                }
            }
        }
    }
}