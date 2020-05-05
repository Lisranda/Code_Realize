using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Player : StateMachine {
    public SM_Player (Controller controller) : base (controller) {
        this.controller = controller;
    }
}
