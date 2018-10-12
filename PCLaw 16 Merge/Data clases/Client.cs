using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLaw_16_Merge
{
    public class Client
    {
        public Client()
        {
            contact = new Contact();
            address = new Address();
        }

        public int oldID { get; set; }
        public int ID { get; set; }
        public string nickName { get; set; }
        public Contact contact { get; set; }
        public Address address { get; set; }
    }
}
