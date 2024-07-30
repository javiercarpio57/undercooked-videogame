using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Knife myKnife;

    CharacterController characterController;
    Animator anim;
    Holder myHolder;
    Timing timing;

    FurnitureDetection furnitureDetection;

    public float speed = 0.1f;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 rotation;

    private bool isChopping = false;

    private Chopper currentChopper = null;
    private Washer washer = null;

    private bool boost = false;
    private float timeBoost;

    public KeyCode take;
    public KeyCode CutAndWash;
    public KeyCode createFood;
    public KeyCode keyBoost;
    public string horizontal;
    public string vertical;

    public int countPlates = 0;

    public MusicElements musicElements;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        myHolder = GetComponent<Holder>();
        furnitureDetection = GetComponent<FurnitureDetection>();
        myKnife.gameObject.SetActive(false);
        timeBoost = 1f;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        
        if (Input.GetKeyDown(take))
        {
            Furniture currentFurniture = furnitureDetection.getSelected();
            if (currentFurniture != null)
            {
                washer = currentFurniture.GetComponent<Washer>();
                if (washer != null)
                {
                    if (washer.hasCleanPlates() && !myHolder.hasMovable())
                    {
                        myHolder.setMovable(washer.getCleanPlate().GetComponent<MovableObject>());
                        washer.updateCleanPosition();
                    }
                }

                Holder currentFurnitureHolder = currentFurniture.GetComponent<Holder>();

                if (myHolder.hasMovable() && !currentFurnitureHolder.hasMovable())
                {
                    MovableObject movable = myHolder.GetMovableObjet();
                    Trash trash = currentFurniture.GetComponent<Trash>();
                    musicElements.let.Play();
                    
                    if(currentFurniture.GetComponent<PlateTable>() == null)
                    {
                        if (trash != null)
                        {
                            Food food = movable.GetComponent<Food>();
                            if (food != null)
                            {
                                trash.destroyObject(movable);
                                myHolder.removeMovable();
                            }

                            Container container = movable.GetComponent<Container>();
                            if (container != null)
                            {
                                container.returnToEmpty();
                            }
                        }
                        else
                        {
                            Stove stove = currentFurniture.GetComponent<Stove>();
                            Container container = movable.GetComponent<Container>();
                            if (stove != null && container != null)
                            {
                                myHolder.removeMovable();
                                currentFurnitureHolder.setMovable(movable);
                            }
                            else if (stove == null)
                            {
                                OrganizingPlates organizingPlates = currentFurniture.GetComponent<OrganizingPlates>();
                                Plate plate = movable.GetComponent<Plate>();

                                if (organizingPlates != null)
                                {
                                    if (plate != null)
                                    {
                                        if (plate.plateContent == plateContent.TOMATO || plate.plateContent == plateContent.ONION || plate.plateContent == plateContent.MUSHROOM)
                                        {
                                            currentFurnitureHolder.setMovable(movable);
                                            plate.toEating();
                                            myHolder.removeMovable();

                                            Delivery delivery = currentFurniture.GetComponent<Delivery>();
                                            organizingPlates.plateTable.sumarPlato();
                                            delivery.receivePlate(plate.plateContent);
                                        }
                                    }
                                }
                                else
                                {
                                    washer = currentFurniture.GetComponent<Washer>();
                                    Plate plato = movable.GetComponent<Plate>();
                                    if (washer != null)
                                    {
                                        if (plato != null)
                                        {
                                            if (plato.plateContent == plateContent.WASTE)
                                            {
                                                myHolder.removeMovable();
                                                washer.setDirtyPlate(plato);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Chopper chop = currentFurniture.GetComponent<Chopper>();
                                        Food f = movable.GetComponent<Food>();

                                        if(chop != null)
                                        {
                                            if(f != null)
                                            {
                                                myHolder.removeMovable();
                                                currentFurnitureHolder.setMovable(movable);
                                            }
                                        }
                                        else
                                        {
                                            myHolder.removeMovable();
                                            currentFurnitureHolder.setMovable(movable);
                                        }
                                    }
                                    
                                }
                            }
                        }
                    }
                }
                else if (myHolder.hasMovable() && currentFurnitureHolder.hasMovable())
                {
                    MovableObject movable = myHolder.GetMovableObjet();
                    Container pot = movable.GetComponent<Container>();
                    Plate plate = currentFurnitureHolder.GetMovableObjet().GetComponent<Plate>();
                    
                    Container container = currentFurnitureHolder.GetComponentInChildren<Container>();
                    if (container != null)
                    {
                        if (container.verifyFood(movable.gameObject))
                        {
                            Food food = myHolder.GetComponentInChildren<Food>();
                            food.changeToCook();
                            container.addFood(food);
                            myHolder.removeMovable();

                            Cooking cooking = container.GetComponent<Cooking>();
                            cooking.setInitialTime();
                            if (!cooking.isActivated())
                            {
                                cooking.activateCanvas();
                            }
                        }
                    }
                    else
                    {
                        Stove stove = currentFurniture.GetComponent<Stove>();
                        if (stove != null && movable.GetComponent<MovableObject>().GetComponent<Container>() != null)
                        {
                            myHolder.removeMovable();
                            currentFurnitureHolder.setMovable(movable);
                        }
                        else
                        {
                            OrganizingPlates organizingPlates = currentFurniture.GetComponent<OrganizingPlates>();
                            Plate platee = movable.GetComponent<Plate>();

                            if (organizingPlates != null)
                            {
                                if (platee != null)
                                {
                                    if (platee.plateContent == plateContent.TOMATO || platee.plateContent == plateContent.ONION || platee.plateContent == plateContent.MUSHROOM)
                                    {
                                        currentFurnitureHolder.setMovable(movable);
                                        platee.toEating();
                                        myHolder.removeMovable();

                                        Delivery delivery = currentFurniture.GetComponent<Delivery>();
                                        organizingPlates.plateTable.sumarPlato();
                                        delivery.receivePlate(plate.plateContent);
                                    }
                                }
                            }
                            else
                            {
                                washer = currentFurniture.GetComponent<Washer>();
                                Plate plato = movable.GetComponent<Plate>();
                                if (washer != null && plato != null)
                                {
                                    if (plato.plateContent == plateContent.WASTE)
                                    {
                                        myHolder.removeMovable();
                                        washer.setDirtyPlate(plato);
                                    }
                                }
                            }
                        }
                    }

                    //--------------------------------------------------------

                    if (pot != null && plate != null)
                    {
                        if (pot.canGiveToPlate() && plate.plateContent == plateContent.NONE)
                        {
                            Cooking cooking = pot.GetComponent<Cooking>();
                            cooking.canvas.enabled = false;
                            plate.receiveContent(cooking.iconCanvas);
                            plate.setPlateContent(pot.getPotContent());
                            pot.returnToEmpty();
                            musicElements.beep.Stop();
                        }
                    }
                    else
                    {
                        Container pot2 = currentFurnitureHolder.GetMovableObjet().GetComponent<Container>();
                        Plate plate2 = movable.GetComponent<Plate>();
                        if (pot2 != null && plate2 != null)
                        {
                            if (pot2.canGiveToPlate() && plate2.plateContent == plateContent.NONE)
                            {
                                Cooking cooking = pot2.GetComponent<Cooking>();
                                cooking.canvas.enabled = false;
                                plate2.receiveContent(cooking.iconCanvas);
                                plate2.setPlateContent(pot2.getPotContent());
                                pot2.returnToEmpty();
                                musicElements.beep.Stop();
                            }
                        }
                    }
                }
                else if (currentFurnitureHolder.hasMovable() && !myHolder.hasMovable())
                {
                    musicElements.grab.Play();

                    MovableObject movable = currentFurnitureHolder.GetMovableObjet();
                    Chopper chopper = currentFurniture.GetComponent<Chopper>();

                    if (chopper != null)
                    {
                        myHolder.setMovable(movable);
                        currentFurnitureHolder.removeMovable();

                        Timing timing = currentFurniture.GetComponent<Timing>();
                        timing.desactivateCanvas();
                        anim.SetBool("chopping", false);
                        chopper.stopChopping();
                    }
                    else
                    {
                        Container container = movable.GetComponent<Container>();

                        if (container != null)
                        {
                            Cooking cooking = container.GetComponent<Cooking>();
                            cooking.imageStatus.sprite = cooking.statusCanvas.none;
                            container.smoke.Stop();
                            myHolder.setMovable(movable);
                            currentFurnitureHolder.removeMovable();
                        }
                        else
                        {
                            PlateTable plateTable = currentFurniture.GetComponent<PlateTable>();
                            if(plateTable != null)
                            {
                                myHolder.setMovable(movable);
                                plateTable.restarPlato();
                                currentFurnitureHolder.removeMovable();
                                anim.SetBool("washing", false);
                            }
                            else
                            {
                                Delivery deliver = currentFurniture.GetComponent<Delivery>();
                                if(deliver == null)
                                {
                                    myHolder.setMovable(movable);
                                    currentFurnitureHolder.removeMovable();
                                }
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(CutAndWash) && !myHolder.hasMovable())
        {
            Furniture currentFurniture = furnitureDetection.getSelected();

            if (currentFurniture != null)
            {
                washer = currentFurniture.GetComponent<Washer>();
                if (washer != null)
                {
                    if (!myHolder.hasMovable() && washer.hasDirtyPlates())
                    {
                        washer.allowToWash = true;
                        washer.washing = true;
                        anim.SetBool("washing", true);
                    }
                }
                else
                {
                    Holder furnitureHolder = currentFurniture.GetComponent<Holder>();
                    if (furnitureHolder != null)
                    {
                        MovableAnchor movableA = furnitureHolder.GetComponentInChildren<MovableAnchor>();
                        if (movableA != null)
                        {
                            Food food = movableA.GetComponentInChildren<Food>();
                            if (food != null && (food.getStatus() == FoodStatus.RAW || food.getStatus() == FoodStatus.CUT))
                            {
                                currentChopper = currentFurniture.GetComponent<Chopper>();
                                if (currentChopper != null)
                                {
                                    timing = currentFurniture.GetComponent<Timing>();
                                    if (timing != null)
                                    {
                                        timing.activateCanvas();
                                    }
                                    myKnife.gameObject.SetActive(true);
                                    isChopping = currentChopper.startChopping();
                                    //musicElements.chop.Play();
                                    anim.SetBool("chopping", isChopping);
                                }
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(createFood))
        {
            Furniture currentFurniture = furnitureDetection.getSelected();

            if (currentFurniture != null)
            {
                Creator creator = currentFurniture.GetComponent<Creator>();
                Holder furnitureHolder = currentFurniture.GetComponent<Holder>();
                if (creator != null && myHolder.movableAnchor.transform.childCount == 0 && furnitureHolder.movableAnchor.transform.childCount == 0)
                {
                    MovableObject movable = creator.createFood();
                    myHolder.setMovable(movable);
                }
            }
        }

        if (Input.GetKeyDown(keyBoost) && !boost)
        {
            boost = true;
            speed = 0.25f;
        }

        if (boost)
        {
            if (timeBoost >= 0)
            {
                timeBoost -= Time.deltaTime;
                speed = speed + (-0.15f) * Time.deltaTime;
            }
            else
            {
                boost = false;
                speed = 0.1f;
                timeBoost = 1f;
            }
        }
    }

    private void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis(horizontal);
        float moveVertical = Input.GetAxis(vertical);

        rotation = new Vector3(moveHorizontal, 0, moveVertical);

        if(rotation != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rotation);
        }

        moveDirection = new Vector3(moveHorizontal, 0, moveVertical) * speed;

        /*
        if(furnitureDetection.getSelected() != null)
        {
            if(furnitureDetection.getSelected().GetComponent<Chopper>() == null)
            {
                if(currentChopper != null)
                {
                    //Debug.Log("No hay chopper");
                    currentChopper.stopChopping();
                    currentChopper = null;
                }
            }
        }
        else
        {
            if (currentChopper != null)
            {
                //Debug.Log("No hay mueble");
                currentChopper.stopChopping();
            }
        }*/

        if (currentChopper != null)
        {
            if (currentChopper.isChopping == false)
            {
                myKnife.gameObject.SetActive(false);
                anim.SetBool("chopping", false);
                currentChopper.stopChopping();
            }
        }

        if (washer != null)
        {
            if(washer.washing == false)
            {
                anim.SetBool("washing", false);
            }
        }

        if(moveDirection != Vector3.zero)
        {
            characterController.Move(moveDirection);
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }

        if (myHolder.hasMovable())
        {
            anim.SetBool("hold", true);
            if(moveDirection != Vector3.zero)
            {
                myHolder.movableAnchor.transform.localPosition = new Vector3(0f, 0.55f, 0.5f);
            }
            else
            {
                myHolder.movableAnchor.transform.localPosition = new Vector3(0f, 0.55f, 0.4f);
            }
        }
        else
        {
            anim.SetBool("hold", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Chopper chop = other.GetComponent<Chopper>();
        if (chop != null)
        {
            chop.stopChopping();
        }
    }
}
