using Il2CppSLZ.VRMK;

namespace AvatarStatsLoader
{
    public static class AvatarExtensions
    {
        private static float defAgility = 0, defStrengthUpper = 0, defStrengthLower = 0, defVitality = 0, defSpeed = 0, defIntelligence = 0;
        private static float loadAgility = 0, loadStrengthUpper = 0, loadStrengthLower = 0, loadVitality = 0, loadSpeed = 0, loadIntelligence = 0;
        private static float defMassChest = 0, defMassPelvis = 0, defMassHead = 0, defMassArm = 0, defMassLeg = 0;
        private static float loadMassChest = 0, loadMassPelvis = 0, loadMassHead = 0, loadMassArm = 0, loadMassLeg = 0;

        public static void SetDefStats(this Avatar avatar)
        {
            defAgility = avatar._agility;
            defStrengthUpper = avatar._strengthUpper;
            defStrengthLower = avatar._strengthLower;
            defVitality = avatar._vitality;
            defSpeed = avatar._speed;
            defIntelligence = avatar._intelligence;
        }

        public static float GetDefAgility(this Avatar avatar)
        {
            return defAgility;
        }

        public static float GetDefStrengthUpper(this Avatar avatar)
        {
            return defStrengthUpper;
        }

        public static float GetDefStrengthLower(this Avatar avatar)
        {
            return defStrengthLower;
        }

        public static float GetDefVitality(this Avatar avatar)
        {
            return defVitality;
        }

        public static float GetDefSpeed(this Avatar avatar)
        {
            return defSpeed;
        }

        public static float GetDefIntelligence(this Avatar avatar)
        {
            return defIntelligence;
        }

        public static void SetLoadStats(this Avatar avatar)
        {
            loadAgility = avatar._agility;
            loadStrengthUpper = avatar._strengthUpper;
            loadStrengthLower = avatar._strengthLower;
            loadVitality = avatar._vitality;
            loadSpeed = avatar._speed;
            loadIntelligence = avatar._intelligence;
        }

        public static float GetLoadAgility(this Avatar avatar)
        {
            return loadAgility;
        }

        public static float GetLoadStrengthUpper(this Avatar avatar)
        {
            return loadStrengthUpper;
        }

        public static float GetLoadStrengthLower(this Avatar avatar)
        {
            return loadStrengthLower;
        }

        public static float GetLoadVitality(this Avatar avatar)
        {
            return loadVitality;
        }

        public static float GetLoadSpeed(this Avatar avatar)
        {
            return loadSpeed;
        }

        public static float GetLoadIntelligence(this Avatar avatar)
        {
            return loadIntelligence;
        }

        public static void SetDefMasses(this Avatar avatar)
        {
            defMassChest = avatar._massChest;
            defMassPelvis = avatar._massPelvis;
            defMassHead = avatar._massHead;
            defMassArm = avatar._massArm;
            defMassLeg = avatar._massLeg;
        }

        public static float GetDefMassChest(this Avatar avatar)
        {
            return defMassChest;
        }

        public static float GetDefMassPelvis(this Avatar avatar)
        {
            return defMassPelvis;
        }

        public static float GetDefMassHead(this Avatar avatar)
        {
            return defMassHead;
        }

        public static float GetDefMassArm(this Avatar avatar)
        {
            return defMassArm;
        }

        public static float GetDefMassLeg(this Avatar avatar)
        {
            return defMassLeg;
        }

        public static void SetLoadMasses(this Avatar avatar)
        {
            loadMassChest = avatar._massChest;
            loadMassPelvis = avatar._massPelvis;
            loadMassHead = avatar._massHead;
            loadMassArm = avatar._massArm;
            loadMassLeg = avatar._massLeg;
        }

        public static float GetLoadMassChest(this Avatar avatar)
        {
            return loadMassChest;
        }

        public static float GetLoadMassPelvis(this Avatar avatar)
        {
            return loadMassPelvis;
        }

        public static float GetLoadMassHead(this Avatar avatar)
        {
            return loadMassHead;
        }

        public static float GetLoadMassArm(this Avatar avatar)
        {
            return loadMassArm;
        }

        public static float GetLoadMassLeg(this Avatar avatar)
        {
            return loadMassLeg;
        }

        public static bool IsEmptyRig(this Avatar avatar)
        {
            return avatar.name == "[RealHeptaRig (Marrow1)]";
        }

        public static string GetName(this Avatar avatar)
        {
            if (avatar.name.EndsWith("(Clone)")) //remove "(Clone)" from mod avatars
                return avatar.name[..^"(Clone)".Length];
            else
                return avatar.name;

        }
        public static void RecalculateTotalMass(this Avatar avatar) => avatar._massTotal = (avatar._massChest + avatar._massPelvis + avatar._massHead + ((avatar._massArm + avatar._massLeg) * 2));
    }
}
