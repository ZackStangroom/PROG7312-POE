using PROG7312_POE.Models;

namespace PROG7312_POE.Services.DataStructures
{
    // Graph node representing a service request with its connections
    public class GraphNode
    {
        public IssueReport Request { get; set; }
        public List<GraphEdge> Connections { get; set; }
        public bool Visited { get; set; }

        public GraphNode(IssueReport request)
        {
            Request = request;
            Connections = new List<GraphEdge>();
            Visited = false;
        }
    }

    // Edge representing relationship between two requests
    public class GraphEdge
    {
        public GraphNode TargetNode { get; set; }
        public RelationType RelationType { get; set; }
        public double Weight { get; set; }

        public GraphEdge(GraphNode targetNode, RelationType relationType, double weight = 1.0)
        {
            TargetNode = targetNode;
            RelationType = relationType;
            Weight = weight;
        }
    }

    // Types of relationships between service requests
    public enum RelationType
    {
        SameLocation,       // Requests in the same location
        SameCategory,       // Requests in the same category
        RelatedCategory,    // Requests in related categories (e.g., Water & Roads)
        DependsOn,          // One request depends on another
        Duplicate           // Potential duplicate requests
    }

    // Graph data structure for managing service request relationships
    public class ServiceRequestGraph
    {
        private readonly Dictionary<string, GraphNode> _nodes;
        private int _edgeCount;

        public ServiceRequestGraph()
        {
            _nodes = new Dictionary<string, GraphNode>();
            _edgeCount = 0;
        }

        // Add a service request to the graph
        public void AddRequest(IssueReport request)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
                return;

            if (!_nodes.ContainsKey(request.Id))
            {
                _nodes[request.Id] = new GraphNode(request);
            }
        }

        // Add a relationship between two requests
        public void AddRelationship(string sourceId, string targetId, RelationType relationType, double weight = 1.0)
        {
            if (!_nodes.ContainsKey(sourceId) || !_nodes.ContainsKey(targetId))
                return;

            var sourceNode = _nodes[sourceId];
            var targetNode = _nodes[targetId];

            // Check if connection already exists
            if (!sourceNode.Connections.Any(c => c.TargetNode.Request.Id == targetId))
            {
                sourceNode.Connections.Add(new GraphEdge(targetNode, relationType, weight));
                _edgeCount++;
            }
        }

        // Build relationships automatically based on request properties
        public void BuildRelationships()
        {
            var allNodes = _nodes.Values.ToList();

            for (int i = 0; i < allNodes.Count; i++)
            {
                for (int j = i + 1; j < allNodes.Count; j++)
                {
                    var req1 = allNodes[i].Request;
                    var req2 = allNodes[j].Request;

                    // Same location relationship
                    if (IsSameLocation(req1.Location, req2.Location))
                    {
                        AddRelationship(req1.Id, req2.Id, RelationType.SameLocation, 0.9);
                        AddRelationship(req2.Id, req1.Id, RelationType.SameLocation, 0.9);
                    }

                    // Same category relationship
                    if (req1.Category.Equals(req2.Category, StringComparison.OrdinalIgnoreCase))
                    {
                        AddRelationship(req1.Id, req2.Id, RelationType.SameCategory, 0.8);
                        AddRelationship(req2.Id, req1.Id, RelationType.SameCategory, 0.8);
                    }

                    // Related category relationship
                    if (AreRelatedCategories(req1.Category, req2.Category))
                    {
                        AddRelationship(req1.Id, req2.Id, RelationType.RelatedCategory, 0.6);
                        AddRelationship(req2.Id, req1.Id, RelationType.RelatedCategory, 0.6);
                    }

                    // Potential duplicate detection
                    if (IsPotentialDuplicate(req1, req2))
                    {
                        AddRelationship(req1.Id, req2.Id, RelationType.Duplicate, 0.95);
                        AddRelationship(req2.Id, req1.Id, RelationType.Duplicate, 0.95);
                    }
                }
            }
        }

        // Get a request node by ID
        public GraphNode? GetNode(string requestId)
        {
            return _nodes.ContainsKey(requestId) ? _nodes[requestId] : null;
        }

        // Get all related requests using BFS (Breadth-First Search)
        public List<IssueReport> GetRelatedRequests(string requestId, int maxDepth = 2)
        {
            if (!_nodes.ContainsKey(requestId))
                return new List<IssueReport>();

            ResetVisited();
            var result = new List<IssueReport>();
            var queue = new Queue<(GraphNode node, int depth)>();
            var startNode = _nodes[requestId];

            queue.Enqueue((startNode, 0));
            startNode.Visited = true;

            while (queue.Count > 0)
            {
                var (currentNode, currentDepth) = queue.Dequeue();

                if (currentNode.Request.Id != requestId)
                {
                    result.Add(currentNode.Request);
                }

                if (currentDepth < maxDepth)
                {
                    foreach (var edge in currentNode.Connections.OrderByDescending(e => e.Weight))
                    {
                        if (!edge.TargetNode.Visited)
                        {
                            edge.TargetNode.Visited = true;
                            queue.Enqueue((edge.TargetNode, currentDepth + 1));
                        }
                    }
                }
            }

            return result;
        }

        // Get related requests by specific relationship type using DFS (Depth-First Search)
        public List<IssueReport> GetRelatedRequestsByType(string requestId, RelationType relationType)
        {
            if (!_nodes.ContainsKey(requestId))
                return new List<IssueReport>();

            ResetVisited();
            var result = new List<IssueReport>();
            var startNode = _nodes[requestId];

            DFSByType(startNode, relationType, result, requestId);

            return result;
        }

        // DFS traversal filtering by relationship type
        private void DFSByType(GraphNode node, RelationType relationType, List<IssueReport> result, string originalId)
        {
            node.Visited = true;

            foreach (var edge in node.Connections.Where(c => c.RelationType == relationType))
            {
                if (!edge.TargetNode.Visited)
                {
                    if (edge.TargetNode.Request.Id != originalId)
                    {
                        result.Add(edge.TargetNode.Request);
                    }
                    DFSByType(edge.TargetNode, relationType, result, originalId);
                }
            }
        }

        // Find shortest path between two requests (could represent dependency chain)
        public List<IssueReport> FindShortestPath(string startId, string endId)
        {
            if (!_nodes.ContainsKey(startId) || !_nodes.ContainsKey(endId))
                return new List<IssueReport>();

            ResetVisited();
            var queue = new Queue<List<GraphNode>>();
            var startNode = _nodes[startId];
            var endNode = _nodes[endId];

            queue.Enqueue(new List<GraphNode> { startNode });
            startNode.Visited = true;

            while (queue.Count > 0)
            {
                var path = queue.Dequeue();
                var lastNode = path[^1];

                if (lastNode.Request.Id == endId)
                {
                    return path.Select(n => n.Request).ToList();
                }

                foreach (var edge in lastNode.Connections.OrderByDescending(e => e.Weight))
                {
                    if (!edge.TargetNode.Visited)
                    {
                        edge.TargetNode.Visited = true;
                        var newPath = new List<GraphNode>(path) { edge.TargetNode };
                        queue.Enqueue(newPath);
                    }
                }
            }

            return new List<IssueReport>();
        }

        // Get graph statistics
        public Dictionary<string, object> GetStatistics()
        {
            return new Dictionary<string, object>
            {
                { "TotalNodes", _nodes.Count },
                { "TotalEdges", _edgeCount },
                { "AverageConnections", _nodes.Count > 0 ? _nodes.Values.Average(n => n.Connections.Count) : 0 },
                { "MostConnectedRequest", GetMostConnectedRequest() },
                { "RelationshipTypeDistribution", GetRelationshipTypeDistribution() }
            };
        }

        // Helper: Reset visited flags for traversal
        private void ResetVisited()
        {
            foreach (var node in _nodes.Values)
            {
                node.Visited = false;
            }
        }

        // Helper: Check if two locations are the same (fuzzy matching with edit distance)
        private bool IsSameLocation(string loc1, string loc2)
        {
            if (string.IsNullOrEmpty(loc1) || string.IsNullOrEmpty(loc2))
                return false;

            // Normalize locations
            var normalized1 = NormalizeLocation(loc1);
            var normalized2 = NormalizeLocation(loc2);

            // Exact match
            if (normalized1 == normalized2)
                return true;

            // Calculate similarity using Levenshtein distance
            double similarity = CalculateLocationSimilarity(normalized1, normalized2);
            
            // Consider same location if similarity >= 85%
            return similarity >= 0.85;
        }

        // Helper: Normalize location string
        private string NormalizeLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
                return string.Empty;

            // Convert to lowercase, remove extra whitespace, trim
            return string.Join(" ", location.Trim()
                .ToLower()
                .Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries));
        }

        // Helper: Calculate location similarity using Levenshtein distance
        private double CalculateLocationSimilarity(string loc1, string loc2)
        {
            if (loc1 == loc2)
                return 1.0;

            int distance = LevenshteinDistance(loc1, loc2);
            int maxLength = Math.Max(loc1.Length, loc2.Length);

            if (maxLength == 0)
                return 1.0;

            return 1.0 - ((double)distance / maxLength);
        }

        // Helper: Calculate Levenshtein distance (edit distance)
        private int LevenshteinDistance(string s1, string s2)
        {
            int[,] dp = new int[s1.Length + 1, s2.Length + 1];

            // Initialize base cases
            for (int i = 0; i <= s1.Length; i++)
                dp[i, 0] = i;
            
            for (int j = 0; j <= s2.Length; j++)
                dp[0, j] = j;

            // Fill the dynamic programming table
            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    int cost = s1[i - 1] == s2[j - 1] ? 0 : 1;

                    dp[i, j] = Math.Min(
                        Math.Min(dp[i - 1, j] + 1,      // Deletion
                                 dp[i, j - 1] + 1),      // Insertion
                                 dp[i - 1, j - 1] + cost // Substitution
                    );
                }
            }

            return dp[s1.Length, s2.Length];
        }

        // Helper: Check if categories are related
        private bool AreRelatedCategories(string cat1, string cat2)
        {
            if (string.IsNullOrEmpty(cat1) || string.IsNullOrEmpty(cat2))
                return false;

            var relatedCategories = new Dictionary<string, List<string>>
            {
                { "Water & Sanitation", new List<string> { "Roads & Transport", "Waste Management" } },
                { "Roads & Transport", new List<string> { "Water & Sanitation", "Electricity" } },
                { "Electricity", new List<string> { "Roads & Transport", "Public Safety" } },
                { "Waste Management", new List<string> { "Water & Sanitation", "Parks & Recreation" } },
                { "Parks & Recreation", new List<string> { "Waste Management", "Public Safety" } },
                { "Emergency Services", new List<string> { "Public Safety", "Electricity" } },
                { "Public Safety", new List<string> { "Emergency Services", "Electricity", "Parks & Recreation" } },
                { "Housing", new List<string> { "Water & Sanitation", "Electricity" } }
            };

            if (relatedCategories.ContainsKey(cat1))
            {
                return relatedCategories[cat1].Contains(cat2, StringComparer.OrdinalIgnoreCase);
            }

            return false;
        }

        // Helper: Check if requests might be duplicates
        private bool IsPotentialDuplicate(IssueReport req1, IssueReport req2)
        {
            if (req1 == null || req2 == null)
                return false;

            // Check if same category
            var sameCategory = req1.Category.Equals(req2.Category, StringComparison.OrdinalIgnoreCase);
            
            // Check if same location (using the improved fuzzy matching)
            var sameLocation = IsSameLocation(req1.Location, req2.Location);
            
            // Check if reported within 48 hours of each other
            var timeDifference = Math.Abs((req1.ReportedDate - req2.ReportedDate).TotalHours);
            var similarTime = timeDifference < 48;

            // Calculate description similarity
            var descriptionSimilarity = CalculateLocationSimilarity(
                NormalizeLocation(req1.Description), 
                NormalizeLocation(req2.Description)
            );
            var similarDescription = descriptionSimilarity > 0.7;

            // Consider duplicate if: same category + same location + similar time
            // OR same category + same location + similar description
            return sameCategory && sameLocation && (similarTime || similarDescription);
        }

        // Helper: Get most connected request
        private string GetMostConnectedRequest()
        {
            if (_nodes.Count == 0)
                return "None";

            var mostConnected = _nodes.Values
                .OrderByDescending(n => n.Connections.Count)
                .FirstOrDefault();

            if (mostConnected == null)
                return "None";

            return $"{mostConnected.Request.Id} ({mostConnected.Connections.Count} connections)";
        }

        // Helper: Get distribution of relationship types
        private Dictionary<string, int> GetRelationshipTypeDistribution()
        {
            var distribution = new Dictionary<string, int>();

            foreach (var node in _nodes.Values)
            {
                foreach (var edge in node.Connections)
                {
                    var typeName = edge.RelationType.ToString();
                    
                    if (!distribution.ContainsKey(typeName))
                    {
                        distribution[typeName] = 0;
                    }
                    
                    distribution[typeName]++;
                }
            }

            return distribution;
        }

        public int NodeCount => _nodes.Count;
        public int EdgeCount => _edgeCount;
    }
}