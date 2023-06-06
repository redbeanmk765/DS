using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.Text;


public class mapCreator : MonoBehaviour
{
    public Rooms rooms;
    public WallType wallType;
    public GameObject Map;
    public TileBase floorTile;
    public TileBase ceiling2Tile;
    public TileBase wallTile;
    public TileBase wall2Tile;
    static List<Room> roomList = new List<Room>();
  

    public int RoomX;
    public int RoomY;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = -80; x < 80; x++)
        {
            for (int y = -54; y < 54; y++)
            {
                Map.gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(RoomX + x, RoomY + y, 0), ceiling2Tile);
            }
        }

        for (int x = -8; x < 8; x++)
        {
            for (int y = -6; y < 6; y++)
            {
                Map.gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(RoomX + x, RoomY + y, 0), floorTile);
            }
        }
        CreateDungeon();



        for (int i = 0; i < roomList.Count; ++i)
        {
            GameObject TmpRoom;
            TmpRoom = Instantiate(rooms.EasyRooms[Random.Range(0, rooms.EasyRooms.Count)]);
            TmpRoom.transform.position = new Vector3Int(roomList[i].roomPos.x + 250, roomList[i].roomPos.y + 125, 0);

            for (int x = -8; x < 8; x++)
            {
                for (int y = -6; y < 6; y++)
                {
                    Map.gameObject.GetComponent<Tilemap>().SetTile(new Vector3Int(roomList[i].roomPos.x + x + 250, roomList[i].roomPos.y + y + 125, 0), floorTile);
                }
               
            }

            switch (roomList[i].dir) {
                
                case "URDL":
                    TmpRoom = Instantiate(wallType.Wall_URDL);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                        break;


                case "URD":
                    TmpRoom = Instantiate(wallType.Wall_URD);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                    break;

                case "URL":
                     TmpRoom = Instantiate(wallType.Wall_URL);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                    break;

                case "UDL":
                     TmpRoom = Instantiate(wallType.Wall_UDL);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                    break;

                case "RDL":
                    TmpRoom = Instantiate(wallType.Wall_RDL);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                    break;


                case "UR":
                    TmpRoom = Instantiate(wallType.Wall_UR);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                    break;


                case "UD":
                    TmpRoom = Instantiate(wallType.Wall_UD);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                    break;


                case "UL":
                    TmpRoom = Instantiate(wallType.Wall_UL);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                    break;

                case "RD":
                    TmpRoom = Instantiate(wallType.Wall_RD);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                    break;

                case "RL":
                    TmpRoom = Instantiate(wallType.Wall_RL);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                    break;

                case "DL":
                    TmpRoom = Instantiate(wallType.Wall_DL);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                    break;


                case "U":
                    TmpRoom = Instantiate(wallType.Wall_U);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                    break;

                case "R":
                    TmpRoom = Instantiate(wallType.Wall_R);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                    break;

                case "D":
                    TmpRoom = Instantiate(wallType.Wall_D);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                    break;

                case "L":
                    TmpRoom = Instantiate(wallType.Wall_L);
                    TmpRoom.transform.SetParent(Map.transform);
                    TmpRoom.transform.position = roomList[i].roomPos;
                    break;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public int levelRooms;
    static public int madeRooms;
    static int[] neighborNum = new int[100];
    static int[] room = new int[100];
    static int[] endRooms = new int[100];
    static List<int> endRoomsNum = new List<int>();
    static public int steps;
    static public int boosRoomStep;
    static public int boosRoomNum;
    

    public enum Diff
    {
        Chest,
        Shop,
        Boss,
        Easy,
        Norm,
        Hard,
        Start
    }
    public class Room
    {
        public int roomNum;
        public Vector3Int roomPos;
        public string dir;
        public Diff roomDiff;

        public Room(int num, string direction, Diff diff)
        {
            roomNum = num;
            dir = direction;
            roomDiff = diff;
            roomPos = new Vector3Int ( (((num % 10) - 5) * 16) - 250, (((num / 10) - 4) * -12 ) - 125, 0);


        }

    }
    static void CreateDungeon()
    {

        int startRoom = 45;
        levelRooms = 9;


        while (true)
        {
            visit(room, startRoom);
            if (madeRooms > levelRooms && endRoomsNum.Count > 3 && neighborNum[45] >= 2)
            {
                break;
            }
            for (int i = 0; i <= 99; i++)
            {
                room[i] = 0;
                neighborNum[i] = 0;
                endRooms[i] = 0;
                
            }
            madeRooms = 0;
            endRoomsNum.Clear();
            steps = 0;
            boosRoomStep = 0;
            boosRoomNum = 0;
        }

        endRoomsNum = ShuffleList<int>(endRoomsNum);


        for (int i = 0; i < endRoomsNum.Count(); i++)
        {
            if (endRoomsNum[i] == boosRoomNum)
            {
                endRoomsNum.RemoveAt(i);
            }

        }

        int chestNum = endRoomsNum[0];
        int shopNum = endRoomsNum[1];

        for (int i = 0; i <= 9; i++)
        {
            for (int j = 0; j <= 9; j++)
            {
                if (room[i * 10 + j] == 1)
                {
                    int num = i * 10 + j;

                    StringBuilder dirB = new StringBuilder();
                    Diff tmpDiff = Diff.Easy;

                    if (room[num - 10] == 1)
                    {
                        dirB.Append("U");
                    }
                    if (room[num + 1] == 1)
                    {
                        dirB.Append("R");
                    }
                    if (room[num + 10] == 1)
                    {
                        dirB.Append("D");
                    }
                    if (room[num - 1] == 1)
                    {
                        dirB.Append("L");
                    }

                    if (num == chestNum)
                    {
                        tmpDiff = Diff.Chest;
                    }
                    else if (num == shopNum)
                    {
                        tmpDiff = Diff.Shop;
                    }
                    else if (num == boosRoomNum)
                    {
                        tmpDiff = Diff.Boss;
                    }
                    else if (num == 45)
                    {
                        tmpDiff = Diff.Start;
                    }
                    else
                    {
                        int chance = Random.Range(0, 4);

                        switch (chance)
                        {
                            case 0:
                                tmpDiff = Diff.Hard;
                                break;
                            case 1:
                            case 2:
                                tmpDiff = Diff.Norm;
                                break;
                            case 3:
                                tmpDiff = Diff.Easy;
                                break;
                        }
                    }
                    string dir = dirB.ToString();

                    roomList.Add(new Room(i * 10 + j, dir, tmpDiff));
                }

            }
        }

        for (int i = 0; i <= 9; i++)
        {
            for (int j = 0; j <= 9; j++)
            {
               


            }

        }


        for (int i = 0; i < roomList.Count(); i++)
        {

        

           

        }

        for (int i = 0; i < endRoomsNum.Count(); i++)
        {

         
            

        }


        
  
    }

    static public void visit(int[] room, int roomNum)
    {
        steps++;
        while (madeRooms <= levelRooms)
        {

            if (roomNum / 10 == 0 || roomNum % 10 == 0 || roomNum / 10 == 9 || room[roomNum] != 0 || neighborNum[roomNum] >= 2)
            {
                steps--;
                return;

            }
            int chance = Random.Range(0, 2);
            if (roomNum != 45)
                if (chance == 1)
                {
                    steps--;
                    return;

                }


            room[roomNum] = 1;
            madeRooms++;




            int[] nextRoomNum = new int[4];
            int[] dir = { 0, 1, 2, 3 };

            nextRoomNum[0] = roomNum - 10;
            nextRoomNum[1] = roomNum + 1;
            nextRoomNum[2] = roomNum + 10;
            nextRoomNum[3] = roomNum - 1;

            for (int i = 0; i <= 3; i++)
            {
                neighborNum[nextRoomNum[i]]++;
            }


            int tmp;

            for (int i = 0; i < dir.Length; ++i)
            {
                int random1 = UnityEngine.Random.Range(0, dir.Length);
                int random2 = UnityEngine.Random.Range(0, dir.Length);

                tmp = dir[random1];
                dir[random1] = dir[random2];
                dir[random2] = tmp;
            }


            for (int i = 0; i <= 3; i++)
            {
                visit(room, nextRoomNum[dir[i]]);
            }

            for (int i = 0; i <= 3; i++)
            {
                if (room[roomNum] == 1)
                    endRooms[roomNum] += room[nextRoomNum[i]];
            }

            if (endRooms[roomNum] == 1)
            {
                endRoomsNum.Add(roomNum);
                if (boosRoomStep <= steps)
                {
                    boosRoomStep = steps;
                    boosRoomNum = roomNum;
                }
            }
        }
        steps--;
        return;

    }

    static public List<T> ShuffleList<T>(List<T> list)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < list.Count; ++i)
        {
            random1 = Random.Range(0, list.Count);
            random2 = Random.Range(0, list.Count);

            temp = list[random1];
            list[random1] = list[random2];
            list[random2] = temp;
        }

        return list;
    }

}

