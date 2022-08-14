using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Idiots_abroad
{

    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;

            var path = @"..\..\..\prices.txt";

            List<Idiot> people = new List<Idiot>();


            string[] lines = File.ReadAllLines(path).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();


            for (int i = 0; i < lines.Length; i++)
            {

                string[] currentLine = lines[i].Split(" - ");

                string person = currentLine[2];
                string owes = currentLine[1];
                decimal money = decimal.Parse(currentLine[0]);

                int indexExists = people.FindIndex(x => x.Name == person);

                Idiot idiot = null;


                if (indexExists < 0)
                {
                    idiot = new Idiot(person);
                    people.Add(idiot);
                }
                else
                {
                    idiot = people[indexExists];
                }


                idiot.AddMoneyOwed(owes, money);




            }


            foreach (var person in people)
            {
                Console.WriteLine(person.ToString());
            }

            Console.WriteLine("---------------------------------------------\n");


            CalculateMoney(people);

        }


        


        public static void CalculateMoney(List<Idiot> people)
        {


            foreach (var person in people)
            {

                foreach (var owedPerson in person.OwedMoney)
                {

                    decimal personOwesToOwedPerson = owedPerson.Value;

                    int indexOfPerson = people.FindIndex(x => x.Name == person.Name);
                    int indexOfOwedPerson = people.FindIndex(x => x.Name == owedPerson.Key.Name);

                    Idiot idiot = people[indexOfOwedPerson];

                    KeyValuePair<Idiot, decimal> owedPersonOwes = idiot.OwedMoney.FirstOrDefault(x => x.Key.Name == person.Name);

                    decimal owedPersonOwesToPerson = owedPersonOwes.Value;



                    string person1, person2; 

                    decimal totalOwed;

                    if (indexOfPerson < indexOfOwedPerson)
                    {
                        if (personOwesToOwedPerson > owedPersonOwesToPerson)
                        {
                            totalOwed = personOwesToOwedPerson - owedPersonOwesToPerson;

                            person1 = person.Name;
                            person2 = idiot.Name;

                        }
                        else
                        {
                            totalOwed = owedPersonOwesToPerson - personOwesToOwedPerson;

                            person1 = idiot.Name;
                            person2 = person.Name;
                        }

                        Console.WriteLine($"{person1} дължи €{totalOwed:F2} на {person2}.");
                    }

                }

            }

        }


        public static void Owes(string name, string name2, decimal money)
        {

            Console.WriteLine($"{name} дължи €{money:F2} на {name2}");

        }

    }
}

