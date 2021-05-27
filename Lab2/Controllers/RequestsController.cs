using Lab2.Helpers;
using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab2.Controllers
{
    public class RequestsController : Controller
    {
        IConfiguration _config;
        public RequestsController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region First query

        [HttpPost]
        public async Task<IActionResult> First(FirstArguments args)
        {
            var result = new List<Artist>();
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new SqlCommand(Queries.First.Query, connection);

                command.Parameters.Add(new SqlParameter("X", args.X));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new Artist
                        {
                            Id = (int)reader.GetValue(0),
                            Name = reader.GetValue(1).ToString(),
                            NickName = reader.GetValue(2).ToString(),
                            LabelId = reader.GetValue(3) as int?,
                        });
                    }
                }

                reader.Close();
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult First()
        {
            return View(Queries.First);
        }

        #endregion

        #region Second query

        [HttpPost]
        public async Task<IActionResult> Second(SecondArguments args)
        {
            var result = new List<User>();
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var command = new SqlCommand(Queries.Second.Query, connection);
                command.Parameters.Add(new SqlParameter("X", args.X));

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new User
                        {
                            Id = (int)reader.GetValue(0),
                            Name = reader.GetValue(1).ToString(),
                            Surname = reader.GetValue(2).ToString(),
                            NickName = reader.GetValue(3).ToString()
                        });
                    }
                }

                reader.Close();
            }
            return View(result);
        }
        [HttpGet]
        public IActionResult Second()
        {
            return View(Queries.Second);
        }

        #endregion

        #region Third query

        [HttpPost]
        public async Task<IActionResult> Third(ThirdArguments args)
        {
            var result = new List<Label>();
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new SqlCommand(Queries.Third.Query, connection);

                command.Parameters.Add(new SqlParameter("X", args.X));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new Label
                        {
                            Id = (int)reader.GetValue(0),
                            Name = reader.GetValue(1).ToString()
                        });
                    }
                }

                reader.Close();
            }
            return View(result);
        }
        [HttpGet]
        public IActionResult Third()
        {
            return View(Queries.Third);
        }

        #endregion

        #region Fourth query

        [HttpPost]
        public async Task<IActionResult> Fourth(FourthArguments args)
        {
            var result = new List<Label>();
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new SqlCommand(Queries.Fourth.Query, connection);

                command.Parameters.Add(new SqlParameter("X", args.X));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new Label
                        {
                            Id = (int)reader.GetValue(0),
                            Name = reader.GetValue(1).ToString()
                        });
                    }
                }

                reader.Close();
            }
            return View(result);
        }
        [HttpGet]
        public IActionResult Fourth()
        {
            return View(Queries.Fourth);
        }

        #endregion

        #region Fifth query

        [HttpPost]
        public async Task<IActionResult> Fifth(FifthArguments args)
        {
            var result = new List<Song>();
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new SqlCommand(Queries.Fifth.Query, connection);

                command.Parameters.Add(new SqlParameter("X", args.X));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new Song
                        {
                            Id = (int)reader.GetValue(0),
                            Name = reader.GetValue(1).ToString(),
                            MainArtistId = (int)reader.GetValue(2),
                            SecondaryArtistId = (int)reader.GetValue(3),
                            AlbumId = (int)reader.GetValue(4),
                            LabelId = (int)reader.GetValue(5)
                        });
                    }
                }

                reader.Close();
            }
            return View(result);
        }
        [HttpGet]
        public IActionResult Fifth()
        {
            return View(Queries.Fifth);
        }

        #endregion

        #region Sixth query

        [HttpPost]
        public async Task<IActionResult> Sixth(SixthArguments args)
        {
            var result = new List<User>();
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new SqlCommand(Queries.Sixth.Query, connection);

                command.Parameters.Add(new SqlParameter("X", args.X));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new User
                        {
                            Id = (int)reader.GetValue(0),
                            Name = reader.GetValue(1).ToString(),
                            Surname = reader.GetValue(2).ToString(),
                            NickName = reader.GetValue(3).ToString()
                        });
                    }
                }

                reader.Close();
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult Sixth()
        {
            return View(Queries.Sixth);
        }

        #endregion

        #region Seventh query

        [HttpPost]
        public async Task<IActionResult> Seventh(SeventhArguments args)
        {
            var result = new List<User>();
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new SqlCommand(Queries.Seventh.Query, connection);

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new User
                        {
                            Id = (int)reader.GetValue(0),
                            Name = reader.GetValue(1).ToString(),
                            Surname = reader.GetValue(2).ToString(),
                            NickName = reader.GetValue(3).ToString()
                        });
                    }
                }

                reader.Close();
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult Seventh()
        {
            return View(Queries.Seventh);
        }

        #endregion


        #region Nineth query

        [HttpPost]
        public async Task<IActionResult> Nineth(NinethArguments args)
        {
            var result = new List<Label>();
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new SqlCommand(Queries.Nineth.Query, connection);

                command.Parameters.Add(new SqlParameter("X", args.X));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new Label
                        {
                            Id = (int)reader.GetValue(0),
                            Name = reader.GetValue(1).ToString()
                        });
                    }
                }

                reader.Close();
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult Nineth()
        {
            return View(Queries.Nineth);
        }

        #endregion

        #region Tenth query

        [HttpPost]
        public async Task<IActionResult> Tenth(NinethArguments args)
        {
            var result = new List<User>();
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new SqlCommand(Queries.Tenth.Query, connection);

                command.Parameters.Add(new SqlParameter("X", args.X));
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new User
                        {
                            Id = (int)reader.GetValue(0),
                            Name = reader.GetValue(1).ToString(),
                            Surname = reader.GetValue(2).ToString(),
                            NickName = reader.GetValue(3).ToString()
                        });
                    }
                }

                reader.Close();
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult Tenth()
        {
            return View(Queries.Tenth);
        }

        #endregion

        #region Arguments

        public class FirstArguments
        {
            public int X { get; set; }
        }

        public class SecondArguments
        {
            public int X { get; set; }
        }

        public class ThirdArguments
        {
            public int X { get; set; }
        }

        public class FourthArguments
        {
            public int X { get; set; }
        }

        public class FifthArguments
        {
            public int X { get; set; }
        }

        public class SixthArguments
        {
            public int X { get; set; }
        }

        public class SeventhArguments
        {
        }
        public class NinethArguments
        {
            public int X { get; set; }
        }

        public class TenthArguments
        {
            public int X { get; set; }
        }


        public class GetViewModel
        {
            public string Query { get; set; }
            public string Description { get; set; }
        }
        #endregion
    }
}
