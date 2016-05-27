using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPortfolioUWPApp.Util
{
    public class PortfolioAppNetworkStatusDetector : EventArgs
    {
        bool isConnected = false;

        public PortfolioAppNetworkStatusDetector(bool isConnected)
            : base()
        {
            this.isConnected = isConnected;
        }

        public bool ConnectionStatus
        {
            get { return isConnected; }
        }

    }
}
