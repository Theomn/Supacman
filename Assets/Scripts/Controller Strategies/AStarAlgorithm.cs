using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarAlgorithm
{
    private static List<List<Node>> nodes;
    private static List<Node> openNodes;
    private static List<Node> adjacentNodes;

    public enum MovementType
    {
        Horizontal, Diagonal
    }

    public enum NodeState
    {
        Pending, Open, Closed, Wall
    }

    public class Node : IComparer<Node>
    {
        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x, y;
        public Node previousNode = null;
        public NodeState state = NodeState.Pending;
        public float distanceFromSource, distanceToTarget;

        public int Compare(Node x, Node y)
        {
            return (int)((x.distanceFromSource + x.distanceToTarget) - (y.distanceFromSource + y.distanceToTarget));
        }

        public bool IsNode(Node other)
        {
            return (x == other.x) && (y == other.y);
        }
    }

    public static void Initialize(List<List<bool>> grid)
    {
        openNodes = new List<Node>();
        adjacentNodes = new List<Node>();
        nodes = new List<List<Node>>();
        int y = 0;
        int x;
        foreach (List<bool> line in grid)
        {
            nodes.Add(new List<Node>());
            x = 0;
            foreach (bool tile in line)
            {
                nodes[y].Add(new Node(x, y));
                if (grid[y][x])
                {
                    nodes[y][x].state = NodeState.Wall;
                }
                x++;
            }
            y++;
        }
    }

    public static List<Node> Compute(int sourceX, int sourceY, int targetX, int targetY, MovementType movementType, float distanceThreshold = 0)
    {
        ResetNodes();
        Node source = nodes[sourceY][sourceX];
        Node target = nodes[targetY][targetX];
        Node currentNode = source;
        while (!currentNode.IsNode(target) && BirdDistance(currentNode, target) >= distanceThreshold)
        {
            openNodes.Remove(currentNode);
            currentNode.state = NodeState.Closed;
            FindAdjacentNodes(currentNode, target, movementType);
            if (openNodes.Count <= 0)
            {
                return null;
            }
            openNodes.Sort((x, y) => (int)((x.distanceFromSource + x.distanceToTarget) - (y.distanceFromSource + y.distanceToTarget)));
            currentNode = openNodes[0];
        }
        return compilePath(currentNode);
    }

    private static void FindAdjacentNodes(Node source, Node target, MovementType movementType)
    {
        // grid is bound by walls so no risk of index out of range
        if (movementType == MovementType.Horizontal)
        {
            GetHorizontalNeighbors(source);
        }
        else if (movementType == MovementType.Diagonal)
        {
            GetDiagonalNeighbors(source);
        }

        foreach (Node node in adjacentNodes)
        {
            if (node.state == NodeState.Pending)
            {
                node.state = NodeState.Open;
                node.previousNode = source;
                if (movementType == MovementType.Horizontal)
                {
                    node.distanceFromSource = source.distanceFromSource + 1;
                    node.distanceToTarget = ManhattanDistance(node, target);
                }
                else if (movementType == MovementType.Diagonal)
                {
                    node.distanceFromSource = source.distanceFromSource + 1.4f;
                    node.distanceToTarget = BirdDistance(node, target);
                }
                openNodes.Add(node);
            }
            else if (node.state == NodeState.Open)
            {
                if (movementType == MovementType.Horizontal)
                {
                    if (node.distanceFromSource > source.distanceFromSource + 1)
                    {
                        node.distanceFromSource = source.distanceFromSource + 1;
                        node.previousNode = source;
                    }
                }
                else if (movementType == MovementType.Diagonal)
                {
                    if (node.distanceFromSource > source.distanceFromSource + 1.4f)
                    {
                        node.distanceFromSource = source.distanceFromSource + 1.4f;
                        node.previousNode = source;
                    }
                }
            }
        }
        adjacentNodes.Clear();
    }

    private static void GetHorizontalNeighbors(Node source)
    {
        adjacentNodes.Add(nodes[source.y - 1][source.x]);
        adjacentNodes.Add(nodes[source.y][source.x + 1]);
        adjacentNodes.Add(nodes[source.y + 1][source.x]);
        adjacentNodes.Add(nodes[source.y][source.x - 1]);
    }

    private static void GetDiagonalNeighbors(Node source)
    {
        adjacentNodes.Add(nodes[source.y - 1][source.x - 1]);
        adjacentNodes.Add(nodes[source.y - 1][source.x + 1]);
        adjacentNodes.Add(nodes[source.y + 1][source.x - 1]);
        adjacentNodes.Add(nodes[source.y + 1][source.x + 1]);
    }

    private static List<Node> compilePath(Node target)
    {
        List<Node> path = new List<Node>();
        Node node = target;
        while (node != null)
        {
            path.Insert(0, node);
            node = node.previousNode;
        }
        return path;
    }

    private static void ResetNodes()
    {
        foreach (List<Node> line in nodes)
        {
            foreach (Node node in line)
            {
                node.previousNode = null;
                if (node.state != NodeState.Wall)
                {
                    node.state = NodeState.Pending;
                }
            }
        }
        openNodes.Clear();
    }

    private static float ManhattanDistance(Node source, Node target)
    {
        return Mathf.Abs(source.x - target.x) + Mathf.Abs(source.y - target.y);
    }

    private static float BirdDistance(Node source, Node target)
    {
        return Mathf.Sqrt(Mathf.Pow((source.x - target.x), 2) + Mathf.Pow((source.y - target.y),2));
    }

    public static void DebugPrintNodeMatrix()
    {
        string outputString = string.Empty;
        foreach (List<Node> line in nodes)
        {
            foreach (Node tile in line)
            {
                switch (tile.state)
                {
                    case NodeState.Wall:
                        outputString += 'X';
                        break;
                    case NodeState.Pending:
                        outputString += '.';
                        break;
                    case NodeState.Open:
                        outputString += ':';
                        break;
                    case NodeState.Closed:
                        outputString += '*';
                        break;
                }
            }
            outputString += '\n';
        }
        Debug.Log(outputString);
    }
}
