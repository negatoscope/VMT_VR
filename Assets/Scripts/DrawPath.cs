using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    public int imagewidth;
    public int imageheight;
    public int worldtop;
    public int worldbottom;
    public int worldright;
    public int worldleft;
    public float xratio,yratio,x,y;
    public Texture2D image;
    public int pixx,pixy;
 
    void Start () 
    {
        int x, y;
        worldtop = 60;
        worldbottom = -60;
        worldright = 60;
        worldleft = -60;
         
        imagewidth = 180;
        imageheight = 180;
        xratio = imagewidth/(float)(worldtop - worldbottom);
        yratio = imageheight/(float)(worldright - worldleft);
 
        image = new Texture2D (imagewidth, imageheight);
        x = imagewidth;
        while(x>0){x--;y = imageheight;
        while(y>0){y--;image.SetPixel(x,y,Color.black);}
        }image.Apply ();
     
    }
     
 
    void Update () 
    {
        x = transform.position.x;
 
        //use transform position z below if you are 3d;
        y = transform.position.z;
 
        pixx = (int)((x - worldleft) *xratio);
        pixy = (int)((y - worldbottom)*yratio);
 
        image.SetPixel (pixx, pixy, Color.white);
        image.Apply ();
    }
     
    void OnGUI()
    {
        bool isPathOn = PlayerPrefs.GetInt("path") == 1 ? true : false;
        if (isPathOn == true){
            GUI.DrawTexture (new Rect (0, 0, 300, 300), image);
        }
        
    }
}
