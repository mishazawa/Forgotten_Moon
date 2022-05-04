using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {
    public const int FORWARD = 1;
    public const int BACKWARD = -1;
    public const int RIGHT = 1;
    public const int LEFT = -1;

    public const int DECELERATION = 2;

    public const float ACCELERATION = 10f;
    public const float ANGULAR_SPEED = 15f;
    public const float MAX_SPEED = 50f;
    public const float MIN_SPEED = 0f;
    public const float LERP_SPEED = 100f;

    // Houdini Attribute store

    public const string COLLISION_TYPE_ATTR = "collider_type";
    public const string RESET_VECTOR_ATTR   = "reset_vector";

    public enum Collision {
        OBSTACLE,
        CHECKPOINT,
    };

    /* TBD */
    public const string CONSTRAINTS_ATTR = "pieces";
    public const string NAME_ATTR        = "name";
    public const string BREAK_FORCE_ATTR = "break_force";
    public const string MASS_ATTR        = "mass";

}
