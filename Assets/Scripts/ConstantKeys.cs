using UnityEngine;

public static class ConstantKeys
{
    public static class GlobalKeys
    {
        public const float Delay = 0.5f;
    }

    public enum PhraseTypes
    {
        Thought = 0,
        Speech = 1,
        Author = 2,
        Choise = 3,
        Npc = 4
    }

    public enum Characters
    {
        Diana,
        Dimitriy,
        Vanessa,
        Mateo,
        Knopa,
        Nurse,
        None
    }

    public enum Emotions
    {
        none,
        confusion,
        surprise_positive,
        surprise_negative,
        fear,
        interest,
        disappointment,
        sad,
        smiling,
        embarrassment,
        delight,
        anger,
        aggression,
        normal,
        brooding,
        fun,
        disgust,
        disturbance,
        silent_agreement,
        flirting,
        dreaminess,
        mobile_talk_lively,
        mobile_talk_dreaminess,
        mobile_talk_agressive,
        mobile_talk_ironic,
        mobile_talk_listen,
        wet_smile,
        latent_anger,
        sorrow,
        suffering,
        surprise,
        wish,
        happiness,
        ironic,
        suspicion,
        contempt,
        ultimatum,
        mobile_talk_smiling,
        wide_smile_mobile_talk,
        despair
    }

    public enum Backgrounds
    {
        cherdak,
        photo_album,
        opened_album,
        stair_day,
        door_closed_day_wide,
        door_closed_night_wide,
        door_opened_day_wide,
        door_opened_night_wide,
        badroom_morning_wide,
        badroom_night_wide
    }
}
