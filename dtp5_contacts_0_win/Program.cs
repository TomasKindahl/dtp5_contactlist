using System;
using System.IO;

namespace dtp5_contacts_0
{
    class MainClass
    {
        static Person[] contactList = new Person[100];
        static string Collapse(string[] strArr)
        {
            string result;
            result = strArr[0];
            for(int i = 1; i < strArr.Length; i++)
            {
                if(strArr[i] != null)
                    result = result + ", " + strArr[i];
            }
            return result;
        }
        class Person
        {
            public string persname, surname,  birthdate;
            public string[] phoneList = new string[20], addressList = new string[20];
            public Person(bool ask = false) {
                if (ask) {
                    Console.Write("personal name: ");
                    persname = Console.ReadLine();
                    Console.Write("surname: ");
                    surname = Console.ReadLine();
                    string phone;
                    do
                    {
                        Console.Write("phone (enter if end): ");
                        phone = Console.ReadLine();
                        if (phone == "") break;
                        for (int i = 0; i < phoneList.Length; i++)
                            if (phoneList[i] == null)
                            {
                                phoneList[i] = phone;
                                break;
                            }
                    } while (phone != "");
                    Console.Write("address: ");
                    addressList[0] = Console.ReadLine();
                    Console.Write("birthdate: ");
                    birthdate = Console.ReadLine();
                }
            }
            public Person(string[] attrs)
            {
                persname = attrs[0];
                surname = attrs[1];
                string[] phones = attrs[2].Split(';');
                phoneList = phones;
                string[] addresses = attrs[3].Split(';');
                addressList = addresses;
                birthdate = attrs[4];
            }
            public void Print()
            {
                string phones = Collapse(phoneList);
                string addresses = Collapse(addressList);
                Console.WriteLine($"{persname} {surname}; {phones}; {addresses}; {birthdate} ");
            }
        }
        public static void Main(string[] args)
        {
            string lastFileName = "address.lis";
            string[] commandLine;
            Console.WriteLine("Hello and welcome to the contact list");
            PrintHelp();
            do
            {
                Console.Write($"> ");
                commandLine = Console.ReadLine().Split(' ');
                if (commandLine[0] == "quit" ) {
                    Console.WriteLine("Not yet implemented: safe quit");
                }
                else if (commandLine[0] == "list")
                {
                    // static method Something(Person[] contactList) TBD
                    foreach (Person p in contactList) {
                        if (p != null) 
                            p.Print();
                    }
                }
                else if (commandLine[0] == "load") {
                    if (commandLine.Length < 2) {
                        lastFileName = "address.lis";
                        ReadContactListFromFile(lastFileName);
                    }
                    else {
                        lastFileName = commandLine[1];
                        ReadContactListFromFile(lastFileName);
                    }
                }
                else if (commandLine[0] == "save") {
                    if (commandLine.Length < 2) {
                        WriteContacListToFile(lastFileName);
                    }
                    else {
                        Console.WriteLine("Not yet implemented: save /file/");
                    }
                }
                else if (commandLine[0] == "new" ) {
                    if (commandLine.Length < 2)
                    {
                        Person p = new Person(ask: true);
                        InsertPersonIntoContactList(p);
                    }
                    else {
                        Console.WriteLine("Not yet implemented: new /person/");
                    }
                }
                else if (commandLine[0] == "help")
                    PrintHelp();
                else
                    Console.WriteLine($"Unknown command: '{commandLine[0]}'");
            } while (commandLine[0] != "quit");
        }

        private static void InsertPersonIntoContactList(Person p)
        {
            for (int ix = 0; ix < contactList.Length; ix++)
            {
                if (contactList[ix] == null)
                {
                    contactList[ix] = p;
                    break;
                }
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Avaliable commands: ");
            Console.WriteLine("  delete       - emtpy the contact list");
            Console.WriteLine("  delete /persname/ /surname/ - delete a person");
            Console.WriteLine("  load        - load contact list data from the file address.lis");
            Console.WriteLine("  load /file/ - load contact list data from the file");
            Console.WriteLine("  new        - create new person");
            Console.WriteLine("  new /persname/ /surname/ - create new person with personal name and surname");
            Console.WriteLine("  quit        - quit the program");
            Console.WriteLine("  save         - save contact list data to the file previously loaded");
            Console.WriteLine("  save /file/ - save contact list data to the file");
            Console.WriteLine();
        }

        private static void WriteContacListToFile(string lastFileName)
        {
            using (StreamWriter outfile = new StreamWriter(lastFileName))
            {
                foreach (Person p in contactList)
                {
                    if (p != null)
                        outfile.WriteLine($"{p.persname};{p.surname};{p.phoneList};{p.addressList};{p.birthdate}");
                }
            }
        }

        private static void ReadContactListFromFile(string lastFileName)
        {
            using (StreamReader infile = new StreamReader(lastFileName))
            {
                string line;
                while ((line = infile.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    string[] attrs = line.Split('|');
                    Person p = new Person(attrs);
                    InsertPersonIntoContactList(p);
                }
            }
        }
    }
}
