using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    public Node start;
    public Node goal;
    private List<Node> open;
    private List<Node> closed;
    private Node[][] grid;

    public Pathfinder()
    {

    }
    
    /*
     * Gird is calculated based on the difference between x1 and x2, and y1 and y2.
     * Might have to consider expanding the grid size to reduce the chance of creating
     * unreachable paths
     */
    private void createGrid()
    {
           
   }

}
