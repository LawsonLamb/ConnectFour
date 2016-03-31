using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node{

   public Board board;
   
    public List<Node> children;
  
    public Node(int Row,int Col,int Connect)
    {
        board = new Board(Row, Col, Connect);
        children = new List<Node>();
       
    }




 

 

  
    

}


