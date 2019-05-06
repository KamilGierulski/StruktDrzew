using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Trees.WebUI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Graph> Graphs { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Edge> Edges { get; set; }
    }

    public class Graph
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Edge> Edges { get; set; }
        public List<Node> Nodes { get; set; }
    }

    public class Node
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Guid { get; set; }

        public string Label { get; set; }

        [ForeignKey(nameof(Graph))]
        public int GraphId { get; set; }

        public Graph Graph { get; set; }
    }

    public class Edge
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }
    }
}
