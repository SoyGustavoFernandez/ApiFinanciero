using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataBackend.Models;

public partial class RetoBackendContext : DbContext
{
    public RetoBackendContext()
    {
    }

    public RetoBackendContext(DbContextOptions<RetoBackendContext> options) : base(options)
    {
    }

    public virtual DbSet<TblCliente> TblClientes { get; set; }

    public virtual DbSet<TblCuentum> TblCuenta { get; set; }

    public virtual DbSet<TblMovimiento> TblMovimientos { get; set; }

    public virtual DbSet<TblParametro> TblParametros { get; set; }

    public virtual DbSet<TblPersona> TblPersonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=GUSTAVO-FERNAND\\LOCALHOST;Database=RetoBackend;User=sa;Password=alumno;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCliente>(entity =>
        {
            entity.HasKey(e => e.NIdCliente);

            entity.ToTable("TblCliente", tb => tb.HasComment("Tabla donde se registran los datos de los clientes."));

            entity.Property(e => e.NIdCliente)
                .HasComment("Id del registro.")
                .HasColumnName("nIdCliente");
            entity.Property(e => e.LVigente)
                .HasDefaultValueSql("((1))")
                .HasComment("Estado del registro.")
                .HasColumnName("lVigente");
            entity.Property(e => e.NIdPersona)
                .HasComment("Id de la tabla TblPersona.")
                .HasColumnName("nIdPersona");
            entity.Property(e => e.SClave)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Contraseña del Cliente.")
                .HasColumnName("sClave");

            entity.HasOne(d => d.NIdPersonaNavigation).WithMany(p => p.TblClientes)
                .HasForeignKey(d => d.NIdPersona)
                .HasConstraintName("FK_TblCliente_TblPersona");
        });

        modelBuilder.Entity<TblCuentum>(entity =>
        {
            entity.HasKey(e => e.NIdCuenta);

            entity.ToTable(tb => tb.HasComment("Tabla donde se registran los datos de las cuentas."));

            entity.Property(e => e.NIdCuenta)
                .HasComment("Id del registro.")
                .HasColumnName("nIdCuenta");
            entity.Property(e => e.LVigente)
                .HasDefaultValueSql("((1))")
                .HasComment("Estado del registro.")
                .HasColumnName("lVigente");
            entity.Property(e => e.NIdCliente)
                .HasComment("Id de la tabla TblCliente.")
                .HasColumnName("nIdCliente");
            entity.Property(e => e.NSaldoActual)
                .HasComment("Saldo actual de la cuenta.")
                .HasColumnType("decimal(14, 2)")
                .HasColumnName("nSaldoActual");
            entity.Property(e => e.NSaldoInicial)
                .HasComment("Saldo inicial de la cuenta.")
                .HasColumnType("decimal(14, 2)")
                .HasColumnName("nSaldoInicial");
            entity.Property(e => e.NTipoCuenta)
                .HasComment("Tipo de cuenta - TblParametros - 4: Ahorros / 5: Corriente.")
                .HasColumnName("nTipoCuenta");
            entity.Property(e => e.SNumCuenta)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasComment("Número de cuenta.")
                .HasColumnName("sNumCuenta");

            entity.HasOne(d => d.NIdClienteNavigation).WithMany(p => p.TblCuenta)
                .HasForeignKey(d => d.NIdCliente)
                .HasConstraintName("FK_TblCuenta_TblCliente");
        });

        modelBuilder.Entity<TblMovimiento>(entity =>
        {
            entity.HasKey(e => e.NIdMovimiento);

            entity.ToTable(tb => tb.HasComment("Tabla donde se registran los movimientos de las cuentas."));

            entity.Property(e => e.NIdMovimiento)
                .HasComment("Id del registro.")
                .HasColumnName("nIdMovimiento");
            entity.Property(e => e.DFechaMovimiento)
                .HasComment("Fecha del Movimiento.")
                .HasColumnType("datetime")
                .HasColumnName("dFechaMovimiento");
            entity.Property(e => e.LVigente)
                .HasDefaultValueSql("((1))")
                .HasComment("Estado del registro.")
                .HasColumnName("lVigente");
            entity.Property(e => e.NIdCuenta)
                .HasComment("Id de la tabla TblCuenta.")
                .HasColumnName("nIdCuenta");
            entity.Property(e => e.NSaldoInicial)
                .HasComment("Saldo antes del movimiento.")
                .HasColumnType("decimal(14, 2)")
                .HasColumnName("nSaldoInicial");
            entity.Property(e => e.NTipoMovimiento)
                .HasComment("Tipo de movimiento - TblParametros - 6: Retiro / 7: Depósito.")
                .HasColumnName("nTipoMovimiento");
            entity.Property(e => e.NValor)
                .HasComment("Valor del movimiento.")
                .HasColumnType("decimal(14, 2)")
                .HasColumnName("nValor");

            entity.HasOne(d => d.NIdCuentaNavigation).WithMany(p => p.TblMovimientos)
                .HasForeignKey(d => d.NIdCuenta)
                .HasConstraintName("FK_TblMovimientos_TblCuenta");
        });

        modelBuilder.Entity<TblParametro>(entity =>
        {
            entity.HasKey(e => e.NIdParametro);

            entity.ToTable("TblParametro", tb => tb.HasComment("Tabla donde se registran los parámetros configurables."));

            entity.Property(e => e.NIdParametro)
                .HasComment("Id del registro.")
                .HasColumnName("nIdParametro");
            entity.Property(e => e.LVigente)
                .HasDefaultValueSql("((1))")
                .HasComment("Estado del registro.")
                .HasColumnName("lVigente");
            entity.Property(e => e.NGrupo)
                .HasComment("Número del grupo del parámetro.")
                .HasColumnName("nGrupo");
            entity.Property(e => e.NParametro)
                .HasComment("Valor del parámetro, se utilizará para hacer join en todas las demás tablas.")
                .HasColumnName("nParametro");
            entity.Property(e => e.NValor)
                .HasComment("Valor numérico del parámetro.")
                .HasColumnName("nValor");
            entity.Property(e => e.SNombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Nombre del parámetro.")
                .HasColumnName("sNombre");
            entity.Property(e => e.SValor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Valor en texto del parámetro.")
                .HasColumnName("sValor");
        });

        modelBuilder.Entity<TblPersona>(entity =>
        {
            entity.HasKey(e => e.NIdPersona);

            entity.ToTable("TblPersona", tb => tb.HasComment("Tabla donde se registran los datos de las personas."));

            entity.Property(e => e.NIdPersona)
                .HasComment("Id del registro.")
                .HasColumnName("nIdPersona");
            entity.Property(e => e.CDireccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Dirección de la persona.")
                .HasColumnName("cDireccion");
            entity.Property(e => e.CIdentificacion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("Número de identificación de la persona.")
                .HasColumnName("cIdentificacion");
            entity.Property(e => e.CTelefono)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasComment("Teléfono de la persona.")
                .HasColumnName("cTelefono");
            entity.Property(e => e.LVigente)
                .HasDefaultValueSql("((1))")
                .HasComment("Estado del registro.")
                .HasColumnName("lVigente");
            entity.Property(e => e.NEdad)
                .HasComment("Edad de la persona.")
                .HasColumnName("nEdad");
            entity.Property(e => e.NGenero)
                .HasComment("Género de la persona TblParametros - 1: Masculino / 2: Femenino.")
                .HasColumnName("nGenero");
            entity.Property(e => e.SNombres)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Nombres completos de la persona.")
                .HasColumnName("sNombres");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
