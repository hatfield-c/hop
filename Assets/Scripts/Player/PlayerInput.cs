using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    protected int horizontal = 0;
    protected int vertical = 0;

    public int GetHorizontal() {
        return this.horizontal;
    }

    public int GetVertical() {
        return this.vertical;
    }

    public void SetHorizontal(int horizontal) {
        this.horizontal = horizontal;
    }

    public void SetVertical(int vertical) {
        this.vertical = vertical;
    }

    public void Clear() {
        this.horizontal = 0;
        this.vertical = 0;
    }
}
