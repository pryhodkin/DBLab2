using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Lab2.Models
{
    public partial class LyricsServiceContext : DbContext
    {
        public LyricsServiceContext()
        {
        }

        public LyricsServiceContext(DbContextOptions<LyricsServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Annotation> Annotations { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Label> Labels { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=SURFACELAPTOP3; Database=LyricsService; Trusted_Connection=True; MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Album>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.ArtistId)
                    .HasConstraintName("FK_Albums_Artists");
            });

            modelBuilder.Entity<Annotation>(entity =>
            {
                entity.Property(e => e.Lines).HasMaxLength(50);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Annotations)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Annotations_Users");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.Annotations)
                    .HasForeignKey(d => d.SongId)
                    .HasConstraintName("FK_Annotations_Songs");
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Label)
                    .WithMany(p => p.Artists)
                    .HasForeignKey(d => d.LabelId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Artists_Labels");
            });

            modelBuilder.Entity<Label>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.Property(e => e.Lyrics)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.Songs)
                    .HasForeignKey(d => d.AlbumId)
                    .HasConstraintName("FK_Songs_Albums");

                entity.HasOne(d => d.Label)
                    .WithMany(p => p.Songs)
                    .HasForeignKey(d => d.LabelId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Songs_Labels");

                entity.HasOne(d => d.MainArtist)
                    .WithMany(p => p.SongMainArtists)
                    .HasForeignKey(d => d.MainArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Songs_Artists");

                entity.HasOne(d => d.SecondaryArtist)
                    .WithMany(p => p.SongSecondaryArtists)
                    .HasForeignKey(d => d.SecondaryArtistId)
                    .HasConstraintName("FK_Songs_Artists1");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.ArtistId)
                    .HasConstraintName("FK_Subscriptions_Artists");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Subscriptions_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
