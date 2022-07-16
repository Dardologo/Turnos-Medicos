using Microsoft.EntityFrameworkCore;

namespace Turnos.Models
{
    public class TurnosContext : DbContext
    {




        public virtual DbSet<Especialidad> Especialidades { get; set; }

        public virtual DbSet<Paciente> Paciente { get; set; }

        public virtual DbSet<Medico> Medicos { get; set; }

        public virtual DbSet<MedicoEspecialidad> MedicoEspecialidad { get; set; }

        public virtual DbSet<Turno>? Turnos { get; set; }
        public virtual DbSet<Login> Login { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            if (!option.IsConfigured)
            {
                option.UseSqlServer(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=Turnos; Integrated Security=true");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //definimos una pk compuesta con esos dos campos de la entidad y una restriccion entre la tabla medico y medicoEspecialidad
            //
            modelBuilder.Entity<MedicoEspecialidad>().HasKey(x => new { x.IdMedico, x.IdEspecialidad });
            //Con hasone y hasMany definimos una relacion de 1 a muchos. 1 medico tiene muchas especialidades
            //con  hasForeignKey establecemos cual va a ser el campo que va a formar parte de la fk. En este caso idMedico 
            //de la clase MedicoEspecialidad
            modelBuilder.Entity<MedicoEspecialidad>().HasOne(x => x.Medico).WithMany(p => p.MedicoEspecialidad)
                .HasForeignKey(p => p.IdMedico);

            //Aca decimos que una especialidad tiene muchos medicos
            modelBuilder.Entity<MedicoEspecialidad>().HasOne(x => x.Especialidad).WithMany(p => p.MedicoEspecialidad)
                .HasForeignKey(p => p.IdEspecialidad);

            //Paciente tiene turnos 
            modelBuilder.Entity<Turno>().HasOne(x => x.Paciente).WithMany(p => p.Turno)
               .HasForeignKey(p => p.IdPaciente);
            //Medico tiene turnos
            modelBuilder.Entity<Turno>().HasOne(x => x.Medico).WithMany(p => p.Turno)
              .HasForeignKey(p => p.IdMedico);

            modelBuilder.Entity<Login>(entidad =>
            {
                entidad.ToTable("Login");
                entidad.HasKey(l => l.LoginId);

                entidad.Property(l => l.Usuario).IsRequired();

                entidad.Property(l => l.Password).IsRequired();
            }
            );


        }

    }
}
