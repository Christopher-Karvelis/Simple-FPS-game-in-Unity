using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerainCreator : MonoBehaviour
{
   
    private int Updates;
    private int terainSize;
    private int livesPointer;
    private int previous_level;

    public static int pause;
    public static int[,,] grid;

    public static bool run;
    private bool finished;
    private bool drop_cubes;
    private bool game_is_paused;
    private bool cubes_have_fallen;

    private bool player_reached_halfway;

    public GameObject fps;
    public GameObject Score;
    public GameObject timer;
    public GameObject cursor;
    public GameObject border;
    public GameObject pauseM;
    public GameObject controlsM;
    public GameObject winMenu;
    public GameObject cylinder;
    public GameObject GameOver;
    public GameObject MainMenu;
    public GameObject SpotLight;
    public GameObject pauseMenu;
    public GameObject controlsMenu;
    public GameObject character;
    public GameObject gray_cube;
    public GameObject health_bar;
    public GameObject winningCube;
    public GameObject StartingCube;
    public GameObject[] coloredCube;

    private GameObject player;
    private GameObject[,,] Cubes;
    private GameObject[,] GrayCubes;
    private GameObject crossCursor;
  
    private GameObject countdown;

    private Vector3 LastPos;
    private FirstPersonController rigidΒody;

    //Get input From InputMenu Script and inialize Size
    public void InitializeSize(int size)
    {      
        pause = 1;
        livesPointer = 3;
        terainSize = size;
        previous_level = 0;
        player_reached_halfway = false;

        Debug.Log("Terain Creator got the value = " + terainSize);
        grid = new int[terainSize, terainSize, terainSize * 10];
        Cubes = new GameObject[terainSize, terainSize, terainSize * 10];
        GrayCubes = new GameObject[terainSize, terainSize];
        CreateBaseLevel();
        rigidΒody = player.GetComponent<FirstPersonController>();

        run = true;
    }

    // Use this for initialization
    void Start()
    {
        Updates = 0;
        run = false;
        finished = false;
        drop_cubes = false;
        game_is_paused = false;
        cubes_have_fallen = false;
        countdown = Instantiate(timer);
        GameObject start = Instantiate(MainMenu);
    }

    private void LateUpdate()
    {
        if (run)
        {
            if (rigidΒody.m_CharacterController.isGrounded)
            {
                LastPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            }

            if (rigidΒody.m_CharacterController.isGrounded)
            {
                CheckLevelFall();
                CheckFinalLevel();
                CheckWin();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            Updates++;
            //Reset update counter for better memory managment
            if (Updates == 10000)
            {
                Updates = 0;
            }

            //Every product of three frames drop cubes
            if (drop_cubes && Updates % 3 == 0)
            {
                drop_cubes = false;
                DropCubes();
                if (!drop_cubes && cubes_have_fallen)
                {
                    Scoring.scoreValue += 30;
                    cubes_have_fallen = false;
                }
            }

            if (!game_is_paused)
            {
                crossCursor.transform.GetChild(0).transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                KickCubes();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                DeleteAllCubes();
            }

            if (finished)
            {
                GameObject win = Instantiate(winMenu);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                finished = false;
                run = false;
            }

            if (Input.GetKeyDown("escape") && pause == 1)
            {
                game_is_paused = true;
                pauseMenu = Instantiate(pauseM);
               
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pause = 0;
            }

            if (Input.GetKeyDown("tab") && pause == 1)
            {
                game_is_paused = true;
                controlsMenu = Instantiate(controlsM);
             
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pause = 0;
            }

            //if player falls
            if (player.transform.position.y < -5)
            {
                ReduceLifeSpan();
                RetunToPreviousPoint();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                drop_cubes = true;
            }
        }
    }

    public void BuildCylinder(int x, int y, int z)
    {
        if (IsInside(x, y, z))
        {
            if (grid[x, z, y + 1] == 0 && grid[x, z, y] == 0)
            {
                SpawnCylinder(new Vector3(x, y + 0.5f, z));
                grid[x, z, y + 1] = 2;
                grid[x, z, y] = 2;
                Scoring.scoreValue += 20;
                CylinderStash.remain -= 1;
            }
        }
    }

    public void BuildCube(int x, int y, int z)
    {
        if (IsInside(x, y, z))
        {
            if (grid[x, z, y] == 0)
            {
                GenerateRandomBlock(new Vector3(x, y, z));
                grid[x, z, y] = 1;
                Scoring.scoreValue += 10;
                CubeStash.remain -= 1;
            }
        }
    }

    public void DeleteCube(int x, int y, int z)
    {
        Destroy(Cubes[x, z, y]);
        Cubes[x, z, y] = null;
        grid[x, z, y] = 0;
    }

    private void DropCubes()
    {
        for (int x = 0; x < terainSize; x++)
        {
            for (int z = 0; z < terainSize; z++)
            {
                for (int y = 1; y < terainSize * 2; y++)
                {
                    if (grid[x, z, y] == 1)
                    {
                        if (y - 1 == 0 && grid[x, z, y - 1] == 0)
                        {
                            MoveCube(x, y, z, x, y - 1, z);
                            DeleteCube(x, 0, z);
                            cubes_have_fallen = true;
                        }
                        else if (grid[x, z, y - 1] == 0 && (((y - 1) != ((int)player.transform.position.y)) || Math.Round(player.transform.position.x) != x || Math.Round(player.transform.position.z) != z))
                        {
                            MoveCube(x, y, z, x, y - 1, z);
                            drop_cubes = true;
                            cubes_have_fallen = true;
                        }
                    }
                }
            }
        }
    }

    private void KickCubes()
    {
        int x = (int)Math.Round(player.transform.position.x);
        int y = (int)player.transform.position.y;
        int z = (int)Math.Round(player.transform.position.z);
        string direction = "";

        if (player.transform.rotation.eulerAngles.y <= 45 || player.transform.rotation.eulerAngles.y >= 315)
        {
            z++;
            direction = "z-positive";
        }
        else if (player.transform.rotation.eulerAngles.y > 45 && player.transform.rotation.eulerAngles.y <= 135)
        {
            x++;
            direction = "x-positive";
        }
        else if (player.transform.rotation.eulerAngles.y > 135 && player.transform.rotation.eulerAngles.y <= 229)
        {
            z--;
            direction = "z-negative";
        }
        else if (player.transform.rotation.eulerAngles.y > 229 && player.transform.rotation.eulerAngles.y <= 315)
        {
            x--;
            direction = "x-negative";
        }

        if (IsInside(x, y, z))
        {
            if (grid[x, z, y] == 1)
            {
                if (direction == "z-positive")
                {
                    bool move_cubes = true;
                    int next_available_position = z + 1;
                    while (IsInside(x, y, next_available_position))
                    {
                        if (grid[x, next_available_position, y] == 1)// there is a cube behind go to next position
                        {
                            next_available_position++;
                        }
                        else if (grid[x, next_available_position, y] == 2)// there is a cylinder behind dont move cubes
                        {
                            move_cubes = false;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (move_cubes)
                    {
                        //cube reached outside the boarders delete it
                        if (!(IsInside(x, y, next_available_position)))
                        {
                            next_available_position--;
                            DeleteCube(x, y, next_available_position);
                        }
                        next_available_position--;

                        for (int i = next_available_position; i >= z; i--)
                        {
                            MoveCube(x, y, z, x, y, z + 1);
                        }
                    }
                }

                if (direction == "x-positive")
                {
                    bool move_cubes = true;
                    int next_available_position = x + 1;
                    while (IsInside(next_available_position, y, z))
                    {
                        if (grid[next_available_position, z, y] == 1)
                        {
                            next_available_position++;
                        }
                        else if (grid[next_available_position, z, y] == 2)
                        {
                            move_cubes = false;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (move_cubes)
                    {
                        //cube reached outside the boarders delete it
                        if ((!IsInside(next_available_position, y, z)))
                        {
                            next_available_position--;
                            DeleteCube(next_available_position, y, z);
                        }
                        next_available_position--;

                        for (int i = next_available_position; i >= x; i--)
                        {
                            MoveCube(x, y, z, x + 1, y, z);
                        }
                    }
                }

                if (direction == "z-negative") {
                    bool move_cubes = true;
                    int next_available_position = z - 1;
                    while (IsInside(x, y, next_available_position))
                    {
                        if (grid[x, next_available_position, y] == 1)
                        {
                            next_available_position--;
                        }
                        else if (grid[x, next_available_position, y] == 2)
                        {
                            move_cubes = false;
                            break;
                        } else
                        {
                            break;
                        }
                    }

                    if (move_cubes)
                    {
                        //cube reached outside the boarders delete it
                        if ((!IsInside(x, y, next_available_position)))
                        {
                            next_available_position++;
                            DeleteCube(x, y, next_available_position);
                        }
                        next_available_position++;

                        for (int i = next_available_position; i <= z; i++)
                        {
                            MoveCube(x, y, z, x, y, z - 1);
                        }
                    }
                }

                if (direction == "x-negative")
                {
                    bool move_cubes = true;
                    int next_available_position = x - 1;
                    while (IsInside(next_available_position, y, z))
                    {
                        if (grid[next_available_position, z, y] == 1)
                        {
                            next_available_position--;
                        }
                        else if (grid[next_available_position, z, y] == 2)
                        {
                            move_cubes = false;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (move_cubes)
                    {
                        //cube reached outside the boarders delete it
                        if ((!IsInside(next_available_position, y, z)))
                        {
                            next_available_position++;
                            DeleteCube(next_available_position, y, z);
                        }
                        next_available_position++;

                        for (int i = next_available_position; i <= x; i++)
                        {
                            MoveCube(x, y, z, x - 1, y, z);
                        }
                    }
                }
            }
        }
    }

    private void MoveCube(int x, int y, int z, int x_new, int y_new, int z_new)
    {
        Cubes[x, z, y].transform.position = new Vector3(x_new, y_new, z_new);
        grid[x, z, y] = 0;
        grid[x_new, z_new, y_new] = 1;
        Cubes[x_new, z_new, y_new] = Cubes[x, z, y];
        Cubes[x, z, y] = null;
    }

    private void DeleteAllCubes()
    {
        int x = (int)Math.Round(player.transform.position.x); ;
        int y = 0;
        int z = (int)Math.Round(player.transform.position.z);

        if (player.transform.rotation.eulerAngles.y <= 45 || player.transform.rotation.eulerAngles.y >= 315)
        {
            z++;
        }
        else if (player.transform.rotation.eulerAngles.y > 45 && player.transform.rotation.eulerAngles.y <= 135)
        {
            x++;
        }
        else if (player.transform.rotation.eulerAngles.y > 135 && player.transform.rotation.eulerAngles.y <= 229)
        {
            z--;
        }
        else if (player.transform.rotation.eulerAngles.y > 229 && player.transform.rotation.eulerAngles.y <= 315)
        {
            x--;
        }

        bool deletion_done = false;
        if (IsInside(x, y, z))
        {
            while (y < terainSize * 2)
            {
                if (grid[x, z, y] == 1)
                {
                    Destroy(Cubes[x, z, y]);
                    grid[x, z, y] = 0;
                    deletion_done = true;
                }
                y++;
            }
        }
        if (deletion_done)
        {
            Scoring.scoreValue -= 20;
            livesRemain.remain++;
        }
    }

    private void CheckWin()
    {
        if ((int)player.transform.position.y - 2 == terainSize * 2)
        {
            if (Math.Round(player.transform.position.x) == GameObject.Find("Winning-Cube(Clone)").transform.position.x)
            {
                if (Math.Round(player.transform.position.z) == GameObject.Find("Winning-Cube(Clone)").transform.position.z)
                {
                    finished = true;
                    Destroy(countdown);
                }
            }
        }
    }

    private void CheckFinalLevel()
    {
        if ((int)player.transform.position.y == terainSize && !player_reached_halfway)
        {
            DestroyGrayCubes();
            Scoring.scoreValue += 100;
            player_reached_halfway = true;
            Timer.countdown_is_enabled = true;
            Timer.time_counter = terainSize * 3;
            
            GenerateWinCube();
            if (livesPointer <= 3)
            {
                livesRemain.remain += 1;
            }
        }
    }

    private void CheckLevelFall()
    {
        if ((int)player.transform.position.y > previous_level)
        {
            previous_level = (int)player.transform.position.y;
            Scoring.scoreValue += 10;

        }
        else if ((int)player.transform.position.y < previous_level)
        {
            Scoring.scoreValue -= (10 * ((previous_level) - (int)player.transform.position.y));
            previous_level = (int)player.transform.position.y;

        }
        else if ((int)player.transform.position.y < previous_level)
        {
            previous_level = (int)player.transform.position.y;
        }
    }

    private void CreateBaseLevel()
    {
        GenerateStartingPoint();
        GenerateGrayBlock(new Vector3(terainSize / 2, terainSize+1, terainSize / 2));
        for (int i = 0; i < terainSize; i++)
        {
            for (int j = 0; j < terainSize; j++)
            {
                if (!((i == terainSize / 2) && (j == terainSize / 2)))
                {
                    GenerateRandomBlock(new Vector3(i, 0, j));
                    GenerateGrayBlock(new Vector3(i, terainSize+1, j));
                    grid[i, j, 0] = 1;
                }
            }
        }
        SpawnCharacter();
        SpawnSpotLights();
        SpawnCursor();
        SpawnLives();
        CreateBorders();
    }

    private void CreateBorders()
    {
        GameObject FrontBound = Instantiate(border);
        GameObject BackBound = Instantiate(border);
        GameObject LeftBound = Instantiate(border);
        GameObject RightBound = Instantiate(border);

        FrontBound.transform.position = new Vector3((terainSize - 0.5f), (terainSize / 2) + 0.5f, (terainSize / 2));
        FrontBound.transform.localScale = new Vector3((float)terainSize / 2, (float)terainSize / 2, (float)terainSize / 2);
        FrontBound.transform.Rotate(0, 0, 90);

        BackBound.transform.position = new Vector3(-0.5f, (terainSize / 2) + 0.5f, (terainSize / 2));
        BackBound.transform.localScale = new Vector3((float)terainSize / 2, (float)terainSize / 10, (float)terainSize / 2);
        BackBound.transform.Rotate(0, 180, 90);

        RightBound.transform.position = new Vector3(terainSize / 2, (terainSize / 2) + 0.5f, -0.5f);
        RightBound.transform.localScale = new Vector3((float)terainSize / 2, (float)terainSize / 2, (float)terainSize / 2);
        RightBound.transform.Rotate(0, 90, 90);


        LeftBound.transform.position = new Vector3(terainSize / 2, (terainSize / 2) + 0.5f, (terainSize - 0.5f));
        LeftBound.transform.localScale = new Vector3((float)terainSize / 2, (float)terainSize / 2, (float)terainSize / 2);
        LeftBound.transform.Rotate(0, -90, 90);
    }

    private void GenerateStartingPoint()
    {
        GameObject startingPoint = Instantiate(StartingCube);
        startingPoint.transform.position = new Vector3(terainSize / 2, 0, terainSize / 2);
        grid[terainSize / 2, terainSize / 2, 0] = 1;
    }

    private void GenerateWinCube()
    {
        GameObject winBox = Instantiate(winningCube);
        winBox.transform.position = new Vector3(terainSize / 2, terainSize * 2 + 1, terainSize / 2);
        grid[(terainSize / 2), (terainSize / 2), (terainSize * 2 + 1) - 1] = 1;
    }

    private void SpawnCharacter()
    {
        player = Instantiate(fps);
        player.transform.position = new Vector3(terainSize / 2, 1, terainSize / 2);
        LastPos = new Vector3(terainSize / 2, 1, terainSize / 2);
    }

    private void SpawnSpotLights()
    {
        GameObject SpotLight1 = Instantiate(SpotLight);
        GameObject SpotLight2 = Instantiate(SpotLight);

        SpotLight1.transform.position = new Vector3(0, terainSize, 0);
        SpotLight2.transform.position = new Vector3(terainSize, terainSize, terainSize);
    }

    private void SpawnCursor()
    {
        Cursor.visible = false;
        crossCursor = Instantiate(cursor);
        GameObject score = Instantiate(Score);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SpawnLives()
    {
        Instantiate(health_bar);
    }

    public void GenerateGrayBlock(Vector3 pos)
    {
        GameObject gray_block = Instantiate(gray_cube);
        gray_block.transform.position = new Vector3(pos.x, pos.y, pos.z);
        GrayCubes[(int)pos.x, (int)pos.z] = gray_block;
    }

    public void DestroyGrayCubes()
    {
        foreach(GameObject Cube in GrayCubes)
        {
            Destroy(Cube);
        }
    }

    public void GenerateRandomBlock(Vector3 pos)
    {
        int rand = UnityEngine.Random.Range(0, 5);
        GameObject colored_block = Instantiate(coloredCube[rand]);
        colored_block.transform.position = new Vector3(pos.x, pos.y, pos.z);
        Cubes[(int)pos.x, (int)pos.z, (int)pos.y] = colored_block;
    }

    public void SpawnCylinder(Vector3 pos)
    {
        GameObject Cylinder = Instantiate(cylinder);
        Cylinder.transform.position = new Vector3(pos.x, pos.y, pos.z);
    }

    public void ReduceLifeSpan()
    {
        //Reduce health bar
        livesRemain.remain -= 1;

        //If no tries left load main menu
        if (livesRemain.remain <= 0)
        {
            Instantiate(GameOver);
            run = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(countdown);
        }
    }

    public void RetunToPreviousPoint()
    {
        //Move Player Back to center
        if (grid[(int)Math.Round(LastPos.x), (int)Math.Round(LastPos.z), (int)LastPos.y] != 1)
        {
            for (int k = 0; k < terainSize * 2; k++)
            {
                for (int j = 0; j < terainSize; j++)
                { 
                    for (int i = 0; i < terainSize; i++)
                    {
                        if(grid[j,i,k] == 1)
                        {
                            player.transform.position = new Vector3(j,k+2, i);
                            k = terainSize * 2;
                            j = terainSize;
                            i = terainSize;                          
                        }
                    }
                }
            }
        }else
        {
            player.transform.position = new Vector3(LastPos.x, LastPos.y, LastPos.z);
        }
    }
   
    public bool IsInside(int x, int y, int z)
    {
        if (x >= terainSize || z >= terainSize || x < 0 || z < 0 || y > terainSize * 2)
        {
            return false;
        }
        return true;
    }
    
    public bool IsInsideJump(int x, int y, int z)
    {
        if (x >= terainSize || z >= terainSize || x < 0 || z < 0 || (y <= terainSize * 2 && y<=0))
        {
            return false;
        }
        return true;
    }
}   
