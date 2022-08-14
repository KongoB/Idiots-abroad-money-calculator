using System;
using System.Collections.Generic;
using System.Linq;

namespace Idiots_abroad
{
    public class Idiot
    {

        public Idiot(string name)
        {
            Name = name;
            OwedMoney = new Dictionary<Idiot, decimal>();

        }


        public string Name { get; set; }


        public Dictionary<Idiot, decimal> OwedMoney { get; private set; }



        public void AddMoneyOwed (string idiot, decimal money)
        {
            KeyValuePair<Idiot, decimal> owedPerson = OwedMoney.FirstOrDefault(x => x.Key.Name == idiot);


            if (owedPerson.Key == null)
            {
                Idiot newIdiot = new Idiot(idiot);
                OwedMoney.Add(newIdiot, money);
            }
            else
            {
                OwedMoney[owedPerson.Key] += money;
            }

        }




        public override string ToString()
        {
            string output = "";


            foreach (var person in OwedMoney)
            {
                if (person.Value > 0)
                {
                    output += $"{this.Name} дължи €{person.Value:F2} на {person.Key.Name}.\n";
                }
            }

            return System.Net.WebUtility.HtmlDecode(output);
        }

    }
}
