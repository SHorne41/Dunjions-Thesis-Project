using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteData : MonoBehaviour {

    string fileName;
    int stage;
    int trial;

    public string deathProgress;
    public string checkPointProgress;

    public damageStats[] damageReceived;
    damageStats[] damageGiven;

    StatsManager statsManagerRef;
    uiSwitchScript uiRef;
    GameMaster gameMasterRef;

    void Start ()
    {
        statsManagerRef = FindObjectOfType<StatsManager>();
        uiRef = FindObjectOfType<uiSwitchScript>();
        gameMasterRef = FindObjectOfType<GameMaster>();

        //Variables used to know which run the player is on
        stage = GameMaster.gameMaster.stage;      //Used when AI is active
        trial = GameMaster.gameMaster.trial;

        if (stage == 0 && trial == 0)
        {
            fileName = System.DateTime.Now + ".txt";
            fileName = fileName.Replace(" ", "  ");
            fileName = fileName.Replace("/", "-");
            fileName = fileName.Replace(":", "-");
            GameMaster.gameMaster.fileName = fileName;
        }
        else
        {
            fileName = GameMaster.gameMaster.fileName;
        }
        
        damageGiven = new damageStats[5];
        damageReceived = new damageStats[5];
        string type = "Unknown";

        deathProgress = "";
        checkPointProgress = "";

        


        for (int i = 0; i < damageReceived.Length; i++)
        {
            if (i == 0)
            {
                type = "skeleton";
            }
            else if (i == 1)
            {
                type = "red";
            }
            else if (i == 2)
            {
                type = "green";
            }
            else if (i == 3)
            {
                type = "spider";
            }

            damageGiven[i] = new damageStats();
            damageGiven[i].type = type;
            damageReceived[i] = new damageStats();
            damageReceived[i].type = type;
        }

        if (stage == 0 && trial == 0)
        {
            writeData(false);
        }
        
    }

    public string spacer(string stringIn, int requiredLength)
    {
        if (stringIn.Length > 5)
        {
            stringIn = stringIn.Substring(0, stringIn.Length - (stringIn.Length - 5));
        }
        
        int stringInLength = stringIn.Length;

        string good = "";
        int start = ((requiredLength / 2) - stringInLength) + 1;

        for (int i = 0; i < requiredLength; i++)
        {
            if (i >= start && i < start + stringInLength)
            {
                string x = stringIn.Substring(i - start, 1);
                good = string.Concat(good, x);
            }
            else
            {
                good = string.Concat(good, " ");
            }
        }
        return good;
    }

    public void resetData()
    {
        for (int i = 0; i < 4; i++)
        {
            damageGiven[i].lastTotalDamage = damageGiven[i].totalDamage;
            damageGiven[i].totalDamage = 0;

            damageGiven[i].lastKilled = damageGiven[i].killed;
            damageGiven[i].killed = 0;

            damageGiven[i].lastQuanitityDamaged = damageGiven[i].quanitityDamaged;
            damageGiven[i].quanitityDamaged = 0;

            damageReceived[i].damage = 0;
        }
    }

    public void addStatReceived(string gameObjectType, float damage, int ID)
    {
        int type = 4;
        if (gameObjectType == "skeleton")
        {
            type = 0;
        }
        else if (gameObjectType == "red")
        {
            type = 1;
        }
        else if (gameObjectType == "green")
        {
            type = 2;
        }
        else if (gameObjectType == "spider")
        {
            type = 3;
        }
        else if (gameObjectType == "health")
        {
            type = 4;
        }
        
        damageReceived[type].damage += damage;
        damageReceived[type].damageOverall += damage;

    }

    public void addStatGiven(string gameObjectType, float damage, bool killed)
    {
        int type = 4;
        if (gameObjectType == "skeleton")
        {
            type = 0;
        }
        else if (gameObjectType == "red")
        {
            type = 1;
        }
        else if (gameObjectType == "green")
        {
            type = 2;
        }
        else if (gameObjectType == "spider")
        {
            type = 3;
        }
        
        if (killed)
        {
            damageGiven[type].killed++;
            damageGiven[type].killedAccumulatedAfterDeath++;
        }

        else if (damageGiven[type] != null)
        {
            damageGiven[type].damage = -damage;

            damageGiven[type].totalDamage -= damage;
            damageGiven[type].totalDamageAccumulatedAfterDeath -= damage;
            damageGiven[type].quanitityDamaged++;
            damageGiven[type].quanitityDamagedAccumulatedAfterDeath++;
        }

    }

    public void writeData(bool death)
    {
        string state2;
        string state;
        if (death)
        {
            state = "last life";
            state2 = "DEATH";
        }
        else
        {
            state = "last stage";
            state2 = "CHECKPOINT";
        }

        System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
        System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");

        //The following lines represent what is written to the file if the AI is active
        if (uiRef.uiSwitch.value == 0)
        {
            if (stage == 0)
            {

                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== ===========");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ===========            *           *            *            *             *                *                *               *            *            *     ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ===========                                THIS IS THE BEGINNING OF A NEW TEST ------------ " + System.DateTime.Now + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ===========        *         *           *             *            *              *                 *                *             *         * ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== ===========");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "    ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " -----------------------------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "| STARTING statistics");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " -----------------------------------------------");

                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            |  HEALTH  |  SPEED  |  ATTACK  |  ATTACKSPEED  |  ATTACKRANGE  |  WEBFREQUENCY  |  FIREBALL SPEED |    ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            |----------|---------|----------|---------------|---------------|----------------|-----------------| ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Skeleton  |" + spacer(statsManagerRef.getSkeletonHealth().ToString(), 10) + "|" + spacer(statsManagerRef.getSkeletonSpeed().ToString(), 9) + "|" + spacer(statsManagerRef.getSkeletonAttackPower().ToString(), 10) + "|" + spacer(statsManagerRef.getSkeletonAttackSpeed().ToString(), 15) + "|" + spacer(statsManagerRef.getSkeletonAttackRange().ToString(), 15) + "|" + spacer("--", 16) + "|" + spacer("--", 17));
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  RedMons   |" + spacer(statsManagerRef.getRedMonsterHealth().ToString(), 10) + "|" + spacer(statsManagerRef.getRedMonsterSpeed().ToString(), 9) + "|" + spacer(statsManagerRef.getRedMonsterAttackPower().ToString(), 10) + "|" + spacer(statsManagerRef.getRedMonsterAttackSpeed().ToString(), 15) + "|" + spacer(statsManagerRef.getRedMonsterAttackRange().ToString(), 15) + "|" + spacer("--", 16) + "|" + spacer(statsManagerRef.getRedMonsterFireballSpeed().ToString(), 17));
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  GreenMons |" + spacer(statsManagerRef.getGreenMonsterHealth().ToString(), 10) + "|" + spacer(statsManagerRef.getGreenMonsterSpeed().ToString(), 9) + "|" + spacer(statsManagerRef.getGreenMonsterAttackPower().ToString(), 10) + "|" + spacer(statsManagerRef.getGreenMonsterAttackSpeed().ToString(), 15) + "|" + spacer("--", 15) + "|" + spacer("--", 16) + "|" + spacer(statsManagerRef.getGreenMonsterFireballSpeed().ToString(), 17));
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Spider    |" + spacer(statsManagerRef.getSpiderHealth().ToString(), 10) + "|" + spacer(statsManagerRef.getSpiderSpeed().ToString(), 9) + "|" + spacer(statsManagerRef.getSpiderAttackPower().ToString(), 10) + "|" + spacer("--", 15) + "|" + spacer("--", 15) + "|" + spacer(statsManagerRef.getSpiderAttackSpeed().ToString(), 16) + "|" + spacer("--", 17));

                trial++;
            }
            else
            {
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "    ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "    ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== ===========");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ===========                                CURRENT   AI   REPORT   COUNT: " + stage + "   REPORT TYPE:  " + state2 + "");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ===========                                                        Statistics              ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== ===========");

                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " -----------------------------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "| GIVEN damage over time period of " + state);
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " -----------------------------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine);
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            | ENEMIES      | ENEMIES      |  AVERAGE     |  TOTAL       | ENEMIES      | ENEMIES      | AVERAGE      | TOTAL        |");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            | HIT          | KILLED       |  DAMAGE      |  DAMAGE      | HIT(ALL)     | KILLED(ALL)  | DAMAGE(ALL)  | DAMAGE(ALL ) |");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            |--------------|--------------|--------------|--------------|--------------|--------------|--------------|--------------|    ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Skeleton  |" + spacer(damageGiven[0].quanitityDamaged.ToString(), 14) + "|" + spacer(damageGiven[0].killed.ToString(), 14) + "|" + spacer((damageGiven[0].totalDamage / damageGiven[0].quanitityDamaged).ToString(), 14) + "|" + spacer(damageGiven[0].totalDamage.ToString(), 14) + "|" + spacer(damageGiven[0].quanitityDamagedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer(damageGiven[0].killedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer((damageGiven[0].totalDamageAccumulatedAfterDeath / damageGiven[0].quanitityDamagedAccumulatedAfterDeath).ToString(), 14) + "|" + spacer(damageGiven[0].totalDamageAccumulatedAfterDeath.ToString(), 14) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  RedMons   |" + spacer(damageGiven[1].quanitityDamaged.ToString(), 14) + "|" + spacer(damageGiven[1].killed.ToString(), 14) + "|" + spacer((damageGiven[1].totalDamage / damageGiven[1].quanitityDamaged).ToString(), 14) + "|" + spacer(damageGiven[1].totalDamage.ToString(), 14) + "|" + spacer(damageGiven[1].quanitityDamagedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer(damageGiven[1].killedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer((damageGiven[1].totalDamageAccumulatedAfterDeath / damageGiven[1].quanitityDamagedAccumulatedAfterDeath).ToString(), 14) + "|" + spacer(damageGiven[1].totalDamageAccumulatedAfterDeath.ToString(), 14) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  GreenMons |" + spacer(damageGiven[2].quanitityDamaged.ToString(), 14) + "|" + spacer(damageGiven[2].killed.ToString(), 14) + "|" + spacer((damageGiven[2].totalDamage / damageGiven[2].quanitityDamaged).ToString(), 14) + "|" + spacer(damageGiven[2].totalDamage.ToString(), 14) + "|" + spacer(damageGiven[2].quanitityDamagedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer(damageGiven[2].killedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer((damageGiven[2].totalDamageAccumulatedAfterDeath / damageGiven[2].quanitityDamagedAccumulatedAfterDeath).ToString(), 14) + "|" + spacer(damageGiven[2].totalDamageAccumulatedAfterDeath.ToString(), 14) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Spider    |" + spacer(damageGiven[3].quanitityDamaged.ToString(), 14) + "|" + spacer(damageGiven[3].killed.ToString(), 14) + "|" + spacer((damageGiven[3].totalDamage / damageGiven[3].quanitityDamaged).ToString(), 14) + "|" + spacer(damageGiven[3].totalDamage.ToString(), 14) + "|" + spacer(damageGiven[3].quanitityDamagedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer(damageGiven[3].killedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer((damageGiven[3].totalDamageAccumulatedAfterDeath / damageGiven[3].quanitityDamagedAccumulatedAfterDeath).ToString(), 14) + "|" + spacer(damageGiven[3].totalDamageAccumulatedAfterDeath.ToString(), 14) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " --------------------------------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "| RECEIVED damage over time period of " + state);
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " --------------------------------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            | DAMAGE      | DAMAGE      | HEALTH     | ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            | THIS ROUND  | OVERALL     | RECEIVED   |");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Skeleton  |" + spacer(damageReceived[0].damage.ToString(), 13) + "|" + spacer(damageReceived[0].damageOverall.ToString(), 13) + "|" + spacer("--", 12) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  RedMons   |" + spacer(damageReceived[1].damage.ToString(), 13) + "|" + spacer(damageReceived[1].damageOverall.ToString(), 13) + "|" + spacer("--", 12) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  GreenMons |" + spacer(damageReceived[2].damage.ToString(), 13) + "|" + spacer(damageReceived[2].damageOverall.ToString(), 13) + "|" + spacer("--", 12) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Spider    |" + spacer(damageReceived[3].damage.ToString(), 13) + "|" + spacer(damageReceived[3].damageOverall.ToString(), 13) + "|" + spacer("--", 12) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  TOTAL     |" + spacer((damageReceived[3].damage + damageReceived[2].damage + damageReceived[1].damage + damageReceived[0].damage).ToString(), 13) + "|" + spacer((damageReceived[3].damageOverall + damageReceived[2].damageOverall + damageReceived[1].damageOverall + damageReceived[0].damageOverall).ToString(), 13) + "|" + spacer("--", 12) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Hearts    |" + spacer("--", 13) + "|" + spacer("--", 13) + "|" + spacer(damageReceived[4].damage.ToString(), 12) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");




                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ----------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "| PREVIOUS AI configuration ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ----------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            |  HEALTH  |  SPEED  |  ATTACK  |  ATTACKSPEED  |  ATTACKRANGE  |  WEBFREQUENCY  |  FIREBALL SPEED |    ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            |----------|---------|----------|---------------|---------------|----------------|-----------------| ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Skeleton  |" + spacer(GameMaster.gameMaster.skeletonHealthLast.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.skeletonSpeedLast.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.skeletonAttackLast.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.skeletonAttackSpeedLast.ToString(), 15) + "|" + spacer(GameMaster.gameMaster.skeletonAttackRangeLast.ToString(), 15) + "|" + spacer("--", 16) + "|" + spacer("--", 17) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  RedMons   |" + spacer(GameMaster.gameMaster.redMonsterHealthLast.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.redMonsterSpeedLast.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.redMonsterAttackPowerLast.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.redMonsterAttackSpeedLast.ToString(), 15) + "|" + spacer(GameMaster.gameMaster.redMonsterAttackRangeLast.ToString(), 15) + "|" + spacer("--", 16) + "|" + spacer(GameMaster.gameMaster.redMonsterFireballSpeedLast.ToString(), 17) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  GreenMons |" + spacer(GameMaster.gameMaster.greenHornsMonsterHealthLast.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.greenHornsMonsterSpeedLast.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.greenHornsMonsterAttackPowerLast.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.greenHornsMonsterAttackSpeedLast.ToString(), 15) + "|" + spacer("--", 15) + "|" + spacer("--", 16) + "|" + spacer(GameMaster.gameMaster.greenHornsMonsterFireballSpeedLast.ToString(), 17) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Spider    |" + spacer(GameMaster.gameMaster.spiderHealthLast.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.spiderSpeedLast.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.spiderWebDamageLast.ToString(), 10) + "|" + spacer("--", 15) + "|" + spacer("--", 15) + "|" + spacer(GameMaster.gameMaster.spiderWebFrequencyLast.ToString(), 16) + "|" + spacer("--", 17) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine);



                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ---------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "| MODIFICATIONS  ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ---------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "                   " + deathProgress);
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ----------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "| MODIFIED AI configuration ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ----------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            |  HEALTH  |  SPEED  |  ATTACK  |  ATTACKSPEED  |  ATTACKRANGE  |  WEBFREQUENCY  |  FIREBALL SPEED |    ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            |----------|---------|----------|---------------|---------------|----------------|-----------------| ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Skeleton  |" + spacer(GameMaster.gameMaster.skeletonHealthMultiplier.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.skeletonSpeedMultiplier.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.skeletonAttackPowerMultiplier.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.skeletonAttackSpeedMultiplier.ToString(), 15) + "|" + spacer(GameMaster.gameMaster.skeletonAttackRangeMultiplier.ToString(), 15) + "|" + spacer("--", 16) + "|" + spacer("--", 17));
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  RedMons   |" + spacer(GameMaster.gameMaster.redMonsterHealthMultiplier.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.redMonsterSpeedMultiplier.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.redMonsterAttackPowerMultiplier.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.redMonsterAttackSpeedMultiplier.ToString(), 15) + "|" + spacer(GameMaster.gameMaster.redMonsterAttackRangeMultiplier.ToString(), 15) + "|" + spacer("--", 16) + "|" + spacer(GameMaster.gameMaster.redMonsterFireballSpeedMultiplier.ToString(), 17));
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  GreenMons |" + spacer(GameMaster.gameMaster.greenMonsterHealthMultiplier.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.greenMonsterSpeedMultiplier.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.greenMonsterAttackPowerMultiplier.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.greenMonsterAttackSpeedMultiplier.ToString(), 15) + "|" + spacer("--", 15) + "|" + spacer("--", 16) + "|" + spacer(GameMaster.gameMaster.greenMonsterFireballSpeedMultiplier.ToString(), 17));
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Spider    |" + spacer(GameMaster.gameMaster.spiderHealthMultiplier.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.spiderSpeedMultiplier.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.spiderAttackPowerMultiplier.ToString(), 10) + "|" + spacer("--", 15) + "|" + spacer("--", 15) + "|" + spacer(GameMaster.gameMaster.spiderAttackSpeedMultiplier.ToString(), 16) + "|" + spacer("--", 17));
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");

            }

            stage++;
            GameMaster.gameMaster.stage = stage;
        }

        //The following represents what is written to the file if the UI is active
        else if (uiRef.uiSwitch.value == 1)
        {
            if (trial > 0)
            {
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "    ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "    ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== ===========");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ===========                                CURRENT   AI   REPORT   COUNT: " + trial + "   REPORT TYPE:  " + state2 + "");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ===========                                                        Statistics              ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== =========== ===========");

                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " -----------------------------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "| GIVEN damage over time period of " + state);
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " -----------------------------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine);
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            | ENEMIES      | ENEMIES      |  AVERAGE     |  TOTAL       | ENEMIES      | ENEMIES      | AVERAGE      | TOTAL        |");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            | HIT          | KILLED       |  DAMAGE      |  DAMAGE      | HIT(ALL)     | KILLED(ALL)  | DAMAGE(ALL)  | DAMAGE(ALL ) |");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            |--------------|--------------|--------------|--------------|--------------|--------------|--------------|--------------|    ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Skeleton  |" + spacer(damageGiven[0].quanitityDamaged.ToString(), 14) + "|" + spacer(damageGiven[0].killed.ToString(), 14) + "|" + spacer((damageGiven[0].totalDamage / damageGiven[0].quanitityDamaged).ToString(), 14) + "|" + spacer(damageGiven[0].totalDamage.ToString(), 14) + "|" + spacer(damageGiven[0].quanitityDamagedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer(damageGiven[0].killedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer((damageGiven[0].totalDamageAccumulatedAfterDeath / damageGiven[0].quanitityDamagedAccumulatedAfterDeath).ToString(), 14) + "|" + spacer(damageGiven[0].totalDamageAccumulatedAfterDeath.ToString(), 14) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  RedMons   |" + spacer(damageGiven[1].quanitityDamaged.ToString(), 14) + "|" + spacer(damageGiven[1].killed.ToString(), 14) + "|" + spacer((damageGiven[1].totalDamage / damageGiven[1].quanitityDamaged).ToString(), 14) + "|" + spacer(damageGiven[1].totalDamage.ToString(), 14) + "|" + spacer(damageGiven[1].quanitityDamagedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer(damageGiven[1].killedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer((damageGiven[1].totalDamageAccumulatedAfterDeath / damageGiven[1].quanitityDamagedAccumulatedAfterDeath).ToString(), 14) + "|" + spacer(damageGiven[1].totalDamageAccumulatedAfterDeath.ToString(), 14) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  GreenMons |" + spacer(damageGiven[2].quanitityDamaged.ToString(), 14) + "|" + spacer(damageGiven[2].killed.ToString(), 14) + "|" + spacer((damageGiven[2].totalDamage / damageGiven[2].quanitityDamaged).ToString(), 14) + "|" + spacer(damageGiven[2].totalDamage.ToString(), 14) + "|" + spacer(damageGiven[2].quanitityDamagedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer(damageGiven[2].killedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer((damageGiven[2].totalDamageAccumulatedAfterDeath / damageGiven[2].quanitityDamagedAccumulatedAfterDeath).ToString(), 14) + "|" + spacer(damageGiven[2].totalDamageAccumulatedAfterDeath.ToString(), 14) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Spider    |" + spacer(damageGiven[3].quanitityDamaged.ToString(), 14) + "|" + spacer(damageGiven[3].killed.ToString(), 14) + "|" + spacer((damageGiven[3].totalDamage / damageGiven[3].quanitityDamaged).ToString(), 14) + "|" + spacer(damageGiven[3].totalDamage.ToString(), 14) + "|" + spacer(damageGiven[3].quanitityDamagedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer(damageGiven[3].killedAccumulatedAfterDeath.ToString(), 14) + "|" + spacer((damageGiven[3].totalDamageAccumulatedAfterDeath / damageGiven[3].quanitityDamagedAccumulatedAfterDeath).ToString(), 14) + "|" + spacer(damageGiven[3].totalDamageAccumulatedAfterDeath.ToString(), 14) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " --------------------------------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "| RECEIVED damage over time period of " + state);
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " --------------------------------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            | DAMAGE      | DAMAGE      | HEALTH     | ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            | THIS ROUND  | OVERALL     | RECEIVED   |");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Skeleton  |" + spacer(damageReceived[0].damage.ToString(), 13) + "|" + spacer(damageReceived[0].damageOverall.ToString(), 13) + "|" + spacer("--", 12) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  RedMons   |" + spacer(damageReceived[1].damage.ToString(), 13) + "|" + spacer(damageReceived[1].damageOverall.ToString(), 13) + "|" + spacer("--", 12) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  GreenMons |" + spacer(damageReceived[2].damage.ToString(), 13) + "|" + spacer(damageReceived[2].damageOverall.ToString(), 13) + "|" + spacer("--", 12) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Spider    |" + spacer(damageReceived[3].damage.ToString(), 13) + "|" + spacer(damageReceived[3].damageOverall.ToString(), 13) + "|" + spacer("--", 12) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  TOTAL     |" + spacer((damageReceived[3].damage + damageReceived[2].damage + damageReceived[1].damage + damageReceived[0].damage).ToString(), 13) + "|" + spacer((damageReceived[3].damageOverall + damageReceived[2].damageOverall + damageReceived[1].damageOverall + damageReceived[0].damageOverall).ToString(), 13) + "|" + spacer("--", 12) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Hearts    |" + spacer("--", 13) + "|" + spacer("--", 13) + "|" + spacer(damageReceived[4].damage.ToString(), 12) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");




                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ----------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "| PREVIOUS WIZARD configuration ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ----------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            |  HEALTH  |  SPEED  |  ATTACK  |  ATTACKSPEED  |  ATTACKRANGE  |  WEBFREQUENCY  |  FIREBALL SPEED |    ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            |----------|---------|----------|---------------|---------------|----------------|-----------------| ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Skeleton  |" + spacer(GameMaster.gameMaster.skeletonHealthLast.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.skeletonSpeedLast.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.skeletonAttackLast.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.skeletonAttackSpeedLast.ToString(), 15) + "|" + spacer(GameMaster.gameMaster.skeletonAttackRangeLast.ToString(), 15) + "|" + spacer("--", 16) + "|" + spacer("--", 17) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  RedMons   |" + spacer(GameMaster.gameMaster.redMonsterHealthLast.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.redMonsterSpeedLast.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.redMonsterAttackPowerLast.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.redMonsterAttackSpeedLast.ToString(), 15) + "|" + spacer(GameMaster.gameMaster.redMonsterAttackRangeLast.ToString(), 15) + "|" + spacer("--", 16) + "|" + spacer(GameMaster.gameMaster.redMonsterFireballSpeedLast.ToString(), 17) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  GreenMons |" + spacer(GameMaster.gameMaster.greenHornsMonsterHealthLast.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.greenHornsMonsterSpeedLast.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.greenHornsMonsterAttackPowerLast.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.greenHornsMonsterAttackSpeedLast.ToString(), 15) + "|" + spacer("--", 15) + "|" + spacer("--", 16) + "|" + spacer(GameMaster.gameMaster.greenHornsMonsterFireballSpeedLast.ToString(), 17) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Spider    |" + spacer(GameMaster.gameMaster.spiderHealthLast.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.spiderSpeedLast.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.spiderWebDamageLast.ToString(), 10) + "|" + spacer("--", 15) + "|" + spacer("--", 15) + "|" + spacer(GameMaster.gameMaster.spiderWebFrequencyLast.ToString(), 16) + "|" + spacer("--", 17) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine);


                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ---------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "| MODIFICATIONS  ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ---------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ----------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "| MODIFIED WIZARD configuration ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ----------------------------");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            |  HEALTH  |  SPEED  |  ATTACK  |  ATTACKSPEED  |  ATTACKRANGE  |  WEBFREQUENCY  |  FIREBALL SPEED |    ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "            |----------|---------|----------|---------------|---------------|----------------|-----------------| ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Skeleton  |" + spacer(GameMaster.gameMaster.skeletonHealthMultiplier.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.skeletonSpeedMultiplier.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.skeletonAttackPowerMultiplier.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.skeletonAttackSpeedMultiplier.ToString(), 15) + "|" + spacer(GameMaster.gameMaster.skeletonAttackRangeMultiplier.ToString(), 15) + "|" + spacer("--", 16) + "|" + spacer("--", 17) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  RedMons   |" + spacer(GameMaster.gameMaster.redMonsterHealthMultiplier.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.redMonsterSpeedMultiplier.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.redMonsterAttackPowerMultiplier.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.redMonsterAttackSpeedMultiplier.ToString(), 15) + "|" + spacer(GameMaster.gameMaster.redMonsterAttackRangeMultiplier.ToString(), 15) + "|" + spacer("--", 16) + "|" + spacer(GameMaster.gameMaster.redMonsterFireballSpeedMultiplier.ToString(), 17) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  GreenMons |" + spacer(GameMaster.gameMaster.greenMonsterHealthMultiplier.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.greenMonsterSpeedMultiplier.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.greenMonsterAttackPowerMultiplier.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.greenMonsterAttackSpeedMultiplier.ToString(), 15) + "|" + spacer("--", 15) + "|" + spacer("--", 16) + "|" + spacer(GameMaster.gameMaster.greenMonsterFireballSpeedMultiplier.ToString(), 17) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + "  Spider    |" + spacer(GameMaster.gameMaster.spiderHealthMultiplier.ToString(), 10) + "|" + spacer(GameMaster.gameMaster.spiderSpeedMultiplier.ToString(), 9) + "|" + spacer(GameMaster.gameMaster.spiderAttackPowerMultiplier.ToString(), 10) + "|" + spacer("--", 15) + "|" + spacer("--", 15) + "|" + spacer(GameMaster.gameMaster.spiderAttackSpeedMultiplier.ToString(), 16) + "|" + spacer("--", 17) + "|");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
                System.IO.File.AppendAllText(Application.dataPath + "/Output/Sessions/test " + fileName, System.Environment.NewLine + " ");
            }

            trial++;
            GameMaster.gameMaster.trial = trial;
        }
    }
}

public class damageStats
{

    public string type;

    public int lastKilled;
    public int killed;
    public int killedAccumulatedAfterDeath;

    public int lastQuanitityDamaged;
    public int quanitityDamaged;
    public int quanitityDamagedAccumulatedAfterDeath;


    public float lastTotalDamage;
    public float totalDamage;
    public float totalDamageAccumulatedAfterDeath;


    public float damage;
    public float damageOverall;


}
