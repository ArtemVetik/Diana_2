public static class ConstantKeys
{
    public static class GlobalKeys
    {
        public const float CharacterMovingDuration = 0.25f;
        public const float PreMessageDelay = 0.3f;
        public const float FadingDuration = 0.35f;
        public const float SceneLoadFadingDuration = 1;
        public const float AnimationMixDuration = 1;
        public const float BackgroundMoveDuration = 4f;
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
        Granny
    }

    public enum Emotions
    {
        confusion = 1,
        surprise_positive,
        surprise_negative,
        fear,
        interest,
        disappointment,
        sad,
        smiling,
        embarrasment,
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
        despair,
        mobile_talk_suspicion,
        anxiety,
        kind_look,
        nervous,
        ñold_view,
        ñuriosity
    }

    public enum Backgrounds
    {
        OP_stair_night,
        OP_stair_day,
        OP_photo_album,
        OP_opened_album,
        OP_kitchen_night_wide,
        OP_kitchen_day_wide,
        OP_kitchen2_day,
        OP_kitchen2_night,
        OP_incoming_call,
        OP_door_opened_night_wide,
        OP_door_opened_day_wide,
        OP_door_closed_night_wide,
        OP_door_closed_day_wide,
        OP_attic,
        OP_bedroom_night_wide,
        OP_bedroom_morning_wide,
        OP_basket,
        OP_DV_house1_night_wide,
        OP_DV_house1_day_wide,
        OP_knopas_room_day,
        OP_knopas_room_night,
        OP_DV_window_day,
        OP_DV_window_night,
        OP_DV_secret_door_night_wide,
        OP_DV_secret_door_day_wide,
        OP_DV_house_night_wide,
        OP_DV_house_day_wide,
        OP_Mateo_house_day,
        OP_Mateo_house_night,
        OP_Dianas_house_day,
        OP_Dianas_house_night,
        OP_Dianas_backyard_day,
        OP_Dianas_backyard_night,
        OP_porch_gift,
        OP_box_table,
        OP_box_opened,
        OP_Mateos_kitchen_day,
        OP_Mateos_kitchen_night
    }
}
