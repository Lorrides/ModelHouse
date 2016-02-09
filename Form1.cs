using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelHouse
{
    public partial class Form1 : Form
    {
        private int Moves;
        private Location currentLocation;

        private RoomWithDoor livingRoom;
        private Room diningRoom;
        private RoomWithDoor kitchen;
        private Room stairs;
        private RoomWithHidingPlace hallway;
        private RoomWithHidingPlace bathroom;
        private RoomWithHidingPlace masterBedroom;
        private RoomWithHidingPlace secondBedroom;

        private OutsideWithDoors frontYard;
        private OutsideWithDoors backYard;
        private OutsideWithHidingPlace garden;
        private OutsideWithHidingPlace driveway;

        private Opponent opponent;




        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            opponent = new Opponent(frontYard);
            ResetGame(false);
        }

        private void MoveToANewLocation(Location newLocation)
        {
            Moves++;
            currentLocation = newLocation;
            RedrawForm();
        }


        private void RedrawForm()
        {

            exits.Items.Clear();
            for (int i = 0; i < currentLocation.Exits.Length; i ++)
                exits.Items.Add(currentLocation.Exits[i].Name);
            exits.SelectedIndex = 0;

            description.Text = currentLocation.Description + "\r \n (move #" + Moves + ")";

            if (currentLocation is IHidingPlace)
            {
                IHidingPlace hidingPlace = currentLocation as IHidingPlace;
                check.Text = "Check" + hidingPlace.HidingPlaceName;
                check.Visible = true;
            }
            else
                check.Visible = false;
            if (currentLocation is IHasExteriorDoor)
                goThroughTheDoor.Visible = true;
            else
                goThroughTheDoor.Visible = false;
        }




        private void CreateObjects()
        {
            livingRoom = new RoomWithDoor("Living Room", "an antique carpet",
                      "inside the closet", "an oak door with a brass handle");
            diningRoom = new RoomWithHidingPlace("Dining Room", "a crystal chandelier",
                       "in the tall armoire");
            kitchen = new RoomWithDoor("Kitchen", "stainless steel appliances",
                      "in the cabinet", "a screen door");
            stairs = new Room("Stairs", "a wooden bannister");
            hallway = new RoomWithHidingPlace("Upstairs Hallway", "a picture of a dog",
                      "in the closet");
            bathroom = new RoomWithHidingPlace("Bathroom", "a sink and a toilet",
                      "in the shower");
            masterBedroom = new RoomWithHidingPlace("Master Bedroom", "a large bed",
                      "under the bed");
            secondBedroom = new RoomWithHidingPlace("Second Bedroom", "a small bed",
                      "under the bed");

            frontYard = new OutsideWithDoors("Front Yard", false, "a heavy-looking oak door");
            backYard = new OutsideWithDoors("Back Yard", true, "a screen door");
            garden = new OutsideWithHidingPlace("Garden", false, "inside the shed");
            driveway = new OutsideWithHidingPlace("Driveway", true, "in the garage");

            diningRoom.Exits = new Location[] { livingRoom, kitchen };
            livingRoom.Exits = new Location[] { diningRoom, stairs };
            kitchen.Exits = new Location[] { diningRoom };
            stairs.Exits = new Location[] { livingRoom, hallway };
            hallway.Exits = new Location[] { stairs, bathroom, masterBedroom, secondBedroom };
            bathroom.Exits = new Location[] { hallway };
            masterBedroom.Exits = new Location[] { hallway };
            secondBedroom.Exits = new Location[] { hallway };
            frontYard.Exits = new Location[] { backYard, garden, driveway };
            backYard.Exits = new Location[] { frontYard, garden, driveway };
            garden.Exits = new Location[] { backYard, frontYard };
            driveway.Exits = new Location[] { backYard, frontYard };

            livingRoom.DoorLocation = frontYard;
            frontYard.DoorLocation = livingRoom;

            kitchen.DoorLocation = backYard;
            backYard.DoorLocation = kitchen;
        }



        private void goHere_Click(object sender, EventArgs e)
        {
            MoveToANewLocation(currentLocation.Exits[exits.SelectedIndex]);
        }

        private void goThroughTheDoor_Click(object sender, EventArgs e)
        {
            IHasExteriorDoor hasDoor = currentLocation as IHasExteriorDoor;
            MoveToANewLocation(hasDoor.DoorLocation);
        }


        private void check_Click(object sender, EventArgs e)
        {

        }
    }
}
