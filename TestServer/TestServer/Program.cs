using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DBChat())
            {
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();
                var blog = new Rooms { Type = name };
                db.PoolRoom.Add(blog);
                db.SaveChanges();



                // Display all Blogs from the database

                Console.WriteLine("All blogs in the database:");
                foreach (var item in db.PoolRoom)
                {
                    Console.WriteLine(item.Type);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
    public class Rooms
    {
        [Key]
        public int RoomID { get; set; }
        public string Type { get; set; }

        public virtual List<Characters> ListCharacter { get; set; }
        public virtual List<Messages> PoolMessage { get; set; }
    }

    public class Characters
    {
        [Key]
        public string CharacterID { get; set; }
        public int RoomID { get; set; }
    }
    public class Messages
    {
        [Key]
        public string MessageID { get; set; }
        public int RoomID { get; set; }
        public string From { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }
    }
    public class DBChat : DbContext
    {
        public DbSet<Rooms> PoolRoom { get; set; }
        /*public DbSet<Characters> ListCharacters { get; set; }
        public DbSet<Messages> PoolMessages { get; set; }*/
    }
}
