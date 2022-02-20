using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soru1
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public class Payment
    {
        public virtual void DoPayment()
        {
            Console.WriteLine("Payment");
        }
    }

    class Cash : Payment
    {
        public override void DoPayment()
        {
            Console.WriteLine("Nakit Ödeme");
        }
    }

    class CreditCart : Payment
    {
        public override void DoPayment()
        {
            Console.WriteLine("Kredi Kartı ile Ödeme");
        }
    }

}
