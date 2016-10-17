using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AgencyAccount
    {
        public int AgencyAccountId { get; set; }

        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public string BankName { get; set; }
        public string IfscCode { get; set; }

    }
}
