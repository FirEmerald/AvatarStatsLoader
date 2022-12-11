﻿using SLZ.VRMK;

namespace AvatarStatsLoader
{
    public static class AvatarExtensions
    {
        private static float defAgility = 0, defStrengthUpper = 0, defStrengthLower = 0, defVitality = 0, defSpeed = 0, defIntelligence = 0;
        private static float defMassChest = 0, defMassPelvis = 0, defMassHead = 0, defMassArm = 0, defMassLeg = 0;

        public static void setDefStats(this Avatar avatar)
        {
            defAgility = avatar._agility;
            defStrengthUpper = avatar._strengthUpper;
            defStrengthLower = avatar._strengthLower;
            defVitality = avatar._vitality;
            defSpeed = avatar._speed;
            defIntelligence = avatar._intelligence;
        }

        public static float getDefAgility(this Avatar avatar)
        {
            return defAgility;
        }

        public static float getDefStrengthUpper(this Avatar avatar)
        {
            return defStrengthUpper;
        }

        public static float getDefStrengthLower(this Avatar avatar)
        {
            return defStrengthLower;
        }

        public static float getDefVitality(this Avatar avatar)
        {
            return defVitality;
        }

        public static float getDefSpeed(this Avatar avatar)
        {
            return defSpeed;
        }

        public static float getDefIntelligence(this Avatar avatar)
        {
            return defIntelligence;
        }

        public static void setDefMasses(this Avatar avatar)
        {
            defMassChest = avatar._massChest;
            defMassPelvis = avatar._massPelvis;
            defMassHead = avatar._massHead;
            defMassArm = avatar._massArm;
            defMassLeg = avatar._massLeg;
        }

        public static float getDefMassChest(this Avatar avatar)
        {
            return defMassChest;
        }

        public static float getDefMassPelvis(this Avatar avatar)
        {
            return defMassPelvis;
        }

        public static float getDefMassHead(this Avatar avatar)
        {
            return defMassHead;
        }

        public static float getDefMassArm(this Avatar avatar)
        {
            return defMassArm;
        }

        public static float getDefMassLeg(this Avatar avatar)
        {
            return defMassLeg;
        }
    }
}