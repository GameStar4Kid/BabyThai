using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour 
{
    public Object prefap_pikachu;
    int ROW, COL;
    public int[][] MAP;
    public bool[][] SHIT;
    public Vec2[][] SHIT_ROOT_POS;
    public Vec2 POS1;
    public Vec2 POS2;
    public Vector3[][] POS;
    public static int MIN_X;
    public static int MIN_Y;
    public static int CELL_WIDH = 28;
    public static int CELL_HEIGHT = 32;
	void Start () 
    {
        prefap_pikachu = Resources.Load("item");
        if (prefap_pikachu == null) Debug.Log("123");
        LMap(10, 15);
        RandomMap();
	}
	void Update () 
    {
	    if(Input.GetMouseButtonDown(0))
        {
            float x = (Input.mousePosition.x - Screen.width / 2) / Screen.width * (Screen.width * 1.0f / Screen.height);
            float y = ((Input.mousePosition.y - Screen.height / 2) / Screen.height)  ;
            x *= 320;
            y *= 320;
            int mouse_col = (int)((x-MIN_X)/CELL_WIDH);
            int mouse_row = (int)((y - MIN_Y) / CELL_HEIGHT);
            //Debug.Log(mouse_col + " " + mouse_row);

            if(POS1 !=null && POS1.C == mouse_col && POS1.R == mouse_row)
            {
                DeSelect();
            }
            else
            if(POS1 == null)
            {
                Select(new Vec2(mouse_row, mouse_col));
            }
            else
            {
                CheckPair(new Vec2(mouse_row, mouse_col));
            }
        }
	}
    void Select(Vec2 pos)
    {
        POS1 = new Vec2(pos.R, pos.C);
        Debug.Log("selected " + POS1.Print());
    }
    void DeSelect()
    {
        POS1 = null;
        POS2 = null;
        Debug.Log("DeSelect");
    }
    void CheckPair(Vec2 pos)
    {
        POS2 = new Vec2(pos.R, pos.C);
        //Debug.Log("CheckPair " + POS1.Print() +" and " + POS2.Print());


       // Debug.Log(MAP[POS1.R][POS1.C] + " " + MAP[POS2.R][POS2.C]);
        if(MAP[POS1.R][POS1.C] != MAP[POS2.R][POS2.C])
        {
            Debug.Log("KHONG PHAI LA 1 CAP");
        }
        else if ( CheckpairOnly(POS1, POS2) != null)
        {
            Debug.Log("la 1 cap");
        }
        else Debug.Log("khong thay dương di ");

        POS1 = null;
        POS2 = null;
    }
    void AddPikachu(int type,Vector3 pos,int width,int height)
    {
        GameObject g = Instantiate(prefap_pikachu) as GameObject;
        g.transform.parent = this.transform;
        g.transform.position = pos;
        Sprite sprite = Resources.Load("Images/item/item"+type, typeof(Sprite)) as Sprite;
        g.GetComponent<SpriteRenderer>().sprite = sprite;
        g.transform.localScale = new Vector3(Mathf.Abs(width * 1.0f / sprite.bounds.size.x), Mathf.Abs (- height * 1.0f / sprite.bounds.size.y), 1);
        
    }
    public void LMap(int row, int col)
    {
        ROW = row;
        COL = col;
        CELL_HEIGHT = (int)(320f / (ROW - 2));
        //Debug.Log(ROW);
        CELL_WIDH = (int)(CELL_HEIGHT * 0.9f);

        ROW = row;
        COL = col;
        MAP = new int[ROW][];
        SHIT = new bool[ROW][];
        SHIT_ROOT_POS = new Vec2[ROW][];
        POS = new Vector3[ROW][];

        MIN_X = -COL * CELL_WIDH / 2;
        MIN_Y = -(ROW  ) * CELL_HEIGHT / 2;

        Debug.Log(MIN_X + " " + MIN_Y );

        for (int i = 0; i < ROW; i++)
        {
            MAP[i] = new int[COL];
            SHIT[i] = new bool[COL];
            SHIT_ROOT_POS[i] = new Vec2[COL];
            POS[i] = new Vector3[COL];
            for (int j = 0; j < COL; j++)
            {
                SHIT[i][j] = false;
                MAP[i][j] = -1;
                SHIT_ROOT_POS[i][j] = new Vec2();


                POS[i][j] = new Vector3(0, 0, 0);
                POS[i][j].x = MIN_X + j * CELL_WIDH + CELL_WIDH / 2;
                POS[i][j].y = MIN_Y + i * CELL_HEIGHT + CELL_HEIGHT / 2;
                POS[i][j].z = i / 10.0f;

                //Debug.Log("a  " + POS[i][j]);
            }
        }
        //PrintMap();
        //RandomMap();
        //CheckAndSwapThings();
    }

    void RandomMap()
    {
       

        int dem = 0;
        for (int i = 1; i < ROW - 1; i++)
            for (int j = 1; j < COL - 1; j++)
                if (MAP[i][j] == 1)
                    dem++;

        if (dem % 2 == 1)
        {
            Debug.LogError("than gnu");
            return;
        }

        int[] pool = new int[dem];

        for (int i = 0; i < dem / 2; i++)
            pool[i] = Random.Range(0, 47);
        for (int i = dem / 2; i < dem; i++)
            pool[i] = pool[dem - 1 - i];



        for (int i = 0; i < dem / 2; i++)
        {
            int index1 = Random.Range(0, dem);
            int index2 = Random.Range(0, dem);
            int temp = pool[index1];
            pool[index1] = pool[index2];
            pool[index2] = temp;
        }

        for (int i = 1; i < ROW - 1; i++)
        {
            for (int j = 1; j < COL - 1; j++)
            {
                //if(i==1 && j==1)
                int type = Random.Range(0, 36);
                AddPikachu(type, POS[i][j], CELL_WIDH, CELL_HEIGHT);
                MAP[i][j] = type;
            }
        }
    }

    //bai 4
    public Vec2 POS1_SAVE;
    static int[] DX = { 0, 0, -1, 1 };
    static int[] DY = { -1, 1, 0, 0 };
    static int[] D = { -1, 1 };
    Vec2 CheckShit_Fast_temp;
    public int POS1_SAVE_INDEX;
    public Vec2 POS_SAVE_MIDDLE;
    public LPath CheckpairOnly(Vec2 v0, Vec2 v1)
    {
        ResetShit();
        SetShit(v0, true);
        return CheckShit(v1, true);
    }

    
    
    public void ResetShit()
    {
        for (int i = 0; i < ROW; i++)
            for (int j = 0; j < COL; j++)
                if (SHIT[i][j] == true) SHIT[i][j] = false;
    }
    
    public void SetShit(Vec2 v, bool isneedtotrack = false)
    {
        POS1_SAVE = new Vec2(v.R, v.C);
        SHIT[v.R][v.C] = true;
        //Debug.Log("(" + v.R + "," + v.C);
        int nc, nr;

        for (int k = 0; k < 2; k++)
        {
            for (int l = 1; l < 10; l++)
            {
                nr = v.R + l * D[k];
                if (nr < 0 || nr >= ROW) break;
                if (MAP[nr][v.C] != -1) break;
                // if (SHIT[nr][v.C]==false)
                {
                    SHIT[nr][v.C] = true;
                    if (isneedtotrack)
                        SHIT_ROOT_POS[nr][v.C].R = -1;
                    //Debug.Log("(" + nr + "," + v.C );
                    SetShitHorizontal(new Vec2(nr, v.C), isneedtotrack);
                }
            }
        }

        for (int k = 0; k < 2; k++)
        {
            for (int l = 1; l < 10; l++)
            {
                nc = v.C + l * D[k];
                if (nc < 0 || nc >= COL) break;
                if (MAP[v.R][nc] != -1) break;
                //if (SHIT[v.R][nc]==false)
                {
                    SHIT[v.R][nc] = true;
                    if (isneedtotrack)
                        SHIT_ROOT_POS[v.R][nc].R = -1;
                    //Debug.Log("(" + v.R + "," + nc );
                    SetShitVerticle(new Vec2(v.R, nc), isneedtotrack);
                }
            }
        }
    }
    public void SetShitHorizontal(Vec2 v, bool isneedtotrack = false)
    {
        int nc;
        for (int k = 0; k < 2; k++)
        {
            for (int l = 1; l < 10; l++)
            {
                nc = v.C + l * D[k];
                if (nc < 0 || nc >= COL) break;
                if (MAP[v.R][nc] != -1) break;
                if (SHIT[v.R][nc] == false)
                {
                    SHIT[v.R][nc] = true;
                    //Debug.Log("(" + v.R + "," + nc +"_ horifrom :" + v.Print());
                    if (isneedtotrack)
                    {
                        SHIT_ROOT_POS[v.R][nc].R = v.R;
                        SHIT_ROOT_POS[v.R][nc].C = v.C;
                    }
                }
            }
        }
    }
    public void SetShitVerticle(Vec2 v, bool isneedtotrack = false)
    {
        int nr;
        for (int k = 0; k < 2; k++)
        {
            for (int l = 1; l < 10; l++)
            {
                nr = v.R + l * D[k];
                if (nr < 0 || nr >= ROW) break;
                if (MAP[nr][v.C] != -1) break;
                if (SHIT[nr][v.C] == false)
                {
                    SHIT[nr][v.C] = true;
                    //Debug.Log("(" + nr + "," + v.C + "_ verfrom :" + v.Print());
                    if (isneedtotrack)
                    {
                        SHIT_ROOT_POS[nr][v.C].R = v.R;
                        SHIT_ROOT_POS[nr][v.C].C = v.C;
                    }
                }
            }
        }
    }
    public LPath CheckShit(Vec2 v, bool is_slow = false)
    {
        //Debug.Log("asd");
        bool b;
        if (is_slow) b = CheckShit_Slow(v);
        else b = (CheckShit_Fast(v));
        if (b)
        {
            if (POS1_SAVE.R == v.R && POS_SAVE_MIDDLE.R == v.R)
                return new LPath(new Vec2(POS1_SAVE.R, POS1_SAVE.C), new Vec2(v.R, v.C));
            if (POS1_SAVE.C == v.C && POS_SAVE_MIDDLE.C == v.C)
                return new LPath(new Vec2(POS1_SAVE.R, POS1_SAVE.C), new Vec2(v.R, v.C));

            if (POS1_SAVE.R == POS_SAVE_MIDDLE.R || POS1_SAVE.C == POS_SAVE_MIDDLE.C)
                return new LPath(new Vec2(POS1_SAVE.R, POS1_SAVE.C), new Vec2(POS_SAVE_MIDDLE.R, POS_SAVE_MIDDLE.C), new Vec2(v.R, v.C));


            return new LPath(new Vec2(POS1_SAVE.R, POS1_SAVE.C),
                SHIT_ROOT_POS[POS_SAVE_MIDDLE.R][POS_SAVE_MIDDLE.C],
                new Vec2(POS_SAVE_MIDDLE.R, POS_SAVE_MIDDLE.C),
                new Vec2(v.R, v.C));
            //if (POS_SAVE_MIDDLE.R != POS1_SAVE.R && POS_SAVE_MIDDLE.C != POS1_SAVE.C)
            //{ 

            // return new LPath(new Vec2(POS1_SAVE.R, POS1_SAVE.C), new Vec2(POS1_SAVE.R, POS_SAVE_MIDDLE.C), new Vec2(POS_SAVE_MIDDLE.R, POS_SAVE_MIDDLE.C), new Vec2(v.R, v.C));
            //}
            //if (POS_SAVE_MIDDLE.C == POS1_SAVE.C)
            //return new LPath(new Vec2(POS1_SAVE.R, POS1_SAVE.C), new Vec2(POS_SAVE_MIDDLE.R, POS1_SAVE.C), new Vec2(POS_SAVE_MIDDLE.R, POS_SAVE_MIDDLE.C), new Vec2(v.R, v.C));


        }
        return null;
    }
    public bool CheckShit_Slow(Vec2 v)
    {
        int dx = 0;
        int dy = 0;
        //bool b = false;
        if (POS1.R == v.R)
        {
            //Debug.Log("11111111111111111111");
            dx = 0; dy = 1;
            if (CheckShit_Slow_Child(v, dx, dy)) return true;
            dx = 0; dy = -1;
            if (CheckShit_Slow_Child(v, dx, dy)) return true;
            dx = 1; dy = 0;
            if (CheckShit_Slow_Child(v, dx, dy)) return true;
            dx = -1; dy = 0;
            if (CheckShit_Slow_Child(v, dx, dy)) return true;
        }
        else if (POS1.C == v.C)
        {
            //Debug.Log("222222222222222222222");
            dx = 1; dy = 0;
            if (CheckShit_Slow_Child(v, dx, dy)) return true;
            dx = -1; dy = 0;
            if (CheckShit_Slow_Child(v, dx, dy)) return true;
            dx = 0; dy = 1;
            if (CheckShit_Slow_Child(v, dx, dy)) return true;
            dx = 0; dy = -1;
            if (CheckShit_Slow_Child(v, dx, dy)) return true;
        }
        else
            return CheckShit_Fast(v, true);
        return false;
    }
    public bool CheckShit_Fast(Vec2 v, bool is_find_best_way = false)
    {
        int nr, nc;
        CheckShit_Fast_temp = null;
        for (int k = 0; k < 4; k++)
        {
            for (int l = 1; l < 10; l++)
            {
                nr = v.R + l * DY[k];
                nc = v.C + l * DX[k];
                //Debug.Log(nr + " " + nc);
                if (nr < 0 || nr >= ROW || nc < 0 || nc >= COL) break;

                if (MAP[nr][nc] != -1) break;
                if (SHIT[nr][nc] == true)
                {
                    if (is_find_best_way == false || SHIT_ROOT_POS[nr][nc].R == -1)
                    {
                        POS_SAVE_MIDDLE = new Vec2(nr, nc);
                        return true;
                    }
                    else if (CheckShit_Fast_temp == null)
                    {
                        CheckShit_Fast_temp = new Vec2(nr, nc);
                    }
                }
            }
        }
        if (CheckShit_Fast_temp != null)
        {
            POS_SAVE_MIDDLE = new Vec2(CheckShit_Fast_temp.R, CheckShit_Fast_temp.C);
            return true;
        }
        return false;
    }
    public bool CheckShit_Slow_Child(Vec2 v, int dx, int dy)
    {
        int nr, nc;
        for (int l = 1; l < 10; l++)
        {
            nr = v.R + l * dx;
            nc = v.C + l * dy;
            if (nr < 0 || nr >= ROW || nc < 0 || nc >= COL) break;
            if (nr == POS1.R && nc == POS1.C)
            {
                POS_SAVE_MIDDLE = new Vec2(POS1.R, POS1.C);
                return true;
            }
            if (MAP[nr][nc] != -1) break;
            if (SHIT[nr][nc] == true)
            {
                POS_SAVE_MIDDLE = new Vec2(nr, nc);
                return true;
            }
        }
        return false;
    }


    //bài 5
    public void CheckAndSwapThings()
    {
        //LLSound.I.playWrong();
        while (CheckIsAvailable() == false)
        {
            Debug.Log("CheckAndSwapThings(): ResetMap here");
            ResetMap();
        }

    }
    Vec2 HINT_POS0;
    Vec2 HINT_POS1;
    int tcount;
    public bool CheckIsAvailable()
    {
        tcount = 0;
        // CoundAndLog();
        HINT_POS0 = null;
        HINT_POS1 = null;
        for (int i = 1; i < ROW - 1; i++)
            for (int j = 1; j < COL - 1; j++)
            {
                if (MAP[i][j] != -1)
                {
                    tcount++;
                    //tcount++;
                    ResetShit();
                    SetShit(new Vec2(i, j), true);

                    for (int i2 = 1; i2 < ROW - 1; i2++)
                        for (int j2 = 1; j2 < COL - 1; j2++)
                        {
                            if (i2 <= i && j2 <= j) continue;
                            if (MAP[i2][j2] == MAP[i][j])
                                if (CheckShit_Fast(new Vec2(i2, j2)))
                                {
                                    HINT_POS0 = new Vec2(i, j);
                                    HINT_POS1 = new Vec2(i2, j2);
                                    return true;
                                }
                        }
                }
            }
        //Debug.Log("tcount=" + tcount);
        if (tcount == 0)
        {
            //ItemManager.I.YouWin();
            return true;
        }
        return false;
    }
    public void ResetMap()
    {
        for (int i = 1; i < ROW - 1; i++)
            for (int j = 1; j < COL - 1; j++)
                if (MAP[i][j] != -1)
                {
                    bool is_swaped = false;
                    for (int i2 = 1; i2 < ROW - 1; i2++)
                    {
                        for (int j2 = 1; j2 < COL - 1; j2++)
                        {
                            if (i2 <= i && j2 <= j) continue;
                            if (MAP[i2][j2] != -1)
                            {
                                if (Random.Range(0, 2) == 0)
                                {
                                    Swap(new Vec2(i, j), new Vec2(i2, j2));
                                }
                                is_swaped = true;
                                break;
                            }
                        }
                        if (is_swaped) break;
                    }
                }
    }
    public void Swap(Vec2 v0, Vec2 v1)
    {
        int index0 = MAP[v0.R][v0.C];
        int index1 = MAP[v1.R][v1.C];
        MAP[v0.R][v0.C] = index1;
        MAP[v1.R][v1.C] = index0;
        //ItemManager.I.Swap(v0, v1);
    }
}
