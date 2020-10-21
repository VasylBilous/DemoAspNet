using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Initializer
{
    public class GamesInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var genres = new List<Genre>
            {
                new Genre{Name = "Action"},
                new Genre{Name = "Shooter"},
                new Genre{Name = "RPG"},
                new Genre{Name = "Strategy"},
                new Genre{Name = "Rasing"},
                new Genre{Name = "Sport Simulator"}
            };



            var developers = new List<Developer>
            {
                new Developer{Name = "RockStar"},
                new Developer{Name = "EA"},
                new Developer{Name = "Epic"},
                new Developer{Name = "Bethesda"},
                new Developer{Name = "Activision"},
                new Developer{Name = "Valve"},
                new Developer{Name = "Ghost Games"},
                new Developer{Name = "Playrix"},
                new Developer{Name = "Ubisoft"},
                 new Developer{Name = "CD Project"}
            };



            var games = new List<Game>
            {
                new Game
                {
                    Name = "FarCry",
                    Description = "Far cry info",
                    Image = "https://img.championat.com/news/big/n/h/chto-my-znaem-o-far-cry-6-zvezda-seriala-vo-vse-tjazhkie-ogromnyj-mir-i-kambek-vaasa_1594481106447280664.jpg",
                    Price = 34,
                    Available = 3,
                    Year = 2018,
                    Genre = genres.FirstOrDefault(x=>x.Name == "Shooter"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "Ubisoft")
                },
                new Game
                {
                    Name = "Assasins Creed",
                    Description = "AC info",
                    Image = "https://www.hdwallpaper.nu/wp-content/uploads/2018/03/assassins_creed_rogue-18.jpg",
                    Price = 54,
                    Available = 4,
                    Year = 2018,
                    Genre = genres.FirstOrDefault(x=>x.Name == "Action"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "Ubisoft")
                },
                 new Game
                {
                    Name = "GTA 5",
                    Description = "Far cry info",
                    Image = "https://cdn.images.express.co.uk/img/dynamic/143/590x/GTA-5-Rockstar-Online-Rockstar-920931.jpg",
                    Price = 84,
                    Available = 2,
                    Year = 2019,
                    Genre = genres.FirstOrDefault(x=>x.Name == "RPG"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "RockStar")
                },
                 new Game
                {
                    Name = "FIFA",
                    Description = "Far cry info",
                    Image = "https://media.contentapi.ea.com/content/www-easports/ru_RU/fifa/ultimate-team/news/2017/fut-online-match-modes/_jcr_content/imageShare.img.jpg",
                    Price = 34,
                    Available = 5,
                    Year = 2020,
                    Genre = genres.FirstOrDefault(x=>x.Name == "Sport Simulator"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "EA")
                },
                 new Game
                {
                    Name = "NFS",
                    Description = "Far cry info",
                    Image = "https://img.youtube.com/vi/9ewiJJe_nYI/maxresdefault.jpg",
                    Price = 134,
                    Available = 6,
                    Year = 2009,
                    Genre = genres.FirstOrDefault(x=>x.Name == "Rasing"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "Ghost Games")
                },
                   new Game
                {
                    Name = "Witcher III",
                    Description = "Witcher info",
                    Image = "https://assets.hardwarezone.com/img/2019/11/the-witcher_0.jpg",
                    Price = 200,
                    Available = 7,
                    Year = 2009,
                    Genre = genres.FirstOrDefault(x=>x.Name == "Action"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "CD Project")
                }
            };

            context.Genres.AddRange(genres);
            context.Developers.AddRange(developers);
            context.Games.AddRange(games);

            context.SaveChanges();
        }
    }

}
