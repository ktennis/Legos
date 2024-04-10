using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Security;

namespace Legos.Models
{
    public class CheckoutFraud
    {
        private static int currentTransactionID = 753991;

        public CheckoutFraud()
        {
            transaction_ID = currentTransactionID++;
        }
      
        public int transaction_ID { get; set; }
        public int customer_ID { get; set; }
        public int date { get; set; }
        public DayOfWeek day_of_week { get; set; }
        public TimeOnly time {  get; set; }
        public string entry_mode { get; set; }
        public int amount { get; set; }
        public string type_of_transaction { get; set; }
        public string bank {  get; set; }
        public string type_of_card {get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int birth_date {  get; set; }
        public string country_of_residence { get; set; }
        public string shipping_country { get; set; }
        public string gender { get; set; }
        public int age { get; set; }

    }
}

