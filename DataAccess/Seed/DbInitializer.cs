using DataAccess.Context;
using DataAccess.EntityModels;

namespace DataAccess.Seed
{
    public class DbInitializer
    {
        public static void Seed(TaskDbContext context)
        {
            if(!context.Priorities.Any())
            {
                context.Priorities.AddRange(
                    new PriorityEntity { Title = "Low", ColorHash = "#8ae699" },
                    new PriorityEntity { Title = "Medium", ColorHash = "#e6c38a" },
                    new PriorityEntity { Title = "High", ColorHash = "#e68a8a" }
                );
            }

            if (!context.Tags.Any())
            {
                context.Tags.AddRange(
                    new TagEntity { Title = "Work", ColorHash = "#8e9e95" },
                    new TagEntity { Title = "Home" , ColorHash = "#d9bd84" },
                    new TagEntity { Title = "Study", ColorHash = "#5cd1b6" }
                );
            }

            context.SaveChanges();
        }
    }
}
