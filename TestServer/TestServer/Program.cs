using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext1())
            {
                // Create and save a new Blog
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new GeneralChat { Name = name };
                db.GenChat.Add(blog);
                db.SaveChanges();



                // Display all Blogs from the database

                Console.WriteLine("All blogs in the database:");
                foreach (var item in db.GenChat)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
    public class GeneralChat
    {
        public int GeneralChatId { get; set; }
        public string Name { get; set; }

        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public virtual GeneralChat Blog { get; set; }
    }
    public class BloggingContext1 : DbContext
    {
        public DbSet<GeneralChat> GenChat { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
