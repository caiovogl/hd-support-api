﻿// <auto-generated />
using System;
using HD_Support_API.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HD_Support_API.Migrations
{
    [DbContext(typeof(BancoContext))]
    [Migration("20240419172621_bancoDadosSqlite")]
    partial class bancoDadosSqlite
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("HD_Support_API.Models.Conversa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClienteId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Criptografia")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Data_conclusao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Data_inicio")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FuncionariosId")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TipoConversa")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("FuncionariosId");

                    b.ToTable("Conversa");
                });

            modelBuilder.Entity("HD_Support_API.Models.Emprestimos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EquipamentosId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EquipamentosId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Emprestimo");
                });

            modelBuilder.Entity("HD_Support_API.Models.Equipamentos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DtEmeprestimoFinal")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DtEmeprestimoInicio")
                        .HasColumnType("TEXT");

                    b.Property<string>("HeadSet")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int?>("IdPatrimonio")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("INTEGER");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Processador")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("SistemaOperacional")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("profissional_HD")
                        .HasColumnType("TEXT");

                    b.Property<int>("statusEquipamento")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Equipamento");
                });

            modelBuilder.Entity("HD_Support_API.Models.Mensagens", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConversaId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Data_envio")
                        .HasColumnType("TEXT");

                    b.Property<string>("Mensagem")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Mensagens");
                });

            modelBuilder.Entity("HD_Support_API.Models.Usuarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("DataHoraGeracaoToken")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<int?>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StatusConversa")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("TokenRedefinicaoSenha")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("HD_Support_API.Models.Conversa", b =>
                {
                    b.HasOne("HD_Support_API.Models.Usuarios", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HD_Support_API.Models.Usuarios", "Funcionario")
                        .WithMany()
                        .HasForeignKey("FuncionariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("HD_Support_API.Models.Emprestimos", b =>
                {
                    b.HasOne("HD_Support_API.Models.Equipamentos", "Equipamento")
                        .WithMany()
                        .HasForeignKey("EquipamentosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HD_Support_API.Models.Usuarios", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipamento");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("HD_Support_API.Models.Mensagens", b =>
                {
                    b.HasOne("HD_Support_API.Models.Usuarios", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
