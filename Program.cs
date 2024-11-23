//Se usando postgres como base de datos
using Npgsql; 


namespace ContactApp
{
    public static class Database
    {
        private static readonly string ConnectionString = "Host={host};Port={port};Username={username};Password={password};Database={db}";

        public static NpgsqlConnection GetConnection()
        {
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }

    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public bool IsBestFriend { get; set; } = false;
    }

    public class ContactManager
    {
        public void AddContact()
        {
            using (var connection = Database.GetConnection())
            {
                var command = new NpgsqlCommand("INSERT INTO Contacts (Name, LastName, Address, Telephone, Email, Age, IsBestFriend) VALUES (@Name, @LastName, @Address, @Telephone, @Email, @Age, @IsBestFriend)", connection);

                var contact = new Contact();

                Console.Write("Nombre: ");
                contact.Name = Console.ReadLine();

                Console.Write("Apellido: ");
                contact.LastName = Console.ReadLine();

                Console.Write("Dirección: ");
                contact.Address = Console.ReadLine();

                Console.Write("Teléfono: ");
                contact.Telephone = Console.ReadLine();

                Console.Write("Email: ");
                contact.Email = Console.ReadLine();

                Console.Write("Edad: ");
                contact.Age = Convert.ToInt32(Console.ReadLine());

                Console.Write("¿Es mejor amigo? (1. Sí, 2. No): ");
                contact.IsBestFriend = Console.ReadLine() == "1";

                command.Parameters.AddWithValue("Name", contact.Name);
                command.Parameters.AddWithValue("LastName", contact.LastName);
                command.Parameters.AddWithValue("Address", contact.Address);
                command.Parameters.AddWithValue("Telephone", contact.Telephone);
                command.Parameters.AddWithValue("Email", contact.Email);
                command.Parameters.AddWithValue("Age", contact.Age);
                command.Parameters.AddWithValue("IsBestFriend", contact.IsBestFriend);

                command.ExecuteNonQuery();
                Console.WriteLine("Contacto agregado.");
            }
        }

        public void ViewContacts()
        {
            using (var connection = Database.GetConnection())
            {
                var command = new NpgsqlCommand("SELECT * FROM Contacts", connection);
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Id | Nombre | Apellido | Dirección | Teléfono | Email | Edad | Mejor Amigo");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Id"]} | {reader["Name"]} | {reader["LastName"]} | {reader["Address"]} | {reader["Telephone"]} | {reader["Email"]} | {reader["Age"]} | {(reader["IsBestFriend"] as bool? == true ? "Sí" : "No")}");
                    }
                }
            }
        }

        public void SearchContact()
        {
            Console.Write("Nombre del contacto a buscar: ");
            string name = Console.ReadLine();

            using (var connection = Database.GetConnection())
            {
                var command = new NpgsqlCommand("SELECT * FROM Contacts WHERE Name ILIKE @Name", connection);
                command.Parameters.AddWithValue("Name", "%" + name + "%");

                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Id | Nombre | Apellido | Dirección | Teléfono | Email | Edad | Mejor Amigo");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Id"]} | {reader["Name"]} | {reader["LastName"]} | {reader["Address"]} | {reader["Telephone"]} | {reader["Email"]} | {reader["Age"]} | {(reader["IsBestFriend"] as bool? == true ? "Sí" : "No")}");
                    }
                }
            }
        }

        public void ModifyContact()
        {
            Console.Write("Teléfono del contacto a modificar: ");
            string telephone = Console.ReadLine();

            using (var connection = Database.GetConnection())
            {
                var command = new NpgsqlCommand("UPDATE Contacts SET Name = @Name, LastName = @LastName, Address = @Address, Email = @Email, Age = @Age, IsBestFriend = @IsBestFriend WHERE Telephone = @Telephone", connection);

                Console.Write("Nuevo nombre: ");
                string name = Console.ReadLine();
                command.Parameters.AddWithValue("Name", name);

                Console.Write("Nuevo apellido: ");
                string lastName = Console.ReadLine();
                command.Parameters.AddWithValue("LastName", lastName);

                Console.Write("Nueva dirección: ");
                string address = Console.ReadLine();
                command.Parameters.AddWithValue("Address", address);

                Console.Write("Nuevo email: ");
                string email = Console.ReadLine();
                command.Parameters.AddWithValue("Email", email);

                Console.Write("Nueva edad: ");
                int age = Convert.ToInt32(Console.ReadLine());
                command.Parameters.AddWithValue("Age", age);

                Console.Write("¿Es mejor amigo? (1. Sí, 2. No): ");
                bool isBestFriend = Console.ReadLine() == "1";
                command.Parameters.AddWithValue("IsBestFriend", isBestFriend);

                command.Parameters.AddWithValue("Telephone", telephone);

                command.ExecuteNonQuery();
                Console.WriteLine("Contacto actualizado.");
            }
        }

        public void DeleteContact()
        {
            Console.Write("Teléfono del contacto a eliminar: ");
            string telephone = Console.ReadLine();

            using (var connection = Database.GetConnection())
            {
                var command = new NpgsqlCommand("DELETE FROM Contacts WHERE Telephone = @Telephone", connection);
                command.Parameters.AddWithValue("Telephone", telephone);

                command.ExecuteNonQuery();
                Console.WriteLine("Contacto eliminado.");
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var contactManager = new ContactManager();
            bool running = true;

            while (running)
            {
                Console.WriteLine("1. Agregar Contacto  2. Ver Contactos  3. Buscar Contacto  4. Modificar Contacto  5. Eliminar Contacto  6. Salir");
                Console.Write("Seleccione una opción: ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
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
}
