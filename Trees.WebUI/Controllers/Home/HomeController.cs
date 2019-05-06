using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trees.WebUI.Data;

namespace Trees.WebUI.Controllers.Home
{
    [Route("/api")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/api/node")]
        public JsonResult Save([FromBody] Graph graph)
        {
            try
            {
                var newGraph = new Data.Graph
                {
                    Name = graph.graphName,
                    Nodes = graph.nodes.Select(x => new Data.Node { Guid = x.id, Label = x.label }).ToList(),
                    Edges = graph.edges.Select(x => new Data.Edge { From = x.from, To = x.to }).ToList(),
                };

                _dbContext.Graphs.Add(newGraph);
                _dbContext.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        [Route("/api/graph")]
        public JsonResult GetGraphs()
        {
            try
            {
                var graphs = _dbContext.Graphs.Select(x => new {id = x.Id, name = x.Name}).ToArray();

                return Json(new { success = true, data = graphs });
            }
            catch (Exception e)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        [Route("/api/graph/{id}")]
        public JsonResult GetGraphs(int id)
        {
            try
            {
                var nodesQuery = _dbContext.Nodes.Where(x => x.GraphId == id);

                var nodes = nodesQuery.Select(x => new { id = x.Guid, label = x.Label }).ToArray();
                var edges = _dbContext.Edges
                    .Where(x => nodes.Any(y => y.id == x.From) || nodes.Any(y => y.id == x.To))
                    .Select(x => new { from = x.From, to = x.To })
                    .ToArray();

                return Json(new { success = true, data = new { nodes = nodes, edges = edges } });
            }
            catch (Exception e)
            {
                return Json(new { success = false });
            }
        }
    }

    public class Graph
    {
        public string graphName { get; set; }
        public Node[] nodes { get; set; }
        public Edge[] edges { get; set; }
    }

    public class Node
    {
        public string id { get; set; }

        public string label { get; set; }
    }

    public class Edge
    {
        public string from { get; set; }

        public string to { get; set; }
    }
}