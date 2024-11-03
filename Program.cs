
class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public bool IsBestFriend { get; set; }
}

class ContactManager
{
    private List<Contact> contacts = new List<Contact>();
    private int nextId = 1;

    public void AddContact()
    {
        var contact = new Contact();

        contact.Id = nextId++;
        Console.WriteLine("Digite el nombre de la persona");
        contact.Name = Console.ReadLine();
        Console.WriteLine("Digite el apellido de la persona");
        contact.LastName = Console.ReadLine();
        Console.WriteLine("Digite la dirección");
        contact.Address = Console.ReadLine();
        Console.WriteLine("Digite el telefono de la persona");
        contact.Telephone = Console.ReadLine();
        Console.WriteLine("Digite el email de la persona");
        contact.Email = Console.ReadLine();
        Console.WriteLine("Digite la edad de la persona en números");
        contact.Age = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Especifique si es mejor amigo: 1. Si, 2. No");
        var temp = Convert.ToInt32(Console.ReadLine());
        contact.IsBestFriend = temp == 1;

        contacts.Add(contact);
    }

    public void ViewContacts()
    {
        Console.WriteLine($"Nombre          Apellido            Dirección           Telefono            Email           Edad            Es Mejor Amigo?");
        Console.WriteLine($"____________________________________________________________________________________________________________________________");
        foreach (var contact in contacts)
        {
            string isBestFriendStr = contact.IsBestFriend ? "Si" : "No";
            Console.WriteLine($"{contact.Name}         {contact.LastName}         {contact.Address}         {contact.Telephone}            {contact.Email}            {contact.Age}          {isBestFriendStr}");
        }
    }

    public void SearchContact()
    {
        Console.WriteLine($"INGRESE EL NOMBRE DEL USUARIO");
        string userSearch = Console.ReadLine();

        Console.WriteLine($"Nombre          Apellido            Dirección           Telefono            Email           Edad            Es Mejor Amigo?");
        Console.WriteLine($"____________________________________________________________________________________________________________________________");
        foreach (var contact in contacts)
        {
            if (contact.Name.Equals(userSearch, StringComparison.OrdinalIgnoreCase))
            {
                string isBestFriendStr = contact.IsBestFriend ? "Si" : "No";
                Console.WriteLine($"{contact.Name}         {contact.LastName}         {contact.Address}         {contact.Telephone}            {contact.Email}            {contact.Age}          {isBestFriendStr}");
            }
        }
    }

    public void ModifyContact()
    {
        Console.WriteLine("Digite el teléfono del contacto que desea modificar:");
        string phoneToModify = Console.ReadLine();

        var contactToModify = contacts.FirstOrDefault(c => c.Telephone == phoneToModify);

        if (contactToModify != null)
        {
            Console.WriteLine("Digite el nuevo nombre de la persona (deje en blanco para no cambiar):");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) contactToModify.Name = newName;

            Console.WriteLine("Digite el nuevo apellido de la persona (deje en blanco para no cambiar):");
            string newLastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newLastName)) contactToModify.LastName = newLastName;

            Console.WriteLine("Digite la nueva dirección (deje en blanco para no cambiar):");
            string newAddress = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newAddress)) contactToModify.Address = newAddress;

            Console.WriteLine("Digite el nuevo email (deje en blanco para no cambiar):");
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail)) contactToModify.Email = newEmail;

            Console.WriteLine("Digite la nueva edad (deje en blanco para no cambiar):");
            string ageInput = Console.ReadLine();
            if (int.TryParse(ageInput, out int newAge)) contactToModify.Age = newAge;

            Console.WriteLine("Especifique si es mejor amigo (1. Si, 2. No, deje en blanco para no cambiar):");
            string bestFriendInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(bestFriendInput))
            {
                contactToModify.IsBestFriend = bestFriendInput == "1";
            }

            Console.WriteLine("Contacto modificado.");
        }
        else
        {
            Console.WriteLine("No se encontró un contacto con ese teléfono digite bien.");
        }
    }

    public void DeleteContact()
    {
        Console.WriteLine("Digite el teléfono del contacto que desea eliminar:");
        string phoneToDelete = Console.ReadLine();

        var contactToDelete = contacts.FirstOrDefault(c => c.Telephone == phoneToDelete);

        if (contactToDelete != null)
        {
            contacts.Remove(contactToDelete);
            Console.WriteLine("Contacto borrado con éxito.");
        }
        else
        {
            Console.WriteLine("No se encontró un contacto con ese teléfono.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ContactManager contactManager = new ContactManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine(@"1. Agregar Contacto     2. Ver Contactos    3. Buscar Contactos     4. Modificar Contacto   5. Eliminar Contacto    6. Salir");
            Console.WriteLine("Digite el número de la opción deseada");

            int typeOption = Convert.ToInt32(Console.ReadLine());

            switch (typeOption)
            {
                case 1:
                    contactManager.AddContact();
                    break;
                case 2:
                    contactManager.ViewContacts();
                    break;
                case 3:
                    contactManager.SearchContact();
                    break;
                case 4:
                    contactManager.ModifyContact();
                    break;
                case 5:
                    contactManager.DeleteContact();
                    break;
                case 6:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }
}
