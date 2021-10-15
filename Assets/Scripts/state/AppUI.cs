using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MenuUI 
{
    public void Refresh();
    public void deactivateAllItems();
    public void activateAllItems();
    public void setDesc();
}
