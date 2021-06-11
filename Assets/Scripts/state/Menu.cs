using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using state;
public enum MenuType
{
    MAIN,
    INVO,
    ITEM_SELECT,
    INSPECT,
};
public enum MenuState
{
    IDLE,
    SCROLLING,
    EXIT,
    MOVE,
};
public class Menu : MonoBehaviour, PlayerState
{
    public MenuTransition transition;
    public GameObject canvas_objct;
    public GameObject menu_panel;
    public GameObject main_menu;
    public GameObject Invo_menu;
    public GameObject DescInspect;
    public GameObject popUp;
    public InventoryUI inventory_UI;
    private MenuType scene;
    public MenuState state;
    public bool onExit;
    public void handleInput(Player player) {
        if (Input.GetKeyDown(KeyCode.X))
        {
            state = MenuState.EXIT;
        }
    }
    public void UpdateState(Player player)
    {
        switch (state)
        {
            case MenuState.EXIT:
                goBack(player);
                break;
            default:
                break;
        }
    }
    public void OnExit(Player player) {
        transition.exitMenu();
        player.act = ACT.IDLE;
        player.UpdateAct();
    }
    public void goBack(Player player)
    {
        switch (scene)
        {
            case MenuType.MAIN:
                OnExit(player);
                break;
            case MenuType.INVO:
                Invo_menu.SetActive(false);
                main_menu.SetActive(true);
                isAtMainMenu();
                break;
            case MenuType.ITEM_SELECT:
                inventory_UI.activateAllItems();
                isAtInventory();
                popUp.SetActive(false);
                break;
            case MenuType.INSPECT:
                popUp.SetActive(true);
                DescInspect.SetActive(false);
                isAtItemSelected();
                break;
        }
        state = MenuState.IDLE;
    }
    public void isAtMainMenu()
    {
        scene = MenuType.MAIN;
    }
    public void isAtInventory()
    {
        scene = MenuType.INVO;
    }
    public void isAtInspection()
    {
        scene = MenuType.INSPECT;
    }
    public void isAtItemSelected()
    {
        scene = MenuType.ITEM_SELECT;
    }
}
