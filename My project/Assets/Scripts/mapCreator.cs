using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class mapCreator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
    static Queue<int> endRoomsNum = new Queue<int>();
    static public int steps;
    static public int boosRoomStep;
    static public int boosRoomNum;

    static void Main(string[] args)
    {

        int startRoom = 45;
        levelRooms = 15;


        while (true)
        {
            visit(room, startRoom);
            if (madeRooms > levelRooms && endRoomsNum.Count > 5)
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

        for (int i = 0; i <= 8; i++)
        {
            for (int j = 0; j <= 9; j++)
            { 
            }
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
            int chance = Random.Range(0,2);
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
                endRoomsNum.Enqueue(roomNum);
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
}
