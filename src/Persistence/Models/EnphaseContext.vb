Imports System
Imports System.Collections.Generic
Imports Microsoft.EntityFrameworkCore

Namespace Models
    Partial Public Class EnphaseContext
        Inherits DbContext

        Public Sub New()
        End Sub

        Public Sub New(options As DbContextOptions(Of EnphaseContext))
            MyBase.New(options)
        End Sub

        Public Overridable Property TCategories As DbSet(Of TCategory)

        Public Overridable Property TPowers As DbSet(Of TPower)

        Public Overridable Property VConsumeds As DbSet(Of VConsumed)

        Public Overridable Property VConsumptions As DbSet(Of VConsumption)

        Public Overridable Property VDailies As DbSet(Of Vdaily)

        Public Overridable Property VHourlies As DbSet(Of Vhourly)

        Public Overridable Property VHoursMonthlies As DbSet(Of VHoursmonthly)

        Public Overridable Property VInOuts As DbSet(Of VInOut)

        Public Overridable Property VMonthHours As DbSet(Of VMonthHour)

        Public Overridable Property VPowers As DbSet(Of VPower)

        Public Overridable Property VProdCons As DbSet(Of VProdCon)

        Public Overridable Property VProduceds As DbSet(Of VProduced)

        Public Overridable Property VProductions As DbSet(Of VProduction)

        Public Overridable Property VSales As DbSet(Of VSale)

        Public Overridable Property VSummerNightlies As DbSet(Of VSummerNightly)

        Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
            modelBuilder.Entity(Of TCategory)(
                Sub(entity)
                    entity.ToTable("tCategory")

                    entity.Property(Function(e) e.Id).HasColumnName("ID")
                    entity.Property(Function(e) e.Name).
                        IsRequired().
                        HasMaxLength(50).
                        IsUnicode(False)
                End Sub)

            modelBuilder.Entity(Of TPower)(
                Sub(entity)
                    entity.ToTable("tPower")

                    entity.HasIndex(Function(e) New With {e.Start, e.Finish}, "IX_StartFinish_Covering")

                    entity.HasIndex(Function(e) e.Finish, "finish_index")

                    entity.HasIndex(Function(e) e.Start, "start_index")

                    entity.Property(Function(e) e.Id).HasColumnName("ID")

                    entity.HasOne(Function(d) d.CategoryNavigation).WithMany(Function(p) p.TPowers).
                        HasForeignKey(Function(d) d.Category).
                        OnDelete(DeleteBehavior.ClientSetNull).
                        HasConstraintName("FK_tPower_tCategory")
                End Sub)

            modelBuilder.Entity(Of VConsumed)(
                Sub(entity)
                    entity.
                    HasNoKey().
                    ToView("vConsumed")
                End Sub)

            modelBuilder.Entity(Of VConsumption)(
                Sub(entity)
                    entity.
                    HasNoKey().
                    ToView("vConsumption")

                    entity.Property(Function(e) e.Consumption).HasColumnType("numeric(38, 6)")
                    entity.Property(Function(e) e.Day).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Hour).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Month).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Year).
                        HasMaxLength(4).
                        IsUnicode(False).
                        IsFixedLength()
                    entity.Property(Function(e) e.Yyyymm).
                        HasMaxLength(6).
                        IsUnicode(False).
                        HasColumnName("YYYYMM")
                End Sub)

            modelBuilder.Entity(Of Vdaily)(
                Sub(entity)
                    entity.
                    HasNoKey().
                    ToView("vdaily")

                    entity.Property(Function(e) e.Consumption).HasColumnType("numeric(38, 6)")
                    entity.Property(Function(e) e.Day).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Month).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Production).HasColumnType("numeric(38, 6)")
                    entity.Property(Function(e) e.Year).
                        HasMaxLength(4).
                        IsUnicode(False).
                        IsFixedLength()
                    entity.Property(Function(e) e.Yyyymm).
                        HasMaxLength(6).
                        IsUnicode(False).
                        HasColumnName("YYYYMM")
                End Sub)

            modelBuilder.Entity(Of Vhourly)(
                Sub(entity)
                    entity.
                    HasNoKey().
                    ToView("vhourly")

                    entity.Property(Function(e) e.Consumption).HasColumnType("numeric(38, 6)")
                    entity.Property(Function(e) e.Hour).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Production).HasColumnType("numeric(38, 6)")
                    entity.Property(Function(e) e.Yyyymm).
                        HasMaxLength(6).
                        IsUnicode(False).
                        HasColumnName("YYYYMM")
                End Sub)

            modelBuilder.Entity(Of VHoursmonthly)(
                Sub(entity)
                    entity.
                    HasNoKey().
                    ToView("vHoursmonthly")

                    entity.Property(Function(e) e.Consumption).HasColumnType("numeric(38, 6)")
                    entity.Property(Function(e) e.Hour).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Production).HasColumnType("numeric(38, 6)")
                    entity.Property(Function(e) e.Yyyymm).
                        HasMaxLength(6).
                        IsUnicode(False).
                        HasColumnName("YYYYMM")
                End Sub)

            modelBuilder.Entity(Of VInOut)(
                Sub(entity)
                    entity.
                    HasNoKey().
                    ToView("vInOut")

                    entity.Property(Function(e) e.Consumption).HasColumnType("numeric(38, 6)")
                    entity.Property(Function(e) e.Day).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Hour).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Month).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Production).HasColumnType("numeric(38, 6)")
                    entity.Property(Function(e) e.Year).
                        HasMaxLength(4).
                        IsUnicode(False).
                        IsFixedLength()
                    entity.Property(Function(e) e.Yyyymm).
                        HasMaxLength(6).
                        IsUnicode(False).
                        HasColumnName("YYYYMM")
                End Sub)

            modelBuilder.Entity(Of VMonthHour)(
                Sub(entity)
                    entity.
                    HasNoKey().
                    ToView("vMonthHours")

                    entity.Property(Function(e) e.Hour).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Month).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Year).
                        HasMaxLength(4).
                        IsUnicode(False).
                        IsFixedLength()
                    entity.Property(Function(e) e.Yyyymm).
                        HasMaxLength(6).
                        IsUnicode(False).
                        HasColumnName("YYYYMM")
                End Sub)

            modelBuilder.Entity(Of VPower)(
                Sub(entity)
                    entity.
                    HasNoKey().
                    ToView("vPower")

                    entity.Property(Function(e) e.Category).
                        IsRequired().
                        HasMaxLength(50).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Day).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Hour).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Kwh).
                        HasColumnType("numeric(38, 6)").
                        HasColumnName("KWH")
                    entity.Property(Function(e) e.Month).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Year).
                        HasMaxLength(4).
                        IsUnicode(False).
                        IsFixedLength()
                    entity.Property(Function(e) e.Yyyymm).
                        HasMaxLength(6).
                        IsUnicode(False).
                        HasColumnName("YYYYMM")
                End Sub)

            modelBuilder.Entity(Of VProdCon)(
                Sub(entity)
                    entity.
                    HasNoKey().
                    ToView("vProdCons")
                End Sub)

            modelBuilder.Entity(Of VProduced)(
                Sub(entity)
                    entity.
                    HasNoKey().
                    ToView("vProduced")
                End Sub)

            modelBuilder.Entity(Of VProduction)(
                Sub(entity)
                    entity.
                    HasNoKey().
                    ToView("vProduction")

                    entity.Property(Function(e) e.Day).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Hour).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Month).
                        HasMaxLength(2).
                        IsUnicode(False)
                    entity.Property(Function(e) e.Production).HasColumnType("numeric(38, 6)")
                    entity.Property(Function(e) e.Year).
                        HasMaxLength(4).
                        IsUnicode(False).
                        IsFixedLength()
                    entity.Property(Function(e) e.Yyyymm).
                        HasMaxLength(6).
                        IsUnicode(False).
                        HasColumnName("YYYYMM")
                End Sub)

            modelBuilder.Entity(Of VSale)(
                Sub(entity)
                    entity.
                    HasNoKey().
                    ToView("vSales")

                    entity.Property(Function(e) e.[Date]).HasColumnType("date")
                    entity.Property(Function(e) e.Net).HasColumnType("numeric(38, 6)")
                End Sub)

            modelBuilder.Entity(Of VSummerNightly)(
                Sub(entity)
                    entity.
                    HasNoKey().
                    ToView("vSummerNightly")

                    entity.Property(Function(e) e.[Date]).HasColumnType("date")
                    entity.Property(Function(e) e.Nightly).HasColumnType("numeric(38, 6)")
                End Sub)

            OnModelCreatingPartial(modelBuilder)
        End Sub

        Partial Private Sub OnModelCreatingPartial(modelBuilder As ModelBuilder)
        End Sub
    End Class
End Namespace
