using System;
using System.Collections.Generic;
using GithubInsight.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GithubInsight.Infrastructure.Shared.Context;

public partial class GithubInsightContext : DbContext
{
    public GithubInsightContext()
    {
    }

    public GithubInsightContext(DbContextOptions<GithubInsightContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LanguageStat> LanguageStats { get; set; }

    public virtual DbSet<ReposCreatedPerYear> ReposCreatedPerYears { get; set; }

    public virtual DbSet<Stat> Stats { get; set; }

    public virtual DbSet<TopRepo> TopRepos { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost;Database=GithubInsight;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LanguageStat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Language__3214EC070DE7D57E");

            entity.HasIndex(e => e.StatsId, "IX_LanguageStats_StatsId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Language).HasMaxLength(100);

            entity.HasOne(d => d.Stats).WithMany(p => p.LanguageStats)
                .HasForeignKey(d => d.StatsId)
                .HasConstraintName("FK__LanguageS__Stats__4222D4EF");
        });

        modelBuilder.Entity<ReposCreatedPerYear>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ReposCre__3214EC073D2A8872");

            entity.ToTable("ReposCreatedPerYear");

            entity.HasIndex(e => e.StatsId, "IX_ReposCreatedPerYear_StatsId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Stats).WithMany(p => p.ReposCreatedPerYears)
                .HasForeignKey(d => d.StatsId)
                .HasConstraintName("FK__ReposCrea__Stats__45F365D3");
        });

        modelBuilder.Entity<Stat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stats__3214EC077F9F6034");

            entity.HasIndex(e => e.UserId, "IX_Stats_UserId");

            entity.HasIndex(e => e.UserId, "UQ__Stats__1788CC4DDC7EE02B").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MostUsedLanguage).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithOne(p => p.Stat)
                .HasForeignKey<Stat>(d => d.UserId)
                .HasConstraintName("FK__Stats__UserId__3E52440B");
        });

        modelBuilder.Entity<TopRepo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TopRepos__3214EC07235F33FD");

            entity.HasIndex(e => e.StatsId, "IX_TopRepos_StatsId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RepoName).HasMaxLength(200);

            entity.HasOne(d => d.Stats).WithMany(p => p.TopRepos)
                .HasForeignKey(d => d.StatsId)
                .HasConstraintName("FK__TopRepos__StatsI__49C3F6B7");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0702FBEF52");

            entity.ToTable("User");

            entity.HasIndex(e => e.Login, "IX_User_Login");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
