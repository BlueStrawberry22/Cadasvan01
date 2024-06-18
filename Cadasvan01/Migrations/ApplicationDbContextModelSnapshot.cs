﻿// <auto-generated />
using System;
using Cadasvan01.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cadasvan01.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cadasvan01.Models.Avaliacao", b =>
                {
                    b.Property<int>("AvaliacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AvaliacaoId"));

                    b.Property<string>("AlunoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AvaliacaoEstrelas")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAvaliacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("MotoristaId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Opiniao")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AvaliacaoId");

                    b.HasIndex("AlunoId");

                    b.HasIndex("MotoristaId");

                    b.ToTable("Avaliacoes");
                });

            modelBuilder.Entity("Cadasvan01.Models.Aviso", b =>
                {
                    b.Property<int>("AvisoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AvisoId"));

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataPublicacao")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Lido")
                        .HasColumnType("bit");

                    b.Property<string>("MotoristaId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AvisoId");

                    b.HasIndex("MotoristaId");

                    b.ToTable("Avisos");
                });

            modelBuilder.Entity("Cadasvan01.Models.Cidade", b =>
                {
                    b.Property<int>("CidadeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CidadeId"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(110)
                        .HasColumnType("nvarchar(110)");

                    b.HasKey("CidadeId");

                    b.ToTable("Cidades");
                });

            modelBuilder.Entity("Cadasvan01.Models.CodigoVinculacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("MotoristaId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("MotoristaId");

                    b.ToTable("CodigosVinculacao");
                });

            modelBuilder.Entity("Cadasvan01.Models.Funcao", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Cadasvan01.Models.Presenca", b =>
                {
                    b.Property<int>("PresencaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PresencaId"));

                    b.Property<bool>("ConfirmadoIda")
                        .HasColumnType("bit");

                    b.Property<bool>("ConfirmadoVolta")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataViagem")
                        .HasColumnType("datetime2");

                    b.Property<string>("MotoristaId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PresencaId");

                    b.HasIndex("MotoristaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Presencas");
                });

            modelBuilder.Entity("Cadasvan01.Models.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CEP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CNH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CPF")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("CaminhoImagemPerfil")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Celular1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Celular2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CidadeId")
                        .HasColumnType("int");

                    b.Property<string>("Complemento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorVan1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorVan2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Endereco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FotoVan1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FotoVan2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Itinerario")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ModeloVan1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModeloVan2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotoristaId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PlacaVan1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlacaVan2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sobrenome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("VanSelecionada")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CidadeId");

                    b.HasIndex("MotoristaId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Cadasvan01.Models.Viagem", b =>
                {
                    b.Property<int>("ViagemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ViagemId"));

                    b.Property<bool>("Ativa")
                        .HasColumnType("bit");

                    b.Property<string>("Destino")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("HoraFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HoraInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("MotoristaId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VanSelecionada")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ViagemId");

                    b.HasIndex("MotoristaId");

                    b.ToTable("Viagens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Cadasvan01.Models.Avaliacao", b =>
                {
                    b.HasOne("Cadasvan01.Models.Usuario", "Aluno")
                        .WithMany("AvaliacoesFeitas")
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Cadasvan01.Models.Usuario", "Motorista")
                        .WithMany("AvaliacoesRecebidas")
                        .HasForeignKey("MotoristaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Aluno");

                    b.Navigation("Motorista");
                });

            modelBuilder.Entity("Cadasvan01.Models.Aviso", b =>
                {
                    b.HasOne("Cadasvan01.Models.Usuario", "Motorista")
                        .WithMany()
                        .HasForeignKey("MotoristaId");

                    b.Navigation("Motorista");
                });

            modelBuilder.Entity("Cadasvan01.Models.CodigoVinculacao", b =>
                {
                    b.HasOne("Cadasvan01.Models.Usuario", "Motorista")
                        .WithMany()
                        .HasForeignKey("MotoristaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Motorista");
                });

            modelBuilder.Entity("Cadasvan01.Models.Presenca", b =>
                {
                    b.HasOne("Cadasvan01.Models.Usuario", "Motorista")
                        .WithMany()
                        .HasForeignKey("MotoristaId");

                    b.HasOne("Cadasvan01.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Motorista");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Cadasvan01.Models.Usuario", b =>
                {
                    b.HasOne("Cadasvan01.Models.Cidade", "Cidade")
                        .WithMany("Usuarios")
                        .HasForeignKey("CidadeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Cadasvan01.Models.Usuario", "Motorista")
                        .WithMany("Alunos")
                        .HasForeignKey("MotoristaId");

                    b.Navigation("Cidade");

                    b.Navigation("Motorista");
                });

            modelBuilder.Entity("Cadasvan01.Models.Viagem", b =>
                {
                    b.HasOne("Cadasvan01.Models.Usuario", "Motorista")
                        .WithMany()
                        .HasForeignKey("MotoristaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Motorista");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Cadasvan01.Models.Funcao", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Cadasvan01.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Cadasvan01.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Cadasvan01.Models.Funcao", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cadasvan01.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Cadasvan01.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cadasvan01.Models.Cidade", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Cadasvan01.Models.Usuario", b =>
                {
                    b.Navigation("Alunos");

                    b.Navigation("AvaliacoesFeitas");

                    b.Navigation("AvaliacoesRecebidas");
                });
#pragma warning restore 612, 618
        }
    }
}
