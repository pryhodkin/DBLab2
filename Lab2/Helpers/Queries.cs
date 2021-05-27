using static Lab2.Controllers.RequestsController;

namespace Lab2.Helpers
{
    public static class Queries
    {
        public static GetViewModel First = new GetViewModel
        {
            Query = @"
SELECT Artist.Id,
       Artist.Name,
       Artist.NickName,
       Artist.LabelId
FROM

Artists AS Artist
INNER JOIN
Albums AS Album
ON Artist.Id = Album.ArtistId

INNER JOIN
Songs AS Song
ON Album.Id = Song.AlbumId

GROUP BY Artist.Id,
         Artist.Name,
         Artist.NickName,
         Artist.LabelId,
         Album.Id
HAVING COUNT(DISTINCT Song.Id) > @X;
            ",
            Description = "Всі артисти, у яких є альбом з більш ніж X піснями."
        };
        public static GetViewModel Second = new GetViewModel
        {
            Query = @"
SELECT U.Id,
       U.Name,
       U.Surname,
       U.NickName FROM

Users AS U
INNER JOIN
Subscriptions AS Subscription
ON U.Id = Subscription.UserId

GROUP BY U.Id,
         U.Name,
         U.Surname,
         U.NickName
HAVING COUNT(DISTINCT Subscription.Id) > @X;
            ",
            Description = "Всі користувачі з більш ніж Х підписками."
        };
        public static GetViewModel Third = new GetViewModel
        {
            Query = @"
SELECT LabelAlbums.LabelId AS Id,
	   LabelName AS Name FROM

	  (SELECT Album.Id AS AlbumId,
		Label.Id AS LabelId,
		Label.Name AS LabelName
	  FROM
		Labels AS Label
		INNER JOIN
		Songs AS Song
		ON Song.LabelId = Label.Id
		
		INNER JOIN
		Albums AS Album
		ON Album.Id = Song.AlbumId
		
		GROUP BY Album.Id,
		         Label.Id,
				 Label.Name
		
		HAVING (SELECT COUNT(S.Id) FROM Songs AS S
		        WHERE S.AlbumId = Album.Id) = COUNT(Song.Id)) AS LabelAlbums

GROUP BY LabelAlbums.LabelId,
         LabelAlbums.LabelName
HAVING COUNT(LabelAlbums.AlbumId) > @X;
            ",
            Description = "Лейбли з більш ніж Х альбомами (Кожна пісня записана в цьому альбомі належить цьому лейблу)."
        };
        public static GetViewModel Fourth = new GetViewModel
        {
            Query = @"
SELECT Label.Id,
       Label.Name FROM

Labels AS Label
LEFT OUTER JOIN
Songs AS Song
ON Song.LabelId = Label.Id

GROUP BY Label.Id,
         Label.Name

HAVING COUNT(Song.Id) < @X;
            ",
            Description = "Лейбли з менш ніж Х піснями."
        };
        public static GetViewModel Fifth = new GetViewModel
        {
            Query = @"
SELECT Song.Id,
       Song.Name,
       Song.MainArtistId,
       Song.SecondaryArtistId,
       Song.AlbumId,
       Song.LabelId FROM

Songs AS Song
INNER JOIN
Annotations AS Annotation
ON Annotation.SongId = Song.Id

INNER JOIN
Users AS U
ON Annotation.AuthorId = U.Id

GROUP BY Song.Id,
         Song.Name,
         Song.MainArtistId,
         Song.SecondaryArtistId,
         Song.AlbumId,
         Song.LabelId

HAVING COUNT(DISTINCT U.Id) > @X;
            ",
            Description = "Пісні, для яких залишили анотації більш ніж Х користувачів."
        };

        public static GetViewModel Sixth = new GetViewModel
        {
            Query = @"
SELECT U.Id,
       U.Name,
       U.Surname,
       U.NickName FROM

Users AS U
WHERE (SELECT COUNT(*) AS C
       FROM ((SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription
              WHERE Subscription.UserId = U.Id)
             EXCEPT
             (SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription
              WHERE Subscription.UserId = @X)) AS Al1) = 0
         AND
      (SELECT COUNT(*) AS C
       FROM ((SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription
              WHERE Subscription.UserId = @X)
             EXCEPT
             (SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription
             WHERE Subscription.UserId = U.Id)) AS Al2) = 0;",
            Description = "Список всіх користувачів, що мають однаковий набір підписок з користувачем із заданим id."
        };

        public static GetViewModel Seventh = new GetViewModel
        {
            Query = @"
SELECT U.Id,
       U.Name,
       U.Surname,
       U.NickName FROM

Users AS U
WHERE (SELECT COUNT(*) AS C
       FROM ((SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription
              WHERE Subscription.UserId = U.Id)
             EXCEPT
             (SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription)) AS Al1) = 0
         AND
      (SELECT COUNT(*) AS C
       FROM ((SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription)
             EXCEPT
             (SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription
             WHERE Subscription.UserId = U.Id)) AS Al2) = 0;
            ",
            Description = "Всі користувачі, що підписані на всіх артистів."
        };

        public static GetViewModel Eighth = new GetViewModel
        {
            Query = @"
SELECT U.Id,
       U.Name,
       U.Surname,
       U.NickName FROM

Users AS U
WHERE (SELECT COUNT(*) AS C
       FROM ((SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription
              WHERE Subscription.UserId = U.Id)
             EXCEPT
             (SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription)) AS Al1) = 0
         AND
      (SELECT COUNT(*) AS C
       FROM ((SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription)
             EXCEPT
             (SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription
             WHERE Subscription.UserId = U.Id)) AS Al2) = 0;
            ",
            Description = "Всі користувачі, що підписані на всіх артистів."
        };

        public static GetViewModel Nineth => new GetViewModel
        {
            Query = @"
SELECT * FROM
Labels AS Label
WHERE EXISTS (SELECT Song.Id FROM
                    Songs AS Song
                    INNER JOIN
                    Annotations AS Annotation
                    ON Song.Id = Annotation.SongId AND Song.LabelId = Label.Id
                    GROUP BY Song.Id
                    HAVING COUNT(DISTINCT Annotation.AuthorId) = 1);
            ",
            Description = "Всі лейбли, що мають пісні з анотаціями лише від користувача X."
        };

        public static GetViewModel Tenth => new GetViewModel
        {
            Query = @"
SELECT U.Id,
       U.Name,
       U.Surname,
       U.NickName FROM

Users AS U
WHERE (SELECT COUNT(*) AS C
       FROM ((SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription
              WHERE Subscription.UserId = U.Id)
             EXCEPT
             (SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription
              WHERE Subscription.UserId = @X)) AS Al1) > 0
         AND
      (SELECT COUNT(*) AS C
       FROM ((SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription
              WHERE Subscription.UserId = @X)
             EXCEPT
             (SELECT Subscription.ArtistId AS S FROM Subscriptions AS Subscription
             WHERE Subscription.UserId = U.Id)) AS Al2) = 0;
            ",
            Description = "Всі користувачі, що мають слухають усіх артистів яких слухає користувач X, та обов'язково ще когось."
        };
    }
}
