using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace AlausDaryklosGamybosValdymoSistema.Models
{
    public class Alaus_daryklos_gamybos_valdymas_DBContext : IdentityDbContext<Vartotojas, Pareigybe,Guid>
    {
        public Alaus_daryklos_gamybos_valdymas_DBContext(DbContextOptions<Alaus_daryklos_gamybos_valdymas_DBContext> options)
    : base(options)
        { }
        public virtual DbSet<GamybosInstrukcija> GamybosInstrukcija { get; set; }
        public virtual DbSet<Ingredientas> Ingredientas { get; set; }
        public virtual DbSet<IngredientoPrasymas> IngredientoPrasymas { get; set; }
    public virtual DbSet<IngredientoRezervacija> IngredientoRezervacija { get; set; }
    public virtual DbSet<ResursaiUzsakymui> LaisviResursaiUzsakymui { get; set; }
    public virtual DbSet<IngredientoReceptas> IngredientoReceptas { get; set; }
        public virtual DbSet<Klientas> Klientas { get; set; }
        public virtual DbSet<KlientoUzsakymoAplankas> KlientoUzsakymoAplankas { get; set; }
        public virtual DbSet<LaisviResursai> LaisviResursai { get; set; }
        //public virtual DbSet<Pareigybe> Pareigybe { get; set; }
        public virtual DbSet<Sutartis> Sutartis { get; set; }
        public virtual DbSet<TechnikosPrietaisas> TechnikosPrietaisas { get; set; }
        public virtual DbSet<TechnikosPrietaisoPatarimas> TechnikosPrietaisoPatarimas { get; set; }
        public virtual DbSet<TechnikosPrietaisoPrasymas> TechnikosPrietaisoPrasymas { get; set; }
    public virtual DbSet<TechnikosPrietaisoRezervacija> TechnikosPrietaisoRezervacija { get; set; }
    public virtual DbSet<Tiekejas> Tiekejas { get; set; }
        //public virtual DbSet<Vartotojas> Vartotojas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-BV01DO0\DEVSQLSERVER2017;Database=Alaus_daryklos_gamybos_valdymas_DB;TreatTinyAsBoolean=false");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GamybosInstrukcija>(entity =>
            {
                entity.HasKey(e => new { e.KlientoUzsakymoId, e.InstrukcijosId })
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.KlientoUzsakymoId)
                    .HasName("TURI_FK");

                entity.Property(e => e.KlientoUzsakymoId)
                    .HasColumnName("KlientoUzsakymoID")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.InstrukcijosId)
                    .HasColumnName("InstrukcijosID")
                    .HasColumnType("numeric(6, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.BrandinimoLaikas)
                    .HasColumnName("Brandinimo laikas")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Busena)
                    .IsRequired()
                    .HasColumnType("Busena");

              entity.Property(e => e.RecepturaParuosta)
                    .IsRequired()
                    .HasColumnType("Busena");

              entity.Property(e => e.TechnikosPatarimaiParuosti)
                    .IsRequired()
                    .HasColumnType("Busena");

              entity.Property(e => e.GamybosTerminas).IsRequired()
              .HasColumnType("datetime");
            });

            modelBuilder.Entity<Ingredientas>(entity =>
            {
                entity.HasKey(e => e.IngredientoId)
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.FormosId)
                    .HasName("RELATIONSHIP_24_FK");

                entity.Property(e => e.IngredientoId)
                    .HasColumnName("IngredientoID")
                    .HasColumnType("numeric(6, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FormosId)
                    .HasColumnName("FormosID")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.IngredientoPavadinimas)
                    .IsRequired()
                    .HasColumnName("Ingrediento pavadinimas")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Formos)
                    .WithMany(p => p.Ingredientas)
                    .HasForeignKey(d => d.FormosId)
                    .HasConstraintName("FK_INGREDIE_RELATIONS_LAISVIRE");
            });

            modelBuilder.Entity<IngredientoPrasymas>(entity =>
            {
                entity.HasKey(e => new { e.KlientoUzsakymoId, e.IngredientoId })
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.IngredientoId)
                    .HasName("TURI9_FK");

                entity.HasIndex(e => e.KlientoUzsakymoId)
                    .HasName("TURI8_FK");

                entity.Property(e => e.KlientoUzsakymoId)
                    .HasColumnName("KlientoUzsakymoID")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.IngredientoId)
                    .HasColumnName("IngredientoID")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Busena)
                    .IsRequired()
                    .HasColumnType("Busena");

                entity.Property(e => e.IngredientoKiekis).HasColumnName("Ingrediento kiekis");

                entity.HasOne(d => d.Ingrediento)
                    .WithMany(p => p.IngredientoPrasymas)
                    .HasForeignKey(d => d.IngredientoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INGREDIE_TURI9_INGREDIE");

                entity.HasOne(d => d.KlientoUzsakymo)
                    .WithMany(p => p.IngredientoPrasymas)
                    .HasForeignKey(d => d.KlientoUzsakymoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INGREDIE_TURI8_KLIENTOU");
            });



      modelBuilder.Entity<IngredientoRezervacija>(entity =>
      {
        entity.HasKey(e => new { e.KlientoUzsakymoId, e.IngredientoId })
            .ForSqlServerIsClustered(false);

        entity.HasIndex(e => e.IngredientoId)
            .HasName("TuriIngr");

        entity.HasIndex(e => e.KlientoUzsakymoId)
            .HasName("TuriKlientoUzsakymoAplanka");

        entity.Property(e => e.KlientoUzsakymoId)
            .HasColumnName("KlientoUzsakymoID")
            .HasColumnType("numeric(6, 0)");

        entity.Property(e => e.IngredientoId)
            .HasColumnName("IngredientoID")
            .HasColumnType("numeric(6, 0)");



        entity.Property(e => e.IngredientoKiekis).HasColumnName("IngredientoKiekis");

        entity.HasOne(d => d.Ingrediento)
            .WithMany(p => p.IngredientoRezervacija)
            .HasForeignKey(d => d.IngredientoId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__Ingredien__Ingre__2EDAF651");

        entity.HasOne(d => d.KlientoUzsakymo)
            .WithMany(p => p.IngredientoRezervacija)
            .HasForeignKey(d => d.KlientoUzsakymoId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__Ingredien__Klien__29221CFB");
      });


      modelBuilder.Entity<IngredientoReceptas>(entity =>
            {
                entity.HasKey(e => new { e.KlientoUzsakymoId, e.InstrukcijosId, e.IngredientoId })
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.IngredientoId)
                    .HasName("TURI6_FK");

                entity.HasIndex(e => new { e.KlientoUzsakymoId, e.InstrukcijosId })
                    .HasName("TURI7_FK");

                entity.Property(e => e.KlientoUzsakymoId)
                    .HasColumnName("KlientoUzsakymoID")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.InstrukcijosId)
                    .HasColumnName("InstrukcijosID")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.IngredientoId)
                    .HasColumnName("IngredientoID")
                    .HasColumnType("numeric(6, 0)");

              entity.Property(e => e.FermentacijosLaikas)
                  .HasColumnName("Fermentacijos laikas")
                  .HasMaxLength(50);

                entity.Property(e => e.IngredientoKiekis).HasColumnName("Ingrediento kiekis");

              entity.Property(e => e.VirimoLaikas)
                  .HasColumnName("Virimo laikas")
                 .HasMaxLength(50);

                entity.HasOne(d => d.Ingrediento)
                    .WithMany(p => p.IngredientoReceptas)
                    .HasForeignKey(d => d.IngredientoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INGREDIE_TURI6_INGREDIE");

                entity.HasOne(d => d.GamybosInstrukcija)
                    .WithMany(p => p.IngredientoReceptas)
                    .HasForeignKey(d => new { d.KlientoUzsakymoId, d.InstrukcijosId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INGREDIE_TURI7_GAMYBOSI");
            });

            modelBuilder.Entity<Klientas>(entity =>
            {
                entity.HasKey(e => e.KlientoId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.KlientoId)
                    .HasColumnName("KlientoID")
                    .HasColumnType("numeric(6, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.KlientoPavadinimas)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

              entity.Property(e => e.ImonesKodas)
                                  .HasMaxLength(9)
                                  .IsUnicode(false);

              entity.Property(e => e.KontaktinisNR)
                    .HasMaxLength(12)
                    .IsUnicode(false);

              entity.Property(e => e.Adresas)
                   .HasMaxLength(50)
                   .IsUnicode(false);
            });

            modelBuilder.Entity<KlientoUzsakymoAplankas>(entity =>
            {
                entity.HasKey(e => e.KlientoUzsakymoId)
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.KlientoId)
                    .HasName("TURI15_FK");

                entity.Property(e => e.KlientoUzsakymoId)
                    .HasColumnName("KlientoUzsakymoID")
                    .HasColumnType("numeric(6, 0)")
                    .ValueGeneratedOnAdd();


              entity.Property(e => e.Busena)
                    .IsRequired()
                    .HasColumnType("Busena");

                entity.Property(e => e.KlientoId)
                    .HasColumnName("KlientoID")
                    .HasColumnType("numeric(6, 0)");


              entity.Property(e => e.Terminas).HasColumnType("datetime");

              entity.Property(e => e.ArchyvavimoLaikas).HasColumnType("datetime");

              entity.Property(e => e.Archyvavimo_Komentaras).HasMaxLength(500);

              entity.Property(e => e.PrasymasZaliavomsReikalingas).HasMaxLength(1);

              entity.Property(e => e.PrasymasTechnikaiReikalingas).HasMaxLength(1);

              entity.HasOne(d => d.Kliento)
                  .WithMany(p => p.KlientoUzsakymoAplankas)
                  .HasForeignKey(d => d.KlientoId);


              entity.HasOne(d => d.GamybosInstrukcija)
              .WithOne(p => p.KlientoUzsakymo)
                   .HasForeignKey<GamybosInstrukcija>(d => d.KlientoUzsakymoId)
             .OnDelete(DeleteBehavior.ClientSetNull).
             HasConstraintName("FK_GAMYBOSI_TURI_KLIENTOU"); 

              entity.HasOne(d => d.Sutartis)
              .WithOne(p => p.KlientoUzsakymo)
                   .HasForeignKey<Sutartis>(d => d.KlientoUzsakymoId)
             .OnDelete(DeleteBehavior.ClientSetNull).
             HasConstraintName("FK_SUTARTIS_TURI14_KLIENTOU");
            });

            modelBuilder.Entity<LaisviResursai>(entity =>
            {
                entity.HasKey(e => e.FormosId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.FormosId)
                    .HasColumnName("FormosID")
                    .HasColumnType("numeric(6, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LaisvuResursuKodas)
                    .IsRequired()
                    .HasColumnType("char(1)");

              entity.Property(e => e.ResursoPavadinimas)
                    .IsRequired()
                    .HasColumnName("ResursoPavadinimas")
                    .HasMaxLength(50);
            });
      modelBuilder.Entity<ResursaiUzsakymui>(entity =>
      {
        entity.HasKey(e => new { e.KlientoUzsakymoId, e.FormosId })
            .ForSqlServerIsClustered(false);

        entity.HasIndex(e => e.FormosId)
            .HasName("index_FormosTuri");

        entity.HasIndex(e => e.KlientoUzsakymoId)
            .HasName("index_KlientoUzsakymoTuri");

        entity.Property(e => e.KlientoUzsakymoId)
            .HasColumnName("KlientoUzsakymoID")
            .HasColumnType("numeric(6, 0)");

        entity.Property(e => e.LaisvuResursuTipas)
                    .IsRequired()
                    .HasColumnType("char(1)");


        entity.Property(e => e.FormosId)
            .HasColumnName("FormosID")
            .HasColumnType("numeric(6, 0)");

        entity.HasOne(d => d.ResursaiLaisvi)
            .WithMany(p => p.LaisviResursaiUzsakymui)
            .HasForeignKey(d => d.FormosId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__LaisviRes__Formo__13BCEBC1");

        entity.HasOne(d => d.UzsakymasKliento)
            .WithMany(p => p.LaisviResursaiUzsakymui)
            .HasForeignKey(d => d.KlientoUzsakymoId);
      });

      modelBuilder.Entity<Sutartis>(entity =>
            {
                entity.HasKey(e => e.SutartiesId);

                entity.HasIndex(e => e.KlientoId)
                    .HasName("TURI13_FK");

                entity.HasIndex(e => e.KlientoUzsakymoId)
                    .HasName("TURI14_FK");

                entity.HasIndex(e => e.TiekejoId)
                    .HasName("TURI12_FK");

                entity.Property(e => e.SutartiesId)
                    .HasColumnName("SutartiesID")
                    .HasColumnType("numeric(6, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.KlientoId)
                    .HasColumnName("KlientoID")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.KlientoUzsakymoId)
                    .HasColumnName("KlientoUzsakymoID")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.SutartiesPasirasymoData).HasColumnType("datetime");

                entity.Property(e => e.SutartiesTerminas).HasColumnType("datetime");

              entity.Property(e => e.FailoPavadinimas)
                   .IsRequired()
                   .HasColumnName("FailoPavadinimas")
                   .HasMaxLength(100);

              

              entity.Property(e => e.TiekejoId)
                    .HasColumnName("TiekejoID")
                    .HasColumnType("numeric(6, 0)");

                entity.HasOne(d => d.Kliento)
                    .WithMany(p => p.Sutartis)
                    .HasForeignKey(d => d.KlientoId)
                    .HasConstraintName("FK_SUTARTIS_TURI13_KLIENTAS");

                entity.HasOne(d => d.Tiekejo)
                    .WithMany(p => p.Sutartis)
                    .HasForeignKey(d => d.TiekejoId)
                    .HasConstraintName("FK_SUTARTIS_TURI12_TIEKEJAS");
            });

            modelBuilder.Entity<TechnikosPrietaisas>(entity =>
            {
                entity.HasKey(e => e.TechnikosPrietaisoId)
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.FormosId)
                    .HasName("TURI55_FK");

                entity.Property(e => e.TechnikosPrietaisoId)
                    .HasColumnName("TechnikosPrietaisoID")
                    .HasColumnType("numeric(6, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FormosId)
                    .HasColumnName("FormosID")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.PrietaisoPavadinimas)
                    .IsRequired()
                    .HasColumnName("Prietaiso pavadinimas")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Formos)
                    .WithMany(p => p.TechnikosPrietaisas)
                    .HasForeignKey(d => d.FormosId)
                    .HasConstraintName("FK_TECHNIKO_TURI55_LAISVIRE");
            });

            modelBuilder.Entity<TechnikosPrietaisoPatarimas>(entity =>
            {
                entity.HasKey(e => new { e.TechnikosPrietaisoId, e.KlientoUzsakymoId, e.InstrukcijosId })
                    .ForSqlServerIsClustered(false);

                entity.ToTable("Technikos prietaiso patarimas");

                entity.HasIndex(e => e.TechnikosPrietaisoId)
                    .HasName("TURI5_FK");

                entity.HasIndex(e => new { e.KlientoUzsakymoId, e.InstrukcijosId })
                    .HasName("TURI4_FK");

                entity.Property(e => e.TechnikosPrietaisoId)
                    .HasColumnName("TechnikosPrietaisoID")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.KlientoUzsakymoId)
                    .HasColumnName("KlientoUzsakymoID")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.InstrukcijosId)
                    .HasColumnName("InstrukcijosID")
                    .HasColumnType("numeric(6, 0)");

              entity.Property(e => e.PrietaisoKiekis).IsRequired()
              .HasColumnName("PrietaisoKiekis");

              entity.Property(e => e.PrietaisoKomentaras)
                    .HasColumnName("Prietaiso komentaras")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.TechnikosPrietaiso)
                    .WithMany(p => p.TechnikosPrietaisoPatarimas)
                    .HasForeignKey(d => d.TechnikosPrietaisoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TECHNIKO_TURI5_TECHNIKO");

                entity.HasOne(d => d.GamybosInstrukcija)
                    .WithMany(p => p.TechnikosPrietaisoPatarimas)
                    .HasForeignKey(d => new { d.KlientoUzsakymoId, d.InstrukcijosId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TECHNIKO_TURI4_GAMYBOSI");
            });

            modelBuilder.Entity<TechnikosPrietaisoPrasymas>(entity =>
            {
                entity.HasKey(e => new { e.KlientoUzsakymoId, e.TechnikosPrietaisoId })
                    .ForSqlServerIsClustered(false);

                entity.ToTable("Technikos prietaiso prasymas");

                entity.HasIndex(e => e.KlientoUzsakymoId)
                    .HasName("TURI11_FK");

                entity.HasIndex(e => e.TechnikosPrietaisoId)
                    .HasName("TURI10_FK");

                entity.Property(e => e.KlientoUzsakymoId)
                    .HasColumnName("KlientoUzsakymoID")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.TechnikosPrietaisoId)
                    .HasColumnName("TechnikosPrietaisoID")
                    .HasColumnType("numeric(6, 0)");

                entity.Property(e => e.Busena)
                    .IsRequired()
                    .HasColumnType("Busena");

                entity.Property(e => e.PrietaisoKiekis).HasColumnName("Prietaiso kiekis");

                entity.HasOne(d => d.KlientoUzsakymo)
                    .WithMany(p => p.TechnikosPrietaisoPrasymas)
                    .HasForeignKey(d => d.KlientoUzsakymoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TECHNIKO_TURI11_KLIENTOU");

                entity.HasOne(d => d.TechnikosPrietaiso)
                    .WithMany(p => p.TechnikosPrietaisoPrasymas)
                    .HasForeignKey(d => d.TechnikosPrietaisoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TECHNIKO_TURI10_TECHNIKO");
            });

      modelBuilder.Entity<TechnikosPrietaisoRezervacija>(entity =>
      {
        entity.HasKey(e => new { e.KlientoUzsakymoId, e.TechnikosPrietaisoId })
            .ForSqlServerIsClustered(false);

        entity.ToTable("TechnikosPrietaisoRezervacija");

        entity.HasIndex(e => e.KlientoUzsakymoId)
            .HasName("TuriKlientoUzsakymoAplanka");

        entity.HasIndex(e => e.TechnikosPrietaisoId)
            .HasName("TuriTech");

        entity.Property(e => e.KlientoUzsakymoId)
            .HasColumnName("KlientoUzsakymoID")
            .HasColumnType("numeric(6, 0)");

        entity.Property(e => e.TechnikosPrietaisoId)
            .HasColumnName("TechnikosPrietaisoID")
            .HasColumnType("numeric(6, 0)");


        entity.Property(e => e.PrietaisoKiekis).HasColumnName("PrietaisoKiekis");

        entity.HasOne(d => d.KlientoUzsakymo)
            .WithMany(p => p.TechnikosPrietaisoRezervacija)
            .HasForeignKey(d => d.KlientoUzsakymoId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__Technikos__Klien__2BFE89A6");

        entity.HasOne(d => d.TechnikosPrietaiso)
            .WithMany(p => p.TechnikosPrietaisoRezervacija)
            .HasForeignKey(d => d.TechnikosPrietaisoId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__Technikos__Techn__2CF2ADDF");
      });


      modelBuilder.Entity<Tiekejas>(entity =>
            {
                entity.HasKey(e => e.TiekejoId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.TiekejoId)
                    .HasColumnName("TiekejoID")
                    .HasColumnType("numeric(6, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TiekejoPavadinimas)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TiekejoTtipas)
                    .IsRequired()
                    .HasColumnType("char(1)");

              entity.Property(e => e.ImonesKodas)
                                 .HasMaxLength(9)
                                 .IsUnicode(false);

              entity.Property(e => e.KontaktinisNR)
                    .HasMaxLength(12)
                    .IsUnicode(false);

              entity.Property(e => e.Adresas)
                   .HasMaxLength(50)
                   .IsUnicode(false);
            });

           /* modelBuilder.Entity<Vartotojas>(entity =>
            {
                entity.HasKey(e => e.VartotojoId)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.VartotojoId)
                    .HasColumnName("VartotojoID")
                    .HasColumnType("numeric(6, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Pavarde)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrisijungimoVardas)
                   
                    .HasColumnName("Prisijungimo_Vardas")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Vardas)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });*/
        }
    }
}
