namespace MinLight.Entities
{
    using System;

    [Flags]
    public enum BsdfEvents
    {
        kNONE        = 0,
        kDiffuse     = 1,
        kPhong       = 2,
        kReflect     = 4,
        kRefract     = 8,
        kSpecular    = (kReflect  | kRefract),
        kNonSpecular = (kDiffuse  | kPhong),
        kAll         = (kSpecular | kNonSpecular)
    };
}