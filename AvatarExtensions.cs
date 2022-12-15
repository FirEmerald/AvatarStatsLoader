using SLZ.VRMK;
using BoneLib;

namespace AvatarStatsLoader
{
    public static class AvatarExtensions
    {
        private static float defAgility = 0, defStrengthUpper = 0, defStrengthLower = 0, defVitality = 0, defSpeed = 0, defIntelligence = 0;
        private static float loadAgility = 0, loadStrengthUpper = 0, loadStrengthLower = 0, loadVitality = 0, loadSpeed = 0, loadIntelligence = 0;
        private static float defMassChest = 0, defMassPelvis = 0, defMassHead = 0, defMassArm = 0, defMassLeg = 0;
        private static float loadMassChest = 0, loadMassPelvis = 0, loadMassHead = 0, loadMassArm = 0, loadMassLeg = 0;

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

        public static void setLoadStats(this Avatar avatar)
        {
            loadAgility = avatar._agility;
            loadStrengthUpper = avatar._strengthUpper;
            loadStrengthLower = avatar._strengthLower;
            loadVitality = avatar._vitality;
            loadSpeed = avatar._speed;
            loadIntelligence = avatar._intelligence;
        }

        public static float getLoadAgility(this Avatar avatar)
        {
            return loadAgility;
        }

        public static float getLoadStrengthUpper(this Avatar avatar)
        {
            return loadStrengthUpper;
        }

        public static float getLoadStrengthLower(this Avatar avatar)
        {
            return loadStrengthLower;
        }

        public static float getLoadVitality(this Avatar avatar)
        {
            return loadVitality;
        }

        public static float getLoadSpeed(this Avatar avatar)
        {
            return loadSpeed;
        }

        public static float getLoadIntelligence(this Avatar avatar)
        {
            return loadIntelligence;
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

        public static void setLoadMasses(this Avatar avatar)
        {
            loadMassChest = avatar._massChest;
            loadMassPelvis = avatar._massPelvis;
            loadMassHead = avatar._massHead;
            loadMassArm = avatar._massArm;
            loadMassLeg = avatar._massLeg;
        }

        public static float getLoadMassChest(this Avatar avatar)
        {
            return loadMassChest;
        }

        public static float getLoadMassPelvis(this Avatar avatar)
        {
            return loadMassPelvis;
        }

        public static float getLoadMassHead(this Avatar avatar)
        {
            return loadMassHead;
        }

        public static float getLoadMassArm(this Avatar avatar)
        {
            return loadMassArm;
        }

        public static float getLoadMassLeg(this Avatar avatar)
        {
            return loadMassLeg;
        }

        public static bool isEmptyRig(this Avatar avatar)
        {
            return avatar.name == "[RealHeptaRig (Marrow1)]";
        }

        public static string getName(this Avatar avatar)
        {
            if (avatar.name.EndsWith("(Clone)")) //remove "(Clone)" from mod avatars
                return avatar.name.Substring(0, avatar.name.Length - "(Clone)".Length);
            else
                return avatar.name;

        }
        public static void recalculateTotalMass(this Avatar avatar) => avatar._massTotal = (avatar._massChest + avatar._massPelvis + avatar._massHead + ((avatar._massArm + avatar._massLeg) * 2));
    }
}
